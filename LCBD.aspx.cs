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
//using Oracle.DataAccess.Client;
using System.Collections;
using Oracle.ManagedDataAccess.Client;
using System.Threading.Tasks;
//using Oracle.ManagedDataAccess.EntityFramework;

namespace BCCBExamPortal
{    
    public partial class LCBD : System.Web.UI.Page
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
                cmd1.Parameters.AddWithValue("@Page_Name", "LCBD");
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

                //MenuItem mnuTest = new MenuItem();
                //mnuTest.NavigateUrl = "~/Home.aspx";
                //mnuTest.Text = "Home";
                //mnuTest.Value = "1";


                //MenuItem mnuTestChild2 = new MenuItem();
                //mnuTestChild2.NavigateUrl = "~/ChangePassword.aspx";
                //mnuTestChild2.Text = "Change Password";
                //mnuTestChild2.Value = "4";
                //MenuItem mnuTestChild3 = new MenuItem();
                //mnuTestChild3.NavigateUrl = "~/HelpPage.aspx";
                //mnuTestChild3.Text = "Help";
                //mnuTestChild3.Value = "3";

                ////MenuItem mnuTestChildx4 = new MenuItem();
                ////mnuTestChildx4.NavigateUrl = "http://www.bccb.co.in/";
                ////mnuTestChildx4.Target = "_blank";
                ////mnuTestChildx4.Text = "BCCB Website";
                ////mnuTestChildx4.Value = "4";

                ////MenuItem mnuTestChildx5 = new MenuItem();
                ////mnuTestChildx5.NavigateUrl = "http://guesthouse.bccb.co.in/";
                ////mnuTestChildx5.Target = "_blank";
                ////mnuTestChildx5.Text = "BCCB Guest House";
                ////mnuTestChildx5.Value = "5";

                //MenuItem mnuTestChildx6 = new MenuItem();
                //mnuTestChildx6.NavigateUrl = "~/EmpDashBoard.aspx";
                //mnuTestChildx6.Target = "_blank";
                //mnuTestChildx6.Text = "E@learning";
                //mnuTestChildx6.Value = "6";

                //MenuItem mnuTestChildx7 = new MenuItem();
                //mnuTestChildx6.NavigateUrl = "~/LCBD.aspx";
                //mnuTestChildx6.Target = "_blank";
                //mnuTestChildx6.Text = "LCBD DATA";
                //mnuTestChildx6.Value = "7";

                //mnuTest.ChildItems.Add(mnuTestChild2);
                //mnuTest.ChildItems.Add(mnuTestChild3);
                ////mnuTest.ChildItems.Add(mnuTestChildx4);
                ////mnuTest.ChildItems.Add(mnuTestChildx5);
                //mnuTest.ChildItems.Add(mnuTestChildx6);
                //mnuTest.ChildItems.Add(mnuTestChildx7);
                ////MenuItem ATM_C = new MenuItem();
                ////ATM_C.Text = "ATM CASH TRANSACTION";
                ////ATM_C.NavigateUrl = "~/AmlRating.aspx";
                ////mnuTest.ChildItems.Add(ATM_C);
                ////if (Models.CommonMethods.Isempallowed("select * from Employee_Access where Code='"+Session["id"].ToString()+"' and AML='1'"))
                ////{
                ////    mnuTest.ChildItems.Add(new MenuItem() {
                ////        NavigateUrl = "~/AmlRating.aspx",
                ////        Text = "View/Update AML Rating"
                ////    });
                ////}

                ////MenuItem mnuBG = new MenuItem();
                ////mnuBG.NavigateUrl = "~/BG.aspx";
                ////mnuBG.Target = "_blank";
                ////mnuBG.Text = "Bank Guarantee";
                ////mnuTest.ChildItems.Add(mnuBG);

                ////MenuItem mnuBGR = new MenuItem();
                ////mnuBGR.NavigateUrl = "~/BG_reports.aspx";
                ////mnuBGR.Target = "_blank";
                ////mnuBGR.Text = "BG Reports";
                ////mnuTest.ChildItems.Add(mnuBGR);

                ////MenuItem mnuTD = new MenuItem();
                ////mnuTD.NavigateUrl = "~/BGTD.aspx";
                ////mnuTD.Target = "_blank";
                ////mnuTD.Text = "BG TD Details";
                ////mnuTest.ChildItems.Add(mnuTD);

                //MenuItem mnuTest1 = new MenuItem();
                //mnuTest1.NavigateUrl = "#";
                //mnuTest1.Text = "Customer Service";
                //mnuTest1.Value = "1";
                ////MenuItem mnuTestChild = new MenuItem();
                ////mnuTestChild.NavigateUrl = "~/SMS.aspx";
                ////mnuTestChild.Text = "SMS Analysis";
                ////mnuTestChild.Value = "2";
                ////mnuTest1.ChildItems.Add(mnuTestChild);

                //MenuItem mnuTestChildn = new MenuItem();
                //mnuTestChildn.NavigateUrl = "~/CustomerService.aspx";
                //mnuTestChildn.Text = "Email Complaints";
                //mnuTestChildn.Value = "3";
                //mnuTest1.ChildItems.Add(mnuTestChildn);


                ////MenuItem mnuTest2 = new MenuItem();
                ////mnuTest2.NavigateUrl = "#";
                ////mnuTest2.Text = "ATM";
                ////mnuTest2.Value = "1";
                ////MenuItem mnuTestChild1 = new MenuItem();
                ////mnuTestChild1.NavigateUrl = "~/MainDashBoard.aspx?Menu=0";
                ////mnuTestChild1.Text = "POS Chargeback";
                ////mnuTestChild1.Value = "3";
                ////mnuTest2.ChildItems.Add(mnuTestChild1);

                ////MenuItem mnuTestChild4 = new MenuItem();
                ////mnuTestChild4.NavigateUrl = "~/ATMCashPosition.aspx?";
                ////mnuTestChild4.Target = "_blank";
                ////mnuTestChild4.Text = "ATM Cash Position";
                ////mnuTestChild4.Value = "4";
                ////mnuTest2.ChildItems.Add(mnuTestChild4);

                ////MenuItem mnuTestChild5 = new MenuItem();
                ////mnuTestChild5.NavigateUrl = "~/DebitCardNotWorking.aspx?";
                ////mnuTestChild5.Target = "_blank";
                ////mnuTestChild5.Text = "Debit Card Not Working";
                ////mnuTestChild5.Value = "5";
                ////mnuTest2.ChildItems.Add(mnuTestChild5);

                ////MenuItem mnuTestChild6 = new MenuItem();
                ////mnuTestChild6.NavigateUrl = "~/HotDB.aspx?";
                ////mnuTestChild6.Target = "_blank";
                ////mnuTestChild6.Text = "Hot Mark Card";
                ////mnuTestChild6.Value = "6";
                ////mnuTest2.ChildItems.Add(mnuTestChild6);


                ////MenuItem mnuAtmMaster = new MenuItem();
                ////mnuAtmMaster.NavigateUrl = "~/ATMMASTER.aspx";
                ////mnuAtmMaster.Target = "_blank";
                ////mnuAtmMaster.Text = "ATM Details";
                ////mnuTest2.ChildItems.Add(mnuAtmMaster);


                ////MenuItem mnuTest4 = new MenuItem();
                ////mnuTest4.NavigateUrl = "http://10.150.1.172:8083/DashBoard/frmBranchView.aspx";
                ////mnuTest4.Target = "_blank";
                ////mnuTest4.Text = "Circulars & Policies";
                ////mnuTest4.Value = "5";

                ////MenuItem mnuTest5 = new MenuItem();
                ////mnuTest5.NavigateUrl = "~/UnlockIB.aspx";
                ////mnuTest5.Target = "_blank";
                ////mnuTest5.Text = "Internet Banking";
                ////mnuTest5.Value = "6";


                ////MenuItem mnuTest6 = new MenuItem();
                ////mnuTest6.NavigateUrl = "#";
                ////mnuTest6.Target = "_blank";
                ////mnuTest6.Text = "PM Scheme";
                ////mnuTest6.Value = "7";


                ////MenuItem mnuTestChild7 = new MenuItem();
                ////mnuTestChild7.NavigateUrl = "~/PendingAuthPMScheme.aspx?";
                ////mnuTestChild7.Target = "_blank";
                ////mnuTestChild7.Text = "Unuthorized PM Scheme";
                ////mnuTestChild7.Value = "7";
                ////mnuTest6.ChildItems.Add(mnuTestChild7);

                ////MenuItem mnuTestChild8 = new MenuItem();
                ////mnuTestChild8.NavigateUrl = "~/StopPMScheme.aspx?";
                ////mnuTestChild8.Target = "_blank";
                ////mnuTestChild8.Text = "Stop PM Scheme";
                ////mnuTestChild8.Value = "8";
                ////mnuTest6.ChildItems.Add(mnuTestChild8);

                ////MenuItem mnuTestChild9 = new MenuItem();
                ////mnuTestChild9.NavigateUrl = "~/SearchPMScheme.aspx?";
                ////mnuTestChild9.Target = "_blank";
                ////mnuTestChild9.Text = "Search PM Scheme";
                ////mnuTestChild9.Value = "9";
                ////mnuTest6.ChildItems.Add(mnuTestChild9);

                ////MenuItem mnuTestChild10 = new MenuItem();
                ////mnuTestChild10.NavigateUrl = "~/ShowPMScheme.aspx";
                ////mnuTestChild10.Target = "_blank";
                ////mnuTestChild10.Text = "Search PM Scheme More";
                ////mnuTestChild10.Value = "10";
                ////mnuTest6.ChildItems.Add(mnuTestChild10);



                ////MenuItem mnuAbbcbsho = new MenuItem();
                ////mnuAbbcbsho.NavigateUrl = "~/ABBCBSHO.aspx";
                ////mnuAbbcbsho.Target = "_blank";
                ////mnuAbbcbsho.Text = "ABBCBSHO";
                ////mnuTest7.ChildItems.Add(mnuAbbcbsho);
                ////From here ECS change 
                ////MenuItem mnuTest7 = new MenuItem();
                ////mnuTest7.NavigateUrl = "~/#";
                ////mnuTest7.Target = "_blank";
                ////mnuTest7.Text = "ECS";
                ////mnuTest7.Value = "7";

                ////MenuItem mnuTestChild10 = new MenuItem();
                ////mnuTestChild10.NavigateUrl = "~/Ecs_menu.aspx?";
                ////mnuTestChild10.Target = "_blank";
                ////mnuTestChild10.Text = "Get ECS Status";
                ////mnuTestChild10.Value = "9";
                ////mnuTest7.ChildItems.Add(mnuTestChild10);

                ////MenuItem mnuTestChild11 = new MenuItem();
                ////mnuTestChild11.NavigateUrl = "~/upadte_ecs.aspx?";
                ////mnuTestChild11.Target = "_blank";
                ////mnuTestChild11.Text = "Update ECS";
                ////mnuTestChild11.Value = "9";
                ////mnuTest7.ChildItems.Add(mnuTestChild11);

                ////--------------------------------------------------------------------------------------------------------

                //MenuItem mnuTest3 = new MenuItem();
                //mnuTest3.NavigateUrl = "~/LogOut.aspx";
                //mnuTest3.Text = "Log Out";
                //mnuTest3.Value = "1";

                //Menu1.Items.Add(mnuTest);
                ////Menu1.Items.Add(mnuTest2);
                //Menu1.Items.Add(mnuTest1);
                ////Menu1.Items.Add(mnuTest4);
                ////Menu1.Items.Add(mnuTest5);
                ////Menu1.Items.Add(mnuTest6);

                //string loggedInEmpCode = Session["id"].ToString();

                ////Delete PM Scheme option only available for Rodney and Dorlisa
                ////if (new string[] { "08430", "06860", "06160" }.Contains(loggedInEmpCode))
                ////{
                ////    MenuItem mnuTestChild11 = new MenuItem();
                ////    mnuTestChild11.NavigateUrl = "~/DeletePMScheme.aspx";
                ////    mnuTestChild11.Target = "_blank";
                ////    mnuTestChild11.Text = "Delete PM Scheme";
                ////    mnuTestChild11.Value = "11";
                ////    mnuTest6.ChildItems.Add(mnuTestChild11);
                ////}
                //MenuItem mnuTest7 = new MenuItem();
                //mnuTest7.NavigateUrl = "#";
                //mnuTest7.Target = "_blank";
                //mnuTest7.Text = "Other";
                //mnuTest7.Value = "13";
                ////if (Application["HeadOfficeEmpCodes"] != null)
                ////{
                ////    string[] empCodes = (string[])Application["HeadOfficeEmpCodes"];
                ////    if (empCodes.Contains(loggedInEmpCode))
                ////    {

                ////        mnuTest7.ChildItems.Add(new MenuItem
                ////        {
                ////            NavigateUrl = "~/ECS.aspx",
                ////            Target = "_blank",
                ////            Text = "Download ECS Report",
                ////            Value = "12"
                ////        });
                ////        //mnuTest7.ChildItems.Add(new MenuItem {
                ////        //    NavigateUrl = "~/TotalDepositors.aspx",
                ////        //    Target = "_blank",
                ////        //    Text = "Total Depositors",
                ////        //});
                ////    }
                ////}
                //// string[] directorLoanVisibleCodes = new string[] { "11059", "11022", "08560","08420", "11067", "07730", "07070", "11007", "08360", "07720", "08313", "07060", "08346", "07610", "08322", "08314", "03240", "08331", "07640", "08391", "03308", "07200", "07860", "07590", "06120", "08337", "08370", "07660", "07300", "07470", "07850", "07580", "07190", "03250", "08352", "07120", "08321", "03304", "07390", "08341", "08345", "08020", "06050", "08406", "08315", "07220", "03312", "08409", "08180", "06280", "07680", "06290", "08325", "08374", "08395", "08327", "07430", "08366", "06780", "06060", "08372", "08376", "06320", "06730", "07670", "08260", "08347", "07810", "08353", "05740", "07770", "07160", "07790", "06770", "08531", "08400", "08452" };
                ////if (Models.CommonMethods.Isempallowed("select * from Employee_Access where Code='" + Session["id"].ToString() + "' and DM='1'"))
                ////    mnuTest7.ChildItems.Add(new MenuItem
                ////    {
                ////        NavigateUrl = "~/DirectorAccounts.aspx",
                ////        Target = "_blank",
                ////        Text = "Director Accounts"
                ////    });

                ////mnuTest7.ChildItems.Add(new MenuItem() {
                ////    NavigateUrl = "~/ABBCBSHO.aspx",
                ////    Target = "_blank",
                ////    Text = "ABBCBSHO"
                ////});
                //// string[] allowedEmpCodes = new string[] { "11059", "02023", "02022", "06180", "06080", "08430","08560", "08420" };
                ////if (Models.CommonMethods.Isempallowed("select * from Employee_Access where Code='" + Session["id"].ToString() + "' and Prod='1'"))
                ////{
                ////    mnuTest7.ChildItems.Add(new MenuItem()
                ////    {
                ////        NavigateUrl = "~/ProductUnauthorized.aspx",
                ////        Target = "_blank",
                ////        Text = "Product Unauthorized",
                ////    });
                ////}
                ////if (Models.CommonMethods.Isempallowed("select * from Employee_Access where Code='" + Session["id"].ToString() + "' and Swift='1'"))
                ////{
                ////    mnuTest7.ChildItems.Add(new MenuItem()
                ////    {
                ////        NavigateUrl = "~/Swiftlimit.aspx",
                ////        Target = "_blank",
                ////        Text = "Swift Limit",
                ////    });
                ////}

                //MenuItem mnuTestChild12 = new MenuItem();
                //mnuTestChild12.NavigateUrl = "~/frmViewSigns.aspx";
                //mnuTestChild12.Target = "_blank";
                //mnuTestChild12.Text = "Sign Verify";
                //mnuTestChild12.Value = "12";
                //mnuTest7.ChildItems.Add(mnuTestChild12);

                //MenuItem mnuTestChild13 = new MenuItem();
                //mnuTestChild13.NavigateUrl = "~/OFSS_Knowledge_Base.aspx";
                //mnuTestChild13.Target = "_blank";
                //mnuTestChild13.Text = "OFSS Tables";
                //mnuTestChild13.Value = "13";
                //mnuTest7.ChildItems.Add(mnuTestChild13);

