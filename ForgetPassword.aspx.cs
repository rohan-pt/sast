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
using System.Net.Mail;
using System.Net;
using System.Runtime.Remoting.Messaging;


namespace BCCBExamPortal
{
    public partial class ForgetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            con.Open();
            try
            {
                //sessionlbl.Text = Session["Id"].ToString();
                string com = "Select * from Locationtbl order by Location asc";
                SqlDataAdapter adpt = new SqlDataAdapter(com, con);
                DataTable dt = new DataTable();
                adpt.Fill(dt);
                dllocation.Items.Clear();
                dllocation.DataSource = dt;
                dllocation.DataBind();
                dllocation.DataTextField = "Location";
                dllocation.DataValueField = "SRNO";
                dllocation.DataBind();
                dllocation.Items.Insert(0, new ListItem("Select Location", "0"));
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
            con.Open();
            try
            {

                string com = "Select * from Employee_des order by Designation asc";
                SqlDataAdapter adpt = new SqlDataAdapter(com, con);
                DataTable dt = new DataTable();
                adpt.Fill(dt);
                dldesignation.Items.Clear();
                dldesignation.DataSource = dt;
                dldesignation.DataBind();
                dldesignation.DataTextField = "Designation";
                dldesignation.DataValueField = "SRNO";
                dldesignation.DataBind();
                dldesignation.Items.Insert(0, new ListItem("Select Designation", "0"));
                // dldesignation.Items.FindByValue("0").Selected = true;

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

        protected void Button1_Click(object sender, EventArgs e)
        {
            // Button1.Enabled = false;
            if (Button1.Text == "Give My credentials")
            {
                Button2.Visible = true;
                string x1 = dldesignation.Items[Convert.ToInt32(Request.Form["dldesignation"].ToString())].ToString();
                string x2 = dllocation.Items[Convert.ToInt32(Request.Form["dllocation"].ToString())].ToString();

                if (Username1.Value != "" && x1 != "Select Designation" && x2 != "Select Location")
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
                    int flag = 0;
                    con.Open();
                    try
                    {

                        SqlCommand cmd = new SqlCommand("select * from Admin_LoginTbl where AD_Code=@username", con);
                        cmd.Parameters.AddWithValue("@username", Username1.Value);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            SendEmail_Admin(dt.Rows[0]["Extra3"].ToString(), Username1.Value);
                            flag = 1;
                        }

                    }
                    catch (Exception ep)
                    {
                        txtlbl.Text = "Problem In Network Connection";
                        txtlbl.Visible = true;
                    }
                    finally
                    {
                        con.Close();
                    }
                    if (flag == 0)
                    {
                        con.Open();
                        try
                        {

                            SqlCommand cmd = new SqlCommand("select * from Employee_LoginTbl where Code=@username and Designation=@Designation and Location=@Location", con);
                            cmd.Parameters.AddWithValue("@username", Username1.Value);
                            cmd.Parameters.AddWithValue("@Designation", dldesignation.Items[Convert.ToInt32(Request.Form["dldesignation"].ToString())].ToString());
                            cmd.Parameters.AddWithValue("@Location", dllocation.Items[Convert.ToInt32(Request.Form["dllocation"].ToString())].ToString());
                            // cmd.Parameters.AddWithValue("@password", Password2.Value);
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {

                                string email = dt.Rows[0]["Email_Id"].ToString();
                                string Check = dt.Rows[0]["Ep3"].ToString();
                                // if (passcode == "")
                                // {
                                flag = 1;
                                int xl = Convert.ToInt32(Check);
                                if (email != "-" && xl < 3)
                                {
                                    SendEmail(email, Username1.Value, xl + 1);
                                }
                                else if (email == "-")
                                {
                                    txtlbl.Text = "No Email linked  with code " + Username1.Value + ". Please Contact to Head Office.";
                                }
                                else if (xl > 2)
                                {
                                    txtlbl.Text = "Maximum attempt to recover password is over";
                                }
                                // }
                                //else
                                //{
                                //    txtlbl.Text = "Not the first time.Try forget Password.";
                                //    Password2.Visible = false;
                                //    btncancel.Visible = false;
                                //    btnsubmit.Visible = false;
                                //    Button2.Visible = true;
                                //    Button1.Text = "Forget Password";
                                //}
                            }
                            else
                            {
                                txtlbl.Text = "Input not matching";
                            }

                        }
                        catch (Exception ep)
                        {
                            txtlbl.Text = "Problem In Network Connection";
                            txtlbl.Visible = true;
                        }
                        finally
                        {
                            con.Close();
                        }
                    }

                    if (flag == 0)
                    {
                        txtlbl.Text = "Input given is not correct. Please Contact to Human Resource department at Head Office.";
                    }

                    txtlbl.Visible = true;
                }
                else if (x1 == "Select Designation")
                {
                    txtlbl.Text = "Please select Designation";
                }
                else if (x2 == "Select Location")
                {
                    txtlbl.Text = "Please select location";
                }
                else if (Username1.Value == "")
                {
                    txtlbl.Text = "Please Enter Employee Code";
                }

            }
        }

