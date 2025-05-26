using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;
using System.Data;
using System.Net;
using System.IO;

namespace BCCBExamPortal
{

    public partial class frmSignUserregistration : System.Web.UI.Page
    {
        protected void audit_trails()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            int flag = 0;
            con.Open();
            try
            {

                SqlCommand cmd1 = new SqlCommand("insert into Audit_trails_Intra  (Usercode,Page_Name,Visit_date_time,Is_there) values(@Usercode,@Page_Name,@Visit_date_time,@Is_there)", con);
                string subtime = Convert.ToString(System.DateTime.Now);
                cmd1.Parameters.AddWithValue("@Visit_date_time", System.DateTime.Now);
                cmd1.Parameters.AddWithValue("@Is_there", '1');
                cmd1.Parameters.AddWithValue("@Page_Name", "Sign_User_Reg");
                cmd1.Parameters.AddWithValue("@Usercode", Session["id"].ToString());
                cmd1.ExecuteNonQuery();
                cmd1.Dispose();

            }
            catch (Exception ep)
            {
                // txtlbl.Text = "Problem In Network, Your Test data is saved for that instance";
            }
            finally
            {
                con.Close();
            }
        }
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
                audit_trails();
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

                MenuItem mnuBG = new MenuItem();
                mnuBG.NavigateUrl = "~/BG.aspx";
                mnuBG.Target = "_blank";
                mnuBG.Text = "Banking Guarantee";
                mnuTest.ChildItems.Add(mnuBG);

                MenuItem mnuBGR = new MenuItem();
                mnuBGR.NavigateUrl = "~/BG_reports.aspx";
                mnuBGR.Target = "_blank";
                mnuBGR.Text = "BG Reports";
                mnuTest.ChildItems.Add(mnuBGR);

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

                Menu1.Items.Add(mnuTest3);

