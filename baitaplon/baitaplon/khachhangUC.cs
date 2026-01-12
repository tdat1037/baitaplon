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
    public partial class khachhangUC : UserControl
    {
        private readonly string connectionString =
    @"Data Source=LAPTOP-31QPVUR4;Initial Catalog=QL_BanHang;Integrated Security=True;TrustServerCertificate=True";

        public khachhangUC()
        {
            InitializeComponent();

            btnLammoi.Click += button2_Click;
        }
        private void LoadKhachHang()
        {
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();

                string sql = @"
SELECT 
    MaKH,
    TenKH,
    SDT,
    DiaChi,
    GhiChu
FROM KhachHang
ORDER BY MaKH DESC;";

                var da = new SqlDataAdapter(sql, con);
                var dt = new DataTable();
                da.Fill(dt);

                dgvKhachHang.DataSource = dt;

                // đặt tiêu đề cột cho đẹp
                if (dgvKhachHang.Columns.Contains("MaKH"))
                    dgvKhachHang.Columns["MaKH"].HeaderText = "Mã KH";

                if (dgvKhachHang.Columns.Contains("TenKH"))
                    dgvKhachHang.Columns["TenKH"].HeaderText = "Tên khách hàng";

                if (dgvKhachHang.Columns.Contains("SDT"))
                    dgvKhachHang.Columns["SDT"].HeaderText = "SĐT";

                if (dgvKhachHang.Columns.Contains("DiaChi"))
                    dgvKhachHang.Columns["DiaChi"].HeaderText = "Địa chỉ";

                if (dgvKhachHang.Columns.Contains("GhiChu"))
                    dgvKhachHang.Columns["GhiChu"].HeaderText = "Ghi chú";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (var f = new themkhachhang())
            {
                f.ShowDialog();
                if (f.Saved)
                {
                    LoadKhachHang(); // reload dgv khách hàng
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // xóa ô tìm kiếm nếu có
            if (txtTim != null)
                txtTim.Clear();

            // load lại danh sách
            LoadKhachHang();

            // bỏ chọn dòng đang chọn
            dgvKhachHang.ClearSelection();
        }
    }
}
