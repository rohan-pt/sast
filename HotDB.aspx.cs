using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;
using System.Collections;
using System.Data;

namespace BCCBExamPortal
{
    public partial class HotDB : System.Web.UI.Page
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

                //MenuItem mnuTestChildx4 = new MenuItem();
                //mnuTestChildx4.NavigateUrl = "http://www.bccb.co.in/";
                //mnuTestChildx4.Target = "_blank";
                //mnuTestChildx4.Text = "BCCB Website";
                //mnuTestChildx4.Value = "4";

                //MenuItem mnuTestChildx5 = new MenuItem();
                //mnuTestChildx5.NavigateUrl = "http://guesthouse.bccb.co.in/";
                //mnuTestChildx5.Target = "_blank";
                //mnuTestChildx5.Text = "BCCB Guest House";
                //mnuTestChildx5.Value = "5";

                MenuItem mnuTestChildx6 = new MenuItem();
                mnuTestChildx6.NavigateUrl = "~/EmpDashBoard.aspx";
                mnuTestChildx6.Target = "_blank";
                mnuTestChildx6.Text = "E@learning";
                mnuTestChildx6.Value = "6";

                MenuItem mnuTestChildx7 = new MenuItem();
                mnuTestChildx6.NavigateUrl = "~/LCBD.aspx";
                mnuTestChildx6.Target = "_blank";
                mnuTestChildx6.Text = "LCBD DATA";
                mnuTestChildx6.Value = "7";

                mnuTest.ChildItems.Add(mnuTestChild2);
                mnuTest.ChildItems.Add(mnuTestChild3);
                //mnuTest.ChildItems.Add(mnuTestChildx4);
                //mnuTest.ChildItems.Add(mnuTestChildx5);
                mnuTest.ChildItems.Add(mnuTestChildx6);
                mnuTest.ChildItems.Add(mnuTestChildx7);
                //MenuItem ATM_C = new MenuItem();
                //ATM_C.Text = "ATM CASH TRANSACTION";
                //ATM_C.NavigateUrl = "~/AmlRating.aspx";
                //mnuTest.ChildItems.Add(ATM_C);
                //if (Models.CommonMethods.Isempallowed("select * from Employee_Access where Code='"+Session["id"].ToString()+"' and AML='1'"))
                //{
                //    mnuTest.ChildItems.Add(new MenuItem() {
                //        NavigateUrl = "~/AmlRating.aspx",
                //        Text = "View/Update AML Rating"
                //    });
                //}

                //MenuItem mnuBG = new MenuItem();
                //mnuBG.NavigateUrl = "~/BG.aspx";
                //mnuBG.Target = "_blank";
                //mnuBG.Text = "Bank Guarantee";
                //mnuTest.ChildItems.Add(mnuBG);

                //MenuItem mnuBGR = new MenuItem();
                //mnuBGR.NavigateUrl = "~/BG_reports.aspx";
                //mnuBGR.Target = "_blank";
                //mnuBGR.Text = "BG Reports";
                //mnuTest.ChildItems.Add(mnuBGR);

                //MenuItem mnuTD = new MenuItem();
                //mnuTD.NavigateUrl = "~/BGTD.aspx";
                //mnuTD.Target = "_blank";
                //mnuTD.Text = "BG TD Details";
                //mnuTest.ChildItems.Add(mnuTD);

                MenuItem mnuTest1 = new MenuItem();
                mnuTest1.NavigateUrl = "#";
                mnuTest1.Text = "Customer Service";
                mnuTest1.Value = "1";
                //MenuItem mnuTestChild = new MenuItem();
                //mnuTestChild.NavigateUrl = "~/SMS.aspx";
                //mnuTestChild.Text = "SMS Analysis";
                //mnuTestChild.Value = "2";
                //mnuTest1.ChildItems.Add(mnuTestChild);

