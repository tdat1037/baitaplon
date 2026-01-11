using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace baitaplon
{
    public partial class homeUC : UserControl
    {
        // Ngưỡng để xem là "sắp hết hàng"
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

            // Demo số liệu (sau này thay bằng DB)
            SetStats(
                productCount: 25,
                invoiceToday: 6,
                revenueToday: 1250000m,
                lowStock: 2
            );

            // Thông tin user + ngày
            if (lblhello != null) lblhello.Text = "Xin chào: Quản lý";
            if (lbldate != null) lbldate.Text = "Ngày: " + DateTime.Now.ToString("dd/MM/yyyy");

            // Demo danh sách sắp hết hàng
            var demo = new List<LowStockItem>
            {
                new LowStockItem("Rau muống", 3, "Bó"),
                new LowStockItem("Cải xanh", 2, "Kg"),
                new LowStockItem("Cà chua", 4, "Kg"),
                new LowStockItem("Hành lá", 1, "Bó"),
            };
            LoadLowStock(demo);
        }

        /// <summary>
        /// Set thống kê lên dashboard
        /// </summary>
        public void SetStats(int productCount, int invoiceToday, decimal revenueToday, int lowStock)
        {
            if (lblProductCount != null) lblProductCount.Text = productCount.ToString();
            if (lblInvoiceToday != null) lblInvoiceToday.Text = invoiceToday.ToString();
            if (lblRevenueToday != null) lblRevenueToday.Text = FormatMoney(revenueToday);
            if (lblLowStock != null) lblLowStock.Text = lowStock.ToString();
        }

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

            // TableLayoutPanel: 4 cột, doanh thu rộng hơn
            if (tlpthongke != null)
            {
                tlpthongke.ColumnCount = 4;
                tlpthongke.RowCount = 1;

                tlpthongke.ColumnStyles.Clear();
                tlpthongke.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f)); // Sản phẩm
                tlpthongke.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20f)); // Hóa đơn
                tlpthongke.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35f)); // Doanh thu
                tlpthongke.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20f)); // Sắp hết hàng

                tlpthongke.RowStyles.Clear();
                tlpthongke.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));

                tlpthongke.Padding = new Padding(8);
            }

            // Style số to để không bị cắt
            StyleBig(lblProductCount);
            StyleBig(lblInvoiceToday);
            StyleBig(lblRevenueToday);
            StyleBig(lblLowStock);

            // Doanh thu nhỏ hơn chút cho chắc vừa ô
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

        // ====== LOW STOCK LISTVIEW ======

        private void SetupLowStockListView()
        {
            if (lvLowStock == null) return;

            lvLowStock.BeginUpdate();
            lvLowStock.View = View.Details;
            lvLowStock.FullRowSelect = true;
            lvLowStock.GridLines = true;
            lvLowStock.HideSelection = false;

            // tránh add cột nhiều lần khi reload
            if (lvLowStock.Columns.Count == 0)
            {
                lvLowStock.Columns.Add("Tên sản phẩm", 220);
                lvLowStock.Columns.Add("Tồn", 70);
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

        // Model gọn để chứa dữ liệu sắp hết hàng
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
