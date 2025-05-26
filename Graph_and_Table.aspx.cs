using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Oracle.ManagedDataAccess.Client;

namespace BCCBExamPortal
{
    public partial class Graph_and_Table : System.Web.UI.Page
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
            //generalrep_btn.CssClass = "menubtn1";
            if (!IsPostBack)
            {
                //audit_trails();
                //fetch_depts();
                //Load_dept_report();
                Load_dept_name();
            }
            else
            {               
                //fetch_depts();
            }
          
            fetch_reports();
           
        }

        protected void EOD_Click(object sender, EventArgs e)
        {
            Response.Redirect("EOD_Admin.aspx");
        }
        protected void mini_ledge_clk(object sender, EventArgs e)
        {
            Response.Redirect("Short.aspx");
        }
        protected void web_Click(object sender, EventArgs e)
        {
            Response.Redirect("ConnectionString.aspx");
        }
        protected void grp_tbl_Click(object sender, EventArgs e)
        {
            Response.Redirect("Graph_and_Table.aspx");
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("EmployeeSettings.aspx");
        }
        protected void btnTNT_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditTest.aspx?ENo=" + Examlbl.Text);
        }

        protected void btnAT_Click(object sender, EventArgs e)
        {
            Response.Redirect("Admin.aspx?ENo=0&Menu=2");
        }

        protected void btnATR_Click(object sender, EventArgs e)
        {
            Response.Redirect("Admin.aspx?ENo=0&Menu=3");
        }

        protected void btnAnalys_Click(object sender, EventArgs e)
        {
            Response.Redirect("Analysis.aspx?");
        }

        protected void btnSettings_Click(object sender, EventArgs e)
        {
            Response.Redirect("Settings.aspx?ENo=0");
        }

        protected void Instrubtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditTest.aspx?ENo=" + Examlbl.Text + "&In=0");
        }

        protected void addmarquee_Click(object sender, EventArgs e)
        {
            Response.Redirect("Admin_Marquee.aspx");
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
        protected void general_users(object sender, EventArgs e)
        {
            Response.Redirect("Active_Users.aspx");
        }

        protected void create_report(object sender, EventArgs e)
        {
            string report_name = Rep_name.Value;
            string query1 = query_txt.Value;
            string query2 = query_br.Value;


            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            if (hdn_edit.Value == "0")
            {
                conn.Open();
                if (report_name != "" && query1 != "" && query2 != "")
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand("Insert into Executive_reports (Module_Type,Report_Name,Query,Branch_Query,Note,Report_Status,DB_CON,Full_width,Theme,New_column_name,Style_width,Report_Build_date,USER1,Is_casa,Is_LN,Is_TD,department) values(@Module_Type,@Report_Name,@Query,@Branch_Query,@Note,@Report_Status,@DB_CON,@Full_width,@Theme,@New_column_name,@Style_width,@Report_Build_date,@USER1,@Is_casa,@Is_LN,@Is_TD,@department)", conn);


                        if (chk1.Checked == true)
                        {
                            cmd.Parameters.AddWithValue("@Is_casa", 1);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Is_casa", 0);
                        }
                        if (chk2.Checked == true)
                        {
                            cmd.Parameters.AddWithValue("@Is_LN", 1);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Is_LN", 0);
                        }
                        if (chk3.Checked == true)
                        {
                            cmd.Parameters.AddWithValue("@Is_TD", 1);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Is_TD", 0);
                        }


                        if (tbl_chk.Checked == true)
                        {
                            cmd.Parameters.AddWithValue("@Module_Type", "T");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Module_Type", "G");
                        }

                        cmd.Parameters.AddWithValue("@Report_Name", report_name);
                        cmd.Parameters.AddWithValue("@Query", query1);
                        cmd.Parameters.AddWithValue("@Branch_Query", query2);
                        cmd.Parameters.AddWithValue("@Note", txt_note.Value);

                        if (rep_id_a.Checked == true)
                        {
                            cmd.Parameters.AddWithValue("@Report_Status", "A");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Report_Status", "D");
                        }

                        string DB_Con = "M";

                        if (chk_mis.Checked == true)
                        {
                            DB_Con = "M";
                        }
                        else if (chk_prod.Checked == true)
                        {
                            DB_Con = "P";
                        }
                        else if (chk_eom.Checked == true)
                        {
                            DB_Con = "E";
                        }
                        else if (chk_dm.Checked == true)
                        {
                            DB_Con = "D";
                        }

                        cmd.Parameters.AddWithValue("@DB_CON", DB_Con);

                        if (full_width.Checked == true)
                        {
                            cmd.Parameters.AddWithValue("@Full_width", "Y");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Full_width", "N");
                        }

                        string theme = "P";

                        if (tbl_chk.Checked == true)
                        {
                            if (chk_plane.Checked == true)
                            {
                                theme = "P";
                            }
                            else if (chk_blue.Checked == true)
                            {
                                theme = "B";
                            }
                            else if (chk_hot_cho.Checked == true)
                            {
                                theme = "H";
                            }
                            else if (chk_ice_creame.Checked == true)
                            {
                                theme = "I";
                            }
                        }
                        else
                        {
                            if (chk_pie.Checked == true)
                            {
                                theme = "P";
                            }
                            else if (chk_bar.Checked == true)
                            {
                                theme = "B";
                            }
                            else if (chk_column.Checked == true)
                            {
                                theme = "C";
                            }
                            else if (chk_donut.Checked == true)
                            {
                                theme = "D";
                            }
                        }
                        cmd.Parameters.AddWithValue("@Theme", theme);
                        //New_column_name,Style_width,Report_Build_date,Report_Edit_date,USER1,USER2
                        cmd.Parameters.AddWithValue("@New_column_name", new_col.Value);
                        cmd.Parameters.AddWithValue("@Style_width", new_style.Value);


                        cmd.Parameters.AddWithValue("@USER1", "Abee"); //Session["Id"].ToString());
                        cmd.Parameters.AddWithValue("@Report_Build_date", System.DateTime.Now); 
                        cmd.Parameters.AddWithValue("@department", ddl_dep.SelectedItem.Text);
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        fetch_reports();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('OK');", true);
                        Rep_name.Value = "";
                        query_txt.Value = "";
                        query_br.Value = "";
                        Create_report_btn.Disabled = true;
                        Create_report_btn.InnerText = "Create Report";
                    }
                    catch (Exception ep)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('Something went wromg');", true);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('Something went wromg1');", true);
                }
            }
            else
            {                
                conn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand("update  Executive_reports set Module_Type=@Module_Type,Report_Name=@Report_Name,Query=@Query,Branch_Query=@Branch_Query,Note=@Note,Report_Status=@Report_Status,DB_CON=@DB_CON,Full_width=@Full_width,Theme=@Theme,New_column_name=@New_column_name,Style_width=@Style_width,Report_Edit_date=@Report_Edit_date,USER2=@USER2,Is_casa=@Is_casa,Is_LN=@Is_LN,Is_TD=@Is_TD,department=@department  where ID=@ID", conn);

                    if (chk1.Checked == true)
                    {
                        cmd.Parameters.AddWithValue("@Is_casa", 1);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Is_casa", 0);
                    }
                    if (chk2.Checked == true)
                    {
                        cmd.Parameters.AddWithValue("@Is_LN", 1);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Is_LN", 0);
                    }
                    if (chk3.Checked == true)
                    {
                        cmd.Parameters.AddWithValue("@Is_TD", 1);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Is_TD", 0);
                    }


                    if (tbl_chk.Checked == true)
                    {
                        cmd.Parameters.AddWithValue("@Module_Type", "T");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Module_Type", "G");
                    }
                    cmd.Parameters.AddWithValue("@Report_Name", report_name);
                    cmd.Parameters.AddWithValue("@Query", query1);
                    cmd.Parameters.AddWithValue("@Branch_Query", query2);
                    cmd.Parameters.AddWithValue("@Note", txt_note.Value);
                    if (rep_id_a.Checked == true)
                    {
                        cmd.Parameters.AddWithValue("@Report_Status", "A");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Report_Status", "D");
                    }
                    string DB_Con = "M";
                    if (chk_mis.Checked == true)
                    {
                        DB_Con = "M";
                    }
                    else if (chk_prod.Checked == true)
                    {
                        DB_Con = "P";
                    }
                    else if (chk_eom.Checked == true)
                    {
                        DB_Con = "E";
                    }
                    else if (chk_dm.Checked == true)
                    {
                        DB_Con = "D";
                    }
                    cmd.Parameters.AddWithValue("@DB_CON", DB_Con);
                    if (full_width.Checked == true)
                    {
                        cmd.Parameters.AddWithValue("@Full_width", "Y");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Full_width", "N");
                    }
                    string theme = "P";
                    if (tbl_chk.Checked == true)
                    {
                        if (chk_plane.Checked == true)
                        {
                            theme = "P";
                        }
                        else if (chk_blue.Checked == true)
                        {
                            theme = "B";
                        }
                        else if (chk_hot_cho.Checked == true)
                        {
                            theme = "H";
                        }
                        else if (chk_ice_creame.Checked == true)
                        {
                            theme = "I";
                        }
                    }
                    else
                    {
                        if (chk_pie.Checked == true)
                        {
                            theme = "P";
                        }
                        else if (chk_bar.Checked == true)
                        {
                            theme = "B";
                        }
                        else if (chk_column.Checked == true)
                        {
                            theme = "C";
                        }
                        else if (chk_donut.Checked == true)
                        {
                            theme = "D";
                        }
                    }
                    cmd.Parameters.AddWithValue("@Theme", theme);
                    cmd.Parameters.AddWithValue("@New_column_name", new_col.Value);
                    cmd.Parameters.AddWithValue("@Style_width", new_style.Value);
                    cmd.Parameters.AddWithValue("@ID", hdn_edit.Value);
                    cmd.Parameters.AddWithValue("@Report_Edit_date", System.DateTime.Now);
                    cmd.Parameters.AddWithValue("@USER2", "Abee");
                    cmd.Parameters.AddWithValue("@department", ddl_dep.SelectedItem.Text);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    fetch_reports();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('Done!!');", true);
                    Rep_name.Value = "";
                    query_txt.Value = "";
                    query_br.Value = "";
                    Create_report_btn.Disabled = true;
                    Create_report_btn.InnerText = "Create Report";
                }
                catch (Exception ep)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('Something went wrong!!!');", true);
                }
                finally
                {
                    conn.Close();
                }
            }
            hdn_edit.Value = "0";
        }

        protected void Load_dept_name()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            string query = "";
            query = "select ROW_NUMBER() OVER (ORDER BY Reporting_Departing) AS serial_number,Reporting_Departing from Reporting_Dept where Curr_stat='1'";
            DataTable dt = get_normal_data(query, con);
            ddl_dep.Items.Clear();
            ddl_dep.DataSource = dt;
            ddl_dep.DataBind();
            ddl_dep.DataTextField = "Reporting_Departing";
            ddl_dep.DataValueField = "serial_number";
            ddl_dep.DataBind();
            ddl_dep.Items.Insert(0, new ListItem("--All Departments--", "0"));
            //if (DepName.Text != "")
            //{
            //    //ddl_dep.SelectedItem.Text = DepName.Text;
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        if (dt.Rows[i]["Reporting_Departing"].ToString() == DepName.Text)
            //        {
            //            ddl_dep.SelectedValue = dt.Rows[i]["serial_number"].ToString();
            //        }
            //    }               
            //}

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
                //string er = "Unable to fetch Customers details due to Network issue: " + el + " : : " + query;
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('Unable to fetch Customers details due to Network issue:');", true);
            }
            finally
            {
                con.Close();
            }

            return dt;
        }
        protected void fetch_reports()
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            ActiArchtbl.Rows.Clear();         
            string query = "select * from Executive_reports where  Report_Status!='Z' and Module_Type='T'";
            DataTable dt = get_normal_data(query, con);
            if (dt.Rows.Count > 0)
            {
                TableRow tRow = new TableRow();
                TableCell tb = new TableCell();
                tb.CssClass = "tab";
                tb.Style.Add("width", "5%");
                tb.Text = "<b>SR No</b>";

                TableCell tb4 = new TableCell();
                tb4.CssClass = "tab";
                tb4.Style.Add("width", "50%");
                tb4.Text = "<b>Report Name</b>";

                TableCell tb7 = new TableCell();
                tb7.CssClass = "tab";
                tb7.Style.Add("width", "20%");
                tb7.Text = "<b>Report Type</b>";

                TableCell tb5 = new TableCell();
                tb5.CssClass = "tab";
                tb5.Style.Add("width", "10%");
                tb5.Text = "<b>Edit</b>";
                TableCell tb6 = new TableCell();
                tb6.CssClass = "tab";
                tb6.Style.Add("width", "10%");
                tb6.Text = "<b>Delete</b>";

                tRow.Controls.Add(tb);
                tRow.Controls.Add(tb4);
                tRow.Controls.Add(tb7);
                tRow.Controls.Add(tb5);
                tRow.Controls.Add(tb6);
                ActiArchtbl.Rows.Add(tRow);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TableRow tr = new TableRow();

                    TableCell td = new TableCell();
                    td.CssClass = "celltab";
                    td.Style.Add("width", "5%");
                    td.Text = Convert.ToString(i + 1);

                    TableCell td2 = new TableCell();
                    td2.CssClass = "celltab";
                    td2.Style.Add("width", "50%");
                    td2.Text = dt.Rows[i]["Report_Name"].ToString(); ;

                    TableCell td7 = new TableCell();
                    td7.CssClass = "celltab";
                    td7.Style.Add("width", "20%");
                    td7.Text = "Table" ;


                    TableCell td4 = new TableCell();
                    td4.CssClass = "celltab";
                    td4.Style.Add("width", "10%");
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
                    td5.Style.Add("width", "10%");
                    Button bt2 = new Button();
                    bt2.Text = "Delete";
                    bt2.CssClass = "cellbtn";
                    bt2.Attributes["type"] = "button";
                    bt2.Attributes.Add("runat", "server");
                    bt2.ID = "Del_" + dt.Rows[i]["ID"].ToString();
                    bt2.Click += new EventHandler(bt2_Click);
                    td5.Controls.Add(bt2);


                    tr.Controls.Add(td);
                    tr.Controls.Add(td2);
                    tr.Controls.Add(td7);
                    tr.Controls.Add(td4);
                    tr.Controls.Add(td5);
                    ActiArchtbl.Rows.Add(tr);

                }

            }
            
            create_graph_table();
            ScriptManager.RegisterStartupScript(this, GetType(), "grapcall", " radio_change();", true);

        }
        protected void create_graph_table()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            ActiArchtblGraph.Rows.Clear();
            string query = "select * from Executive_reports where  Report_Status!='Z' and Module_Type='G'";
            DataTable dt = get_normal_data(query, con);
            if (dt.Rows.Count > 0)
            {
                TableRow tRow = new TableRow();
                TableCell tb = new TableCell();
                tb.CssClass = "tab";
                tb.Style.Add("width", "5%");
                tb.Text = "<b>SR No</b>";

                TableCell tb4 = new TableCell();
                tb4.CssClass = "tab";
                tb4.Style.Add("width", "50%");
                tb4.Text = "<b>Report Name</b>";

                TableCell tb7 = new TableCell();
                tb7.CssClass = "tab";
                tb7.Style.Add("width", "20%");
                tb7.Text = "<b>Report Type</b>";
                
                TableCell tb5 = new TableCell();
                tb5.CssClass = "tab";
                tb5.Style.Add("width", "10%");
                tb5.Text = "<b>Edit</b>";
                TableCell tb6 = new TableCell();
                tb6.CssClass = "tab";
                tb6.Style.Add("width", "10%");
                tb6.Text = "<b>Delete</b>";

                tRow.Controls.Add(tb);
                tRow.Controls.Add(tb4);
                tRow.Controls.Add(tb7);
                tRow.Controls.Add(tb5);
                tRow.Controls.Add(tb6);
                ActiArchtblGraph.Rows.Add(tRow);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TableRow tr = new TableRow();

                    TableCell td = new TableCell();
                    td.CssClass = "celltab";
                    td.Style.Add("width", "5%");
                    td.Text = Convert.ToString(i + 1);

                    TableCell td2 = new TableCell();
                    td2.CssClass = "celltab";
                    td2.Style.Add("width", "45%");
                    td2.Text = dt.Rows[i]["Report_Name"].ToString();

                    TableCell td7 = new TableCell();
                    td7.CssClass = "celltab";
                    td7.Style.Add("width", "20%");
                    td7.Text = "Graph";

                    TableCell td4 = new TableCell();
                    td4.CssClass = "celltab";
                    td4.Style.Add("width", "10%");
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
                    td5.Style.Add("width", "10%");
                    Button bt2 = new Button();
                    bt2.Text = "Delete";
                    bt2.CssClass = "cellbtn";
                    bt2.Attributes["type"] = "button";
                    bt2.Attributes.Add("runat", "server");
                    bt2.ID = "Del_" + dt.Rows[i]["ID"].ToString();
                    bt2.Click += new EventHandler(bt2_Click);
                    td5.Controls.Add(bt2);


                    tr.Controls.Add(td);
                    tr.Controls.Add(td2);
                    tr.Controls.Add(td7);
                    tr.Controls.Add(td4);
                    tr.Controls.Add(td5);
                    ActiArchtblGraph.Rows.Add(tr);

                }

            }
        }
        protected void bt2_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            string[] btnId = b.ID.Split('_');
            string query = "update Executive_reports set Report_Status='Z' where ID = '" + btnId[1] + "'";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            conn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('Done!!!');", true);
                fetch_reports();
            }
            catch (Exception ep)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('Something went wrong!!!');", true);
            }
            finally
            {
                conn.Close();
            }

        }
        protected void radio_change(object sender, EventArgs e)
        {
            if(tbl_chk.Checked==true)
            {
                tbl_sel_div.Visible = true;
                tbl_graph_div.Visible = false;
            }
            else
            {
                tbl_graph_div.Visible = true;
                tbl_sel_div.Visible = false;
            }
        }
        protected void bt1_Click(object sender, EventArgs e)
        {
            error_stat.InnerText = "";         
            Create_report_btn.Disabled = false;
            Button b = sender as Button;
            string[] btnId = b.ID.Split('_');
            string query = "select * from  Executive_reports where ID = '" + btnId[1] + "'";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            DataTable dt = get_normal_data(query, con);
            if (dt.Rows.Count > 0)
            {
                hdn_edit.Value = dt.Rows[0]["ID"].ToString();
                Rep_name.Value = dt.Rows[0]["Report_Name"].ToString();
                query_txt.Value = dt.Rows[0]["Query"].ToString();
                query_br.Value = dt.Rows[0]["Branch_Query"].ToString();
                txt_note.Value = dt.Rows[0]["Note"].ToString();
                //,,,
                string st = dt.Rows[0]["Report_Status"].ToString();
                if (st == "A")
                {
                    rep_id_a.Checked = true;
                }
                else
                {
                    rep_id_d.Checked = true;
                }
                st = dt.Rows[0]["DB_CON"].ToString();
                if (st == "M")
                {
                    chk_mis.Checked = true;
                }
                else if(st == "P")
                {
                    chk_prod.Checked = true;
                }
                else if (st == "E")
                {
                    chk_eom.Checked = true;
                }
                else
                {
                    chk_dm.Checked = true;
                }
                st = dt.Rows[0]["Full_width"].ToString();
                if(st=="Y")
                {
                    full_width.Checked = true;
                }
                else
                {
                    full_width.Checked = false;
                }
                st = dt.Rows[0]["Theme"].ToString();
                if (tbl_chk.Checked == true)
                {
                    if (st == "P")
                    {
                        chk_plane.Checked = true;
                    }
                    else if (st == "B")
                    {
                        chk_blue.Checked = true;
                    }
                    else if (st == "H")
                    {
                        chk_hot_cho.Checked = true;
                    }
                    else if (st == "I")
                    {
                        chk_ice_creame.Checked = true;
                    }
                }
                else
                {
                    if (st == "P")
                    {
                        chk_pie.Checked = true;
                    }
                    else if (st == "B")
                    {
                        chk_bar.Checked = true;
                    }
                    else if (st == "C")
                    {
                        chk_column.Checked = true;
                    }
                    else if (st == "D")
                    {
                        chk_donut.Checked = true;
                    }
                }

                if (dt.Rows[0]["Is_casa"].ToString() == "1")
                {
                    chk1.Checked = true;
                }
                else
                {
                    chk1.Checked = false;
                }
                if (dt.Rows[0]["Is_LN"].ToString() == "1")
                {
                    chk2.Checked = true;
                }
                else
                {
                    chk2.Checked = false;
                }
                if (dt.Rows[0]["Is_TD"].ToString() == "1")
                {
                    chk3.Checked = true;
                }
                else
                {
                    chk3.Checked = false;
                }
                if (dt.Rows[0]["department"].ToString() != "")
                {
                    ddl_dep.SelectedItem.Text = dt.Rows[0]["department"].ToString();
                }
                new_col.Value = dt.Rows[0]["New_column_name"].ToString();
                new_style.Value = dt.Rows[0]["Style_width"].ToString();
                Create_report_btn.InnerText = "Edit Report";
            }
        }
        protected void validate_design(object sender, EventArgs e)
        {
            string ConString = "";
            if (chk_mis.Checked == true)
            {
                ConString = "Data Source=(DESCRIPTION =" +
            "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.175)(PORT = 1521))" +
            "(CONNECT_DATA =" +
              "(SERVER = DEDICATED)" +
              "(SERVICE_NAME = FCMIS)));" +
              "User Id=fcrhost_read;Password=fcmis_read$2023;Connection Timeout=600; Max Pool Size=150;";
            }
            else if (chk_eom.Checked==true)
            {
                ConString = "Data Source=(DESCRIPTION =" +
           "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.175)(PORT = 1521))" +
           "(CONNECT_DATA =" +
             "(SERVER = DEDICATED)" +
             "(SERVICE_NAME = MISDB)));" +
             "User Id=misread;Password=m1sR3ad$2023;Connection Timeout=600; Max Pool Size=150;";
              
            }
            else if(chk_prod.Checked==true)
            {
                ConString = "Data Source=(DESCRIPTION =" +
             "(ADDRESS = (PROTOCOL = TCP)(HOST = fcobdxdb-scan)(PORT = 1522))" +
             "(CONNECT_DATA =" +
               "(SERVER = DEDICATED)" +
               "(SERVICE_NAME = FCPROD)));" +
               "User Id=pfms_read;Password=pFm$_Read$2023;Connection Timeout=600; Max Pool Size=150;";
            }
            else
            {
                ConString = "NA";
            }
                       string query = query_txt.InnerText;
            if (query != "")
            {
                DataTable dm = get_oracle_data(query, ConString);
                no_col.InnerText = dm.Columns.Count.ToString();

                if (tbl_chk.Checked == true)
                {
                    if (new_col.Value != "")
                    {
                        string[] col_num_cn = new_col.Value.Split(',');
                        if (col_num_cn.Length == dm.Columns.Count)
                        {
                            if (new_style.Value != "")
                            {
                                string[] style_new = new_style.Value.Split(',');

                                if (style_new.Length == dm.Columns.Count)
                                {
                                    int width = 0;
                                    for (int i = 0; i < style_new.Length; i++)
                                    {
                                        width = width + Convert.ToInt32(style_new[i]);
                                    }
                                    if (width == 100)
                                    {
                                        error_stat.InnerText = "Success"; //error_stat_green
                                        error_stat.Attributes.Add("class", "error_stat_green");
                                        Create_report_btn.Disabled = false;
                                    }
                                    else
                                    {
                                        error_stat.InnerText = "Width of the Columns are not 100% (Total)!!!";
                                        error_stat.Attributes.Add("class", "error_stat_red");
                                        Create_report_btn.Disabled = true;
                                    }
                                }
                                else
                                {
                                    error_stat.InnerText = "Style width Count and Count of Query Columns are not matching!!!";
                                    error_stat.Attributes.Add("class", "error_stat_red");
                                    Create_report_btn.Disabled = true;
                                }
                            }
                            else
                            {
                                error_stat.InnerText = "Style width not mentioned!!!";
                                error_stat.Attributes.Add("class", "error_stat_red");
                                Create_report_btn.Disabled = true;
                            }
                        }
                        else
                        {
                            error_stat.InnerText = "Column Names and Count of Query Columns are not matching!!!";
                            error_stat.Attributes.Add("class", "error_stat_red");
                            Create_report_btn.Disabled = true;
                        }
                    }
                    else
                    {
                        error_stat.InnerText = "New Column names not mentioned!!! ";
                        error_stat.Attributes.Add("class", "error_stat_red");
                        Create_report_btn.Disabled = true;
                    }
                    //
                }
                else
                {
                    if (dm.Rows.Count > 1)
                    {
                        //string[] col_num_cn = new_col.Value.Split(',');
                        //if (col_num_cn.Length == dm.Rows.Count)
                        //{
                        error_stat.InnerText = "Success";
                        error_stat.Attributes.Add("class", "error_stat_green");
                        Create_report_btn.Disabled = false;
                        //}
                        //else
                        //{                          
                        //}
                    }
                    else
                    {
                        error_stat.InnerText = "There is a flow in design.!!!";
                        error_stat.Attributes.Add("class", "error_stat_red");
                        Create_report_btn.Disabled = true;
                    }
                }
            
           
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('Please give Query to check !!!');", true);
            }

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
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('" + yy.Message + "');", true);               
            }
            finally
            {
                con.Close();
            }
            return dt;


        }
    }
}