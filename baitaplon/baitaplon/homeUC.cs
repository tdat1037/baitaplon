using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace baitaplon
{
    public partial class homeUC : UserControl
    {
        private readonly string connectionString =
            @"Data Source=LAPTOP-31QPVUR4;Initial Catalog=QL_BanHang;Integrated Security=True;TrustServerCertificate=True";

        // Ngưỡng để xem là "sắp hết hàng" (đúng như hình bạn muốn)
        private const int LOW_STOCK_THRESHOLD = 5;

        public homeUC()
        {
            InitializeComponent();
            this.Load += HomeUC_Load;
        }

        private void HomeUC_Load(object sender, EventArgs e)
        {
            SetupLayout();
            SetupLowStockListView();

            // Header: user + ngày
            if (lblhello != null) lblhello.Text = "Xin chào: Quản lý";
            if (lbldate != null) lbldate.Text = "Ngày: " + DateTime.Now.ToString("dd/MM/yyyy");

            // Load DB
            try
            {
                LoadDashboardFromDb();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không load được trang chủ.\n\n" + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                // fallback: set 0 để không trống UI
                SetStats(0, 0, 0m, 0);
                LoadLowStock(new List<LowStockItem>());
            }
        }

        // =========================
        // LOAD DASHBOARD FROM DB
        // =========================
        private void LoadDashboardFromDb()
        {
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();

                // 1) Tổng sản phẩm
                int productCount = ExecuteScalarInt(con, "SELECT COUNT(*) FROM Products;");

                // 2) Hóa đơn hôm nay
                int invoiceToday = ExecuteScalarInt(con, @"
SELECT COUNT(*) 
FROM HoaDon 
WHERE CONVERT(date, NgayBan) = CONVERT(date, GETDATE());");

                // 3) Doanh thu hôm nay
                // Nếu HoaDon.TongTien được cập nhật đúng thì dùng SUM(TongTien).
                // Nếu chưa chắc, bạn có thể tính từ ChiTietHoaDon.
                decimal revenueToday = ExecuteScalarDecimal(con, @"
SELECT ISNULL(SUM(TongTien), 0)
FROM HoaDon
WHERE CONVERT(date, NgayBan) = CONVERT(date, GETDATE());");

                // 4) Số SP sắp hết hàng (<= threshold)
                int lowStockCount = ExecuteScalarInt(con, @"
SELECT COUNT(*) 
FROM Products
WHERE Tonkho <= @th;", new SqlParameter("@th", LOW_STOCK_THRESHOLD));

                SetStats(productCount, invoiceToday, revenueToday, lowStockCount);

                // 5) List SP sắp hết hàng (hiện bảng dưới)
                var items = new List<LowStockItem>();

                using (var cmd = new SqlCommand(@"
SELECT TOP 50
    Ten,
    Tonkho,
    Donvi
FROM Products
WHERE Tonkho <= @th
ORDER BY Tonkho ASC, Ten ASC;", con))
                {
                    cmd.Parameters.AddWithValue("@th", LOW_STOCK_THRESHOLD);

                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            string ten = rd["Ten"]?.ToString() ?? "";
                            int ton = Convert.ToInt32(rd["Tonkho"]);
                            string donvi = rd["Donvi"]?.ToString() ?? "";

                            items.Add(new LowStockItem(ten, ton, donvi));
                        }
                    }
                }

                LoadLowStock(items);
            }
        }

        private int ExecuteScalarInt(SqlConnection con, string sql, params SqlParameter[] ps)
        {
            using (var cmd = new SqlCommand(sql, con))
            {
                if (ps != null && ps.Length > 0) cmd.Parameters.AddRange(ps);
                object val = cmd.ExecuteScalar();
                if (val == null || val == DBNull.Value) return 0;
                return Convert.ToInt32(val);
            }
        }

        private decimal ExecuteScalarDecimal(SqlConnection con, string sql, params SqlParameter[] ps)
        {
            using (var cmd = new SqlCommand(sql, con))
            {
                if (ps != null && ps.Length > 0) cmd.Parameters.AddRange(ps);
                object val = cmd.ExecuteScalar();
                if (val == null || val == DBNull.Value) return 0m;
                return Convert.ToDecimal(val);
            }
        }

        // =========================
        // SET STATS
        // =========================
        public void SetStats(int productCount, int invoiceToday, decimal revenueToday, int lowStock)
        {
            if (lblProductCount != null) lblProductCount.Text = productCount.ToString();
            if (lblInvoiceToday != null) lblInvoiceToday.Text = invoiceToday.ToString();
            if (lblRevenueToday != null) lblRevenueToday.Text = FormatMoney(revenueToday);
            if (lblLowStock != null) lblLowStock.Text = lowStock.ToString();
        }

        // =========================
        // UI STYLE (giống ảnh)
        // =========================
        private void SetupLayout()
        {
            // Title
            if (lblTitle != null)
            {
                lblTitle.Text = "TRANG CHỦ";
                lblTitle.Font = new Font("Segoe UI", 22, FontStyle.Bold);
                lblTitle.Padding = new Padding(10, 0, 0, 0);
                lblTitle.TextAlign = ContentAlignment.MiddleLeft;
            }

            // 4 ô thống kê: sản phẩm / hóa đơn / doanh thu / sắp hết
            if (tlpthongke != null)
            {
                tlpthongke.ColumnCount = 4;
                tlpthongke.RowCount = 1;

                tlpthongke.ColumnStyles.Clear();
                tlpthongke.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
                tlpthongke.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20f));
                tlpthongke.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35f));
                tlpthongke.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20f));

                tlpthongke.RowStyles.Clear();
                tlpthongke.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
                tlpthongke.Padding = new Padding(8);
            }

            StyleBig(lblProductCount);
            StyleBig(lblInvoiceToday);
            StyleBig(lblRevenueToday);
            StyleBig(lblLowStock);

            // Doanh thu chữ vừa đẹp
            if (lblRevenueToday != null)
                lblRevenueToday.Font = new Font("Segoe UI", 20, FontStyle.Bold);
        }

        private void StyleBig(Label lbl)
        {
            if (lbl == null) return;
            lbl.AutoSize = false;
            lbl.Dock = DockStyle.Fill;
            lbl.TextAlign = ContentAlignment.MiddleCenter;
            lbl.Font = new Font("Segoe UI", 22, FontStyle.Bold);
        }

        private string FormatMoney(decimal money)
        {
            return money.ToString("N0") + " đ";
        }

        // =========================
        // LOW STOCK LISTVIEW
        // =========================
        private void SetupLowStockListView()
        {
            if (lvLowStock == null) return;

            lvLowStock.BeginUpdate();
            lvLowStock.View = View.Details;
            lvLowStock.FullRowSelect = true;
            lvLowStock.GridLines = true;
            lvLowStock.HideSelection = false;

            if (lvLowStock.Columns.Count == 0)
            {
                lvLowStock.Columns.Add("Tên sản phẩm", 220);
                lvLowStock.Columns.Add("Tồn kho", 90);
                lvLowStock.Columns.Add("Đơn vị", 80);
                lvLowStock.Columns.Add("Trạng thái", 120);
            }

            lvLowStock.EndUpdate();
        }

        public void LoadLowStock(List<LowStockItem> items)
        {
            if (lvLowStock == null) return;

            lvLowStock.BeginUpdate();
            lvLowStock.Items.Clear();

            foreach (var x in items)
            {
                // theo ảnh bạn: tồn ít -> "SẮP HẾT"
                string status = x.Stock <= 0 ? "HẾT HÀNG"
                              : x.Stock <= LOW_STOCK_THRESHOLD ? "SẮP HẾT"
                              : "BÌNH THƯỜNG";

                var it = new ListViewItem(x.Name);
                it.SubItems.Add(x.Stock.ToString());
                it.SubItems.Add(x.Unit);
                it.SubItems.Add(status);

                lvLowStock.Items.Add(it);
            }

            lvLowStock.EndUpdate();
        }

        public class LowStockItem
        {
            public string Name { get; }
            public int Stock { get; }
            public string Unit { get; }

            public LowStockItem(string name, int stock, string unit)
            {
                Name = name;
                Stock = stock;
                Unit = unit;
            }
        }
        // Các event click label (nếu không dùng có thể xóa)
        private void label1_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void lblProductTitle_Click(object sender, EventArgs e) { }
    }
}
