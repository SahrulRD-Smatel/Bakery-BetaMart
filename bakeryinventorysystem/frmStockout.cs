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
    public partial class frmStockout : Form
    {
        public frmStockout()
        {
            InitializeComponent();
        }
        SQLConfig config = new SQLConfig();
        usableFunction func = new usableFunction();
        string query;
        int maxrow;
        private void BTNNEW_Click(object sender, EventArgs e)
        {
            func.clearTxt(GroupBox1);
            func.clearTxt(GroupBox2);
            LBLMSG.Text = "";
            LBLMSG.BackColor = Color.Transparent;


            query = "SELECT  PROCODE  FROM   tblProductInfo";
            config.autocomplete(query, txtPROCODE);

            config.autonumber_transaction(1, LBLTRANSNUM);
       
        }

        private void frmStockout_Load(object sender, EventArgs e)
        {
            BTNNEW_Click(sender, e);
        }

        private void txtPROCODE_TextChanged(object sender, EventArgs e)
        {
            query = "SELECT * FROM tblProductInfo WHERE PROCODE ='" + txtPROCODE.Text + "'";
            maxrow = config.maxrow(query);

            if (maxrow > 0)
            {
                foreach (DataRow r in config.dt.Rows)
                {
                    TXTPRODUCT.Text = r.Field<string>("PRONAME");
                    TXTDESC.Text = r.Field<string>("PRODESC") + " [" + r.Field<string>("CATEGORY") + "]";
                    TXTPRICE.Text = r.Field<decimal>("PROPRICE").ToString();
                    TXTAVAILQTY.Text = r.Field<int>("PROQTY").ToString();
                }

            }
            else
            {
                TXTPRODUCT.Clear();
                TXTDESC.Clear();
                TXTPRICE.Clear();
                TXTQTY.Clear();
                TXTTOT.Clear();
                TXTAVAILQTY.Clear();
            }
        }

        private void TXTQTY_TextChanged(object sender, EventArgs e)
        {
            double remainqty, totalamount;
            if(TXTQTY.Text == "")
            {
                TXTTOT.Text = "0";
                TXTREMAINQTY.Text = TXTAVAILQTY.Text;
            }
            else
            {
                remainqty= Double.Parse(TXTAVAILQTY.Text) - Double.Parse(TXTQTY.Text);
                TXTREMAINQTY.Text = remainqty.ToString();
                totalamount = Double.Parse(TXTPRICE.Text) * Double.Parse(TXTQTY.Text);
                TXTTOT.Text = totalamount.ToString();
            } 
        }

        private void BTNCLOSE_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BTNSAVE_Click(object sender, EventArgs e)
        {
            if( TXTDESC.Text != "" &&  TXTQTY.Text != "")
            {
                double Rqty = Double.Parse(TXTAVAILQTY.Text);
                double qty = Double.Parse(TXTQTY.Text);

                if ( Rqty < qty)
                {
                    LBLMSG.Text = "Not valid. The entered quantity is greater than the available quantity!";
                    LBLMSG.BackColor = Color.Red;
                    LBLMSG.ForeColor = Color.White;
                }
                else
                { 
                    query = "INSERT INTO tblStockOut (TRANSNUM,PROCODE,DATEOUT,OUTQTY,OUTUNIT,OUTTOTPRICE ) " +
                        " VALUES ('" + LBLTRANSNUM.Text + "','" + txtPROCODE.Text + "','" + DTPTRANSDATE.Value + "','" + TXTQTY.Text + "','" + LBLUNIT.Text + "','" + TXTTOT.Text + "')";
                    config.Execute_Query(query);

                    query = "UPDATE tblProductInfo SET PROQTY = PROQTY - " + TXTQTY.Text + "  WHERE PROCODE = '" + txtPROCODE.Text + "'";
                     config.Execute_Query(query);


                    LBLMSG.Text = "The " + TXTPRODUCT.Text + " has been deducted into the inventory.";
                    LBLMSG.BackColor = Color.Aquamarine;
                    LBLMSG.ForeColor = Color.Black;
                    ;
                    config.update_Autonumber(1);

                    BTNNEW_Click(sender, e);

                }
            }
            else
            {
                LBLMSG.Text = "Fill up the correct product in the empty fields inorder to save.";
                LBLMSG.BackColor = Color.Red;
                LBLMSG.ForeColor = Color.White;
            }
 
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            Form frm = new frmListStockout();
            frm.ShowDialog();
        }
    }
}
