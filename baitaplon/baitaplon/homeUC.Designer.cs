namespace baitaplon
{
    partial class homeUC
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnheader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnbody = new System.Windows.Forms.Panel();
            this.gbLowStock = new System.Windows.Forms.GroupBox();
            this.lvLowStock = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pninfo = new System.Windows.Forms.Panel();
            this.lbldate = new System.Windows.Forms.Label();
            this.lblhello = new System.Windows.Forms.Label();
            this.tlpthongke = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblProductCount = new System.Windows.Forms.Label();
            this.lblProductTitle = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lblInvoiceToday = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.lblRevenueToday = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.lblLowStock = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pnheader.SuspendLayout();
            this.pnbody.SuspendLayout();
            this.gbLowStock.SuspendLayout();
            this.pninfo.SuspendLayout();
            this.tlpthongke.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnheader
            // 
            this.pnheader.BackColor = System.Drawing.SystemColors.Info;
            this.pnheader.Controls.Add(this.lblTitle);
            this.pnheader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnheader.Location = new System.Drawing.Point(0, 0);
            this.pnheader.Name = "pnheader";
            this.pnheader.Size = new System.Drawing.Size(710, 60);
            this.pnheader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblTitle.Size = new System.Drawing.Size(259, 42);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "TRANG CHỦ";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnbody
            // 
            this.pnbody.Controls.Add(this.gbLowStock);
            this.pnbody.Controls.Add(this.pninfo);
            this.pnbody.Controls.Add(this.tlpthongke);
            this.pnbody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnbody.Location = new System.Drawing.Point(0, 60);
            this.pnbody.Name = "pnbody";
            this.pnbody.Size = new System.Drawing.Size(710, 486);
            this.pnbody.TabIndex = 1;
            // 
            // gbLowStock
            // 
            this.gbLowStock.Controls.Add(this.lvLowStock);
            this.gbLowStock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbLowStock.Location = new System.Drawing.Point(0, 250);
            this.gbLowStock.Name = "gbLowStock";
            this.gbLowStock.Size = new System.Drawing.Size(710, 236);
            this.gbLowStock.TabIndex = 2;
            this.gbLowStock.TabStop = false;
            this.gbLowStock.Text = "SẢN PHẨM SẮP HẾT HÀNG";
            // 
            // lvLowStock
            // 
            this.lvLowStock.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.lvLowStock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvLowStock.FullRowSelect = true;
            this.lvLowStock.GridLines = true;
            this.lvLowStock.HideSelection = false;
            this.lvLowStock.Location = new System.Drawing.Point(3, 18);
            this.lvLowStock.Name = "lvLowStock";
            this.lvLowStock.Size = new System.Drawing.Size(704, 215);
            this.lvLowStock.TabIndex = 0;
            this.lvLowStock.UseCompatibleStateImageBehavior = false;
            this.lvLowStock.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Tên sản phẩm";
            this.columnHeader1.Width = 200;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Tồn kho";
            this.columnHeader2.Width = 100;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Đơn vị";
            this.columnHeader3.Width = 100;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Trạng thái";
            this.columnHeader4.Width = 400;
            // 
            // pninfo
            // 
            this.pninfo.Controls.Add(this.lbldate);
            this.pninfo.Controls.Add(this.lblhello);
            this.pninfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pninfo.Location = new System.Drawing.Point(0, 150);
            this.pninfo.Name = "pninfo";
            this.pninfo.Size = new System.Drawing.Size(710, 100);
            this.pninfo.TabIndex = 1;
            // 
            // lbldate
            // 
            this.lbldate.AutoSize = true;
            this.lbldate.Location = new System.Drawing.Point(4, 38);
            this.lbldate.Name = "lbldate";
            this.lbldate.Size = new System.Drawing.Size(110, 16);
            this.lbldate.TabIndex = 1;
            this.lbldate.Text = "Ngày: 09/01/2026";
            // 
            // lblhello
            // 
            this.lblhello.AutoSize = true;
            this.lblhello.Location = new System.Drawing.Point(4, 12);
            this.lblhello.Name = "lblhello";
            this.lblhello.Size = new System.Drawing.Size(109, 16);
            this.lblhello.TabIndex = 0;
            this.lblhello.Text = "Xin chào: Quản lý";
            // 
            // tlpthongke
            // 
            this.tlpthongke.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.tlpthongke.ColumnCount = 4;
            this.tlpthongke.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpthongke.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpthongke.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tlpthongke.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpthongke.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tlpthongke.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tlpthongke.Controls.Add(this.tableLayoutPanel3, 2, 0);
            this.tlpthongke.Controls.Add(this.tableLayoutPanel4, 3, 0);
            this.tlpthongke.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpthongke.Location = new System.Drawing.Point(0, 0);
            this.tlpthongke.Name = "tlpthongke";
            this.tlpthongke.RowCount = 1;
            this.tlpthongke.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpthongke.Size = new System.Drawing.Size(710, 150);
            this.tlpthongke.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.lblProductCount, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblProductTitle, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(136, 144);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lblProductCount
            // 
            this.lblProductCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProductCount.Location = new System.Drawing.Point(3, 50);
            this.lblProductCount.Name = "lblProductCount";
            this.lblProductCount.Size = new System.Drawing.Size(130, 94);
            this.lblProductCount.TabIndex = 1;
            this.lblProductCount.Text = "label8";
            this.lblProductCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblProductTitle
            // 
            this.lblProductTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProductTitle.Location = new System.Drawing.Point(3, 0);
            this.lblProductTitle.Name = "lblProductTitle";
            this.lblProductTitle.Size = new System.Drawing.Size(130, 50);
            this.lblProductTitle.TabIndex = 0;
            this.lblProductTitle.Text = "🥬 Sản phẩm";
            this.lblProductTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblProductTitle.Click += new System.EventHandler(this.lblProductTitle_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.lblInvoiceToday, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(145, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(136, 144);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // lblInvoiceToday
            // 
            this.lblInvoiceToday.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblInvoiceToday.Location = new System.Drawing.Point(3, 50);
            this.lblInvoiceToday.Name = "lblInvoiceToday";
            this.lblInvoiceToday.Size = new System.Drawing.Size(130, 94);
            this.lblInvoiceToday.TabIndex = 1;
            this.lblInvoiceToday.Text = "label7";
            this.lblInvoiceToday.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 50);
            this.label2.TabIndex = 0;
            this.label2.Text = "🧾 Hóa đơn hôm nay";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.lblRevenueToday, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(287, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(278, 144);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // lblRevenueToday
            // 
            this.lblRevenueToday.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRevenueToday.Location = new System.Drawing.Point(3, 50);
            this.lblRevenueToday.Name = "lblRevenueToday";
            this.lblRevenueToday.Size = new System.Drawing.Size(272, 94);
            this.lblRevenueToday.TabIndex = 2;
            this.lblRevenueToday.Text = "label6";
            this.lblRevenueToday.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(272, 50);
            this.label3.TabIndex = 1;
            this.label3.Text = "💰 Doanh thu hôm nay";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.lblLowStock, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(571, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(136, 144);
            this.tableLayoutPanel4.TabIndex = 3;
            // 
            // lblLowStock
            // 
            this.lblLowStock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLowStock.Location = new System.Drawing.Point(3, 50);
            this.lblLowStock.Name = "lblLowStock";
            this.lblLowStock.Size = new System.Drawing.Size(130, 94);
            this.lblLowStock.TabIndex = 3;
            this.lblLowStock.Text = "label5";
            this.lblLowStock.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(130, 50);
            this.label4.TabIndex = 2;
            this.label4.Text = "⚠️ Sắp hết hàng";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // homeUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnbody);
            this.Controls.Add(this.pnheader);
            this.Name = "homeUC";
            this.Size = new System.Drawing.Size(710, 546);
            this.pnheader.ResumeLayout(false);
            this.pnheader.PerformLayout();
            this.pnbody.ResumeLayout(false);
            this.gbLowStock.ResumeLayout(false);
            this.pninfo.ResumeLayout(false);
            this.pninfo.PerformLayout();
            this.tlpthongke.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnheader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnbody;
        private System.Windows.Forms.TableLayoutPanel tlpthongke;
        private System.Windows.Forms.Panel pninfo;
        private System.Windows.Forms.Label lbldate;
        private System.Windows.Forms.Label lblhello;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label lblProductCount;
        private System.Windows.Forms.Label lblProductTitle;
        private System.Windows.Forms.Label lblInvoiceToday;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblRevenueToday;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblLowStock;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox gbLowStock;
        private System.Windows.Forms.ListView lvLowStock;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
    }
}
