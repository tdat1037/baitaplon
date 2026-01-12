using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace baitaplon
{
    public partial class themncc : Form
    {
        private readonly string connectionString =
            @"Data Source=LAPTOP-31QPVUR4;Initial Catalog=QL_BanHang;Integrated Security=True;TrustServerCertificate=True";

        // để form gọi biết là đã lưu hay chưa
        public bool Saved { get; private set; } = false;

        public themncc()
        {
            InitializeComponent();

            // chỉ cho nhập số ở SĐT
            txtSdt.KeyPress += OnlyNumber_KeyPress;
        }

        // =========================
        // LƯU NHÀ CUNG CẤP
        // =========================
        private void btnLuu_Click(object sender, EventArgs e)
        {
            string ten = txtTenNCC.Text.Trim();
            string sdt = txtSdt.Text.Trim();
            string diachi = txtDiachi.Text.Trim();
            string ghichu = txtGhichu.Text.Trim();

            // validate
            if (string.IsNullOrEmpty(ten))
            {
                MessageBox.Show("Vui lòng nhập tên nhà cung cấp!");
                txtTenNCC.Focus();
                return;
            }

            try
            {
                using (var con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string sql = @"
INSERT INTO NhaCungCap(TenNCC, SDT, DiaChi, GhiChu)
VALUES (@TenNCC, @SDT, @DiaChi, @GhiChu);";

                    using (var cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@TenNCC", ten);
                        cmd.Parameters.AddWithValue("@SDT", string.IsNullOrEmpty(sdt) ? (object)DBNull.Value : sdt);
                        cmd.Parameters.AddWithValue("@DiaChi", string.IsNullOrEmpty(diachi) ? (object)DBNull.Value : diachi);
                        cmd.Parameters.AddWithValue("@GhiChu", string.IsNullOrEmpty(ghichu) ? (object)DBNull.Value : ghichu);

                        cmd.ExecuteNonQuery();
                    }
                }

                Saved = true;
                MessageBox.Show("Thêm nhà cung cấp thành công!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Thêm nhà cung cấp thất bại!\n\n" + ex.Message);
            }
        }

        // =========================
        // CHỈ CHO NHẬP SỐ (SĐT)
        // =========================
        private void OnlyNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;
            if (!char.IsDigit(e.KeyChar)) e.Handled = true;
        }
    }
}