        public void SendEmail_Admin(String email, string username)
        {
            string password = CreateRandomPassword(8);
            string flag = "0";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            con.Open();
            try
            {

                SqlCommand cmd1 = new SqlCommand("UPDATE [Admin_LoginTbl] SET [AD_Password]=@Passcode WHERE ([AD_Code]=@Code)", con);
                string subtime = Convert.ToString(System.DateTime.Now);
                cmd1.Parameters.AddWithValue("@Passcode", password);
                cmd1.Parameters.AddWithValue("@Code", Username1.Value);
                cmd1.ExecuteNonQuery();
                cmd1.Dispose();

            }
            catch (Exception ep)
            {
                txtlbl.Text = "Problem In Network, Your Test data is saved for that instance";
                flag = "1";
                txtlbl.Visible = true;
            }
            finally
            {
                con.Close();
            }
            if (flag == "0")
            {

                try
                {

                    string HostAdd = "smtp.gmail.com";// ConfigurationManager.AppSettings["Host"].ToString();
                    string FromEmailid = "BCCB Alert <no-reply@bccb.co.in>";// ConfigurationManager.AppSettings["FromMail"].ToString();
                    string Pass = "bccb@123";//ConfigurationManager.AppSettings["Password"].ToString();

                    //creating the object of MailMessage  
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress(FromEmailid); //From Email Id  
                    mailMessage.Subject = "System Generated password for BCCB Intranet Admin Log In"; //Subject of Email  

                    mailMessage.Body = "Hello Admin<br/>Your Credentials for BCCB Intranet website for Admin Log In<br/>" +
                            "Your Code : " + username + "<br/>Your Password : " + password + "<br/><h4>Thank you.</h4>"; //body or message of Email  
                    mailMessage.IsBodyHtml = true;
                    mailMessage.To.Add(new MailAddress(email)); //adding multiple TO Email Id  

                    SmtpClient smtp = new SmtpClient();  // creating object of smptpclient  
                    smtp.Host = HostAdd;              //host of emailaddress for example smtp.gmail.com etc  

                    //network and security related credentials  

                    smtp.EnableSsl = true;
                    NetworkCredential NetworkCred = new NetworkCredential();
                    NetworkCred.UserName = mailMessage.From.Address;
                    NetworkCred.Password = Pass;
                    smtp.UseDefaultCredentials = false;

                    smtp.Credentials = NetworkCred;
                    smtp.Port = 587;
                    //WriteErrorLog("Email sent in progress");
                    smtp.Send(mailMessage); //sending Email  
                    txtlbl.Text = "Password is sent in Email to Email-id " + email + ".";

                }
                catch (Exception en)
                {
                    txtlbl.Text = "Problem in sending Email. Please Contact to Head Office.";
                }

            }


        }

        public void SendEmail(String email, string username,int i)
        {
           
            string password = CreateRandomPassword(8);
            string flag = "0";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            con.Open();
            try
            {
                SqlCommand cmd1 = new SqlCommand("UPDATE [Employee_LoginTbl] SET [Passcode]=@Passcode , [Ep3]=@Ep3 WHERE ([Code]=@Code and [Email_Id]=@Email_Id and [Designation]=@Designation and [Location]=@Location)", con);
                string subtime = Convert.ToString(System.DateTime.Now);
                cmd1.Parameters.AddWithValue("@Passcode", password);
                cmd1.Parameters.AddWithValue("@Ep3", Convert.ToString(i+1));
                cmd1.Parameters.AddWithValue("@Code", Username1.Value);
                cmd1.Parameters.AddWithValue("@Email_Id", email);
                cmd1.Parameters.AddWithValue("@Designation", dldesignation.Items[Convert.ToInt32(Request.Form["dldesignation"].ToString())].ToString());
                cmd1.Parameters.AddWithValue("@Location", dllocation.Items[Convert.ToInt32(Request.Form["dllocation"].ToString())].ToString());
                cmd1.ExecuteNonQuery();
                cmd1.Dispose();
            }
            catch (Exception ep)
            {
                txtlbl.Text = "Problem In Network, Your Test data is saved for that instance";
                flag = "1";
            }
            finally
            {
                con.Close();
            }
            if (flag == "0")
            {

                try
                {

                    string HostAdd = "smtp.gmail.com";// ConfigurationManager.AppSettings["Host"].ToString();
                    string FromEmailid = "BCCB Alert <no-reply@bccb.co.in>";// ConfigurationManager.AppSettings["FromMail"].ToString();
                    string Pass = "bccb@123";//ConfigurationManager.AppSettings["Password"].ToString();

                    //creating the object of MailMessage  
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress(FromEmailid); //From Email Id  
                    mailMessage.Subject = "System Generated password for BCCB Intranet Log In"; //Subject of Email  

                    mailMessage.Body = "Hello<br/>Your Credentials for BCCB Intranet website<br/>" +
                            "Your Code : " + username + "<br/>Your Password : " + password + "<br/><h4>Thank you.</h4>"; //body or message of Email  
                    mailMessage.IsBodyHtml = true;
                    mailMessage.To.Add(new MailAddress(email)); //adding multiple TO Email Id  

                    SmtpClient smtp = new SmtpClient();  // creating object of smptpclient  
                    smtp.Host = HostAdd;              //host of emailaddress for example smtp.gmail.com etc  

                    //network and security related credentials  

                    smtp.EnableSsl = true;
                    NetworkCredential NetworkCred = new NetworkCredential();
                    NetworkCred.UserName = mailMessage.From.Address;
                    NetworkCred.Password = Pass;
                    smtp.UseDefaultCredentials = false;

                    smtp.Credentials = NetworkCred;
                    smtp.Port = 587;
                    //WriteErrorLog("Email sent in progress");
                    smtp.Send(mailMessage); //sending Email  
                    txtlbl.Text = "Password is sent in Email to Email-id " + email + ".";

                }
                catch (Exception en)
                {
                    txtlbl.Text = "Problem in sending Email. Please Contact to Head Office.";
                }

            }


        }

        public static string CreateRandomPassword(int PasswordLength)
        {
            string _allowedChars = "0123456789abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ";
            Random randNum = new Random();
            char[] chars = new char[PasswordLength];
            int allowedCharCount = _allowedChars.Length;
            for (int i = 0; i < PasswordLength; i++)
            {
                chars[i] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
            }
            return new string(chars);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("LogIn.aspx");
        }
    }
}