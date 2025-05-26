using System;
using System.Collections.Generic;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Sockets;

namespace BCCBExamPortal
{
    public partial class Short : System.Web.UI.Page
    {
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
            fetch_data();
            int r = fetch_sub_data(hdn_sub_id.Value);
            sbu_info_desk.InnerHtml = "<p class='p'>" + "Total Sub GL Count".PadRight(30, '.') + ": " + r.ToString().PadRight(30, ' ') + "</p>";
        }

        protected void btnTNT_Click(object sender, EventArgs e)
        {

            Response.Redirect("Admin.aspx?ENo=0");
        }

        protected void btnAT_Click(object sender, EventArgs e)
        {

            Response.Redirect("Admin.aspx?ENo=0&Menu=2");

        }

        protected void mini_ledge_clk(object sender, EventArgs e)
        {
            Response.Redirect("Short.aspx");
        }
        protected void btnATR_Click(object sender, EventArgs e)
        {
            Response.Redirect("Admin.aspx?ENo=0&Menu=3");
        }

        protected void btnAnalys_Click(object sender, EventArgs e)
        {
            Response.Redirect("Analysis.aspx?");
        }

        protected void general_Click(object sender, EventArgs e)
        {
            Response.Redirect("Maintain_Report.aspx");
        }
        protected void grp_tbl_Click(object sender, EventArgs e)
        {
            Response.Redirect("Graph_and_Table.aspx");
        }
        protected void general_users(object sender, EventArgs e)
        {
            Response.Redirect("Active_Users.aspx");
        }

        protected void btnSettings_Click(object sender, EventArgs e)
        {
            Response.Redirect("Settings.aspx?ENo=0");
        }

