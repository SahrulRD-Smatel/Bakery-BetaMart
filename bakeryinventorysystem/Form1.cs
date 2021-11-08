using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BakeryInventorySystem.Properties;

namespace BakeryInventorySystem
{
    public partial class Form1 : Form
    {
        //Sahrul Ramadhani(3103119171)
        //XII RPL 5
        //32
        public Form1()
        {
            InitializeComponent();
        }
        private void shwfrm( Form frm)
        { 
            frm.ShowDialog();
        }

        public void enable_menu()
        {
            tsBread.Enabled = true;
            tsListofProducts.Enabled = true;
            tsLogin.Text = "Logout";
            tsLogin.Image = Properties.Resources.logout;
            tsReport.Enabled = true;
            tsStockin.Enabled = true;
            tsStockout.Enabled = true;
            tsUser.Enabled = true;

        }

        private void disable_menu()
        {
            tsBread.Enabled = false;
            tsListofProducts.Enabled = false;
            tsLogin.Text = "Login";
            tsLogin.Image = Properties.Resources.login;
            tsReport.Enabled = false;
            tsStockin.Enabled = false;
            tsStockout.Enabled = false;
            tsUser.Enabled = false;

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            disable_menu();
        }

        private void tsBread_Click(object sender, EventArgs e)
        {
            shwfrm(new frmProduct());
        }

        private void tsListofProducts_Click(object sender, EventArgs e)
        {
            shwfrm(new frmListofProducts());
        }

        private void tsStockin_Click(object sender, EventArgs e)
        {
            shwfrm(new frmStockin());
        }

        private void tsStockout_Click(object sender, EventArgs e)
        {
            shwfrm(new frmStockout());
        }

        private void tsUser_Click(object sender, EventArgs e)
        {
            shwfrm(new frmUser());
        }

        private void tsReport_Click(object sender, EventArgs e)
        {
            shwfrm(new frmInventoryReports());
        }

        private void tsLogin_Click(object sender, EventArgs e)
        {
            if(tsLogin.Text == "Login")
            {
                shwfrm(new frmLogin(this));
            }
            else
            {
                disable_menu();
            }
           
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
