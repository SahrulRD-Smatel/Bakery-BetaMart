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
    public partial class frmStockin : Form
    {
        public frmStockin()
        {
            InitializeComponent();
        }
        SQLConfig config = new SQLConfig();
        usableFunction func = new usableFunction();
        string sql;
        int maxrow;
        private void frmStockin_Load(object sender, EventArgs e)
        {
            BTNNEW_Click(sender, e);
        }

        private void BTNNEW_Click(object sender, EventArgs e)
        {
            func.clearTxt(GroupBox1);
            func.clearTxt(GroupBox3);
            CBOUNIT.Text = "";
            LBLMSG.Text = "";
            LBLUNIT.Text = "";
            LBLMSG.BackColor = Color.Transparent;

            config.autonumber_transaction(1, LBLTRANSNUM); 

            config.autocomplete("SELECT PROCODE FROM tblProductInfo", txtPROCODE);
        }

        private void BTNCLOSE_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BTNADD_Click(object sender, EventArgs e)
        {
            if(TXTDESC.Text != "" && TXTQTY.Text != "")
            {
                int qty = int.Parse(TXTQTY.Text);
                int transnum = int.Parse(LBLTRANSNUM.Text);

                sql = "INSERT INTO tblStockIn (TRANSNUM,PROCODE,DATERECEIVED,RECEIVEDQTY,RECEIVEDTOTPRICE) " +
                 " VALUES (" + transnum + ",'" + txtPROCODE.Text + "','" + DTPTRANSDATE.Value + "'," + qty + " ,'" + TXTTOT.Text + "')";
                config.Execute_Query(sql);


                //    'ADDING THE QTY OF THE STOCK
                sql = "UPDATE tblProductInfo SET PROQTY = PROQTY + '" + TXTQTY.Text + "'  WHERE PROCODE = '" + txtPROCODE.Text + "'";
                config.Execute_Query(sql);

                LBLMSG.Text = "The " + TXTPRODUCT.Text + " has been added into the inventory.";
                LBLMSG.BackColor = Color.Aquamarine;
                LBLMSG.ForeColor = Color.Black;
                 
          
                config.update_Autonumber(1);

                BTNNEW_Click(sender, e);
            }
            else
            {
                LBLMSG.Text = "Fill up the correct product in the empty fields inorder to save.";
                LBLMSG.BackColor = Color.Red;
                LBLMSG.ForeColor = Color.White;
            }
             
        }

        private void txtPROCODE_TextChanged(object sender, EventArgs e)
        {
            sql = "SELECT * FROM tblProductInfo WHERE PROCODE ='" + txtPROCODE.Text + "'";
            maxrow = config.maxrow(sql);

           if(maxrow > 0)
            {
                foreach(DataRow r in config.dt.Rows)
                {
                    TXTPRODUCT.Text = r.Field<string>("PRONAME");
                    TXTDESC.Text = r.Field<string>("PRODESC") + " [" + r.Field<string>("CATEGORY") + "]";
                    TXTPRICE.Text = r.Field<decimal>("PROPRICE").ToString();
                }
            
            }
           else
            {
                TXTPRODUCT.Clear();
                TXTDESC.Clear();
                TXTPRICE.Clear();
                TXTQTY.Clear();
                TXTTOT.Clear();
            } 
        }

        private void TXTQTY_TextChanged(object sender, EventArgs e)
        {
            double total;
            if( TXTQTY.Text == "")
            {
                TXTTOT.Text ="0";
            }
            else
            {
                total = Double.Parse(TXTPRICE.Text) * Double.Parse(TXTQTY.Text);
                TXTTOT.Text = total.ToString();
            } 
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            Form frm = new frmListStockin();
            frm.ShowDialog();
        }
    }
}
