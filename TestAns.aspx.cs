using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Web.UI.HtmlControls;

namespace BCCBExamPortal
{
    public partial class TestAns : System.Web.UI.Page
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
            generate_ExamInfo();
            Generate_ans();


        }

        protected void generate_ExamInfo()
        {
          
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            con.Open();
            try
            {
               
                SqlCommand cmd1 = new SqlCommand("select * from ExamNametbl where E_Id=@E_Id", con);
                cmd1.Parameters.AddWithValue("@E_Id", Session["XLNo"].ToString());
                SqlDataReader usernameRdr = null;

                usernameRdr = cmd1.ExecuteReader();

                if (usernameRdr.HasRows == true)
                {
                    while (usernameRdr.Read())
                    {
                        divexaminfo.InnerText = usernameRdr["TestName"].ToString();
                       
                    }
                }
            }
            catch (Exception tr)
            {
                divmain.InnerHtml = "Seems Network Problem";
            }
            finally
            {
                con.Close();
            }
         
        }

        protected void Generate_ans()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            DataTable dataTable = new DataTable();
            con.Open();
            try
            {
                
                SqlCommand cmd1 = new SqlCommand("select A.EmpResponse from EmpExam_Responsetbl A,EmpExam_RecTbl B where B.E_ID=@E_Id and B.Employee_Code=@EmpCode and A.Exam_ID=B.Exam_ID order by A.R_Id asc", con);
                cmd1.Parameters.AddWithValue("@E_Id", Session["XLNo"].ToString());
                cmd1.Parameters.AddWithValue("@EmpCode", Session["id"].ToString());
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
                SqlCommand cmd1 = new SqlCommand("select * from Questiontbl where E_Id=@E_Id order by Q_Num asc", con);
                cmd1.Parameters.AddWithValue("@E_Id", Session["XLNo"].ToString());

                SqlDataReader usernameRdr = null;

                usernameRdr = cmd1.ExecuteReader();

                if (usernameRdr.HasRows == true)
                {
                    int i = 1;
                    if (dataTable.Rows.Count > 0)
                    {

                        while (usernameRdr.Read())
                        {
                            int fag = 0;
                            string ans = usernameRdr["Correct_Op"].ToString();
                            HtmlGenericControl divXL = new HtmlGenericControl("div");
                            string idxl = "Q" + (i + 1).ToString();
                            divXL.Attributes.Add("id", idxl);
                            divXL.Style.Add("width", "100%");
                            HtmlGenericControl divQ = new HtmlGenericControl("div");
                            divQ.Attributes.Add("class", "finansQ");
                            string id = "Q" + (i + 1).ToString();
                            divQ.Attributes.Add("id", id);
                            divQ.InnerText = "(" + (i) + ") " + usernameRdr["Question"].ToString();
                            divXL.Controls.Add(divQ);
                            //finalopa=hitted correctly
                            //finalop=normal
                            //finalopa1=Correct Ans
                            //finalopa2=your response

                            string b = dataTable.Rows[i - 1]["EmpResponse"].ToString();

                            HtmlGenericControl Op1 = new HtmlGenericControl("div");
                            if (ans == "A")
                            {
                                if (dataTable.Rows[i - 1]["EmpResponse"].ToString() == "A")
                                {
                                    Op1.Attributes.Add("class", "finalopa");
                                    fag = 1;
                                }
                                else
                                {
                                    Op1.Attributes.Add("class", "finalopa1");
                                }


                            }
                            else
                            {
                                Op1.Attributes.Add("class", "finalop");
                            }

                            string id1 = "OP1" + (i + 1).ToString();
                            Op1.Attributes.Add("id", id1);
                            Op1.InnerText = usernameRdr["Op1"].ToString();
                            divXL.Controls.Add(Op1);


                            HtmlGenericControl Op2 = new HtmlGenericControl("div");
                            if (ans == "B")
                            {
                                if (dataTable.Rows[i - 1]["EmpResponse"].ToString() == "B")
                                {
                                    Op2.Attributes.Add("class", "finalopa");
                                    fag = 1;
                                }
                                else
                                {
                                    Op2.Attributes.Add("class", "finalopa1");
                                }


                            }
                            else
                            {
                                Op2.Attributes.Add("class", "finalop");
                            }

                            string id2 = "OP2" + (i + 1).ToString();
                            Op2.Attributes.Add("id", id2);
                            Op2.InnerText = usernameRdr["Op2"].ToString();
                            divXL.Controls.Add(Op2);

                            HtmlGenericControl Op3 = new HtmlGenericControl("div");
                            if (ans == "C")
                            {
                                if (dataTable.Rows[i - 1]["EmpResponse"].ToString() == "C")
                                {
                                    Op3.Attributes.Add("class", "finalopa");
                                    fag = 1;
                                }
                                else
                                {
                                    Op3.Attributes.Add("class", "finalopa1");
                                }


                            }
                            else
                            {
                                Op3.Attributes.Add("class", "finalop");
                            }

                            string id3 = "OP3" + (i + 1).ToString();
                            Op3.Attributes.Add("id", id1);
                            Op3.InnerText = usernameRdr["Op3"].ToString();
                            divXL.Controls.Add(Op3);

                            HtmlGenericControl Op4 = new HtmlGenericControl("div");
                            if (ans == "D")
                            {
                                if (dataTable.Rows[i - 1]["EmpResponse"].ToString() == "D")
                                {
                                    Op4.Attributes.Add("class", "finalopa");
                                    fag = 1;
                                }
                                else
                                {
                                    Op4.Attributes.Add("class", "finalopa1");
                                }



                            }
                            else
                            {
                                Op4.Attributes.Add("class", "finalop");
                            }

                            string id4 = "OP4" + (i + 1).ToString();
                            Op4.Attributes.Add("id", id1);
                            Op4.InnerText = usernameRdr["Op4"].ToString();
                            divXL.Controls.Add(Op4);
                            divmain.Controls.Add(divXL);
                            if (fag == 0)
                            {
                                if (dataTable.Rows[i - 1]["EmpResponse"].ToString() == "A")
                                {
                                    Op1.Attributes.Add("class", "finalopa2");

                                }
                                else if (dataTable.Rows[i - 1]["EmpResponse"].ToString() == "B")
                                {
                                    Op2.Attributes.Add("class", "finalopa2");
                                }
                                else if (dataTable.Rows[i - 1]["EmpResponse"].ToString() == "C")
                                {
                                    Op3.Attributes.Add("class", "finalopa2");
                                }
                                else if (dataTable.Rows[i - 1]["EmpResponse"].ToString() == "D")
                                {
                                    Op4.Attributes.Add("class", "finalopa2");
                                }
                            }




                            i++;
                        }

                    }
                    else
                    {
                        while (usernameRdr.Read())
                        {
                           // int fag = 0;
                            string ans = usernameRdr["Correct_Op"].ToString();
                            HtmlGenericControl divXL = new HtmlGenericControl("div");
                            string idxl = "Q" + (i + 1).ToString();
                            divXL.Attributes.Add("id", idxl);
                            divXL.Style.Add("width", "100%");
                            HtmlGenericControl divQ = new HtmlGenericControl("div");
                            divQ.Attributes.Add("class", "finansQ");
                            string id = "Q" + (i + 1).ToString();
                            divQ.Attributes.Add("id", id);
                            divQ.InnerText = "(" + (i) + ") " + usernameRdr["Question"].ToString();
                            divXL.Controls.Add(divQ);
                            //finalopa=hitted correctly
                            //finalop=normal
                            //finalopa1=Correct Ans
                            //finalopa2=your response

                          //  string b = dataTable.Rows[i - 1]["EmpResponse"].ToString();

                            HtmlGenericControl Op1 = new HtmlGenericControl("div");
                            if (ans == "A")
                            {
                                   Op1.Attributes.Add("class", "finalopa1");

                            }
                            else
                            {
                                Op1.Attributes.Add("class", "finalop");
                            }

                            string id1 = "OP1" + (i + 1).ToString();
                            Op1.Attributes.Add("id", id1);
                            Op1.InnerText = usernameRdr["Op1"].ToString();
                            divXL.Controls.Add(Op1);


                            HtmlGenericControl Op2 = new HtmlGenericControl("div");
                            if (ans == "B")
                            {
                                    Op2.Attributes.Add("class", "finalopa1");
                            }
                            else
                            {
                                Op2.Attributes.Add("class", "finalop");
                            }

                            string id2 = "OP2" + (i + 1).ToString();
                            Op2.Attributes.Add("id", id2);
                            Op2.InnerText = usernameRdr["Op2"].ToString();
                            divXL.Controls.Add(Op2);

                            HtmlGenericControl Op3 = new HtmlGenericControl("div");
                            if (ans == "C")
                            {
                                    Op3.Attributes.Add("class", "finalopa1");
                            }
                            else
                            {
                                Op3.Attributes.Add("class", "finalop");
                            }

                            string id3 = "OP3" + (i + 1).ToString();
                            Op3.Attributes.Add("id", id1);
                            Op3.InnerText = usernameRdr["Op3"].ToString();
                            divXL.Controls.Add(Op3);

                            HtmlGenericControl Op4 = new HtmlGenericControl("div");
                            if (ans == "D")
                            {
                                    Op4.Attributes.Add("class", "finalopa1");
                            }
                            else
                            {
                                Op4.Attributes.Add("class", "finalop");
                            }

                            string id4 = "OP4" + (i + 1).ToString();
                            Op4.Attributes.Add("id", id1);
                            Op4.InnerText = usernameRdr["Op4"].ToString();
                            divXL.Controls.Add(Op4);
                            divmain.Controls.Add(divXL);
                            
                            i++;
                        }
                    }
                    

                }
                
            }
            catch (Exception rr)
            {
                divmain.InnerText = "Problem In network.";
            }
            finally
            {
                con.Close();
            }


        }

        protected void btnchangepassword_Click(object sender, EventArgs e)
        {
            Session.Remove("XLNo");
            Response.Redirect("ChangePassword.aspx");
        }

        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("LogIn.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("EmpDashBoard.aspx");
        }
    }
}