                MenuItem mnuTestChildn = new MenuItem();
                mnuTestChildn.NavigateUrl = "~/CustomerService.aspx";
                mnuTestChildn.Text = "Email Complaints";
                mnuTestChildn.Value = "3";
                mnuTest1.ChildItems.Add(mnuTestChildn);


                MenuItem mnuTest2 = new MenuItem();
                mnuTest2.NavigateUrl = "#";
                mnuTest2.Text = "ATM";
                mnuTest2.Value = "1";
                //MenuItem mnuTestChild1 = new MenuItem();
                //mnuTestChild1.NavigateUrl = "~/MainDashBoard.aspx?Menu=0";
                //mnuTestChild1.Text = "POS Chargeback";
                //mnuTestChild1.Value = "3";
                //mnuTest2.ChildItems.Add(mnuTestChild1);

                //MenuItem mnuTestChild4 = new MenuItem();
                //mnuTestChild4.NavigateUrl = "~/ATMCashPosition.aspx?";
                //mnuTestChild4.Target = "_blank";
                //mnuTestChild4.Text = "ATM Cash Position";
                //mnuTestChild4.Value = "4";
                //mnuTest2.ChildItems.Add(mnuTestChild4);

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


                //MenuItem mnuAtmMaster = new MenuItem();
                //mnuAtmMaster.NavigateUrl = "~/ATMMASTER.aspx";
                //mnuAtmMaster.Target = "_blank";
                //mnuAtmMaster.Text = "ATM Details";
                //mnuTest2.ChildItems.Add(mnuAtmMaster);


                //MenuItem mnuTest4 = new MenuItem();
                //mnuTest4.NavigateUrl = "http://10.150.1.172:8083/DashBoard/frmBranchView.aspx";
                //mnuTest4.Target = "_blank";
                //mnuTest4.Text = "Circulars & Policies";
                //mnuTest4.Value = "5";

                //MenuItem mnuTest5 = new MenuItem();
                //mnuTest5.NavigateUrl = "~/UnlockIB.aspx";
                //mnuTest5.Target = "_blank";
                //mnuTest5.Text = "Internet Banking";
                //mnuTest5.Value = "6";


                //MenuItem mnuTest6 = new MenuItem();
                //mnuTest6.NavigateUrl = "#";
                //mnuTest6.Target = "_blank";
                //mnuTest6.Text = "PM Scheme";
                //mnuTest6.Value = "7";


                //MenuItem mnuTestChild7 = new MenuItem();
                //mnuTestChild7.NavigateUrl = "~/PendingAuthPMScheme.aspx?";
                //mnuTestChild7.Target = "_blank";
                //mnuTestChild7.Text = "Unuthorized PM Scheme";
                //mnuTestChild7.Value = "7";
                //mnuTest6.ChildItems.Add(mnuTestChild7);

                //MenuItem mnuTestChild8 = new MenuItem();
                //mnuTestChild8.NavigateUrl = "~/StopPMScheme.aspx?";
                //mnuTestChild8.Target = "_blank";
                //mnuTestChild8.Text = "Stop PM Scheme";
                //mnuTestChild8.Value = "8";
                //mnuTest6.ChildItems.Add(mnuTestChild8);

                //MenuItem mnuTestChild9 = new MenuItem();
                //mnuTestChild9.NavigateUrl = "~/SearchPMScheme.aspx?";
                //mnuTestChild9.Target = "_blank";
                //mnuTestChild9.Text = "Search PM Scheme";
                //mnuTestChild9.Value = "9";
                //mnuTest6.ChildItems.Add(mnuTestChild9);

                //MenuItem mnuTestChild10 = new MenuItem();
                //mnuTestChild10.NavigateUrl = "~/ShowPMScheme.aspx";
                //mnuTestChild10.Target = "_blank";
                //mnuTestChild10.Text = "Search PM Scheme More";
                //mnuTestChild10.Value = "10";
                //mnuTest6.ChildItems.Add(mnuTestChild10);



