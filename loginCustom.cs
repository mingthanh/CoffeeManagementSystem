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
    public partial class loginCustom : Form
    {
        MYCOFFEEEntitiesS me = DataContextSingleton.Instance;
        Customer use;
        public loginCustom()
        {
            InitializeComponent();
        }

        private void loginCustom_Load(object sender, EventArgs e)
        {
       
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            string userName = txtlogin.Text.Trim();
            string passWord = txtpassword.Text.Trim();

            // Khai báo biến user


            // Gọi hàm Login để xác thực
            if (Login(userName, passWord, out use)) // Truyền mật khẩu đã mã hóa vào hàm Login
            {
                UserSession.UserEmail = use.CustomerGmail; // Lưu loại tài khoản vào UserSession
                UserSession.password = use.Password;
                UserSession.UserFullname = use.FullName;
                UserSession.UserPhone = use.PhoneNumber;
              
                frmManageMent mn = new frmManageMent();
                this.Hide();
                mn.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
            }

        }
        bool Login(string username, string password, out Customer use)
        {

            use = null;

          
            if (me.Customers == null)
            {
                return false;
            }

            // Tìm tài khoản có username và password khớp
            use = me.Customers.SingleOrDefault(u => u.CustomerGmail == username && u.Password == password);

           
            return use != null;
         


        }
    }
}
