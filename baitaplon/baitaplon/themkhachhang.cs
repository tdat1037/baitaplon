using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace baitaplon
{
    public partial class themkhachhang : Form
    {
        private readonly string connectionString =
            @"Data Source=LAPTOP-31QPVUR4;Initial Catalog=QL_BanHang;Integrated Security=True;TrustServerCertificate=True";

        // để form cha biết có lưu thành công không
        public bool Saved { get; private set; } = false;

        public themkhachhang()
        {
            InitializeComponent();

            btnLuu.Click += btnLuu_Click;
            btnThoat.Click += (s, e) => this.Close();

            // chỉ cho nhập số ở SĐT
            txtSDT.KeyPress += OnlyNumber_KeyPress;

            // mã KH không cho nhập
            txtMaKH.ReadOnly = true;
        }

        // =========================
        // LƯU KHÁCH HÀNG
        // =========================
        private void btnLuu_Click(object sender, EventArgs e)
        {
            string ten = txtTenKH.Text.Trim();
            string sdt = txtSDT.Text.Trim();
            string diachi = txtDiaChi.Text.Trim();
            string ghichu = txtGhiChu.Text.Trim();

            // validate
            if (string.IsNullOrEmpty(ten))
            {
                MessageBox.Show("Vui lòng nhập tên khách hàng!");
                txtTenKH.Focus();
                return;
            }

            try
            {
                using (var con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string sql = @"
INSERT INTO KhachHang(TenKH, SDT, DiaChi, GhiChu)
VALUES (@TenKH, @SDT, @DiaChi, @GhiChu);";

                    using (var cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@TenKH", ten);
                        cmd.Parameters.AddWithValue("@SDT",
                            string.IsNullOrEmpty(sdt) ? (object)DBNull.Value : sdt);
                        cmd.Parameters.AddWithValue("@DiaChi",
                            string.IsNullOrEmpty(diachi) ? (object)DBNull.Value : diachi);
                        cmd.Parameters.AddWithValue("@GhiChu",
                            string.IsNullOrEmpty(ghichu) ? (object)DBNull.Value : ghichu);

                        cmd.ExecuteNonQuery();
                    }
                }

                Saved = true;
                MessageBox.Show("Thêm khách hàng thành công!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Thêm khách hàng thất bại!\n\n" + ex.Message);
            }
        }

        // =========================
        // CHỈ NHẬP SỐ CHO SĐT
        // =========================
        private void OnlyNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;
            if (!char.IsDigit(e.KeyChar)) e.Handled = true;
        }
    }
}
