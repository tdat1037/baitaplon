using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Printing;


namespace baitaplon
{
    public partial class banhangUC : UserControl
    {
        private readonly string connectionString =
            @"Data Source=LAPTOP-31QPVUR4;Initial Catalog=QL_BanHang;Integrated Security=True;TrustServerCertificate=True";

        private int currentMaHD = -1;
        private PrintDocument printDoc = new PrintDocument();

        public banhangUC()
        {
            InitializeComponent();
            this.Load += banhangUC_Load;
        }

        private void banhangUC_Load(object sender, EventArgs e)
        {
            SetupGrids();
            ClearPreview();
            LoadDanhSachHoaDon();
        }

        private void SetupGrids()
        {
            dgvHoadon.ReadOnly = true;
            dgvHoadon.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHoadon.MultiSelect = false;
            dgvHoadon.AllowUserToAddRows = false;
            dgvHoadon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvHoadon.RowHeadersVisible = false;

            dgvHoadon.CellClick -= dgvHoadon_CellContentClick;
            dgvHoadon.CellClick += dgvHoadon_CellContentClick;

            dgvChitiet.ReadOnly = true;
            dgvChitiet.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvChitiet.MultiSelect = false;
            dgvChitiet.AllowUserToAddRows = false;
            dgvChitiet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvChitiet.RowHeadersVisible = false;
        }

        private void LoadDanhSachHoaDon(string keyword = "")
        {
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();

                string sql = @"
SELECT 
    hd.MaHD,
    hd.NgayBan,
    ISNULL(kh.TenKH, N'Khách lẻ') AS TenKH,
    hd.TongTien
FROM HoaDon hd
LEFT JOIN KhachHang kh ON hd.MaKH = kh.MaKH
WHERE (@kw = '' 
    OR CAST(hd.MaHD AS NVARCHAR) LIKE '%' + @kw + '%'
    OR ISNULL(kh.TenKH,'') LIKE N'%' + @kw + '%')
ORDER BY hd.MaHD DESC;";

                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@kw", keyword ?? "");

                    var da = new SqlDataAdapter(cmd);
                    var dt = new DataTable();
                    da.Fill(dt);

                    dgvHoadon.DataSource = dt;

                    if (dgvHoadon.Columns.Contains("MaHD")) dgvHoadon.Columns["MaHD"].HeaderText = "Mã HĐ";

                    if (dgvHoadon.Columns.Contains("NgayBan"))
                    {
                        dgvHoadon.Columns["NgayBan"].HeaderText = "Ngày bán";
                        dgvHoadon.Columns["NgayBan"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                    }

                    if (dgvHoadon.Columns.Contains("TenKH"))
                        dgvHoadon.Columns["TenKH"].HeaderText = "Khách hàng";

                    if (dgvHoadon.Columns.Contains("TongTien"))
                    {
                        dgvHoadon.Columns["TongTien"].HeaderText = "Tổng tiền";
                        dgvHoadon.Columns["TongTien"].DefaultCellStyle.Format = "#,##0";
                    }
                }
            }
        }

