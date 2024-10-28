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
    
    public partial class frmRegister : Form
    {
        MYCOFFEEEntitiesS luv = DataContextSingleton.Instance;
        public frmRegister()
        {
            InitializeComponent();
        }

        private void frmRegister_Load(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void frmRegister_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you really want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                e.Cancel = true; // Hủy sự kiện đóng form
            }
        }

        void LoadAccount()
        {
           
          
        }
   

        private void btnAccept_Click(object sender, EventArgs e)
        {
            string email = txtLogin.Text;
            string displayName = txtDisplayName.Text.Trim();
            string password = txtPassword.Text.Trim();
            string repass = txtRepassword.Text.Trim();
            int type = 0;

            // Kiểm tra định dạng email
            if (!IsValidEmail(email))
            {
                MessageBox.Show("Địa chỉ email không hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtLogin.Focus();
                return;
            }

            // Kiểm tra mật khẩu và xác nhận mật khẩu
            if (password != repass)
            {
                MessageBox.Show("Mật khẩu và xác nhận mật khẩu không khớp.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtRepassword.Focus();
                return;
            }

            // Kiểm tra email đã tồn tại hay chưa
            if (luv.Accounts.Any(a => a.UserName == email))
            {
                MessageBox.Show("Email này đã tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtLogin.Focus();
                return;
            }

            // Mã hóa mật khẩu (hash password)
           // string hashedPassword = HashPassword(password);

            // Thêm tài khoản mới
            Account acc = new Account()
            {
                UserName = email,
                DisplayName = displayName,
                PassWord = password, // Lưu mật khẩu đã mã hóa
                Type = type
            };

            luv.Accounts.Add(acc);
            luv.SaveChanges();

            MessageBox.Show("Đăng ký thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
            #region
            //string username = txtLogin.Text;
            //string displayname = txtDisplayName.Text;
            //string password = txtPassword.Text.Trim();
            //string repass = txtRepassword.Text.Trim();
            //int type = 0;
            //Console.WriteLine($"Password: {password}, Repassword: {repass}");
            //if (password != repass)
            //{
            //    MessageBox.Show("Mật khẩu và xác nhận mật khẩu không khớp.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    txtRepassword.Focus();
            //    return;
            //}

            //Account acc = new Account()
            //{
            //    UserName = username,
            //    DisplayName = displayname,
            //    PassWord = password,
            //    Type = type,

            //};
           
            //luv.Accounts.Add(acc);
            //luv.SaveChanges();
            //MessageBox.Show("Successful! Congratulation");
            //this.Close();
            #endregion
        }


        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        //private string HashPassword(string password)
        //{
        //    using (var sha256 = System.Security.Cryptography.SHA256.Create())
        //    {
        //        byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        //        StringBuilder builder = new StringBuilder();
        //        foreach (byte b in bytes)
        //        {
        //            builder.Append(b.ToString("x2"));
        //        }
        //        return builder.ToString();
        //    }
        //}
    }
}
