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
    public partial class AccountAccept : Form
    {
        public AccountAccept()
        {
            InitializeComponent();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
            if (UserSession.UserType == 1)
            {             
                frmAdministrator frmAdministrator = new frmAdministrator();             
                frmAdministrator.ShowDialog();
            }
            else
            {
                frmManageMent mn = new frmManageMent();
                mn.ShowDialog();
            }
        }

        private void AccountAccept_Load(object sender, EventArgs e)
        {
          
        }
    }
}
