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

namespace TienSuToCoffee
{
  
    public partial class frmOTP : Form
    {
        MYCOFFEEEntitiesS luv = DataContextSingleton.Instance;
        public static int soLanGui = 5;
        public string textCheck = "";
        public frmOTP()
        {
            InitializeComponent();
        }

        private void lblExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmOTP_Load(object sender, EventArgs e)
        {

        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            textCheck = txtNhapOtp.Text;
            if (txtNhapOtp.Text == "")
            {
                MessageBox.Show("Không được để trống mã OTP");
                return;
            }
            if (txtNhapOtp.Text == SendEmail.otp.ToString())
            {
                
                if (MessageBox.Show("Mã OTP hợp lệ", "Thông báo") == DialogResult.OK)
                {
                    this.Close();
                   ForgotPassword forgotPassword = new ForgotPassword();
                    forgotPassword.Close();
                    Form1 fm = new Form1();
                    fm.Hide();
                   frmManageMent frmManageMent = new frmManageMent();
                    frmManageMent.ShowDialog();

                  
                }
            }
            else
            {
                MessageBox.Show("Mã sai vui lòng nhập lại");
                txtNhapOtp.Text = "";
            }
        }

        private void btnSendAgain_Click(object sender, EventArgs e)
        {

            if (soLanGui > 0)
            {
                soLanGui--;
                MessageBox.Show($"Bạn còn {soLanGui} lần gửi");

                //SendEmail.sendEmail(DangKy.textEmail_Gui, this);
                if (SendEmail.chuoi == "QuenMatKhau")
                    SendEmail.sendEmail(ForgotPassword.dangKy, this);
            

                MessageBox.Show("OTP đã được gửi qua mail\nVui lòng xác nhận vào ô OTP");
            }
            else
            {
                MessageBox.Show($"Bạn đã hết số lần gửi lại", "Cảnh báo");
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
