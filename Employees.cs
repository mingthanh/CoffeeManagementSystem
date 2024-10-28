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
    public partial class Employees : UserControl
    {

        MYCOFFEEEntitiesS me = DataContextSingleton.Instance;
        public Employees()
        {
            InitializeComponent();
        }

        public void Employees_Load(object sender, EventArgs e)
        {
            LoadData();
            cmbType.Items.Add(0);
            cmbType.Items.Add(1);
            pictureBoxGif.Image = Properties.Resources.search;

        }
        void LoadData()
        {
            var result = me.Accounts;
            dgvStudent.DataSource = result.ToList();
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            if (dgvStudent.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một tài khoản để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DataGridViewRow selectedRow = dgvStudent.SelectedRows[0];

            string username = selectedRow.Cells["UserName"].Value.ToString();

            var lo = me.Accounts.First(s => s.UserName == username);

            if (int.TryParse(selectedRow.Cells["Type"].Value.ToString(), out int typeValue))
            {
                if (typeValue == 1) // Giả sử Type = 1 là Admin
                {
                    MessageBox.Show("Không được phép xóa tài khoản Admin.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                else
                {

                    try
                    {
                        me.Accounts.Remove(lo);
                        me.SaveChanges();

                    }
                    catch (Exception ex)
                    {
                        LoadData();
                        MessageBox.Show(ex.Message);

                    }
                    txtUsername.Clear();
                    txtDisplayname.Clear();
                    cmbType.SelectedIndex = -1;
                    MessageBox.Show(" Xóa thành công!");
                    LoadData();
                }
            }

           
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

            if (dgvStudent.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một tài khoản để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DataGridViewRow selectedRow = dgvStudent.SelectedRows[0];
            string username = selectedRow.Cells["UserName"].Value.ToString();
            string displayname = txtDisplayname.Text;
            string password = txtPassword.Text;
            // Lấy dữ liệu từ các điều khiển
            // string name = txtUsername.Text.Trim();
            int typeValue = int.Parse(cmbType.Text);

            var N = me.Accounts.First(s => s.UserName == username);
            N.DisplayName = displayname;
            N.Type = typeValue;
            N.PassWord = password;

            me.SaveChanges();
            LoadData();
        }

        private void dgvStudent_SelectionChanged(object sender, EventArgs e)
        {

            if (dgvStudent.SelectedRows.Count > 0)
            {
                // Lấy hàng đầu tiên được chọn
                DataGridViewRow selectedRow = dgvStudent.SelectedRows[0];

                // Kiểm tra để tránh lỗi khi DataGridView chưa có dữ liệu
                if (selectedRow.Cells["UserName"].Value != null)
                {
                    // Lấy giá trị từ các cột và đổ vào các điều khiển
                    txtUsername.Text = selectedRow.Cells["UserName"].Value.ToString();
                    txtDisplayname.Text = selectedRow.Cells["DisplayName"].Value.ToString();
                    int typeValue = int.Parse(selectedRow.Cells["Type"].Value.ToString());
                    cmbType.Text = typeValue.ToString();
                    txtPassword.Text = selectedRow.Cells["Password"].Value.ToString();

                    // Xử lý giá trị điểm trung bình

                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            frmRegister rg = new frmRegister();

            rg.ShowDialog();
            LoadData();
        }

       
        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {

            string filterText = textBoxSearch.Text.ToLower();

            // Lọc danh sách theo chuỗi tìm kiếm bắt đầu bằng từ khóa
            List<Account> filteredList;

            if (string.IsNullOrEmpty(filterText))
            {
                // Nếu ô tìm kiếm trống, trả về toàn bộ danh sách
                filteredList = me.Accounts.ToList();
            }
            else
            {
                filteredList = me.Accounts.Where(p => p.UserName.ToLower().StartsWith(filterText)).ToList();
            }

            // Cập nhật DataGridView với danh sách đã lọc
            dgvStudent.DataSource = new BindingSource { DataSource = filteredList };

            // Hiển thị số kết quả tìm thấy
            lblResultCount.Text = $"{filteredList.Count}";

            // Chỉ thông báo nếu tìm kiếm không ra kết quả và chuỗi tìm kiếm không trống
            if (filteredList.Count == 0 && !string.IsNullOrEmpty(filterText))
            {
                MessageBox.Show("No results found", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //string filterText = textBoxSearch.Text.ToLower();

            //// Lọc danh sách theo chuỗi tìm kiếm
            //List<Account> filteredList;

            //if (string.IsNullOrEmpty(filterText))
            //{
            //    // Nếu ô tìm kiếm trống, trả về toàn bộ danh sách
            //    filteredList = me.Accounts.ToList();
            //}
            //else
            //{
            //    filteredList = me.Accounts.Where(p => p.UserName.ToLower().Contains(filterText)).ToList();
            //}

            //// Cập nhật DataGridView với danh sách đã lọc
            //dgvStudent.DataSource = new BindingSource { DataSource = filteredList };

            //// Hiển thị số kết quả tìm thấy
            //lblResultCount.Text = $"{filteredList.Count} kết quả tìm thấy";

            //// Chỉ thông báo nếu tìm kiếm không ra kết quả và chuỗi tìm kiếm không trống
            //if (filteredList.Count == 0 && !string.IsNullOrEmpty(filterText))
            //{
            //    MessageBox.Show("No results found", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }

        private void lblResultCount_Click(object sender, EventArgs e)
        {

        }
    }
}
