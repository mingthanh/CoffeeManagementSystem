using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace TienSuToCoffee
{
    public partial class Customers : UserControl
    {
        MYCOFFEEEntitiesS me = DataContextSingleton.Instance;
        public Customers()
        {
            InitializeComponent();
        }

        private void Customers_Load(object sender, EventArgs e)
        {
            DataLoad();
           // AddBinding();
        }

        void DataLoad()
        {
            var N = me.Customers.ToList();
            dgvCustomers.DataSource = N;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy thông tin từ các trường nhập liệu
                string customerGmail = txtGmail.Text.Trim();
                string fullName = txtFullname.Text.Trim();
                string password = txtPassword.Text.Trim();
                string phoneNumber = txtPhone.Text.Trim();

                // Kiểm tra xem các trường có được nhập đầy đủ không
                if (string.IsNullOrEmpty(customerGmail) || string.IsNullOrEmpty(fullName) ||
                    string.IsNullOrEmpty(password) || string.IsNullOrEmpty(phoneNumber))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra xem email đã tồn tại chưa
                var existingCustomer = me.Customers.FirstOrDefault(c => c.CustomerGmail == customerGmail);
                if (existingCustomer != null)
                {
                    MessageBox.Show("Email này đã được sử dụng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Tạo đối tượng khách hàng mới
                Customer newCustomer = new Customer
                {
                    CustomerGmail = customerGmail,
                    FullName = fullName,
                    Password = password,
                    PhoneNumber = phoneNumber,
                    Status = true, // Hoặc gán giá trị khác nếu cần
                    RegistrationDate = DateTime.Now,
                    Points = 0 // Khởi tạo điểm
                };

                // Thêm khách hàng vào cơ sở dữ liệu
                me.Customers.Add(newCustomer);
                me.SaveChanges();

                // Cập nhật lại DataGridView để hiển thị khách hàng vừa thêm
                Customers_Load(sender, e); // Gọi lại hàm Load để refresh dữ liệu

                MessageBox.Show("Khách hàng đã được thêm thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            DataLoad();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem có khách hàng nào được chọn trong DataGridView không
                if (dgvCustomers.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn một khách hàng để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Lấy thông tin khách hàng được chọn
                var selectedRow = dgvCustomers.SelectedRows[0];
                string customerGmail = selectedRow.Cells["CustomerGmail"].Value.ToString();

                // Tìm khách hàng trong cơ sở dữ liệu
                var customerToDelete = me.Customers.FirstOrDefault(c => c.CustomerGmail == customerGmail);
                if (customerToDelete != null)
                {
                    // Xóa khách hàng
                    me.Customers.Remove(customerToDelete);
                    me.SaveChanges();

                    // Cập nhật lại DataGridView để hiển thị dữ liệu mới
                    Customers_Load(sender, e); // Gọi lại hàm Load để refresh dữ liệu

                    MessageBox.Show("Khách hàng đã được xóa thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy khách hàng để xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            DataLoad();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvCustomers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một sinh viên để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DataGridViewRow selectedRow = dgvCustomers.SelectedRows[0];
            string mssv = selectedRow.Cells["CustomerGmail"].Value.ToString();
            // Lấy dữ liệu từ các điều khiển
        
            string fullname = txtFullname.Text.Trim();
            string password = txtPassword.Text.Trim();
            string phone = txtPhone.Text.Trim();

            var N = me.Customers.First(s => s.CustomerGmail == mssv);
           
            N.FullName = fullname;
            N.Password = password;
            N.PhoneNumber = phone;
            me.SaveChanges();
            DataLoad();
        }

        private void dgvCustomers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Kiểm tra xem người dùng có nhấp vào hàng hợp lệ không
                if (e.RowIndex < 0) return; // Nếu nhấp vào tiêu đề cột thì thoát

                // Lấy thông tin của khách hàng từ hàng đã chọn
                DataGridViewRow selectedRow = dgvCustomers.Rows[e.RowIndex];
                string customerGmail = selectedRow.Cells["CustomerGmail"].Value.ToString();
                string fullName = selectedRow.Cells["FullName"].Value.ToString();
                string password = selectedRow.Cells["Password"].Value.ToString();
                string phoneNumber = selectedRow.Cells["PhoneNumber"].Value.ToString();

                // Điền thông tin vào các trường nhập liệu
                txtGmail.Text = customerGmail;
                txtFullname.Text = fullName;
                txtPassword.Text = password;
                txtPhone.Text = phoneNumber;

                // Bật nút Chỉnh sửa và ẩn nút Thêm
               // btnAdd.Visible = false; // Ẩn nút thêm
               // btnSave.Visible = true; // Hiện nút lưu
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void AddBinding()
        {
            txtGmail.DataBindings.Clear();
            txtFullname.DataBindings.Clear();
            txtPassword.DataBindings.Clear();
            txtPhone.DataBindings.Clear();

            // Thêm Binding mới
            txtGmail.DataBindings.Add(new Binding("Text", dgvCustomers.DataSource, "CustomerGmail"));
            txtFullname.DataBindings.Add(new Binding("Text", dgvCustomers.DataSource, "FullName"));
            txtPassword.DataBindings.Add(new Binding("Text", dgvCustomers.DataSource, "Password"));
            txtPhone.DataBindings.Add(new Binding("Text", dgvCustomers.DataSource, "PhoneNumber"));

        }

        private void btnView_Click(object sender, EventArgs e)
        {
            DataLoad();
        }
    }
}
