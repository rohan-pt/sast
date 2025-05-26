using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BCCBExamPortal
{
    public partial class EOD_Admin : System.Web.UI.Page
    {
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
            if (!IsPostBack)
            {
                load_ddl();
                load_users();
            }
            else
            {
                variable_search(sender, e);
                if (ddl_category.SelectedValue != "0")
                {
                    load_eod_issues();
                }
            }
           
        }

        protected void user_btn_clk(object sender, EventArgs e)
        {
            if (user_code.Value.Trim() != "")
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);

                if (btn_ok2.InnerText == "Ok")
                {
                    user_code.Disabled = true;
                    string query = "select * from Employee_LoginTbl where Code=" + user_code.Value.Trim() + "";
                    DataTable dt = get_normal_data(query, con);
                    string output = " <div class='detail_div'>" + "Code".PadRight(20, ' ') + ": <span class='detail_info'>" + user_code.Value.Trim() + "</span></div>";
                    //Employee_Name,Designation,Email_Id,Location,Ep1,Ep2,Last_Log_In,MobileNumber,is_memo,memo_update_code
                    if (dt.Rows.Count > 0)
                    {
                        output = output + " <div class='detail_div'>" + "Name".PadRight(20, ' ') + ": <span class='detail_info'>" + dt.Rows[0]["Employee_Name"].ToString() + "</span></div>";
                        output = output + " <div class='detail_div'>" + "Designation".PadRight(20, ' ') + ": <span class='detail_info'>" + dt.Rows[0]["Designation"].ToString() + "</span></div>";

                        output = output + " <div class='detail_div'>" + "Email Id".PadRight(20, ' ') + ": <span class='detail_info'>" + dt.Rows[0]["Email_Id"].ToString() + "</span></div>";
                        output = output + " <div class='detail_div'>" + "Location".PadRight(20, ' ') + ": <span class='detail_info'>" + dt.Rows[0]["Location"].ToString() + "</span></div>";
                        output = output + " <div class='detail_div'>" + "Mobile Number".PadRight(20, ' ') + ": <span class='detail_info'>" + dt.Rows[0]["MobileNumber"].ToString() + "</span></div>";
                        output = output + " <div class='detail_div'>" + "Last Log In".PadRight(20, ' ') + ": <span class='detail_info'>" + dt.Rows[0]["Last_Log_In"].ToString() + "</span></div>";
                        string st = dt.Rows[0]["Ep2"].ToString();
                        if (st == "1")
                        {
                            st = "Active";
                        }
                        else if (st == "2")
                        {
                            st = "Retired";
                        }
                        else if (st == "3")
                        {
                            st = "Left Organization";
                        }
                        else if (st == "4")
                        {
                            st = "Terminated";
                        }
                        output = output + " <div class='detail_div'>" + "Employee Status".PadRight(20, ' ') + ": <span class='detail_info'>" + st + "</span></div>";
                        st = dt.Rows[0]["is_memo"].ToString();
                        if (st == "2")
                        {
                            st = "Yes";
                        }
                        else if (st == "3")
                        {
                            st = "Yes (Not in CBS)";
                        }
                        else if (st == "0")
                        {
                            st = "No (Not in CBS)";
                        }
                        else
                        {
                            st = "No";
                        }
                        output = output + " <div class='detail_div'>" + "Blocked".PadRight(20, ' ') + ": <span class='detail_info'>" + st + "</span></div>";
                        if (dt.Rows[0]["eod_user"].ToString() == "1")
                        {
                            eod_chk1.Checked = true;
                            if (dt.Rows[0]["eod_prevledg"].ToString() == "E")
                            {
                                eod_prev1.Checked = true;
                            }
                            else
                            {
                                eod_prev1.Checked = false;
                            }
                        }
                        else
                        {
                            eod_chk1.Checked = false;
                            eod_prev1.Checked = false;
                        }
                    }
                    else
                    {
                        output = output + " <div class='detail_div'>" + "Information".PadRight(20, ' ') + ": <span class='detail_info'>No User Found</span></div>";
                    }
                    btn_ok2.InnerText = "Edit";
                    user_details.InnerHtml = output;
                    ScriptManager.RegisterStartupScript(this, GetType(), "show_div23", "show_div2();", true);
                }
                else
                {
                    string a1 = "";
                    string a2 = "";
                    if (eod_chk1.Checked == true)
                    {
                        a1 = "1";
                    }
                    else
                    {
                        a1 = "0";
                    }

                    if (eod_prev1.Checked == true)
                    {
                        a2 = "E";
                    }
                    else
                    {
                        a2 = "";
                    }

                    string query = "update Employee_LoginTbl set eod_prevledg='" + a2 + "',eod_user='" + a1 + "' where Code=" + user_code.Value.Trim() + "";

                    query_updater(query, con);
                    btn_ok2.InnerText = "Ok";
                    user_code.Disabled = false;
                    ScriptManager.RegisterStartupScript(this, GetType(), "show_div23", "show_div2();", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage33", "special_msg3('Please check the User Code!!!');", true);
                btn_ok2.InnerText = "Ok";
                user_code.Disabled = false;
            }

        }

        private void query_updater(string query, SqlConnection con)
        {
            // SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            con.Open();
            try
            {
                SqlCommand cmd1 = new SqlCommand(query, con);
                cmd1.ExecuteNonQuery();
                cmd1.Dispose();
                con.Close();
                user_details.InnerHtml = "";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage35", "special_msg3('User Record Updated!!!');", true);
            }
            catch (Exception ep1)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage34", "special_msg3(" + ep1.Message + ");", true);
            }
            finally
            {
                con.Close();
            }
        }
        private void load_users()
        {
            string query = "select * from Employee_LoginTbl where eod_user=1";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            DataTable dt = get_normal_data(query, con);
            query = "Nothing to display";
            if (dt.Rows.Count > 0)
            {
                query = "<table class='user_tbl'>";
                query += " <tr> " +
" <td class='user_td1'>SR no</td> " +
" <td class='user_td2'>Code</td> " +
" <td class='user_td2'>Name</td> " +
" <td class='user_td2'>Location</td> " +
" </tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    query += " <tr> " +
" <td class='user_td1_1'>" + (i + 1) + "</td> " +
" <td class='user_td2_1'>" + dt.Rows[i]["Code"].ToString() + "</td> " +
" <td class='user_td2_1'>" + dt.Rows[i]["Employee_Name"].ToString() + "</td> " +
" <td class='user_td2_1'>" + dt.Rows[i]["Location"].ToString() + "</td> " +
" </tr>";
                }
            }
            user_table.InnerHtml = query + "</table>";
        }



        protected void insert_new_record(object sender, EventArgs e)
        {
            if (txt_variable.Value != "" && textarea1.Value != "" && ddl_commands.SelectedItem.Text != "--Please Select Command--")
            {
                string query = "";
                string msg = "";
                if (btn_ok.InnerText == "OK")
                {
                    query = "insert into COMMAND_VARIABLE (VARIABLE,USES,F_ID,CREATOR,CREATE_DATE,VAR_STAT) values('" + txt_variable.Value + "','" + textarea1.Value + "','" + hdn_fID.Value + "','" + sessionlbl.Text + "',sysdate,'1')";
                    msg = "Record Inserted Successfully!!!";
                }
                else
                {
                    query = "update COMMAND_VARIABLE set VARIABLE='" + txt_variable.Value + "',USES='" + textarea1.Value + "',EDITOR='" + sessionlbl.Text + "',EDIT_DATE=sysdate where ID='" + hdn_ID.Value + "'";
                    msg = "Record Updated Successfully!!!";
                }
                string ConString = "Data Source=(DESCRIPTION =" +
       "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.197)(PORT = 1522))" +
       "(CONNECT_DATA =" +
         "(SERVER = DEDICATED)" +
         "(SERVICE_NAME = FCPROD)));" +
         "User Id=APPLOGS;Password=A#9pLO7g$2o23;Connection Timeout=600; Max Pool Size=150;";
                execute_query(ConString, query, msg);
                hdn_ID.Value = "";
                hdn_fID.Value = "";
                txt_variable.Value = "";
                textarea1.Value = "";
                btn_ok.InnerText = "OK";
                variable_search(sender, e);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "special_msg2('Please check the Inputs Provided!!!');", true);
            }
        }


        protected void execute_query(string ConString, string query, string msg)
        {
            OracleConnection con = new OracleConnection(ConString);
            con.Open();
            OracleCommand command = new OracleCommand(query, con);
            try
            {
                command.ExecuteNonQuery();
                command.Connection.Close();
                con.Close();
                ScriptManager.RegisterStartupScript(this, GetType(), "special_msg22", "special_msg2('" + msg + "');", true);
            }
            catch (Exception ep)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "special_msg23", "special_msg2('" + ep.Message + "');", true);
            }
            finally
            {
                con.Close();
            }
        }


        protected void execute_query_2(string ConString, string query, string msg)
        {
            OracleConnection con = new OracleConnection(ConString);
            con.Open();
            OracleCommand command = new OracleCommand(query, con);
            try
            {
                command.ExecuteNonQuery();
                command.Connection.Close();
                con.Close();
                //ScriptManager.RegisterStartupScript(this, GetType(), "special_msg22", "special_msg2('" + msg + "');", true);
            }
            catch (Exception ep)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "special_msg23", "special_msg2('" + ep.Message + "');", true);
            }
            finally
            {
                con.Close();
            }
        }





        protected void variable_search(object sender, EventArgs e)
        {
            if (ddl_commands.SelectedItem.Text != "--Please Select Command--")
            {
                hdn_fID.Value = ddl_commands.SelectedValue.ToString();
                string query = " select a.COMMANDNAME,a.INFORMATION, b.ID, " +
    " b.VARIABLE  " +
    " from commands a  " +
    " inner join COMMAND_VARIABLE b  " +
    " on a.ID=b.F_ID  " +
    " where b.var_stat=1 and a.COMMANDNAME='" + ddl_commands.SelectedItem.Text + "'  ";
                string ConString = "Data Source=(DESCRIPTION =" +
               "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.197)(PORT = 1522))" +
               "(CONNECT_DATA =" +
                 "(SERVER = DEDICATED)" +
                 "(SERVICE_NAME = FCPROD)));" +
                 "User Id=APPLOGS;Password=A#9pLO7g$2o23;Connection Timeout=600; Max Pool Size=150;";
                DataTable dt = get_oracle_data(query, ConString);
                tbl_values.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    TableRow tRow = new TableRow();
                    TableCell tb = new TableCell();
                    tb.CssClass = "tab";
                    tb.Style.Add("width", "5%");
                    tb.Text = "<b>SR No</b>";

                    TableCell tb4 = new TableCell();
                    tb4.CssClass = "tab";
                    tb4.Style.Add("width", "30%");
                    tb4.Text = "<b>Command Name</b>";

                    TableCell tb7 = new TableCell();
                    tb7.CssClass = "tab";
                    tb7.Style.Add("width", "25%");
                    tb7.Text = "<b>Command Variable</b>";

                    TableCell tb5 = new TableCell();
                    tb5.CssClass = "tab";
                    tb5.Style.Add("width", "20%");
                    tb5.Text = "<b>Edit</b>";
                    TableCell tb6 = new TableCell();
                    tb6.CssClass = "tab";
                    tb6.Style.Add("width", "20%");
                    tb6.Text = "<b>Delete</b>";

                    tRow.Controls.Add(tb);
                    tRow.Controls.Add(tb4);
                    tRow.Controls.Add(tb7);
                    tRow.Controls.Add(tb5);
                    tRow.Controls.Add(tb6);
                    tbl_values.Rows.Add(tRow);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TableRow tr = new TableRow();

                        TableCell td = new TableCell();
                        td.CssClass = "celltab";
                        td.Style.Add("width", "5%");
                        td.Text = Convert.ToString(i + 1);

                        TableCell td2 = new TableCell();
                        td2.CssClass = "celltab";
                        td2.Style.Add("width", "30%");
                        td2.Text = dt.Rows[i]["COMMANDNAME"].ToString();

                        TableCell td7 = new TableCell();
                        td7.CssClass = "celltab";
                        td7.Style.Add("width", "25%");
                        td7.Text = dt.Rows[i]["VARIABLE"].ToString(); ;


                        TableCell td4 = new TableCell();
                        td4.CssClass = "celltab";
                        td4.Style.Add("width", "20%");
                        Button bt1 = new Button();
                        bt1.Text = "Edit";
                        bt1.CssClass = "cellbtn";
                        bt1.Attributes.Add("runat", "server");
                        bt1.Attributes["type"] = "button";
                        bt1.ID = "Edit_" + dt.Rows[i]["ID"].ToString();
                        bt1.Click += new EventHandler(bt1_Click);
                        td4.Controls.Add(bt1);


                        TableCell td5 = new TableCell();
                        td5.CssClass = "celltab";
                        td5.Style.Add("width", "20%");
                        Button bt2 = new Button();
                        bt2.Text = "Delete";
                        bt2.CssClass = "cellbtn";
                        bt2.Attributes["type"] = "button";
                        bt2.Attributes.Add("runat", "server");
                        bt2.ID = "Del_" + dt.Rows[i]["ID"].ToString();
                        bt2.Click += new EventHandler(bt3_Click);
                        td5.Controls.Add(bt2);


                        tr.Controls.Add(td);
                        tr.Controls.Add(td2);
                        tr.Controls.Add(td7);
                        tr.Controls.Add(td4);
                        tr.Controls.Add(td5);
                        tbl_values.Rows.Add(tr);

                    }

                }
            }
            else
            {
                hdn_fID.Value = "";
            }

        }

        protected void bt1_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            string[] btnId = b.ID.Split('_');
            string query = "select * from  COMMAND_VARIABLE where ID = '" + btnId[1] + "'";
            string ConString = "Data Source=(DESCRIPTION =" +
              "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.197)(PORT = 1522))" +
              "(CONNECT_DATA =" +
                "(SERVER = DEDICATED)" +
                "(SERVICE_NAME = FCPROD)));" +
                "User Id=APPLOGS;Password=A#9pLO7g$2o23;Connection Timeout=600; Max Pool Size=150;";
            DataTable dt = get_oracle_data(query, ConString);
            if (dt.Rows.Count > 0)
            {
                hdn_ID.Value = dt.Rows[0]["ID"].ToString();
                hdn_fID.Value = dt.Rows[0]["F_ID"].ToString();
                txt_variable.Value = dt.Rows[0]["VARIABLE"].ToString();
                textarea1.Value = dt.Rows[0]["USES"].ToString();
                btn_ok.InnerText = "Update Record";
            }
        }

        protected void bt3_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            string[] btnId = b.ID.Split('_');
            string query = "update COMMAND_VARIABLE set var_stat='0',EDITOR='" + sessionlbl.Text + "',EDIT_DATE=sysdate where ID = '" + btnId[1] + "'";
            string ConString = "Data Source=(DESCRIPTION =" +
             "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.197)(PORT = 1522))" +
             "(CONNECT_DATA =" +
               "(SERVER = DEDICATED)" +
               "(SERVICE_NAME = FCPROD)));" +
               "User Id=APPLOGS;Password=A#9pLO7g$2o23;Connection Timeout=600; Max Pool Size=150;";
            execute_query(ConString, query, "Record Deleted Successfully!!!");
            variable_search(sender, e);
        }

        protected void load_ddl()
        {
            string query = "select ID,COMMANDNAME from commands order by ID asc";
            string ConString = "Data Source=(DESCRIPTION =" +
            "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.197)(PORT = 1522))" +
            "(CONNECT_DATA =" +
              "(SERVER = DEDICATED)" +
              "(SERVICE_NAME = FCPROD)));" +
              "User Id=APPLOGS;Password=A#9pLO7g$2o23;Connection Timeout=600; Max Pool Size=150;";
            DataTable dt = get_oracle_data(query, ConString);

            ddl_commands.Items.Clear();
            ddl_commands.DataSource = dt;
            ddl_commands.DataBind();
            ddl_commands.DataTextField = "COMMANDNAME";
            ddl_commands.DataValueField = "ID";
            ddl_commands.DataBind();
            ddl_commands.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Please Select Command--", "0"));
        }

        private DataTable get_oracle_data(string query, string ConString)
        {
            DataTable dt = new DataTable();
            OracleConnection con = new OracleConnection(ConString);
            try
            {
                con.Open();
                OracleCommand cmd = new OracleCommand(query, con);
                OracleDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                int l = dt.Rows.Count;
                con.Close();
            }
            catch (Exception yy)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "special_msg2('" + yy.Message + "');", true);
            }
            finally
            {
                con.Close();
            }
            return dt;


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
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "special_msg2('Unable to fetch Customers details due to Network issue:');", true);
            }
            finally
            {
                con.Close();
            }

            return dt;
        }
        protected void btnAT_Click(object sender, EventArgs e)
        {
            Response.Redirect("Admin.aspx?ENo=0&Menu=2");
        }

        protected void btnATR_Click(object sender, EventArgs e)
        {
            Response.Redirect("Admin.aspx?ENo=0&Menu=3");
        }

        protected void EOD_Click(object sender, EventArgs e)
        {
            Response.Redirect("EOD_Admin.aspx");
        }

        protected void btnAnalys_Click(object sender, EventArgs e)
        {
            Response.Redirect("Analysis.aspx?");
        }

        protected void btnSettings_Click(object sender, EventArgs e)
        {
            Response.Redirect("Settings.aspx?ENo=0");
        }
        protected void general_Click(object sender, EventArgs e)
        {
            Response.Redirect("ReportSettings.aspx");
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("EmployeeSettings.aspx");
        }

        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("LogIn.aspx");
        }

        protected void addmarquee_Click(object sender, EventArgs e)
        {
            Response.Redirect("Admin_Marquee.aspx");
        }

        protected void btnTNT_Click(object sender, EventArgs e)
        {
            Response.Redirect("Admin.aspx?ENo=0");
        }
        protected void bt2_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            string btnId = b.ID;
            Response.Redirect("Admin_Marquee.aspx?CID=" + btnId);

        }

        protected void web_Click(object sender, EventArgs e)
        {
            Response.Redirect("ConnectionString.aspx");
        }
        protected void grp_tbl_Click(object sender, EventArgs e)
        {
            Response.Redirect("Graph_and_Table.aspx");
        }
        protected void general_users(object sender, EventArgs e)
        {
            Response.Redirect("Active_Users.aspx");
        }

        protected void mini_ledge_clk(object sender, EventArgs e)
        {
            Response.Redirect("Short.aspx");
        }

        protected void new_issue_clk(object sender, EventArgs e)
        {
            string msg = "";
            string title = "DATA Error in Record Submission";
            int p = 0;
            if (sys_date.Text == "")
            {
                msg = "-- Please provide System Date";
                p = 1;
            }
            if (pro_date.Text == "")
            {
                msg = msg + "</br>-- Please Provide Process Date";
                p = 1;
            }
            if (ddl_category.SelectedValue == "0")
            {
                msg = msg + "</br>-- Please Provide Batch Category";
                p = 1;
            }
            if (ddl_issue.SelectedValue == "0")
            {
                msg = msg + "</br>-- Please Provide Issue Type";
                p = 1;
            }
            if (txt_shell_name.Value == "")
            {
                msg = msg + "</br>-- Please Provide Shell Name";
                p = 1;
            }
            if (txt_issue_des.Value == "")
            {
                msg = msg + "</br>-- Please Provide Issue Description";
                p = 1;
            }
            if (txt_resolution.Value == "")
            {
                msg = msg + "</br>-- Please Provide Issue Resolution";
                p = 1;
            }
            if (tct_note.Value == "")
            {
                msg = msg + "</br>-- Please Provide Issue Note";
                p = 1;
            }
            if (ddl_status.SelectedValue == "0")
            {
                msg = msg + "</br>-- Please Provide Issue Status";
                p = 1;
            }
            if (p == 0)
            {
                string query = "";
                title = "DATA Submission";
                string ConString = "Data Source=(DESCRIPTION =" +
     "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.197)(PORT = 1522))" +
     "(CONNECT_DATA =" +
       "(SERVER = DEDICATED)" +
       "(SERVICE_NAME = FCPROD)));" +
       "User Id=APPLOGS;Password=A#9pLO7g$2o23;Connection Timeout=600; Max Pool Size=150;";
                if (new_issue.InnerText == "OK")
                {
                    query = "insert into EOD_ISSUES (sydate,pro_date,category,issue_category,shell_name,issue_des,issue_res,note,owner,status,c_code,c_date) " +
                        " values(date'" + sys_date.Text + "',date'" + pro_date.Text + "',:category,:issue_category,:shell_name,:issue_des,:issue_res,:note,:owner,:status,:c_code,sysdate)";
                    OracleConnection con = new OracleConnection(ConString);
                    con.Open();
                    OracleCommand command = new OracleCommand(query, con);                  
                    command.Parameters.Add(new OracleParameter("category", ddl_category.SelectedItem.Text.ToString()));
                    command.Parameters.Add(new OracleParameter("issue_category", ddl_issue.SelectedItem.Text.ToString()));
                    command.Parameters.Add(new OracleParameter("shell_name", txt_shell_name.Value));
                    command.Parameters.Add(new OracleParameter("issue_des", txt_issue_des.Value));
                    command.Parameters.Add(new OracleParameter("issue_res", txt_resolution.Value));
                    command.Parameters.Add(new OracleParameter("note", tct_note.Value));
                    command.Parameters.Add(new OracleParameter("owner", ddl_owner.SelectedItem.Text.ToString()));
                    command.Parameters.Add(new OracleParameter("status", ddl_status.SelectedItem.Text.ToString()));
                    command.Parameters.Add(new OracleParameter("c_code", sessionlbl.Text));
                    try
                    {
                        command.ExecuteNonQuery();
                        command.Connection.Close();
                        con.Close();
                        msg = "Record Inserted Successfully!!!";
                    }
                    catch (Exception ep)
                    {
                        msg = ep.Message;
                    }
                    finally
                    {
                        con.Close();
                    }
                }
                else
                {
                    query = "update EOD_ISSUES set sydate=date'" + sys_date.Text + "',pro_date=date'" + pro_date.Text + "',category=:category" +
                        ",issue_category=:issue_category,shell_name=:shell_name,issue_des=:issue_des,issue_res=:issue_res,note=:note,owner=:owner,status=:status,e_code=:c_code,e_date=sysdate where ID='" + hdn_matter.Value + "'";

                    OracleConnection con = new OracleConnection(ConString);
                    con.Open();
                    OracleCommand command = new OracleCommand(query, con);
                   
                    command.Parameters.Add(new OracleParameter("category", ddl_category.SelectedItem.Text.ToString()));
                    command.Parameters.Add(new OracleParameter("issue_category", ddl_issue.SelectedItem.Text.ToString()));
                    command.Parameters.Add(new OracleParameter("shell_name", txt_shell_name.Value));
                    command.Parameters.Add(new OracleParameter("issue_des", txt_issue_des.Value));
                    command.Parameters.Add(new OracleParameter("issue_res", txt_resolution.Value));
                    command.Parameters.Add(new OracleParameter("note", tct_note.Value));
                    command.Parameters.Add(new OracleParameter("owner", ddl_owner.SelectedItem.Text.ToString()));
                    command.Parameters.Add(new OracleParameter("status", ddl_status.SelectedItem.Text.ToString()));
                    command.Parameters.Add(new OracleParameter("c_code", sessionlbl.Text));
                    try
                    {
                        command.ExecuteNonQuery();
                        command.Connection.Close();
                        con.Close();
                        msg = "Record Updated Successfully!!!";
                    }
                    catch (Exception ep)
                    {
                        msg = ep.Message;
                    }
                    finally
                    {
                        con.Close();
                    }
                  
                }
              

                new_issue.InnerText = "OK";
                sys_date.Text = "";
                pro_date.Text = "";
                //ddl_category.SelectedValue = "0";
                ddl_issue.SelectedValue = "0";
                txt_shell_name.Value = "";
                txt_issue_des.Value = "";
                txt_resolution.Value = "";
                tct_note.Value = "";
                ddl_owner.SelectedValue = "0";
                ddl_status.SelectedValue = "0";
                hdn_matter.Value = "";
                ddl_category.Enabled = true;
               // execute_query_2(ConString, query, msg);
                ddl_category_load(sender,e);
            }



            ScriptManager.RegisterStartupScript(this, GetType(), "showscreen11m", "showscreenmm('" + title + "','" + msg + "');", true);

        }

        protected void ddl_category_load(object sender, EventArgs e)
        {
            if(ddl_category.SelectedValue!="0")
            {
                load_eod_issues();
            }
            else
            {
                ActiArchtbl.Rows.Clear();
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "show_div356", "show_div3();", true);
        }

        protected void bt_load_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            string[] btnId = b.ID.Split('_');
            string query = "select ID,to_char(sydate,'YYYY-MM-DD') as sydate,to_char(pro_date,'YYYY-MM-DD') as pro_date,category,status,owner,note,issue_res,issue_category,shell_name,issue_des from EOD_ISSUES where ID='" + btnId[1].ToString() + "'";
            string ConString = "Data Source=(DESCRIPTION =" +
           "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.197)(PORT = 1522))" +
           "(CONNECT_DATA =" +
             "(SERVER = DEDICATED)" +
             "(SERVICE_NAME = FCPROD)));" +
             "User Id=APPLOGS;Password=A#9pLO7g$2o23;Connection Timeout=600; Max Pool Size=150;";
            DataTable dt = get_oracle_data(query, ConString);
            string p = "";
            try
            {
                if (dt.Rows.Count > 0)
                {
                    // sys_date.Text = Convert.ToDateTime(dt.Rows[0]["sydate"].ToString()).ToString("yyyy-MM-dd");
                    p = dt.Rows[0]["sydate"].ToString();
                    sys_date.Text = dt.Rows[0]["sydate"].ToString();
                   
                    // pro_date.Text = Convert.ToDateTime(dt.Rows[0]["pro_date"].ToString()).ToString("yyyy-MM-dd");
                    pro_date.Text = dt.Rows[0]["pro_date"].ToString();
                    ddl_category.SelectedValue = ddl_category.Items.FindByText(dt.Rows[0]["category"].ToString()).Value;
                    ddl_issue.SelectedValue = ddl_issue.Items.FindByText(dt.Rows[0]["issue_category"].ToString()).Value;
                    txt_shell_name.Value = dt.Rows[0]["shell_name"].ToString();
                    txt_issue_des.Value = dt.Rows[0]["issue_des"].ToString();
                    txt_resolution.Value = dt.Rows[0]["issue_res"].ToString();
                    tct_note.Value = dt.Rows[0]["note"].ToString();
                    ddl_owner.SelectedValue = ddl_owner.Items.FindByText(dt.Rows[0]["owner"].ToString()).Value;
                    ddl_status.SelectedValue = ddl_status.Items.FindByText(dt.Rows[0]["status"].ToString()).Value;
                    hdn_matter.Value = dt.Rows[0]["Id"].ToString();
                    new_issue.InnerHtml = "Edit";
                    ddl_category.Enabled = false;
                }
                else
                {
                    new_issue.InnerHtml = "OK";
                    hdn_matter.Value = "";
                    ddl_category.Enabled = true;
                }
            }
            catch(Exception ep)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "special_msg2('" + ep.Message + ": " + p + "');", true);
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "show_div356", "show_div3();", true);
        }

        protected void load_eod_issues()
        {
            string query = "select * from EOD_ISSUES where category='" + ddl_category.SelectedItem.Text.Trim().ToString() + "'";
            string ConString = "Data Source=(DESCRIPTION =" +
           "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.197)(PORT = 1522))" +
           "(CONNECT_DATA =" +
             "(SERVER = DEDICATED)" +
             "(SERVICE_NAME = FCPROD)));" +
             "User Id=APPLOGS;Password=A#9pLO7g$2o23;Connection Timeout=600; Max Pool Size=150;";
            ActiArchtbl.Rows.Clear();
            DataTable dt = get_oracle_data(query, ConString);
            if (dt.Rows.Count > 0)
            {               
                TableRow tRow = new TableRow();
                TableCell tbh = new TableCell();
                tbh.CssClass = "c_tab_td11";
                tbh.Text = "<b>SR No</b>";

                TableCell tbh1 = new TableCell();
                tbh1.CssClass = "c_tab_td12";
                tbh1.Text = "<b>System Date</b>";

                TableCell tbh2 = new TableCell();
                tbh2.CssClass = "c_tab_td12";
                tbh2.Text = "<b>Process Date</b>";

                TableCell tbh3 = new TableCell();
                tbh3.CssClass = "c_tab_td12";
                tbh3.Text = "<b>Category</b>";

                TableCell tbh4 = new TableCell();
                tbh4.CssClass = "c_tab_td12";
                tbh4.Text = "<b>Creator</b>";

                TableCell tbh5 = new TableCell();
                tbh5.CssClass = "c_tab_td12";
                tbh5.Text = "<b>Shell Name</b>";

                TableCell tbh6 = new TableCell();
                tbh6.CssClass = "c_tab_td11";
                tbh6.Text = "<b>#</b>";

                tRow.Controls.Add(tbh);
                tRow.Controls.Add(tbh1);
                tRow.Controls.Add(tbh2);
                tRow.Controls.Add(tbh4);
                tRow.Controls.Add(tbh5);
                tRow.Controls.Add(tbh6);

                ActiArchtbl.Rows.Add(tRow);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TableRow tRow1 = new TableRow();
                    TableCell tb = new TableCell();
                    tb.CssClass = "c_tab_td21";
                    tb.Text = "<b>" + (i + 1) + "</b>";

                    TableCell tb1 = new TableCell();
                    tb1.CssClass = "c_tab_td22";
                    tb1.Text = "<b>" + Convert.ToDateTime(dt.Rows[i]["SYDATE"].ToString()).ToString("yyyy-MM-dd") + "</b>";

                    TableCell tb2 = new TableCell();
                    tb2.CssClass = "c_tab_td22";
                    tb2.Text = "<b>" + Convert.ToDateTime(dt.Rows[i]["PRO_DATE"].ToString()).ToString("yyyy-MM-dd") + "</b>";

                    TableCell tb3 = new TableCell();
                    tb3.CssClass = "c_tab_td22";
                    string jj = "" + dt.Rows[i]["C_CODE"].ToString() + " || " + Convert.ToDateTime(dt.Rows[i]["C_date"].ToString()).ToString("yyyy-MM-dd") + "</br>";
                    if(dt.Rows[i]["E_CODE"].ToString()!="")
                    {
                        jj = jj + "" + dt.Rows[i]["E_CODE"].ToString() + " || " + Convert.ToDateTime(dt.Rows[i]["E_date"].ToString()).ToString("yyyy-MM-dd");
                    }
                    tb3.Text = "<b>" + jj + "</b>";
                 

                    TableCell tb5 = new TableCell();
                    tb5.CssClass = "c_tab_td22";
                    tb5.Text = "<b>" + dt.Rows[i]["SHELL_NAME"].ToString() + "</b>";

                 

                    TableCell tb6 = new TableCell();
                    tb6.CssClass = "c_tab_td21";                  
                    Button bt1 = new Button();
                    bt1.Text = "Edit";
                    bt1.CssClass = "c_btn";
                    bt1.Attributes.Add("runat", "server");
                    bt1.ID = "Edit_" + dt.Rows[i]["ID"].ToString();
                    bt1.Click += new EventHandler(bt_load_Click);
                    tb6.Controls.Add(bt1);


                    tRow1.Controls.Add(tb);
                    tRow1.Controls.Add(tb1);
                    tRow1.Controls.Add(tb2);
                    tRow1.Controls.Add(tb3);
                    tRow1.Controls.Add(tb5);
                    tRow1.Controls.Add(tb6);

                    ActiArchtbl.Rows.Add(tRow1);
                }
            }
            else
            {
                TableRow tRow = new TableRow();
                TableCell tbh = new TableCell();
                tbh.CssClass = "c_tab_td11";
                tbh.Text = "<b>No record found</b>";
                ActiArchtbl.Rows.Add(tRow);
            }

        }

    }
}