                //MenuItem mnuAbbcbsho = new MenuItem();
                //mnuAbbcbsho.NavigateUrl = "~/ABBCBSHO.aspx";
                //mnuAbbcbsho.Target = "_blank";
                //mnuAbbcbsho.Text = "ABBCBSHO";
                //mnuTest7.ChildItems.Add(mnuAbbcbsho);
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
                //Menu1.Items.Add(mnuTest4);
                //Menu1.Items.Add(mnuTest5);
                //Menu1.Items.Add(mnuTest6);

                string loggedInEmpCode = Session["id"].ToString();

                //Delete PM Scheme option only available for Rodney and Dorlisa
                //if (new string[] { "08430", "06860", "06160" }.Contains(loggedInEmpCode))
                //{
                //    MenuItem mnuTestChild11 = new MenuItem();
                //    mnuTestChild11.NavigateUrl = "~/DeletePMScheme.aspx";
                //    mnuTestChild11.Target = "_blank";
                //    mnuTestChild11.Text = "Delete PM Scheme";
                //    mnuTestChild11.Value = "11";
                //    mnuTest6.ChildItems.Add(mnuTestChild11);
                //}
                MenuItem mnuTest7 = new MenuItem();
                mnuTest7.NavigateUrl = "#";
                mnuTest7.Target = "_blank";
                mnuTest7.Text = "Other";
                mnuTest7.Value = "13";
                //if (Application["HeadOfficeEmpCodes"] != null)
                //{
                //    string[] empCodes = (string[])Application["HeadOfficeEmpCodes"];
                //    if (empCodes.Contains(loggedInEmpCode))
                //    {

                //        mnuTest7.ChildItems.Add(new MenuItem
                //        {
                //            NavigateUrl = "~/ECS.aspx",
                //            Target = "_blank",
                //            Text = "Download ECS Report",
                //            Value = "12"
                //        });
                //        //mnuTest7.ChildItems.Add(new MenuItem {
                //        //    NavigateUrl = "~/TotalDepositors.aspx",
                //        //    Target = "_blank",
                //        //    Text = "Total Depositors",
                //        //});
                //    }
                //}
                // string[] directorLoanVisibleCodes = new string[] { "11059", "11022", "08560","08420", "11067", "07730", "07070", "11007", "08360", "07720", "08313", "07060", "08346", "07610", "08322", "08314", "03240", "08331", "07640", "08391", "03308", "07200", "07860", "07590", "06120", "08337", "08370", "07660", "07300", "07470", "07850", "07580", "07190", "03250", "08352", "07120", "08321", "03304", "07390", "08341", "08345", "08020", "06050", "08406", "08315", "07220", "03312", "08409", "08180", "06280", "07680", "06290", "08325", "08374", "08395", "08327", "07430", "08366", "06780", "06060", "08372", "08376", "06320", "06730", "07670", "08260", "08347", "07810", "08353", "05740", "07770", "07160", "07790", "06770", "08531", "08400", "08452" };
                //if (Models.CommonMethods.Isempallowed("select * from Employee_Access where Code='" + Session["id"].ToString() + "' and DM='1'"))
                //    mnuTest7.ChildItems.Add(new MenuItem
                //    {
                //        NavigateUrl = "~/DirectorAccounts.aspx",
                //        Target = "_blank",
                //        Text = "Director Accounts"
                //    });

