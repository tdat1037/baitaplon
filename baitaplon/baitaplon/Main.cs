using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace baitaplon
{
    public partial class Main : Form
    {
        private bool isLoggingOut = false;
        public Main()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadUC(new productUC());
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pnMenu_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void LoadUC(UserControl uc)
        {
            pnMain.Controls.Clear();   // xóa màn hình cũ
            uc.Dock = DockStyle.Fill;     // full panel
            pnMain.Controls.Add(uc);   // add màn mới
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            LoadUC(new homeUC());
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void btnNhaphang_Click(object sender, EventArgs e)
        {
            LoadUC(new nhaphangUC());
        }

        private void btnTaikhoan_Click(object sender, EventArgs e)
        {
           LoadUC(new taikhoanUC());
        }

        private void btnDangxuat_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
        "Bạn có chắc chắn muốn đăng xuất không?",
        "Đăng xuất",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Question
    );

            if (result != DialogResult.Yes) return;

            // 1) Ẩn Main thay vì Close (để tránh FormClosing / Application.Exit)
            this.Hide();

            // 2) Mở lại đăng nhập
            var login = new dangnhap();

            // 3) Khi đăng nhập đóng (user thoát hoặc login thành công mở main khác)
            //    thì đóng Main hiện tại luôn
            login.FormClosed += (s, args) =>
            {
                // Nếu bạn muốn khi tắt login thì thoát hẳn:
                this.Close();
            };

            login.Show();
        }

        private void btnHoadon_Click(object sender, EventArgs e)
        {
            LoadUC(new banhangUC());
        }

        private void btnNCC_Click(object sender, EventArgs e)
        {
            LoadUC(new nhacungcapUC());
        }

        private void btnKhachhang_Click(object sender, EventArgs e)
        {
            LoadUC(new khachhangUC());
        }
    }
}
