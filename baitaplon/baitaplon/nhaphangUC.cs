using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace baitaplon
{
    public partial class nhaphangUC : UserControl
    {
        private readonly string connectionString =
            @"Data Source=LAPTOP-31QPVUR4;Initial Catalog=QL_BanHang;Integrated Security=True;TrustServerCertificate=True";

        private int currentMaPN = -1;

        public nhaphangUC()
        {
            InitializeComponent();

            // Load event
            this.Load += nhaphangUC_Load;

            // (Nếu bạn chưa gán click trong Designer thì gán ở đây luôn cho chắc)
            btnTim.Click += btnTimKiem_Click;
            btnLammoi.Click += btnLamMoi_Click;

            // TextChanged (nếu muốn search realtime thì dùng)
            txtTimkiem.TextChanged += txtTimkiem_TextChanged;
        }

        private void nhaphangUC_Load(object sender, EventArgs e)
        {
            SetupGrids();
            ClearPreview();

            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnXuat.Enabled = false;

            // ✅ Bọc try/catch để UC không bị crash khi load
            try
            {
                LoadDanhSachPhieuNhap();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không load được danh sách phiếu nhập.\n\n" +
                    "Hãy kiểm tra: connectionString, bảng PhieuNhap/ChiTietPhieuNhap có tồn tại.\n\n" +
                    "Chi tiết lỗi:\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error
                );
            }
        }

        // =========================
        // 1) SETUP 2 GRID
        // =========================
        private void SetupGrids()
        {
            dgvNhaphang.ReadOnly = true;
            dgvNhaphang.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvNhaphang.MultiSelect = false;
            dgvNhaphang.AllowUserToAddRows = false;
            dgvNhaphang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvNhaphang.RowHeadersVisible = false;

            // ✅ Gán đúng event: CellClick -> handler CellClick
            dgvNhaphang.CellClick -= dgvNhaphang_CellContentClick;
            dgvNhaphang.CellClick += dgvNhaphang_CellContentClick;

            dgvChitiet.ReadOnly = true;
            dgvChitiet.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvChitiet.MultiSelect = false;
            dgvChitiet.AllowUserToAddRows = false;
            dgvChitiet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvChitiet.RowHeadersVisible = false;
        }

        // =========================
        // 2) LOAD DANH SÁCH PHIẾU NHẬP -> dgvNhaphang
        // =========================
        private void LoadDanhSachPhieuNhap(string keyword = "")
        {
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();

                string sql = @"
SELECT 
    MaPN,
    NgayNhap,
    MaNCC,
    GhiChu,
    TongTien
FROM PhieuNhap
WHERE (@kw = '' 
       OR CAST(MaPN AS NVARCHAR) LIKE '%' + @kw + '%'
       OR ISNULL(GhiChu,'') LIKE N'%' + @kw + '%')
ORDER BY MaPN DESC;";

                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@kw", keyword ?? "");

                    var da = new SqlDataAdapter(cmd);
                    var dt = new DataTable();
                    da.Fill(dt);

                    dgvNhaphang.DataSource = dt;

                    if (dgvNhaphang.Columns.Contains("MaPN")) dgvNhaphang.Columns["MaPN"].HeaderText = "Mã phiếu";
                    if (dgvNhaphang.Columns.Contains("NgayNhap"))
                    {
                        dgvNhaphang.Columns["NgayNhap"].HeaderText = "Ngày nhập";
                        dgvNhaphang.Columns["NgayNhap"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                    }
                    if (dgvNhaphang.Columns.Contains("MaNCC")) dgvNhaphang.Columns["MaNCC"].HeaderText = "Mã NCC";
                    if (dgvNhaphang.Columns.Contains("GhiChu")) dgvNhaphang.Columns["GhiChu"].HeaderText = "Ghi chú";
                    if (dgvNhaphang.Columns.Contains("TongTien"))
                    {
                        dgvNhaphang.Columns["TongTien"].HeaderText = "Tổng tiền";
                        dgvNhaphang.Columns["TongTien"].DefaultCellStyle.Format = "#,##0";
                    }
                }
            }
        }

        // =========================
        // 3) CLICK 1 PHIẾU -> LOAD PREVIEW + LOAD DETAIL
        // =========================
        private void dgvNhaphang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvNhaphang.DataSource == null) return;

            var row = dgvNhaphang.Rows[e.RowIndex];
            if (row == null) return;

            var cellVal = row.Cells["MaPN"]?.Value;
            if (cellVal == null || cellVal == DBNull.Value) return;

            currentMaPN = Convert.ToInt32(cellVal);

            LoadPreviewHeaderFromGrid(row);
            LoadChiTietPhieuNhap(currentMaPN);

            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnXuat.Enabled = true;
        }

        private void LoadPreviewHeaderFromGrid(DataGridViewRow row)
        {
            lblMaPN.Text = "Mã phiếu: " + (row.Cells["MaPN"].Value?.ToString() ?? "");

            if (row.Cells["NgayNhap"].Value != null && DateTime.TryParse(row.Cells["NgayNhap"].Value.ToString(), out DateTime ngayNhap))
                lblNgayNhap.Text = "Ngày nhập: " + ngayNhap.ToString("dd/MM/yyyy HH:mm");
            else
                lblNgayNhap.Text = "Ngày nhập:";

            lblNCC.Text = "Nhà cung cấp (Mã NCC): " + (row.Cells["MaNCC"].Value?.ToString() ?? "");
            lblGhiChu.Text = "Ghi chú: " + (row.Cells["GhiChu"].Value?.ToString() ?? "");

            decimal tongTien = 0;
            if (row.Cells["TongTien"].Value != null) decimal.TryParse(row.Cells["TongTien"].Value.ToString(), out tongTien);
            lblTongTien.Text = "Tổng tiền: " + tongTien.ToString("#,##0") + " đ";
        }

        // =========================
        // 4) LOAD CHI TIẾT -> dgvChitiet + tính tổng
        // =========================
        private void LoadChiTietPhieuNhap(int maPN)
        {
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();

                string sql = @"
SELECT 
    MaCT,
    MaPN,
    MaSP,
    SoLuong,
    DonGia,
    ThanhTien
FROM ChiTietPhieuNhap
WHERE MaPN = @maPN
ORDER BY MaCT;";

                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@maPN", maPN);

                    var da = new SqlDataAdapter(cmd);
                    var dt = new DataTable();
                    da.Fill(dt);

                    dt.Columns.Add("STT", typeof(int));
                    for (int i = 0; i < dt.Rows.Count; i++) dt.Rows[i]["STT"] = i + 1;
                    dt.Columns["STT"].SetOrdinal(0);

                    dgvChitiet.DataSource = dt;

                    if (dgvChitiet.Columns.Contains("STT")) dgvChitiet.Columns["STT"].HeaderText = "STT";
                    if (dgvChitiet.Columns.Contains("MaSP")) dgvChitiet.Columns["MaSP"].HeaderText = "Mã SP";
                    if (dgvChitiet.Columns.Contains("SoLuong")) dgvChitiet.Columns["SoLuong"].HeaderText = "Số lượng";
                    if (dgvChitiet.Columns.Contains("DonGia"))
                    {
                        dgvChitiet.Columns["DonGia"].HeaderText = "Đơn giá";
                        dgvChitiet.Columns["DonGia"].DefaultCellStyle.Format = "#,##0";
                    }
                    if (dgvChitiet.Columns.Contains("ThanhTien"))
                    {
                        dgvChitiet.Columns["ThanhTien"].HeaderText = "Thành tiền";
                        dgvChitiet.Columns["ThanhTien"].DefaultCellStyle.Format = "#,##0";
                    }

                    // ẩn cột kỹ thuật
                    if (dgvChitiet.Columns.Contains("MaCT")) dgvChitiet.Columns["MaCT"].Visible = false;
                    if (dgvChitiet.Columns.Contains("MaPN")) dgvChitiet.Columns["MaPN"].Visible = false;

                    int totalQty = 0;
                    decimal totalMoney = 0;

                    foreach (DataRow r in dt.Rows)
                    {
                        totalQty += Convert.ToInt32(r["SoLuong"]);
                        if (r["ThanhTien"] != DBNull.Value)
                            totalMoney += Convert.ToDecimal(r["ThanhTien"]);
                        else
                            totalMoney += Convert.ToDecimal(r["SoLuong"]) * Convert.ToDecimal(r["DonGia"]);
                    }

                    lblTongSoLuong.Text = "Tổng số lượng: " + totalQty;
                    lblTongTien.Text = "Tổng tiền: " + totalMoney.ToString("#,##0") + " đ";
                }
            }
        }

        // =========================
        // 5) CLEAR PREVIEW
        // =========================
        private void ClearPreview()
        {
            lblMaPN.Text = "Mã phiếu:";
            lblNgayNhap.Text = "Ngày nhập:";
            lblNCC.Text = "Nhà cung cấp:";
            lblGhiChu.Text = "Ghi chú:";

            dgvChitiet.DataSource = null;

            lblTongSoLuong.Text = "Tổng số lượng: 0";
            lblTongTien.Text = "Tổng tiền: 0 đ";
        }

        // =========================
        // 6) SEARCH + REFRESH
        // =========================
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            LoadDanhSachPhieuNhap(txtTimkiem.Text.Trim());
            ClearPreview();
            currentMaPN = -1;

            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnXuat.Enabled = false;
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTimkiem.Clear();
            LoadDanhSachPhieuNhap();
            ClearPreview();
            currentMaPN = -1;

            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnXuat.Enabled = false;
        }

        // Nếu bạn không muốn search realtime thì để trống hoặc bỏ event này
        private void txtTimkiem_TextChanged(object sender, EventArgs e)
        {
            // Ví dụ muốn gõ tới đâu lọc tới đó:
            // LoadDanhSachPhieuNhap(txtTimkiem.Text.Trim());
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // để trống cũng được, miễn là có hàm
        }

        private void btnTaophieu_Click(object sender, EventArgs e)
        {
            using (var f = new phieunhap())
            {
                f.ShowDialog();

                if (f.Saved)
                {
                    LoadDanhSachPhieuNhap(); // reload dgvNhaphang
                    ClearPreview();
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (currentMaPN <= 0)
            {
                MessageBox.Show("Vui lòng chọn 1 phiếu nhập để xóa!");
                return;
            }

            var rs = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa phiếu nhập #{currentMaPN} không?\n\n" +
                "Lưu ý: sẽ xóa toàn bộ chi tiết phiếu nhập.",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (rs != DialogResult.Yes) return;

            try
            {
                DeletePhieuNhap(currentMaPN);

                MessageBox.Show("Xóa phiếu nhập thành công!");

                // reset UI
                currentMaPN = -1;
                LoadDanhSachPhieuNhap();
                ClearPreview();

                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                btnXuat.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xóa phiếu nhập thất bại!\n\n" + ex.Message);
            }
        }
        private void DeletePhieuNhap(int maPN)
        {
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                using (var tran = con.BeginTransaction())
                {
                    try
                    {
                        // 1) Xóa chi tiết trước
                        using (var cmd = new SqlCommand(
                            "DELETE FROM ChiTietPhieuNhap WHERE MaPN = @MaPN", con, tran))
                        {
                            cmd.Parameters.AddWithValue("@MaPN", maPN);
                            cmd.ExecuteNonQuery();
                        }

                        // 2) Xóa phiếu
                        using (var cmd = new SqlCommand(
                            "DELETE FROM PhieuNhap WHERE MaPN = @MaPN", con, tran))
                        {
                            cmd.Parameters.AddWithValue("@MaPN", maPN);
                            int affected = cmd.ExecuteNonQuery();

                            if (affected == 0)
                                throw new Exception("Không tìm thấy phiếu nhập để xóa (có thể đã bị xóa trước đó).");
                        }

                        tran.Commit();
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }
        }

    }
}
