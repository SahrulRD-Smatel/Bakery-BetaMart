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
    public partial class frmPrint : Form
    {


        SQLConfig config = new SQLConfig();
        usableFunction func = new usableFunction();
        string query;
        int maxrow;


        private void reports(string sql, string rptname)
        {

            try
            {

                config.loadReports(sql);

                string reportname = rptname;


                CrystalDecisions.CrystalReports.Engine.ReportDocument reportdoc = new CrystalDecisions.CrystalReports.Engine.ReportDocument(); ;

                string strReportPath = Application.StartupPath + "\\reports\\" + reportname + ".rpt";


                reportdoc.Load(strReportPath);
                reportdoc.SetDataSource(config.dt);

                crystalReportViewer1.ReportSource = reportdoc;
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "No crystal reports installed. Pls. contact administrator.");
            }


        }

        public frmPrint (string lbllist, string category,string dtpFrom,string dtpTo,string txtsearch,
            RadioButton rdoToday,RadioButton rdoWeekly,RadioButton rdoMonthly,RadioButton rdoStockIn,
            RadioButton rdoStockOut,RadioButton rdoProduct)
        {
            InitializeComponent();

            DateTime dfrom = DateTime.Parse(dtpFrom);
            DateTime dto = DateTime.Parse(dtpTo);

            if (lbllist == "Stock of " + category + " from " + dfrom + " to " + dto)
            {
                query = "SELECT  P.PROCODE AS [ProductCode],DATEVALUE(DATERECEIVED) AS [ReceivedDate], (PRONAME + ' ' + PRODESC) AS [Product],(PROPRICE) AS [Price],RECEIVEDQTY  AS [Quantity],((ROUND(RECEIVEDQTY * PROPRICE)) ) AS [TotalPrice] " +
                     " FROM tblStockIn AS S,  tblProductInfo AS P " +
               " WHERE S.PROCODE=P.PROCODE AND DATEVALUE(DATERECEIVED) BETWEEN #" + dfrom + "# AND #" + dto + "#  AND PRODESC LIKE '%" + txtsearch + "%' AND PRONAME LIKE '%" + txtsearch + "%'";
                reports(query, "stockinasof");

            }
            else if (lbllist == "Sold " + category + " from " + dfrom + " to " + dto)
            {
                query = "SELECT P.PROCODE AS [ProductCode],DATEVALUE(DATEOUT) AS [TransactionDate], (PRONAME + ' ' + PRODESC) AS [Product],(PROPRICE) AS [Price],OUTQTY   AS [Quantity],((ROUND(OUTQTY * PROPRICE))) AS [TotalPrice] " +
                     " FROM tblStockOut AS S,  tblProductInfo AS P  " +
                     " WHERE S.PROCODE=P.PROCODE  AND  DATEVALUE(DATEOUT) BETWEEN #" + dfrom + "# AND #" + dto + "#  AND CATEGORY ='" + category + "' AND PRONAME LIKE '%" + txtsearch + "%'";
                reports(query, "stockoutasof");
            }
            else
            {

                if (rdoToday.Checked)
                {
                    if(rdoStockIn.Checked)
                    {
                        query = "SELECT P.PROCODE AS [ProductCode],DATEVALUE(DATERECEIVED) AS [ReceivedDate], (PRONAME + ' ' + PRODESC) AS [Product],(PROPRICE) AS [Price],RECEIVEDQTY  AS [Quantity],(ROUND(RECEIVEDQTY * PROPRICE)) AS [TotalPrice] " +
                                " FROM tblStockIn AS S,  tblProductInfo AS P " +
                                " WHERE S.PROCODE=P.PROCODE AND DATEVALUE(DATERECEIVED) = DATEVALUE(NOW()) AND PRODESC LIKE '%" + txtsearch + "%' AND PRONAME LIKE '%" + txtsearch + "%'";
                         reports(query, "dailystockin");
                    }
                    else
                    {
                        query = "SELECT TRANSNUM, P.PROCODE AS [ProductCode],DATEVALUE(DATEOUT) AS [TransactionDate], (PRONAME + ' ' + PRODESC) AS [Product], (OUTQTY) AS [Quantity],PROPRICE as [Price], (ROUND(OUTQTY * PROPRICE)) AS [TotalPrice] " +
                                  " FROM tblStockOut AS S,  tblProductInfo AS P  WHERE S.PROCODE=P.PROCODE  AND  DATEVALUE(DATEOUT)  = DATEVALUE(NOW())  AND CATEGORY ='" + category + "' AND PRONAME LIKE '%" + txtsearch + "%'";
                        reports(query, "dailystockout");
                    }   
                          
                }
                else if (rdoWeekly.Checked)
                {
                    if (rdoStockIn.Checked)
                    {
                        query = "SELECT P.PROCODE AS [ProductCode],DATEVALUE(DATERECEIVED) AS [ReceivedDate], (PRONAME + ' ' + PRODESC) AS [Product],PROPRICE AS [Price],RECEIVEDQTY  AS [Quantity],(ROUND(RECEIVEDQTY * PROPRICE)) AS [TotalPrice] " +
                                  " FROM tblStockIn AS S,  tblProductInfo AS P " +
                                  " WHERE S.PROCODE=P.PROCODE AND MONTH(DATERECEIVED) = MONTH(NOW()) AND DATEPART('ww',DATEVALUE(DATERECEIVED)) =  DATEPART('ww',DATEVALUE(NOW())) AND PRODESC LIKE '%" + txtsearch + "%' AND PRONAME LIKE '%" + txtsearch + "%'";
                        reports(query, "weeklystockins");
                    }
                    else
                    {
                        query = "SELECT TRANSNUM, P.PROCODE AS [ProductCode],DATEVALUE(DATEOUT) AS [TransactionDate], (PRONAME + ' ' + PRODESC) AS [Product], OUTQTY AS [Quantity],PROPRICE as [Price], (ROUND(OUTQTY * PROPRICE)) AS [TotalPrice]  " +
                             " FROM tblStockOut AS S,  tblProductInfo AS P " +
                             " WHERE S.PROCODE=P.PROCODE  AND  MONTH(DATEOUT) = MONTH(NOW()) AND DATEPART('ww',DATEVALUE(DATEOUT)) =  DATEPART('ww',DATEVALUE(NOW()))  AND CATEGORY ='" + category + "' AND PRONAME LIKE '%" +  txtsearch + "%'";
                        reports(query, "weeklystockout");
                    }
                }
                else if (rdoMonthly.Checked)
                {
                    if (rdoStockIn.Checked)
                    {
                        query = "SELECT P.PROCODE AS [ProductCode],DATEVALUE(DATERECEIVED) AS [ReceivedDate], (PRONAME + ' ' + PRODESC) AS [Product],PROPRICE,RECEIVEDQTY  AS [Quantity],(ROUND(RECEIVEDQTY * PROPRICE)) AS [TotalPrice] " +
                               " FROM tblStockIn AS S,  tblProductInfo AS P " +
                               " WHERE S.PROCODE=P.PROCODE AND MONTH(DATERECEIVED) = MONTH(NOW()) AND PRODESC LIKE '%" + txtsearch + "%' AND PRONAME LIKE '%" + txtsearch + "%'";
                        reports(query, "monthlystockin");
                    }
                    else
                    {
                        query = "SELECT TRANSNUM, P.PROCODE AS [ProductCode],DATEVALUE(DATEOUT) AS [TransactionDate], (PRONAME + ' ' + PRODESC) AS [Product], (OUTQTY) AS [Quantity],PROPRICE as [Price], (ROUND(OUTQTY * PROPRICE)) AS [TotalPrice]  " +
                               " FROM tblStockOut AS S,  tblProductInfo AS P  " +
                               " WHERE S.PROCODE=P.PROCODE  AND MONTH(DATEOUT) = MONTH(NOW())  AND CATEGORY ='" + category + "' AND PRONAME LIKE '%" + txtsearch + "%'";
                        reports(query, "monthlystockout");
                    }
                }
                else
                {
                    if( rdoProduct.Checked)
                    {
                        query = "SELECT * " +
                               " FROM  tblProductInfo WHERE   CATEGORY ='" + category + "' AND PRONAME LIKE '%" + txtsearch + "%'"; 
                        reports(query, "listofproducts");
                    }
                    else if (rdoStockIn.Checked)
                    {
                        query = "SELECT * " +
                              " FROM tblStockIn AS S, tblProductInfo AS P WHERE S.PROCODE=P.PROCODE AND CATEGORY ='" + category +
                              "' AND PRONAME LIKE '%" + txtsearch + "%'"; 
                        reports(query, "stockinCMD");
                    }
                    else if (rdoStockOut.Checked)
                    {
                        query = "SELECT * FROM tblStockOut AS S,  tblProductInfo AS P WHERE S.PROCODE=P.PROCODE  AND CATEGORY ='" + category +
                            "' AND PRONAME LIKE '%" + txtsearch + "%'"; 
                        reports(query, "stockoutCMD");
                    }
                      
                       
                }

            } 
        }

        private void frmPrint_Load(object sender, EventArgs e)
        {

        }
    }
}
