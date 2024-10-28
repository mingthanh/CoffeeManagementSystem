using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;
using System.Security.Principal;

namespace TienSuToCoffee
{
    public partial class Form1 : Form
    {
        MYCOFFEEEntitiesS me = DataContextSingleton.Instance;
        Account user;
       
        public Form1()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            frmRegister rg = new frmRegister();
            this.Hide();
            rg.ShowDialog();
            this.Show();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string userName = txtUser.Text.Trim();
            string passWord = txtPassword.Text.Trim();

            // Khai báo biến user
           

            // Gọi hàm Login để xác thực
            if (Login(userName, passWord, out user)) // Truyền mật khẩu đã mã hóa vào hàm Login
            {
                UserSession.UserType = user.Type; // Lưu loại tài khoản vào UserSession
                UserSession.UserName = user.UserName;
                UserSession.Password = user.PassWord;
                UserSession.UserDisplayname=user.DisplayName;
                AccountAccept aa = new AccountAccept();
                aa.ShowDialog();
                //MessageBox.Show("Chào mừng chủ nhân quay trở lại","",MessageBoxButtons.OK, MessageBoxIcon.Information);
             
                // Mở form quản lý
               // aa.Close();
                    //frmManageMent mn = new frmManageMent();
                    //this.Hide();
                    //mn.ShowDialog();
                    //this.Show();
                

            }
            else
            {
                MessageBox.Show("Invalid username or password.");
            }
     
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

            DialogResult result = MessageBox.Show("Do you really want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                e.Cancel = true; // Hủy sự kiện đóng form
            }
        }

        bool Login(string username, string password, out Account user)
        {

            user = null;

            // Kiểm tra điều kiện null cho me.Accounts trước khi tìm kiếm
            if (me.Accounts == null)
            {
                return false;
            }

            // Tìm tài khoản có username và password khớp
            user = me.Accounts.SingleOrDefault(u => u.UserName == username && u.PassWord == password);

            // Trả về true nếu tài khoản hợp lệ, false nếu không tìm thấy
            return user != null;
            //user = me.Accounts.SingleOrDefault(u => u.UserName == username && u.PassWord == password);

            //return user != null;


        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnForget_Click(object sender, EventArgs e)
        {
            ForgotPassword fr = new ForgotPassword();
            this.Hide();
            fr.ShowDialog();
            this.Show();
        }

        private void lbleyeShut_Click(object sender, EventArgs e)
        {
            // Kiểm tra trạng thái hiển thị của mật khẩu
            if (txtPassword.UseSystemPasswordChar == false)
            {
                // Nếu mật khẩu đang bị ẩn, thì hiển thị nó
                txtPassword.UseSystemPasswordChar = true;
                // Thay đổi icon thành mắt mở (nếu có)
                lbleyeShut.Hide();
                lbleyeOpen.Show();
            }
        }
        

        private void lbleyeOpen_Click(object sender, EventArgs e)
        {
            // Kiểm tra trạng thái hiển thị của mật khẩu
            if (txtPassword.UseSystemPasswordChar == true)
            {
                // Nếu mật khẩu đang bị ẩn, thì hiển thị nó
                txtPassword.UseSystemPasswordChar = false;
                lbleyeOpen.Hide();
                lbleyeShut.Show();

                
            }
        }

        private void btnLoginCustomer_Click(object sender, EventArgs e)
        {
            loginCustom loginCustom = new loginCustom();
            this.Hide();
            loginCustom.ShowDialog();
            this.Show();
        }
    }
}
