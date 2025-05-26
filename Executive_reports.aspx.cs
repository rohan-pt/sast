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
    public partial class Executive_reports : System.Web.UI.Page
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


            if (!IsPostBack)
            {
                sessionlbl.Text = Session["id"].ToString();
                get_user();
                Load_drop_down();
                load_data(sender,e);
                Load_dept_name();
               // load_graph();
            }
            else
            {
                load_data(sender,e);
                //get_user();
            }
        }

        protected void Load_dept_name()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            string query = "";
            query = "select ROW_NUMBER() OVER (ORDER BY t.department ) AS serial_number,t.department as Reporting_Departing from ( select distinct  b.department  from Reporting_Dept a left join Executive_reports b on a.Reporting_Departing=b.department where a.Curr_stat='1')t where t.department is not null";
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
        protected void home_redirect(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }

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
                cmd1.Parameters.AddWithValue("@Page_Name", "Executive_report");
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

        protected void get_user()
        {
            string query = "select * from Employee_LoginTbl where Code='" + sessionlbl.Text + "'";
            //Employee_Name,Location
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            DataTable dt = get_normal_data(query, con);

            string branch = dt.Rows[0]["Location"].ToString();
            e_name.InnerText= dt.Rows[0]["Employee_Name"].ToString(); 
            Bran.InnerText= dt.Rows[0]["Location"].ToString();
            branch_id_pl.Text= get_info("select Loc_Id from Locationtbl where Location='" + branch + "'");
            if (branch_id_pl.Text=="2")
            {
                dllocation.Visible = true;
            }
            else
            {
                dllocation.Visible = false;
            }

          

        }
        protected void Load_drop_down()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            string query = "Select * from Locationtbl order by Location asc";
            DataTable dt = get_normal_data(query, con);
            dllocation.Items.Clear();
            dllocation.DataSource = dt;
            dllocation.DataBind();
            dllocation.DataTextField = "Location";
            dllocation.DataValueField = "SRNO";
            dllocation.DataBind();
            dllocation.Items.Insert(0, new ListItem("All Branches", "0"));

        }
        protected void download(object sender, EventArgs e)
        {
            Button b = sender as Button;
            string[] btnId = b.ID.Split('_');
            string query = "select * from  Executive_reports where ID = '" + btnId[1] + "'";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            DataTable dt = get_normal_data(query, con);

             query = dt.Rows[0]["Query"].ToString();

            string ConString = "";
            if (dt.Rows[0]["DB_CON"].ToString() == "M")
            {
                ConString = "Data Source=(DESCRIPTION =" +
            "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.175)(PORT = 1521))" +
            "(CONNECT_DATA =" +
              "(SERVER = DEDICATED)" +
              "(SERVICE_NAME = FCMIS)));" +
              "User Id=fcrhost_read;Password=fcmis_read$2023;Connection Timeout=600; Max Pool Size=150;";
            }
            else if (dt.Rows[0]["DB_CON"].ToString() == "E")
            {
                ConString = "Data Source=(DESCRIPTION =" +
           "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.175)(PORT = 1521))" +
           "(CONNECT_DATA =" +
             "(SERVER = DEDICATED)" +
             "(SERVICE_NAME = MISDB)));" +
             "User Id=misread;Password=m1sR3ad$2023;Connection Timeout=600; Max Pool Size=150;";
                /* "(SERVICE_NAME = MISDB)));" +*/
            }
            else if (dt.Rows[0]["DB_CON"].ToString() == "P")
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
            //Theme,Style_width
            DataTable dm = get_oracle_data(query, ConString);
            heavy_export(dm, "Test 1", "p1");
        }

        protected void heavy_export(DataTable table,string dep,string click_str)
        {
            //DataTable table = new DataTable();
            //table.Columns.AddRange(new[] { new DataColumn("Key"), new DataColumn("Value") });
            //foreach (string name in Request.ServerVariables)
            //    table.Rows.Add(name, Request.ServerVariables[name]);

            // This actually makes your HTML output to be downloaded as .xls file

            // string click_str = ddlreport.Items[Convert.ToInt32(Request.Form["ddlreport"].ToString())].ToString();
            // string dep = ddl_dep.Items[Convert.ToInt32(Request.Form["ddl_dep"].ToString())].ToString();
          
            string FileName = "" + dep + "_" + click_str + "_" + DateTime.Now + ".xls";
            Response.Clear();
            Response.ClearContent();
            Response.ContentType = "application/octet-stream";
            string style = @"<style> td { mso-number-format:\@;} </style>";
            Response.Write(style);
            Response.AddHeader("Content-Disposition", "attachment; filename=" + FileName);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            // Create a dynamic control, populate and render it
            GridView excel = new GridView();
            excel.DataSource = table;
            excel.DataBind();
            excel.GridLines = GridLines.Both;
            excel.HeaderStyle.Font.Bold = true;
            excel.RenderControl(new HtmlTextWriter(Response.Output));
            Response.Flush();
            Response.End();
        }

        protected void apply_new_setting(object sender, EventArgs e)
        {
            if (dllocation.Visible == true)
            {
                if (dllocation.SelectedItem.Text.ToString() == "All Branches")
                {
                    branch_id_pl.Text = "2";
                }
                else
                {
                    branch_id_pl.Text = get_info("select Loc_Id from Locationtbl where Location='" + dllocation.SelectedItem.Text.ToString() + "'");
                }
            }
           
           // load_data();
        }
        protected void load_data(object sender, EventArgs e)
        {
            if (dllocation.Visible == true)
            {
                if (dllocation.SelectedItem.Text.ToString() == "All Branches")
                {
                    branch_id_pl.Text = "2";
                }
                else
                {
                    branch_id_pl.Text = get_info("select Loc_Id from Locationtbl where Location='" + dllocation.SelectedItem.Text.ToString() + "'");
                }
            }

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);

            string query1 = "select * from Executive_reports where  Report_Status!='Z'";
            string query = "";
            query = query1 + " and department='"+ ddl_dep.SelectedItem.Text + "'";
            int p = 0;
            //    if(chk1.Checked==true)
            //{
            //    query = query1 + " and Is_casa=1";
            //    p = 1;
            //}
            //if (chk2.Checked == true)
            //{
            //    if (p == 0)
            //    {
            //        query = query1 + " and Is_LN=1";
            //        p = 2;
            //    }
            //    else
            //    {
            //        query = query1 + " and (Is_casa=1 or Is_LN=1)";
            //        p = 3;
            //    }
            //}
            //if (chk3.Checked == true)
            //{
            //    if (p == 0)
            //    {
            //        query = query1 + " and Is_TD=1";
            //    }
            //    else if (p == 1)
            //    {
            //        query = query1 + " and ( Is_casa=1 or Is_TD=1)";
            //    }
            //    else if (p == 2)
            //    {
            //        query = query1 + " and ( Is_LN=1 or Is_TD=1)";
            //    }
            //    else
            //    {
            //        query = query1 + " and ( Is_casa=1 or Is_LN=1 or Is_TD=1)";
            //    }
            //}

            DataTable dt = get_normal_data(query, con);
            all_report.InnerHtml = "";
            if (dt.Rows.Count > 0)
            {
                int r = 0;
                System.Web.UI.HtmlControls.HtmlGenericControl newdivs_temp = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                  
                    if (dt.Rows[i]["Module_Type"].ToString() == "T")
                    {
                       
                        if (dt.Rows[i]["Full_width"].ToString() == "N" && r==0)
                        {
                            System.Web.UI.HtmlControls.HtmlGenericControl newdivs = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                            newdivs.Attributes.Add("class", "report_container");
                            r = create_div1(dt, ref i, newdivs);
                            if(r==1)
                            {
                                newdivs_temp = newdivs;
                            }
                        }
                        else if(dt.Rows[i]["Full_width"].ToString() == "N" && r == 1)
                        {
                            create_div3(dt, ref i, newdivs_temp);
                            r = 0;
                        }
                        else
                        {
                            System.Web.UI.HtmlControls.HtmlGenericControl newdivs = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                            newdivs.Attributes.Add("class", "report_container");
                            create_div2(dt, ref i, newdivs);
                            r = 0;
                        }
                    }
                    else
                    {
                        if (dt.Rows[i]["Full_width"].ToString() == "N" && r == 0)
                        {
                            System.Web.UI.HtmlControls.HtmlGenericControl newdivs = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                            newdivs.Attributes.Add("class", "report_container");
                            r = create_graph(dt, ref i, newdivs);
                            if (r == 1)
                            {
                                newdivs_temp = newdivs;
                            }
                        }
                        else if (dt.Rows[i]["Full_width"].ToString() == "N" && r == 1)
                        {
                            create_graph3(dt, ref i, newdivs_temp);
                            r = 0;
                        }
                        else
                        {
                            System.Web.UI.HtmlControls.HtmlGenericControl newdivs = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                            newdivs.Attributes.Add("class", "report_container");
                            create_graph2(dt, ref i, newdivs);
                            r = 0;
                        }
                    }
                }
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "gif_hide", "hide_gif();", true);
        }

        private void create_graph3(DataTable dt, ref int i, System.Web.UI.HtmlControls.HtmlGenericControl newdivs)
        {
            string cls = "sectionG2";
            System.Web.UI.HtmlControls.HtmlGenericControl secdiv = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
            secdiv.ID = "Sec_" + i;
            secdiv.Attributes.Add("class", cls);
            newdivs.Controls.Add(secdiv);

            string title = dt.Rows[i]["Report_Name"].ToString();

            string query = "";
           
            if (branch_id_pl.Text == "2")
            {
                query = dt.Rows[i]["Query"].ToString();
            }
            else
            {
                query = dt.Rows[i]["Branch_Query"].ToString();
                query = query.Replace("####$$$$", branch_id_pl.Text);
            }

            string ConString = "";
            if (dt.Rows[i]["DB_CON"].ToString() == "M")
            {
                ConString = "Data Source=(DESCRIPTION =" +
            "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.175)(PORT = 1521))" +
            "(CONNECT_DATA =" +
              "(SERVER = DEDICATED)" +
              "(SERVICE_NAME = FCMIS)));" +
              "User Id=fcrhost_read;Password=fcmis_read$2023;Connection Timeout=600; Max Pool Size=150;";
            }
            else if (dt.Rows[i]["DB_CON"].ToString() == "E")
            {
                ConString = "Data Source=(DESCRIPTION =" +
           "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.175)(PORT = 1521))" +
           "(CONNECT_DATA =" +
             "(SERVER = DEDICATED)" +
             "(SERVICE_NAME = MISDB)));" +
             "User Id=misread;Password=m1sR3ad$2023;Connection Timeout=600; Max Pool Size=150;";
                /* "(SERVICE_NAME = MISDB)));" +*/
            }
            else if (dt.Rows[i]["DB_CON"].ToString() == "P")
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
            //Theme,Style_width
            DataTable dm = get_oracle_data(query, ConString);
            string[] Column_Name = dt.Rows[i]["New_column_name"].ToString().Split(',');
            string[] style = dt.Rows[i]["Style_width"].ToString().Split(',');
            string color = "#e0baba";
            if (style.Length == Column_Name.Length)
            {

            }
            else
            {
                color = "#e0baba";
            }

            string output = "";

            for (int j = 0; j < dm.Rows.Count; j++)
            {
                if (dm.Columns.Count < 3)
                {
                    if (j == 0)
                    {
                        if (style.Length == Column_Name.Length)
                        {
                            if (dm.Rows.Count == Column_Name.Length)
                            {
                                output = Column_Name[j] + ":" + dm.Rows[j][1].ToString() + ":" + style[j];
                            }
                            else
                            {
                                output = dm.Rows[j][0].ToString() + ":" + dm.Rows[j][1].ToString() + ":" + color;
                            }
                        }
                        else
                        {
                            if (dm.Rows.Count == Column_Name.Length)
                            {
                                output = Column_Name[j] + ":" + dm.Rows[j][1].ToString() + ":" + color;
                            }
                            else if (dm.Rows.Count == style.Length)
                            {
                                output = dm.Rows[j][0].ToString() + ":" + dm.Rows[j][1].ToString() + ":" + style[j];
                            }
                            else
                            {
                                output = dm.Rows[j][0].ToString() + ":" + dm.Rows[j][1].ToString() + ":" + color;
                            }                               
                        }

                    }
                    else
                    {
                        if (style.Length == Column_Name.Length)
                        {
                            if (dm.Rows.Count == Column_Name.Length)
                            {
                                output = output + "," + Column_Name[j] + ":" + dm.Rows[j][1].ToString() + ":" + style[j];
                            }
                            else
                            {
                                output = output + "," + dm.Rows[j][0].ToString() + ":" + dm.Rows[j][1].ToString() + ":" + color;
                            }
                        }
                        else
                        {
                            if (dm.Rows.Count == Column_Name.Length)
                            {
                                output = output + "," + Column_Name[j] + ":" + dm.Rows[j][1].ToString() + ":" + color;
                            }
                            else if (dm.Rows.Count == style.Length)
                            {
                                output = output + "," + dm.Rows[j][0].ToString() + ":" + dm.Rows[j][1].ToString() + ":" + style[j];
                            }
                            else
                            {
                                output = output + "," + dm.Rows[j][0].ToString() + ":" + dm.Rows[j][1].ToString() + ":" + color;
                            }
                        }
                    }
                }
                else
                {
                    string output1 = "";
                    for (int q = 0; q < dm.Columns.Count; q++)
                    {
                        if (q == 0)
                        {
                            output1 = dm.Rows[j][q].ToString();

                        }
                        else
                        {
                            output1 = output1 + ":" + dm.Rows[j][q].ToString();
                        }

                    }
                    if (j == 0)
                    {
                        output = output1;
                    }
                    else
                    {
                        output = output + "," + output1;
                    }
                }
            }
            string theme = "Sec_" + i;
            if (dm.Columns.Count < 3)
            {
                if (dt.Rows[i]["Theme"].ToString() == "P")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Grm1" + i, "callone('" + theme + "','" + output + "','" + title + "');", true);
                }
                else if (dt.Rows[i]["Theme"].ToString() == "B")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Grm2" + i, "calltwo('" + theme + "','" + output + "','" + title + "');", true);
                }
                else if (dt.Rows[i]["Theme"].ToString() == "C")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Grm3" + i, "callthree('" + theme + "','" + output + "','" + title + "');", true);
                }
                else if (dt.Rows[i]["Theme"].ToString() == "D")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Grm4" + i, "callfour('" + theme + "','" + output + "','" + title + "');", true);
                }
            }
            else
            {
                string idv = "";
                if (dm.Columns.Count == Column_Name.Length)
                {
                    for (int r = 0; r < Column_Name.Length; r++)
                    {
                        if (r == 0)
                        {
                            idv = Column_Name[r];
                        }
                        else
                        {
                            idv = idv + "," + Column_Name[r];
                        }
                    }

                }
                else
                {
                    for (int r = 0; r < dm.Columns.Count; r++)
                    {
                        if (r == 0)
                        {
                            idv = dm.Columns[r].ColumnName.ToString();
                        }
                        else
                        {
                            idv = idv + "," + dm.Columns[r].ColumnName.ToString();
                        }
                    }
                }

                ScriptManager.RegisterStartupScript(this, GetType(), "Grm88" + i, "callfive('" + idv + "','" + theme + "','" + output + "','" + title + "');", true);
            }

        }

        private void create_graph2(DataTable dt, ref int i, System.Web.UI.HtmlControls.HtmlGenericControl newdivs)
        {           
            string cls = "sectionG";
            //System.Web.UI.HtmlControls.HtmlGenericControl newdivs = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
            //newdivs.Attributes.Add("class", "report_container");
            System.Web.UI.HtmlControls.HtmlGenericControl secdiv = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
            secdiv.ID = "Sec_" + i;
            secdiv.Attributes.Add("class", cls);
            newdivs.Controls.Add(secdiv);


            string title = dt.Rows[i]["Report_Name"].ToString();
            all_report.Controls.Add(newdivs);

            string query = "";
           
            if (branch_id_pl.Text == "2")
            {
                query = dt.Rows[i]["Query"].ToString();
            }
            else
            {
                query = dt.Rows[i]["Branch_Query"].ToString();
                query = query.Replace("####$$$$", branch_id_pl.Text);
            }

            string ConString = "";
            if (dt.Rows[i]["DB_CON"].ToString() == "M")
            {
                ConString = "Data Source=(DESCRIPTION =" +
            "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.175)(PORT = 1521))" +
            "(CONNECT_DATA =" +
              "(SERVER = DEDICATED)" +
              "(SERVICE_NAME = FCMIS)));" +
              "User Id=fcrhost_read;Password=fcmis_read$2023;Connection Timeout=600; Max Pool Size=150;";
            }
            else if (dt.Rows[i]["DB_CON"].ToString() == "E")
            {
                ConString = "Data Source=(DESCRIPTION =" +
           "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.175)(PORT = 1521))" +
           "(CONNECT_DATA =" +
             "(SERVER = DEDICATED)" +
             "(SERVICE_NAME = MISDB)));" +
             "User Id=misread;Password=m1sR3ad$2023;Connection Timeout=600; Max Pool Size=150;";
                /* "(SERVICE_NAME = MISDB)));" +*/
            }
            else if (dt.Rows[i]["DB_CON"].ToString() == "P")
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
            //Theme,Style_width
            DataTable dm = get_oracle_data(query, ConString);
            string[] Column_Name = dt.Rows[i]["New_column_name"].ToString().Split(',');
            string[] style = dt.Rows[i]["Style_width"].ToString().Split(',');
            string color = "#e0baba";
            if (style.Length == Column_Name.Length)
            {

            }
            else
            {
                color = "#e0baba";
            }

            string output = "";

            for (int j = 0; j < dm.Rows.Count; j++)
            {
                if (dm.Columns.Count < 3)
                {
                    if (j == 0)
                    {
                        if (style.Length == Column_Name.Length)
                        {
                            if (dm.Rows.Count == Column_Name.Length)
                            {
                                output = Column_Name[j] + ":" + dm.Rows[j][1].ToString() + ":" + style[j];
                            }
                            else
                            {
                                output = dm.Rows[j][0].ToString() + ":" + dm.Rows[j][1].ToString() + ":" + color;
                            }
                        }
                        else
                        {
                            if (dm.Rows.Count == Column_Name.Length)
                            {
                                output = Column_Name[j] + ":" + dm.Rows[j][1].ToString() + ":" + color;
                            }
                            else if (dm.Rows.Count == style.Length)
                            {
                                output = dm.Rows[j][0].ToString() + ":" + dm.Rows[j][1].ToString() + ":" + style[j];
                            }
                            else
                            {
                                output = dm.Rows[j][0].ToString() + ":" + dm.Rows[j][1].ToString() + ":" + color;
                            }
                        }

                    }
                    else
                    {
                        if (style.Length == Column_Name.Length)
                        {
                            if (dm.Rows.Count == Column_Name.Length)
                            {
                                output = output + "," + Column_Name[j] + ":" + dm.Rows[j][1].ToString() + ":" + style[j];
                            }
                            else
                            {
                                output = output + "," + dm.Rows[j][0].ToString() + ":" + dm.Rows[j][1].ToString() + ":" + color;
                            }
                        }
                        else
                        {
                            if (dm.Rows.Count == Column_Name.Length)
                            {
                                output = output + "," + Column_Name[j] + ":" + dm.Rows[j][1].ToString() + ":" + color;
                            }
                            else if (dm.Rows.Count == style.Length)
                            {
                                output = output + "," + dm.Rows[j][0].ToString() + ":" + dm.Rows[j][1].ToString() + ":" + style[j];
                            }
                            else
                            {
                                output = output + "," + dm.Rows[j][0].ToString() + ":" + dm.Rows[j][1].ToString() + ":" + color;
                            }
                        }
                    }
                }
                else
                {
                    string output1 = "";
                    for (int q = 0; q < dm.Columns.Count; q++)
                    {
                        if (q == 0)
                        {
                            output1 = dm.Rows[j][q].ToString();

                        }
                        else
                        {
                            output1 = output1 + ":" + dm.Rows[j][q].ToString();
                        }

                    }
                    if (j == 0)
                    {
                        output = output1;
                    }
                    else
                    {
                        output = output + "," + output1;
                    }
                }
            }
            string theme = "Sec_" + i;
            if (dm.Columns.Count < 3)
            {
                if (dt.Rows[i]["Theme"].ToString() == "P")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Grm1" + i, "callone('" + theme + "','" + output + "','" + title + "');", true);
                }
                else if (dt.Rows[i]["Theme"].ToString() == "B")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Grm2" + i, "calltwo('" + theme + "','" + output + "','" + title + "');", true);
                }
                else if (dt.Rows[i]["Theme"].ToString() == "C")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Grm3" + i, "callthree('" + theme + "','" + output + "','" + title + "');", true);
                }
                else if (dt.Rows[i]["Theme"].ToString() == "D")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Grm4" + i, "callfour('" + theme + "','" + output + "','" + title + "');", true);
                }
            }
            else
            {
                string idv = "";
                if (dm.Columns.Count == Column_Name.Length)
                {
                    for (int r = 0; r < Column_Name.Length; r++)
                    {
                        if (r == 0)
                        {
                            idv = Column_Name[r];
                        }
                        else
                        {
                            idv = idv + "," + Column_Name[r];
                        }
                    }

                }
                else
                {
                    for (int r = 0; r < dm.Columns.Count; r++)
                    {
                        if (r == 0)
                        {
                            idv = dm.Columns[r].ColumnName.ToString();
                        }
                        else
                        {
                            idv = idv + "," + dm.Columns[r].ColumnName.ToString();
                        }
                    }
                }

                ScriptManager.RegisterStartupScript(this, GetType(), "Grm88" + i, "callfive('" + idv + "','" + theme + "','" + output + "','" + title + "');", true);
            }


        }

        private int create_graph(DataTable dt, ref int i, System.Web.UI.HtmlControls.HtmlGenericControl newdivs)
        {
            int ret = 0;
            string cls = "sectionG1";
            //System.Web.UI.HtmlControls.HtmlGenericControl newdivs = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
            //newdivs.Attributes.Add("class", "report_container");
            System.Web.UI.HtmlControls.HtmlGenericControl secdiv = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
            secdiv.ID = "Sec_" + i;
            secdiv.Attributes.Add("class", cls);
            newdivs.Controls.Add(secdiv);           
           
           
            string title = dt.Rows[i]["Report_Name"].ToString();           
            all_report.Controls.Add(newdivs);  

            string query = "";
           
            if (branch_id_pl.Text == "2")
            {
                query = dt.Rows[i]["Query"].ToString();
            }
            else
            {
                query = dt.Rows[i]["Branch_Query"].ToString();
                query = query.Replace("####$$$$", branch_id_pl.Text);
            }

            string ConString = "";
            if (dt.Rows[i]["DB_CON"].ToString() == "M")
            {
                ConString = "Data Source=(DESCRIPTION =" +
            "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.175)(PORT = 1521))" +
            "(CONNECT_DATA =" +
              "(SERVER = DEDICATED)" +
              "(SERVICE_NAME = FCMIS)));" +
              "User Id=fcrhost_read;Password=fcmis_read$2023;Connection Timeout=600; Max Pool Size=150;";
            }
            else if (dt.Rows[i]["DB_CON"].ToString() == "E")
            {
                ConString = "Data Source=(DESCRIPTION =" +
           "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.175)(PORT = 1521))" +
           "(CONNECT_DATA =" +
             "(SERVER = DEDICATED)" +
             "(SERVICE_NAME = MISDB)));" +
             "User Id=misread;Password=m1sR3ad$2023;Connection Timeout=600; Max Pool Size=150;";
                /* "(SERVICE_NAME = MISDB)));" +*/
            }
            else if (dt.Rows[i]["DB_CON"].ToString() == "P")
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
            //Theme,Style_width
            DataTable dm = get_oracle_data(query, ConString);
            string[] Column_Name = dt.Rows[i]["New_column_name"].ToString().Split(',');
            string[] style = dt.Rows[i]["Style_width"].ToString().Split(',');

            string color = "#e0baba";
            if (style.Length == Column_Name.Length)
            {

            }
            else
            {
                color = "#e0baba";
            }

            string output = "";

            for (int j = 0; j < dm.Rows.Count; j++)
            {
                if (dm.Columns.Count < 3)
                {
                    if (j == 0)
                    {
                        if (style.Length == Column_Name.Length)
                        {
                            if (dm.Rows.Count == Column_Name.Length)
                            {
                                output = Column_Name[j] + ":" + dm.Rows[j][1].ToString() + ":" + style[j];
                            }
                            else
                            {
                                output = dm.Rows[j][0].ToString() + ":" + dm.Rows[j][1].ToString() + ":" + color;
                            }
                        }
                        else
                        {
                            if (dm.Rows.Count == Column_Name.Length)
                            {
                                output = Column_Name[j] + ":" + dm.Rows[j][1].ToString() + ":" + color;
                            }
                            else if (dm.Rows.Count == style.Length)
                            {
                                output = dm.Rows[j][0].ToString() + ":" + dm.Rows[j][1].ToString() + ":" + style[j];
                            }
                            else
                            {
                                output = dm.Rows[j][0].ToString() + ":" + dm.Rows[j][1].ToString() + ":" + color;
                            }
                        }

                    }
                    else
                    {
                        if (style.Length == Column_Name.Length)
                        {
                            if (dm.Rows.Count == Column_Name.Length)
                            {
                                output = output + "," + Column_Name[j] + ":" + dm.Rows[j][1].ToString() + ":" + style[j];
                            }
                            else
                            {
                                output = output + "," + dm.Rows[j][0].ToString() + ":" + dm.Rows[j][1].ToString() + ":" + color;
                            }
                        }
                        else
                        {
                            if (dm.Rows.Count == Column_Name.Length)
                            {
                                output = output + "," + Column_Name[j] + ":" + dm.Rows[j][1].ToString() + ":" + color;
                            }
                            else if (dm.Rows.Count == style.Length)
                            {
                                output = output + "," + dm.Rows[j][0].ToString() + ":" + dm.Rows[j][1].ToString() + ":" + style[j];
                            }
                            else
                            {
                                output = output + "," + dm.Rows[j][0].ToString() + ":" + dm.Rows[j][1].ToString() + ":" + color;
                            }
                        }
                    }
                }
                else
                {
                    string output1 = "";
                    for (int q = 0; q < dm.Columns.Count; q++)
                    {
                        if (q == 0)
                        {
                            output1 = dm.Rows[j][q].ToString();

                        }
                        else
                        {
                            output1 = output1 + ":" + dm.Rows[j][q].ToString();
                        }

                    }
                    if (j == 0)
                    {
                        output = output1;
                    }
                    else
                    {
                        output = output + "," + output1;
                    }
                }
            }
            string theme = "Sec_" + i;
            if (dm.Columns.Count <3)
            {
                if (dt.Rows[i]["Theme"].ToString() == "P")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Grm1" + i, "callone('" + theme + "','" + output + "','" + title + "');", true);
                }
                else if (dt.Rows[i]["Theme"].ToString() == "B")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Grm2" + i, "calltwo('" + theme + "','" + output + "','" + title + "');", true);
                }
                else if (dt.Rows[i]["Theme"].ToString() == "C")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Grm3" + i, "callthree('" + theme + "','" + output + "','" + title + "');", true);
                }
                else if (dt.Rows[i]["Theme"].ToString() == "D")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Grm4" + i, "callfour('" + theme + "','" + output + "','" + title + "');", true);
                }
            }
            else
            {
                string idv = "";
                if(dm.Columns.Count == Column_Name.Length)
                {
                    for(int r=0;r< Column_Name.Length;r++)
                    {
                        if (r == 0)
                        {
                            idv = Column_Name[r];
                        }
                        else
                        {
                            idv = idv + "," + Column_Name[r];
                        }
                    }
                    
                }
                else
                {
                    for (int r = 0; r < dm.Columns.Count; r++)
                    {
                        if (r == 0)
                        {
                            idv = dm.Columns[r].ColumnName.ToString();
                        }
                        else
                        {
                            idv = idv + "," + dm.Columns[r].ColumnName.ToString();
                        }
                    }
                }

                ScriptManager.RegisterStartupScript(this, GetType(), "Grm88" + i, "callfive('"+idv+"','" + theme + "','" + output + "','" + title + "');", true);
            }

            if ((i + 1) < dt.Rows.Count)
            {
                int k = i + 1;
                cls = "sectionG2";
                if (dt.Rows[k]["Full_width"].ToString() == "N" && dt.Rows[k]["Module_Type"].ToString() == "G")
                {
                    System.Web.UI.HtmlControls.HtmlGenericControl secdiv2 = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                    secdiv2.ID = "Sec_" + k;
                    secdiv2.Attributes.Add("class", cls);
                    newdivs.Controls.Add(secdiv2);


                    title = dt.Rows[k]["Report_Name"].ToString();
                    all_report.Controls.Add(newdivs);

                    query = "";
                    if (branch_id_pl.Text == "2")
                    {
                        query = dt.Rows[k]["Query"].ToString();
                    }
                    else
                    {
                        query = dt.Rows[k]["Branch_Query"].ToString();
                        query = query.Replace("####$$$$", branch_id_pl.Text);
                    }

                    ConString = "";
                    if (dt.Rows[k]["DB_CON"].ToString() == "M")
                    {
                        ConString = "Data Source=(DESCRIPTION =" +
                    "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.175)(PORT = 1521))" +
                    "(CONNECT_DATA =" +
                      "(SERVER = DEDICATED)" +
                      "(SERVICE_NAME = FCMIS)));" +
                      "User Id=fcrhost_read;Password=fcmis_read$2023;Connection Timeout=600; Max Pool Size=150;";
                    }
                    else if (dt.Rows[k]["DB_CON"].ToString() == "E")
                    {
                        ConString = "Data Source=(DESCRIPTION =" +
                   "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.175)(PORT = 1521))" +
                   "(CONNECT_DATA =" +
                     "(SERVER = DEDICATED)" +
                     "(SERVICE_NAME = MISDB)));" +
                     "User Id=misread;Password=m1sR3ad$2023;Connection Timeout=600; Max Pool Size=150;";
                        /* "(SERVICE_NAME = MISDB)));" +*/
                    }
                    else if (dt.Rows[k]["DB_CON"].ToString() == "P")
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
                    //Theme,Style_width
                    DataTable dmi = get_oracle_data(query, ConString);
                    string[] row_Name = dt.Rows[k]["New_column_name"].ToString().Split(',');
                    string[] style_2 = dt.Rows[k]["Style_width"].ToString().Split(',');




                     color = "#062eae";
                    if (style_2.Length == row_Name.Length)
                    {

                    }
                    else
                    {
                        color = "#062eae";
                    }

                     output = "";

                    for (int j = 0; j < dmi.Rows.Count; j++)
                    {
                        if (dmi.Columns.Count < 3)
                        {
                            if (j == 0)
                            {
                                if (style_2.Length == row_Name.Length)
                                {
                                    if (dmi.Rows.Count == row_Name.Length)
                                    {
                                        output = row_Name[j] + ":" + dmi.Rows[j][1].ToString() + ":" + style_2[j];
                                    }
                                    else
                                    {
                                        output = dmi.Rows[j][0].ToString() + ":" + dmi.Rows[j][1].ToString() + ":" + color;
                                    }
                                }
                                else
                                {
                                    if (dmi.Rows.Count == row_Name.Length)
                                    {
                                        output = row_Name[j] + ":" + dmi.Rows[j][1].ToString() + ":" + color;
                                    }
                                    else if (dmi.Rows.Count == style_2.Length)
                                    {
                                        output = dmi.Rows[j][0].ToString() + ":" + dmi.Rows[j][1].ToString() + ":" + style_2[j];
                                    }
                                    else
                                    {
                                        output = dmi.Rows[j][0].ToString() + ":" + dmi.Rows[j][1].ToString() + ":" + color;
                                    }
                                }

                            }
                            else
                            {
                                if (style_2.Length == row_Name.Length)
                                {
                                    if (dmi.Rows.Count == row_Name.Length)
                                    {
                                        output = output + "," + row_Name[j] + ":" + dmi.Rows[j][1].ToString() + ":" + style_2[j];
                                    }
                                    else
                                    {
                                        output = output + "," + dmi.Rows[j][0].ToString() + ":" + dmi.Rows[j][1].ToString() + ":" + color;
                                    }
                                }
                                else
                                {
                                    if (dmi.Rows.Count == row_Name.Length)
                                    {
                                        output = output + "," + row_Name[j] + ":" + dmi.Rows[j][1].ToString() + ":" + color;
                                    }
                                    else if (dmi.Rows.Count == style_2.Length)
                                    {
                                        output = output+ ","+ dmi.Rows[j][0].ToString() + ":" + dmi.Rows[j][1].ToString() + ":" + style_2[j];
                                    }
                                    else
                                    {
                                        output = output + ","+ dmi.Rows[j][0].ToString() + ":" + dmi.Rows[j][1].ToString() + ":" + color;
                                    }
                                }
                            }
                        }
                        else
                        {
                            string output1 = "";
                            for (int q = 0; q < dmi.Columns.Count; q++)
                            {
                                if (q == 0)
                                {
                                    output1 = dmi.Rows[j][q].ToString();

                                }
                                else
                                {
                                    output1 = output1 + ":" + dmi.Rows[j][q].ToString();
                                }

                            }
                            if (j == 0)
                            {
                                output = output1;
                            }
                            else
                            {
                                output = output + "," + output1;
                            }
                        }
                    }
                    theme = "Sec_" + k;
                    if (dmi.Columns.Count < 3)
                    {
                        if (dt.Rows[k]["Theme"].ToString() == "P")
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "Grm1" + k, "callone('" + theme + "','" + output + "','" + title + "');", true);
                        }
                        else if (dt.Rows[k]["Theme"].ToString() == "B")
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "Grm2" + k, "calltwo('" + theme + "','" + output + "','" + title + "');", true);
                        }
                        else if (dt.Rows[k]["Theme"].ToString() == "C")
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "Grm3" + k, "callthree('" + theme + "','" + output + "','" + title + "');", true);
                        }
                        else if (dt.Rows[k]["Theme"].ToString() == "D")
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "Grm4" + k, "callfour('" + theme + "','" + output + "','" + title + "');", true);
                        }
                    }
                    else
                    {
                        string idv = "";
                        if (dmi.Columns.Count == Column_Name.Length)
                        {
                            for (int r = 0; r < Column_Name.Length; r++)
                            {
                                if (r == 0)
                                {
                                    idv = Column_Name[r];
                                }
                                else
                                {
                                    idv = idv + "," + Column_Name[r];
                                }
                            }

                        }
                        else
                        {
                            for (int r = 0; r < dmi.Columns.Count; r++)
                            {
                                if (r == 0)
                                {
                                    idv = dmi.Columns[r].ColumnName.ToString();
                                }
                                else
                                {
                                    idv = idv + "," + dmi.Columns[r].ColumnName.ToString();
                                }
                            }
                        }

                        ScriptManager.RegisterStartupScript(this, GetType(), "Grm88" + k, "callfive('" + idv + "','" + theme + "','" + output + "','" + title + "');", true);
                    }
                        i = i + 1;
                }
                else
                {
                    ret = 1;
                }
            }
            return ret;
        }


        private void create_div3(DataTable dt, ref int i, System.Web.UI.HtmlControls.HtmlGenericControl newdivs)
        {
            string cls = "section2";
            System.Web.UI.HtmlControls.HtmlGenericControl secdiv = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
            secdiv.Attributes.Add("class", cls);
            newdivs.Controls.Add(secdiv);
            System.Web.UI.HtmlControls.HtmlGenericControl rep_til = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
            System.Web.UI.HtmlControls.HtmlGenericControl header_til = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
            System.Web.UI.HtmlControls.HtmlGenericControl body_div = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
            string theme = "";
            if (dt.Rows[i]["Theme"].ToString() == "P")
            {
                theme = "plain";
            }
            else if (dt.Rows[i]["Theme"].ToString() == "B")
            {
                theme = "blue";
            }
            else if (dt.Rows[i]["Theme"].ToString() == "I")
            {
                theme = "ice";
            }
            else if (dt.Rows[i]["Theme"].ToString() == "H")
            {
                theme = "hot";
            }
            rep_til.Attributes.Add("class", "title_of_rep_" + theme);
            rep_til.InnerText = dt.Rows[i]["Report_Name"].ToString();
            header_til.Attributes.Add("class", "header_div_tbl_til");
            body_div.Attributes.Add("class", "header_div_tbl_til_body");
            Button bt1 = new Button();
            bt1.Text = "Download";
            bt1.CssClass = "dv_btn";
            bt1.Attributes.Add("runat", "server");
            bt1.Attributes["type"] = "button";
            bt1.ID = "Download_" + dt.Rows[i]["ID"].ToString();
            bt1.Click += new EventHandler(download);
            if (dt.Rows[i]["Note"].ToString() != "")
            {
                System.Web.UI.HtmlControls.HtmlGenericControl b2 = new System.Web.UI.HtmlControls.HtmlGenericControl("button");
                b2.ID = "Redirect" + dt.Rows[i]["ID"].ToString();
                b2.InnerText = "Main Report";
                b2.Attributes.Add("class", "dv_btn0");
                b2.Attributes.Add("onclick", "replace('" + dt.Rows[i]["Note"].ToString() + "');");
                header_til.Controls.Add(b2);
            }
            //secdiv.Controls.Add(bt1);
            header_til.Controls.Add(bt1);
            header_til.Controls.Add(rep_til);
            //secdiv.Controls.Add(rep_til);
            System.Web.UI.HtmlControls.HtmlGenericControl table1 = new System.Web.UI.HtmlControls.HtmlGenericControl("table");
            System.Web.UI.HtmlControls.HtmlGenericControl table = new System.Web.UI.HtmlControls.HtmlGenericControl("table");
            table1.Attributes.Add("class", "table_XX");
            table.Attributes.Add("class", "table_" + theme + "");

            string query = "";
            if (branch_id_pl.Text == "2")
            {
                query = dt.Rows[i]["Query"].ToString();
            }
            else
            {
                query = dt.Rows[i]["Branch_Query"].ToString();
                query = query.Replace("####$$$$", branch_id_pl.Text);
            }

            string ConString = "";
            if (dt.Rows[i]["DB_CON"].ToString() == "M")
            {
                ConString = "Data Source=(DESCRIPTION =" +
            "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.175)(PORT = 1521))" +
            "(CONNECT_DATA =" +
              "(SERVER = DEDICATED)" +
              "(SERVICE_NAME = FCMIS)));" +
              "User Id=fcrhost_read;Password=fcmis_read$2023;Connection Timeout=600; Max Pool Size=150;";
            }
            else if (dt.Rows[i]["DB_CON"].ToString() == "E")
            {
                ConString = "Data Source=(DESCRIPTION =" +
           "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.175)(PORT = 1521))" +
           "(CONNECT_DATA =" +
             "(SERVER = DEDICATED)" +
             "(SERVICE_NAME = MISDB)));" +
             "User Id=misread;Password=m1sR3ad$2023;Connection Timeout=600; Max Pool Size=150;";
                /* "(SERVICE_NAME = MISDB)));" +*/
            }
            else if (dt.Rows[i]["DB_CON"].ToString() == "P")
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
            //Theme,Style_width
            DataTable dm = get_oracle_data(query, ConString);
            string[] Column_Name = dt.Rows[i]["New_column_name"].ToString().Split(',');
            string[] style = dt.Rows[i]["Style_width"].ToString().Split(',');

            string output = "";
            output = output + "<tr>";
            for (int q = 0; q < Column_Name.Length; q++)
            {
                output = output + "<td class='td_" + theme + "_th' style='width:" + style[q] + "%;'>" + Column_Name[q] + "</td>";
            }
            output = output + "</tr>";
            table1.InnerHtml = output;
            header_til.Controls.Add(table1);
            output = "";
            for (int j = 0; j < dm.Rows.Count; j++)
            {
                output = output + "<tr>";
                for (int q = 0; q < style.Length; q++)
                {
                    output = output + "<td class='td_" + theme + "_td' style='width:" + style[q] + "%;'>" + dm.Rows[j][q].ToString() + "</td>";
                }
                output = output + "</tr>";
            }
            table.InnerHtml = output;
            body_div.Controls.Add(table);
            secdiv.Controls.Add(header_til);
            secdiv.Controls.Add(body_div);
        }


        private void create_div2(DataTable dt, ref int i, System.Web.UI.HtmlControls.HtmlGenericControl newdivs)
        {
            string cls = "section";
            //System.Web.UI.HtmlControls.HtmlGenericControl newdivs = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
            //newdivs.Attributes.Add("class", "report_container");
            System.Web.UI.HtmlControls.HtmlGenericControl secdiv = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
            secdiv.Attributes.Add("class", cls);
            newdivs.Controls.Add(secdiv);
            System.Web.UI.HtmlControls.HtmlGenericControl rep_til = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
            System.Web.UI.HtmlControls.HtmlGenericControl header_til = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
            System.Web.UI.HtmlControls.HtmlGenericControl body_div = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
            string theme = "";
            if (dt.Rows[i]["Theme"].ToString() == "P")
            {
                theme = "plain";
            }
            else if (dt.Rows[i]["Theme"].ToString() == "B")
            {
                theme = "blue";
            }
            else if (dt.Rows[i]["Theme"].ToString() == "I")
            {
                theme = "ice";
            }
            else if (dt.Rows[i]["Theme"].ToString() == "H")
            {
                theme = "hot";
            }
            rep_til.Attributes.Add("class", "title_of_rep_" + theme);
            rep_til.InnerText = dt.Rows[i]["Report_Name"].ToString();
            header_til.Attributes.Add("class", "header_div_tbl_til");
            body_div.Attributes.Add("class", "header_div_tbl_til_body");
            Button bt1 = new Button();
            bt1.Text = "Download";
            bt1.CssClass = "dv_btn";
            bt1.Attributes.Add("runat", "server");
            bt1.Attributes["type"] = "button";
            bt1.ID = "Download_" + dt.Rows[i]["ID"].ToString();
            bt1.Click += new EventHandler(download);
            if (dt.Rows[i]["Note"].ToString() != "")
            {
                System.Web.UI.HtmlControls.HtmlGenericControl b2 = new System.Web.UI.HtmlControls.HtmlGenericControl("button");
                b2.ID = "Redirect" + dt.Rows[i]["ID"].ToString();
                b2.InnerText = "Main Report";
                b2.Attributes.Add("class", "dv_btn0");
                b2.Attributes.Add("onclick", "replace('" + dt.Rows[i]["Note"].ToString() + "');");
                header_til.Controls.Add(b2);
            }
            //secdiv.Controls.Add(bt1);
            header_til.Controls.Add(bt1);
            header_til.Controls.Add(rep_til);
            //secdiv.Controls.Add(rep_til);
            all_report.Controls.Add(newdivs);
            System.Web.UI.HtmlControls.HtmlGenericControl table1 = new System.Web.UI.HtmlControls.HtmlGenericControl("table");
            System.Web.UI.HtmlControls.HtmlGenericControl table = new System.Web.UI.HtmlControls.HtmlGenericControl("table");
            table1.Attributes.Add("class", "table_XX");
            table.Attributes.Add("class", "table_" + theme + "");

            string query = "";
            if (branch_id_pl.Text == "2")
            {
                query = dt.Rows[i]["Query"].ToString();
            }
            else
            {
                query = dt.Rows[i]["Branch_Query"].ToString();
                query = query.Replace("####$$$$", branch_id_pl.Text);
            }

            string ConString = "";
            if (dt.Rows[i]["DB_CON"].ToString() == "M")
            {
                ConString = "Data Source=(DESCRIPTION =" +
            "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.175)(PORT = 1521))" +
            "(CONNECT_DATA =" +
              "(SERVER = DEDICATED)" +
              "(SERVICE_NAME = FCMIS)));" +
              "User Id=fcrhost_read;Password=fcmis_read$2023;Connection Timeout=600; Max Pool Size=150;";
            }
            else if (dt.Rows[i]["DB_CON"].ToString() == "E")
            {
                ConString = "Data Source=(DESCRIPTION =" +
           "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.175)(PORT = 1521))" +
           "(CONNECT_DATA =" +
             "(SERVER = DEDICATED)" +
             "(SERVICE_NAME = MISDB)));" +
             "User Id=misread;Password=m1sR3ad$2023;Connection Timeout=600; Max Pool Size=150;";
                /* "(SERVICE_NAME = MISDB)));" +*/
            }
            else if (dt.Rows[i]["DB_CON"].ToString() == "P")
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
            //Theme,Style_width
            DataTable dm = get_oracle_data(query, ConString);
            string[] Column_Name = dt.Rows[i]["New_column_name"].ToString().Split(',');
            string[] style = dt.Rows[i]["Style_width"].ToString().Split(',');




            string output = "";
            output = output + "<tr>";

            table1.InnerHtml = output;
            header_til.Controls.Add(table1);
            output = "";
            for (int q = 0; q < Column_Name.Length; q++)
            {
                output = output + "<td class='td_" + theme + "_th' style='width:" + style[q] + "%;'>" + Column_Name[q] + "</td>";
            }
            output = output + "</tr>";
            for (int j = 0; j < dm.Rows.Count; j++)
            {
                output = output + "<tr>";
                for (int q = 0; q < style.Length; q++)
                {
                    output = output + "<td class='td_" + theme + "_td' style='width:" + style[q] + "%;'>" + dm.Rows[j][q].ToString() + "</td>";
                }
                output = output + "</tr>";
            }
            table.InnerHtml = output;
            body_div.Controls.Add(table);
            secdiv.Controls.Add(header_til);
            secdiv.Controls.Add(body_div);

        }
        private int create_div1(DataTable dt, ref int i, System.Web.UI.HtmlControls.HtmlGenericControl newdivs)
        {
            int ret = 0;
            string cls = "section1";          
            System.Web.UI.HtmlControls.HtmlGenericControl secdiv = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
            secdiv.Attributes.Add("class", cls);
            newdivs.Controls.Add(secdiv);
            System.Web.UI.HtmlControls.HtmlGenericControl rep_til = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
            System.Web.UI.HtmlControls.HtmlGenericControl header_til = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
            System.Web.UI.HtmlControls.HtmlGenericControl body_div = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
            string theme = "";
            if (dt.Rows[i]["Theme"].ToString() == "P")
            {
                theme = "plain";
            }
            else if (dt.Rows[i]["Theme"].ToString() == "B")
            {
                theme = "blue";
            }
            else if (dt.Rows[i]["Theme"].ToString() == "I")
            {
                theme = "ice";
            }
            else if (dt.Rows[i]["Theme"].ToString() == "H")
            {
                theme = "hot";
            }
            rep_til.Attributes.Add("class", "title_of_rep_"+ theme);
            rep_til.InnerText = dt.Rows[i]["Report_Name"].ToString();
            header_til.Attributes.Add("class","header_div_tbl_til");
            body_div.Attributes.Add("class", "header_div_tbl_til_body");

            Button bt1 = new Button();
            bt1.Text = "Download";
            bt1.CssClass = "dv_btn";
            bt1.Attributes.Add("runat", "server");
            bt1.Attributes["type"] = "button";
            bt1.ID = "Download_" + dt.Rows[i]["ID"].ToString();
            bt1.Click += new EventHandler(download);



            if (dt.Rows[i]["Note"].ToString() != "")
            {
                System.Web.UI.HtmlControls.HtmlGenericControl b2 = new System.Web.UI.HtmlControls.HtmlGenericControl("button");
                b2.ID = "Redirect" + dt.Rows[i]["ID"].ToString();
                b2.InnerText = "Main Report";
                b2.Attributes.Add("class", "dv_btn0");
                b2.Attributes.Add("onclick", "replace('" + dt.Rows[i]["Note"].ToString() + "');");
                //secdiv.Controls.Add(b2);
                header_til.Controls.Add(b2);
            }
            //secdiv.Controls.Add(bt1);
            header_til.Controls.Add(bt1);
            header_til.Controls.Add(rep_til);
            //secdiv.Controls.Add(rep_til);
            all_report.Controls.Add(newdivs);
            System.Web.UI.HtmlControls.HtmlGenericControl table1 = new System.Web.UI.HtmlControls.HtmlGenericControl("table");
            System.Web.UI.HtmlControls.HtmlGenericControl table = new System.Web.UI.HtmlControls.HtmlGenericControl("table");
            table1.Attributes.Add("class", "table_XX");
            table.Attributes.Add("class", "table_" + theme + "");
            string query = "";
            if (branch_id_pl.Text == "2")
            {
                query = dt.Rows[i]["Query"].ToString();
            }
            else
            {
                query = dt.Rows[i]["Branch_Query"].ToString();
                query = query.Replace("####$$$$", branch_id_pl.Text);
            }

            string ConString = "";
            if (dt.Rows[i]["DB_CON"].ToString() == "M")
            {
                ConString = "Data Source=(DESCRIPTION =" +
            "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.175)(PORT = 1521))" +
            "(CONNECT_DATA =" +
              "(SERVER = DEDICATED)" +
              "(SERVICE_NAME = FCMIS)));" +
              "User Id=fcrhost_read;Password=fcmis_read$2023;Connection Timeout=600; Max Pool Size=150;";
            }
            else if (dt.Rows[i]["DB_CON"].ToString() == "E")
            {
                ConString = "Data Source=(DESCRIPTION =" +
           "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.175)(PORT = 1521))" +
           "(CONNECT_DATA =" +
             "(SERVER = DEDICATED)" +
             "(SERVICE_NAME = MISDB)));" +
             "User Id=misread;Password=m1sR3ad$2023;Connection Timeout=600; Max Pool Size=150;";
                /* "(SERVICE_NAME = MISDB)));" +*/
            }
            else if (dt.Rows[i]["DB_CON"].ToString() == "P")
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
            //Theme,Style_width
            DataTable dm = get_oracle_data(query, ConString);
            string[] Column_Name = dt.Rows[i]["New_column_name"].ToString().Split(',');
            string[] style = dt.Rows[i]["Style_width"].ToString().Split(',');
           



            string output = "";
            output = output + "<tr>";
            for (int q = 0; q < Column_Name.Length; q++)
            {
                output = output + "<td class='td_" + theme + "_th' style='width:" + style[q] + "%;'>" + Column_Name[q] + "</td>";
            }
            output = output + "</tr>";

            table1.InnerHtml = output;
            header_til.Controls.Add(table1);
            output = "";
            for (int j = 0; j < dm.Rows.Count; j++)
            {
                output = output + "<tr>";
                for (int q = 0; q < style.Length; q++)
                {
                    output = output + "<td class='td_" + theme + "_td' style='width:" + style[q] + "%;'>" + dm.Rows[j][q].ToString() + "</td>";
                }
                output = output + "</tr>";
            }
            table.InnerHtml = output;
            body_div.Controls.Add(table);
            secdiv.Controls.Add(header_til);
            secdiv.Controls.Add(body_div);
            if ((i+1) < dt.Rows.Count)
            {
                int k = i + 1;
                if (dt.Rows[k]["Full_width"].ToString() == "N" && dt.Rows[k]["Module_Type"].ToString()!="G")
                {                  
                    System.Web.UI.HtmlControls.HtmlGenericControl secdiv2 = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                    secdiv2.Attributes.Add("class", cls);
                    newdivs.Controls.Add(secdiv2);
                    System.Web.UI.HtmlControls.HtmlGenericControl rep_til2 = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                    System.Web.UI.HtmlControls.HtmlGenericControl header_til2 = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                    System.Web.UI.HtmlControls.HtmlGenericControl body_div2 = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                    theme = "";
                    if (dt.Rows[k]["Theme"].ToString() == "P")
                    {
                        theme = "plain";
                    }
                    else if (dt.Rows[k]["Theme"].ToString() == "B")
                    {
                        theme = "blue";
                    }
                    else if (dt.Rows[k]["Theme"].ToString() == "I")
                    {
                        theme = "ice";
                    }
                    else if (dt.Rows[k]["Theme"].ToString() == "H")
                    {
                        theme = "hot";
                    }

                   
                   
                    rep_til2.Attributes.Add("class", "title_of_rep_" + theme);
                    rep_til2.InnerText = dt.Rows[k]["Report_Name"].ToString();
                    header_til2.Attributes.Add("class", "header_div_tbl_til");
                    body_div2.Attributes.Add("class", "header_div_tbl_til_body");
                    Button bt11 = new Button();
                    bt11.Text = "Download";
                    bt11.CssClass = "dv_btn";
                    bt11.Attributes.Add("runat", "server");
                    bt11.Attributes["type"] = "button";
                    bt11.ID = "Download_" + dt.Rows[k]["ID"].ToString();
                    bt11.Click += new EventHandler(download);
                    if (dt.Rows[k]["Note"].ToString() != "")
                    {
                        System.Web.UI.HtmlControls.HtmlGenericControl b22 = new System.Web.UI.HtmlControls.HtmlGenericControl("button");
                        b22.ID = "Redirect" + dt.Rows[k]["ID"].ToString();
                        b22.InnerText = "Main Report";
                        b22.Attributes.Add("class", "dv_btn0");
                        b22.Attributes.Add("onclick", "replace('" + dt.Rows[k]["Note"].ToString() + "');");
                        header_til2.Controls.Add(b22);
                    }
                    //secdiv2.Controls.Add(bt11);
                    //secdiv2.Controls.Add(rep_til2);
                    header_til2.Controls.Add(bt11);
                    header_til2.Controls.Add(rep_til2);
                    all_report.Controls.Add(newdivs);
                    System.Web.UI.HtmlControls.HtmlGenericControl table2 = new System.Web.UI.HtmlControls.HtmlGenericControl("table");
                    System.Web.UI.HtmlControls.HtmlGenericControl table3 = new System.Web.UI.HtmlControls.HtmlGenericControl("table");
                    table2.Attributes.Add("class", "table_" + theme + "");
                    table3.Attributes.Add("class", "table_XX");
                    query = "";
                    if (branch_id_pl.Text == "2")
                    {
                        query = dt.Rows[k]["Query"].ToString();
                    }
                    else
                    {
                        query = dt.Rows[k]["Branch_Query"].ToString();
                        query = query.Replace("####$$$$", branch_id_pl.Text);
                    }

                    ConString = "";
                    if (dt.Rows[k]["DB_CON"].ToString() == "M")
                    {
                        ConString = "Data Source=(DESCRIPTION =" +
                    "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.175)(PORT = 1521))" +
                    "(CONNECT_DATA =" +
                      "(SERVER = DEDICATED)" +
                      "(SERVICE_NAME = FCMIS)));" +
                      "User Id=fcrhost_read;Password=fcmis_read$2023;Connection Timeout=600; Max Pool Size=150;";
                    }
                    else if (dt.Rows[k]["DB_CON"].ToString() == "E")
                    {
                        ConString = "Data Source=(DESCRIPTION =" +
                   "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.175)(PORT = 1521))" +
                   "(CONNECT_DATA =" +
                     "(SERVER = DEDICATED)" +
                     "(SERVICE_NAME = MISDB)));" +
                     "User Id=misread;Password=m1sR3ad$2023;Connection Timeout=600; Max Pool Size=150;";
                        /* "(SERVICE_NAME = MISDB)));" +*/
                    }
                    else if (dt.Rows[k]["DB_CON"].ToString() == "P")
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
                    //Theme,Style_width
                    DataTable dmi = get_oracle_data(query, ConString);
                    string[] Column_Name1 = dt.Rows[k]["New_column_name"].ToString().Split(',');
                    string[] style1 = dt.Rows[k]["Style_width"].ToString().Split(',');

                    


                    output = "";
                    output = output + "<tr>";
                    for (int q = 0; q < Column_Name1.Length; q++)
                    {
                        output = output + "<td class='td_" + theme + "_th' style='width:" + style1[q] + "%;'>" + Column_Name1[q] + "</td>";
                    }
                    output = output + "</tr>";
                    table3.InnerHtml = output;
                    header_til2.Controls.Add(table3);
                    output = "";
                    for (int j = 0; j < dmi.Rows.Count; j++)
                    {
                        output = output + "<tr>";
                        for (int q = 0; q < style1.Length; q++)
                        {
                            output = output + "<td class='td_" + theme + "_td' style='width:" + style1[q] + "%;'>" + dmi.Rows[j][q].ToString() + "</td>";
                        }
                        output = output + "</tr>";
                    }
                    //table2.InnerHtml = output;
                    //secdiv2.Controls.Add(table2);

                    table2.InnerHtml = output;
                    body_div2.Controls.Add(table2);
                    secdiv2.Controls.Add(header_til2);
                    secdiv2.Controls.Add(body_div2);
                    i = i + 1;
                }
                else
                {
                    ret = 1;
                }
            }
            return ret;
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
        protected string get_info(string query)
        {
            string acc_no = "";
            SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString); ;

            try
            {
                cnn.Open();
                string sqlcommand = query;
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
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('Unable to fetch Customers details due to Network issue:');", true);
            }
            finally
            {
                cnn.Close();
            }
            return acc_no;
        }
        protected void load_graph()
        {
            //string data = "A1:100:200:150,C1:100:300:90,B1:456:45:650";
            //ScriptManager.RegisterStartupScript(this, GetType(), "bulb88", "callfive('Column,MNT1,MNT2,MNT3','piechart','" + data + "','Active Accounts Count');", true);
            //data = "A11:190,C11:190,B11:956";
            //ScriptManager.RegisterStartupScript(this, GetType(), "bulb3", "callone('piechart2','" + data + "','New Account Opened Last Month');", true);
        }


    }
}