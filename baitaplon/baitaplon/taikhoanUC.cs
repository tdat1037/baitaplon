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
    public partial class taikhoanUC : UserControl
    {
        private readonly string connectionString =
            @"Data Source=LAPTOP-31QPVUR4;Initial Catalog=QL_BanHang;Integrated Security=True;TrustServerCertificate=True";
        public int CurrentUserId { get; set; } = 1;
        public taikhoanUC()
        {
            InitializeComponent();
            txtcu.UseSystemPasswordChar = true;
            txtmoi.UseSystemPasswordChar = true;
            txtxacnhan.UseSystemPasswordChar = true;
        }

        private void btnxacnhan_Click(object sender, EventArgs e)
        {
            string oldPass = txtcu.Text.Trim();
            string newPass = txtmoi.Text.Trim();
            string confirm = txtxacnhan.Text.Trim();

            if (string.IsNullOrEmpty(oldPass) || string.IsNullOrEmpty(newPass) || string.IsNullOrEmpty(confirm))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }
            if (newPass.Length < 1)
            {
                MessageBox.Show("Mật khẩu mới tối thiểu 4 ký tự!");
                return;
            }
            if (newPass != confirm)
            {
                MessageBox.Show("Nhập lại mật khẩu mới không khớp!");
                return;
            }

            try
            {
                if (!CheckOldPassword(CurrentUserId, oldPass))
                {
                    MessageBox.Show("Mật khẩu cũ không đúng!");
                    return;
                }

                UpdatePassword(CurrentUserId, newPass);
                MessageBox.Show("Đổi mật khẩu thành công!");

                txtcu.Clear();
                txtmoi.Clear();
                txtxacnhan.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đổi mật khẩu:\n" + ex.Message);
            }
        }
        private bool CheckOldPassword(int userId, string oldPass)
        {
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                string sql = "SELECT COUNT(*) FROM Users WHERE Id = @id AND Password = @pass";
                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@id", userId);
                    cmd.Parameters.AddWithValue("@pass", oldPass);
                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
        }
        private void UpdatePassword(int userId, string newPass)
        {
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                string sql = "UPDATE Users SET Password = @pass WHERE Id = @id";
                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@pass", newPass);
                    cmd.Parameters.AddWithValue("@id", userId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
