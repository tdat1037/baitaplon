using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace baitaplon
{
    public partial class dangnhap : Form
    {
        private string connectionString =
            @"Data Source=LAPTOP-31QPVUR4;Initial Catalog=QL_BanHang;Integrated Security=True;TrustServerCertificate=True";
        public dangnhap()
        {
            InitializeComponent();
            txtMatkhau.UseSystemPasswordChar = true;
        }
        

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            String username = txtTendangnhap.Text;
            String password = txtMatkhau.Text;
            if (username == "" || password == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return;
            }
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string sql = @"SELECT Role 
                                   FROM Users 
                                   WHERE Username = @u 
                                     AND Password = @p 
                                     AND IsActive = 1";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@u", username);
                    cmd.Parameters.AddWithValue("@p", password);

                    object result = cmd.ExecuteScalar();

                    if (result == null)
                    {
                        MessageBox.Show("Sai tài khoản hoặc mật khẩu!");
                        return;
                    }

                    string role = result.ToString();

                    MessageBox.Show("Đăng nhập thành công! Vai trò: " + role);

                    // Mở form chính
                    Main f = new Main(); // đổi đúng tên form của bạn
                    f.Show();
                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối DB: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dangky f = new dangky();
            f.Show();
            this.Hide();
        }

        private void dangnhap_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void dangnhap_Shown(object sender, EventArgs e)
        {
            txtTendangnhap.Focus();
        }
    }

}
