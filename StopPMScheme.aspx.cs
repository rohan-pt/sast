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
    public partial class StopPMScheme : System.Web.UI.Page
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

        protected void btn12_Click(object sender, EventArgs e)
        {
            txtaccno1.Disabled = false;
            ddlaactype.Enabled = true;
            ddlbranch.Enabled = true;
            btn12.CssClass = "btnhard";
            btn330.CssClass = "btnsoft";
            Gridtbl.DataSource = null;
            Gridtbl.DataBind();
            lblmsg.Text =  "";
            btnStop.Visible = false;
        }

        protected void btn330_Click(object sender, EventArgs e)
        {
            txtaccno1.Disabled = false;
            ddlaactype.Enabled = true;
            ddlbranch.Enabled = true;
            btn12.CssClass = "btnsoft";
            btn330.CssClass = "btnhard";
            Gridtbl.DataSource = null;
            Gridtbl.DataBind();
            lblmsg.Text = "";
            btnStop.Visible = false;
        }

        protected void btnfind_Click(object sender, EventArgs e)
        {
            Run_Code();
        }

        protected void btnStop_Click(object sender, EventArgs e)
        {
            string cardnumber = "", branchCd = "";
            int i = 1;
            if (btn12.CssClass == "btnhard")
                branchCd = "BrnCode";
            else if (btn330.CssClass == "btnhard")
                branchCd = "BranchCd";
            foreach (GridViewRow gvrow in Gridtbl.Rows)
            {
                CheckBox chk = (CheckBox)gvrow.FindControl("CheckBox1");
                if (chk != null & chk.Checked)
                {
                    cardnumber += "(CustNo = '" + gvrow.Cells[1].Text + "' AND " + branchCd + "='" + gvrow.Cells[2].Text + "' AND ApplId='" + gvrow.Cells[5].Text + "') OR ";
                    //if (i == 1)
                    //{
                    //    cardnumber = "'" + gvrow.Cells[2].Text + "'";
                    //}
                    //else
                    //{
                    //    cardnumber = cardnumber + ",'" + gvrow.Cells[2].Text + "'";
                    //}
                    //i = i + 1;
                }
            }
            if (cardnumber.EndsWith(" OR "))
                cardnumber = cardnumber.Substring(0, cardnumber.Length - 4);
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection2"].ConnectionString);
            con.Open();
            try
            {
                int x= Convert.ToInt32(Session["id"].ToString());
                string completed = "";
                if (btn12.CssClass == "btnhard")
                {
                    completed = "UPDATE D045025 SET RecStatus=3, DbtrLupdMk=" + x + ", DbtrLupdCk=" + x + ", " +
                            "DbtrLupdMd=CONVERT(date, getdate(), 23), DbtrLupdCd=CONVERT(date, getdate(), 23) " +
                            "WHERE " + cardnumber;//" BrnCode = " + ddlbranch.SelectedItem.Value.ToString() + " AND ApplId IN ( " + cardnumber + ")";




                }
                else if (btn330.CssClass == "btnhard")
                {
                    completed = "UPDATE D045046 SET RecStatus = 3, DbtrLupdMk = " + x + ", DbtrLupdCk = " + x + ", " +
                          "DbtrLupdMd=CONVERT(date, getdate(), 23), DbtrLupdCd=CONVERT(date, getdate(), 23) " +
                          "WHERE " + cardnumber;//BranchCd = " + ddlbranch.SelectedItem.Value.ToString() + " AND ApplId IN ( " + cardnumber + ")";

                }

                SqlCommand cmd1 = new SqlCommand(completed, con);

                cmd1.ExecuteNonQuery();
                cmd1.Dispose();

                Run_Code();
                lblmsg.Text = "Executed Successfully";
            }
            catch (Exception ep)
            {
                lblmsg.Text = "Problem In Network. Unable to Update Stuff right now.";
            }
            finally
            {
                con.Close();
            }

        }


        protected void Run_Code()
        {
            SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection2"].ConnectionString);
            lbl_rowCount.Visible = false;
            if (btn12.CssClass == "btnsoft" && btn330.CssClass == "btnsoft")
            {
                lblmsg.Text = "Please select Type Of Scheme at the Top";
            }
            else
            {
                try
                {
                    string completed = "";
                    Gridtbl.DataSource = null;
                    Gridtbl.DataBind();
                    if (txt_custNo.Text.Trim() != "")
                    {
                        string custNo = String.Join(",", txt_custNo.Text.Trim().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries));
                        completed = "WHERE CustNo IN (" + custNo + ")";
                        lbl_rowCount.Visible = true;
                    }
                    else if (txtaccno1.Value.ToString() != "" && ddlbranch.SelectedItem.Text.ToString() != "Select Location" && ddlaactype.SelectedItem.Text.ToString() != "Select Account Type")
                    {
                        completed = "WHERE " + (btn12.CssClass == "btnhard" ? "BrnCode": "BranchCd") + " = " + ddlbranch.SelectedItem.Value.ToString() + " AND LTRIM(RTRIM(SUBSTRING(AcctId,0,8)))+'/'+" +
                       "CONVERT(VARCHAR(8),LTRIM(RTRIM(CONVERT(INT,SUBSTRING(AcctId,9,16))))) = '" + ddlaactype.SelectedItem.Text.ToString() + "/" + txtaccno1.Value + "'";
                    }
                    else
                    {
                        lblmsg.Text = "Please provide Input";
                    }
                    if (completed != "")
                    {
                        if (btn12.CssClass == "btnhard")
                            completed = "SELECT CustNo, UPPER(LTRIM(RTRIM(InsuredName))) as Name, ApplId, BrnCode, LTRIM(RTRIM(SUBSTRING(AcctId, 1, 8))) + '/' + CAST(CAST(SUBSTRING(AcctId, 17, 8) AS INT) AS VARCHAR) AcctId," +
                            "CASE WHEN RecStatus = 3 THEN 'Closed' ELSE 'Active' END as Stat FROM D045025 " + completed + " ORDER BY CustNo";
                        else if (btn330.CssClass == "btnhard")
                            completed = "SELECT CustNo, UPPER(LTRIM(RTRIM(AcctHolderName))) as Name, ApplId, BranchCd BrnCode, LTRIM(RTRIM(SUBSTRING(AcctId, 1, 8))) + '/' + CAST(CAST(SUBSTRING(AcctId, 17, 8) AS INT) AS VARCHAR) AcctId," +
                            "CASE WHEN RecStatus = 3 THEN 'Closed' ELSE 'Active' END as Stat FROM D045046 " + completed + " ORDER BY CustNo";
                        cnn.Open();
                        SqlCommand sqlCommand;
                        sqlCommand = new SqlCommand(completed, cnn);
                        sqlCommand.CommandTimeout = 900;

                        SqlDataReader rr = sqlCommand.ExecuteReader();

                        Gridtbl.DataSource = rr;
                        Gridtbl.DataBind();

                        txtaccno1.Disabled = true;
                        ddlaactype.Enabled = false;
                        ddlbranch.Enabled = false;


                        if (Gridtbl.Rows.Count > 0)
                        {
                            btnStop.Visible = chk_selectAll.Visible = true;
                            lbl_rowCount.Text = "Records Found: " + Gridtbl.Rows.Count;
                        }
                        else
                        {
                            btnStop.Visible = chk_selectAll.Visible = false;
                            lblmsg.Text = "No data found.";
                        }
                        chk_selectAll.Checked = false;
                        cnn.Close();
                    }
                }
                catch(Exception ex)
                {
                    lblmsg.Text = "Problem In Network. Unable to Update Stuff right now.";
                }
            }
        }
    }
}