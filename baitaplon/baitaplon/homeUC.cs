using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace baitaplon
{
    public partial class homeUC : UserControl
    {
        public homeUC()
        {
            InitializeComponent();
            this.Load += HomeUC_Load;
        }
        private void HomeUC_Load(object sender, EventArgs e)
        {
            SetupLayout();

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
        }

        /// <summary>
        /// Set thống kê lên dashboard
        /// </summary>
        public void SetStats(int productCount, int invoiceToday, decimal revenueToday, int lowStock)
        {
            if (lblProductCount != null) lblProductCount.Text = productCount.ToString();
            if (lblInvoiceToday != null) lblInvoiceToday.Text = invoiceToday.ToString();
            if (lblRevenueToday != null) lblRevenueToday.Text = FormatMoney(revenueToday); // "1.250.000 đ"
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
                // bạn có thể chỉnh % theo ý, nhưng nên để doanh thu lớn hơn
                tlpthongke.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f)); // Sản phẩm
                tlpthongke.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20f)); // Hóa đơn
                tlpthongke.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35f)); // Doanh thu (rộng)
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

            // Nếu muốn doanh thu nhỏ hơn 1 chút cho chắc chắn vừa khung:
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
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void lblProductTitle_Click(object sender, EventArgs e)
        {

        }
    }
}
