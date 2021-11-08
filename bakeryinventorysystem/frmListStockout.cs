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
    public partial class frmListStockout : Form
    {
        public frmListStockout()
        {
            InitializeComponent();
        }
        SQLConfig config = new SQLConfig();
        usableFunction func = new usableFunction();
        string query;
        int maxrow;

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            query = " SELECT TRANSNUM AS [Transaction#],P.PROCODE as [ProductCode], PRONAME AS [Product], CATEGORY AS [Category],DATEOUT AS  [TransactionDate],OUTQTY AS [Quantity],OUTTOTPRICE AS [Price] " +
               " FROM tblStockOut as O, tblProductInfo AS P  " +
               " WHERE P.PROCODE=O.PROCODE  AND (PRONAME + ' ' + PRODESC + ' ' + CATEGORY) LIKE '%" + TXTSEARCH.Text + "%'";
            config.Load_DTG(query, DTGLIST);
        }

        private void TXTSEARCH_TextChanged(object sender, EventArgs e)
        {
            btnRefresh_Click(sender, e);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            query = "UPDATE tblProductInfo AS P, tblStockOut AS S SET PROQTY = PROQTY + OUTQTY  WHERE P.PROCODE=S.PROCODE AND TRANSNUM = " + DTGLIST.CurrentRow.Cells[0].Value;
            config.Execute_Query(query);

            query = "DELETE * FROM tblStockOut WHERE TRANSNUM = " + DTGLIST.CurrentRow.Cells[0].Value;
            config.Execute_Query(query);

            MessageBox.Show("Transaction has been deleted.");
            btnRefresh_Click(sender, e);
        }

        private void frmListStockout_Load(object sender, EventArgs e)
        {
            btnRefresh_Click(sender, e);
        }
    }
}
