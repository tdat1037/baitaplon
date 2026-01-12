using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace baitaplon
{
    public partial class Formbanhang : Form
    {
        private readonly string connectionString =
            @"Data Source=LAPTOP-31QPVUR4;Initial Catalog=QL_BanHang;Integrated Security=True;TrustServerCertificate=True";

        public bool Saved { get; private set; } = false;

        public Formbanhang()
        {
            InitializeComponent();

            this.Load += Formbanhang_Load;
            txtSL.KeyPress += OnlyNumber_KeyPress;
            txtGia.KeyPress += OnlyNumber_KeyPress;

            // Khi chọn sản phẩm -> tự đổ giá bán
            cboSP.SelectionChangeCommitted += cboSP_SelectionChangeCommitted;
        }

        private void Formbanhang_Load(object sender, EventArgs e)
        {
            SetupCartGrid();
            LoadKhachHang();
            LoadSanPham();
        }


        private void LoadKhachHang()
        {
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();

                string sql = @"SELECT MaKH, TenKH FROM KhachHang ORDER BY TenKH;";

                var da = new SqlDataAdapter(sql, con);
                var dt = new DataTable();
                da.Fill(dt);

                cboKH.DisplayMember = "TenKH";
                cboKH.ValueMember = "MaKH";
                cboKH.DataSource = dt;
            }
        }

        private void LoadSanPham()
        {
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();

                // ✅ theo DB của bạn: Products(Ma, Ten, Giaban, Tonkho)
                string sql = @"
SELECT 
    Ma   AS MaSP,
    Ten  AS TenSP,
    Giaban,
    Tonkho
FROM Products
ORDER BY Ten;";

                var da = new SqlDataAdapter(sql, con);
                var dt = new DataTable();
                da.Fill(dt);

                cboSP.DisplayMember = "TenSP";
                cboSP.ValueMember = "MaSP";
                cboSP.DataSource = dt;

                // set giá bán lần đầu
                SetGiaBanTheoSanPhamDangChon();
            }
        }

        private void cboSP_SelectionChangeCommitted(object sender, EventArgs e)
        {
            SetGiaBanTheoSanPhamDangChon();
        }

        private void SetGiaBanTheoSanPhamDangChon()
        {
            if (cboSP.SelectedItem is DataRowView rv)
            {
                // tự điền giá bán
                txtGia.Text = rv["Giaban"].ToString();
            }
        }

        // =========================
        // C) SETUP GRID GIỎ HÀNG
        // =========================
        private void SetupCartGrid()
        {
            dgvBan.AutoGenerateColumns = false;
            dgvBan.AllowUserToAddRows = false;
            dgvBan.RowHeadersVisible = false;
            dgvBan.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBan.MultiSelect = false;

            dgvBan.Columns.Clear();

            dgvBan.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MaSP",
                HeaderText = "Mã SP",
                DataPropertyName = "MaSP",
                ReadOnly = true
            });

            dgvBan.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TenSP",
                HeaderText = "Tên SP",
                DataPropertyName = "TenSP",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dgvBan.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SoLuong",
                HeaderText = "Số lượng",
                DataPropertyName = "SoLuong",
                ReadOnly = true,
                Width = 90
            });

            dgvBan.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DonGia",
                HeaderText = "Giá bán",
                DataPropertyName = "DonGia",
                ReadOnly = true,
                Width = 110,
                DefaultCellStyle = { Format = "#,##0" }
            });

            dgvBan.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ThanhTien",
                HeaderText = "Thành tiền",
                DataPropertyName = "ThanhTien",
                ReadOnly = true,
                Width = 120,
                DefaultCellStyle = { Format = "#,##0" }
            });

            var btnDel = new DataGridViewButtonColumn
            {
                Name = "Xoa",
                HeaderText = "",
                Text = "Xóa",
                UseColumnTextForButtonValue = true,
                Width = 60
            };
            dgvBan.Columns.Add(btnDel);

            dgvBan.CellContentClick += dgvBan_CellContentClick;

            var cart = new DataTable();
            cart.Columns.Add("MaSP", typeof(string));
            cart.Columns.Add("TenSP", typeof(string));
            cart.Columns.Add("SoLuong", typeof(int));
            cart.Columns.Add("DonGia", typeof(decimal));
            cart.Columns.Add("ThanhTien", typeof(decimal));

            dgvBan.DataSource = cart;
        }

        private void dgvBan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (dgvBan.Columns[e.ColumnIndex].Name != "Xoa") return;
            if (e.RowIndex >= dgvBan.Rows.Count) return;
            if (dgvBan.Rows[e.RowIndex].IsNewRow) return;

            dgvBan.Rows.RemoveAt(e.RowIndex);
        }

        // =========================
        // D) THÊM SP VÀO GIỎ
        // =========================

        private void ClearInputLine()
        {
            txtSL.Clear();
            // txtGia giữ lại cũng được, nhưng mình để người dùng nhập tiếp
            txtSL.Focus();
        }

        private void OnlyNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;
            if (!char.IsDigit(e.KeyChar)) e.Handled = true;
        }

        // =========================
        // E) LƯU HÓA ĐƠN + TRỪ TỒN KHO
        // =========================
        
        private void btnThemSP_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (cboSP.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn sản phẩm!");
                    return;
                }

                if (!(cboSP.SelectedItem is DataRowView rv))
                {
                    MessageBox.Show("Dữ liệu sản phẩm chưa load đúng (SelectedItem không hợp lệ).");
                    return;
                }

                string maSP = cboSP.SelectedValue.ToString();       // "MS1"
                string tenSP = rv["TenSP"].ToString();              // alias từ query
                int tonKho = Convert.ToInt32(rv["Tonkho"]);          // Products.Tonkho
                decimal giaBan;

                if (!int.TryParse(txtSL.Text.Trim(), out int sl) || sl <= 0)
                {
                    MessageBox.Show("Số lượng phải là số > 0");
                    return;
                }

                // nếu bạn muốn tự lấy giá từ DB, cho phép bỏ trống txtGia
                if (string.IsNullOrWhiteSpace(txtGia.Text))
                {
                    giaBan = Convert.ToDecimal(rv["Giaban"]);
                    txtGia.Text = giaBan.ToString("0");
                }
                else if (!decimal.TryParse(txtGia.Text.Trim(), out giaBan) || giaBan < 0)
                {
                    MessageBox.Show("Giá bán không hợp lệ!");
                    return;
                }

                // check tồn
                if (sl > tonKho)
                {
                    MessageBox.Show($"Không đủ tồn kho! Tồn hiện tại: {tonKho}");
                    return;
                }

                var dt = dgvBan.DataSource as DataTable;
                if (dt == null)
                {
                    MessageBox.Show("Giỏ hàng chưa được khởi tạo DataSource!");
                    return;
                }

                // nếu trùng SP -> cộng dồn
                foreach (DataRow r in dt.Rows)
                {
                    if (r["MaSP"].ToString() == maSP)
                    {
                        int old = Convert.ToInt32(r["SoLuong"]);
                        int newQty = old + sl;

                        if (newQty > tonKho)
                        {
                            MessageBox.Show($"Không đủ tồn kho! Tồn hiện tại: {tonKho}");
                            return;
                        }

                        r["SoLuong"] = newQty;
                        r["DonGia"] = giaBan;
                        r["ThanhTien"] = newQty * giaBan;

                        txtSL.Clear();
                        txtSL.Focus();
                        return;
                    }
                }

                // thêm mới
                dt.Rows.Add(maSP, tenSP, sl, giaBan, sl * giaBan);

                txtSL.Clear();
                txtSL.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm sản phẩm:\n" + ex.Message);
            }
        }

        private void btnLuuPhieu_Click(object sender, EventArgs e)
        {
            try
            {
                var cart = dgvBan.DataSource as DataTable;
                if (cart == null || cart.Rows.Count == 0)
                {
                    MessageBox.Show("Vui lòng thêm ít nhất 1 sản phẩm!");
                    return;
                }

                int maKH = 1; // mặc định Khách lẻ
                if (cboKH.SelectedValue != null) maKH = Convert.ToInt32(cboKH.SelectedValue);

                decimal tongTien = 0;
                foreach (DataRow r in cart.Rows)
                    tongTien += Convert.ToDecimal(r["ThanhTien"]);

                using (var con = new SqlConnection(connectionString))
                {
                    con.Open();
                    using (var tran = con.BeginTransaction())
                    {
                        try
                        {
                            // 1) tạo hóa đơn
                            string sqlHD = @"
INSERT INTO HoaDon(NgayBan, MaKH, TongTien, GhiChu)
VALUES (GETDATE(), @MaKH, @TongTien, NULL);
SELECT SCOPE_IDENTITY();";

                            int maHD;
                            using (var cmd = new SqlCommand(sqlHD, con, tran))
                            {
                                // nếu bạn muốn Khách lẻ lưu NULL thì giữ dòng dưới
                                cmd.Parameters.AddWithValue("@MaKH", maKH == 1 ? (object)DBNull.Value : maKH);
                                cmd.Parameters.AddWithValue("@TongTien", tongTien);
                                maHD = Convert.ToInt32(cmd.ExecuteScalar());
                            }

                            // 2) insert chi tiết
                            string sqlCT = @"
INSERT INTO ChiTietHoaDon(MaHD, MaSP, SoLuong, DonGia)
VALUES (@MaHD, @MaSP, @SoLuong, @DonGia);";

                            // 3) trừ tồn kho theo Products.Ma
                            string sqlTruTon = @"
UPDATE Products
SET Tonkho = Tonkho - @SoLuong
WHERE Ma = @MaSP AND Tonkho >= @SoLuong;";

                            foreach (DataRow r in cart.Rows)
                            {
                                string maSP = r["MaSP"].ToString(); // "MS1"
                                int soLuong = Convert.ToInt32(r["SoLuong"]);
                                decimal donGia = Convert.ToDecimal(r["DonGia"]);

                                using (var cmd = new SqlCommand(sqlCT, con, tran))
                                {
                                    cmd.Parameters.AddWithValue("@MaHD", maHD);
                                    cmd.Parameters.AddWithValue("@MaSP", maSP);
                                    cmd.Parameters.AddWithValue("@SoLuong", soLuong);
                                    cmd.Parameters.AddWithValue("@DonGia", donGia);
                                    cmd.ExecuteNonQuery();
                                }

                                using (var cmd = new SqlCommand(sqlTruTon, con, tran))
                                {
                                    cmd.Parameters.AddWithValue("@MaSP", maSP);
                                    cmd.Parameters.AddWithValue("@SoLuong", soLuong);

                                    int affected = cmd.ExecuteNonQuery();
                                    if (affected == 0)
                                        throw new Exception($"Không đủ tồn kho để bán sản phẩm {maSP}.");
                                }
                            }

                            tran.Commit();
                            Saved = true;
                            MessageBox.Show("Lưu hóa đơn thành công!");
                            this.Close();
                        }
                        catch
                        {
                            tran.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu hóa đơn:\n" + ex.Message);
            }
        }
    }
}