        void btn_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            //Response.Redirect("Admin.aspx?ENo=" + EIdlabel.Text + "&QNo=" + b.Text + "");
        }
        void bt2_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            string btnId = b.ID;
            string[] values = btnId.Split('_');
            Response.Redirect("EditTest.aspx?ENo=" + values[1].ToString());


        }
        void bt1_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            string btnId = b.ID;
            string[] values = btnId.Split('_');
            Session["Eno"] = values[1].ToString();
            Session["SS"] = "2";
            Response.Redirect("AnalysisTest.aspx?XNo=2");

        }
        void bt3_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            string btnId = b.ID;
            string[] values = btnId.Split('_');
            Session["Eno"] = values[1].ToString();
            Session["SS"] = "3";
            Response.Redirect("AnalysisTest.aspx?XNo=3");

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("EmployeeSettings.aspx");
        }

        protected void addmarquee_Click(object sender, EventArgs e)
        {
            Response.Redirect("Admin_Marquee.aspx");
        }

        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("LogIn.aspx");
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

            }
            finally
            {
                con.Close();
            }
            return dt;
        }


        private void fetch_data()
        {
            tbl_xx1.Rows.Clear();
            if (ddl_category.SelectedItem.Text != "--PLEASE SELECT CATEGORY OF GLs--")
            {
                string query = "select * from SBS_MASTER_TBL where NODE_STAT='1' and TYPE='" + ddl_category.SelectedItem.Text + "' order by ID";
                string ConString = "Data Source=(DESCRIPTION =" +
            "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.197)(PORT = 1522))" +
            "(CONNECT_DATA =" +
              "(SERVER = DEDICATED)" +
              "(SERVICE_NAME = FCPROD)));" +
              "User Id=APPLOGS;Password=A#9pLO7g$2o23;Connection Timeout=600; Max Pool Size=150;";
                DataTable dt = get_oracle_data(query, ConString);
              
                tot_cnt.InnerHtml = dt.Rows.Count.ToString();              
                if (dt.Rows.Count > 0)
                {
                    TableRow tRow = new TableRow();
                    TableCell tb = new TableCell();
                    tb.CssClass = "std_td d1";
                    tb.Text = "<b>SR No</b>";

                    TableCell tb3 = new TableCell();
                    tb3.CssClass = "std_td d2";
                    tb3.Text = "<b>GL Name</b>";

                    TableCell tb4 = new TableCell();
                    tb4.CssClass = "std_td d3";
                    tb4.Text = "<b>Creator/Editor</b>";

                    TableCell tb5 = new TableCell();
                    tb5.CssClass = "std_td d3";
                    tb5.Text = "<b>Create/Edit date</b>";
                    TableCell tb6 = new TableCell();
                    tb6.CssClass = "std_td d4";
                    tb6.Text = "<b>AB1</b>";

                    TableCell tb7 = new TableCell();
                    tb7.CssClass = "std_td d4";
                    tb7.Text = "<b>AB2</b>";
                    TableCell tb8 = new TableCell();
                    tb8.CssClass = "std_td d4";
                    tb8.Text = "<b>AB3</b>";

                    tRow.Controls.Add(tb);
                    tRow.Controls.Add(tb3);
                    tRow.Controls.Add(tb4);
                    tRow.Controls.Add(tb5);
                    tRow.Controls.Add(tb6);
                    tRow.Controls.Add(tb7);
                    tRow.Controls.Add(tb8);
                    tbl_xx1.Rows.Add(tRow);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TableRow tr = new TableRow();
                        TableCell td = new TableCell();
                        td.CssClass = "std_td1 d1";
                        td.Text = Convert.ToString(i + 1);
                        TableCell td1 = new TableCell();
                        td1.CssClass = "std_td1 d2";
                        td1.Text = dt.Rows[i]["NAME"].ToString();
                        TableCell td2 = new TableCell();
                        td2.CssClass = "std_td1 d3";
                        string vc = dt.Rows[i]["CREATOR"].ToString();
                        string tc = Convert.ToDateTime(dt.Rows[i]["CREATE_DATE"].ToString()).ToShortDateString();
                        if (dt.Rows[i]["EDITOR"].ToString() != "")
                        {
                            vc = vc + "/" + dt.Rows[i]["EDITOR"].ToString();
                            tc = tc + "/" + Convert.ToDateTime(dt.Rows[i]["EDIT_DATE"].ToString()).ToShortDateString();
                        }
                        td2.Text = vc;
                        TableCell td3 = new TableCell();
                        td3.CssClass = "std_td1 d3";
                        td3.Text = tc;
                        TableCell td4 = new TableCell();
                        td4.CssClass = "std_td1 d4";
                        TableCell td5 = new TableCell();
                        td5.CssClass = "std_td1 d4";
                        TableCell td6 = new TableCell();
                        td6.CssClass = "std_td1 d4";
                        Button bt2 = new Button();
                        bt2.Text = "Edit";
                        bt2.CssClass = "n_btn";
                        bt2.Attributes["type"] = "button";
                        bt2.Attributes.Add("runat", "server");
                        bt2.ID = "Edit_" + dt.Rows[i]["ID"].ToString();
                        bt2.Click += new EventHandler(Edit_click);
                        td4.Controls.Add(bt2);
                        Button bt1 = new Button();
                        bt1.Text = "Delete";
                        bt1.CssClass = "n_btn";
                        bt1.Attributes["type"] = "button";
                        bt1.Attributes.Add("runat", "server");
                        bt1.ID = "Delete_" + dt.Rows[i]["ID"].ToString();
                        bt1.Click += new EventHandler(delete_click);
                        td5.Controls.Add(bt1);

                        Button bt3 = new Button();
                        bt3.Text = "Add Sub GL";
                        bt3.CssClass = "n_btn";
                        bt3.Attributes["type"] = "button";
                        bt3.Attributes.Add("runat", "server");
                        bt3.ID = "ASG_" + dt.Rows[i]["ID"].ToString();
                        bt3.Click += new EventHandler(Sub_gl_click);
                        td6.Controls.Add(bt3);

                        tr.Controls.Add(td);
                        tr.Controls.Add(td1);
                        tr.Controls.Add(td2);
                        tr.Controls.Add(td3);
                        tr.Controls.Add(td4);
                        tr.Controls.Add(td5);
                        tr.Controls.Add(td6);
                        tbl_xx1.Rows.Add(tr);
                    }
                }
                else
                {
                    TableRow tRow = new TableRow();
                    TableCell tb = new TableCell();
                    tb.CssClass = "std_td1 d2";
                    tb.Text = "<b>No Record Available</b>";
                    tRow.Controls.Add(tb);
                    tbl_xx1.Rows.Add(tRow);
                }
            }
            else
            {
                TableRow tRow = new TableRow();
                TableCell tb = new TableCell();
                tb.CssClass = "std_td1 d2";
                tb.Text = "<b>No Record Available</b>";
                tRow.Controls.Add(tb);
                tbl_xx1.Rows.Add(tRow);
            }
        }
        private int fetch_sub_data(string id)
        {
            ActiArchtbl.Rows.Clear();
            int r = 0;
            if (id != "")
            {
                string query = "select b.s_id,a.TYPE,a.CURRENCY,a.NAME,b.CREATE_DATE,b.CREATOR,b.EDITOR,b.EDIT_DATE,b.GL_LOGIC,b.S_NAME,b.C_NAME from SBS_MASTER_TBL a left join SBS_SUB_GL_TABLE b on a.ID=b.R_ID where g_stat=1 and a.ID=" + id + "";
                string ConString = "Data Source=(DESCRIPTION =" +
            "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.197)(PORT = 1522))" +
            "(CONNECT_DATA =" +
              "(SERVER = DEDICATED)" +
              "(SERVICE_NAME = FCPROD)));" +
              "User Id=APPLOGS;Password=A#9pLO7g$2o23;Connection Timeout=600; Max Pool Size=150;";
                DataTable dt = get_oracle_data(query, ConString);
                r = dt.Rows.Count;
                if (dt.Rows.Count > 0)
                {
                    TableRow tRow = new TableRow();
                    TableCell tb = new TableCell();
                    tb.CssClass = "std_td d1";
                    tb.Text = "<b>SR No</b>";

                    TableCell tb3 = new TableCell();
                    tb3.CssClass = "std_td d2";
                    tb3.Text = "<b>SUB GL Name</b>";

                    TableCell tb4 = new TableCell();
                    tb4.CssClass = "std_td d3";
                    tb4.Text = "<b>Creator/Editor</b>";

                    TableCell tb5 = new TableCell();
                    tb5.CssClass = "std_td d3";
                    tb5.Text = "<b>Create/Edit date</b>";
                    TableCell tb6 = new TableCell();
                    tb6.CssClass = "std_td d3";
                    tb6.Text = "<b>AB1</b>";

                    TableCell tb7 = new TableCell();
                    tb7.CssClass = "std_td d3";
                    tb7.Text = "<b>AB2</b>";


                    tRow.Controls.Add(tb);
                    tRow.Controls.Add(tb3);
                    tRow.Controls.Add(tb4);
                    tRow.Controls.Add(tb5);
                    tRow.Controls.Add(tb6);
                    tRow.Controls.Add(tb7);

                    ActiArchtbl.Rows.Add(tRow);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TableRow tr = new TableRow();
                        TableCell td = new TableCell();
                        td.CssClass = "std_td1 d1";
                        td.Text = Convert.ToString(i + 1);
                        TableCell td1 = new TableCell();
                        td1.CssClass = "std_td1 d2";
                        td1.Text = dt.Rows[i]["S_NAME"].ToString();
                        TableCell td2 = new TableCell();
                        td2.CssClass = "std_td1 d3";
                        string vc = dt.Rows[i]["CREATOR"].ToString();
                        string tc = Convert.ToDateTime(dt.Rows[i]["CREATE_DATE"].ToString()).ToShortDateString();
                        if (dt.Rows[i]["EDITOR"].ToString() != "")
                        {
                            vc = vc + "/" + dt.Rows[i]["EDITOR"].ToString();
                            tc = tc + "/" + Convert.ToDateTime(dt.Rows[i]["EDIT_DATE"].ToString()).ToShortDateString();
                        }
                        td2.Text = vc;
                        TableCell td3 = new TableCell();
                        td3.CssClass = "std_td1 d3";
                        td3.Text = tc;
                        TableCell td4 = new TableCell();
                        td4.CssClass = "std_td1 d4";
                        TableCell td5 = new TableCell();
                        td5.CssClass = "std_td1 d4";

                        Button bt2 = new Button();
                        bt2.Text = "Edit";
                        bt2.CssClass = "n_btn";
                        bt2.Attributes["type"] = "button";
                        bt2.Attributes.Add("runat", "server");
                        bt2.ID = "Edi_" + dt.Rows[i]["S_ID"].ToString();
                        bt2.Click += new EventHandler(Sub_Edit_Click);
                        td4.Controls.Add(bt2);
                        Button bt1 = new Button();
                        bt1.Text = "Delete";
                        bt1.CssClass = "n_btn";
                        bt1.Attributes["type"] = "button";
                        bt1.Attributes.Add("runat", "server");
                        bt1.ID = "Delm_" + dt.Rows[i]["S_ID"].ToString();
                        bt1.Click += new EventHandler(sub_delete_click);
                        td5.Controls.Add(bt1);

                        tr.Controls.Add(td);
                        tr.Controls.Add(td1);
                        tr.Controls.Add(td2);
                        tr.Controls.Add(td3);
                        tr.Controls.Add(td4);
                        tr.Controls.Add(td5);
                        ActiArchtbl.Rows.Add(tr);
                    }
                }
                else
                {
                    TableRow tRow = new TableRow();
                    TableCell tb = new TableCell();
                    tb.CssClass = "std_td1 d2";
                    tb.Text = "<b>No Record Available</b>";
                    tRow.Controls.Add(tb);
                    ActiArchtbl.Rows.Add(tRow);
                }
            }
            else
            {
                TableRow tRow = new TableRow();
                TableCell tb = new TableCell();
                tb.CssClass = "std_td1 d2";
                tb.Text = "<b>No Record Available</b>";
                tRow.Controls.Add(tb);
                ActiArchtbl.Rows.Add(tRow);
            }
            return r;
        }
        protected void fetch_Main_data(object sender, EventArgs e)
        {
            hdn_main_gl.Value = "";          
            fetch_data();
            op_info.Visible = false;
            flying_tbl.Visible = true;
        }

        protected void Sub_gl_click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            string[] btnId = b.ID.Split('_');
            hdn_sub_id.Value = btnId[1].ToString();
            string query = "select * from SBS_MASTER_TBL where ID='"+ btnId[1].ToString() + "'";
            string ConString = "Data Source=(DESCRIPTION =" +
        "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.197)(PORT = 1522))" +
        "(CONNECT_DATA =" +
          "(SERVER = DEDICATED)" +
          "(SERVICE_NAME = FCPROD)));" +
          "User Id=APPLOGS;Password=A#9pLO7g$2o23;Connection Timeout=600; Max Pool Size=150;";
            DataTable dt = get_oracle_data(query, ConString);
            s_mode.InnerHtml = "Mode.........: Add New GL";
            s_curr.InnerHtml = "Currency Code: " + dt.Rows[0]["CURRENCY"].ToString();
            sub_currency.Value = dt.Rows[0]["CURRENCY"].ToString();
            n_name.InnerHtml = "Node Name....: " + dt.Rows[0]["NAME"].ToString();
            int r= fetch_sub_data(btnId[1].ToString());
            sbu_info_desk.InnerHtml = "<p class='p'>" + "Total Sub GL Count".PadRight(30, '.') + ": " + r.ToString().PadRight(30, ' ') + "</p>";            
            ScriptManager.RegisterStartupScript(this, GetType(), "show_div132", "show_div1();", true);
        }

        protected void Edit_click(object sender, EventArgs e)
        {
            string msg = "";
            Button b = sender as Button;
            string[] btnId = b.ID.Split('_');
            string query = "select * from SBS_MASTER_TBL where ID= '" + btnId[1] + "'";
            string ConString = "Data Source=(DESCRIPTION =" +
             "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.197)(PORT = 1522))" +
             "(CONNECT_DATA =" +
               "(SERVER = DEDICATED)" +
               "(SERVICE_NAME = FCPROD)));" +
               "User Id=APPLOGS;Password=A#9pLO7g$2o23;Connection Timeout=600; Max Pool Size=150;";
            DataTable dt = get_oracle_data(query, ConString);
           if(dt.Rows.Count>0)
            {               
                string op= dt.Rows[0]["CURRENCY"].ToString();
                if(op=="INR")
                {
                    ddl_currency.SelectedItem.Text= "INDIAN RUPEE";
                }
                if (op == "USD")
                {
                    ddl_currency.SelectedItem.Text = "US DOLLAR";
                }
                if (op == "GBP")
                {
                    ddl_currency.SelectedItem.Text = "GREAT BRITAIN POUND";
                }
                if (op == "EUR")
                {
                    ddl_currency.SelectedItem.Text = "EURO";
                }

                txt_name.InnerText = dt.Rows[0]["NAME"].ToString();
                note.InnerText= dt.Rows[0]["NOTE"].ToString();
                hdn_main_gl.Value= dt.Rows[0]["ID"].ToString();
                string t = "Edit Mode";
                msg = "<p class='p'>" + t.PadRight(30, '.') + ": Enable".PadRight(30, ' ') + "</p>";
                op_info.InnerHtml = msg;
                flying_tbl.Visible = false;
                op_info.Visible = true;
            }
            else
            {
                string t = "Edit Mode";
                msg = "<p class='p_error'>" + t.PadRight(30, '.') + ": Disable".PadRight(30, ' ') + "</p>";
                t = "Data Issue";
                msg = "<p class='p_error'>" + t.PadRight(30, '.') + ": No record found".PadRight(30, ' ') + "</p>";
                op_info.InnerHtml = msg;
                flying_tbl.Visible = false;
                op_info.Visible = true;
                hdn_main_gl.Value = "";
            }
           
        }

        protected void Sub_Edit_Click(object sender, EventArgs e)
        {
            string msg = "";
            Button b = sender as Button;
            string[] btnId = b.ID.Split('_');
            string query = "select S_ID,REPLACE(GL_Logic,':INR','') as GL_Logic,S_NAME,Note from SBS_SUB_GL_TABLE where S_ID='" + btnId[1] + "' and G_stat=1";
            string ConString = "Data Source=(DESCRIPTION =" +
             "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.197)(PORT = 1522))" +
             "(CONNECT_DATA =" +
               "(SERVER = DEDICATED)" +
               "(SERVICE_NAME = FCPROD)));" +
               "User Id=APPLOGS;Password=A#9pLO7g$2o23;Connection Timeout=600; Max Pool Size=150;";
            DataTable dt = get_oracle_data(query, ConString);
            if (dt.Rows.Count > 0)
            {
                txtarea_gl_logic.InnerText = dt.Rows[0]["GL_LOGIC"].ToString();
                txtarea_gl_name.InnerText = dt.Rows[0]["S_NAME"].ToString();
                txtarea_note.InnerText = dt.Rows[0]["Note"].ToString();
                sub_s_id.Value = dt.Rows[0]["S_ID"].ToString();
                s_mode.InnerHtml = "Mode.........: Edit GL";
                msg = "<p class='p'>Edit Mode".PadRight(30, '.') + ": Enable".PadRight(30, ' ') + "</p>";
                sbu_info_desk.InnerHtml = msg;               
            }
            else
            {
                string t = "Edit Mode";
                msg = "<p class='p_error'>" + t.PadRight(30, '.') + ": Disable".PadRight(30, ' ') + "</p>";
                t = "Data Issue";
                msg = "<p class='p_error'>" + t.PadRight(30, '.') + ": No record found".PadRight(30, ' ') + "</p>";
                sbu_info_desk.InnerHtml = msg;
                sub_s_id.Value = "";
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "show_div132", "show_div1();", true);
        }

        protected void delete_click(object sender, EventArgs e)
        {
            string msg = "";
            Button b = sender as Button;
            string[] btnId = b.ID.Split('_');
            string query = "update SBS_MASTER_TBL set NODE_STAT='2' where ID= '" + btnId[1] + "'";
            string ConString = "Data Source=(DESCRIPTION =" +
             "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.197)(PORT = 1522))" +
             "(CONNECT_DATA =" +
               "(SERVER = DEDICATED)" +
               "(SERVICE_NAME = FCPROD)));" +
               "User Id=APPLOGS;Password=A#9pLO7g$2o23;Connection Timeout=600; Max Pool Size=150;";
            msg = insert_sql_data(query, ConString, "Record Deleted");
            op_info.InnerHtml = msg;
            flying_tbl.Visible = false;
            op_info.Visible = true;          
            fetch_data();
        }

        protected void sub_delete_click(object sender, EventArgs e)
        {
            string msg = "";
            Button b = sender as Button;
            string[] btnId = b.ID.Split('_');
            string query = "update SBS_SUB_GL_TABLE set G_Stat='2' where S_ID= '" + btnId[1] + "'";
            string ConString = "Data Source=(DESCRIPTION =" +
             "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.197)(PORT = 1522))" +
             "(CONNECT_DATA =" +
               "(SERVER = DEDICATED)" +
               "(SERVICE_NAME = FCPROD)));" +
               "User Id=APPLOGS;Password=A#9pLO7g$2o23;Connection Timeout=600; Max Pool Size=150;";
            msg = insert_sql_data(query, ConString, "Record Deleted");
            int r = fetch_sub_data(hdn_sub_id.Value);
            sbu_info_desk.InnerHtml = "<p class='p'>" + "Total Sub GL Count".PadRight(30, '.') + ": " + r.ToString().PadRight(30, ' ') + "</p>" + msg;
            ScriptManager.RegisterStartupScript(this, GetType(), "show_div132", "show_div1();", true);
        }

        protected void reset_M1_data(object sender, EventArgs e)
        {
            TableRow tRow = new TableRow();
            TableCell tb = new TableCell();
            tb.CssClass = "std_td1 d2";
            tb.Text = "<b>No Record Available</b>";
            tRow.Controls.Add(tb);
            tbl_xx1.Rows.Add(tRow);
            tot_cnt.InnerHtml = "0";
            op_info.Visible = false;
            flying_tbl.Visible = true;
            hdn_main_gl.Value = "";
            ddl_category.SelectedValue = "0";
            fetch_data();
            reset_fields();
        }
        private void reset_fields()
        {
            txt_name.InnerText = "";
            note.InnerText = "";
        }

        protected void insert_sub_data(object sender, EventArgs e)
        {
            int p = 0;
            string ele = "";
            string gl_logic = txtarea_gl_logic.Value;
            string gl_name = txtarea_gl_name.Value;
            string user_note1 = txtarea_note.Value.ToString();
            string b = "";
            if (gl_logic == "")
            {
                ele = "txtarea_gl_logic"; p = 1; b = "GL Logic";
            }
            else if (gl_name == "")
            {
                ele = "txtarea_gl_name"; p = 1; b = "Sub GL Name";
            }
            string msg = "";
            string t = "Message Type";
            msg = "<p class='p_error'>" + t.PadRight(30, '.') + ": Input Error".PadRight(30, ' ') + "</p>";
            t = "Error Type";
            msg = msg + "<p class='p_error'>" + t.PadRight(30, '.') + ": Insufficient Data".PadRight(30, ' ') + "</p>";
            t = "Related data";
            msg = msg + "<p class='p_error'>" + t.PadRight(30, '.') + ": " + b + " Not provided".PadRight(30, ' ') + "</p>";
            if (p == 0)
            {
                string c_logic = "";
                t = "Message Type";
                msg = "<p class='p'>" + t.PadRight(30, '.') + ": Data Validation".PadRight(30, ' ') + "</p>";
                gl_logic = gl_logic.Trim();
                msg = msg + bitwise_check(ref gl_logic, ref c_logic, ref p);
                if (p == 0)
                {
                    string query = "";
                    if (sub_s_id.Value == "")
                    {
                        // query = "insert into SBS_SUB_GL_TABLE (R_ID,GL_LOGIC,Create_date,creator,G_stat,note,s_name,c_name) values('" + hdn_sub_id.Value + "','" + gl_logic + "',sysdate,'"+Session["Id"].ToString()+"','1','" + user_note1 + "','" + gl_name + "','" + c_logic + "')";
                        query = " declare " +
" cb_var clob; " +
" cb_var2 clob; " +
" begin " +
" insert into SBS_SUB_GL_TABLE (R_ID,GL_LOGIC,Create_date,creator,G_stat,note,s_name,c_name) " +
" values('" + hdn_sub_id.Value + "',empty_clob(),sysdate,'" + Session["Id"].ToString() + "','1','" + user_note1 + "','" + gl_name + "',empty_clob()) " +
" returning GL_LOGIC,c_name into cb_var2,cb_var; " +
" dbms_lob.append(cb_var,'" + c_logic + "'); " +
" dbms_lob.append(cb_var2,'" + gl_logic + "'); " +
" commit; " +
" end;";
                        t = "New Record Inserted";
                    }
                    else
                    {
                        // query = "update SBS_SUB_GL_TABLE set GL_LOGIC='" + gl_logic + "',NOTE='" + user_note1 + "',EDITOR='" + Session["Id"].ToString() + "',EDIT_DATE=sysdate,s_name='" + gl_name + "',c_name='" + c_logic + "' where S_ID='" + sub_s_id.Value + "' ";
                        query = "declare  cb_var clob;  cb_var2 clob;  begin   " +
 " update SBS_SUB_GL_TABLE  " +
 " SET " +
 " GL_LOGIC=empty_clob(), " +
 " NOTE='" + user_note1 + "', " +
 " EDITOR='" + Session["Id"].ToString() + "', " +
 " EDIT_DATE=sysdate, " +
 " s_name='" + gl_name + "', " +
 " c_name=empty_clob() " +
 " where S_ID='" + sub_s_id.Value + "' " +
 " returning GL_LOGIC,c_name into cb_var2,cb_var;   " +
" dbms_lob.append(cb_var,'" + c_logic + "'); " +
" dbms_lob.append(cb_var2,'" + gl_logic + "'); " +
" commit; " +
" end;";

                        t = "Record Updated";
                    }
                    string ConString = "Data Source=(DESCRIPTION =" +
          "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.197)(PORT = 1522))" +
          "(CONNECT_DATA =" +
            "(SERVER = DEDICATED)" +
            "(SERVICE_NAME = FCPROD)));" +
            "User Id=APPLOGS;Password=A#9pLO7g$2o23;Connection Timeout=600; Max Pool Size=150;";
                    msg = insert_sql_data(query, ConString, t) + msg;
                    int r = fetch_sub_data(hdn_sub_id.Value);
                    sbu_info_desk.InnerHtml = "<p class='p'>" + "Total Sub GL Count".PadRight(30, '.') + ": " + r.ToString().PadRight(30, ' ') + "</p>" + msg;
                    reset_data();
                    ScriptManager.RegisterStartupScript(this, GetType(), "show_div132", "show_div1();", true);
                }
                else
                {
                    sbu_info_desk.InnerHtml = msg;
                    ScriptManager.RegisterStartupScript(this, GetType(), "show_div132", "show_div1();", true);
                }
               
            }
            else
            {
                sbu_info_desk.InnerHtml = msg;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert_elementcc", "alert_elementcc('" + ele + "');", true);
            }

        }

        protected void reset_btn_sub(object sender, EventArgs e)
        {
            reset_data();
            int r = fetch_sub_data(hdn_sub_id.Value);
            sbu_info_desk.InnerHtml = "<p class='p'>" + "Total Sub GL Count".PadRight(30, '.') + ": " + r.ToString().PadRight(30, ' ') + "</p>";
            ScriptManager.RegisterStartupScript(this, GetType(), "show_div132", "show_div1();", true);
        }

        private void reset_data()
        {
            txtarea_gl_logic.Value = "";
            txtarea_gl_name.Value = "";
            txtarea_note.Value = "";
            sub_s_id.Value = "";
        }

        private string bitwise_check(ref string gl_logic, ref string C_logic, ref int p)
        {
            string msg = "", gl = "", temp = "";
            int e = 0, c = 0, cnt = 0;
            for (int i = 0; i < gl_logic.Length; i++)
            {
                string g = gl_logic[i].ToString();
                if (g == "+" || g == "-")
                {
                    if (c == 1 && i+1 < gl_logic.Length)
                    {
                        temp = temp + g;
                        C_logic = C_logic + ",";
                        c = 0;
                    }
                    else if(c == 1 && i + 1 == gl_logic.Length)
                    {
                        msg = msg + "<p class='p_error'>" + "Operator Error ".PadRight(30, '.') + ":  Symbol :" + g + " at position :" + (i + 1).ToString().PadRight(30, ' ') + "</p>";
                        p = 1;
                        return msg;
                    }
                    else
                    {
                        msg = msg + "<p class='p_error'>" + "Operator Error ".PadRight(30, '.') + ":  Symbol :" + g + " at position :" + (i + 1).ToString().PadRight(30, ' ') + "</p>";
                        p = 1;                    
                        return msg;
                    }
                }
                else
                {
                    gl = gl + g;
                    cnt = cnt + 1;
                    if (cnt == 9)
                    {
                        msg = msg + check_gl_code(gl,ref p);
                        temp = temp + gl + ":" + sub_currency.Value;
                        C_logic = C_logic+gl + ":" + sub_currency.Value;
                        gl = "";
                        cnt = 0;
                        c = 1;
                        if(p==1)
                        {                         
                            return msg;
                        }
                    }
                }

            }
            if(gl!="")
            {
                msg = msg + "<p class='p_error'>" + "Invalid GL Code ".PadRight(30, '.') + ":  " + gl.ToString().PadRight(30, ' ') + "</p>";
                p = 1;
            }
            gl_logic = temp;
            return msg;
        }

        private string check_gl_code(string gl,ref int p)
        {
            string msg = "";
            string query = "select ACCOUNT,ACC_CCY from fc3gl.ACTB_ACCBAL_HISTORY where ACCOUNT='" + gl + "' and ROWNUM=1";
            string ConString = "Data Source=(DESCRIPTION =" +
                "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.175)(PORT = 1521))" +
                "(CONNECT_DATA =" +
                  "(SERVER = DEDICATED)" +
                  "(SERVICE_NAME = FCMIS)));" +
                  "User Id=fcrhost_read;Password=fcmis_read$2023;Connection Timeout=600; Max Pool Size=150;";
            DataTable dt = get_oracle_data(query, ConString);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //if (dt.Rows[i]["ACC_CCY"].ToString() == sub_currency.Value)
                    //{
                        msg = "<p class='p'>" + gl.PadRight(30, '.') + ":  " + "OK".ToString().PadRight(30, ' ') + "</p>";
                        p = 0;
                        return msg;                       
                    //}
                    //else
                    //{
                    //    msg = "<p class='p_error'>" + "Invalid Currency Code".PadRight(30, '.') + ":  " + gl.ToString().PadRight(30, ' ') + "</p>";
                    //    p = 1;
                    //}
                }
            }
            else
            {
                p = 1;
                query = "select * from UBSHOST.GLTM_GLMASTER where GL_CODE='" + gl + "'";
                dt = get_oracle_data(query, ConString);
                if (dt.Rows.Count > 0)
                {
                    if(dt.Rows[0]["LEAF"].ToString()=="Y")
                    {
                        msg = "<p class='p'>" + gl.PadRight(30, '.') + ":  " + "OK".ToString().PadRight(30, ' ') + "</p>";
                        p = 0;
                    }
                    else
                    {
                        msg = "<p class='p_error'>" + "GL Error".PadRight(30, '.') + ": Not a Leaf GL ".ToString().PadRight(30, ' ') + "</p>";
                    }                  
                }
                else
                {
                    msg = msg + "<p class='p_error'>" + "Invalid GL Code".PadRight(30, '.') + ":  " + gl.ToString().PadRight(30, ' ') + "</p>";
                }

            }
            return msg;
        }
        protected void insert_Main_data(object sender, EventArgs e)
        {
            int p = 0;
            string ele = "";
            string category = ddl_category.SelectedItem.Text;
            string selected_currency = ddl_currency.SelectedItem.Text;
            string main_name = txt_name.Value.ToString();
            string user_note1 = note.Value.ToString();
            string b = "";
            if(category== "--PLEASE SELECT CATEGORY OF GLs--")
            {
                ele = "ddl_category";
                p = 1;
                b = "GL Section";
            }
            else if(selected_currency== "--PLEASE SELECT CURRENCY TYPE--")
            {
                ele = "ddl_currency"; p = 1;b = "GL Currency";
            }
            else if(main_name=="")
            {
                ele = "txt_name"; p = 1;b = "Main GL Name";
            }

            if (selected_currency != "--PLEASE SELECT CURRENCY TYPE--")
            {
               if(selected_currency== "INDIAN RUPEE")
                {
                    selected_currency = "INR";
                }
               else if(selected_currency == "US DOLLAR")
                {
                    selected_currency = "USD";
                }
                else if (selected_currency == "GREAT BRITAIN POUND")
                {
                    selected_currency = "GBP";
                }
                else if (selected_currency == "EURO")
                {
                    selected_currency = "EUR";
                }
            }
            string msg = "";
            string t = "Message Type";
            msg = "<p class='p_error'>" + t.PadRight(30, '.') + ": Input Error".PadRight(30, ' ') + "</p>";
            t = "Error Type";
            msg = msg + "<p class='p_error'>" + t.PadRight(30, '.') + ": Insufficient Data".PadRight(30, ' ') + "</p>";
            t = "Related data";
            msg = msg + "<p class='p_error'>" + t.PadRight(30, '.') + ": " + b + " Not provided".PadRight(30, ' ') + "</p>";
            if (p==0)
            {
                string query = "";
                if (hdn_main_gl.Value == "")
                {
                    query = "insert into SBS_MASTER_TBL (TYPE,NAME,creator,create_date,NODE_STAT,CURRENCY,NOTE) values('" + category + "','" + main_name + "','"+Session["Id"].ToString()+"',sysdate,'1','" + selected_currency + "','" + user_note1 + "')";
                    t = "New Record Inserted";
                }
                else
                {
                    query = "update SBS_MASTER_TBL set NAME='" + main_name + "',CURRENCY='" + selected_currency + "',NOTE='" + user_note1 + "',EDITOR='"+Session["Id"].ToString()+"',EDIT_DATE=sysdate where ID='" + hdn_main_gl.Value + "' ";
                    t = "Record Updated";
                }
                string ConString = "Data Source=(DESCRIPTION =" +
      "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.197)(PORT = 1522))" +
      "(CONNECT_DATA =" +
        "(SERVER = DEDICATED)" +
        "(SERVICE_NAME = FCPROD)));" +
        "User Id=APPLOGS;Password=A#9pLO7g$2o23;Connection Timeout=600; Max Pool Size=150;";
                msg = insert_sql_data(query, ConString,t);              
             
                op_info.InnerHtml = msg;
                flying_tbl.Visible = false;
                op_info.Visible = true;
                hdn_main_gl.Value = "";
                reset_fields();
                fetch_data();
            }
            else
            {
                op_info.InnerHtml = msg;
                flying_tbl.Visible = false;
                op_info.Visible = true;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert_element", "alert_element('" + ele + "');", true);
            }
        }

        private string insert_sql_data(string query,string ConString,string tmn)
        {
            string msg = "";
            string t = "Message Type";
            try
            {
                OracleConnection connection = new OracleConnection(ConString);
                OracleCommand command = new OracleCommand(query, connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
                command.Connection.Close();
                msg = "<p class='p'>" + t.PadRight(30, '.') + ": Oracle Operations".PadRight(30, ' ') + "</p>";
                t = "Query Execution Status";
                msg = msg + "<p class='p'>" + t.PadRight(30, '.') + ": Completed".PadRight(30, ' ') + "</p>";
                msg = msg + "<p class='p'>" + "Data Status".PadRight(30, '.') + ": " + tmn.PadRight(30, ' ') + "</p>";
            }
            catch (OracleException ex)
            {
                msg = "<p class='p_error'>" + t.PadRight(30, '.') + ": Oracle Error".PadRight(30, ' ') + "</p>";
                t = "Error Code";
                string[] pmy = ex.Message.ToString().Split(':');
                msg = msg + "<p class='p_error'>" + t.PadRight(30, '.') + ": " + pmy[0].PadRight(30, ' ') + "</p>";
                t =pmy[1];
                int k = 0;
                string dtm = "";
                int j = 0;
                for(int i=0;i<t.Length;i++)
                {
                    dtm = dtm + t[i];
                    j = j + 1;
                    if (j == 29 || (i + 1) == t.Length)
                    {
                        j = 0;
                        if(k==0)
                        {
                            msg = msg + "<p class='p_error'>" + "Error Message".PadRight(30, '.') + ": " + dtm.PadRight(30, ' ') + "</p>";
                        }
                        else
                        {
                            msg = msg + "<p class='p_error'>" + "".PadRight(30, '.') + ": " + dtm.PadRight(30, ' ') + "</p>";
                        }
                        dtm = "";
                        k = k + 1;
                    }
                }
            }
            catch (Exception ex)
            {
                msg = "<p class='p_error'>" + t.PadRight(30, '.') + ": Oracle Error".PadRight(30, ' ') + "</p>";
                //t = "Error Code";
                //msg = msg + "<p class='p'>" + t.PadRight(30, '.') + ": " + ex.ErrorCode.ToString().PadRight(30, ' ') + "</p>";
                t = ex.Message.ToString();
                int k = 0;
                string dtm = "";
                int j = 0;
                for (int i = 0; i < t.Length; i++)
                {
                    dtm = dtm + t[i];
                    j = j + 1;
                    if (j == 29 || (i + 1) == t.Length)
                    {
                        j = 0;
                        if (k == 0)
                        {
                            msg = msg + "<p class='p_error'>" + "Error Message".PadRight(30, '.') + ": " + dtm.PadRight(30, ' ') + "</p>";
                        }
                        else
                        {
                            msg = msg + "<p class='p_error'>" + "".PadRight(30, '.') + ": " + dtm.PadRight(30, ' ') + "</p>";
                        }
                        dtm = "";
                        k = k + 1;
                    }
                }
            }

            //msg = "<p class='p'>" + t.PadRight(30, '.') + ": Oracle Error".PadRight(30, ' ') + "</p>";
            //    msg = t1.;
            //}
            return msg;
        }

       

    }
}