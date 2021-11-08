using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BakeryInventorySystem
{
    public partial class frmLogin : Form
    {
        Form1 frm;
        public frmLogin(Form1 frm)
        {
            InitializeComponent();
            this.frm = frm;
        }
        SQLConfig config = new SQLConfig();
        usableFunction func = new usableFunction();
        string query;
        int maxrow;

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }

        private void OK_Click(object sender, EventArgs e)
        {
            query = "SELECT * FROM tblUser WHERE U_UNAME ='" + UsernameTextBox.Text + "' AND U_PASS = '" + PasswordTextBox.Text + "'";
            maxrow = config.maxrow(query);

            if(maxrow > 0)
            {
                MessageBox.Show("User successfully logged in");
                frm.enable_menu();
                this.Close();
            }
            else
            {
                MessageBox.Show("Account does not exist!","Invalid",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }
    }
}
