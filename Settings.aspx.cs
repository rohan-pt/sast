using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace BCCBExamPortal
{
    public partial class Settings : System.Web.UI.Page
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
                cmd1.Parameters.AddWithValue("@Page_Name", "Settings");
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

        protected void general_users(object sender, EventArgs e)
        {
            Response.Redirect("Active_Users.aspx");
        }
        protected void mini_ledge_clk(object sender, EventArgs e)
        {
            Response.Redirect("Short.aspx");
        }

        protected void EOD_Click(object sender, EventArgs e)
        {
            Response.Redirect("EOD_Admin.aspx");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                sessionlbl.Text = Session["Id"].ToString();
            }
            catch (Exception ep)
            {
                Response.Redirect("LogIn.aspx");
            }
            btnSettings.CssClass = "menubtn1";
            if (!IsPostBack)
            {
                audit_trails();
                Examlbl.Text = Request.QueryString["ENo"];
               // btnSettings.CssClass = "menubtn1";
                Fill_Admin();
            }
        }

        protected void web_Click(object sender, EventArgs e)
        {
            Response.Redirect("ConnectionString.aspx");
        }
        protected void Fill_Admin()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
       


            DataTable dt = new DataTable();
            con.Open();
            try
            {
               
                SqlCommand cmd1 = new SqlCommand("select * from Admin_LoginTbl where AD_Code=@username", con);
                cmd1.Parameters.AddWithValue("@username", sessionlbl.Text);
                SqlDataAdapter da = new SqlDataAdapter(cmd1);
                da.Fill(dt);
              
                if (dt.Rows.Count > 0)
                {
                    UserCode.Value = dt.Rows[0]["AD_Code"].ToString();
                    email.Value = dt.Rows[0]["Extra3"].ToString();
                    txtlocation.Value= dt.Rows[0]["Extra1"].ToString();
                    txtname.Value= dt.Rows[0]["Extra2"].ToString();
                }

                da.Dispose();
            }
            catch (Exception oo)
            {
                mainblk.InnerHtml = "Problem In network, your exam instance is saved.";
            }
            finally
            {
                con.Close();
            }
        }
        protected void NewTestbtn_Click(object sender, EventArgs e)
        {
            if (txtnewpass.Value == txtconpass.Value)
            {

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);


                DataTable dataTable = new DataTable();
                con.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand("select * from Admin_LoginTbl where AD_Code=@username and AD_Password=@password", con);

                    cmd.Parameters.AddWithValue("@username", Session["Id"].ToString());
                    cmd.Parameters.AddWithValue("@password", txtcurrpass.Value);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    // this will query your database and return the result to your datatable
                    da.Fill(dataTable);
                    da.Dispose();
                }
                catch (Exception oo)
                {
                    mainblk.InnerHtml = "Problem In network, your exam instance is saved.";
                }
                finally
                {
                    con.Close();
                }

                if (dataTable.Rows.Count > 0)
                {
                    con.Open();
                    try
                    {
                        SqlCommand cmd1 = new SqlCommand("UPDATE [Admin_LoginTbl] SET [AD_Password]=@NewPasscode WHERE ([AD_Code]=@Code and [AD_Password]=@OldPasscode)", con);
                        string subtime = Convert.ToString(System.DateTime.Now);
                        cmd1.Parameters.AddWithValue("@Code", Session["Id"].ToString());
                        cmd1.Parameters.AddWithValue("@OldPasscode", txtcurrpass.Value.ToString());
                        cmd1.Parameters.AddWithValue("@NewPasscode", txtconpass.Value.ToString());
                        cmd1.ExecuteNonQuery();
                        cmd1.Dispose();
                        lblstat2.Text = "Your Password is Correctly Updated.";
                        lblstat2.Visible = true;

                    }
                    catch (Exception ep)
                    {
                        lblstat2.Text = "Problem In Network. Unable to Update Password.";
                        lblstat2.Visible = true;
                    }
                    finally
                    {
                        con.Close();
                    }




                }
            
            else
            {
                lblstat2.Text = "Current Password is not Correct. Unable to Update.";
                    lblstat2.Visible = true;
            }
                dataTable.Dispose();

            }
            else
            {
                lblstat2.Text = "New Password and Conform Password does not match.";
                lblstat2.Visible = true;
            }
        }

        protected void btnSettings_Click(object sender, EventArgs e)
        {
            Response.Redirect("Settings.aspx?ENo=0");
        }

        protected void btnAnalys_Click(object sender, EventArgs e)
        {
            Response.Redirect("Analysis.aspx?");
        }

        protected void btnATR_Click(object sender, EventArgs e)
        {
            Response.Redirect("Admin.aspx?ENo=0&Menu=3");
        }
        protected void grp_tbl_Click(object sender, EventArgs e)
        {
            Response.Redirect("Graph_and_Table.aspx");
        }
        protected void btnAT_Click(object sender, EventArgs e)
        {
            Response.Redirect("Admin.aspx?ENo=0&Menu=2");
        }

        protected void btnTNT_Click(object sender, EventArgs e)
        {
            Response.Redirect("Admin.aspx?ENo=0");
        }
        protected void general_Click(object sender, EventArgs e)
        {
            Response.Redirect("ReportSettings.aspx");
        }
       
        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("LogIn.aspx");
        }

        protected void btninfo_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            string flag = "ok";
            con.Open();
            try
            {               
                SqlCommand cmd1 = new SqlCommand("UPDATE [Admin_LoginTbl] SET [AD_Code]=@AD_Code,[Extra1]=@Extra1,[Extra2]=@Extra2,[Extra3]=@Extra3 WHERE ([Ad_Id]='1')", con);
                string subtime = Convert.ToString(System.DateTime.Now);
                cmd1.Parameters.AddWithValue("@AD_Code", Request.Form["UserCode"].ToString());                      
                cmd1.Parameters.AddWithValue("@Extra2", Request.Form["txtname"].ToString()); 
                cmd1.Parameters.AddWithValue("@Extra1",  Request.Form["txtlocation"].ToString());
                cmd1.Parameters.AddWithValue("@Extra3",  Request.Form["email"].ToString());
                cmd1.ExecuteNonQuery();
                cmd1.Dispose();
                lblstat1.Text = "Info Updated Correctly.";
                lblstat1.Visible = true;

            }
            catch (Exception ep)
            {
                mainblk.InnerHtml = "Problem In network, your exam instance is saved.";

            }
            finally
            {
                con.Close();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("EmployeeSettings.aspx");
        }

        protected void addmarquee_Click(object sender, EventArgs e)
        {
            Response.Redirect("Admin_Marquee.aspx");
        }
    }
}