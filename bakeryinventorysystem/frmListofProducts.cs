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
    public partial class frmListofProducts : Form
    {
        public frmListofProducts()
        {
            InitializeComponent();
        }
        SQLConfig config = new SQLConfig();
        usableFunction func = new usableFunction();
        string sql;
        int maxrow;
        private void btnNew_Click(object sender, EventArgs e)
        {
            Form frm = new frmProduct();
            frm.ShowDialog();
        }

        public void refresh()
        {
            sql = "SELECT PROCODE as [ProductCode], (PRONAME + ' ' + PRODESC) AS [Product],CATEGORY AS [Category], (PROPRICE) AS [Price],PROQTY AS [Quantity] FROM  tblProductInfo";
            config.Load_DTG(sql, DTGLIST);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            sql = "SELECT PROCODE as [ProductCode], (PRONAME + ' ' + PRODESC) AS [Product],CATEGORY AS [Category], (PROPRICE) AS [Price],PROQTY AS [Quantity] FROM  tblProductInfo";
            config.Load_DTG(sql, DTGLIST);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            sql = "DELETE * FROM tblStockIn WHERE PROCODE = '" + DTGLIST.CurrentRow.Cells[0].Value + "'";
            config.Execute_Query(sql); 

            sql = "DELETE * FROM tblStockOut WHERE PROCODE = '" + DTGLIST.CurrentRow.Cells[0].Value + "'";
            config.Execute_Query(sql);
              
            sql = "DELETE * FROM tblProductInfo WHERE PROCODE = '" + DTGLIST.CurrentRow.Cells[0].Value + "'";
            config.Execute_CUD(sql, "Failed to delete", "Product has been deleted.");
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Form frm = new frmEditProduct(DTGLIST.CurrentRow.Cells[0].Value.ToString(),this);
            frm.ShowDialog();
        }

        private void frmListofProducts_Load(object sender, EventArgs e)
        {
            btnRefresh_Click(sender, e);
        }

        private void TXTSEARCH_TextChanged(object sender, EventArgs e)
        {
             
            DTGLIST.Columns.Clear();
             
            sql = "SELECT PROCODE as [ProductCode], (PRONAME + ' ' + PRODESC) AS [Product],CATEGORY AS [Category], (PROPRICE) AS [Price],PROQTY  AS [Quantity] FROM  tblProductInfo WHERE  (PROCODE + ' ' + PRONAME + ' ' + PRODESC  + ' ' + CATEGORY)  LIKE '%" + TXTSEARCH.Text + "%'";
            config.Load_DTG(sql, DTGLIST);


        }
    }
}