                //mnuTest7.ChildItems.Add(new MenuItem() {
                //    NavigateUrl = "~/ABBCBSHO.aspx",
                //    Target = "_blank",
                //    Text = "ABBCBSHO"
                //});
                // string[] allowedEmpCodes = new string[] { "11059", "02023", "02022", "06180", "06080", "08430","08560", "08420" };
                //if (Models.CommonMethods.Isempallowed("select * from Employee_Access where Code='" + Session["id"].ToString() + "' and Prod='1'"))
                //{
                //    mnuTest7.ChildItems.Add(new MenuItem()
                //    {
                //        NavigateUrl = "~/ProductUnauthorized.aspx",
                //        Target = "_blank",
                //        Text = "Product Unauthorized",
                //    });
                //}
                //if (Models.CommonMethods.Isempallowed("select * from Employee_Access where Code='" + Session["id"].ToString() + "' and Swift='1'"))
                //{
                //    mnuTest7.ChildItems.Add(new MenuItem()
                //    {
                //        NavigateUrl = "~/Swiftlimit.aspx",
                //        Target = "_blank",
                //        Text = "Swift Limit",
                //    });
                //}

                MenuItem mnuTestChild12 = new MenuItem();
                mnuTestChild12.NavigateUrl = "~/frmViewSigns.aspx";
                mnuTestChild12.Target = "_blank";
                mnuTestChild12.Text = "Sign Verify";
                mnuTestChild12.Value = "12";
                mnuTest7.ChildItems.Add(mnuTestChild12);

                MenuItem mnuTestChild13 = new MenuItem();
                mnuTestChild13.NavigateUrl = "~/OFSS_Knowledge_Base.aspx";
                mnuTestChild13.Target = "_blank";
                mnuTestChild13.Text = "OFSS Tables";
                mnuTestChild13.Value = "13";
                mnuTest7.ChildItems.Add(mnuTestChild13);

                MenuItem mnuTestChild14 = new MenuItem();
                mnuTestChild14.NavigateUrl = "~/Clearing_Reports.aspx";
                mnuTestChild14.Target = "_blank";
                mnuTestChild14.Text = "Clearing Reports";
                mnuTestChild14.Value = "14";
                mnuTest7.ChildItems.Add(mnuTestChild14);

                if (mnuTest7.ChildItems.Count > 0)
                    Menu1.Items.Add(mnuTest7);
                Menu1.Items.Add(mnuTest3);
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
                con.Open();
                try
                {
                    // sessionlbl.Text = Session["Id"].ToString();
                    string com = "Select * from Locationtbl order by Location asc";
                    SqlDataAdapter adpt = new SqlDataAdapter(com, con);
                    DataTable dt = new DataTable();
                    adpt.Fill(dt);
                    ddlbranch.Items.Clear();
                    ddlbranch.DataSource = dt;
                    ddlbranch.DataBind();
                    ddlbranch.DataTextField = "Location";
                    ddlbranch.DataValueField = "Loc_Id";
                    ddlbranch.DataBind();
                    ddlbranch.Items.Insert(0, new ListItem("Select Location", "0"));
                    //dllocation.Items.FindByValue("0").Selected = true;

                }
                catch (Exception el)
                {
                    //lblmsg.Text = "Problem In Network, Unable to update";
                }
                finally
                {
                    con.Close();
                }

