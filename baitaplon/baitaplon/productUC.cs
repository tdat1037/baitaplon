using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace baitaplon
{
    public partial class productUC : UserControl
    {
        private readonly string connectionString =
            @"Data Source=LAPTOP-31QPVUR4;Initial Catalog=QL_BanHang;Integrated Security=True;TrustServerCertificate=True";

        private int currentID = -1;

        public productUC()
        {
            InitializeComponent();
            this.Load += productUC_Load;
        }

        private void productUC_Load(object sender, EventArgs e)
        {
            SetupGrid();
            LoadProducts();
        }

        private void SetupGrid()
        {
            dgvSanpham.ReadOnly = true;
            dgvSanpham.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSanpham.MultiSelect = false;
            dgvSanpham.AllowUserToAddRows = false;
            dgvSanpham.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // ✅ tránh gắn event nhiều lần (nếu UC load lại)
            dgvSanpham.CellClick -= dgvSanpham_CellClick;
            dgvSanpham.CellClick += dgvSanpham_CellClick;
        }

        private void LoadProducts(string keyword = "")
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string sql = @"
SELECT 
    Id,
    Ma     AS [Mã],
    Ten    AS [Tên],
    Donvi  AS [Đơn vị],
    Giaban AS [Giá bán],
    Tonkho AS [Tồn kho]
FROM Products
WHERE (@kw = '' OR Ma LIKE @like OR Ten LIKE @like)
ORDER BY Id DESC";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@kw", keyword ?? "");
                        cmd.Parameters.AddWithValue("@like", "%" + (keyword ?? "") + "%");

                        DataTable dt = new DataTable();
                        new SqlDataAdapter(cmd).Fill(dt);

                        dgvSanpham.DataSource = dt;

                        if (dgvSanpham.Columns["Id"] != null)
                            dgvSanpham.Columns["Id"].Visible = false;

                        // ✅ format cột giá bán (nếu muốn)
                        if (dgvSanpham.Columns["Giá bán"] != null)
                        {
                            dgvSanpham.Columns["Giá bán"].DefaultCellStyle.Format = "N0";
                            dgvSanpham.Columns["Giá bán"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        }

                        // ✅ reset lựa chọn để tránh dính currentID cũ
                        dgvSanpham.ClearSelection();
                        currentID = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải sản phẩm: " + ex.Message);
            }
        }

        private void dgvSanpham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var r = dgvSanpham.Rows[e.RowIndex];

            // ✅ đảm bảo có cột Id
            if (r.Cells["Id"].Value == null) { currentID = -1; return; }

            currentID = Convert.ToInt32(r.Cells["Id"].Value);
        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            LoadProducts(txtTimkiem.Text.Trim());
        }

        private void btnLammoi_Click(object sender, EventArgs e)
        {
            txtTimkiem.Clear();
            LoadProducts();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (currentID < 0)
            {
                MessageBox.Show("Hãy chọn 1 sản phẩm trong bảng để xóa.");
                return;
            }

            if (MessageBox.Show("Bạn có chắc muốn xóa sản phẩm này?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string sql = "DELETE FROM Products WHERE Id = @id";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@id", currentID);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Đã xóa!");

                // ✅ reload xong clear selection + reset id
                LoadProducts();
                dgvSanpham.ClearSelection();
                currentID = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xóa: " + ex.Message);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (currentID < 0)
            {
                MessageBox.Show("Hãy chọn 1 sản phẩm trong bảng để sửa.");
                return;
            }

            using (var f = new FormSua(connectionString, currentID))
            {
                if (f.ShowDialog() == DialogResult.OK)
                    LoadProducts();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            using (var f = new FormSua(connectionString))
            {
                if (f.ShowDialog() == DialogResult.OK)
                    LoadProducts();
            }
        }
    }
}