        // ✅ LOAD HEADER HÓA ĐƠN -> ĐỔ LÊN LABEL
        private void LoadHoaDonHeader(int maHD)
        {
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();

                string sql = @"
SELECT 
    hd.MaHD,
    hd.NgayBan,
    ISNULL(kh.TenKH, N'Khách lẻ') AS TenKH,
    ISNULL(hd.GhiChu, N'') AS GhiChu
FROM HoaDon hd
LEFT JOIN KhachHang kh ON hd.MaKH = kh.MaKH
WHERE hd.MaHD = @maHD;";

                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@maHD", maHD);

                    using (var r = cmd.ExecuteReader())
                    {
                        if (!r.Read())
                        {
                            // không có hóa đơn
                            lblMaHD.Text = "";
                            lblNgayBan.Text = "";
                            lblKhachHang.Text = "";
                            lblGhiChu.Text = "";
                            return;
                        }

                        lblMaHD.Text = r["MaHD"].ToString();
                        lblNgayBan.Text = Convert.ToDateTime(r["NgayBan"]).ToString("dd/MM/yyyy HH:mm");
                        lblKhachHang.Text = r["TenKH"].ToString();
                        lblGhiChu.Text = r["GhiChu"].ToString();
                    }
                }
            }
        }

        private void dgvHoadon_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dgvHoadon.Rows[e.RowIndex];
            if (row.Cells["MaHD"]?.Value == null) return;

            currentMaHD = Convert.ToInt32(row.Cells["MaHD"].Value);

            // ✅ 1) đổ thông tin hóa đơn bên dưới
            LoadHoaDonHeader(currentMaHD);

            // ✅ 2) đổ chi tiết + tính tổng
            LoadChiTietHoaDon(currentMaHD);
        }

        private void LoadChiTietHoaDon(int maHD)
        {
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();

                string sql = @"
SELECT 
    ct.MaCT,
    ct.MaHD,
    ct.MaSP,
    p.Ten AS TenSP,
    ct.SoLuong,
    ct.DonGia,
    (ct.SoLuong * ct.DonGia) AS ThanhTien
FROM ChiTietHoaDon ct
LEFT JOIN Products p ON ct.MaSP = p.Ma
WHERE ct.MaHD = @maHD
ORDER BY ct.MaCT;";

                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@maHD", maHD);

                    var da = new SqlDataAdapter(cmd);
                    var dt = new DataTable();
                    da.Fill(dt);

                    // STT
                    dt.Columns.Add("STT", typeof(int));
                    for (int i = 0; i < dt.Rows.Count; i++) dt.Rows[i]["STT"] = i + 1;
                    dt.Columns["STT"].SetOrdinal(0);

                    dgvChitiet.DataSource = dt;

                    // Header
                    if (dgvChitiet.Columns.Contains("STT")) dgvChitiet.Columns["STT"].HeaderText = "STT";
                    if (dgvChitiet.Columns.Contains("MaSP")) dgvChitiet.Columns["MaSP"].HeaderText = "Mã SP";
                    if (dgvChitiet.Columns.Contains("TenSP")) dgvChitiet.Columns["TenSP"].HeaderText = "Tên SP";
                    if (dgvChitiet.Columns.Contains("SoLuong")) dgvChitiet.Columns["SoLuong"].HeaderText = "SL";

                    if (dgvChitiet.Columns.Contains("DonGia"))
                    {
                        dgvChitiet.Columns["DonGia"].HeaderText = "Giá bán";
                        dgvChitiet.Columns["DonGia"].DefaultCellStyle.Format = "#,##0";
                    }

                    if (dgvChitiet.Columns.Contains("ThanhTien"))
                    {
                        dgvChitiet.Columns["ThanhTien"].HeaderText = "Thành tiền";
                        dgvChitiet.Columns["ThanhTien"].DefaultCellStyle.Format = "#,##0";
                    }

                    // Ẩn cột kỹ thuật
                    if (dgvChitiet.Columns.Contains("MaCT")) dgvChitiet.Columns["MaCT"].Visible = false;
                    if (dgvChitiet.Columns.Contains("MaHD")) dgvChitiet.Columns["MaHD"].Visible = false;

                    // ✅ TÍNH TỔNG (tính từ chi tiết)
                    int totalQty = 0;
                    decimal totalMoney = 0;

                    foreach (DataRow r in dt.Rows)
                    {
                        totalQty += Convert.ToInt32(r["SoLuong"]);
                        totalMoney += Convert.ToDecimal(r["ThanhTien"]);
                    }

                    lblTongSL.Text = totalQty.ToString();
                    lblTongTien.Text = totalMoney.ToString("#,##0") + " đ";
                }
            }
        }

        private void ClearPreview()
        {
            dgvChitiet.DataSource = null;
            currentMaHD = -1;

            lblMaHD.Text = "";
            lblNgayBan.Text = "";
            lblKhachHang.Text = "";
            lblGhiChu.Text = "";

            lblTongSL.Text = "0";
            lblTongTien.Text = "0 đ";
        }

        // =========================
        // THÊM (MỞ FORM TẠO HÓA ĐƠN)
        // =========================
        private void btnThem_Click(object sender, EventArgs e)
        {
            using (var f = new Formbanhang())
            {
                f.ShowDialog();
                if (f.Saved)
                {
                    LoadDanhSachHoaDon(txtTimKiem.Text.Trim());
                    ClearPreview();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            btnThem_Click(sender, e);
        }



        private int GetSelectedMaHD()
        {
            if (currentMaHD > 0) return currentMaHD;

            if (dgvHoadon.CurrentRow != null && dgvHoadon.CurrentRow.Cells["MaHD"]?.Value != null)
                return Convert.ToInt32(dgvHoadon.CurrentRow.Cells["MaHD"].Value);

            return -1;
        }

        // =========================
        // XÓA (XÓA CHI TIẾT TRƯỚC, RỒI XÓA HÓA ĐƠN)
        // =========================
        private void btnXoa_Click_1(object sender, EventArgs e)
        {
            int maHD = GetSelectedMaHD();
            if (maHD <= 0)
            {
                MessageBox.Show("Bạn hãy chọn 1 hóa đơn để xóa.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show(
                $"Bạn chắc chắn muốn xóa hóa đơn #{maHD} ?\n(Hệ thống sẽ xóa cả chi tiết hóa đơn)",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            try
            {
                using (var con = new SqlConnection(connectionString))
                {
                    con.Open();
                    using (var tran = con.BeginTransaction())
                    {
                        try
                        {
                            using (var cmd1 = new SqlCommand("DELETE FROM ChiTietHoaDon WHERE MaHD = @maHD", con, tran))
                            {
                                cmd1.Parameters.AddWithValue("@maHD", maHD);
                                cmd1.ExecuteNonQuery();
                            }

                            using (var cmd2 = new SqlCommand("DELETE FROM HoaDon WHERE MaHD = @maHD", con, tran))
                            {
                                cmd2.Parameters.AddWithValue("@maHD", maHD);
                                int affected = cmd2.ExecuteNonQuery();
                                if (affected == 0)
                                    throw new Exception("Không tìm thấy hóa đơn để xóa (có thể đã bị xóa trước đó).");
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

                MessageBox.Show("Xóa hóa đơn thành công.", "OK",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadDanhSachHoaDon(txtTimKiem.Text.Trim());
                ClearPreview();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xóa thất bại: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (currentMaHD <= 0)
            {
                MessageBox.Show("Vui lòng chọn hóa đơn để xuất!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            printDoc.PrintPage -= PrintDoc_PrintPage;
            printDoc.PrintPage += PrintDoc_PrintPage;

            PrintPreviewDialog preview = new PrintPreviewDialog();
            preview.Document = printDoc;
            preview.Width = 1000;
            preview.Height = 700;

            preview.ShowDialog();
        }
        private void PrintDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            int startX = 50;
            int startY = 40;
            int offsetY = 0;

            Font titleFont = new Font("Arial", 18, FontStyle.Bold);
            Font normalFont = new Font("Arial", 11);
            Font boldFont = new Font("Arial", 11, FontStyle.Bold);

            // ======================
            // TIÊU ĐỀ
            // ======================
            g.DrawString("HÓA ĐƠN BÁN HÀNG", titleFont, Brushes.Black, startX + 200, startY);
            offsetY += 50;

            // ======================
            // THÔNG TIN HÓA ĐƠN
            // ======================
            g.DrawString($"Mã hóa đơn: {lblMaHD.Text}", normalFont, Brushes.Black, startX, startY + offsetY);
            offsetY += 25;

            g.DrawString($"Ngày bán: {lblNgayBan.Text}", normalFont, Brushes.Black, startX, startY + offsetY);
            offsetY += 25;

            g.DrawString($"Khách hàng: {lblKhachHang.Text}", normalFont, Brushes.Black, startX, startY + offsetY);
            offsetY += 25;

            if (!string.IsNullOrWhiteSpace(lblGhiChu.Text))
            {
                g.DrawString($"Ghi chú: {lblGhiChu.Text}", normalFont, Brushes.Black, startX, startY + offsetY);
                offsetY += 25;
            }

            offsetY += 10;
            g.DrawLine(Pens.Black, startX, startY + offsetY, startX + 700, startY + offsetY);
            offsetY += 20;

            // ======================
            // HEADER BẢNG
            // ======================
            int[] colX = { startX, startX + 60, startX + 170, startX + 420, startX + 500, startX + 620 };

            g.DrawString("STT", boldFont, Brushes.Black, colX[0], startY + offsetY);
            g.DrawString("Mã SP", boldFont, Brushes.Black, colX[1], startY + offsetY);
            g.DrawString("Tên SP", boldFont, Brushes.Black, colX[2], startY + offsetY);
            g.DrawString("SL", boldFont, Brushes.Black, colX[3], startY + offsetY);
            g.DrawString("Giá", boldFont, Brushes.Black, colX[4], startY + offsetY);
            g.DrawString("Thành tiền", boldFont, Brushes.Black, colX[5], startY + offsetY);

            offsetY += 25;
            g.DrawLine(Pens.Black, startX, startY + offsetY, startX + 700, startY + offsetY);
            offsetY += 10;

            // ======================
            // CHI TIẾT HÓA ĐƠN
            // ======================
            foreach (DataGridViewRow row in dgvChitiet.Rows)
            {
                if (row.IsNewRow) continue;

                g.DrawString(row.Cells["STT"].Value.ToString(), normalFont, Brushes.Black, colX[0], startY + offsetY);
                g.DrawString(row.Cells["MaSP"].Value.ToString(), normalFont, Brushes.Black, colX[1], startY + offsetY);
                g.DrawString(row.Cells["TenSP"].Value.ToString(), normalFont, Brushes.Black, colX[2], startY + offsetY);
                g.DrawString(row.Cells["SoLuong"].Value.ToString(), normalFont, Brushes.Black, colX[3], startY + offsetY);
                g.DrawString(
                    Convert.ToDecimal(row.Cells["DonGia"].Value).ToString("#,##0"),
                    normalFont, Brushes.Black, colX[4], startY + offsetY);
                g.DrawString(
                    Convert.ToDecimal(row.Cells["ThanhTien"].Value).ToString("#,##0"),
                    normalFont, Brushes.Black, colX[5], startY + offsetY);

                offsetY += 22;
            }

            offsetY += 10;
            g.DrawLine(Pens.Black, startX, startY + offsetY, startX + 700, startY + offsetY);
            offsetY += 25;

            // ======================
            // TỔNG
            // ======================
            g.DrawString($"Tổng số lượng: {lblTongSL.Text}", boldFont, Brushes.Black, startX + 420, startY + offsetY);
            offsetY += 25;

            g.DrawString($"Tổng tiền: {lblTongTien.Text}", boldFont, Brushes.Black, startX + 420, startY + offsetY);
        }

        private void btnLammoi_Click_1(object sender, EventArgs e)
        {
            txtTimKiem.Text = "";
            LoadDanhSachHoaDon();
            ClearPreview();
        }

        private void btnTimKiem_Click_1(object sender, EventArgs e)
        {
            string kw = txtTimKiem.Text.Trim();
            LoadDanhSachHoaDon(kw);
            ClearPreview();
        }
    }
}
