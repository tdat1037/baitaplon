namespace baitaplon
{
    partial class Main
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnMenu = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pnTop = new System.Windows.Forms.Panel();
            this.lbltitle = new System.Windows.Forms.Label();
            this.pnMain = new System.Windows.Forms.Panel();
            this.btnKhachhang = new System.Windows.Forms.Button();
            this.btnDangxuat = new System.Windows.Forms.Button();
            this.btnTaikhoan = new System.Windows.Forms.Button();
            this.btnBaocao = new System.Windows.Forms.Button();
            this.btnNCC = new System.Windows.Forms.Button();
            this.btnHoadon = new System.Windows.Forms.Button();
            this.btnNhaphang = new System.Windows.Forms.Button();
            this.btnsanpham = new System.Windows.Forms.Button();
            this.btnHome = new System.Windows.Forms.Button();
            this.pnMenu.SuspendLayout();
            this.pnTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnMenu
            // 
            this.pnMenu.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.pnMenu.Controls.Add(this.label1);
            this.pnMenu.Controls.Add(this.btnKhachhang);
            this.pnMenu.Controls.Add(this.btnDangxuat);
            this.pnMenu.Controls.Add(this.btnTaikhoan);
            this.pnMenu.Controls.Add(this.btnBaocao);
            this.pnMenu.Controls.Add(this.btnNCC);
            this.pnMenu.Controls.Add(this.btnHoadon);
            this.pnMenu.Controls.Add(this.btnNhaphang);
            this.pnMenu.Controls.Add(this.btnsanpham);
            this.pnMenu.Controls.Add(this.btnHome);
            this.pnMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnMenu.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pnMenu.Location = new System.Drawing.Point(0, 0);
            this.pnMenu.Name = "pnMenu";
            this.pnMenu.Size = new System.Drawing.Size(223, 642);
            this.pnMenu.TabIndex = 0;
            this.pnMenu.Paint += new System.Windows.Forms.PaintEventHandler(this.pnMenu_Paint);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Location = new System.Drawing.Point(71, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "MENU";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // pnTop
            // 
            this.pnTop.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.pnTop.Controls.Add(this.lbltitle);
            this.pnTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnTop.Location = new System.Drawing.Point(223, 0);
            this.pnTop.Name = "pnTop";
            this.pnTop.Size = new System.Drawing.Size(912, 50);
            this.pnTop.TabIndex = 0;
            // 
            // lbltitle
            // 
            this.lbltitle.AutoSize = true;
            this.lbltitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltitle.Location = new System.Drawing.Point(297, 9);
            this.lbltitle.Name = "lbltitle";
            this.lbltitle.Size = new System.Drawing.Size(329, 36);
            this.lbltitle.TabIndex = 0;
            this.lbltitle.Text = "QUẢN LÝ SẢN PHẨM";
            this.lbltitle.Click += new System.EventHandler(this.label2_Click);
            // 
            // pnMain
            // 
            this.pnMain.BackColor = System.Drawing.SystemColors.Control;
            this.pnMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnMain.Location = new System.Drawing.Point(223, 50);
            this.pnMain.Name = "pnMain";
            this.pnMain.Size = new System.Drawing.Size(912, 592);
            this.pnMain.TabIndex = 0;
            // 
            // btnKhachhang
            // 
            this.btnKhachhang.BackColor = System.Drawing.Color.White;
            this.btnKhachhang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKhachhang.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnKhachhang.Image = global::baitaplon.Properties.Resources.khachhang;
            this.btnKhachhang.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnKhachhang.Location = new System.Drawing.Point(12, 312);
            this.btnKhachhang.Name = "btnKhachhang";
            this.btnKhachhang.Size = new System.Drawing.Size(200, 45);
            this.btnKhachhang.TabIndex = 8;
            this.btnKhachhang.Text = "Khách hàng";
            this.btnKhachhang.UseVisualStyleBackColor = false;
            // 
            // btnDangxuat
            // 
            this.btnDangxuat.BackColor = System.Drawing.Color.White;
            this.btnDangxuat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDangxuat.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnDangxuat.Image = global::baitaplon.Properties.Resources.dangxuat;
            this.btnDangxuat.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDangxuat.Location = new System.Drawing.Point(12, 585);
            this.btnDangxuat.Name = "btnDangxuat";
            this.btnDangxuat.Size = new System.Drawing.Size(200, 45);
            this.btnDangxuat.TabIndex = 7;
            this.btnDangxuat.Text = "Đăng xuất";
            this.btnDangxuat.UseVisualStyleBackColor = false;
            this.btnDangxuat.Click += new System.EventHandler(this.btnDangxuat_Click);
            // 
            // btnTaikhoan
            // 
            this.btnTaikhoan.BackColor = System.Drawing.Color.White;
            this.btnTaikhoan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTaikhoan.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnTaikhoan.Image = global::baitaplon.Properties.Resources.taikhoan;
            this.btnTaikhoan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTaikhoan.Location = new System.Drawing.Point(12, 414);
            this.btnTaikhoan.Name = "btnTaikhoan";
            this.btnTaikhoan.Size = new System.Drawing.Size(200, 45);
            this.btnTaikhoan.TabIndex = 6;
            this.btnTaikhoan.Text = "Tài khoản";
            this.btnTaikhoan.UseVisualStyleBackColor = false;
            this.btnTaikhoan.Click += new System.EventHandler(this.btnTaikhoan_Click);
            // 
            // btnBaocao
            // 
            this.btnBaocao.BackColor = System.Drawing.Color.White;
            this.btnBaocao.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBaocao.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnBaocao.Image = global::baitaplon.Properties.Resources.baocao;
            this.btnBaocao.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBaocao.Location = new System.Drawing.Point(12, 363);
            this.btnBaocao.Name = "btnBaocao";
            this.btnBaocao.Size = new System.Drawing.Size(200, 45);
            this.btnBaocao.TabIndex = 5;
            this.btnBaocao.Text = "Báo cáo";
            this.btnBaocao.UseVisualStyleBackColor = false;
            // 
            // btnNCC
            // 
            this.btnNCC.BackColor = System.Drawing.Color.White;
            this.btnNCC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNCC.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnNCC.Image = global::baitaplon.Properties.Resources.ncc;
            this.btnNCC.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNCC.Location = new System.Drawing.Point(12, 261);
            this.btnNCC.Name = "btnNCC";
            this.btnNCC.Size = new System.Drawing.Size(200, 45);
            this.btnNCC.TabIndex = 4;
            this.btnNCC.Text = "Nhà cung cấp";
            this.btnNCC.UseVisualStyleBackColor = false;
            // 
            // btnHoadon
            // 
            this.btnHoadon.BackColor = System.Drawing.Color.White;
            this.btnHoadon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHoadon.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnHoadon.Image = global::baitaplon.Properties.Resources.banhang;
            this.btnHoadon.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHoadon.Location = new System.Drawing.Point(12, 210);
            this.btnHoadon.Name = "btnHoadon";
            this.btnHoadon.Size = new System.Drawing.Size(200, 45);
            this.btnHoadon.TabIndex = 3;
            this.btnHoadon.Text = "Bán hàng";
            this.btnHoadon.UseVisualStyleBackColor = false;
            // 
            // btnNhaphang
            // 
            this.btnNhaphang.BackColor = System.Drawing.Color.White;
            this.btnNhaphang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNhaphang.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnNhaphang.Image = global::baitaplon.Properties.Resources.nhaphang;
            this.btnNhaphang.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNhaphang.Location = new System.Drawing.Point(12, 159);
            this.btnNhaphang.Name = "btnNhaphang";
            this.btnNhaphang.Size = new System.Drawing.Size(200, 45);
            this.btnNhaphang.TabIndex = 2;
            this.btnNhaphang.Text = "Nhập hàng";
            this.btnNhaphang.UseVisualStyleBackColor = false;
            this.btnNhaphang.Click += new System.EventHandler(this.btnNhaphang_Click);
            // 
            // btnsanpham
            // 
            this.btnsanpham.BackColor = System.Drawing.Color.White;
            this.btnsanpham.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnsanpham.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnsanpham.Image = global::baitaplon.Properties.Resources.sanpham;
            this.btnsanpham.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnsanpham.Location = new System.Drawing.Point(12, 108);
            this.btnsanpham.Name = "btnsanpham";
            this.btnsanpham.Size = new System.Drawing.Size(200, 45);
            this.btnsanpham.TabIndex = 1;
            this.btnsanpham.Text = "Sản phẩm";
            this.btnsanpham.UseVisualStyleBackColor = false;
            this.btnsanpham.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnHome
            // 
            this.btnHome.BackColor = System.Drawing.Color.White;
            this.btnHome.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHome.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnHome.Image = global::baitaplon.Properties.Resources.home;
            this.btnHome.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHome.Location = new System.Drawing.Point(12, 57);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(200, 45);
            this.btnHome.TabIndex = 0;
            this.btnHome.Text = "Home";
            this.btnHome.UseVisualStyleBackColor = false;
            this.btnHome.Click += new System.EventHandler(this.btnHome_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1135, 642);
            this.Controls.Add(this.pnMain);
            this.Controls.Add(this.pnTop);
            this.Controls.Add(this.pnMenu);
            this.Name = "Main";
            this.Text = "Form3";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.pnMenu.ResumeLayout(false);
            this.pnMenu.PerformLayout();
            this.pnTop.ResumeLayout(false);
            this.pnTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnMenu;
        private System.Windows.Forms.Panel pnTop;
        private System.Windows.Forms.Panel pnMain;
        private System.Windows.Forms.Button btnHome;
        private System.Windows.Forms.Button btnNCC;
        private System.Windows.Forms.Button btnHoadon;
        private System.Windows.Forms.Button btnNhaphang;
        private System.Windows.Forms.Button btnsanpham;
        private System.Windows.Forms.Button btnDangxuat;
        private System.Windows.Forms.Button btnTaikhoan;
        private System.Windows.Forms.Button btnBaocao;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnKhachhang;
        private System.Windows.Forms.Label lbltitle;
    }
}