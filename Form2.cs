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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace TienSuToCoffee
{
   
    public partial class frmPersonal : Form
    {
        MYCOFFEEEntitiesS me = DataContextSingleton.Instance;
      
        public frmPersonal()
        {
            InitializeComponent();
        }
        string login = UserSession.UserName;
        string pass = UserSession.Password;

        private void frmPersonal_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you really want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                e.Cancel = true; // Hủy sự kiện đóng form
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmPersonal_Load(object sender, EventArgs e)
        {
          
            txtLogin.Text = login;
   
            txtPassword.Text = pass;
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            string name = txtDisplayname.Text.Trim();
            string newp = txtnewPass.Text.Trim();
            string renew = txtreNewpass.Text.Trim();

            // Kiểm tra xem các trường có bị bỏ trống hay không
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Tên hiển thị không được để trống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDisplayname.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(newp))
            {
                MessageBox.Show("Mật khẩu mới không được để trống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtnewPass.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(renew))
            {
                MessageBox.Show("Xác nhận mật khẩu không được để trống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtreNewpass.Focus();
                return;
            }

            var N = me.Accounts.First(s => s.UserName == login);

            if (newp == renew && newp != pass)
            {
                N.DisplayName = name;

                // Cập nhật loại tài khoản
                N.Type = (N.Type != 1) ? 0 : 1;
                N.PassWord = newp;

                me.SaveChanges();
                MessageBox.Show("Đổi thành công!");
                this.Close();
            }
            else
            {
                if (newp != renew)
                {
                    MessageBox.Show("Mật khẩu mới và xác nhận mật khẩu không khớp.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtreNewpass.Focus();
                }
                else
                {
                    MessageBox.Show("Mật khẩu mới không được giống mật khẩu cũ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtnewPass.Focus();
                }
            }

            //string name = txtDisplayname.Text;  
            //string newp = txtnewPass.Text;
            //string renew = txtreNewpass.Text;
            //var N = me.Accounts.First(s => s.UserName == login);
            //if (newp == renew && newp != pass)
            //{
            //    N.DisplayName = name;
            //    if(N.Type!=1)
            //    N.Type = 0;
            //    else
            //        N.Type = 1;
            //    N.PassWord = newp;

            //    me.SaveChanges();
            //    MessageBox.Show("Doi thanh cong!");
            //    this.Close();
            //}

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }

}
