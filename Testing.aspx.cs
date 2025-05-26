using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BCCBExamPortal
{
    public partial class Testing : System.Web.UI.Page
    {
        int holder = 0;
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
            //sessionlbl.Text = "08560";
            if (!IsPostBack)
            {
                string Eno = Session["E_No"].ToString();
                lblExamNo.Text = Session["Exam_No"].ToString();
                string Qno = "";
                get_live_Question(ref Qno, ref holder);
                int Qcount = Generate_ExamInfo(Eno);
                Session["Qcount"] = Qcount.ToString();
                Hidden1.Value = holder.ToString();
                generate_instructions();               
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "my56", "show_ins();", true);
                if (Qno == "1")
                {
                    Session["Q_No"] = "1";
                    back_btn.Visible = false;
                    Generate_Page1(sessionlbl.Text);
                }
                else
                {
                    Session["Q_No"] = Qno;
                    back_btn.Visible = true;
                    Generate_Page1(sessionlbl.Text);
                }
               
                if (Session["Q_No"].ToString() == Convert.ToString(Qcount))
                {
                    next_btn.Visible = false;
                }
                else
                {
                    next_btn.Visible = true;
                }
                Generate_Page2(Session["Exam_No"].ToString(), Qcount);
            }
            else
            {  
                   Generate_Page2(Session["Exam_No"].ToString(), Convert.ToInt32(Session["Qcount"].ToString()));               
            }
        }

        private void generate_instructions()
        {
            string svg = " <div class='header_war' style='display:inline-block;line-height:40px;background:#94daf1;'> " +
" <svg  width='50' height='40' viewBox='0 0 50 40' style='display:inline-block;margin-left:10%;float:left;'> " +
" <circle r='15' cx='25' cy='20' stroke='black' fill='#5cdc63' /> " +
" <line x1='25' y1='18' x2='25' y2='33' stroke='black' stroke-width='3'></line> " +
" <circle r='2' cx='25' cy='13' fill='black' /> " +
" </svg>   " +
" <span style='width:50%;display:inline-block;float:left;font-size:1.5em;font-family:'Courier New';color:#145f67;'>Exam Information</span> " +
" </div> ";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);

            string query = "select * from Test_Instruction_tbl where E_ID=" + Session["E_No"].ToString();
            DataTable dt = get_normal_data(query, con);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    svg = svg + " <div class='header_war war_add'> " +
    " <svg  width='25' height='25' viewBox='0 0 25 25' style='display:inline-block;margin-left:10%;float:left;'> " +
    " <circle r='5' cx='12' cy='12' fill='black' /></svg> " +
    " <span style='width:80%;display:inline-block;float:left;font-size:1.3em;font-family: Calibri;color:#000000;min-height:40px; margin-left: 2%;text-align:left;'> " +
    " " + dt.Rows[i]["Instruction"] + " " +
    " </span></div> ";
                }
            }
            else
            {
                svg = svg + " <div class='header_war war_add'> " +
    " <svg  width='25' height='25' viewBox='0 0 25 25' style='display:inline-block;margin-left:10%;float:left;'> " +
    " <circle r='5' cx='12' cy='12' fill='black' /></svg> " +
    " <span style='width:80%;display:inline-block;float:left;font-size:1.3em;font-family: Calibri;color:#000000;min-height:40px; margin-left: 2%;text-align:left;'> " +
    " NA " +
    " </span></div> ";
            }
            start_instruct.InnerHtml = svg;
        }
        private void get_live_Question(ref string Qno,ref int time)
        {
            string query = "select top 1 * from EmpExam_Responsetbl where Exam_ID='"+ lblExamNo.Text + "' and Time_Stamp!=0 order by Extra3 desc";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            DataTable dt = get_normal_data(query, con);
            if (dt.Rows.Count != 0)
            {
                Qno = dt.Rows[0]["QNumber"].ToString();
                time = Convert.ToInt32(dt.Rows[0]["Time_Stamp"].ToString());
            }
            else
            {
                Qno = "1";
                time = 0;
            }

        }

        protected void submit_exam(object sender, EventArgs e)
        {
            Response.Redirect("ExamResult.aspx?resCode=" + hdn_response.Value);
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
                cmd1.Parameters.AddWithValue("@E_Id", Session["Exam_No"].ToString()); //Session["Exam_No"].ToString()
                SqlDataAdapter da = new SqlDataAdapter(cmd1);
                da.Fill(dataTable);
                da.Dispose();
            }
            catch (Exception oo)
            {
                //divmain.InnerHtml = "Problem In network, your exam instance is saved.";
            }
            finally
            {
                con.Close();
            }
            //divnavigation.InnerHtml = "";
            btn_holder.Controls.Clear();
            int looper = 0;
            int cssLoop = 0;
            int qnum1 = Convert.ToInt32(Session["Qcount"].ToString());
            int xf = 1;
            while (looper == 0)
            {
                System.Web.UI.HtmlControls.HtmlGenericControl createDiv = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                createDiv.Attributes["class"] = "disk_div";
                createDiv.ID = "createDiv" + Convert.ToString(qnum1);
                int loopcounter = 5;
                if (qnum1 <= 5)
                {
                    loopcounter = qnum1;
                    looper = 1;
                }
                else
                {
                    qnum1 = qnum1 - 5;
                }

                for (int i = 1; i <= loopcounter; i++)
                {
                    Button btn = new Button();
                    btn.ID = "btn_" + cssLoop.ToString();
                    btn.Text = Convert.ToString(cssLoop + 1);
                    btn.Attributes.Add("runat", "server");
                   
                    string position = dataTable.Rows[cssLoop]["Extra1"].ToString();
                    if (position == "0")
                    {
                        btn.CssClass = "round_btn_pln";
                        v1 = v1 + 1;
                    }
                    else if (position == "1")
                    {
                        btn.CssClass = "round_btn_not_answered";
                        v2 = v2 + 1;
                    }
                    else if (position == "2")
                    {
                        btn.CssClass = "round_btn_answered";
                        v3 = v3 + 1;
                    }
                    cssLoop = cssLoop + 1;
                    if (xf.ToString() == Session["Q_No"].ToString())
                    {
                        btn.CssClass = "round_btn_selected";
                    }
                    btn.Click += new EventHandler(btn_Click);
                    createDiv.Controls.Add(btn);
                    xf = xf + 1;
                }
                btn_holder.Controls.Add(createDiv);
            }
        }
        private DataTable get_normal_data(string query, SqlConnection con)
        {
            DataTable dt = new DataTable();
            con.Open();
            try
            {
                SqlDataAdapter adpt = new SqlDataAdapter(query, con);
                adpt.Fill(dt);

            }
            catch (Exception el)
            {
                string er = "Unable to fetch Customers details due to Network issue: " + el + " : : " + query;
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('Unable to fetch Customers details due to Network issue:');", true);
            }
            finally
            {
                con.Close();
            }

            return dt;
        }
        protected int Generate_ExamInfo(string Ecode)
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
                        if (holder == 0)
                        {
                            holder = sec;
                        }
                    }
                }
            }
            catch (Exception tr)
            {
               // divmain.InnerHtml = "Seems Network Problem";
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
                        Emp_name.InnerText = EmpName;
                        Session["EMPName"] = EmpName;
                        Emp_Location.InnerText = usernameRdr["Location"].ToString();
                        Emp_code.InnerText = usernameRdr["Code"].ToString();
                        Emp_des.InnerText = usernameRdr["Designation"].ToString();
                        Emp_email.InnerText = usernameRdr["Email_Id"].ToString();
                    }

                }
                else
                {
                    //divempname.InnerText = "Nothing To Display, Seeems like error in session";
                }


            }
            catch (Exception et)
            {
                //divempname.InnerText = "Seems Network Issue";
            }
            finally
            {
                con.Close();
            }

        }

        protected void Generate_Page2(string Ecode, int qnum)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            int v1 = 0, v2 = 0, v3 = 0;
            DataTable dataTable = new DataTable();
            try
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("select * from EmpExam_Responsetbl where Exam_ID=@E_Id order by R_Id asc", con);
                cmd1.Parameters.AddWithValue("@E_Id", Ecode);
                SqlDataAdapter da = new SqlDataAdapter(cmd1);
                // this will query your database and return the result to your datatable
                da.Fill(dataTable);
                da.Dispose();
            }
            catch (Exception oo)
            {
                //divmain.InnerHtml = "Problem In network, your exam instance is saved.";
            }
            finally
            {
                con.Close();
            }

            btn_holder.Controls.Clear();
           // divnavigation.InnerHtml = "";

            int looper = 0;
            int cssLoop = 0;
            int qnum1 = qnum;
            int xf = 1;
            while (looper == 0)
            {
                System.Web.UI.HtmlControls.HtmlGenericControl createDiv = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                createDiv.Attributes["class"] = "disk_div";
                createDiv.ID = "createDiv" + Convert.ToString(qnum1);
                int loopcounter = 5;
                if (qnum1 <= 5)
                {
                    loopcounter = qnum1;
                    looper = 1;
                }
                else
                {
                    qnum1 = qnum1 - 5;
                }


                for (int i = 1; i <= loopcounter; i++)
                {

                    Button btn = new Button();
                    btn.ID = "btn" + cssLoop.ToString();
                    btn.Text = Convert.ToString(cssLoop+1);
                    btn.Attributes.Add("runat", "server");
                    string position = dataTable.Rows[cssLoop]["Extra1"].ToString();
                    if (position == "0")
                    {
                        btn.CssClass = "round_btn_pln";
                        v1 = v1 + 1;
                    }
                    else if (position == "1")
                    {
                        btn.CssClass = "round_btn_not_answered";
                        v2 = v2 + 1;
                    }
                    else if (position == "2")
                    {

                        btn.CssClass = "round_btn_answered";
                        v3 = v3 + 1;
                    }
                    cssLoop = cssLoop + 1;
                  
                    if (xf.ToString() == Session["Q_No"].ToString())
                    {
                        btn.CssClass = "round_btn_selected";
                    }
                    btn.Click += new EventHandler(btn_Click);
                    createDiv.Controls.Add(btn);
                    xf = xf + 1;
                }
               // divnavigation.Controls.Add(createDiv);
                btn_holder.Controls.Add(createDiv);
            }
           
           con.Open();

            try
            {
                //Questiontbl

                SqlCommand cmd1 = new SqlCommand("select * from Questiontbl where E_Id=@E_Id and Q_Num=@Q_Id", con);
                cmd1.Parameters.AddWithValue("@E_Id", Session["E_No"].ToString());
                // string tem = Session["Q_No"].ToString();
                cmd1.Parameters.AddWithValue("@Q_Id", Convert.ToInt32(Session["Q_No"].ToString()));
                SqlDataReader usernameRdr = null;

                usernameRdr = cmd1.ExecuteReader();

                if (usernameRdr.HasRows == true)
                {
                    int i = 1;
                    while (usernameRdr.Read())
                    {
                        option1.Checked = false;
                        option2.Checked = false;
                        option3.Checked = false;
                        option4.Checked = false;
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
                        string ans = dataTable.Rows[response - 1]["EmpResponse"].ToString();
                        lbl_op1.Attributes.Add("class", "option_lbl");
                        lbl_op2.Attributes.Add("class", "option_lbl");
                        lbl_op3.Attributes.Add("class", "option_lbl");
                        lbl_op4.Attributes.Add("class", "option_lbl");
                        option1.Checked = false;
                        option2.Checked = false;
                        option3.Checked = false;
                        option4.Checked = false;

                        if (ans == "A")
                        {
                            option1.Checked = true;
                            lbl_op1.Attributes.Add("class", "option_lbl checked");
                            //Session["State"] = "2";
                            //Session["resAns"] = "A";
                        }
                        else if (ans == "B")
                        {
                            lbl_op2.Attributes.Add("class", "option_lbl checked");
                            option2.Checked = true;
                            //Session["State"] = "2";
                            //Session["resAns"] = "B";
                        }
                        else if (ans == "C")
                        {
                            lbl_op3.Attributes.Add("class", "option_lbl checked");
                            option3.Checked = true;
                            //Session["State"] = "2";
                            //Session["resAns"] = "C";
                        }
                        else if (ans == "D")
                        {
                            lbl_op4.Attributes.Add("class", "option_lbl checked");
                            option4.Checked = true;
                            //Session["State"] = "2";
                            //Session["resAns"] = "D";
                        }
                        //else
                        //{
                        //    Session["State"] = "1";
                        //    Session["resAns"] = "X";
                        //}
                        if (hdn_response.Value == "")
                        {
                            if (ans != "X")
                            {
                                hdn_response.Value = ans;
                            }
                        }
                    }
                    divqtrack.InnerText = "Question " + Session["Q_No"].ToString() + " of " + Convert.ToString(qnum);
                    if (v1 < 1)
                    {
                        submit_btn.Visible = true;                      
                    }
                    tdNotVisited.InnerText = Convert.ToString(v1);
                    tdNotAns.InnerText = Convert.ToString(v2);
                    tdAns.InnerText = Convert.ToString(v3);
                }
            }
            catch (Exception el)
            {
                //divmain.InnerHtml = "Problem In Network, Your Test data is saved for that instance";
            }
            finally
            {
                con.Close();
            }
        }

        protected void btn_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            string btnId = b.Text;

            string option = "X";
            string q_state = "1";
            //if (option1.Checked == true)
            //{
            //    option = "A";
            //    q_state = "2";
            //}
            //else if (option2.Checked == true)
            //{
            //    option = "B";
            //    q_state = "2";
            //}
            //else if (option3.Checked == true)
            //{
            //    option = "C";
            //    q_state = "2";
            //}
            //else if (option4.Checked == true)
            //{
            //    option = "D";
            //    q_state = "2";
            //}
            if(hdn_response.Value!="")
            {
                option = hdn_response.Value;
                q_state = "2";
            }

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            con.Open();
            try
            {
                SqlCommand cmd1 = new SqlCommand("UPDATE [EmpExam_Responsetbl] SET [EmpResponse]=@EmpResponse,[Time_Stamp]=@Time_Stamp,[Extra1]=@Extra1,[Extra2]=@Extra2,[Extra3]=@Extra3 WHERE ([Exam_ID]=@Exam_ID and [QNumber]=@QNumber)", con);
                string subtime = Convert.ToString(System.DateTime.Now);
                cmd1.Parameters.AddWithValue("@Exam_ID", Convert.ToInt32(Session["Exam_No"].ToString()));
                cmd1.Parameters.AddWithValue("@QNumber", Convert.ToInt32(Session["Q_No"].ToString()));
                cmd1.Parameters.AddWithValue("@EmpResponse", option);
                string time = Hidden1.Value;
                double seconds = TimeSpan.Parse(time).TotalSeconds;
                Session["time"] = seconds;
                cmd1.Parameters.AddWithValue("@Time_Stamp", Convert.ToInt32(seconds));
                cmd1.Parameters.AddWithValue("@Extra1", q_state);
                cmd1.Parameters.AddWithValue("@Extra2", "");
                cmd1.Parameters.AddWithValue("@Extra3", subtime);
                cmd1.ExecuteNonQuery();
                cmd1.Dispose();
                hdn_response.Value = "";
            }
            catch (Exception ep)
            {
                //divmain.InnerHtml = "Problem In Network, Your Test data is saved for that instance";
                //flag = "xx";
            }
            finally
            {
                con.Close();
            }
            int Q = Convert.ToInt32(btnId);
            Session["Q_No"] = Convert.ToString(Q);
            Generate_Page2(Session["Exam_No"].ToString(), Convert.ToInt32(Session["Qcount"].ToString()));
            string question_no = Session["Q_No"].ToString();
            string no_of_question = Session["Qcount"].ToString();
            back_btn.Visible = true;
            if (question_no == no_of_question)
            {
                next_btn.Visible = false;
            }
            else
            {
                next_btn.Visible = true;
            }          
        }

        protected void submitbtn_Click(object sender, EventArgs e)
        {
            //ExamResult.aspx
            Response.Redirect("ExamResult.aspx");
        }

        protected void btnNext1_Click(object sender, ImageClickEventArgs e)
        {
            string option = "X";
            string q_state = "1";
            //if(option1.Checked==true)
            //{
            //    option = "A";
            //    q_state = "2";
            //}
            //else if (option2.Checked == true)
            //{
            //    option = "B";
            //    q_state = "2";
            //}
            //else if (option3.Checked == true)
            //{
            //    option = "C";
            //    q_state = "2";
            //}
            //else if (option4.Checked == true)
            //{
            //    option = "D";
            //    q_state = "2";
            //}
            if (hdn_response.Value != "")
            {
                option = hdn_response.Value;
                q_state = "2";
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);          
            con.Open();
            try
            {
                SqlCommand cmd1 = new SqlCommand("UPDATE [EmpExam_Responsetbl] SET [EmpResponse]=@EmpResponse,[Time_Stamp]=@Time_Stamp,[Extra1]=@Extra1,[Extra2]=@Extra2,[Extra3]=@Extra3 WHERE ([Exam_ID]=@Exam_ID and [QNumber]=@QNumber)", con);
                string subtime = Convert.ToString(System.DateTime.Now);
                cmd1.Parameters.AddWithValue("@Exam_ID", Convert.ToInt32(Session["Exam_No"].ToString()));
                cmd1.Parameters.AddWithValue("@QNumber", Convert.ToInt32(Session["Q_No"].ToString()));               
                cmd1.Parameters.AddWithValue("@EmpResponse", option);              
                string time = Hidden1.Value;               
                double seconds = TimeSpan.Parse(time).TotalSeconds;
                Session["time"] = seconds;
                cmd1.Parameters.AddWithValue("@Time_Stamp", Convert.ToInt32(seconds));
                cmd1.Parameters.AddWithValue("@Extra1", q_state);
                cmd1.Parameters.AddWithValue("@Extra2", "");
                cmd1.Parameters.AddWithValue("@Extra3", subtime);
                cmd1.ExecuteNonQuery();
                cmd1.Dispose();
                hdn_response.Value = "";
            }
            catch (Exception ep)
            {
                //divmain.InnerHtml = "Problem In Network, Your Test data is saved for that instance";
                //flag = "xx";
            }
            finally
            {
                con.Close();
            }
            int Q = Convert.ToInt32(Session["Q_No"].ToString()) + 1;
            Session["Q_No"] = Convert.ToString(Q);
            Generate_Page2(Session["Exam_No"].ToString(), Convert.ToInt32(Session["Qcount"].ToString()));
            string question_no = Session["Q_No"].ToString();
            string no_of_question = Session["Qcount"].ToString();
            back_btn.Visible = true;
            if (question_no == no_of_question)
            {
                next_btn.Visible = false;
            }
            else
            {
                next_btn.Visible = true;
            }

        }

        protected void btnBack_Click(object sender, ImageClickEventArgs e)
        {
            string option = "X";
            string q_state = "1";
            //if (option1.Checked == true)
            //{
            //    option = "A";
            //    q_state = "2";
            //}
            //else if (option2.Checked == true)
            //{
            //    option = "B";
            //    q_state = "2";
            //}
            //else if (option3.Checked == true)
            //{
            //    option = "C";
            //    q_state = "2";
            //}
            //else if (option4.Checked == true)
            //{
            //    option = "D";
            //    q_state = "2";
            //}
            if (hdn_response.Value != "")
            {
                option = hdn_response.Value;
                q_state = "2";
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            // string flag = "ok";
            con.Open();
            try
            {
                SqlCommand cmd1 = new SqlCommand("UPDATE [EmpExam_Responsetbl] SET [EmpResponse]=@EmpResponse,[Time_Stamp]=@Time_Stamp,[Extra1]=@Extra1,[Extra2]=@Extra2,[Extra3]=@Extra3 WHERE ([Exam_ID]=@Exam_ID and [QNumber]=@QNumber)", con);
                string subtime = Convert.ToString(System.DateTime.Now);
                cmd1.Parameters.AddWithValue("@Exam_ID", Convert.ToInt32(Session["Exam_No"].ToString()));
                cmd1.Parameters.AddWithValue("@QNumber", Convert.ToInt32(Session["Q_No"].ToString()));
                cmd1.Parameters.AddWithValue("@EmpResponse", option);              
                string time = Hidden1.Value;              
                double seconds = TimeSpan.Parse(time).TotalSeconds;
                Session["time"] = seconds;
                cmd1.Parameters.AddWithValue("@Time_Stamp", Convert.ToInt32(seconds));
                cmd1.Parameters.AddWithValue("@Extra1", q_state);
                cmd1.Parameters.AddWithValue("@Extra2", "");
                cmd1.Parameters.AddWithValue("@Extra3", subtime);
                cmd1.ExecuteNonQuery();
                cmd1.Dispose();
                hdn_response.Value = "";
            }
            catch (Exception ep)
            {
                //divmain.InnerHtml = "Problem In Network, Your Test data is saved for that instance";
                //flag = "xx";

            }
            finally
            {
                con.Close();
            }
            int Q = Convert.ToInt32(Session["Q_No"].ToString()) - 1;
            Session["Q_No"] = Convert.ToString(Q);
            Generate_Page2(Session["Exam_No"].ToString(), Convert.ToInt32(Session["Qcount"].ToString()));
            string question_no = Session["Q_No"].ToString();
            next_btn.Visible = true;
            if (question_no == "1")
            {
                back_btn.Visible = false;
            }
            else
            {
                back_btn.Visible = true;
            }
        }
    }
}