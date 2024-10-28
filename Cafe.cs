using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TienSuToCoffee
{
    public partial class Cafe : UserControl
    {
        MYCOFFEEEntitiesS me = DataContextSingleton.Instance;
        public Cafe()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Cafe_Load(object sender, EventArgs e)
        {
            LoadData1();
            LoadCategories();
            
        }
        void LoadData1()
        {
            var result = me.Foods.Select(c => new { c.id, c.name, c.idCategory, c.price });
            dgvFood.DataSource = result.ToList();

        }

        void LoadCategories()
        {
            // Truy vấn để lấy danh sách danh mục thực phẩm
            var categories = me.FoodCategories.Select(c => new { c.id, c.name }).ToList();

            // Gán dữ liệu vào ComboBox
            cmbCategory.DataSource = categories;
            cmbCategory.DisplayMember = "name"; // Hiển thị tên trong ComboBox
            cmbCategory.ValueMember = "id";      // Lưu id khi chọn

        }

        private void btnViewFood_Click(object sender, EventArgs e)
        {
            LoadData1();
        }

       

        private void btnAddFood_Click(object sender, EventArgs e)
        {

             // Kiểm tra các ô nhập liệu có đầy đủ không
    if (!string.IsNullOrEmpty(txtDish.Text) && cmbCategory.SelectedValue != null && !string.IsNullOrEmpty(txtPriceFood.Text))
    {
        try
        {
            // Tạo đối tượng món ăn mới
            Food newFood = new Food
            {
                id = int.Parse(txtIDFood.Text),
                name = txtDish.Text,
                idCategory = (int)cmbCategory.SelectedValue,
                price = float.Parse(txtPriceFood.Text)
            };

            // Thêm vào cơ sở dữ liệu
            me.Foods.Add(newFood);
            me.SaveChanges();

            // Tải lại dữ liệu và hiển thị thông báo thành công
            LoadData1();
            MessageBox.Show("Món ăn đã được thêm thành công!");
        }
        catch (Exception ex)
        {
            MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
        }
    }
    else
    {
        MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
    }
}



      

        private void btnEditFood_Click(object sender, EventArgs e)
        {
            if (dgvFood.CurrentRow != null)
            {
                int foodID = (int)dgvFood.CurrentRow.Cells["id"].Value;

                var foodToEdit = me.Foods.SingleOrDefault(f => f.id == foodID);
                if (foodToEdit != null)
                {
                    // Cập nhật thông tin món ăn
                    foodToEdit.id = int.Parse(txtIDFood.Text);
                    foodToEdit.name = txtDish.Text;
                    foodToEdit.idCategory = (int)cmbCategory.SelectedValue;
                    foodToEdit.price = float.Parse(txtPriceFood.Text);

                    // Lưu thay đổi
                    me.SaveChanges();
                    LoadData1();
                    MessageBox.Show("Món ăn đã được sửa thành công.");
                }
                else
                {
                    MessageBox.Show("Không tìm thấy món ăn để sửa.");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn món ăn để sửa.");
            }
        }

        private void btnSearchFood_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearchFood.Text.Trim(); // Lấy từ khóa tìm kiếm và loại bỏ khoảng trắng

            if (!string.IsNullOrEmpty(searchTerm))
            {
                // Tìm các món ăn có tên bắt đầu bằng từ khóa tìm kiếm
                var searchResult = me.Foods
                    .Where(f => f.name.StartsWith(searchTerm))
                    .Select(c => new { c.id, c.name, c.idCategory, c.price })
                    .ToList();

                // Cập nhật DataGridView với kết quả tìm kiếm
                dgvFood.DataSource = searchResult;

                if (searchResult.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy món ăn nào tương ứng.");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập từ khóa tìm kiếm.");
            }
        }

        private void dgvFood_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvFood.CurrentRow != null)
            {
                if (dgvFood.CurrentRow.Cells["id"].Value != null)
                    txtIDFood.Text = dgvFood.CurrentRow.Cells["id"].Value.ToString();

                if (dgvFood.CurrentRow.Cells["name"].Value != null)
                    txtDish.Text = dgvFood.CurrentRow.Cells["name"].Value.ToString();

                if (dgvFood.CurrentRow.Cells["idCategory"].Value != null)
                    cmbCategory.SelectedValue = dgvFood.CurrentRow.Cells["idCategory"].Value;

                if (dgvFood.CurrentRow.Cells["price"].Value != null)
                    txtPriceFood.Text = dgvFood.CurrentRow.Cells["price"].Value.ToString();
            }
        }

        private void btnDeleteFood_Click(object sender, EventArgs e)
        {
            if (dgvFood.CurrentRow != null)
            {
                int foodID = (int)dgvFood.CurrentRow.Cells["id"].Value;

                var foodToDelete = me.Foods.SingleOrDefault(f => f.id == foodID);
                if (foodToDelete != null)
                {
                    me.Foods.Remove(foodToDelete);
                    me.SaveChanges();
                    LoadData1();
                    MessageBox.Show("Món ăn đã được xóa thành công.");
                }
                else
                {
                    MessageBox.Show("Không tìm thấy món ăn để xóa.");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn món ăn để xóa.");
            }
        }
    }
}
