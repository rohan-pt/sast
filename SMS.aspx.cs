using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;

namespace BCCBExamPortal
{
    public partial class SMS : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //select * from tblSMSUpdate where convert(date,dtEntryDate, 103) >='2021-05-13'
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
                mnuTest1.Text = "Customer Service";
                mnuTest1.Value = "1";
                MenuItem mnuTestChild = new MenuItem();
                mnuTestChild.NavigateUrl = "~/SMS.aspx";
                mnuTestChild.Text = "SMS Analysis";
                mnuTestChild.Value = "2";
                mnuTest1.ChildItems.Add(mnuTestChild);

                MenuItem mnuTestChildn = new MenuItem();
                mnuTestChildn.NavigateUrl = "~/CustomerService.aspx";
                mnuTestChildn.Text = "Email Complaints";
                mnuTestChildn.Value = "3";
                mnuTest1.ChildItems.Add(mnuTestChildn);


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


                DateTime DateString = DateTime.Now.AddDays(-30);//ToString("yyyy-mm-dd")
                lbl_response.Text = "";
                string pp = DateString.ToShortDateString();
                string date_format = Convert.ToDateTime(pp).ToString("yyyy-MM-dd");
                string query = "select * from tblSMSUpdate where convert(date,dtEntryDate, 103) >='" + date_format + "' order by convert(date,dtEntryDate, 103) desc";
                lbl_response.Text = "Last 30 days SMS update Data";
                Get_data(query);
            }

        }
        protected void check_cust_no(object sender, EventArgs e)
        {
            string pp = Text1.Value.Trim();
            lbl_response.Text = "";
            //string date_format = Convert.ToDateTime(pp).ToString("yyyy-MM-dd");
            if (pp != "")
            {
                string query = "select * from tblSMSUpdate  where custID='" + pp + "'  order by convert(date,dtEntryDate, 103) desc";
                Get_data(query);
            }
            else
            {
                lbl_response.Text = "Please input Customer Number.";
            }
        }
        protected void check_mobile(object sender, EventArgs e)
        {
            string pp = txt_search.Value.Trim();
            lbl_response.Text = "";
            //string date_format = Convert.ToDateTime(pp).ToString("yyyy-MM-dd");
            if (pp != "")
            {
                string query = "select * from tblSMSUpdate  where custMobileNo='" + pp + "'  order by convert(date,dtEntryDate, 103) desc";
                Get_data(query);
            }
            else
            {
                lbl_response.Text = "Please input Mobile Number.";
            }
        }
        protected void get_date_data(object sender, EventArgs e)
        {
            lbl_response.Text = "";
            string pp = datepicker1.Text;
            //string date_format = Convert.ToDateTime(pp).ToString("yyyy-MM-dd");
            string query = "select * from tblSMSUpdate where convert(date,dtEntryDate, 103)='" + pp + "' order by convert(date,dtEntryDate, 103) desc";
            Get_data(query);
        }


        protected void Get_data(string query)
        {
           
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            con.Open();
            try
            {
                //DateTime temp = DateTime.ParseExact(sourceDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                // IFormatProvider culture = new CultureInfo("en-US", true);
               

                SqlCommand cmd1 = new SqlCommand(query, con);
                SqlDataReader usernameRdr = null;

                usernameRdr = cmd1.ExecuteReader();
                string tbl = "<table class='tbl_result'><tr><td class='tbl_th'>Customer Number</td><td class='tbl_th'>Name</td><td class='tbl_th'>Mobile Number</td><td class='tbl_th'>Updation Date</td><td class='tbl_th'>Satus</td></tr>";
                int done = 0, NR = 0, DC = 0, AD = 0, IB = 0, UR = 0,DU=0;
                if (usernameRdr.HasRows == true)
                {
                    while (usernameRdr.Read())
                    {
                        tbl =tbl+ "<tr><td class='tbl_td'>"+ usernameRdr["custID"].ToString() + "</td><td class='tbl_td'>" + usernameRdr["custName"].ToString() + "</td><td class='tbl_td'>" + usernameRdr["custMobileNo"].ToString() + "</td><td class='tbl_td'>" + usernameRdr["dtEntryDate"].ToString() + "</td>";
                        string reason = usernameRdr["reason"].ToString().Trim();
                       if (reason== "smsUpdate Successful" || reason == "numberUpdate Successful")
                        {
                            tbl = tbl+ "<td class='tbl_td'>Done</td>";
                            done++;
                        }
                        else if(reason == "New Registration")
                        {
                            tbl = tbl + "<td class='tbl_tdx'>New Registration</td>";
                            NR++;
                        }
                        else if (reason == "Delete create")
                        {
                            tbl = tbl + "<td class='tbl_td'>Delete Create</td>";
                            DC++;
                        }
                        else if (reason == "Already Done") 
                        {
                            tbl = tbl + "<td class='tbl_td'>Already Done</td>";
                            AD++;
                        }

                         else if (reason == "IB Customer")
                        {
                            tbl = tbl + "<td class='tbl_td'>IB Customer</td>";
                            IB++;
                        }//Double Entry Updated.
                        else if (reason == "Double Entry Updated.")
                        {
                            tbl = tbl + "<td class='tbl_td'>Double Updated.</td>";
                            DU++;
                        }
                        else
                        {
                            tbl = tbl + "<td class='tbl_td'>Unknown Reason</td>";
                            UR++;
                        }
                        tbl = tbl + "</tr>";
                    }
                    string xx = done.ToString()+"," + NR.ToString()+","+ DC.ToString()+ "," + AD.ToString()+ "," + IB.ToString()+ "," + UR.ToString() + "," + DU.ToString();
                    //[['Task', 'Hours per Day'],['Work',11],['Eat',2]];
                    // string[] cars = { "'SMS form type','Number of forms'", "'Successfully Done'," + done + "", "'New Registration'," + NR + "", "'Delete Create'," + DC + "", "'Already Done'," + AD + "", "'IB Customer'," + IB + "", "'Unknown Reason'," + UR + "" };
                    // string var1 = "[['SMS form type','Number of forms'],['Successfully Done',"+done+ "],['New Registration'," + NR + "],['Delete Create'," + DC + "],['Already Done'," + AD + "],['IB Customer'," + IB + "],['Unknown Reason'," + UR + "]]";
                    //ScriptManager.RegisterStartupScript(this, typeof(string), "Passing", "callone("+xx+");", true);
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "callone("+xx+")", true);
                    ScriptManager.RegisterStartupScript(this, typeof(string), "script1", "callone('" + xx + "');", true);
                }
                else
                {
                    tbl = "<table class='tbl_result'><tr><td class='tbl_th' style='background:#5da6f6;'>Nothing to display</td></tr>";
                }

                result_tbl.InnerHtml = tbl+ "</table>";

            }
            catch (Exception rr)
            {
                lbl_response.Text = rr.Message;
            }
        }
    }
}