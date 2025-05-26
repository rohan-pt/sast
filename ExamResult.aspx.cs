using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;


namespace BCCBExamPortal
{
    public partial class ExamResult : System.Web.UI.Page
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
                cmd1.Parameters.AddWithValue("@Page_Name", "ExamResult");
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
                Session["resAns"] = Request.QueryString["resCode"];
                if (Session["resAns"].ToString() != "")
                {
                    Session["State"] = "2";
                }
                else
                {
                    Session["State"] = "1";
                    Session["resAns"] = "X";
                }
            }
            catch (Exception ep)
            {
                Response.Redirect("LogIn.aspx");
            }

            //resCode
            if (!IsPostBack)
            {
                audit_trails();
                string Eno = Session["E_No"].ToString();
                lblExamNo.Text = Session["Exam_No"].ToString();
                initial_info();
                result_Page();
                GenerateResult(Eno);
                Session.Remove("E_No");
                Session.Remove("Exam_No");
                Session.Remove("Q_No");
                Session.Remove("resAns");
            }

        }

        //select AD_Code,L.Extra1,L.Extra2,M.TestName from Admin_LoginTbl as L,EmpExam_RecTbl as p,ExamNametbl as M where L.AD_Code=p.Employee_Code and p.Exam_ID=19 and p.Employee_Code='X1' and p.E_ID=M.E_Id
        protected void initial_info()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            con.Open();
            try
            {
                //Questiontbl
               
                SqlCommand cmd1 = new SqlCommand("select Code,L.Location,L.Employee_Name,M.TestName from Employee_LoginTbl as L,EmpExam_RecTbl as p,ExamNametbl as M where L.Code=p.Employee_Code and p.Exam_ID=@E_Id and Employee_Code=@Code and p.E_ID=M.E_Id", con);
                string po = Session["E_No"].ToString();
                cmd1.Parameters.AddWithValue("@Code", Session["id"].ToString());//Session["E_No"]
                cmd1.Parameters.AddWithValue("@E_Id", Convert.ToInt32(Session["Exam_No"].ToString()));
                SqlDataReader usernameRdr = null;
              
                usernameRdr = cmd1.ExecuteReader();
               
                if (usernameRdr.HasRows == true)
                {
                    while (usernameRdr.Read())
                    {
                        tdname.InnerText =  usernameRdr["Employee_Name"].ToString();
                        tdempcode.InnerText= usernameRdr["Code"].ToString();
                        tdloc.InnerText= usernameRdr["Location"].ToString();
                        tdtestname.InnerText= usernameRdr["TestName"].ToString();
                    }
                }              

            }
            catch (Exception el)
            {
                divmain.InnerHtml = "Problem In network, your exam instance is saved.";
            }
            finally
            {
                con.Close();
            }
         
        }



        protected void result_Page()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            con.Open();
            try
            {
              
                SqlCommand cmd1 = new SqlCommand("UPDATE [EmpExam_Responsetbl] SET [EmpResponse]=@EmpResponse,[Time_Stamp]=@Time_Stamp,[Extra1]=@Extra1,[Extra2]=@Extra2,[Extra3]=@Extra3 WHERE ([Exam_ID]=@Exam_ID and [QNumber]=@QNumber)", con);
                string subtime = Convert.ToString(System.DateTime.Now);
                cmd1.Parameters.AddWithValue("@Exam_ID", Convert.ToInt32(Session["Exam_No"].ToString()));
                cmd1.Parameters.AddWithValue("@QNumber", Convert.ToInt32(Session["Q_No"].ToString()));
                cmd1.Parameters.AddWithValue("@EmpResponse", Session["resAns"].ToString());
                cmd1.Parameters.AddWithValue("@Time_Stamp", Convert.ToInt32("0"));
                cmd1.Parameters.AddWithValue("@Extra1", Session["State"].ToString());
                cmd1.Parameters.AddWithValue("@Extra2", "");
                cmd1.Parameters.AddWithValue("@Extra3", subtime);
                cmd1.ExecuteNonQuery();
                cmd1.Dispose();


            }
            catch (Exception ep)
            {
                divmain.InnerHtml = "Problem In Network, Your Test data is saved for that instance";

            }
            finally
            {
                con.Close();
            }
            
        }
        protected void GenerateResult(string Eno)
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString); 
            DataTable dataTable = new DataTable();
            con.Open();
            try
            {               
                SqlCommand cmd1 = new SqlCommand("select * from EmpExam_Responsetbl where Exam_ID=@E_Id order by R_Id asc", con);
                cmd1.Parameters.AddWithValue("@E_Id", lblExamNo.Text);
                SqlDataAdapter da = new SqlDataAdapter(cmd1);
                // this will query your database and return the result to your datatable
                da.Fill(dataTable);
                da.Dispose();
            }
            catch (Exception oo)
            {
                divmain.InnerHtml = "Problem In network, your exam instance is saved.";
            }
            finally
            {
                con.Close();
            }
            con.Open();
            try
            {
                //Questiontbl               
                SqlCommand cmd1 = new SqlCommand("select * from Questiontbl where E_Id=@E_Id order by Q_Num asc", con);
                cmd1.Parameters.AddWithValue("@E_Id", Eno);             
                SqlDataReader usernameRdr = null;
                int i = 1,n=0;
                usernameRdr = cmd1.ExecuteReader();
                int marks = 0;
                if (usernameRdr.HasRows == true)
                {
                   
                    while (usernameRdr.Read())
                    {

                        string response = usernameRdr["Correct_Op"].ToString();

                        string ans = dataTable.Rows[i - 1]["EmpResponse"].ToString();
                        if (response == ans)
                        {
                            marks = marks + 1;
                        }
                        else if(ans=="X")
                        {
                            n = n + 1;
                        }

                        i = i + 1;

                    }
                }

                tdqnum.InnerText = Convert.ToString(i - 1);
                tdmarks.InnerText = Convert.ToString(marks);
                double total = Convert.ToDouble(i) - 1;
                tdpercentage.InnerText = Convert.ToString(Math.Round(((Convert.ToDouble(marks) / total) * 100),2));
                int cal = (i - 1) - n;

                tdatt.InnerText = Convert.ToString(cal);
                
            }
            catch (Exception el)
            {
                divmain.InnerHtml = "Problem In network, your exam instance is saved.";
            }
            finally
            {
                con.Close();
            }
            con.Open();
            try
            {               
                SqlCommand cmd1 = new SqlCommand("UPDATE [EmpExam_RecTbl] SET [Num_que_attep]=@Num_que_attep,[Extra1]=@Extra1,[Extra2]=@Extra2 WHERE ([Exam_ID]=@Exam_ID )", con);

                cmd1.Parameters.AddWithValue("@Exam_ID", Convert.ToInt32(lblExamNo.Text));              
                cmd1.Parameters.AddWithValue("@Num_que_attep", tdatt.InnerText);
                cmd1.Parameters.AddWithValue("@Extra1", tdpercentage.InnerText);
                cmd1.Parameters.AddWithValue("@Extra2", "1");
                cmd1.ExecuteNonQuery();
                cmd1.Dispose();

            }
            catch (Exception zz)
            {
                divmain.InnerHtml = "Problem In network, your exam instance is saved.";
            }
            finally
            {
                con.Close();
            }
            
        }

        protected void btnok_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }
    }
}