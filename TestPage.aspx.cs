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
    public partial class TestPage : System.Web.UI.Page
    {
        static int hh, mm, ss;
        int holder = 0;
       public static int MinutesAllowed { get; set; }
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
               // sessionlbl.Text = Session["id"].ToString();
                string Eno = Session["E_No"].ToString();
                string Qno = Session["Q_No"].ToString();
                lblExamNo.Text = Session["Exam_No"].ToString();

                int Qcount = Generate_ExamInfo(Eno, Qno);
                Session["Qcount"] = Qcount.ToString();
               
                int p = Convert.ToInt32(Session["time"].ToString());
                string jsFunc = "startCountdown(" + p + ");";
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "my", jsFunc, true);
                Session["time"] = " ";

                Session["State"] = "1";
                Session["resAns"] = "X";              

                if (Qno == "0")
                {
                    lblQnumber.Text = "1";
                    Session["Q_No"] = "1";
                    btnBack.Visible = false;
                    Generate_Page1(sessionlbl.Text);  // can be change with saving name of Employee in session
                }
                else
                {
                    lblQnumber.Text = Qno;
                    btnBack.Visible = true;
                    if (Session["EMPName"].ToString() == "0")
                    {
                        Generate_Page1(sessionlbl.Text);
                    }
                    else
                    {
                        divempname.InnerText = "     " + Session["EMPName"].ToString();
                    }
                }
                if (lblQnumber.Text == "P")
                {
                    divmain.InnerHtml = "Problem in network, your instance is saved";
                }
                else
                {

                    if (lblQnumber.Text == Convert.ToString(Qcount))
                    {
                        btnNext1.Visible = false;
                    }
                    else
                    {
                        btnNext1.Visible = true;
                    }

                    Generate_Page2(Eno, Qcount);

                }
            }
            else
            {
                dynamic_controls();
            }
            
            
            
        }

        protected void dynamic_controls()
        {
            
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            int v1 = 0, v2 = 0, v3 = 0;


            DataTable dataTable = new DataTable();
            con.Open();
            try
            {               
                SqlCommand cmd1 = new SqlCommand("select * from EmpExam_Responsetbl where Exam_ID=@E_Id order by R_Id asc", con);
                cmd1.Parameters.AddWithValue("@E_Id", Session["Exam_No"].ToString());
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
            divnavigation.InnerHtml = "";

            int looper = 0;
            int cssLoop = 0;
            int qnum1 = Convert.ToInt32(Session["Qcount"].ToString());
          
            while (looper == 0)
            {
                System.Web.UI.HtmlControls.HtmlGenericControl createDiv = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");

                createDiv.ID = "createDiv" + Convert.ToString(qnum1);
                int loopcounter = 4;
                if (qnum1 <= 4)
                {
                    loopcounter = qnum1;
                    looper = 1;
                }
                else
                {
                    qnum1 = qnum1 - 4;
                }


                for (int i = 1; i <= loopcounter; i++)
                {

                    Button btn = new Button();
                    btn.ID = "btn" + cssLoop.ToString();
                    btn.Text = Convert.ToString(cssLoop + 1);
                    btn.Attributes.Add("runat", "server");
                    string position = dataTable.Rows[cssLoop]["Extra1"].ToString();
                    if (position == "0")
                    {
                        btn.CssClass = "newexbtn";
                        v1 = v1 + 1;
                    }
                    else if (position == "1")
                    {
                        btn.CssClass = "newqbtnblue";
                        v2 = v2 + 1;
                    }
                    else if (position == "2")
                    {
                        btn.CssClass = "newqbtngreen";
                        v3 = v3 + 1;
                    }
                    cssLoop = cssLoop + 1;
                    string qnumdata = Session["Q_No"].ToString();
                    if (qnumdata == Convert.ToString(cssLoop))
                    {
                        btn.CssClass = "newqbtncurrent";
                    }
                    //btn.Click += new EventHandler(btn_Click);
                    createDiv.Controls.Add(btn);

                }
                divnavigation.Controls.Add(createDiv);

            }
        }
      
        protected void Timer1_Tick(object sender, EventArgs e)
        {
          
        }
        protected int Generate_ExamInfo(string Ecode,string Qno)
        {
            int p = 0;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("select * from ExamNametbl where E_Id=@E_Id", con);
                cmd1.Parameters.AddWithValue("@E_Id", Ecode);
                SqlDataReader usernameRdr = null;

                usernameRdr = cmd1.ExecuteReader();

                if (usernameRdr.HasRows == true)
                {
                    while (usernameRdr.Read())
                    {
                        string ExamName = usernameRdr["TestName"].ToString();
                        string Time = usernameRdr["Altime"].ToString();
                        string Num_Q = usernameRdr["NumQue"].ToString();
                        p = Convert.ToInt32(Num_Q);
                        divEname.InnerText = ExamName;
                        int sec = Convert.ToInt32(Time) * 60;
                        holder = sec;
                       
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
            return p;
        }

       
        protected void Generate_Page1(string Ecode)
        {            
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            con.Open();
            try
            {
               
                SqlCommand cmd1 = new SqlCommand("select * from Employee_LoginTbl where Code=@username", con);
                cmd1.Parameters.AddWithValue("@username", Ecode);
                SqlDataReader usernameRdr = null;

                usernameRdr = cmd1.ExecuteReader();
               
                if (usernameRdr.HasRows == true)
                {
                    while (usernameRdr.Read())
                    {
                        string EmpName = usernameRdr["Employee_Name"].ToString();
                        divempname.InnerText = "    " + EmpName;
                        Session["EMPName"] = EmpName;
                    }

                }
                else
                {
                    divempname.InnerText = "Nothing To Display, Seeems like error in session";
                }              

            }
            catch (Exception et)
            {
                divempname.InnerText = "Seems Network Issue";               
            }
            finally
            {
                con.Close();
            }

        }
      
        protected void Generate_Page2(string Ecode,int qnum)
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            int v1=0, v2=0, v3=0;
           

            DataTable dataTable = new DataTable();
            try
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("select * from EmpExam_Responsetbl where Exam_ID=@E_Id order by R_Id asc", con);
                cmd1.Parameters.AddWithValue("@E_Id", Session["Exam_No"].ToString());
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


            divnavigation.InnerHtml = "";

                        int looper = 0;
            int cssLoop = 0;
            int qnum1 = qnum;
                        while (looper == 0)
                        {
                            System.Web.UI.HtmlControls.HtmlGenericControl createDiv = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");

                createDiv.ID = "createDiv" + Convert.ToString(qnum1);
                            int loopcounter = 4;
                            if(qnum1<=4)
                            {
                                loopcounter = qnum1;
                                looper = 1;
                            }
                            else
                            {
                                qnum1 = qnum1 - 4;
                            }


                            for (int i = 1; i <= loopcounter; i++)
                            {                           

                                Button btn = new Button();
                                btn.ID = "btn" + cssLoop.ToString();
                                btn.Text = Convert.ToString(cssLoop + 1);
                                btn.Attributes.Add("runat", "server");
                    string position = dataTable.Rows[cssLoop]["Extra1"].ToString();
                        if(position=="0")
                    {
                        btn.CssClass = "newexbtn";
                        v1 = v1 + 1;
                    }
                        else if(position=="1")
                    {
                        btn.CssClass = "newqbtnblue";
                        v2 = v2 + 1;
                    }
                        else if(position=="2")
                    {
                        
                        btn.CssClass = "newqbtngreen";
                        v3 = v3 + 1;
                    }
                    cssLoop = cssLoop + 1;
                    string qnumdata = Session["Q_No"].ToString();
                    if (qnumdata == Convert.ToString(cssLoop))
                                {
                                btn.CssClass = "newqbtncurrent";
                                }
                               

                              
                                //btn.Click += new EventHandler(btn_Click);
                                createDiv.Controls.Add(btn);
                               
                            }
                            divnavigation.Controls.Add(createDiv);

                        }

            con.Open();

            try
            {
                //Questiontbl
               
                SqlCommand cmd1 = new SqlCommand("select * from Questiontbl where E_Id=@E_Id and Q_Num=@Q_Id", con);
                cmd1.Parameters.AddWithValue("@E_Id", Ecode);
               // string tem = Session["Q_No"].ToString();
                cmd1.Parameters.AddWithValue("@Q_Id", Convert.ToInt32(Session["Q_No"].ToString()));
                SqlDataReader usernameRdr = null;

                usernameRdr = cmd1.ExecuteReader();

                if (usernameRdr.HasRows == true)
                {
                    int i = 1;
                    while (usernameRdr.Read())
                    {
                        rado1.Checked = false;
                        radop4.Checked = false;
                        radop3.Checked = false;
                        radop2.Checked = false;
                        divquestionname.InnerText = "";
                        divop1.InnerText = "";
                        divop2.InnerText = "";
                        divop3.InnerText = "";
                        divop4.InnerText = "";


                        divquestionname.InnerHtml = usernameRdr["Question"].ToString().Replace(@"&lt;", @"<").Replace(@"&gt;", @">");
                            divop1.InnerText = usernameRdr["Op1"].ToString();
                            divop2.InnerText = usernameRdr["Op2"].ToString();
                            divop3.InnerText = usernameRdr["Op3"].ToString();
                            divop4.InnerText = usernameRdr["Op4"].ToString();



                        int response = Convert.ToInt32(Session["Q_No"].ToString());
                        string ans= dataTable.Rows[response-1]["EmpResponse"].ToString();

                       
                        divop1.Attributes.CssStyle.Add("background-color", "#c1e8ad");
                        divop2.Attributes.CssStyle.Add("background-color", "#c1e8ad");
                        divop3.Attributes.CssStyle.Add("background-color", "#c1e8ad");
                        divop4.Attributes.CssStyle.Add("background-color", "#c1e8ad");
                       
                        if (ans=="A")
                        {
                            rado1.Checked = true;
                            divop1.Attributes.CssStyle.Add("background-color", "#9330ea");
                            Session["State"] = "2";
                            Session["resAns"] = "A";
                        }
                        else if(ans == "B")
                        {
                            divop2.Attributes.CssStyle.Add("background-color", "#9330ea");
                            radop2.Checked = true;
                            Session["State"] = "2";
                            Session["resAns"] = "B";
                        }
                        else if (ans == "C")
                        {
                            divop3.Attributes.CssStyle.Add("background-color", "#9330ea");
                            radop3.Checked = true;
                            Session["State"] = "2";
                            Session["resAns"] = "C";
                        }
                        else if (ans == "D")
                        {
                            divop4.Attributes.CssStyle.Add("background-color", "#9330ea");
                            //// divop4.Attributes["background-color"] = "#9330ea";
                            ////divop4.Attributes.Add("class", "radioact");
                            radop4.Checked = true;
                            Session["State"] = "2";
                            Session["resAns"] = "D";
                        }
                        else
                        {
                            Session["State"] = "1";
                            Session["resAns"] = "X";
                        }


                    }
                    divqtrack.InnerText = "Question " + Session["Q_No"].ToString() + " of " + Convert.ToString(qnum);
                    if (v1 <= 1)
                    {
                        submitbtn.Enabled = true;
                        submitbtn.CssClass = "btnchange";
                    }                    
                    tdNotVisited.InnerText = Convert.ToString(v1);
                    tdNotAns.InnerText = Convert.ToString(v2);
                    tdAns.InnerText = Convert.ToString(v3);
                }
            }
            catch (Exception el)
            {
                divmain.InnerHtml = "Problem In Network, Your Test data is saved for that instance";
            }
            finally
            {
                con.Close();
            }
        }

        protected void rado1_CheckedChanged(object sender, EventArgs e)
        {
            if (rado1.Checked == true)
            {
                divop1.Style.Add("background-color", "#9330ea");
                divop2.Style.Add("background-color", "#c1e8ad");
                divop3.Style.Add("background-color", "#c1e8ad");
                divop4.Style.Add("background-color", "#c1e8ad");
                Session["State"] = "2";
                Session["resAns"] = "A";
            }
           
        }

        protected void radop2_CheckedChanged(object sender, EventArgs e)
        {
            if (radop2.Checked == true)
            {
                divop1.Style.Add("background-color", "#c1e8ad");
                divop2.Style.Add("background-color", "#9330ea");
                divop3.Style.Add("background-color", "#c1e8ad");
                divop4.Style.Add("background-color", "#c1e8ad");
                Session["State"] = "2";
                Session["resAns"] = "B";
            }
           
        }

        protected void radop3_CheckedChanged(object sender, EventArgs e)
        {
            if (radop3.Checked == true)
            {
                divop1.Style.Add("background-color", "#c1e8ad");
                divop2.Style.Add("background-color", "#c1e8ad");
                divop3.Style.Add("background-color", "#9330ea");
                divop4.Style.Add("background-color", "#c1e8ad");
                Session["State"] = "2";
                Session["resAns"] = "C";
            }
          
        }

        protected void radop4_CheckedChanged(object sender, EventArgs e)
        {
            if (radop4.Checked == true)
            {
                divop1.Style.Add("background-color", "#c1e8ad");
                divop2.Style.Add("background-color", "#c1e8ad");
                divop3.Style.Add("background-color", "#c1e8ad");
                divop4.Style.Add("background-color", "#9330ea");
                Session["State"] = "2";
                Session["resAns"] = "D";
            }
           
        }

        protected void submitbtn_Click(object sender, EventArgs e)
        {
            //ExamResult.aspx
            Response.Redirect("ExamResult.aspx");
        }

        protected void btnNext1_Click(object sender, ImageClickEventArgs e)
        {
            
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            // string flag="ok";         
            con.Open();
            try
            {
                SqlCommand cmd1 = new SqlCommand("UPDATE [EmpExam_Responsetbl] SET [EmpResponse]=@EmpResponse,[Time_Stamp]=@Time_Stamp,[Extra1]=@Extra1,[Extra2]=@Extra2,[Extra3]=@Extra3 WHERE ([Exam_ID]=@Exam_ID and [QNumber]=@QNumber)", con);
                string subtime = Convert.ToString(System.DateTime.Now);
                cmd1.Parameters.AddWithValue("@Exam_ID", Convert.ToInt32(Session["Exam_No"].ToString()));
                cmd1.Parameters.AddWithValue("@QNumber", Convert.ToInt32(Session["Q_No"].ToString()));
                string p1 = Session["resAns"].ToString();
                cmd1.Parameters.AddWithValue("@EmpResponse", Session["resAns"].ToString());
                string time = Hidden1.Value;
                double seconds = TimeSpan.Parse(time).TotalSeconds;
                cmd1.Parameters.AddWithValue("@Time_Stamp", Convert.ToInt32(seconds));
                cmd1.Parameters.AddWithValue("@Extra1", Session["State"].ToString());
                cmd1.Parameters.AddWithValue("@Extra2", "");
                cmd1.Parameters.AddWithValue("@Extra3", subtime);
                cmd1.ExecuteNonQuery();
                cmd1.Dispose();
            }
            catch (Exception ep)
            {
                divmain.InnerHtml = "Problem In Network, Your Test data is saved for that instance";
                //flag = "xx";
            }
            finally
            {
                con.Close();
            }
            int Q = Convert.ToInt32(Session["Q_No"].ToString()) + 1;
            Session["Q_No"] = Convert.ToString(Q);
            Generate_Page2(Session["E_No"].ToString(), Convert.ToInt32(Session["Qcount"].ToString()));
            string question_no = Session["Q_No"].ToString();
            string no_of_question = Session["Qcount"].ToString();
            btnBack.Visible = true;
            if (question_no == no_of_question)
            {
                btnNext1.Visible = false;
            }
            else
            {
                btnNext1.Visible = true;
            }       
    
        }

        protected void btnBack_Click(object sender, ImageClickEventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            // string flag = "ok";
            con.Open();
            try
            {  
                SqlCommand cmd1 = new SqlCommand("UPDATE [EmpExam_Responsetbl] SET [EmpResponse]=@EmpResponse,[Time_Stamp]=@Time_Stamp,[Extra1]=@Extra1,[Extra2]=@Extra2,[Extra3]=@Extra3 WHERE ([Exam_ID]=@Exam_ID and [QNumber]=@QNumber)", con);
                string subtime = Convert.ToString(System.DateTime.Now);
                cmd1.Parameters.AddWithValue("@Exam_ID", Convert.ToInt32(Session["Exam_No"].ToString()));
                cmd1.Parameters.AddWithValue("@QNumber", Convert.ToInt32(Session["Q_No"].ToString()));
                string p1 = Session["resAns"].ToString();
                cmd1.Parameters.AddWithValue("@EmpResponse", Session["resAns"].ToString());
                // string time = Request.Form["timeleft"].ToString();
                string time = Hidden1.Value;
                double seconds = TimeSpan.Parse(time).TotalSeconds;
                cmd1.Parameters.AddWithValue("@Time_Stamp", Convert.ToInt32(seconds));
                cmd1.Parameters.AddWithValue("@Extra1", Session["State"].ToString());
                cmd1.Parameters.AddWithValue("@Extra2", "");
                cmd1.Parameters.AddWithValue("@Extra3", subtime);
                cmd1.ExecuteNonQuery();
                cmd1.Dispose();

            }
            catch (Exception ep)
            {
                divmain.InnerHtml = "Problem In Network, Your Test data is saved for that instance";
                //flag = "xx";

            }
            finally
            {
                con.Close();
            }
            int Q = Convert.ToInt32(Session["Q_No"].ToString()) - 1;
            Session["Q_No"] = Convert.ToString(Q);

            Generate_Page2(Session["E_No"].ToString(), Convert.ToInt32(Session["Qcount"].ToString()));

            string question_no = Session["Q_No"].ToString();


            btnNext1.Visible = true;
            if (question_no == "1")
            {
                btnBack.Visible = false;
            }
            else
            {
                btnBack.Visible = true;
            }
           
        }
    }
}