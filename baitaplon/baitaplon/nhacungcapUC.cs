using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace baitaplon
{
    public partial class nhacungcapUC : UserControl
    {
        private readonly string connectionString =
    @"Data Source=LAPTOP-31QPVUR4;Initial Catalog=QL_BanHang;Integrated Security=True;TrustServerCertificate=True";

        public nhacungcapUC()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        private void LoadNhaCungCap()
        {
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                string sql = "SELECT MaNCC, TenNCC, SDT, DiaChi, GhiChu FROM NhaCungCap ORDER BY MaNCC DESC";
                var da = new SqlDataAdapter(sql, con);
                var dt = new DataTable();
                da.Fill(dt);

                dgvNCC.DataSource = dt;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (var f = new themncc())
            {
                f.ShowDialog();

                if (f.Saved)
                {
                    LoadNhaCungCap();   // reload danh sách NCC
                }
            }
        }
    }
}