                //MenuItem mnuTestChild14 = new MenuItem();
                //mnuTestChild14.NavigateUrl = "~/Clearing_Reports.aspx";
                //mnuTestChild14.Target = "_blank";
                //mnuTestChild14.Text = "Clearing Reports";
                //mnuTestChild14.Value = "14";
                //mnuTest7.ChildItems.Add(mnuTestChild14);

                //if (mnuTest7.ChildItems.Count > 0)
                //    Menu1.Items.Add(mnuTest7);
                //Menu1.Items.Add(mnuTest3);
                customer_details();
            }
        }

        protected void search_item(object sender, EventArgs e)
        {
            if (search_txt.Value.Trim() != "")
            {
                string querystring = "select RRN,Transaction_Date,Maturaty_Date,Customer_Name,LC_RRN,Invoice_number,Invoice_amt from LCBD_DATA where LC_RRN like'%" + search_txt.Value.Trim() + "%' or RRN like '%" + search_txt.Value.Trim() + "%' or Customer_Name like '%" + search_txt.Value.Trim() + "%' or Transaction_Date like '%" + search_txt.Value.Trim() + "%' or Maturaty_Date like '%" + search_txt.Value.Trim() + "%' or Invoice_number like'%" + search_txt.Value.Trim() + "%' or Invoice_amt like '%" + search_txt.Value.Trim() + "%' order by LCBD_ID desc";
                DataTable dt = get_normal_data(querystring);
                string query_build = "<table class='tbl_con_hold'><tr><td class='th_css' style='width:5%;'>NO</td><td class='th_css' style='width:15%;'>LCBD RRN</td><td class='th_css' style='width:10%;'>Transaction Date</td><td class='th_css' style='width:10%;'>Maturity Date</td>  <td class='th_css' style='width:15%;'>Name</td> <td class='th_css' style='width:15%;'>LC RRN</td> <td class='th_css' style='width:10%;'>Invoice Number</td> <td class='th_css' style='width:10%;'>Invoice Amount</td></tr>";
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        query_build = query_build + "<tr><td class='td_css' style='width:5%;'>" + (i + 1) + "</td><td class='td_css' style='width:15%;'>" + dt.Rows[i]["RRN"].ToString() + "</td><td class='td_css' style='width:10%;'>" + dt.Rows[i]["Transaction_Date"].ToString() + "</td><td class='td_css' style='width:10%;'>" + dt.Rows[i]["Maturaty_Date"].ToString() + "</td>  <td class='td_css' style='width:15%;'>" + dt.Rows[i]["Customer_Name"].ToString() + "</td> <td class='td_css' style='width:15%;'>" + dt.Rows[i]["LC_RRN"].ToString() + "</td> <td class='td_css' style='width:10%;'>" + dt.Rows[i]["Invoice_number"].ToString() + "</td> <td class='td_css' style='width:10%;'>" + dt.Rows[i]["Invoice_amt"].ToString() + "</td></tr>";

                    }
                }
                query_build = query_build + "</table>";
                tbl_data_bind.InnerHtml = query_build;

            }


            //select RRN,Transaction_Date,Maturaty_Date,Customer_Name,LC_RRN,Invoice_number,Invoice_amt from LCBD_DATA where LC_RRN like'%Abhijeet Gharat%' or RRN like '%Abhijeet Gharat%' or Customer_Name like '%2022-05-01%' or Transaction_Date like '%2022-05-01%' or Maturaty_Date like '%2022-05-01%' or Invoice_number like'%2022-05-01%' or Invoice_amt like '%2022-05-01%' order by LCBD_ID desc
        }
        protected void on_btn_submit(object sender, EventArgs e)
        {
            if (!IsCallback)
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);

                lcbd_submit.Disabled = true;

                string branch = "59";

                string rrn = txt_rrn.Value.Trim();
                string tr_date = dt_tra.Text.Trim();
                string lc_rrn = LC_rrn.Value;
                string customer_name = Cust_Name.Value.Replace("'", "");//lcustomer.SelectedItem.ToString();
                string tr_mat_date = dt_matu.Text.Trim();
                string customer_number = Cust_num.Value;
                string acc_num = Acc_Num.Value;
                string nat_busi_act = natu_busi_act.Value.Replace("'", "");
                string Latest_ship_new_dt = late_ship_date_new.Text;
                string amt_dis_on_dis = amt_dis_dis.Value;
                string due_date = Due_dt_res.Text;
                string inv_amt = txt_amt_bill.Value;
                string let_nego_dt = lt_nego_dt.Text;
                string pay_date_lcdis = pay_dt_lc_dt.Text;
                string int_rate = rate_int.Value;
                string Cour_cahg = cour_chg.Value;
                string Stat = "0";
                string flag = "OK";
                string Ln_Start_DT = "", Ln_end_DT = "", Limit_amt = "";
                        
                if (customer_number.Trim()!="")
                {
                    Stat = "1";
                    Ln_Start_DT = "21-05-2022";
                    Ln_end_DT = "31-12-2023";
                    Limit_amt = "750000000";
                }
                


                if (ddlcustomer.SelectedItem.ToString() == "Add New Customer")
                {
                   
                    if (customer_name.Trim() != "")
                    {
                        con.Open();
                        try
                        {

                            DateTime dx = System.DateTime.Now;
                            SqlCommand cmd1 = new SqlCommand("insert into [LCBD_Customer] (LCBD_Cust_Name,Customer_Number,Acc_Number,Nature_Busi,Stat,Ln_Start_DT,Ln_end_DT,Limit_amt) values (@LCBD_Cust_Name,@Customer_Number,@Acc_Number,@Nature_Busi,@Stat,@Ln_Start_DT,@Ln_end_DT,@Limit_amt)", con);
                            string subtime = Convert.ToString(System.DateTime.Now);
                            cmd1.Parameters.AddWithValue("@LCBD_Cust_Name", customer_name);
                            cmd1.Parameters.AddWithValue("@Customer_Number", customer_number.Trim());
                            cmd1.Parameters.AddWithValue("@Acc_Number", acc_num);
                            cmd1.Parameters.AddWithValue("@Nature_Busi", nat_busi_act);
                            cmd1.Parameters.AddWithValue("@Stat", Stat);
                            cmd1.Parameters.AddWithValue("@Ln_Start_DT", Ln_Start_DT);
                            cmd1.Parameters.AddWithValue("@Ln_end_DT", Ln_end_DT);
                            cmd1.Parameters.AddWithValue("@Limit_amt", Limit_amt);
                            cmd1.ExecuteNonQuery();
                            cmd1.Dispose();


                        }
                        catch (Exception ep)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('Unable to create record because of some Exceptions!!!');", true);
                        }
                        finally
                        {
                            con.Close();
                        }
                    }
                    else
                    {
                        flag = "NOK";
                    }
                }
                else
                {
                    customer_name = ddlcustomer.SelectedItem.ToString();

                    con.Open();
                    try
                    {

                        DateTime dx = System.DateTime.Now;
                        SqlCommand cmd1 = new SqlCommand("update [LCBD_Customer] set Customer_Number=@Customer_Number,Acc_Number=@Acc_Number,Nature_Busi=@Nature_Busi,Stat=@Stat,Ln_Start_DT=@Ln_Start_DT,Ln_end_DT=@Ln_end_DT,Limit_amt=@Limit_amt where LCBD_Cust_Name=@LCBD_Cust_Name", con);
                        string subtime = Convert.ToString(System.DateTime.Now);
                        cmd1.Parameters.AddWithValue("@LCBD_Cust_Name", customer_name);
                        cmd1.Parameters.AddWithValue("@Customer_Number", customer_number);
                        cmd1.Parameters.AddWithValue("@Acc_Number", acc_num);
                        cmd1.Parameters.AddWithValue("@Nature_Busi", nat_busi_act);
                        cmd1.Parameters.AddWithValue("@Stat", Stat);
                        cmd1.Parameters.AddWithValue("@Ln_Start_DT", Ln_Start_DT);
                        cmd1.Parameters.AddWithValue("@Ln_end_DT", Ln_end_DT);
                        cmd1.Parameters.AddWithValue("@Limit_amt", Limit_amt);
                        cmd1.ExecuteNonQuery();
                        cmd1.Dispose();


                    }
                    catch (Exception ep)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('Unable to create record because of some Exceptions!!!');", true);
                    }
                    finally
                    {
                        con.Close();
                    }


                }

                if (rrn != "" && tr_date != "" && lc_rrn != "" && flag=="OK" && Latest_ship_new_dt != "" && amt_dis_on_dis!="" && due_date!="" && inv_amt!="" && let_nego_dt!="" && pay_date_lcdis!="" && int_rate!="" && Cour_cahg!="")
                { 

                    string lc_res_ty = "N";
                    if (LC_Res.Checked == true)
                    {
                        lc_res_ty = "Y";
                    }
                    string noc = "N";//CHK_Noc
                    if (CHK_Noc.Checked == true)
                    {
                        noc = "Y";
                    }
                  
                
                    string invoi_num = txt_inv.Value;
              
                    string wfrod = "N";//CHK_Noc
                    if (chk_funds.Checked == true)
                    {
                        wfrod = "Y";
                    }
                    string wout31 = "N";//CHK_Noc
                    if (chk_outsta.Checked == true)
                    {
                        wout31 = "Y";
                    }
                    string ag_name = agent_name.Value;
                 
                   
                    string num_days = num_dys.Value;
                   

                    string mar_rate = rate_mar.Value;
                    string no_days_mar = no_dys_mar.Value;
                    string mardays = mar_days.Value;
                    string lc_hand_chag = lc_hand_chg.Value;
                    string SFMS_CHa = sfms_chg.Value;
                    string Serv_chg = serv_chg.Value;
                  
                    string Check_amt_re = "N";
                    if (chk_amt_rec.Checked == true)
                    {
                        Check_amt_re = "Y";
                    }

                    string CHe_actu_rec = txt_amt_rec.Value;
                   
                   
                     
                    
                    string Date_pay_rec = dt_pay_rec.Text;
                    string Excess_or_less = excess_less_in.Value;
                    string delayed = delayed_early.Value;
                    string over_due_int = over_int.Value;
                    string intr_rev = intrev.Value;
                    string mar_rev = "N";
                    if (mar_rev_ben_chk.Checked == true)
                    {
                        mar_rev = "Y";
                    }
                    string Date_amt = dt_amt_paid.Text;
                    string lc_add_chg = lc_adv_chg.Value;
                    string Net_rev1 = Net_rev.Value;
                    string Int_Pl_3903_1 = Int_Pl_3903.Value;



                    // string flag="ok";         
                    con.Open();
                    string msg = "";
                    try
                    {
                        if (lcbd_submit.InnerText == "ADD")
                        {
                            DateTime dx = System.DateTime.Now;
                            string query1 = "INSERT INTO LCBD_DATA " +
    "(Branch, RRN, Transaction_Date, Maturaty_Date, Customer_Name, Customer_ID, Acc_Num, Business_Nature, Currency, LC_RRN, NOC_BNK, LC_res_any_bnk, amt_dis_disc, due_date_rec_disc, Invoice_number, Invoice_amt, funds_rec_issu_bnk, wether_outstd_31, agent_name, latest_nego_date, payment_date, no_of_D, rate_of_int, rate_of_mar, days_margin, LC_Mar_days, LC_Hand_CHG, LC_ADV_CHG, SFMS_Chg, Service_Chg, Courier_Chg, Amount_rec_chg, Actual_amt_rec, Date_pay_rec, Excess_less, delayed, overdue_int, int_rec_early_pay, margin_rev_ben, amt_paid_ben, creator, creation_date, updated_by, updation_date,Net_rev,Int_Pl_3903,Latest_Ship_dt) " +
         "VALUES " +
     " (@Branch, @RRN, @Transaction_Date, @Maturaty_Date, @Customer_Name, @Customer_ID, @Acc_Num, @Business_Nature, @Currency, @LC_RRN, @NOC_BNK, @LC_res_any_bnk, @amt_dis_disc, @due_date_rec_disc, @Invoice_number, @Invoice_amt, " +
     "@funds_rec_issu_bnk, @wether_outstd_31, @agent_name, @latest_nego_date, @payment_date, @no_of_D, @rate_of_int, @rate_of_mar, @days_margin, @LC_Mar_days, @LC_Hand_CHG, @LC_ADV_CHG, @SFMS_Chg, @Service_Chg, @Courier_Chg, @Amount_rec_chg, @Actual_amt_rec, @Date_pay_rec, @Excess_less, @delayed, @overdue_int, @int_rec_early_pay, @margin_rev_ben, @amt_paid_ben, @creator, @creation_date, @updated_by, @updation_date,@Net_rev,@Int_Pl_3903,@Latest_ship_new_dt)";

                            SqlCommand cmd1 = new SqlCommand(query1, con);
                            string subtime = Convert.ToString(System.DateTime.Now);


                            cmd1.Parameters.AddWithValue("@funds_rec_issu_bnk", wfrod);
                            cmd1.Parameters.AddWithValue("@wether_outstd_31", wout31);
                            cmd1.Parameters.AddWithValue("@agent_name", ag_name);
                            cmd1.Parameters.AddWithValue("@latest_nego_date", let_nego_dt);

                            cmd1.Parameters.AddWithValue("@payment_date", pay_date_lcdis);
                            cmd1.Parameters.AddWithValue("@no_of_D", num_days);
                            cmd1.Parameters.AddWithValue("@rate_of_int", int_rate);
                            cmd1.Parameters.AddWithValue("@rate_of_mar", mar_rate);



                            cmd1.Parameters.AddWithValue("@days_margin", no_days_mar);
                            cmd1.Parameters.AddWithValue("@LC_Mar_days", mardays);
                            cmd1.Parameters.AddWithValue("@LC_Hand_CHG", lc_hand_chag);
                            cmd1.Parameters.AddWithValue("@LC_ADV_CHG", lc_add_chg);

                            cmd1.Parameters.AddWithValue("@SFMS_Chg", SFMS_CHa);
                            cmd1.Parameters.AddWithValue("@Service_Chg", Serv_chg);
                            cmd1.Parameters.AddWithValue("@Courier_Chg", Cour_cahg);
                            cmd1.Parameters.AddWithValue("@Amount_rec_chg", Check_amt_re);

                            cmd1.Parameters.AddWithValue("@Actual_amt_rec", CHe_actu_rec);
                            cmd1.Parameters.AddWithValue("@Date_pay_rec", Date_pay_rec);
                            cmd1.Parameters.AddWithValue("@Excess_less", Excess_or_less);
                            cmd1.Parameters.AddWithValue("@delayed", delayed);

                            cmd1.Parameters.AddWithValue("@overdue_int", over_due_int);
                            cmd1.Parameters.AddWithValue("@int_rec_early_pay", intr_rev);
                            cmd1.Parameters.AddWithValue("@margin_rev_ben", mar_rev);
                            cmd1.Parameters.AddWithValue("@amt_paid_ben", Date_amt);

                            cmd1.Parameters.AddWithValue("@Currency", "INR");
                            cmd1.Parameters.AddWithValue("@LC_RRN", lc_rrn);
                            cmd1.Parameters.AddWithValue("@NOC_BNK", noc);
                            cmd1.Parameters.AddWithValue("@LC_res_any_bnk", lc_res_ty);

                            cmd1.Parameters.AddWithValue("@amt_dis_disc", amt_dis_on_dis);
                            cmd1.Parameters.AddWithValue("@due_date_rec_disc", due_date);
                            cmd1.Parameters.AddWithValue("@Invoice_number", invoi_num);
                            cmd1.Parameters.AddWithValue("@Invoice_amt", inv_amt);


                            cmd1.Parameters.AddWithValue("@Branch", branch);
                            cmd1.Parameters.AddWithValue("@RRN", rrn);
                            cmd1.Parameters.AddWithValue("@Transaction_Date", tr_date);
                            cmd1.Parameters.AddWithValue("@Maturaty_Date", tr_mat_date);

                            cmd1.Parameters.AddWithValue("@Customer_Name", customer_name);
                            cmd1.Parameters.AddWithValue("@Customer_ID", customer_number);
                            cmd1.Parameters.AddWithValue("@Acc_Num", acc_num);
                            cmd1.Parameters.AddWithValue("@Business_Nature", nat_busi_act);
                            DateTime dt = System.DateTime.Now;
                            cmd1.Parameters.AddWithValue("@creator", Session["id"].ToString());
                            cmd1.Parameters.AddWithValue("@creation_date", dt.ToShortDateString());
                            cmd1.Parameters.AddWithValue("@updated_by", "");
                            cmd1.Parameters.AddWithValue("@updation_date", ""); //@Net_rev,@Int_Pl_3903
                            cmd1.Parameters.AddWithValue("@Net_rev", Net_rev1);
                            cmd1.Parameters.AddWithValue("@Int_Pl_3903", Int_Pl_3903_1);
                            cmd1.Parameters.AddWithValue("@Latest_ship_new_dt", Latest_ship_new_dt);

                            cmd1.ExecuteNonQuery();
                            cmd1.Dispose();
                            msg = "LCBD Record added successfully!!";
                        }
                        else
                        {
                            DateTime dx = System.DateTime.Now;
                            string query1 = "update LCBD_DATA " +
    " set Transaction_Date=@Transaction_Date, Maturaty_Date=@Maturaty_Date, Customer_Name=@Customer_Name, Customer_ID=@Customer_ID, Acc_Num=@Acc_Num, " +
    " Business_Nature=@Business_Nature, Currency=@Currency, LC_RRN=@LC_RRN, NOC_BNK=@NOC_BNK, LC_res_any_bnk=@LC_res_any_bnk, amt_dis_disc=@amt_dis_disc, due_date_rec_disc=@due_date_rec_disc, " +
    " Invoice_number=@Invoice_number, Invoice_amt=@Invoice_amt, funds_rec_issu_bnk=@funds_rec_issu_bnk, wether_outstd_31=@wether_outstd_31, agent_name=@agent_name, latest_nego_date=@latest_nego_date " +
                                " , payment_date=@payment_date, no_of_D=@no_of_D, rate_of_int=@rate_of_int, rate_of_mar=@rate_of_mar, days_margin=@days_margin, LC_Mar_days=@LC_Mar_days, LC_Hand_CHG=@LC_Hand_CHG " +
                                " , LC_ADV_CHG=@LC_ADV_CHG, SFMS_Chg=@SFMS_Chg, Service_Chg=@Service_Chg, Courier_Chg=@Courier_Chg, Amount_rec_chg=@Amount_rec_chg, Actual_amt_rec=@Actual_amt_rec, Date_pay_rec=@Date_pay_rec " +
                                " , Excess_less=@Excess_less, delayed=@delayed, overdue_int=@overdue_int, int_rec_early_pay=@int_rec_early_pay, margin_rev_ben=@margin_rev_ben, amt_paid_ben= @amt_paid_ben " +
                                " , updated_by=@updated_by, updation_date=@updation_date, Net_rev=@Net_rev,Int_Pl_3903=@Int_Pl_3903,Latest_Ship_dt=@Latest_ship_new_dt where LCBD_ID='" + lbl_id_hdn.InnerText + "'";
                            SqlCommand cmd1 = new SqlCommand(query1, con);
                            string subtime = Convert.ToString(System.DateTime.Now);


                            cmd1.Parameters.AddWithValue("@funds_rec_issu_bnk", wfrod);
                            cmd1.Parameters.AddWithValue("@wether_outstd_31", wout31);
                            cmd1.Parameters.AddWithValue("@agent_name", ag_name);
                            cmd1.Parameters.AddWithValue("@latest_nego_date", let_nego_dt);

                            cmd1.Parameters.AddWithValue("@payment_date", pay_date_lcdis);
                            cmd1.Parameters.AddWithValue("@no_of_D", num_days);
                            cmd1.Parameters.AddWithValue("@rate_of_int", int_rate);
                            cmd1.Parameters.AddWithValue("@rate_of_mar", mar_rate);



                            cmd1.Parameters.AddWithValue("@days_margin", no_days_mar);
                            cmd1.Parameters.AddWithValue("@LC_Mar_days", mardays);
                            cmd1.Parameters.AddWithValue("@LC_Hand_CHG", lc_hand_chag);
                            cmd1.Parameters.AddWithValue("@LC_ADV_CHG", lc_add_chg);

                            cmd1.Parameters.AddWithValue("@SFMS_Chg", SFMS_CHa);
                            cmd1.Parameters.AddWithValue("@Service_Chg", Serv_chg);
                            cmd1.Parameters.AddWithValue("@Courier_Chg", Cour_cahg);
                            cmd1.Parameters.AddWithValue("@Amount_rec_chg", Check_amt_re);

                            cmd1.Parameters.AddWithValue("@Actual_amt_rec", CHe_actu_rec);
                            cmd1.Parameters.AddWithValue("@Date_pay_rec", Date_pay_rec);
                            cmd1.Parameters.AddWithValue("@Excess_less", Excess_or_less);
                            cmd1.Parameters.AddWithValue("@delayed", delayed);

                            cmd1.Parameters.AddWithValue("@overdue_int", over_due_int);
                            cmd1.Parameters.AddWithValue("@int_rec_early_pay", intr_rev);
                            cmd1.Parameters.AddWithValue("@margin_rev_ben", mar_rev);
                            cmd1.Parameters.AddWithValue("@amt_paid_ben", Date_amt);

                            cmd1.Parameters.AddWithValue("@Currency", "INR");
                            cmd1.Parameters.AddWithValue("@LC_RRN", lc_rrn);
                            cmd1.Parameters.AddWithValue("@NOC_BNK", noc);
                            cmd1.Parameters.AddWithValue("@LC_res_any_bnk", lc_res_ty);

                            cmd1.Parameters.AddWithValue("@amt_dis_disc", amt_dis_on_dis);
                            cmd1.Parameters.AddWithValue("@due_date_rec_disc", due_date);
                            cmd1.Parameters.AddWithValue("@Invoice_number", invoi_num);
                            cmd1.Parameters.AddWithValue("@Invoice_amt", inv_amt);
                          
                            cmd1.Parameters.AddWithValue("@Transaction_Date", tr_date);
                            cmd1.Parameters.AddWithValue("@Maturaty_Date", tr_mat_date);

                            cmd1.Parameters.AddWithValue("@Customer_Name", customer_name);
                            cmd1.Parameters.AddWithValue("@Customer_ID", customer_number);
                            cmd1.Parameters.AddWithValue("@Acc_Num", acc_num);
                            cmd1.Parameters.AddWithValue("@Business_Nature", nat_busi_act);
                            DateTime dt = System.DateTime.Now;                            
                            cmd1.Parameters.AddWithValue("@updated_by", Session["id"].ToString());
                            cmd1.Parameters.AddWithValue("@updation_date", dt.ToShortDateString());
                            cmd1.Parameters.AddWithValue("@Net_rev", Net_rev1);
                            cmd1.Parameters.AddWithValue("@Int_Pl_3903", Int_Pl_3903_1);
                            cmd1.Parameters.AddWithValue("@Latest_ship_new_dt", Latest_ship_new_dt);

                            cmd1.ExecuteNonQuery();
                            cmd1.Dispose();
                            msg = "LCBD Record updated successfully!!";
                        }


                    }
                    catch (Exception ep)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('Unable to create record because of some Exceptions!!!');", true);
                    }
                    finally
                    {
                        con.Close();
                    }




                    reset_all();

                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('" + msg + "');", true);
                    lcbd_submit.Disabled = false;
                }
                else
                {
                    if (rrn == "")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('Please give Reference Number!!');", true);
                        txt_rrn.Focus();
                    }
                    else if(tr_date == "")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('Please give Transaction Date!!');", true);
                        dt_tra.Focus();
                    }
                    else if (flag != "OK")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('Please give Customer Details!!');", true);
                        ddlcustomer.Focus();
                    }
                    else if (lc_rrn == "")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('Please give LC Reference Number!!');", true);
                        LC_rrn.Focus();
                    }
                    else if (Latest_ship_new_dt == "")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('Please give Latest Shipment Date!!');", true);
                        late_ship_date_new.Focus();
                    }
                    else if (amt_dis_on_dis == "")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('Please give input for Amount disbursed after discounting!!');", true);
                        amt_dis_dis.Focus();
                    }
                    else if (due_date == "")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('Please give Due Date of receipt of funds under discounted LC!!');", true);
                        Due_dt_res.Focus();
                    }
                    else if (inv_amt == "")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('Please give Invoice Amount!!');", true);
                        txt_amt_bill.Focus();
                    }
                    else if (let_nego_dt == "")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('Please give Latest Negotiation Date!!');", true);
                        lt_nego_dt.Focus();
                    }
                    else if (pay_date_lcdis == "")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('Please give Payment Date for LC Dis!!');", true);
                        pay_dt_lc_dt.Focus();
                    }
                    else if (int_rate == "")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('Please give Rate of interest!!');", true);
                        rate_int.Focus();
                    }
                    else if (Cour_cahg == "")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('Please give Courier Charges!!');", true);
                        cour_chg.Focus();
                    }
                    

                    lcbd_submit.Disabled = false;
                }
            }

        }

        private void reset_all()
        {
            //txt_rrn.Value = "";
            dt_tra.Text = "";
            LC_rrn.Value = "";
            dt_matu.Text = "";
            Cust_Name.Value = "";
            Cust_num.Value = "";
            Acc_Num.Value = "";
            natu_busi_act.Value = "";
            LC_Res.Checked = false;
            CHK_Noc.Checked = false;
            chk_funds.Checked = false;
            chk_outsta.Checked = false;
            chk_amt_rec.Checked = false;
            txt_amt_rec.Value = "";
            trans_ship.Checked = false;
            parti_ship.Checked = false;
            amt_dis_dis.Value = "";
            Due_dt_res.Text = "";
            txt_inv.Value = "";
            txt_amt_bill.Value = "";
            agent_name.Value = "";
            lt_nego_dt.Text = "";
            pay_dt_lc_dt.Text = "";
            num_dys.Value = "";
            rate_int.Value = "";
            rate_mar.Value = "";

            mar_days.Value = "";
            lc_hand_chg.Value = "1475";
            lc_adv_chg.Value = "590";
            sfms_chg.Value = "";
            serv_chg.Value = "";
            cour_chg.Value = "";
            dt_pay_rec.Text = "";
            excess_less_in.Value = "";
            delayed_early.Value = "";
            over_int.Value = "";
            intrev.Value = "";
            mar_rev_ben_chk.Checked = false;
            dt_amt_paid.Text = "";

            LC_issue_bank.Value = "";
            LC_App.Value = "";
            LC_Beni.Value = "";
            Pl_of_exp.Value = "";
            LC_Amt.Value = "";
            Tenor_txt.Value = "";
            loading_bd.Value = "";
            trans_to.Value = "";
            LC_rest_bnk.Value = "";
            ship_date.Text = "";
            late_ship_date_new.Text = "";
            lcbd_submit.InnerText = "ADD";
            Net_rev.Value = "";
            Int_Pl_3903.Value = "";
        }


        protected string get_info(string query)
        {
            string acc_no = "";
            try
            {
                string conString = @"server=10.200.1.96; database=BCCB_CBR_DB; uid=bccbreport; password=bccb#123;";
                //string conString = @"server=10.200.1.154; database=NotivaPrime; uid=notivaprime; password=notiva123;";
                string sqlcommand = query;
                //string sqlcommand = "select OFSS_Field,Mandatory,OFSS_Field_Type from OFSS_Logic where OFSS_Module='LC' and OFSS_Tbl='Lctb_Upload_Shipment'";
                SqlConnection cnn;
                cnn = new SqlConnection(conString);
                cnn.Open();
                SqlCommand sqlCommand = new SqlCommand(sqlcommand, cnn);
                sqlCommand.CommandTimeout = 900;
                SqlDataAdapter sda = new SqlDataAdapter(sqlCommand);
                DataTable dtx = new DataTable();
                sda.Fill(dtx);
                cnn.Close();


                for (int j = 0; j < dtx.Rows.Count; j++)
                {
                    acc_no = dtx.Rows[j][0].ToString();
                }


            }

            catch (Exception ep)
            {
               
            }
            return acc_no;
        }



        private DataTable get_oracle_data(string query)
        {
            DataTable dt = new DataTable();
          //  query = "select * from ICTM_SDE";
            try
            {
                //            string ConString = "Data Source=(DESCRIPTION =" +
                //"(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.40)(PORT = 1521))" +
                //"(CONNECT_DATA =" +
                //  "(SERVER = DEDICATED)" +
                //  "(SERVICE_NAME = FC_UAT)));" +
                //  "User Id=UBS_UAT1;Password=UBS_UAT1;";

                string ConString = "Data Source=(DESCRIPTION =" +
                "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.80)(PORT = 1521))" +
                "(CONNECT_DATA =" +
                  "(SERVER = DEDICATED)" +
                  "(SERVICE_NAME = pidb)));" +
                  "User Id=pidb;Password=pidb;";
                OracleConnection con = new OracleConnection(ConString);
                con.Open();
                OracleCommand cmd = new OracleCommand(query, con);
                OracleDataReader dr = cmd.ExecuteReader();            
                dt.Load(dr);
                int l = dt.Rows.Count;
                con.Close();

            }
            catch (Exception yy)
            {
                // ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('" + yy.Message + "');", true);
                Label1.InnerText = yy.Message;
            }


            return dt;


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
                string er = "Unable to fetch Customers details due to Network issue: " + el +" : : "+ query;
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('Unable to fetch Customers details due to Network issue:');", true);
            }
            finally
            {
                con.Close();
            }

            return dt;
        }


        protected void check_report1(object sender, EventArgs e)
        {
            if(report_date.Text!="")
            {
                string query = "select SUM(cast(Invoice_amt as float)) as Invoice_amt  from LCBD_DATA where Maturaty_Date>='" + report_date.Text.ToString() + "' and Transaction_Date<='" + report_date.Text.ToString() + "' and (Amount_rec_chg='N' or Date_pay_rec='' or Actual_amt_rec='')";// and Amount_rec_chg='N'";
                string query1 = "select  isnull(SUM(cast(Invoice_amt as float)),0) as Invoice_amt  from LCBD_DATA where Maturaty_Date<'" + report_date.Text.ToString() + "'   and (Amount_rec_chg='N' or Date_pay_rec='' or Actual_amt_rec='') ";
                div_outstanding.InnerText=Convert.ToString(Convert.ToDouble(get_info(query))+ Convert.ToDouble(get_info(query1)));
                query = "select COUNT(*)  as con_num  from LCBD_DATA where Maturaty_Date>='" + report_date.Text.ToString() + "' and Transaction_Date<='" + report_date.Text.ToString() + "' and (Amount_rec_chg='N' or Date_pay_rec='' or Actual_amt_rec='')";// and Amount_rec_chg='N'";
                query1 = "select  COUNT(*) as con_num  from LCBD_DATA where Maturaty_Date<'" + report_date.Text.ToString() + "'   and (Amount_rec_chg='N' or Date_pay_rec='' or Actual_amt_rec='') ";
                contr_count.InnerText= Convert.ToString(Convert.ToInt32(get_info(query))+ Convert.ToInt32(get_info(query1)));

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('Please select Date of Report!!');", true);
            }
        }

        protected void check_report3(object sender, EventArgs e)
        {
            // //RRN,Transaction_Date,Maturaty_Date,Customer_Name,Amount_rec_chg,Actual_amt_rec,Date_pay_rec

        
                    DateTime df = DateTime.Now;
            string query = "select RRN,Transaction_Date,Maturaty_Date,Customer_Name,Amount_rec_chg,Actual_amt_rec,Date_pay_rec from LCBD_DATA where Transaction_Date<='" + df.ToString("yyyy-MM-dd") + "' and Maturaty_Date>='" + df.ToString("yyyy-MM-dd") + "' and (Amount_rec_chg!='N' or Actual_amt_rec!='' or Date_pay_rec!='')";
              
            DataTable dt = get_normal_data(query);
                string query_build = "<table class='tbl_con_hold_new' id='new_bind' runat='server'>";
                double d = 0;
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                    query_build = query_build + "<tr><td class='rx1_td1'>" + dt.Rows[i]["RRN"].ToString() + "</td><td class='rx1_td2'>" + dt.Rows[i]["Transaction_Date"].ToString() + "</td><td class='rx1_td1'>" + dt.Rows[i]["Maturaty_Date"].ToString() + "</td><td class='rx1_td1'>" + dt.Rows[i]["Customer_Name"].ToString() + "</td>  <td class='rx1_td1'>" + dt.Rows[i]["Amount_rec_chg"].ToString() + "</td> <td class='rx1_td1'>" + dt.Rows[i]["Actual_amt_rec"].ToString() + "</td><td class='rx1_td2'>" + dt.Rows[i]["Date_pay_rec"].ToString() + "</td></tr>";                      
                    }
                }             
                query_build = query_build + "</table>";
            final_error_repo.InnerHtml = query_build;

        }

        protected void check_report4(object sender, EventArgs e)
        {
            // //RRN,Transaction_Date,Maturaty_Date,Customer_Name,Amount_rec_chg,Actual_amt_rec,Date_pay_rec


            DateTime df = DateTime.Now;
            string query = "select LC_RRN as RRN,'Issue Date' as Date_of_issue  from LC_RFMS_PENDING_LCBD where F31C1=''";

            DataTable dt = get_normal_data(query);
            string query_build = "<table class='tbl_con_hold_blun' runat='server'><tr><td class='br_th_br'>Check Number</td><td class='br_th'>Check Description</td><td class='br_th'>Check Status</td></tr>";
            string original_data = "";
            int d = 0;

            if (dt.Rows.Count > 0)
            {
                d = 1;
                div_lbl.InnerText = "LC Date of Issue Blank";
                query_build = query_build + "<tr><td class='br_td_br'>1.</td><td class='br_td'>LC Date of Issue Blank</td><td class='br_td_red'>Pending</td></tr>";
                original_data = "<table class='tbl_con_hold_data_container' runat='server'><tr><td class='blun_th'>Issue SR No</td><td class='blun_th'>Issue Primary Key</td><td class='blun_th'>Target To rectify</td></tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    original_data = original_data + "<tr><td class='blun_td'>" + (i + 1) + "</td><td class='blun_td'>" + dt.Rows[i]["RRN"].ToString() + "</td><td class='blun_td'>" + dt.Rows[i]["Date_of_issue"].ToString() + "</td></tr>";
                }
            }
            else
            {
                query_build = query_build + "<tr><td class='br_td_br'>1.</td><td class='br_td'>LC Date of Issue Blank</td><td class='br_td_green'>Clear</td></tr>";
            }
          

            if (d == 0)
            {
                query = "select LC_RRN as RRN,cast(DATEDIFF(Day,cast((SUBSTRING(F31C1,5,2)+'/'+SUBSTRING(F31C1,7,2)+'/'+SUBSTRING(F31C1,1,4)) as datetime),cast((SUBSTRING(F31D1,5,2)+'/'+SUBSTRING(F31D1,7,2)+'/'+SUBSTRING(F31D1,1,4))  as datetime)) as varchar) +'D' as Check_Point from LC_RFMS_PENDING_LCBD where cast(DATEDIFF(Day,cast((SUBSTRING(F31C1,5,2)+'/'+SUBSTRING(F31C1,7,2)+'/'+SUBSTRING(F31C1,1,4)) as datetime),cast((SUBSTRING(F31D1,5,2)+'/'+SUBSTRING(F31D1,7,2)+'/'+SUBSTRING(F31D1,1,4))  as datetime)) as int) <=0";

                dt = get_normal_data(query);
                              
                if (dt.Rows.Count > 0)
                {
                    d = 1;
                    div_lbl.InnerText = "Tenor days in negative";
                    query_build = query_build + "<tr><td class='br_td_br'>1.</td><td class='br_td'>Tenor days in negative</td><td class='br_td_red'>Pending</td></tr>";
                    original_data = "<table class='tbl_con_hold_data_container' runat='server'><tr><td class='blun_th'>Issue SR No</td><td class='blun_th'>Issue Primary Key</td><td class='blun_th'>Target To rectify</td></tr>";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        original_data = original_data + "<tr><td class='blun_td'>" + (i + 1) + "</td><td class='blun_td'>" + dt.Rows[i]["RRN"].ToString() + "</td><td class='blun_td'>" + dt.Rows[i]["Check_Point"].ToString() + "</td></tr>";
                    }
                }
                else
                {
                    query_build = query_build + "<tr><td class='br_td_br'>2.</td><td class='br_td'>Tenor days in Negative</td><td class='br_td_green'>Clear</td></tr>";
                }
            }

            if (d == 0)
            {
                query = "select LC_RRN as RRN,'Latest Shipment Date Blank' as Check_Point from LC_RFMS_PENDING_LCBD where F44C1=''";

                dt = get_normal_data(query);

                if (dt.Rows.Count > 0)
                {
                    d = 1;
                    div_lbl.InnerText = "Latest Shipment Date Blank";
                    query_build = query_build + "<tr><td class='br_td_br'>1.</td><td class='br_td'>Latest Shipment Date Blank</td><td class='br_td_red'>Pending</td></tr>";
                    original_data = "<table class='tbl_con_hold_data_container' runat='server'><tr><td class='blun_th'>Issue SR No</td><td class='blun_th'>Issue Primary Key</td><td class='blun_th'>Target To rectify</td></tr>";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        original_data = original_data + "<tr><td class='blun_td'>" + (i + 1) + "</td><td class='blun_td'>" + dt.Rows[i]["RRN"].ToString() + "</td><td class='blun_td'>" + dt.Rows[i]["Check_Point"].ToString() + "</td></tr>";
                    }
                }
                else
                {
                    query_build = query_build + "<tr><td class='br_td_br'>3.</td><td class='br_td'>Latest Shipment Date Blank</td><td class='br_td_green'>Clear</td></tr>";
                }
            }
            
            if (d == 0)
            {
                query = "select RRN,'Customer Number Issue' as Check_Point from LCBD_DATA where LC_RRN in(select distinct LC_RRN from LC_RFMS_PENDING_LCBD where F501 in (select APPLI_NAME from LC_Applicant where Cust_Id='')) and Customer_ID=''";

                dt = get_normal_data(query);

                if (dt.Rows.Count > 0)
                {
                    d = 1;
                    div_lbl.InnerText = "Customer Number Issue";
                    query_build = query_build + "<tr><td class='br_td_br'>1.</td><td class='br_td'>Customer Number Issue</td><td class='br_td_red'>Pending</td></tr>";
                    original_data = "<table class='tbl_con_hold_data_container' runat='server'><tr><td class='blun_th'>Issue SR No</td><td class='blun_th'>Issue Primary Key</td><td class='blun_th'>Target To rectify</td></tr>";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        original_data = original_data + "<tr><td class='blun_td'>" + (i + 1) + "</td><td class='blun_td'>" + dt.Rows[i]["RRN"].ToString() + "</td><td class='blun_td'>" + dt.Rows[i]["Check_Point"].ToString() + "</td></tr>";
                    }
                }
                else
                {
                    query_build = query_build + "<tr><td class='br_td_br'>4.</td><td class='br_td'>Customer Number Issue</td><td class='br_td_green'>Clear</td></tr>";
                }
            }
            if (d == 0)
            {
                query = "select LC_RRN as RRN,REPLACE(REPLACE(SUBSTRING(F32B1,4,15),',00',''),',','') as Check_Point from LC_RFMS_PENDING_LCBD where cast(REPLACE(REPLACE(SUBSTRING(F32B1,4,15),',00',''),',','') as float)<=0";
               
                dt = get_normal_data(query);

                if (dt.Rows.Count > 0)
                {
                    d = 1;
                    div_lbl.InnerText = "LC Amount is wrong";
                    query_build = query_build + "<tr><td class='br_td_br'>1.</td><td class='br_td'>LC Amount is wrong</td><td class='br_td_red'>Pending</td></tr>";
                    original_data = "<table class='tbl_con_hold_data_container' runat='server'><tr><td class='blun_th'>Issue SR No</td><td class='blun_th'>Issue Primary Key</td><td class='blun_th'>Target To rectify</td></tr>";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        original_data = original_data + "<tr><td class='blun_td'>" + (i + 1) + "</td><td class='blun_td'>" + dt.Rows[i]["RRN"].ToString() + "</td><td class='blun_td'>" + dt.Rows[i]["Check_Point"].ToString() + "</td></tr>";
                    }
                }
                else
                {
                    query_build = query_build + "<tr><td class='br_td_br'>5.</td><td class='br_td'>LC Amount is wrong</td><td class='br_td_green'>Clear</td></tr>";
                }
            }
            if (d == 0)
            {
                query = "select LC_RRN as RRN,'Credit Avl with Blank' as Check_Point from LC_RFMS_PENDING_LCBD where F41A1=''";
                //select LC_RRN as RRN,'Credit Avl with Blank' as Check_Point from LC_RFMS_PENDING_LCBD where F41A1=''
                dt = get_normal_data(query);

                if (dt.Rows.Count > 0)
                {
                    d = 1;
                    div_lbl.InnerText = "Credit Avl with Blank";
                    query_build = query_build + "<tr><td class='br_td_br'>1.</td><td class='br_td'>Credit Avl with Blank</td><td class='br_td_red'>Pending</td></tr>";
                    original_data = "<table class='tbl_con_hold_data_container' runat='server'><tr><td class='blun_th'>Issue SR No</td><td class='blun_th'>Issue Primary Key</td><td class='blun_th'>Target To rectify</td></tr>";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        original_data = original_data + "<tr><td class='blun_td'>" + (i + 1) + "</td><td class='blun_td'>" + dt.Rows[i]["RRN"].ToString() + "</td><td class='blun_td'>" + dt.Rows[i]["Check_Point"].ToString() + "</td></tr>";
                    }
                }
                else
                {
                    query_build = query_build + "<tr><td class='br_td_br'>6.</td><td class='br_td'>Credit Avl with Blank</td><td class='br_td_green'>Clear</td></tr>";
                }
            }
          
            if (d == 0)
            {
                query = "select RRN,'Latest Shipment Date Blank' as Check_Point from LCBD_DATA where Latest_Ship_dt is null";
                //select LC_RRN as RRN,'Credit Avl with Blank' as Check_Point from LC_RFMS_PENDING_LCBD where F41A1=''
                dt = get_normal_data(query);

                if (dt.Rows.Count > 0)
                {
                    d = 1;
                    div_lbl.InnerText = "Latest Shipment Date Blank";
                    query_build = query_build + "<tr><td class='br_td_br'>1.</td><td class='br_td'>Latest Shipment Date Blank</td><td class='br_td_red'>Pending</td></tr>";
                    original_data = "<table class='tbl_con_hold_data_container' runat='server'><tr><td class='blun_th'>Issue SR No</td><td class='blun_th'>Issue Primary Key</td><td class='blun_th'>Target To rectify</td></tr>";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        original_data = original_data + "<tr><td class='blun_td'>" + (i + 1) + "</td><td class='blun_td'>" + dt.Rows[i]["RRN"].ToString() + "</td><td class='blun_td'>" + dt.Rows[i]["Check_Point"].ToString() + "</td></tr>";
                    }
                }
                else
                {
                    query_build = query_build + "<tr><td class='br_td_br'>7.</td><td class='br_td'>Latest Shipment Date Blank</td><td class='br_td_green'>Clear</td></tr>";
                }
            }
            if (d == 0)
            {
                query = "select LC_RRN as RRN,cast(DATEDIFF(Day,cast((SUBSTRING(F31C1,5,2)+'/'+SUBSTRING(F31C1,7,2)+'/'+SUBSTRING(F31C1,1,4)) as datetime),cast((SUBSTRING(F44C1,5,2)+'/'+SUBSTRING(F44C1,7,2)+'/'+SUBSTRING(F44C1,1,4))  as datetime)) as varchar) +'D' as Check_Point from LC_RFMS_PENDING_LCBD where cast(DATEDIFF(Day,cast((SUBSTRING(F31C1,5,2)+'/'+SUBSTRING(F31C1,7,2)+'/'+SUBSTRING(F31C1,1,4)) as datetime),cast((SUBSTRING(F44C1,5,2)+'/'+SUBSTRING(F44C1,7,2)+'/'+SUBSTRING(F44C1,1,4))  as datetime)) as int) <=0";
                dt = get_normal_data(query);

                if (dt.Rows.Count > 0)
                {
                    d = 1;
                    div_lbl.InnerText = "Issue Date and Latest Shipment date Issue";
                    query_build = query_build + "<tr><td class='br_td_br'>1.</td><td class='br_td'>Issue Date and Latest Shipment date Issue</td><td class='br_td_red'>Pending</td></tr>";
                    original_data = "<table class='tbl_con_hold_data_container' runat='server'><tr><td class='blun_th'>Issue SR No</td><td class='blun_th'>Issue Primary Key</td><td class='blun_th'>Target To rectify</td></tr>";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        original_data = original_data + "<tr><td class='blun_td'>" + (i + 1) + "</td><td class='blun_td'>" + dt.Rows[i]["RRN"].ToString() + "</td><td class='blun_td'>" + dt.Rows[i]["Check_Point"].ToString() + "</td></tr>";
                    }
                }
                else
                {
                    query_build = query_build + "<tr><td class='br_td_br'>8.</td><td class='br_td'>Issue Date and Latest Shipment date Issue</td><td class='br_td_green'>Clear</td></tr>";
                }
            }
            if (d == 0)
            {
                query = "select LC_RRN as RRN,cast(DATEDIFF(Day,cast((SUBSTRING(F44C1,5,2)+'/'+SUBSTRING(F44C1,7,2)+'/'+SUBSTRING(F44C1,1,4)) as datetime),cast((SUBSTRING(F31D1,5,2)+'/'+SUBSTRING(F31D1,7,2)+'/'+SUBSTRING(F31D1,1,4))  as datetime)) as varchar) +'D' as Check_Point from LC_RFMS_PENDING_LCBD where cast(DATEDIFF(Day,cast((SUBSTRING(F44C1,5,2)+'/'+SUBSTRING(F44C1,7,2)+'/'+SUBSTRING(F44C1,1,4)) as datetime),cast((SUBSTRING(F31D1,5,2)+'/'+SUBSTRING(F31D1,7,2)+'/'+SUBSTRING(F31D1,1,4))  as datetime)) as int) <=0";

                dt = get_normal_data(query);

                if (dt.Rows.Count > 0)
                {
                    d = 1;
                    div_lbl.InnerText = "Expiry Date and Latest Shipment date Issue";
                    query_build = query_build + "<tr><td class='br_td_br'>1.</td><td class='br_td'>Expiry Date and Latest Shipment date Issue</td><td class='br_td_red'>Pending</td></tr>";
                    original_data = "<table class='tbl_con_hold_data_container' runat='server'><tr><td class='blun_th'>Issue SR No</td><td class='blun_th'>Issue Primary Key</td><td class='blun_th'>Target To rectify</td></tr>";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        original_data = original_data + "<tr><td class='blun_td'>" + (i + 1) + "</td><td class='blun_td'>" + dt.Rows[i]["RRN"].ToString() + "</td><td class='blun_td'>" + dt.Rows[i]["Check_Point"].ToString() + "</td></tr>";
                    }
                }
                else
                {
                    query_build = query_build + "<tr><td class='br_td_br'>9.</td><td class='br_td'>Expiry Date and Latest Shipment date Issue</td><td class='br_td_green'>Clear</td></tr>";
                }
            }            
            if (d == 0)
            {
                query = "select LC_RRN as RRN,'Expiry Place Issue' as Check_Point from LC_RFMS_PENDING_LCBD where rtrim(ltrim(substring(F31D1,9,20)))=''";

                dt = get_normal_data(query);

                if (dt.Rows.Count > 0)
                {
                    d = 1;
                    div_lbl.InnerText = "Expiry Place Issue";
                    query_build = query_build + "<tr><td class='br_td_br'>1.</td><td class='br_td'>Expiry Place Issue</td><td class='br_td_red'>Pending</td></tr>";
                    original_data = "<table class='tbl_con_hold_data_container' runat='server'><tr><td class='blun_th'>Issue SR No</td><td class='blun_th'>Issue Primary Key</td><td class='blun_th'>Target To rectify</td></tr>";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        original_data = original_data + "<tr><td class='blun_td'>" + (i + 1) + "</td><td class='blun_td'>" + dt.Rows[i]["RRN"].ToString() + "</td><td class='blun_td'>" + dt.Rows[i]["Check_Point"].ToString() + "</td></tr>";
                    }
                }
                else
                {
                    query_build = query_build + "<tr><td class='br_td_br'>10.</td><td class='br_td'>Expiry Place Issue</td><td class='br_td_green'>Clear</td></tr>";
                }
            }
            if (d == 0)
            {
                query = "select distinct  rtrim(ltrim(SENADDR)) as SENADDR from BCCB_CBR_DB.dbo.LC_RFMS_PENDING_LCBD";

                dt = get_normal_data(query);
                if (check_ifsc(dt) != "")
                {
                    query = "select LC_RRN as RRN,rtrim(ltrim(SENADDR)) as Check_Point from BCCB_CBR_DB.dbo.LC_RFMS_PENDING_LCBD where SENADDR in (" + check_ifsc(dt) + ")";
                    dt = get_normal_data(query);
                    if (dt.Rows.Count > 0)
                    {
                        d = 1;
                        div_lbl.InnerText = "IFSC Code Issue";
                        query_build = query_build + "<tr><td class='br_td_br'>1.</td><td class='br_td'>IFSC Code Issue</td><td class='br_td_red'>Pending</td></tr>";
                        original_data = "<table class='tbl_con_hold_data_container' runat='server'><tr><td class='blun_th'>Issue SR No</td><td class='blun_th'>Issue Primary Key</td><td class='blun_th'>Target To rectify</td></tr>";
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            original_data = original_data + "<tr><td class='blun_td'>" + (i + 1) + "</td><td class='blun_td'>" + dt.Rows[i]["RRN"].ToString() + "</td><td class='blun_td'>" + dt.Rows[i]["Check_Point"].ToString() + "</td></tr>";
                        }
                    }
                    else
                    {
                        query_build = query_build + "<tr><td class='br_td_br'>11.</td><td class='br_td'>IFSC Code Issue</td><td class='br_td_green'>Clear</td></tr>";
                    }
                }
                else
                {
                    query_build = query_build + "<tr><td class='br_td_br'>11.</td><td class='br_td'>IFSC Code Issue</td><td class='br_td_green'>Clear</td></tr>";
                }
            }



            if (d != 0)
            {
                original_data = original_data + "</table>";
            }
            else
            {
                div_lbl.InnerText = "All Checks are cleared...Congrats. You can Go Live Now. ";
            }
            query_build = query_build + "</table>";
            blunder_summary.InnerHtml = query_build;
            blunder_data.InnerHtml = original_data;
        }

        protected string check_ifsc(DataTable dt)
        {
            string tr = "";
            for(int i=0;i<dt.Rows.Count;i++)
            {
                string query = "select IFSCCD from BCCBREPORT.dbo.D946022 where IFSCCD='"+dt.Rows[i]["SENADDR"].ToString() +"' ";
                DataTable dr = get_normal_data(query);
                if(dr.Rows.Count==0)
                {
                    if(tr=="")
                    {
                        tr = "'" + dt.Rows[i]["SENADDR"].ToString() + "'";
                    }
                    else
                    {
                        tr = tr + "," + "'" + dt.Rows[i]["SENADDR"].ToString() + "'";
                    }
                }

            }
            return tr;
        }


        protected void check_report2(object sender, EventArgs e)
        {
           
            if (ddlcustomer2.SelectedItem.ToString() != "Add New Customer")
            {              
                new_cut_div.Visible = false;
                string query = "";
                if (chk_all.Checked == true)
                {
                     query = "select *,'A' as Cur_Stat from LCBD_DATA where Customer_Name='" + ddlcustomer2.SelectedItem.ToString() + "'";
                }
                else
                {
                    DateTime df = DateTime.Now;
                    query = "select LCBD_ID,Transaction_Date,Maturaty_Date,Customer_Name,LC_RRN,case when Invoice_amt='' then 0 else cast(Invoice_amt as float) end as Invoice_amt,Latest_Ship_dt,RRN,'A' as Cur_Stat " +
"from LCBD_DATA " +
"where Customer_Name='" + ddlcustomer2.SelectedItem.ToString() + "' and   Transaction_Date<='" + df.ToString("yyyy-MM-dd") + "' and Maturaty_Date>='" + df.ToString("yyyy-MM-dd") + "' " +
"union all  " +
"select LCBD_ID,Transaction_Date,Maturaty_Date,Customer_Name,LC_RRN,case when Invoice_amt='' then 0 else cast(Invoice_amt as float) end as Invoice_amt,Latest_Ship_dt,RRN,'M' as Cur_Stat " +
"from LCBD_DATA " +
"where Customer_Name='" + ddlcustomer2.SelectedItem.ToString() + "' and   Transaction_Date<='" + df.ToString("yyyy-MM-dd") + "' and Maturaty_Date<'" + df.ToString("yyyy-MM-dd") + "' " +
"and (Amount_rec_chg='N' or Date_pay_rec='' or Actual_amt_rec='')";

                    //query = "select * from LCBD_DATA where Customer_Name='" + ddlcustomer2.SelectedItem.ToString() + "' and   Transaction_Date<='" + df.ToString("yyyy-MM-dd") + "' and Maturaty_Date>'" + df.ToString("yyyy-MM-dd") + "'";// and Amount_rec_chg='N'";
                    //select * from LCBD_DATA where Customer_Name='RITEX EXPORTS' and   Transaction_Date<='2022-10-28' and Maturaty_Date>'2022-10-28' 
                }
                // string querystring = "select RRN,Transaction_Date,Maturaty_Date,Customer_Name,LC_RRN,Invoice_number,Invoice_amt from LCBD_DATA where LC_RRN like'%" + search_txt.Value.Trim() + "%' or RRN like '%" + search_txt.Value.Trim() + "%' or Customer_Name like '%" + search_txt.Value.Trim() + "%' or Transaction_Date like '%" + search_txt.Value.Trim() + "%' or Maturaty_Date like '%" + search_txt.Value.Trim() + "%' or Invoice_number like'%" + search_txt.Value.Trim() + "%' or Invoice_amt like '%" + search_txt.Value.Trim() + "%' order by LCBD_ID desc";
                DataTable dt = get_normal_data(query);
                string query_build = "<table class='tbl_con_hold_new' id='new_bind' runat='server'>";
                double d = 0;
                int a = 0, m = 0;
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["Cur_Stat"].ToString() == "A")
                        {
                            query_build = query_build + "<tr><td class='r1_td1'>" + dt.Rows[i]["RRN"].ToString() + "</td><td class='r1_td2'>" + dt.Rows[i]["Transaction_Date"].ToString() + "</td><td class='r1_td1'>" + dt.Rows[i]["Maturaty_Date"].ToString() + "</td><td class='r1_td1'>" + dt.Rows[i]["Customer_Name"].ToString() + "</td>  <td class='r1_td1'>" + dt.Rows[i]["LC_RRN"].ToString() + "</td> <td class='r1_td1'>" + dt.Rows[i]["Invoice_amt"].ToString() + "</td><td class='r1_td2'>" + dt.Rows[i]["Latest_Ship_dt"].ToString() + "</td></tr>";
                            a++;
                        }
                        else
                        {
                            query_build = query_build + "<tr><td class='r1_td1'>" + dt.Rows[i]["RRN"].ToString() + "</td><td class='r1_td2'>" + dt.Rows[i]["Transaction_Date"].ToString() + "</td><td class='r1_td1x'>" + dt.Rows[i]["Maturaty_Date"].ToString() + "</td><td class='r1_td1'>" + dt.Rows[i]["Customer_Name"].ToString() + "</td>  <td class='r1_td1'>" + dt.Rows[i]["LC_RRN"].ToString() + "</td> <td class='r1_td1'>" + dt.Rows[i]["Invoice_amt"].ToString() + "</td><td class='r1_td2'>" + dt.Rows[i]["Latest_Ship_dt"].ToString() + "</td></tr>";
                            m++;
                        }
                      
                            d = d + Convert.ToDouble(dt.Rows[i]["Invoice_amt"].ToString());
                     
                    }
                }
                con_count.InnerHtml = "Contracts Count:<span style='color: red; '>" + dt.Rows.Count + "</span>";
                con_amt.InnerHtml= "Contracts Count:<span style='color: red; '>" + d + "</span>";
                con_act.InnerHtml = "Active Contract:<span style='color: green; '>" + a + "</span>";
                con_matu_out.InnerHtml = "Mature but Outstanding Amount:<span style='color: red; '>" + m + "</span>";
                query_build = query_build + "</table>";
                data_repo_1.InnerHtml = query_build;


            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('Please select Customer Name!!');", true);
            }
        }

        protected void Chart3_Load(object sender, EventArgs e)
        {
            if (ddl_fin_yr.SelectedItem.ToString() != "Select Financial Year")
            {
                SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);

                DataTable dataTable = new DataTable();
                Chart3.Palette = System.Web.UI.DataVisualization.Charting.ChartColorPalette.Pastel;
                cnn.Open();
                try
                {

                    ArrayList listDataSource = new ArrayList();
                    Chart3.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
                    Chart3.ChartAreas[0].AxisY.MajorGrid.LineWidth = 0;



                    Chart3.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
                    Chart3.ChartAreas[0].AxisY.MinorGrid.Enabled = false;
                    Chart3.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                    Chart3.ChartAreas[0].AxisX.MinorGrid.Enabled = false;

                    List<int> yValues = new List<int>();
                    List<string> xValues = new List<string>();


                    xValues.Add("APR");
                    xValues.Add("MAY");
                    xValues.Add("JUN");
                    xValues.Add("JUL");
                    xValues.Add("AUG");
                    xValues.Add("SEP");
                    xValues.Add("OCT");
                    xValues.Add("NOV");
                    xValues.Add("DEC");
                    xValues.Add("JAN");
                    xValues.Add("FEB");
                    xValues.Add("MAR");

                    for (int i = 0; i < 12; i++)
                    {
                        yValues.Add(0);
                    }
                    string[] yr = ddl_fin_yr.SelectedItem.ToString().Split('-');
                    string sqlcommand1 = "select  count(*) as tr_count,MONTH(CONVERT(date, Transaction_Date)) Month from LCBD_DATA where CONVERT(date, Transaction_Date)>'" + yr[0] + "-04-01' and CONVERT(date, Transaction_Date)<'" + yr[1] + "-03-31' group by MONTH(CONVERT(date, Transaction_Date))";



                    SqlCommand sqlCommand = new SqlCommand(sqlcommand1, cnn);

                    sqlCommand.CommandTimeout = 900;


                    SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                    da.Fill(dataTable);
                    da.Dispose();

                    if (dataTable.Rows.Count > 0)
                    {
                        for (int j = 0; j < dataTable.Rows.Count; j++)
                        {
                            int p = Convert.ToInt32(dataTable.Rows[j]["Month"].ToString());
                            p = p - 4;
                            if (p < 0)
                            {
                                p = p + 12;
                            }

                            yValues[p] = Convert.ToInt32(dataTable.Rows[j]["tr_count"].ToString());
                        }

                    }

                    Chart3.Series[0].Points.DataBindXY(xValues, yValues);
                    Chart3.Series[0].IsValueShownAsLabel = true;

                    Chart3.ChartAreas[0].AxisX.Title = "LCBD Business (For Year :" + ddl_fin_yr.SelectedItem.ToString() + ")";
                    Chart3.ChartAreas[0].AxisY.Title = "Number of Contracts";

                    //  Chart3.Legends[0].Enabled = true;
                    Chart3.Series[0].Points[0].Color = System.Drawing.Color.GreenYellow;
                    Chart3.Series[0].Points[1].Color = System.Drawing.Color.Yellow;
                    Chart3.Series[0].Points[2].Color = System.Drawing.Color.SkyBlue;
                    Chart3.Series[0].Points[3].Color = System.Drawing.Color.OrangeRed;
                    Chart3.Series[0].Points[4].Color = System.Drawing.Color.GreenYellow;
                    Chart3.Series[0].Points[5].Color = System.Drawing.Color.Yellow;
                    Chart3.Series[0].Points[6].Color = System.Drawing.Color.SkyBlue;
                    Chart3.Series[0].Points[7].Color = System.Drawing.Color.OrangeRed;
                    Chart3.Series[0].Points[8].Color = System.Drawing.Color.GreenYellow;
                    Chart3.Series[0].Points[9].Color = System.Drawing.Color.Yellow;
                    Chart3.Series[0].Points[10].Color = System.Drawing.Color.SkyBlue;
                    Chart3.Series[0].Points[11].Color = System.Drawing.Color.OrangeRed;

                }
                catch (Exception el)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('Unable to fetch details due to Network issue.');", true);
                }
                finally
                {
                    cnn.Close();
                }
            }
        }


        protected void Chart2_Load(object sender, EventArgs e)
        {

            if (ddl_fin_yr.SelectedItem.ToString() != "Select Financial Year")
            {
                SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);

                DataTable dataTable = new DataTable();
                Chart2.Palette = System.Web.UI.DataVisualization.Charting.ChartColorPalette.Pastel;
                cnn.Open();
                try
                {

                    ArrayList listDataSource = new ArrayList();
                    Chart2.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
                    Chart2.ChartAreas[0].AxisY.MajorGrid.LineWidth = 0;



                    Chart2.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
                    Chart2.ChartAreas[0].AxisY.MinorGrid.Enabled = false;
                    Chart2.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                    Chart2.ChartAreas[0].AxisX.MinorGrid.Enabled = false;

                    List<double> yValues = new List<double>();
                    List<string> xValues = new List<string>();

                  
                    xValues.Add("APR");
                    xValues.Add("MAY");
                    xValues.Add("JUN");
                    xValues.Add("JUL");
                    xValues.Add("AUG");
                    xValues.Add("SEP");
                    xValues.Add("OCT");
                    xValues.Add("NOV");
                    xValues.Add("DEC");
                    xValues.Add("JAN");
                    xValues.Add("FEB");
                    xValues.Add("MAR");

                    for (int i = 0; i < 12; i++)
                    {
                        yValues.Add(0);
                    }
                    string[] yr = ddl_fin_yr.SelectedItem.ToString().Split('-');
                    string sqlcommand1 = "select  SUM(CONVERT(float, Invoice_amt)) as Invoice_Amt,MONTH(CONVERT(date, Transaction_Date)) Month from LCBD_DATA where CONVERT(date, Transaction_Date)>'" + yr[0] + "-04-01' and CONVERT(date, Transaction_Date)<'" + yr[1] + "-03-31' group by MONTH(CONVERT(date, Transaction_Date))";


                    SqlCommand sqlCommand = new SqlCommand(sqlcommand1, cnn);

                    sqlCommand.CommandTimeout = 900;


                    SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                    da.Fill(dataTable);
                    da.Dispose();

                    if (dataTable.Rows.Count > 0)
                    {
                        for (int j = 0; j < dataTable.Rows.Count; j++)
                        {
                            int p = Convert.ToInt32(dataTable.Rows[j]["Month"].ToString());
                            p = p - 4;
                            if(p<0)
                            {
                                p = p + 12;
                            }
                          
                            yValues[p] = Convert.ToDouble(dataTable.Rows[j]["Invoice_Amt"].ToString());
                        }

                    }

                    Chart2.Series[0].Points.DataBindXY(xValues, yValues);
                    Chart2.Series[0].IsValueShownAsLabel = true;

                    Chart2.ChartAreas[0].AxisX.Title = "LCBD Business (For Year :"+ ddl_fin_yr.SelectedItem.ToString() + ")";
                    Chart2.ChartAreas[0].AxisY.Title = "Value";

                    //  Chart2.Legends[0].Enabled = true;
                    Chart2.Series[0].Points[0].Color = System.Drawing.Color.GreenYellow;
                    Chart2.Series[0].Points[1].Color = System.Drawing.Color.Yellow;
                    Chart2.Series[0].Points[2].Color = System.Drawing.Color.Orchid;
                    Chart2.Series[0].Points[3].Color = System.Drawing.Color.OrangeRed;
                    Chart2.Series[0].Points[4].Color = System.Drawing.Color.Cyan;
                    Chart2.Series[0].Points[5].Color = System.Drawing.Color.LimeGreen;
                    Chart2.Series[0].Points[6].Color = System.Drawing.Color.Magenta;
                    Chart2.Series[0].Points[7].Color = System.Drawing.Color.Maroon;
                    Chart2.Series[0].Points[8].Color = System.Drawing.Color.SkyBlue;
                    Chart2.Series[0].Points[9].Color = System.Drawing.Color.Peru;
                    Chart2.Series[0].Points[10].Color = System.Drawing.Color.Salmon;
                    Chart2.Series[0].Points[11].Color = System.Drawing.Color.RoyalBlue;

                }
                catch (Exception el)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('Unable to fetch details due to Network issue.');", true);
                }
                finally
                {
                    cnn.Close();
                }
            }
        }
        protected void button_change_act(object sender, EventArgs e)
        {

            btn_general.Attributes["class"] = "top_btn_active";
            btn_reports.Attributes["class"] = "top_btn";
            btn_pending_lc.Attributes["class"] = "top_btn";
            pending_lc_div.Visible = false;
            main_dr_div.Visible = true;
            report_div.Visible = false;
            lcbd_submit.Visible = true; 
        }

        protected void get_contract_details()
        {
            string querystring = "select top 15 RRN,Transaction_Date,Maturaty_Date,Customer_Name,LC_RRN,Invoice_number,Invoice_amt from LCBD_DATA order by LCBD_ID desc";
            DataTable dt = get_normal_data(querystring);
            string query_build = "<table class='tbl_con_hold'><tr><td class='th_css' style='width:5%;'>NO</td><td class='th_css' style='width:15%;'>LCBD RRN</td><td class='th_css' style='width:10%;'>Transaction Date</td><td class='th_css' style='width:10%;'>Maturity Date</td>  <td class='th_css' style='width:15%;'>Name</td> <td class='th_css' style='width:15%;'>LC RRN</td> <td class='th_css' style='width:10%;'>Invoice Number</td> <td class='th_css' style='width:10%;'>Invoice Amount</td></tr>";
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    query_build = query_build + "<tr><td class='td_css' style='width:5%;'>"+(i+1)+"</td><td class='td_css' style='width:15%;'>"+dt.Rows[i]["RRN"].ToString() + "</td><td class='td_css' style='width:10%;'>" + dt.Rows[i]["Transaction_Date"].ToString() + "</td><td class='td_css' style='width:10%;'>" + dt.Rows[i]["Maturaty_Date"].ToString() + "</td>  <td class='td_css' style='width:15%;'>" + dt.Rows[i]["Customer_Name"].ToString() + "</td> <td class='td_css' style='width:15%;'>" + dt.Rows[i]["LC_RRN"].ToString() + "</td> <td class='td_css' style='width:10%;'>" + dt.Rows[i]["Invoice_number"].ToString() + "</td> <td class='td_css' style='width:10%;'>" + dt.Rows[i]["Invoice_amt"].ToString() + "</td></tr>";

                }
            }
            query_build = query_build + "</table>";
            tbl_data_bind.InnerHtml = query_build;

        }

        protected void button_change_act2(object sender, EventArgs e)
        {

            btn_general.Attributes["class"] = "top_btn";
            btn_reports.Attributes["class"] = "top_btn_active";
            btn_pending_lc.Attributes["class"] = "top_btn";
            main_dr_div.Visible = false;
            report_div.Visible = true;
            get_contract_details();
            lcbd_submit.Visible = false;
            pending_lc_div.Visible = false;

        }

        protected void refine_lc_data(object sender, EventArgs e)
        {
            string querystring = "delete LC_RFMS_PENDING_LCBD where LC_Stat='0' or LC_Stat='1'";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);         

            try
            {
                con.Open();
                SqlCommand cmd2 = new SqlCommand(querystring, con);
                cmd2.ExecuteNonQuery();
                con.Close();
            }
            catch(Exception tr)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('Some Error Occured!!!');", true);
            }
            finally
            {
                con.Close();
            }

            querystring = "select distinct LC_RRN from LCBD_DATA";
            DataTable dtx = get_normal_data(querystring);
            //lc_data_refine.Attributes.Remove("refine_btn");
            lc_data_refine.Attributes.Add("class", "refine_btn_1");
            lc_data_refine.Disabled = true;
            for (int i=0;i<dtx.Rows.Count;i++)
            {
                string LC_RRN = dtx.Rows[i]["LC_RRN"].ToString().Trim();
                string chk=get_info("select count(*) as LC_Count from LC_RFMS_PENDING_LCBD where LC_RRN='" + LC_RRN + "' and  LC_Stat in ('1','2')");
                double wid = (i / (dtx.Rows.Count-1)) * 100;
                progress_div_1.Style.Add("width", Convert.ToInt32(wid).ToString() + "%");
                System.Threading.Thread.Sleep(10);
                if (Convert.ToInt32(chk) == 0)
                {
                    string LC_Stat = "0";
                    querystring = "select SENADDR,F46A1,F591,F44E1,F44F1,F44C1,F32B1,F39A1,F31D1,F31C1,F41A1,F44A1,F44B1,F43P1,F43T1,F44D1,F45A1,F501,msgtype,F31E1,F34B1 from LCMESSAGEHIST where F201='" + dtx.Rows[i]["LC_RRN"].ToString().Trim() + "' and (msgtype='700' or msgtype='707') order by orgindate asc";
                    DataTable dt = get_oracle_data(querystring);
                    string SENADDR = "", F46A1 = "", F591="", F44E1 = "", F44F1 = "", F44C1 = "", F32B1 = "", F39A1 = "", F31D1 = "", F31C1 = "", F41A1 = "", F44A1 = "", F44B1 = "", F43P1 = "", F43T1 = "", F44D1 = "", F45A1 = "", F501 = "";
                    if (dt.Rows.Count > 0)
                    {
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            LC_Stat = "1";
                            if (dt.Rows[j]["SENADDR"].ToString().Trim() != "")
                            {
                                SENADDR = dt.Rows[j]["SENADDR"].ToString().Trim();
                            }
                            if (dt.Rows[j]["F591"].ToString().Trim() != "")
                            {
                                F591 = dt.Rows[j]["F591"].ToString().Trim();
                            }
                            if (dt.Rows[j]["F46A1"].ToString().Trim() != "")
                            {
                                F46A1 = dt.Rows[j]["F46A1"].ToString().Trim();
                            }
                            if (dt.Rows[j]["F44E1"].ToString().Trim() != "")
                            {
                                F44E1 = dt.Rows[j]["F44E1"].ToString().Trim();
                            }
                            if (dt.Rows[j]["F44F1"].ToString().Trim() != "")
                            {
                                F44F1 = dt.Rows[j]["F44F1"].ToString().Trim();
                            }
                            if (dt.Rows[j]["F44C1"].ToString().Trim() != "")
                            {
                                F44C1 = dt.Rows[j]["F44C1"].ToString().Trim();
                            }
                            if (dt.Rows[j]["F32B1"].ToString().Trim() != "")
                            {
                                F32B1 = dt.Rows[j]["F32B1"].ToString().Trim();
                            }
                            /*Amendment Field for Amount*/
                            if (dt.Rows[j]["F34B1"].ToString().Trim() != "")
                            {
                                F32B1 = dt.Rows[j]["F34B1"].ToString().Trim();
                            }
                            if (dt.Rows[j]["F39A1"].ToString().Trim() != "")
                            {
                                F39A1 = dt.Rows[j]["F39A1"].ToString().Trim();
                            }
                            if (dt.Rows[j]["F31D1"].ToString().Trim() != "")
                            {
                                F31D1 = dt.Rows[j]["F31D1"].ToString().Trim();
                            }
                            /*Amendment Field for Expiry date*/
                            if (dt.Rows[j]["F31E1"].ToString().Trim() != "")
                            {
                                if(F31D1!="")
                                {
                                    F31D1 = dt.Rows[j]["F31E1"].ToString().Trim() + F31D1.Substring(8);
                                }
                                else
                                {
                                    F31D1 = dt.Rows[j]["F31E1"].ToString().Trim();
                                }
                               
                            }
                            if (dt.Rows[j]["F31C1"].ToString().Trim() != "")
                            {
                                F31C1 = dt.Rows[j]["F31C1"].ToString().Trim();
                            }
                            if (dt.Rows[j]["F41A1"].ToString().Trim() != "")
                            {
                                F41A1 = dt.Rows[j]["F41A1"].ToString().Trim();
                            }
                            if (dt.Rows[j]["F44A1"].ToString().Trim() != "")
                            {
                                F44A1 = dt.Rows[j]["F44A1"].ToString().Trim();
                            }
                            if (dt.Rows[j]["F44B1"].ToString().Trim() != "")
                            {
                                F44B1 = dt.Rows[j]["F44B1"].ToString().Trim();
                            }
                            if (dt.Rows[j]["F43P1"].ToString().Trim() != "")
                            {
                                F43P1 = dt.Rows[j]["F43P1"].ToString().Trim();
                            }
                            if (dt.Rows[j]["F43T1"].ToString().Trim() != "")
                            {
                                F43T1 = dt.Rows[j]["F43T1"].ToString().Trim();
                            }
                            if (dt.Rows[j]["F44D1"].ToString().Trim() != "")
                            {
                                F44D1 = dt.Rows[j]["F44D1"].ToString().Trim();
                            }
                            if (dt.Rows[j]["F45A1"].ToString().Trim() != "")
                            {
                                F45A1 = dt.Rows[j]["F45A1"].ToString().Trim();
                            }
                            if (dt.Rows[j]["F501"].ToString().Trim() != "")
                            {
                                F501 = dt.Rows[j]["F501"].ToString().Trim();
                            }
                        }
                    }



                    //LC_RRN,LC_Stat,SENADDR,F46A1,F44E1,F44F1,F44C1,F32B1,F39A1,F31D1,F31C1,F41A1,F44A1,F44B1,F43P1,F43T1,F44D1,F45A1,F501



                  
                    try
                    {
                        con.Open();

                        string query1 = "INSERT INTO LC_RFMS_PENDING_LCBD " +
                           "(LC_RRN,LC_Stat,SENADDR,F591,F46A1,F44E1,F44F1,F44C1,F32B1,F39A1,F31D1,F31C1,F41A1,F44A1,F44B1,F43P1,F43T1,F44D1,F45A1,F501) " +
                                "VALUES " +
                            " (@LC_RRN,@LC_Stat,@SENADDR,@F591,@F46A1,@F44E1,@F44F1,@F44C1,@F32B1,@F39A1,@F31D1,@F31C1,@F41A1,@F44A1,@F44B1,@F43P1,@F43T1,@F44D1,@F45A1,@F501)";

                        SqlCommand cmd1 = new SqlCommand(query1, con);
                        string subtime = Convert.ToString(System.DateTime.Now);
                        cmd1.Parameters.AddWithValue("@LC_RRN", LC_RRN);
                        cmd1.Parameters.AddWithValue("@LC_Stat", LC_Stat);
                        cmd1.Parameters.AddWithValue("@SENADDR", SENADDR);
                        cmd1.Parameters.AddWithValue("@F591", F591);
                        cmd1.Parameters.AddWithValue("@F46A1", F46A1);
                        cmd1.Parameters.AddWithValue("@F44E1", F44E1);
                        cmd1.Parameters.AddWithValue("@F44F1", F44F1);

                        cmd1.Parameters.AddWithValue("@F44C1", F44C1);
                        cmd1.Parameters.AddWithValue("@F32B1", F32B1);
                        cmd1.Parameters.AddWithValue("@F39A1", F39A1);
                        cmd1.Parameters.AddWithValue("@F31D1", F31D1);
                        cmd1.Parameters.AddWithValue("@F31C1", F31C1);
                        cmd1.Parameters.AddWithValue("@F41A1", F41A1);

                        cmd1.Parameters.AddWithValue("@F44A1", F44A1);
                        cmd1.Parameters.AddWithValue("@F44B1", F44B1);
                        cmd1.Parameters.AddWithValue("@F43P1", F43P1);
                        cmd1.Parameters.AddWithValue("@F43T1", F43T1);
                        cmd1.Parameters.AddWithValue("@F44D1", F44D1);
                        cmd1.Parameters.AddWithValue("@F45A1", F45A1);

                        cmd1.Parameters.AddWithValue("@F501", F501);
                        cmd1.ExecuteNonQuery();
                        cmd1.Dispose();
                        // msg = "LCBD Record added successfully!!";
                        con.Close();
                    }
                    catch(Exception t)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('Some Error Occured!!!');", true);
                    }
                    finally
                    {
                        con.Close();
                    }

                }
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('Done!!!');", true);
            lc_data_refine.Attributes.Add("class", "refine_btn");
            lc_data_refine.Disabled = false;
        }
        public void check_change_fn(object sender, EventArgs e)
        {            
            if (chk_rec.Checked == true)
            {
                lbl_new_rd.CssClass = "lbl_head_slave";
                lbl_old_rec.CssClass = "lbl_head_master";
                div_EM.Visible = false;
                div_ED.Visible = true;
                load_pending_LC();
            }
            else
            {
                lbl_old_rec.CssClass = "lbl_head_slave";
                lbl_new_rd.CssClass = "lbl_head_master";
                div_EM.Visible = true;
                div_ED.Visible = false;
            }

        }

        protected void load_pending_LC()
        {          
            try
            {                
                string com = "select LC_RRN,ROW_NUMBER() OVER(ORDER BY LC_RRN DESC) AS LCBD_Id from LC_RFMS_PENDING_LCBD where LC_Stat=0";
                DataTable dt = get_normal_data(com);
                ddl_pending_lc.Items.Clear();
                ddl_pending_lc.DataSource = dt;
                ddl_pending_lc.DataBind();
                ddl_pending_lc.DataTextField = "LC_RRN";
                ddl_pending_lc.DataValueField = "LCBD_Id";
                ddl_pending_lc.DataBind();
                ddl_pending_lc.Items.Insert(0, new ListItem("Select LC RRN", "0"));
                //ddlcustomer.Items.Insert(0, new ListItem("LCBD 1", "1"));


               

            }
            catch (Exception el)
            {
                //lblmsg.Text = "Problem In Network, Unable to update";
            }
          
        }

        protected void add_lc_data(object sender, EventArgs e)
        {
            string LC_CUR = lc_currency.Value;
            string LC_ISS_BNK = lc_issue_bnk_2.Value;
            string LC_DT_ISS = lc_dt_iss_2.Text;
            string LC_APP = lc_applicant_2.Value;
            string LC_BENI_2 = lc_beni_2.Value;
            string LC_PLACE_EXP = lc_place_exp_2.Value;
            string LC_DT_EXP = dt_exp_2.Text;
            string LC_AMT = lc_amt_2.Value;
            string LC_SHIP_DT = dt_ship_lat_2.Text;
            string LC_POS = pos_tol.Value;
            string LC_NEG = neg_tol.Value;
            string LC_AVA = lc_avail_with.Value;
            string LC_PLC_DIS = lc_pl_dis.Value;
            string LC_PLC_DEL = lc_pl_del.Value;
            string LC_PAR_SHIP = "ALLOWED";
            if (chk_par_ship.Checked == true)
            {
                LC_PAR_SHIP = "ALLOWED";
            }
            else
            {
                LC_PAR_SHIP = "NOT ALLOWED";
            }
            string LC_Trans_ship = "ALLOWED";
            if (tra_ship_2.Checked == true)
            {
                LC_Trans_ship = "ALLOWED";
            }
            else
            {
                LC_Trans_ship = "NOT ALLOWED";
            }
            string ship_peri = ship_perio_2.Value;
            string goods_desc = goods_serv_txt.Value;
            string port_load = port_load_txt.Value;
            string port_dis = port_dis_txt.Value;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            string msg = "";
            submit_lc_chk.Disabled = true;
            try
            {
                //    if (submit_lc_chk.InnerText == "ADD")
                //    {
                //        if (ddl_pending_lc.SelectedItem.Text.ToString().Trim() != "Select LC RRN")
                //        {
                //            if (LC_ISS_BNK != "")
                //            {

                //                DateTime dx = System.DateTime.Now;
                //                string query1 = "INSERT INTO LC_RFMS_PENDING_LCBD " +
                //"(LC_RRN ,LC_Stat,SENADDR,F591,F46A1,F44E1,F44F1,F44C1,F32B1,F39A1,F31D1,F31C1,F41A1,F44A1,F44B1,F43P1,F43T1,F44D1,F45A1,F501) " +
                //"VALUES " +
                //" (@LC_RRN,@LC_Stat,@SENADDR,@F591,@F46A1,@F44E1,@F44F1,@F44C1,@F32B1,@F39A1,@F31D1,@F31C1,@F41A1,@F44A1,@F44B1,@F43P1,@F43T1,@F44D1,@F45A1,@F501)";

                //                SqlCommand cmd1 = new SqlCommand(query1, con);
                //                string subtime = Convert.ToString(System.DateTime.Now);


                //                cmd1.Parameters.AddWithValue("@LC_RRN", ddl_pending_lc.SelectedItem.Text.ToString().Trim());
                //                cmd1.Parameters.AddWithValue("@LC_Stat", '1');
                //                cmd1.Parameters.AddWithValue("@SENADDR", LC_ISS_BNK);
                //                cmd1.Parameters.AddWithValue("@F591", LC_BENI_2);

                //                cmd1.Parameters.AddWithValue("@F46A1", "");
                //                cmd1.Parameters.AddWithValue("@F44E1", port_load);
                //                cmd1.Parameters.AddWithValue("@F44F1", port_dis);
                //                cmd1.Parameters.AddWithValue("@F44C1", LC_SHIP_DT);



                //                cmd1.Parameters.AddWithValue("@F32B1", LC_CUR + LC_AMT);
                //                cmd1.Parameters.AddWithValue("@F39A1", LC_POS + "/" + LC_NEG);
                //                cmd1.Parameters.AddWithValue("@F31D1", LC_DT_EXP + LC_PLACE_EXP);
                //                cmd1.Parameters.AddWithValue("@F31C1", LC_DT_ISS);

                //                cmd1.Parameters.AddWithValue("@F41A1", LC_AVA);
                //                cmd1.Parameters.AddWithValue("@F44A1", LC_PLC_DIS);
                //                cmd1.Parameters.AddWithValue("@F44B1", LC_PLC_DEL);
                //                cmd1.Parameters.AddWithValue("@F43P1", LC_PAR_SHIP);

                //                cmd1.Parameters.AddWithValue("@F43T1", LC_Trans_ship);
                //                cmd1.Parameters.AddWithValue("@F44D1", ship_peri);
                //                cmd1.Parameters.AddWithValue("@F45A1", goods_desc);
                //                        cmd1.Parameters.AddWithValue("@F501", LC_APP);

                //                con.Open();

                //                cmd1.ExecuteNonQuery();
                //                cmd1.Dispose();
                //                con.Close();
                //                msg = "Please give LC Issueing Bank!!";
                //            }
                //            else
                //            {
                //                msg = "Please select LC Reference number!!";
                //            }
                //        }
                //        else
                //        {
                //            msg = "Please select LC Reference number!!";
                //        }
                //    }
                //    else
                //    {

                if (hdn_lc_rrn.Value == "")
                {
                    hdn_lc_rrn.Value=ddl_pending_lc.SelectedItem.Text.ToString().Trim();
                }
                if (hdn_lc_rrn.Value != "Select LC RRN")
                {
                    DateTime dx = System.DateTime.Now;

                    string query1 = "update LC_RFMS_PENDING_LCBD " +
    " set  LC_Stat=@LC_Stat,SENADDR=@SENADDR,F591=@F591,F46A1=@F46A1,F44E1=@F44E1,F44F1=@F44F1,F44C1=@F44C1,F32B1=@F32B1,F39A1=@F39A1,F31D1=@F31D1,F31C1=@F31C1,F41A1=@F41A1,F44A1=@F44A1,F44B1=@F44B1,F43P1=@F43P1,F43T1=@F43T1,F44D1=@F44D1,F45A1=@F45A1,F501=@F501 where LC_RRN='" + hdn_lc_rrn.Value + "'";
                    SqlCommand cmd1 = new SqlCommand(query1, con);
                    string subtime = Convert.ToString(System.DateTime.Now);

                    cmd1.Parameters.AddWithValue("@LC_Stat", '2');
                    cmd1.Parameters.AddWithValue("@SENADDR", LC_ISS_BNK);
                    cmd1.Parameters.AddWithValue("@F591", LC_BENI_2);

                    cmd1.Parameters.AddWithValue("@F46A1", "");
                    cmd1.Parameters.AddWithValue("@F44E1", port_load);
                    cmd1.Parameters.AddWithValue("@F44F1", port_dis);
                    cmd1.Parameters.AddWithValue("@F44C1", LC_SHIP_DT.Replace("-", ""));

                    cmd1.Parameters.AddWithValue("@F32B1", LC_CUR + LC_AMT);
                    cmd1.Parameters.AddWithValue("@F39A1", LC_POS + "/" + LC_NEG);
                    cmd1.Parameters.AddWithValue("@F31D1", LC_DT_EXP.Replace("-","") + LC_PLACE_EXP);
                    cmd1.Parameters.AddWithValue("@F31C1", LC_DT_ISS.Replace("-", ""));

                    cmd1.Parameters.AddWithValue("@F41A1", LC_AVA);
                    cmd1.Parameters.AddWithValue("@F44A1", LC_PLC_DIS);
                    cmd1.Parameters.AddWithValue("@F44B1", LC_PLC_DEL);
                    cmd1.Parameters.AddWithValue("@F43P1", LC_PAR_SHIP);

                    cmd1.Parameters.AddWithValue("@F43T1", LC_Trans_ship);
                    cmd1.Parameters.AddWithValue("@F44D1", ship_peri);
                    cmd1.Parameters.AddWithValue("@F45A1", goods_desc);
                    cmd1.Parameters.AddWithValue("@F501", LC_APP);
                    con.Open();
                    cmd1.ExecuteNonQuery();
                    cmd1.Dispose();
                    con.Close();
                    msg = "LC Record updated successfully!!";
                    hdn_lc_rrn.Value = "";
                    //}

                }
                else
                {
                    msg = "Please give proper LC RRN!!";
                }
            }
            catch (Exception ep)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('Unable to create record because of some Exceptions!!!');", true);
            }
            finally
            {
                con.Close();
            }




            //reset_all();
            reset_fields();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('" + msg + "');", true);
            submit_lc_chk.Disabled = false;
        }
        //                            else
        //                            {
        //                                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('Issues in the Mandatory Inputs');", true);
        //lcbd_submit.Disabled = false;
        //}


        protected void reset_fields()
        {
            lc_issue_bnk_2.Value = "";
            lc_dt_iss_2.Text = "";
            lc_applicant_2.Value = "";
            lc_beni_2.Value = "";
            lc_place_exp_2.Value = "";
            dt_exp_2.Text = "";
            lc_amt_2.Value = "";
            dt_ship_lat_2.Text = "";
            pos_tol.Value = "0";
            neg_tol.Value = "0";
            lc_avail_with.Value = "";
            lc_pl_dis.Value = "";
            lc_pl_del.Value = "";
            chk_par_ship.Checked = false;
            submit_lc_chk.InnerText = "ADD";
            tra_ship_2.Checked = false;
            ship_perio_2.Value = "";
            goods_serv_txt.Value = "";
            port_load_txt.Value = "";
            port_dis_txt.Value = "";
            load_pending_LC();
        }

        protected void button_change_act3(object sender, EventArgs e)
        {
            btn_pending_lc.Attributes["class"] = "top_btn_active";
            btn_general.Attributes["class"] = "top_btn";
            btn_reports.Attributes["class"] = "top_btn";
            main_dr_div.Visible = false;
            report_div.Visible = false;         
            lcbd_submit.Visible = false;
            pending_lc_div.Visible = true;
            load_pending_LC();
        }

        public void get_lcbc_rrn_data(object sender, EventArgs e)
        {
            try
            {
                string lcbd_rrn = txt_rrn.Value.Trim();
                reset_all();
                get_lcbd_data.Disabled = true;
                if (lcbd_rrn != "")
                {
                    string querystring = "select * from LCBD_DATA where RRN='" + lcbd_rrn + "'";
                    DataTable dt = get_normal_data(querystring);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            lcbd_submit.InnerText = "Edit";
                            lbl_id_hdn.InnerText = dt_tra.Text = dt.Rows[i]["LCBD_ID"].ToString();
                            if (dt.Rows[i]["Transaction_Date"].ToString().Trim() != "")
                            {
                                dt_tra.Text = dt.Rows[i]["Transaction_Date"].ToString();
                            }
                            if (dt.Rows[i]["Maturaty_Date"].ToString().Trim() != "")
                            {
                                dt_matu.Text = dt.Rows[i]["Maturaty_Date"].ToString();
                            }
                            if (dt.Rows[i]["Customer_Name"].ToString() != "")
                            {
                                //ddlcustomer.SelectedItem.Text = dt.Rows[i]["Customer_Name"].ToString().Trim();
                                //ddlcustomer.Items.FindByText(dt.Rows[i]["Customer_Name"].ToString().Trim()).Selected = true;
                                //string po = dt.Rows[i]["Customer_Name"].ToString().Trim();
                                ddlcustomer.SelectedIndex = ddlcustomer.Items.IndexOf(ddlcustomer.Items.FindByText(dt.Rows[i]["Customer_Name"].ToString()));

                                // ddl_namechange_react(sender, e);
                                new_cut_div.Visible = false;

                            }
                            if (dt.Rows[i]["Customer_Id"].ToString() != "")
                            {
                                Cust_num.Value = dt.Rows[i]["Customer_Id"].ToString();                                
                            }
                            if (dt.Rows[i]["Acc_Num"].ToString() != "")
                            {
                                Acc_Num.Value = dt.Rows[i]["Acc_Num"].ToString();
                            }
                            if (dt.Rows[i]["Business_Nature"].ToString() != "")
                            {
                                natu_busi_act.Value = dt.Rows[i]["Business_Nature"].ToString();
                            }
                            if (dt.Rows[i]["LC_RRN"].ToString() != "")
                            {
                                LC_rrn.Value = dt.Rows[i]["LC_RRN"].ToString();
                                get_lc_data(sender, e);
                            }
                            if (dt.Rows[i]["NOC_BNK"].ToString() != "")
                            {
                                if (dt.Rows[i]["NOC_BNK"].ToString() == "Y")
                                {
                                    CHK_Noc.Checked = true;
                                }
                            }
                            if (dt.Rows[i]["LC_res_any_bnk"].ToString() != "")
                            {
                                if (dt.Rows[i]["LC_res_any_bnk"].ToString() == "Y")
                                {
                                    LC_Res.Checked = true;
                                }
                            }
                            if (dt.Rows[i]["amt_dis_disc"].ToString() != "")
                            {
                                amt_dis_dis.Value = dt.Rows[i]["amt_dis_disc"].ToString().Trim();
                            }
                            if (dt.Rows[i]["due_date_rec_disc"].ToString().Trim() != "")
                            {
                                Due_dt_res.Text = dt.Rows[i]["due_date_rec_disc"].ToString().Trim();
                            }
                            if (dt.Rows[i]["Invoice_number"].ToString() != "")
                            {
                                txt_inv.Value = dt.Rows[i]["Invoice_number"].ToString().Trim();
                            }
                            if (dt.Rows[i]["Invoice_amt"].ToString() != "")
                            {
                                txt_amt_bill.Value = dt.Rows[i]["Invoice_amt"].ToString().Trim();
                            }
                            if (dt.Rows[i]["funds_rec_issu_bnk"].ToString() != "")
                            {
                                if (dt.Rows[i]["funds_rec_issu_bnk"].ToString() == "Y")
                                {
                                    chk_funds.Checked = true;
                                }
                            }
                            if (dt.Rows[i]["wether_outstd_31"].ToString() != "")
                            {
                                if (dt.Rows[i]["wether_outstd_31"].ToString() == "Y")
                                {
                                    chk_outsta.Checked = true;
                                }
                            }
                            if (dt.Rows[i]["agent_name"].ToString() != "")
                            {
                                agent_name.Value = dt.Rows[i]["agent_name"].ToString().Trim();
                            }
                            if (dt.Rows[i]["latest_nego_date"].ToString().Trim() != "")
                            {
                                lt_nego_dt.Text = dt.Rows[i]["latest_nego_date"].ToString().Trim();
                            }
                            if (dt.Rows[i]["payment_date"].ToString().Trim() != "")
                            {
                                pay_dt_lc_dt.Text = dt.Rows[i]["payment_date"].ToString().Trim();
                            }
                            if (dt.Rows[i]["no_of_D"].ToString() != "")
                            {
                                num_dys.Value = dt.Rows[i]["no_of_D"].ToString().Trim();
                            }
                            if (dt.Rows[i]["rate_of_int"].ToString() != "")
                            {
                                rate_int.Value = dt.Rows[i]["rate_of_int"].ToString().Trim();
                            }
                            if (dt.Rows[i]["rate_of_mar"].ToString() != "")
                            {
                                rate_mar.Value = dt.Rows[i]["rate_of_mar"].ToString().Trim();
                            }
                            if (dt.Rows[i]["days_margin"].ToString() != "")
                            {
                                no_dys_mar.Value = dt.Rows[i]["days_margin"].ToString().Trim();
                            }
                            if (dt.Rows[i]["LC_Mar_days"].ToString() != "")
                            {
                                mar_days.Value = dt.Rows[i]["LC_Mar_days"].ToString().Trim();
                            }
                            if (dt.Rows[i]["LC_Hand_CHG"].ToString() != "")
                            {
                                lc_hand_chg.Value = dt.Rows[i]["LC_Hand_CHG"].ToString().Trim();
                            }
                            if (dt.Rows[i]["LC_ADV_CHG"].ToString() != "")
                            {
                                lc_adv_chg.Value = dt.Rows[i]["LC_ADV_CHG"].ToString().Trim();
                            }
                            if (dt.Rows[i]["SFMS_Chg"].ToString() != "")
                            {
                                sfms_chg.Value = dt.Rows[i]["SFMS_Chg"].ToString().Trim();
                            }
                            if (dt.Rows[i]["Service_Chg"].ToString() != "")
                            {
                                serv_chg.Value = dt.Rows[i]["Service_Chg"].ToString().Trim();
                            }
                            if (dt.Rows[i]["Courier_Chg"].ToString() != "")
                            {
                                cour_chg.Value = dt.Rows[i]["Courier_Chg"].ToString().Trim();
                            }
                            if (dt.Rows[i]["Amount_rec_chg"].ToString() != "")
                            {
                                if (dt.Rows[i]["Amount_rec_chg"].ToString() == "Y")
                                {
                                    chk_amt_rec.Checked = true;
                                }
                            }
                            if (dt.Rows[i]["Actual_amt_rec"].ToString() != "")
                            {
                                txt_amt_rec.Value = dt.Rows[i]["Actual_amt_rec"].ToString();
                            }

                            if (dt.Rows[i]["Date_pay_rec"].ToString().Trim() != "")
                            {
                                dt_pay_rec.Text = dt.Rows[i]["Date_pay_rec"].ToString().Trim();
                            }
                            if (dt.Rows[i]["Excess_less"].ToString() != "")
                            {
                                excess_less_in.Value = dt.Rows[i]["Excess_less"].ToString().Trim();
                            }
                            if (dt.Rows[i]["delayed"].ToString() != "")
                            {
                                delayed_early.Value = dt.Rows[i]["delayed"].ToString().Trim();
                            }
                            if (dt.Rows[i]["delayed"].ToString() != "")
                            {
                                delayed_early.Value = dt.Rows[i]["delayed"].ToString().Trim();
                            }
                            if (dt.Rows[i]["overdue_int"].ToString() != "")
                            {
                                over_int.Value = dt.Rows[i]["overdue_int"].ToString().Trim();
                            }
                            if (dt.Rows[i]["int_rec_early_pay"].ToString() != "")
                            {
                                intrev.Value = dt.Rows[i]["int_rec_early_pay"].ToString().Trim();
                            }

                            //mar_rev_ben_chk
                            if (dt.Rows[i]["margin_rev_ben"].ToString() != "")
                            {
                                if (dt.Rows[i]["margin_rev_ben"].ToString() == "Y")
                                {
                                    mar_rev_ben_chk.Checked = true;
                                }
                            }

                            if (dt.Rows[i]["amt_paid_ben"].ToString().Trim() != "")
                            {
                                dt_amt_paid.Text = dt.Rows[i]["amt_paid_ben"].ToString().Trim();
                            }
                            if (dt.Rows[i]["Net_rev"].ToString() != "")
                            {
                                Net_rev.Value = dt.Rows[i]["Net_rev"].ToString().Trim();
                            }
                            if (dt.Rows[i]["Int_Pl_3903"].ToString() != "")
                            {
                                Int_Pl_3903.Value = dt.Rows[i]["Int_Pl_3903"].ToString().Trim();
                            }
                            /*New Addition on 7th Nov 2022*/
                            if (dt.Rows[i]["Latest_Ship_dt"].ToString().Trim() != "")
                            {
                                late_ship_date_new.Text = dt.Rows[i]["Latest_Ship_dt"].ToString();
                            }
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('No Such record found!!!');", true);
                    }
                }
                else
                {
                    lcbd_submit.InnerText = "ADD";
                    lbl_id_hdn.InnerText = dt_tra.Text = "";
                }
                get_lcbd_data.Disabled = false;
            }
            catch(Exception y)
            {
                Label1.InnerText = y.Message;
            }
        }

        protected void get_days(object sender, EventArgs e)
        {
            string date_matu = dt_matu.Text;
            string date_pay = pay_dt_lc_dt.Text;
            if(date_matu!="" && date_pay!="")
            {
                double result2 = Convert.ToDateTime(date_matu).Subtract(Convert.ToDateTime(date_pay)).TotalDays;
                num_dys.Value = result2.ToString();
                if (delayed_early.Value != "" && Int_Pl_3903.Value != "")
                {
                    if (Convert.ToInt32(delayed_early.Value) < 0)
                    {
                        double d = (Convert.ToDouble(Int_Pl_3903.Value) / Convert.ToDouble(num_dys.Value)) *(-Convert.ToDouble(delayed_early.Value));
                        intrev.Value = Math.Round(d).ToString();
                    }
                    else
                    {
                        intrev.Value = "0";
                    }
                    ScriptManager.RegisterStartupScript(this, GetType(), "Javascript", "Javascript:cal40();", true);
                }
                if (txt_amt_bill.Value != "" && rate_int.Value != "")
                {
                    double d = (Convert.ToDouble(txt_amt_bill.Value) * Convert.ToDouble(num_dys.Value) * Convert.ToDouble(rate_int.Value)) / 365;
                    Int_Pl_3903.Value = Math.Round((d/100)).ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "Javascript", "Javascript:cal3();", true);
                    //ScriptManager.RegisterStartupScript(GetType(), "Javascript", "javascript:FUNCTIONNAME(); ", true);
                    // ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:FUNCTIONNAME(); ", true);
                    //onblur="cal3();"
                }

            }
            string date_rec = dt_pay_rec.Text;
            string date_res = Due_dt_res.Text;
            if (date_rec != "" && date_res != "")
            {
                double result2 = Convert.ToDateTime(date_rec).Subtract(Convert.ToDateTime(date_res)).TotalDays;
                delayed_early.Value = result2.ToString();
                if(Convert.ToInt32(delayed_early.Value)<0 && num_dys.Value!="" && Int_Pl_3903.Value!="")
                {
                    double d = (Convert.ToDouble(Int_Pl_3903.Value) / Convert.ToDouble(num_dys.Value))*(- Convert.ToDouble(delayed_early.Value));
                    intrev.Value = Math.Round(d).ToString();
                }
                else
                {
                    intrev.Value = "0";
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "Javascript", "Javascript:cal40();", true);
            }
        }


        public void get_lc_data_2(object sender, EventArgs e)
        {
            submit_lc_chk.InnerText = "Edit";
            try
            {
                hdn_lc_rrn.Value = "";
                string lc_rrn = lc_rrn_num.Value.Trim();
                btn_lc_da_search.Disabled = true;
                if (lc_rrn != "")
                {
                    //string querystring = "select SENADDR,F46A1,F591,F44E1,F44F1,F44C1,F32B1,F39A1,F31D1,F31C1,F41A1,F44A1,F44B1,F43P1,F43T1,F44D1,F45A1,F501 from LCMESSAGEHIST where F201='" + lc_rrn + "' order by orgindate asc";
                    //DataTable dt = get_oracle_data(querystring);
                    string querystring = "select SENADDR,F46A1,F591,F44E1,F44F1,F44C1,F32B1,F39A1,F31D1,F31C1,F41A1,F44A1,F44B1,F43P1,F43T1,F44D1,F45A1,F501 from LC_RFMS_PENDING_LCBD where LC_RRN='" + lc_rrn + "'";
                    DataTable dt = get_normal_data(querystring);

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            hdn_lc_rrn.Value = lc_rrn;
                            submit_lc_chk.InnerText = "Edit";
                            if (dt.Rows[i]["SENADDR"].ToString() != "")
                            {
                                lc_issue_bnk_2.Value = dt.Rows[i]["SENADDR"].ToString();
                            }
                            if (dt.Rows[i]["F591"].ToString() != "")
                            {
                                lc_beni_2.Value = dt.Rows[i]["F591"].ToString();
                            }
                            if (dt.Rows[i]["F501"].ToString() != "")
                            {
                                lc_applicant_2.Value = dt.Rows[i]["F501"].ToString();
                            }
                            if (dt.Rows[i]["F31C1"].ToString().Trim() != "")
                            {
                                string gt = dt.Rows[i]["F31C1"].ToString();
                                gt = gt.Replace("-", "").Replace("/", "");
                                string po = gt.Substring(0, 4) + "-" + gt.Substring(4, 2) + "-" + gt.Substring(6, 2);//"yyyy-MM-dd"    .ToString("yyyy-MM-dd", new CultureInfo("en-GB"))

                                lc_dt_iss_2.Text = po;
                            }
                            if (dt.Rows[i]["F31D1"].ToString() != "")
                            {
                                lc_place_exp_2.Value = dt.Rows[i]["F31D1"].ToString().Substring(8);
                            }
                            if (dt.Rows[i]["F31D1"].ToString().Trim() != "")
                            {
                                //Convert.ToDateTime(this.txtFrom.Text, new CultureInfo("en-GB")
                                //string po = dt.Rows[i]["F44c1"].ToString().Substring(6, 2) + "-" + dt.Rows[i]["F44c1"].ToString().Substring(4, 2) + "-" + dt.Rows[i]["F44c1"].ToString().Substring(0, 4);//"yyyy-MM-dd"    .ToString("yyyy-MM-dd", new CultureInfo("en-GB"))
                                string gt = dt.Rows[i]["F31D1"].ToString();
                                gt = gt.Replace("-","").Replace("/", "");
                                string po = gt.Substring(0, 4) + "-" + gt.Substring(4, 2) + "-" + gt.Substring(6, 2);//"yyyy-MM-dd"    .ToString("yyyy-MM-dd", new CultureInfo("en-GB"))

                                //  ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('"+ po + "');" , true);

                                dt_exp_2.Text = po;
                            }
                            if (dt.Rows[i]["F32B1"].ToString() != "")
                            {
                                lc_amt_2.Value = dt.Rows[i]["F32B1"].ToString().Substring(3);
                            }
                            //if (dt.Rows[i]["F42C1"].ToString() != "")
                            //{
                            //    Tenor_txt.Value = dt.Rows[i]["F42C1"].ToString();
                            //}
                            if (dt.Rows[i]["F44C1"].ToString().Trim() != "")
                            {
                                string gt = dt.Rows[i]["F44C1"].ToString();
                                gt = gt.Replace("-", "").Replace("/", "");
                                string po = gt.Substring(0, 4) + "-" + gt.Substring(4, 2) + "-" + gt.Substring(6, 2);//"yyyy-MM-dd"    .ToString("yyyy-MM-dd", new CultureInfo("en-GB"))


                                dt_ship_lat_2.Text = po;
                            }
                            if (dt.Rows[i]["F39A1"].ToString() != "")
                            {
                                string[] p = dt.Rows[i]["F39A1"].ToString().Split('/');
                                if (p[0] != "")
                                {
                                    pos_tol.Value = p[0];
                                }
                                if (p[1] != "")
                                {
                                    neg_tol.Value = p[1];
                                }
                            }
                            if (dt.Rows[i]["F41A1"].ToString() != "")
                            {
                                lc_avail_with.Value = dt.Rows[i]["F41A1"].ToString();
                            }
                            if (dt.Rows[i]["F44A1"].ToString() != "")
                            {
                                lc_pl_dis.Value = dt.Rows[i]["F44A1"].ToString();
                            }
                            if (dt.Rows[i]["F44B1"].ToString() != "")
                            {
                                lc_pl_del.Value = dt.Rows[i]["F44B1"].ToString();
                            }
                            if (dt.Rows[i]["f43p1"].ToString() != "")
                            {
                                string uu = dt.Rows[i]["f43p1"].ToString();
                                if (uu.ToUpper() == "ALLOWED")
                                {
                                    chk_par_ship.Checked = true;
                                }
                                else
                                {
                                    chk_par_ship.Checked = false;
                                }
                            }
                            if (dt.Rows[i]["f43t1"].ToString() != "")
                            {
                                string uu = dt.Rows[i]["f43t1"].ToString();
                                if (uu.ToUpper() == "ALLOWED")
                                {
                                    tra_ship_2.Checked = true;
                                }
                                else
                                {
                                    tra_ship_2.Checked = false;
                                }
                            }
                            if (dt.Rows[i]["F44D1"].ToString() != "")
                            {
                                ship_perio_2.Value = dt.Rows[i]["F44D1"].ToString();
                            }//
                            if (dt.Rows[i]["F45A1"].ToString() != "")
                            {
                                goods_serv_txt.Value = dt.Rows[i]["F45A1"].ToString();
                            }
                            if (dt.Rows[i]["F44E1"].ToString() != "")
                            {
                                port_load_txt.Value = dt.Rows[i]["F44E1"].ToString();
                            }
                            if (dt.Rows[i]["F44F1"].ToString() != "")
                            {
                                port_dis_txt.Value = dt.Rows[i]["F44F1"].ToString();
                            }

                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('LC RRN Not available in DB!!!');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('Please give Proper LC Number !!!');", true);
                }
                btn_lc_da_search.Disabled = false;
            }
            catch (Exception y)
            {
                Label1.InnerText = y.Message;
            }
        }

        public void get_lc_data(object sender, EventArgs e)
        {
            try
            {
               
                string lc_rrn = LC_rrn.Value.Trim();
                btn_lc_chk.Disabled = true;
                if (lc_rrn != "")
                {
                    string querystring = "select SENADDR,F501,F591,F31D1,F32B1,F42C1,F44A1,F44B1,F41D1,f43p1,f43t1,F44c1 from LCMESSAGEHIST where F201='" + lc_rrn + "' order by orgindate asc";
                    DataTable dt = get_oracle_data(querystring);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["SENADDR"].ToString() != "")
                        {
                            LC_issue_bank.Value = dt.Rows[i]["SENADDR"].ToString();
                        }
                        if (dt.Rows[i]["F501"].ToString() != "")
                        {
                            LC_App.Value = dt.Rows[i]["F501"].ToString();
                        }
                        if (dt.Rows[i]["F591"].ToString() != "")
                        {
                            LC_Beni.Value = dt.Rows[i]["F591"].ToString();
                        }
                        if (dt.Rows[i]["F31D1"].ToString() != "")
                        {
                            Pl_of_exp.Value = dt.Rows[i]["F31D1"].ToString().Substring(8);
                        }
                        if (dt.Rows[i]["F32B1"].ToString() != "")
                        {
                            LC_Amt.Value = dt.Rows[i]["F32B1"].ToString();
                        }
                        if (dt.Rows[i]["F42C1"].ToString() != "")
                        {
                            Tenor_txt.Value = dt.Rows[i]["F42C1"].ToString();
                        }
                        if (dt.Rows[i]["F44A1"].ToString() != "")
                        {
                            loading_bd.Value = dt.Rows[i]["F44A1"].ToString();
                        }
                        if (dt.Rows[i]["F44B1"].ToString() != "")
                        {
                            trans_to.Value = dt.Rows[i]["F44B1"].ToString();
                        }
                        if (dt.Rows[i]["F41D1"].ToString() != "")
                        {
                            LC_rest_bnk.Value = dt.Rows[i]["F41D1"].ToString();
                        }
                        if (dt.Rows[i]["f43p1"].ToString() != "")
                        {
                            string uu = dt.Rows[i]["f43p1"].ToString();
                            if (uu.ToUpper() == "ALLOWED")
                            {
                                parti_ship.Checked = true;
                            }
                            else
                            {
                                parti_ship.Checked = false;
                            }
                        }
                        if (dt.Rows[i]["f43t1"].ToString() != "")
                        {
                            string uu = dt.Rows[i]["f43t1"].ToString();
                            if (uu.ToUpper() == "ALLOWED")
                            {
                                trans_ship.Checked = true;
                            }
                            else
                            {
                                trans_ship.Checked = false;
                            }
                        }
                        if (dt.Rows[i]["F44c1"].ToString().Trim() != "")
                        {
                            //Convert.ToDateTime(this.txtFrom.Text, new CultureInfo("en-GB")
                            //string po = dt.Rows[i]["F44c1"].ToString().Substring(6, 2) + "-" + dt.Rows[i]["F44c1"].ToString().Substring(4, 2) + "-" + dt.Rows[i]["F44c1"].ToString().Substring(0, 4);//"yyyy-MM-dd"    .ToString("yyyy-MM-dd", new CultureInfo("en-GB"))
                            string po = dt.Rows[i]["F44c1"].ToString().Substring(0, 4) + "-" + dt.Rows[i]["F44c1"].ToString().Substring(4, 2) + "-" + dt.Rows[i]["F44c1"].ToString().Substring(6, 2);//"yyyy-MM-dd"    .ToString("yyyy-MM-dd", new CultureInfo("en-GB"))

                            //  ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('"+ po + "');" , true);

                            ship_date.Text = po;
                        }


                    }

                }
                btn_lc_chk.Disabled = false;
            }
            catch (Exception y)
            {
                Label1.InnerText = y.Message;
            }
        }          


        public void ddl_namechange_react(object sender, EventArgs e)
        {
            if(ddlcustomer.SelectedItem.ToString()== "Add New Customer")
            {
                new_cut_div.Visible = true; 
            }
            else
            {
                new_cut_div.Visible = false;              
                string query = "select * from LCBD_Customer where LCBD_Cust_Name='" + ddlcustomer.SelectedItem.ToString() + "'";
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
                con.Open();
                try
                {
                    SqlDataAdapter adpt = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    adpt.Fill(dt);
                    Cust_num.Value = dt.Rows[0]["Customer_Number"].ToString();
                    Acc_Num.Value = dt.Rows[0]["Acc_Number"].ToString();
                    natu_busi_act.Value = dt.Rows[0]["Nature_Busi"].ToString();


                }
                catch (Exception el)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('Unable to fetch Customers details due to Network issue.');", true);
                }
                finally
                {
                    con.Close();
                }


            }
        }


        public void customer_details()
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            con.Open();
            try
            {
                //sessionlbl.Text = Session["Id"].ToString();
                string com = "select LCBD_ID,LCBD_Cust_Name from LCBD_Customer order by LCBD_Cust_Name asc";
                SqlDataAdapter adpt = new SqlDataAdapter(com, con);
                DataTable dt = new DataTable();
                adpt.Fill(dt);              
                ddlcustomer.Items.Clear();
                ddlcustomer.DataSource = dt;
                ddlcustomer.DataBind();
                ddlcustomer.DataTextField = "LCBD_Cust_Name";
                ddlcustomer.DataValueField = "LCBD_Id";
                ddlcustomer.DataBind();
                ddlcustomer.Items.Insert(0, new ListItem("Add New Customer", "0"));
                //ddlcustomer.Items.Insert(0, new ListItem("LCBD 1", "1"));

                ddlcustomer2.Items.Clear();
                ddlcustomer2.DataSource = dt;
                ddlcustomer2.DataBind();
                ddlcustomer2.DataTextField = "LCBD_Cust_Name";
                ddlcustomer2.DataValueField = "LCBD_Id";
                ddlcustomer2.DataBind();
                ddlcustomer2.Items.Insert(0, new ListItem("Add New Customer", "0"));

                DateTime sdt = System.DateTime.Now;
                //ddl_fin_yr
                int cyear = sdt.Year;
                ddl_fin_yr.Items.Clear();
                ddl_fin_yr.Items.Insert(0, new ListItem(cyear.ToString()+"-"+ (cyear+1).ToString(), "1"));
                ddl_fin_yr.Items.Insert(0, new ListItem((cyear-1).ToString() + "-" + (cyear).ToString(), "2"));
                ddl_fin_yr.Items.Insert(0, new ListItem((cyear - 2).ToString() + "-" + (cyear-1).ToString(), "3"));
                ddl_fin_yr.Items.Insert(0, new ListItem("Select Financial Year", "0"));


            }
            catch (Exception el)
            {
                //lblmsg.Text = "Problem In Network, Unable to update";
            }
            finally
            {
                con.Close();
            }

        }
    }
}