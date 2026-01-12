using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace baitaplon
{
    public partial class phieunhap : Form
    {
        private readonly string connectionString =
            @"Data Source=LAPTOP-31QPVUR4;Initial Catalog=QL_BanHang;Integrated Security=True;TrustServerCertificate=True";

        // để form UC bên ngoài biết cần reload
        public bool Saved { get; private set; } = false;
        public phieunhap()
        {
            InitializeComponent();
            this.Load += phieunhap_Load;
            txtSL.KeyPress += OnlyNumber_KeyPress;
            txtGianhap.KeyPress += OnlyNumber_KeyPress;
        }
        private void phieunhap_Load(object sender, EventArgs e)
        {
            SetupCartGrid();
            LoadNCC();
            LoadSanPham();
        }
        private void LoadNCC()
        {
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();

                // ⚠️ Nếu bảng NCC của bạn tên khác thì đổi ở đây
                string sql = @"SELECT MaNCC, TenNCC FROM NhaCungCap ORDER BY TenNCC";

                var da = new SqlDataAdapter(sql, con);
                var dt = new DataTable();
                da.Fill(dt);

                cboNCC.DisplayMember = "TenNCC";
                cboNCC.ValueMember = "MaNCC";
                cboNCC.DataSource = dt;
            }
        }
        private void LoadSanPham()
        {
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();

                // ✅ đúng tên bảng + đúng cột theo DB của bạn
                string sql = @"
            SELECT 
                Ma AS MaSP,
                Ten AS TenSP
            FROM Products
            ORDER BY Ten";

                var da = new SqlDataAdapter(sql, con);
                var dt = new DataTable();
                da.Fill(dt);

                cboSP.DisplayMember = "TenSP";   // hiển thị tên
                cboSP.ValueMember = "MaSP";      // lấy mã (MS1, MS2...)
                cboSP.DataSource = dt;
            }
        }


        private void SetupCartGrid()
        {
            dgvNhap.AutoGenerateColumns = false;
            dgvNhap.AllowUserToAddRows = false;
            dgvNhap.RowHeadersVisible = false;
            dgvNhap.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvNhap.MultiSelect = false;

            dgvNhap.Columns.Clear();

            dgvNhap.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MaSP",
                HeaderText = "Mã SP",
                DataPropertyName = "MaSP",
                ReadOnly = true
            });

            dgvNhap.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TenSP",
                HeaderText = "Tên SP",
                DataPropertyName = "TenSP",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dgvNhap.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SoLuong",
                HeaderText = "SL",
                DataPropertyName = "SoLuong",
                ReadOnly = true
            });

            dgvNhap.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DonGia",
                HeaderText = "Giá nhập",
                DataPropertyName = "DonGia",
                ReadOnly = true,
                DefaultCellStyle = { Format = "#,##0" }
            });

            // Cột nút xóa
            var btnDel = new DataGridViewButtonColumn
            {
                Name = "Xoa",
                HeaderText = "",
                Text = "Xóa",
                UseColumnTextForButtonValue = true,
                Width = 60
            };
            dgvNhap.Columns.Add(btnDel);

            dgvNhap.CellContentClick += dgvNhap_CellContentClick;

            // tạo DataTable làm nguồn dữ liệu
            var cart = new DataTable();
            cart.Columns.Add("MaSP", typeof(string));
            cart.Columns.Add("TenSP", typeof(string));
            cart.Columns.Add("SoLuong", typeof(int));
            cart.Columns.Add("DonGia", typeof(decimal));

            dgvNhap.DataSource = cart;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (cboNCC.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp!");
                return;
            }

            var cart = (DataTable)dgvNhap.DataSource;
            if (cart.Rows.Count == 0)
            {
                MessageBox.Show("Vui lòng thêm ít nhất 1 sản phẩm!");
                return;
            }

            int maNCC = Convert.ToInt32(cboNCC.SelectedValue);

            // tính tổng tiền
            decimal tongTien = 0;
            foreach (DataRow r in cart.Rows)
            {
                int sl = Convert.ToInt32(r["SoLuong"]);
                decimal gia = Convert.ToDecimal(r["DonGia"]);
                tongTien += sl * gia;
            }

            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                using (var tran = con.BeginTransaction())
                {
                    try
                    {
                        // 1) Insert PhieuNhap
                        string sqlPN = @"
INSERT INTO PhieuNhap(NgayNhap, MaNCC, GhiChu, TongTien)
VALUES (GETDATE(), @MaNCC, NULL, @TongTien);
SELECT SCOPE_IDENTITY();";

                        int maPN;
                        using (var cmd = new SqlCommand(sqlPN, con, tran))
                        {
                            cmd.Parameters.AddWithValue("@MaNCC", maNCC);
                            cmd.Parameters.AddWithValue("@TongTien", tongTien);
                            maPN = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        // 2) Insert ChiTietPhieuNhap
                        string sqlCT = @"
INSERT INTO ChiTietPhieuNhap(MaPN, MaSP, SoLuong, DonGia)
VALUES (@MaPN, @MaSP, @SoLuong, @DonGia);";

                        foreach (DataRow r in cart.Rows)
                        {
                            using (var cmd = new SqlCommand(sqlCT, con, tran))
                            {
                                cmd.Parameters.AddWithValue("@MaPN", maPN);
                                cmd.Parameters.AddWithValue("@MaSP", r["MaSP"].ToString());
                                cmd.Parameters.AddWithValue("@SoLuong", Convert.ToInt32(r["SoLuong"]));
                                cmd.Parameters.AddWithValue("@DonGia", Convert.ToDecimal(r["DonGia"]));
                                cmd.ExecuteNonQuery();
                            }
                        }

                        tran.Commit();
                        Saved = true;
                        MessageBox.Show("Lưu phiếu nhập thành công!");
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        MessageBox.Show("Lưu phiếu thất bại!\n\n" + ex.Message);
                    }
                }
            }
        }

        private void dgvNhap_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // click header -> bỏ
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            // chỉ xử lý khi click đúng cột Xoa
            if (dgvNhap.Columns[e.ColumnIndex].Name != "Xoa") return;

            // đảm bảo index hợp lệ
            if (e.RowIndex >= dgvNhap.Rows.Count) return;

            // nếu có dòng new row thì tránh remove nhầm
            if (dgvNhap.Rows[e.RowIndex].IsNewRow) return;

            dgvNhap.Rows.RemoveAt(e.RowIndex);
        }


        private void btnThemSP_Click(object sender, EventArgs e)
        {
            if (cboSP.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm!");
                return;
            }

            string maSP = cboSP.SelectedValue.ToString();
            string tenSP = ((DataRowView)cboSP.SelectedItem)["TenSP"].ToString();

            if (!int.TryParse(txtSL.Text.Trim(), out int sl) || sl <= 0)
            {
                MessageBox.Show("Số lượng phải > 0");
                return;
            }

            if (!decimal.TryParse(txtGianhap.Text.Trim(), out decimal gia) || gia < 0)
            {
                MessageBox.Show("Giá nhập không hợp lệ!");
                return;
            }

            var dt = (DataTable)dgvNhap.DataSource;

            // nếu trùng SP thì cộng dồn SL
            foreach (DataRow r in dt.Rows)
            {
                if (r["MaSP"].ToString() == maSP)
                {
                    int old = Convert.ToInt32(r["SoLuong"]);
                    r["SoLuong"] = old + sl;
                    r["DonGia"] = gia; // cập nhật giá mới nhất
                    ClearInputLine();
                    return;
                }
            }

            // thêm dòng mới
            dt.Rows.Add(maSP, tenSP, sl, gia);
            ClearInputLine();
        }
        private void ClearInputLine()
        {
            txtSL.Clear();
            txtGianhap.Clear();
            txtSL.Focus();
        }
        private void OnlyNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            // cho phép backspace
            if (char.IsControl(e.KeyChar)) return;

            // chỉ cho số
            if (!char.IsDigit(e.KeyChar))
                e.Handled = true;
        }
    }
}
