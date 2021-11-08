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
    public partial class frmProduct : Form
    {
        public frmProduct()
        {
            InitializeComponent();
        }
        SQLConfig config = new SQLConfig();
        usableFunction func = new usableFunction();
        string sql;
        int maxrow;

        private void Button1_Click(object sender, EventArgs e)
        {
            func.clearTxt(this);
            cboCateg.Text = "Select";
            config.autonumber(2, txtPROCODE);
        }

        private void frmProduct_Load(object sender, EventArgs e)
        {
            Button1_Click(sender, e);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BTNSAVE_Click(object sender, EventArgs e)
        {


            if (TXTPRONAME.Text == "" || TXTDESC.Text == "" || cboCateg.Text == "Select" || TXTPRICE.Text == "")
            {
                func.messagerequired();
            }


            sql = "SELECT * FROM tblProductInfo WHERE PROCODE='" + txtPROCODE.Text + "'";
            maxrow = config.maxrow(sql);

            if (maxrow > 0)
            {
                sql = "UPDATE  tblProductInfo  SET PRONAME='" + TXTPRONAME.Text +
                    "' ,PRODESC='" + TXTDESC.Text +
                    "',CATEGORY='" + cboCateg.Text +
                    "',PROPRICE=" + TXTPRICE.Text + "  WHERE PROCODE='" + txtPROCODE.Text + "'";
                config.Execute_CUD(sql, "Error to update Bread", "Bread Has Been Updated.");
            }
            else
            {
                sql = "INSERT INTO tblProductInfo (PROCODE,PRONAME,PRODESC,CATEGORY,PROPRICE,PROQTY)"
                      + " VALUES ( '" + txtPROCODE.Text + "', '" + TXTPRONAME.Text + "','" + TXTDESC.Text + "','" + cboCateg.Text + "','" + TXTPRICE.Text + "',0)";
                config.Execute_CUD(sql, "Error to save Product.", "New Bread Has Been Saved.");

                config.update_Autonumber(2);
            }

            Button1_Click(sender, e);
          
        }
    }
}
