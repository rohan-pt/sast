using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace BCCBExamPortal
{
    public partial class ShowPMScheme : System.Web.UI.Page
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
            if (!IsPostBack)
            {
                MenuItem mnuTest = new MenuItem();
                mnuTest.NavigateUrl = "~/Home.aspx";
                mnuTest.Text = "Home";
                mnuTest.Value = "1";


                MenuItem mnuTestChild2 = new MenuItem();
                mnuTestChild2.NavigateUrl = "~/ChangePassword.aspx";
                mnuTestChild2.Text = "Change Password";
                mnuTestChild2.Value = "4";
                MenuItem mnuTestChild3 = new MenuItem();
                mnuTestChild3.NavigateUrl = "~/HelpPage.aspx";
                mnuTestChild3.Text = "Help";
                mnuTestChild3.Value = "3";

                MenuItem mnuTestChildx4 = new MenuItem();
                mnuTestChildx4.NavigateUrl = "http://www.bccb.co.in/";
                mnuTestChildx4.Target = "_blank";
                mnuTestChildx4.Text = "BCCB Website";
                mnuTestChildx4.Value = "4";

                MenuItem mnuTestChildx5 = new MenuItem();
                mnuTestChildx5.NavigateUrl = "http://guesthouse.bccb.co.in/";
                mnuTestChildx5.Target = "_blank";
                mnuTestChildx5.Text = "BCCB Guest House";
                mnuTestChildx5.Value = "5";

                MenuItem mnuTestChildx6 = new MenuItem();
                mnuTestChildx6.NavigateUrl = "~/EmpDashBoard.aspx";
                mnuTestChildx6.Target = "_blank";
                mnuTestChildx6.Text = "E@learning";
                mnuTestChildx6.Value = "6";

                mnuTest.ChildItems.Add(mnuTestChild2);
                mnuTest.ChildItems.Add(mnuTestChild3);
                mnuTest.ChildItems.Add(mnuTestChildx4);
                mnuTest.ChildItems.Add(mnuTestChildx5);
                mnuTest.ChildItems.Add(mnuTestChildx6);

                MenuItem mnuTest1 = new MenuItem();
                mnuTest1.NavigateUrl = "#";
                mnuTest1.Text = "SMS";
                mnuTest1.Value = "1";
                MenuItem mnuTestChild = new MenuItem();
                mnuTestChild.NavigateUrl = "~/SMS.aspx";
                mnuTestChild.Text = "SMS Analysis";
                mnuTestChild.Value = "2";
                mnuTest1.ChildItems.Add(mnuTestChild);


                MenuItem mnuTest2 = new MenuItem();
                mnuTest2.NavigateUrl = "#";
                mnuTest2.Text = "ATM";
                mnuTest2.Value = "1";
                MenuItem mnuTestChild1 = new MenuItem();
                mnuTestChild1.NavigateUrl = "~/MainDashBoard.aspx?Menu=0";
                mnuTestChild1.Text = "POS Chargeback";
                mnuTestChild1.Value = "3";
                mnuTest2.ChildItems.Add(mnuTestChild1);

                MenuItem mnuTestChild4 = new MenuItem();
                mnuTestChild4.NavigateUrl = "~/ATMCashPosition.aspx?";
                mnuTestChild4.Target = "_blank";
                mnuTestChild4.Text = "ATM Cash Position";
                mnuTestChild4.Value = "4";
                mnuTest2.ChildItems.Add(mnuTestChild4);

                MenuItem mnuTestChild5 = new MenuItem();
                mnuTestChild5.NavigateUrl = "~/DebitCardNotWorking.aspx?";
                mnuTestChild5.Target = "_blank";
                mnuTestChild5.Text = "Debit Card Not Working";
                mnuTestChild5.Value = "5";
                mnuTest2.ChildItems.Add(mnuTestChild5);

                MenuItem mnuTestChild6 = new MenuItem();
                mnuTestChild6.NavigateUrl = "~/HotDB.aspx?";
                mnuTestChild6.Target = "_blank";
                mnuTestChild6.Text = "Hot Mark Card";
                mnuTestChild6.Value = "6";
                mnuTest2.ChildItems.Add(mnuTestChild6);

                MenuItem mnuTest4 = new MenuItem();
                mnuTest4.NavigateUrl = "http://10.150.1.172:8083/DashBoard/frmBranchView.aspx";
                mnuTest4.Target = "_blank";
                mnuTest4.Text = "Circulars & Policies";
                mnuTest4.Value = "5";

                MenuItem mnuTest5 = new MenuItem();
                mnuTest5.NavigateUrl = "~/UnlockIB.aspx";
                mnuTest5.Target = "_blank";
                mnuTest5.Text = "Internet Banking";
                mnuTest5.Value = "6";


                MenuItem mnuTest6 = new MenuItem();
                mnuTest6.NavigateUrl = "#";
                mnuTest6.Target = "_blank";
                mnuTest6.Text = "PM Scheme";
                mnuTest6.Value = "7";


                MenuItem mnuTestChild7 = new MenuItem();
                mnuTestChild7.NavigateUrl = "~/PendingAuthPMScheme.aspx?";
                mnuTestChild7.Target = "_blank";
                mnuTestChild7.Text = "Unuthorized PM Scheme";
                mnuTestChild7.Value = "7";
                mnuTest6.ChildItems.Add(mnuTestChild7);

                MenuItem mnuTestChild8 = new MenuItem();
                mnuTestChild8.NavigateUrl = "~/StopPMScheme.aspx?";
                mnuTestChild8.Target = "_blank";
                mnuTestChild8.Text = "Stop PM Scheme";
                mnuTestChild8.Value = "8";
                mnuTest6.ChildItems.Add(mnuTestChild8);

                MenuItem mnuTestChild9 = new MenuItem();
                mnuTestChild9.NavigateUrl = "~/SearchPMScheme.aspx?";
                mnuTestChild9.Target = "_blank";
                mnuTestChild9.Text = "Search PM Scheme";
                mnuTestChild9.Value = "9";
                mnuTest6.ChildItems.Add(mnuTestChild9);

                MenuItem mnuTestChild10 = new MenuItem();
                mnuTestChild10.NavigateUrl = "~/ShowPMScheme.aspx";
                mnuTestChild10.Target = "_blank";
                mnuTestChild10.Text = "Search PM Scheme More";
                mnuTestChild10.Value = "10";
                mnuTest6.ChildItems.Add(mnuTestChild10);

                //MenuItem mnuTestChild11 = new MenuItem();
                //mnuTestChild11.NavigateUrl = "~/DeletePMScheme.aspx";
                //mnuTestChild11.Target = "_blank";
                //mnuTestChild11.Text = "Delete PM Scheme";
                //mnuTestChild11.Value = "11";
                //mnuTest6.ChildItems.Add(mnuTestChild11);
                //From here ECS change 
                //MenuItem mnuTest7 = new MenuItem();
                //mnuTest7.NavigateUrl = "~/#";
                //mnuTest7.Target = "_blank";
                //mnuTest7.Text = "ECS";
                //mnuTest7.Value = "7";

                //MenuItem mnuTestChild10 = new MenuItem();
                //mnuTestChild10.NavigateUrl = "~/Ecs_menu.aspx?";
                //mnuTestChild10.Target = "_blank";
                //mnuTestChild10.Text = "Get ECS Status";
                //mnuTestChild10.Value = "9";
                //mnuTest7.ChildItems.Add(mnuTestChild10);

                //MenuItem mnuTestChild11 = new MenuItem();
                //mnuTestChild11.NavigateUrl = "~/upadte_ecs.aspx?";
                //mnuTestChild11.Target = "_blank";
                //mnuTestChild11.Text = "Update ECS";
                //mnuTestChild11.Value = "9";
                //mnuTest7.ChildItems.Add(mnuTestChild11);

                //--------------------------------------------------------------------------------------------------------

                MenuItem mnuTest3 = new MenuItem();
                mnuTest3.NavigateUrl = "~/LogOut.aspx";
                mnuTest3.Text = "Log Out";
                mnuTest3.Value = "1";

                Menu1.Items.Add(mnuTest);
                Menu1.Items.Add(mnuTest2);
                Menu1.Items.Add(mnuTest1);
                Menu1.Items.Add(mnuTest4);
                Menu1.Items.Add(mnuTest5);
                Menu1.Items.Add(mnuTest6);
                //Menu1.Items.Add(new MenuItem
                //{
                //    NavigateUrl = "~/ECS.aspx",
                //    Target = "_blank",
                //    Text = "ECS",
                //    Value = "12"
                //});
                //Menu1.Items.Add(mnuTest7);
                Menu1.Items.Add(mnuTest3);

                DataTable dt_branches = Models.SqlManager.ExecuteSelect(Models.CommonMethods.ConnectionStrings["BCCBREPORT"], "SELECT PBrCode, Name FROM D001003 WHERE PBrCode > 2 ORDER BY PBrCode");
                string branchName;
                foreach (DataRow row in dt_branches.Rows)
                {
                    branchName = row["Name"].ToString();
                    branchName = branchName[0].ToString().ToUpper() + branchName.Substring(1);
                    ddlbranch.Items.Add(new ListItem(branchName, row["PBrCode"].ToString()));
                }

                string[] acctTypes = new string[] { "SB", "CD", "NRI", "CC", "ODCC" };
                int l = acctTypes.Length;
                for (int i = 0; i < l; i++)
                    ddlaactype.Items.Add(new ListItem(acctTypes[i], acctTypes[i]));
            }
            string ajaxType = Request.Form["AjaxType"];
            if (ajaxType != null)
            {
                string jsObj = "[]";
                switch (ajaxType)
                {
                    case "GetRecord":
                        jsObj = GetRecord();
                        break;
                }
                Response.Write("###" + jsObj + "###");
            }
        }
        private string GetRecord()
        {
            string query, type = Request.Form["Type"];
            if (Request.Form["Scheme"] == "PMSBY")
                query = "SELECT InsuredName, ApplId, NomineeName, ProductID, RecStatus, LBrCode, AcctId, CustNo FROM D045025";
            else
                query = "SELECT AcctHolderName, ApplId, NomineeName, ProductID, RecStatus, LBrCode, AcctId, CustNo FROM D045046"; ;
            if (type == "CustNo")
                query += " WHERE CustNo = '" + Request["CustNo"] + "'";
            else if (type == "AcctNo")
                query += " WHERE LBrCode = " + Request["BranchCode"] + " AND AcctId LIKE '" + Request.Form["AcctType"] + "%" + Request.Form["AcctNo"] + "%'";
            else
                query += " WHERE LBrCode = " + Request["BranchCode"] + " And ApplId = '" + Request.Form["AppId"] + "'";
            DataTable dt = Models.SqlManager.ExecuteSelect(Models.CommonMethods.ConnectionStrings["BCCBREPORT"], query);
            return Models.CommonMethods.ConvertDatatableToJavascriptObject(dt);
        }
    }
}