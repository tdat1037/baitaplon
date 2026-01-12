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
    public partial class dangky : Form
    {
        private string connectionString =
            @"Data Source=LAPTOP-31QPVUR4;Initial Catalog=QL_BanHang;Integrated Security=True;TrustServerCertificate=True";
        public dangky()
        {
            InitializeComponent();
            txtMatkhau.UseSystemPasswordChar = true;
            txtxacnhan.UseSystemPasswordChar = true;
            rdnhanvien.Checked = true; // mặc định


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = txtTendangnhap.Text;
            string password = txtMatkhau.Text;
            string confirmPassword = txtxacnhan.Text;
            if (username == "" || password == "" || confirmPassword == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Mật khẩu nhập lại không khớp!");
                return;
            }

            string role = rdquanly.Checked ? "QuanLy" : "NhanVien";

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // 2️⃣ Check trùng username
                    string checkSql = "SELECT COUNT(*) FROM Users WHERE Username = @u";
                    SqlCommand checkCmd = new SqlCommand(checkSql, con);
                    checkCmd.Parameters.AddWithValue("@u", username);

                    int exists = (int)checkCmd.ExecuteScalar();
                    if (exists > 0)
                    {
                        MessageBox.Show("Tên đăng nhập đã tồn tại!");
                        return;
                    }

                    // 3️⃣ Insert user
                    string insertSql = @"
                    INSERT INTO Users (Username, Password, Role, IsActive)
                    VALUES (@u, @p, @r, 1)";

                    SqlCommand insertCmd = new SqlCommand(insertSql, con);
                    insertCmd.Parameters.AddWithValue("@u", username);
                    insertCmd.Parameters.AddWithValue("@p", password); // demo (chưa hash)
                    insertCmd.Parameters.AddWithValue("@r", role);

                    insertCmd.ExecuteNonQuery();

                    MessageBox.Show("Đăng ký thành công!");
                    this.Close(); // quay về form login
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi DB: " + ex.Message);
            }
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            this.Close(); // đóng form đăng ký, quay về login
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dangnhap f = new dangnhap();
            f.Show();
            this.Close();
        }
    }
}
    

