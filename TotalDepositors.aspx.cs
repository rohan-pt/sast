using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BCCBExamPortal
{
    public partial class TotalDepositors : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                sessionlbl.Text = Session["id"].ToString();
            }
            catch (Exception ep)
            {
                Response.Redirect("LogIn.aspx");
            }
            string ajaxCall = Request.Form["AjaxCall"];
            if (ajaxCall == "TotalDepositors")
                Response.Write("####" + GetTotalDepositors() + "####");
        }
        protected int GetTotalDepositors()
        {
            string date = Request.Form["Date"], query = "SELECT COUNT(DISTINCT CustNo) " +
            "FROM( " +
            "SELECT CustNo " +
            "FROM D009022 " +
            "WHERE DateOpen <= '" + date + "' AND AcctStat <> 3 AND LBrCode BETWEEN 2 AND 124 AND RTRIM(SUBSTRING(PrdAcctId, 1, 8)) IN(SELECT PrdCd FROM D009021 WHERE ModuleType IN(11, 12) AND LBrCode = 4) " +
            "UNION " +
            "SELECT b.CustNo " +
            "FROM D020004 a " +
            "INNER JOIN D009022 b ON a.LBrCode = b.LBrCode AND SUBSTRING(a.PrdAcctId, 1, 24) = SUBSTRING(b.PrdAcctId, 1, 24) " +
            "WHERE DateOpen <= '" + date + "' AND b.LBrCode BETWEEN 2 AND 124 AND a.ReceiptStatus = 51" +
            ") As Tbl; ";
            System.Data.DataTable dt = Models.SqlManager.ExecuteSelect(System.Configuration.ConfigurationManager.ConnectionStrings["dbconnection2"].ConnectionString, query);
            return Convert.ToInt32(dt.Rows[0][0]);
        }
    }
}