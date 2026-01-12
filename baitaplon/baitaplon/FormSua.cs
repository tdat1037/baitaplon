using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace baitaplon
{
    public partial class FormSua : Form
    {
        private readonly string connectionString;
        private int editId = -1;

        // THÊM MỚI
        public FormSua(string cs)
        {
            InitializeComponent();
            connectionString = cs;

            this.StartPosition = FormStartPosition.CenterParent;
            this.AcceptButton = btnLuu;

            // Nếu bạn đã gắn Click trong Designer rồi thì XÓA 2 dòng dưới để tránh chạy 2 lần
            btnThoat.Click += (s, e) => this.Close();
        }

        // SỬA
        public FormSua(string cs, int id) : this(cs)
        {
            editId = id;
            LoadProduct();
        }

        private void LoadProduct()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string sql = @"SELECT Ma, Ten, Donvi, Giaban, Tonkho
                                   FROM Products
                                   WHERE Id=@id";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@id", editId);

                        using (var r = cmd.ExecuteReader())
                        {
                            if (!r.Read()) return;

                            txtMa.Text = r["Ma"].ToString();
                            txtTen.Text = r["Ten"].ToString();
                            txtDonvi.Text = r["Donvi"].ToString();
                            txtGiaban.Text = r["Giaban"].ToString();
                            nudTonkho.Value = Convert.ToDecimal(r["Tonkho"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load sản phẩm: " + ex.Message);
            }
        }

        private bool ValidateForm(out decimal giaBan)
        {
            giaBan = 0;

            string ma = txtMa.Text.Trim();
            string ten = txtTen.Text.Trim();
            string donVi = txtDonvi.Text.Trim();
            string giaText = txtGiaban.Text.Trim();

            if (ma == "" || ten == "" || donVi == "" || giaText == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ: Mã, Tên, Đơn vị, Giá bán.");
                return false;
            }

            if (!decimal.TryParse(giaText, out giaBan) || giaBan < 0)
            {
                MessageBox.Show("Giá bán không hợp lệ.");
                return false;
            }

            return true;
        }

        // ✅ Check trùng mã: Thêm thì check Ma, Sửa thì check Ma nhưng loại trừ chính nó
        private bool IsMaDuplicate(string ma)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string sql = (editId < 0)
                    ? "SELECT COUNT(*) FROM Products WHERE Ma=@ma"
                    : "SELECT COUNT(*) FROM Products WHERE Ma=@ma AND Id<>@id";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@ma", ma);
                    if (editId >= 0) cmd.Parameters.AddWithValue("@id", editId);

                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!ValidateForm(out decimal giaBan)) return;

            string ma = txtMa.Text.Trim();
            string ten = txtTen.Text.Trim();
            string donVi = txtDonvi.Text.Trim();
            int tonKho = (int)nudTonkho.Value;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // ✅ Check trùng Mã (đúng logic)
                    if (IsMaDuplicate(ma))
                    {
                        MessageBox.Show("Mã sản phẩm bị trùng!");
                        txtMa.Focus();
                        txtMa.SelectAll();
                        return;
                    }

                    string sql = (editId < 0)
                        ? @"INSERT INTO Products(Ma, Ten, Donvi, Giaban, Tonkho)
                    VALUES(@ma, @ten, @dv, @gia, @ton)"
                        : @"UPDATE Products
                    SET Ma=@ma, Ten=@ten, Donvi=@dv, Giaban=@gia, Tonkho=@ton
                    WHERE Id=@id";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        if (editId >= 0) cmd.Parameters.AddWithValue("@id", editId);

                        cmd.Parameters.AddWithValue("@ma", ma);
                        cmd.Parameters.AddWithValue("@ten", ten);
                        cmd.Parameters.AddWithValue("@dv", donVi);
                        cmd.Parameters.AddWithValue("@gia", giaBan);
                        cmd.Parameters.AddWithValue("@ton", tonKho);

                        int rows = cmd.ExecuteNonQuery();

                        // ✅ THÊM THÀNH CÔNG sẽ báo rõ
                        if (rows > 0)
                        {
                            MessageBox.Show(editId < 0 ? "Thêm sản phẩm thành công!" : "Cập nhật sản phẩm thành công!");
                        }
                    }
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (SqlException ex)
            {
                // ✅ Hiện đúng lỗi SQL để biết chính xác đang lỗi gì
                MessageBox.Show($"Lỗi SQL ({ex.Number}): {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

    }
}
