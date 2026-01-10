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
        public Main()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {

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
    }
}