                ddlaactype.Items.Clear();
                ddlaactype.Items.Insert(0, new ListItem("Select Account Type", "0"));
                ddlaactype.Items.Insert(0, new ListItem("SB", "1"));
                ddlaactype.Items.Insert(0, new ListItem("CD", "2"));
                ddlaactype.Items.Insert(0, new ListItem("NRI", "3"));
                ddlaactype.Items.Insert(0, new ListItem("CC", "4"));
                ddlaactype.Items.Insert(0, new ListItem("ODCC", "5"));
                ddlaactype.Items.FindByText("Select Account Type").Selected = true;
            }

        }

        protected void btnacc_Click(object sender, EventArgs e)
        {
            btnacc.CssClass = "glow";//
            btnmob.CssClass = "btnfuse";
            btncard.CssClass = "btnfuse";
            divacc.Visible = true;
            divcard.Visible = false;
            divmob.Visible = false;
            btnBlock.Visible = false;
            btndetail.Visible = true;
            Gridtbl.DataSource = null;
            Gridtbl.DataBind();
        }

        protected void btnmob_Click(object sender, EventArgs e)
        {
            btnmob.CssClass = "glow";
            btncard.CssClass = "btnfuse";
            btnacc.CssClass = "btnfuse";
            divacc.Visible = false;
            divcard.Visible = false;
            divmob.Visible = true;
            btnBlock.Visible = false;
            btndetail.Visible = true;
            Gridtbl.DataSource = null;
            Gridtbl.DataBind();
        }

        protected void btncard_Click(object sender, EventArgs e)
        {
            btncard.CssClass = "glow";
            btnmob.CssClass = "btnfuse";
            btnacc.CssClass = "btnfuse";
            divacc.Visible = false;
            divcard.Visible = true;
            divmob.Visible = false;
            btnBlock.Visible = false;
            btndetail.Visible = true;
            Gridtbl.DataSource = null;
            Gridtbl.DataBind();
        }

        protected void Run_Code()
        {
           
            if (btnacc.CssClass == "glow")
            {
                try
                {
                    if (txtaccno.Value.ToString() != "" && ddlbranch.SelectedItem.Text.ToString() != "Select Location" && ddlaactype.SelectedItem.Text.ToString() != "Select Account Type")
                    {
                       
                        Gridtbl.DataSource = null;
                        string completed = "SELECT DISTINCT UPPER(LTRIM(RTRIM(C1.CardId))) AS CardId, " +
                                                    "CASE WHEN C1.Status = 1 THEN 'ACTIVE' WHEN C1.Status = 2 THEN 'HOT-MARKED' " +
                                                    "WHEN C1.Status = 3 THEN 'CLOSED' WHEN C1.Status = 4 THEN 'EXPIRED' ELSE CAST(C1.Status AS VARCHAR) END AS CardStatus, " +
                                                    "A.LBrCode, LTRIM(RTRIM(SUBSTRING(A.PrdAcctId,0,8)))+'/'+CONVERT(VARCHAR(8),LTRIM(RTRIM(CONVERT(INT,SUBSTRING(A.PrdAcctId,9,16))))) as AcctNum, " +
                                                    "A.CustNo, UPPER(LTRIM(RTRIM(A.LongName))) as LongName FROM D390060 C1 JOIN D390070 C2 ON C1.CardId = C2.CardNo " +
                                                    "JOIN D009022 A ON C2.Brcode = A.LBrCode AND CAST(LTRIM(RTRIM(C2.BrPrdCd)) AS VARCHAR)+'/'+CAST(LTRIM(RTRIM(C2.BrAccNo)) AS VARCHAR) " +
                                                    "= LTRIM(RTRIM(SUBSTRING(A.PrdAcctId,0,8)))+'/'+CONVERT(VARCHAR(8),LTRIM(RTRIM(CONVERT(INT,SUBSTRING(A.PrdAcctId,9,16))))) " +
                                                    "WHERE SUBSTRING(A.PrdAcctId,1,8) IN (SELECT DISTINCT PrdCd FROM D009021 WHERE ModuleType <20 AND CustInt = 'C') " +
                                                    "AND A.LBrCode = " + ddlbranch.SelectedItem.Value.ToString() + " AND LTRIM(RTRIM(SUBSTRING(A.PrdAcctId,0,8)))+'/'+CONVERT(VARCHAR(8)," +
                                                    "LTRIM(RTRIM(CONVERT(INT,SUBSTRING(A.PrdAcctId,9,16))))) = '" + ddlaactype.SelectedItem.Text.ToString() + "/" + txtaccno.Value.ToString() + "'";

                        SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection2"].ConnectionString);
                        cnn.Open();
                        SqlCommand sqlCommand;
                        sqlCommand = new SqlCommand(completed, cnn);
                        sqlCommand.CommandTimeout = 900;

                        SqlDataReader rr = sqlCommand.ExecuteReader();

                        Gridtbl.DataSource = rr;
                        Gridtbl.DataBind();
                        if (Gridtbl.Rows.Count > 0)
                        {
                            btnBlock.Visible = false;
                        }
                        else
                        {
                            btnBlock.Visible = false;
                            lblerror.Text = "No data found.";
                        }
                        cnn.Close();
                    }
                    else
                    {
                        lblerror.Text = "Please provide Input";
                    }

                }
                catch (Exception rr)
                {
                    lblerror.Text = "Input related or Network related error occured";
                }

            }
            else if (btncard.CssClass == "glow")
            {
                try
                {
                    if (txtcard.Value != "")
                    {
                       
                        Gridtbl.DataSource = null;
                        string completed = "SELECT UPPER(LTRIM(RTRIM(C1.CardId))) AS CardId, " +
                                "CASE WHEN C1.Status = 1 THEN 'ACTIVE' WHEN C1.Status = 2 THEN 'HOT-MARKED' " +
                                "WHEN C1.Status = 3 THEN 'CLOSED' WHEN C1.Status = 4 THEN 'EXPIRED' " +
                                "ELSE CAST(C1.Status AS VARCHAR) END AS CardStatus, A.LBrCode, " +
                                "LTRIM(RTRIM(SUBSTRING(A.PrdAcctId,0,8)))+'/'+CONVERT(VARCHAR(8),LTRIM(RTRIM(CONVERT(INT,SUBSTRING(A.PrdAcctId,9,16))))) as AcctNum, A.LongName " +
                                "FROM D390060 C1 JOIN D390070 C2 ON C1.CardId = C2.CardNo JOIN D009022 A ON C2.Brcode = A.LBrCode " +
                                "AND CAST(LTRIM(RTRIM(C2.BrPrdCd)) AS VARCHAR)+'/'+CAST(LTRIM(RTRIM(C2.BrAccNo)) AS VARCHAR) " +
                                "= LTRIM(RTRIM(SUBSTRING(A.PrdAcctId,0,8)))+'/'+CONVERT(VARCHAR(8)," +
                                "LTRIM(RTRIM(CONVERT(INT,SUBSTRING(A.PrdAcctId,9,16))))) " +
                                "WHERE A.LBrCode BETWEEN 3 AND 123 AND C1.CardId IN ('" + txtcard.Value + "')";

                        SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection2"].ConnectionString);
                        cnn.Open();
                        SqlCommand sqlCommand;
                        sqlCommand = new SqlCommand(completed, cnn);
                        sqlCommand.CommandTimeout = 900;
                        SqlDataReader rr = sqlCommand.ExecuteReader();
                        Gridtbl.DataSource = rr;
                        Gridtbl.DataBind();
                        if (Gridtbl.Rows.Count > 0)
                        {
                            btnBlock.Visible = false;
                        }
                        else
                        {
                            btnBlock.Visible = false;
                            lblerror.Text = "No data found.";
                        }
                        cnn.Close();
                    }
                    else
                    {
                        lblerror.Text = "Please provide Input";
                    }
            }
                catch (Exception rr)
                {
                    lblerror.Text = "Input related or Network related error occured";
                }

            }
            else if (btnmob.CssClass == "glow")
            {
                try
                {
                    if (txtmob.Value != "")
                    {
                      
                        Gridtbl.DataSource = null;
                        string completed = "SELECT DISTINCT UPPER(LTRIM(RTRIM(C1.CardId))) AS CardId, CASE WHEN C1.Status = 1 THEN 'ACTIVE' " +
                                    "WHEN C1.Status = 2 THEN 'HOT-MARKED' WHEN C1.Status = 3 THEN 'CLOSED' WHEN C1.Status = 4 THEN 'EXPIRED' " +
                                    "ELSE CAST(C1.Status AS VARCHAR) END AS CardStatus, A.LBrCode, " +
                                    "LTRIM(RTRIM(SUBSTRING(A.PrdAcctId,0,8)))+'/'+CONVERT(VARCHAR(8),LTRIM(RTRIM(CONVERT(INT,SUBSTRING(A.PrdAcctId,9,16))))) as AcctNum, " +
                                    "A.CustNo, UPPER(LTRIM(RTRIM(A.LongName))) as LongName, S.MobileNo FROM D390060 C1 JOIN D390070 C2 ON C1.CardId = C2.CardNo " +
                                    "JOIN D009022 A ON C2.Brcode = A.LBrCode AND CAST(LTRIM(RTRIM(C2.BrPrdCd)) AS VARCHAR)+'/'+CAST(LTRIM(RTRIM(C2.BrAccNo)) AS VARCHAR) " +
                                    "= LTRIM(RTRIM(SUBSTRING(A.PrdAcctId,0,8)))+'/'+CONVERT(VARCHAR(8) ,LTRIM(RTRIM(CONVERT(INT,SUBSTRING(A.PrdAcctId,9,16))))) " +
                                    "LEFT JOIN D350078 S ON CAST(C2.CustNo AS VARCHAR) = CAST(S.CustNo AS VARCHAR) " +
                                    "WHERE SUBSTRING(A.PrdAcctId,1,8) IN (SELECT DISTINCT PrdCd FROM D009021 WHERE ModuleType <20 AND CustInt = 'C') " +
                                    "AND S.MobileNo LIKE '%" + txtmob.Value + "%' ";

                        SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection2"].ConnectionString);
                        cnn.Open();
                        SqlCommand sqlCommand;
                        sqlCommand = new SqlCommand(completed, cnn);
                        sqlCommand.CommandTimeout = 900;
                        SqlDataReader rr = sqlCommand.ExecuteReader();
                        Gridtbl.DataSource = rr;
                        Gridtbl.DataBind();
                        if (Gridtbl.Rows.Count>0)
                        {
                            btnBlock.Visible = false;                         
                        }
                        else
                        {
                            btnBlock.Visible = false;
                            lblerror.Text = "No data found.";
                        }

                        cnn.Close();
                    }
                    else
                    {
                        lblerror.Text = "Please provide Input";
                    }
            }
                catch (Exception rr)
            {
                lblerror.Text = "Input related or Network related error occured";
            }
        }

        }

        protected void btnBlock_Click(object sender, EventArgs e)
        {
            string cardnumber = "";
            int i = 1;
            foreach (GridViewRow gvrow in Gridtbl.Rows)
            {
                CheckBox chk = (CheckBox)gvrow.FindControl("CheckBox1");
                if (chk != null & chk.Checked)
                {
                    if (i == 1)
                    {
                        cardnumber = "'" + gvrow.Cells[2].Text + "'";
                    }
                    else
                    {
                        cardnumber = cardnumber + ",'" + gvrow.Cells[2].Text + "'";
                    }
                    i = i + 1;
                }
            }
            //string final = cardnumber;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection2"].ConnectionString);
            con.Open();
            try
            {
                int x = Convert.ToInt32(Session["id"].ToString());
                SqlCommand cmd1 = new SqlCommand("UPDATE D390060 SET Status = 2, DbtrLupdMk=" + x + ", DbtrLupdCk = " + x + ", DbtrLupdMd=CONVERT(date, getdate(), 23) WHERE CardId IN ( " + cardnumber + " )", con);

                cmd1.ExecuteNonQuery();
                cmd1.Dispose();
               
                Run_Code();
                lblerror.Text = "Card / Cards are HOT-MARKED Correctly";

            }
            catch (Exception ep)
            {
                lblerror.Text = "Problem In Network. Unable to Update Stuff right now.";
            }
            finally
            {
                con.Close();
            }


           
        }




        protected void btndetail_Click(object sender, EventArgs e)
        {
            Run_Code();
        }
    }
}