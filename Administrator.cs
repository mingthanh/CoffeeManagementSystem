using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Windows.Interop;





namespace TienSuToCoffee
{
    public partial class frmAdministrator : Form
    {
        MYCOFFEEEntitiesS me = DataContextSingleton.Instance;
        public frmAdministrator()
        {
            InitializeComponent();
         

        }

        private void frmAdministrator_Load(object sender, EventArgs e)
        {
            
            pictureBox2.Image = Properties.Resources.customer;

        }

        void LoadCMB()
        {
           
        }
      
      
      


      
      
        private void btnAddFood_Click(object sender, EventArgs e)
        {
        
        }

        private void btnDeleteFood_Click(object sender, EventArgs e)
        {

          

        }

        private void btnEditFood_Click(object sender, EventArgs e)
        {
           
           
        }

        private void btnSearchFood_Click(object sender, EventArgs e)
        {
           
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
      

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

           
        }

        

        private void btnEdit_Click(object sender, EventArgs e)
        {
            

        }

        private void dgvStudent_SelectionChanged(object sender, EventArgs e)
        {

           
        }



      


        private void dgvTableFood_SelectionChanged(object sender, EventArgs e)
        {
           
        }

        private void btnEditTable_Click(object sender, EventArgs e)
        {
            
        }

        private void btnAddTable_Click(object sender, EventArgs e)
        {

           
        }

        private void btnDeleteTable_Click(object sender, EventArgs e)
        {

       
            }
        private void LoadBillData()
        {
       
        }
        private void dgvBill_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
           
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
           
        }

        private void dgvStudent_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        NetworkCredential login;
        SmtpClient client;
        MailMessage msg;

        private void btnSend_Click_1(object sender, EventArgs e)
        {

        //    login = new NetworkCredential(txtUsername.Text, txtPassword.Text);
        //    client = new SmtpClient(txtSmtp.Text);
        //    client.Port = Convert.ToInt32(txtPort.Text);
        //    client.EnableSsl = chkSSL.Checked;
        //    client.Credentials = login;
        //    msg = new MailMessage{From = new MailAddress(txtUsername.Text+txtSmtp.Text.Replace("smtp.","@"),"ngoc",Encoding.UTF8)};
        //    msg.To.Add(new MailAddress(txtTo.Text));
        //    if (!string.IsNullOrEmpty(txtCC.Text))
        //        msg.To.Add(new MailAddress(txtCC.Text));
        //    msg.Subject = txtSubject.Text;
        //    msg.Body = txtMessage.Text;
        //    msg.BodyEncoding = Encoding.UTF8;
        //    msg.IsBodyHtml = true;
        //    msg.Priority = MailPriority.Normal;
        //    msg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
        //    client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
        //    string userstate = "Sending...";
        //    client.SendAsync(msg, userstate);
        }
        private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            //if (e.Cancelled)
            //    MessageBox.Show(string.Format("{0} send canceled.", e.UserState), "Message");
            //if (e.Error != null)
            //    MessageBox.Show(string.Format("{0} {1}", e.UserState, e.Error), "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //else
            //    MessageBox.Show("Your message has been successfully sent.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnGmail_Click(object sender, EventArgs e)
        {
            //frmGmail frmGmail = new frmGmail();
            //this.Hide();
            //frmGmail.ShowDialog();
            //this.Show();
        }

        private void employees1_Load(object sender, EventArgs e)
        {

        }


        #region
        private void panelContainer_Paint(object sender, PaintEventArgs e)
        {

        }
        private void LoadUserControl(UserControl uc)
        {
            // Xóa các control hiện tại trong panel
            panelContainer.Controls.Clear();

            // Thiết lập dock cho UserControl để nó lấp đầy Panel
            uc.Dock = DockStyle.Fill;

            // Thêm UserControl vào Panel
            panelContainer.Controls.Add(uc);
        }

        private void btnEmployees_Click(object sender, EventArgs e)
        {
            Employees uc1 = new Employees();
            LoadUserControl(uc1);
        }

        private void btnIncome_Click(object sender, EventArgs e)
        {
            Income uc1 = new Income();
            LoadUserControl(uc1);
        }

        private void btnCafe_Click(object sender, EventArgs e)
        {
            Cafe uc1 = new Cafe();
            LoadUserControl(uc1);
        }

        private void btnTable_Click(object sender, EventArgs e)
        {
            TableControl uc1 = new TableControl();
            LoadUserControl(uc1);
        }
        #endregion

        private void btnManage_Click(object sender, EventArgs e)
        {
            frmManageMent frmManageMent = new frmManageMent();
            this.Hide();        
            frmManageMent.ShowDialog();
            this.Close();
            
            
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            Customers uc1 = new Customers();
            LoadUserControl(uc1);
        }

        private void lblexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
    