                check_user_details();
            }
           
        }

        private void check_user_details()
        {
            DataTable dp = new DataTable();
            string query = "select * from SC_USER_LOGIN where UserId='" + sessionlbl.Text + "'";
            dp = get_normal_data(query);
            if (dp.Rows.Count > 0)
            {
                if (dp.Rows[0]["Remarks"].ToString() == "Requested for OTP")
                {
                    txt_otp.Disabled = false;
                    btn_enroll.InnerText = "Enroll";
                }
                else if (dp.Rows[0]["Remarks"].ToString() == "Requested for Enrollment")
                {
                    txt_otp.Disabled = true;
                    txt_mobilenumber.Disabled = true;
                    btn_enroll.Disabled = true;
                    lbl_msg.Visible = true;
                    lbl_msg.InnerHtml = "Your request for Sign verification enrollment is under process. Please send an email on the below email address from your respective Branch email Id for authorization." + Environment.NewLine + " Email Id :  <span class='dark_span'>leon.m.gonsalves@bccb.co.in</span>";                
                }
                else
                {
                    Response.Redirect("frmViewSigns.aspx");
                }
            }
           
        }

        private DataTable get_normal_data(string query)
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            con.Open();
            try
            {
                SqlDataAdapter adpt = new SqlDataAdapter(query, con);
                adpt.Fill(dt);

            }
            catch (Exception el)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('Unable to fetch Customers details due to Network issue.');", true);
            }
            finally
            {
                con.Close();
            }

            return dt;
        }

        protected void generate_OTP(object sender, EventArgs e)
        {

            if(btn_enroll.InnerText== "Generate OTP")
            {
                if (txt_mobilenumber.Value != "")
                {
                    string xurl = "https://webpostservice.com/sendsms_v2.0/sendsms.php?apikey=YmNjYm5rVDphVnBiWmd5YQ==&type=TEXT&sender=BCCBnK&mobile=";

                    //Your Confidential OTP is{#var#}and it expires on{#var#}-BCCB
                    Random rn = new Random();
                    int otp = rn.Next(1001, 9999);
                    //try
                    //{
                      

                    //    string message = "Your Confidential OTP is " + otp + " and it expires on " + otp + " -BCCB";

                    //    string url = xurl.ToString() + txt_mobilenumber.Value.ToString() + "&message=" + message.ToString();   // pass data to API while traversing the table

                    //    WebClient htpclient = new WebClient();

                    //    Stream data = htpclient.OpenRead(url);

                    //    StreamReader reader = new StreamReader(data);

                    //    string respHTML = reader.ReadToEnd();
                    //}
                    //catch(Exception rt)
                    //{
                    //    ScriptManager.RegisterStartupScript(this, typeof(string), "script1", "callone('" + rt.Message + "');", true);
                    //}
                    DataTable dp = new DataTable();
                    string query = "select * from Employee_LoginTbl where Code='" + sessionlbl.Text + "'";
                    dp = get_normal_data(query);


                    //insert into SC_USER_LOGIN (UserId,UsrName,Password,Dept,CreateDate,UpdateDate,EmailId,LastLoginDate,Remarks,Extra) values(UserId,UsrName,Password,Dept,CreateDate,UpdateDate,EmailId,LastLoginDate,Remarks,Extra)
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
                    con.Open();
                    try
                    {
                        string dtc = DateTime.Now.ToString("yyyy-MM-dd");
                        DateTime dx = System.DateTime.Now;
                        string query1 = "insert into SC_USER_LOGIN (UserId,UsrName,Password,Dept,CreateDate,UpdateDate,EmailId,LastLoginDate,Remarks,Extra,MobileNumber) values(@UserId,@UsrName,@Password,@Dept,@CreateDate,@UpdateDate,@EmailId,@LastLoginDate,@Remarks,@Extra,@MobileNumber)";
                        SqlCommand cmd1 = new SqlCommand(query1, con);
                        cmd1.Parameters.AddWithValue("@UserId", dp.Rows[0]["Code"].ToString());
                        cmd1.Parameters.AddWithValue("@UsrName", dp.Rows[0]["Employee_Name"].ToString());
                        cmd1.Parameters.AddWithValue("@Password", "1");
                        cmd1.Parameters.AddWithValue("@Dept", "BRANCH");
                        cmd1.Parameters.AddWithValue("@CreateDate", DateTime.Now);
                        cmd1.Parameters.AddWithValue("@UpdateDate", DateTime.Now);
                        cmd1.Parameters.AddWithValue("@EmailId", dp.Rows[0]["Email_Id"].ToString());
                        cmd1.Parameters.AddWithValue("@LastLoginDate", DateTime.Now);
                        cmd1.Parameters.AddWithValue("@Remarks", "Requested for OTP");
                        cmd1.Parameters.AddWithValue("@Extra", otp);
                        cmd1.Parameters.AddWithValue("@MobileNumber", txt_mobilenumber.Value);
                        cmd1.ExecuteNonQuery();
                        cmd1.Dispose();
                        ScriptManager.RegisterStartupScript(this, typeof(string), "script1", "callone('OTP Generated for the inputed Mobile Number.');", true);
                        txt_otp.Disabled = false;
                        btn_enroll.InnerText = "Enroll";

                    }
                    catch (Exception rt)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(string), "script1", "callone('Error in Operation!!!');", true);
                    }
                    finally
                    {
                        con.Close();
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "script1", "callone('Please give valid details!!!');", true);
                }
            }
            else
            {
                if (txt_mobilenumber.Value != "" && txt_otp.Value != "")
                {
                    DataTable dp = new DataTable();
                    string query = "select * from SC_USER_LOGIN where UserId='" + sessionlbl.Text + "'";
                    dp = get_normal_data(query);

                    if (dp.Rows[0]["Extra"].ToString() == txt_otp.Value)
                    {
                        //insert into SC_USER_LOGIN (UserId,UsrName,Password,Dept,CreateDate,UpdateDate,EmailId,LastLoginDate,Remarks,Extra) values(UserId,UsrName,Password,Dept,CreateDate,UpdateDate,EmailId,LastLoginDate,Remarks,Extra)
                        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
                        con.Open();
                        try
                        {
                            string dtc = DateTime.Now.ToString("yyyy-MM-dd");
                            DateTime dx = System.DateTime.Now;
                            string query1 = "update SC_USER_LOGIN set Remarks=@Remarks,UpdateDate=@UpdateDate,LastLoginDate=@LastLoginDate where UserId=@UserId";
                            SqlCommand cmd1 = new SqlCommand(query1, con);
                            cmd1.Parameters.AddWithValue("@UserId", sessionlbl.Text);
                            cmd1.Parameters.AddWithValue("@UpdateDate", DateTime.Now);
                            cmd1.Parameters.AddWithValue("@LastLoginDate", DateTime.Now);
                            cmd1.Parameters.AddWithValue("@Remarks", "Requested for Enrollment");
                            cmd1.ExecuteNonQuery();
                            cmd1.Dispose();
                            ScriptManager.RegisterStartupScript(this, typeof(string), "script1", "callone('Request for Enrollment has been sent. Please wait till it approved.');", true);
                            txt_otp.Disabled = true;
                            txt_mobilenumber.Disabled = true;
                            btn_enroll.Disabled = true;
                            lbl_msg.Visible = true;
                            lbl_msg.InnerHtml = "Your request for Sign verification enrollment is under process. Please send an email on the below email address from your respective Branch email Id for authorization." + Environment.NewLine + " Email Id :  <span class='dark_span'>leon.m.gonsalves@bccb.co.in</span>";
                           
                            txt_mobilenumber.Value = "";
                            txt_otp.Value = "";
                        }
                        catch (Exception rt)
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(string), "script1", "callone('Error in Operation!!!');", true);
                        }
                        finally
                        {
                            con.Close();
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(string), "script1", "callone('Invalid OTP. Please input correct OTP.');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "script1", "callone('Please give valid details!!!');", true);
                }
                }
            


        }
    }
}