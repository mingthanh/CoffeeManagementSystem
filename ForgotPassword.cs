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
    public partial class ForgotPassword : Form
    {
        MYCOFFEEEntitiesS luv = DataContextSingleton.Instance;
        public MenuStrip MenuAdmin { get; set; }
        public static string dangKy="";
        public ForgotPassword()
        {
            InitializeComponent();
           
        }
        frmManageMent me = new frmManageMent(); 
        public void btnGetPass_Click(object sender, EventArgs e)
        {
          frmOTP fr = new frmOTP();          
            Form1 frm = new Form1();
            frm.Hide();
              
                this.Show();
            SendEmail.chuoi = "ForgotPassword";
            string email = txtEmail1.Text;
            dangKy = email;
            // Kiểm tra email có tồn tại trong hệ thống với điều kiện Type == 0
           
            var account = luv.Accounts.FirstOrDefault(p => p.UserName == txtEmail1.Text && p.Type ==1);
            if (account != null)
            {
                
                // Nếu tài khoản tồn tại
                MessageBox.Show("Email hợp lệ, vui lòng kiểm tra mã OTP.");
                SendEmail.GuiOTp(txtEmail1.Text, this);
               
               fr.Hide();
            
               
            }
            else
            {
                // Nếu không có tài khoản nào với email và Type == 1
                MessageBox.Show("Email không hợp lệ hoặc không tồn tại trong hệ thống.");
            }
           
             
        }

        private void lblExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ForgotPassword_Load(object sender, EventArgs e)
        {

            //if (UserSession.UserType == 1)
            //{
            //    MenuAdmin.Visible = false; // Hiện thị menuAdmin ở Form2 (nếu cần)
            //}
        }
    }
}
