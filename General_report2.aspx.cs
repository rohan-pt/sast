using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;
using System.IO;

namespace BCCBExamPortal
{
    public partial class General_report2 : System.Web.UI.Page
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
                cmd1.Parameters.AddWithValue("@Page_Name", "General_report2");
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

        protected void download_cert2(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            string output = "                                   ****************Ready Reports*********";
            output = output + Environment.NewLine + Environment.NewLine + "                                   BASSEIN CATHOLIC CO-OP BANK LTD. (SCHEDULED BANK)";

            string query = "select Dept_Name,Report_Name,cast(Report_Build_date as date) as Report_Build_date,Query from General_reports order by Dept_Name";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            string new_dept_name = "";
            DataTable dm = get_normal_data(query, con);
            for (int i = 0; i < dm.Rows.Count; i++)
            {
                string dept_name = dm.Rows[i]["Dept_Name"].ToString();
                if (new_dept_name != dept_name)
                {
                    output = output + Environment.NewLine + Environment.NewLine + dept_name;
                    output = output + Environment.NewLine + "--------------------------------";
                    output = output + Environment.NewLine;
                    new_dept_name = dept_name;
                }
                output = output + Environment.NewLine + "SR No : " + (i + 1) + " " + dm.Rows[i]["Report_Name"].ToString() + "     ===> Report Build Date - " + Convert.ToDateTime(dm.Rows[i]["Report_Build_date"].ToString()).ToShortDateString() + Environment.NewLine;
                output = output + Environment.NewLine + Environment.NewLine + dm.Rows[i]["Query"].ToString()+ Environment.NewLine;

            }
            output = output + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + "Signature of  Data Engineer                                                                                                     Signature of CIO.";

            sb.Append(output);
            sb.Append("\r\n");

            string text = sb.ToString();

            Response.Clear();
            Response.ClearHeaders();

            Response.AppendHeader("Content-Length", text.Length.ToString());
            Response.ContentType = "text/plain";
            Response.AppendHeader("Content-Disposition", "attachment;filename=\"Report Summary.txt\"");

            Response.Write(text);
            Response.End();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage5", " show_search();", true);

        }

        protected void check_notifybutton()
        {
            string query = "select * from reporting_notification where notification_name='" + ddlreport.SelectedItem.Text + "' and usercode='" + sessionlbl.Text + "' and IS_REPORT='Y' and n_stat='1'";
            string ConString = "Data Source=(DESCRIPTION =" +
              "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.197)(PORT = 1522))" +
              "(CONNECT_DATA =" +
                "(SERVER = DEDICATED)" +
                "(SERVICE_NAME = FCPROD)));" +
                "User Id=APPLOGS;Password=A#9pLO7g$2o23;Connection Timeout=600; Max Pool Size=150;";
            DataTable dt = get_oracle_data(query, ConString);
            if(dt.Rows.Count>0)
            {
                notify.InnerText = "Stop Notification";
                notify.Attributes.Add("class", "notify_btn_R");
            }
            else
            {
                notify.InnerText = "Create Notification";
                notify.Attributes.Add("class", "notify_btn");
               // notify.Attributes.Remove("class", "notify_btn");
            }
        }

        protected void notify_report(object sender, EventArgs e)
        {
            try
            {
                string p = from_date.Text.ToString();
                p = "N";

                Random rn = new Random();
                string s = rn.Next(0, 1000000).ToString("000000");

                String str = "abcdefghijklmnopqrstuvwxyz";
                int size = 3;

                // Initializing the empty string 
                String ran = "";

                for (int i = 0; i < size; i++)
                {
                    int x = rn.Next(26);
                    ran = ran + str[x];
                }

                ran = ran + "_" + s.ToString();

                string url = HttpContext.Current.Request.Url.AbsoluteUri + "?D=" + ddl_dep.SelectedItem.Text + "&R=" + ddlreport.SelectedItem.Text;
                string s1 = "Notification has been created for this report. You can Edit the Notification properties in the Settings section.";
                string query = "insert into reporting_notification (usercode,n_date_create,notification_name,notification_start_date,notification_link,notification_frequency,is_report,n_edit_date,is_email,is_sms,n_stat,ran_id) " +
    "values('" + sessionlbl.Text + "', SYSDATE, '" + ddlreport.SelectedItem.Text + "',sysdate, '" + url + "', 'Daily', 'Y', sysdate, '" + p + "', 'N', '1','" + ran + "')";
                if (notify.InnerText != "Create Notification")
                {
                    query = "update reporting_notification set n_stat='0'  where notification_name='" + ddlreport.SelectedItem.Text + "' and usercode='"+ sessionlbl.Text + "'";
                    s1 = "Notification has been stopped.";
                }
                string ConString = "Data Source=(DESCRIPTION =" +
                  "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.197)(PORT = 1522))" +
                  "(CONNECT_DATA =" +
                    "(SERVER = DEDICATED)" +
                    "(SERVICE_NAME = FCPROD)));" +
                    "User Id=APPLOGS;Password=A#9pLO7g$2o23;Connection Timeout=600; Max Pool Size=150;";

                OracleConnection connection = new OracleConnection(ConString);
                OracleCommand command = new OracleCommand(query, connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
                command.Connection.Close();
                check_notifybutton();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('"+ s1 + "');", true);
            }
            catch (Exception er)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('Error in the operation!!!');", true);
            }

        
    }


        protected void download_cert(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            string output = "                                   ****************Ready Reports*********";
            output = output + Environment.NewLine + Environment.NewLine + "                                   BASSEIN CATHOLIC CO-OP BANK LTD. (SCHEDULED BANK)";

            string query = "select Dept_Name,Report_Name,cast(Report_Build_date as date) as Report_Build_date from General_reports order by Dept_Name";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            string new_dept_name="";
            DataTable dm = get_normal_data(query, con);
            for (int i = 0; i < dm.Rows.Count; i++)
            {
                string dept_name = dm.Rows[i]["Dept_Name"].ToString();
                if (new_dept_name != dept_name)
                {
                    output = output + Environment.NewLine + Environment.NewLine + dept_name;
                    output = output + Environment.NewLine + "--------------------------------";
                    output = output + Environment.NewLine;
                    new_dept_name = dept_name;
                }
                output = output + Environment.NewLine + dm.Rows[i]["Report_Name"].ToString() + "     ===> Report Build Date - " + Convert.ToDateTime(dm.Rows[i]["Report_Build_date"].ToString()).ToShortDateString();
            }
            output = output + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + "Signature of  Data Engineer                                                                                                     Signature of CIO.";

            sb.Append(output);
            sb.Append("\r\n");

            string text = sb.ToString();

            Response.Clear();
            Response.ClearHeaders();

            Response.AppendHeader("Content-Length", text.Length.ToString());
            Response.ContentType = "text/plain";
            Response.AppendHeader("Content-Disposition", "attachment;filename=\"Report Summary.txt\"");

            Response.Write(text);
            Response.End();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage5", " show_search();", true);
           
        }


        protected void search_report(object sender, EventArgs e)
        {         

            string query = "select distinct Dept_Name,Report_Name,cast(Report_Build_date as date) as Report_Build_date from General_reports where (upper(Report_Name) like '%"+ txt_search.Value.Trim().ToUpper() + "%' or upper(Dept_Name) like '%" + txt_search.Value.Trim().ToUpper() + "%') and Report_Status!='3'  order by Dept_Name";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            string new_dept_name = "";
            DataTable dm = get_normal_data(query, con);
            string output = "";
            for (int i = 0; i < dm.Rows.Count; i++)
            {
                string dept_name = dm.Rows[i]["Dept_Name"].ToString();
                if (new_dept_name != dept_name)
                {
                    output = output + "<div class='search_and_dep'>" + dm.Rows[i]["Dept_Name"].ToString() + "</div>";                   
                    new_dept_name = dept_name;
                }

                output = output+ "<div class='search_and_rep'> <a class='a_ref_class' href='General_report2.aspx?D=" + dm.Rows[i]["Dept_Name"].ToString() + "&R=" + dm.Rows[i]["Report_Name"].ToString() + "'>" + dm.Rows[i]["Report_Name"].ToString() + "</a></div>";

            }
            report_display.InnerHtml = output;
           // ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage5", " show_search();", true);

        }



        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect("LogIn.aspx");
            //try
            //{
            //    sessionlbl.Text = Session["id"].ToString();
            //    try
            //    {
            //        DepName.Text = Request.QueryString["D"];
            //        RepName.Text = Request.QueryString["R"];
            //        if(DepName.Text==null)
            //        {
            //            DepName.Text = "";
            //        }
            //        if(RepName.Text==null)
            //        {
            //            RepName.Text = "";
            //        }
            //    }
            //    catch (Exception ep)
            //    {
            //        DepName.Text = "";
            //        RepName.Text = "";
            //    }
            //}
            //catch (Exception ep)
            //{
            //    Response.Redirect("LogIn.aspx");
            //}

            //if (!IsPostBack)
            //{
            //    audit_trails();
            //    Load_drop_down();
            //    // Load_dept_report();
            //    Load_dept_name(sender, e);
            //    check_env_type();
            //}          
            //get_user();
        }

        protected void check_env_type()
        {
            con_type.Value = "";
            string ConString = "Data Source=(DESCRIPTION =" +
                "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.197)(PORT = 1522))" +
                "(CONNECT_DATA =" +
                  "(SERVER = DEDICATED)" +
                  "(SERVICE_NAME = FCPROD)));" +
                  "User Id=APPLOGS;Password=A#9pLO7g$2o23;Connection Timeout=600; Max Pool Size=150;";
            string query = " select a.ENVIRNMENT,b.CON_NAME,a.CREATE_USER,a.CREATE_DATE,a.ENVI_STAT,a.REASON,b.CON_STRING,b.CON_TYPE,b.SPLIT_STR from envirnment_setup a " +
" left join database_connection b on a.CON_NAME=b.ID " +
" where a.ENVIRNMENT='General Report Section' and a.ENVI_STAT='1' and b.CON_TYPE='Oracle Connection'";
            DataTable dt = get_oracle_data(query, ConString);
            if(dt.Rows.Count>0)
            {
                con_name_d.InnerText = dt.Rows[0]["CON_NAME"].ToString();
                act_reason.InnerText = dt.Rows[0]["REASON"].ToString();
                con_type.Value= dt.Rows[0]["CON_STRING"].ToString();
                db_type.InnerText = dt.Rows[0]["CON_TYPE"].ToString();
                schema_hid.Value= dt.Rows[0]["SPLIT_STR"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "showmaster", "show_master();", true);
            }
            else
            {
                con_name_d.InnerText = "";
                act_reason.InnerText = "";
                con_type.Value = "";
                db_type.InnerText = "";
                schema_hid.Value = "";
                ScriptManager.RegisterStartupScript(this, GetType(), "hidemaster", "hide_master();", true);
            }

        }

        protected void get_user()
        {
            string query = "select * from Employee_LoginTbl where Code='"+ sessionlbl.Text + "'";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            DataTable dt = get_normal_data(query, con);
            if(dt.Rows.Count>0)
            {
                master_user.InnerText=dt.Rows[0]["Employee_Name"].ToString();
                
            }
            else
            {
                master_user.InnerText = "";
            }

            query = "select COUNT(*) as Reports from General_reports";          
            DataTable dm = get_normal_data(query, con);
            if (dm.Rows.Count > 0)
            {
                report_cnt.InnerText = dm.Rows[0]["Reports"].ToString();
            }
            else
            {
                report_cnt.InnerText = "NW ISSUE";
            }

        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Generate_report(sender,e);
            Grid_view.PageIndex = e.NewPageIndex;
            Grid_view.DataBind();
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }
        protected void cmbActivity_SelectedIndexChanged(object sender, EventArgs e)
        {
            DepName.Text = "";
            RepName.Text = "";
            Load_dept_report(sender,e);
            Grid_view.DataSource = null;
            Grid_view.DataBind();
            export_to_excel.Visible = false;
            con_lbl.InnerText = "";
            lbl_rep_note.InnerText = "";
            lbl_rep_size.InnerText = "";
            author_name.InnerText = "";
            creation_date.InnerText = "";
            lbl_note.InnerText = "";
            button_panel.Visible = false;
            con_lbl.Attributes.Add("style", "background:#f5f6f6;");
            frm_dat_div.Visible = false;
            to_dat_div.Visible = false;
            txt_inp_div.Visible = false;
            txt_inp_div2.Visible = false;
            notify.Visible = false;
        }
        protected void conreport_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid_view.DataSource = null;
            Grid_view.DataBind();
            export_to_excel.Visible = false;
            //string click_str = ddlreport.Items[Convert.ToInt32(Request.Form["ddlreport"].ToString())].ToString();
           // string dept_name = ddl_dep.Items[Convert.ToInt32(Request.Form["ddl_dep"].ToString())].ToString();
            string dept_name = ddl_dep.SelectedItem.Text;
            string click_str = ddlreport.SelectedItem.Text;
            if (click_str != "Please Select Report")
            {
                //string query = "select * from General_reports where Report_Name='" + click_str + "' and Report_Status!='3' and Dept_Name='"+ dept_name + "'";
                string query = "select * from General_reports a left join Admin_LoginTbl b on a.USER1=b.AD_Code where a.Report_Name='" + click_str + "' and a.Report_Status!='3' and a.Dept_Name='" + dept_name + "'";

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
                DataTable dt = get_normal_data(query, con);
                if (dt.Rows[0]["Big_Small"].ToString() == "1")
                {
                    lbl_rep_size.InnerText = "Small  (It should take less than 2 min to get Output)";
                }
                else if (dt.Rows[0]["Big_Small"].ToString() == "0")
                {
                    lbl_rep_size.InnerText = "Big  (It may take more than 2 min to get Output)";
                }
                else
                {
                    lbl_rep_size.InnerText = "Large  (It may take more than 15 min to get full Output)";
                }

                if (dt.Rows[0]["Is_Branch_Allowed"].ToString() == "0")
                {
                    dllocation.Enabled = false;
                }
                else
                {
                    dllocation.Enabled = true;
                }

                query = dt.Rows[0]["Query"].ToString();
                if (query.Contains("#$#$#$#$") == true)
                {
                    frm_dat_div.Visible = true;
                }
                else
                {
                    frm_dat_div.Visible = false;
                }
                if (query.Contains("$#$#$#$#") == true)
                {
                    to_dat_div.Visible = true;
                }
                else
                {
                    to_dat_div.Visible = false;
                }
                if(query.Contains("@@@@$$$$")==true)
                {
                    txt_inp_div.Visible = true;
                }
                else
                {
                    txt_inp_div.Visible = false;
                }
                if (query.Contains("$$$$@@@@") == true)
                {
                    txt_inp_div2.Visible = true;
                }
                else
                {
                    txt_inp_div2.Visible = false;
                }
                lbl_rep_note.InnerText = dt.Rows[0]["Note"].ToString();
                if (con_type.Value == "")
                {
                    if (dt.Rows[0]["MIS_PROD"].ToString() == "1")
                    {
                        con_lbl.InnerText = "MIS (T-1)";
                    }
                    else if (dt.Rows[0]["MIS_PROD"].ToString() == "2")
                    {
                        con_lbl.InnerText = "EOM";
                    }
                    else
                    {
                        con_lbl.InnerText = "Production";                       
                    }
                }
                else
                {
                    con_lbl.InnerText = con_name_d.InnerText;
                    lbl_rep_note.InnerText = act_reason.InnerText;
                }
                author_name.InnerText= dt.Rows[0]["Extra2"].ToString();
                creation_date.InnerText = dt.Rows[0]["Report_Build_date"].ToString();
                lbl_note.InnerText = "Branch should not contact directly to the author. For any change in the report, they should contact to the concerned department.";
                check_connection();
                button_panel.Visible = true;
                notify.Visible = true;
                check_notifybutton();
            }
            else
            {
                con_lbl.InnerText = "";
                lbl_rep_note.InnerText = "";
                lbl_rep_size.InnerText = "";
                author_name.InnerText = "";
                creation_date.InnerText = "";
                lbl_note.InnerText = "";
                button_panel.Visible = false;
                con_lbl.Attributes.Add("style", "background:#f5f6f6;");
                frm_dat_div.Visible = false;
                to_dat_div.Visible = false;
                txt_inp_div.Visible = false;
                txt_inp_div2.Visible = false;
                notify.Visible = false;
            }
        }

        protected void check_connection()
        {
            string ConString = "";
            if (con_type.Value == "")
            {
                if (con_lbl.InnerText == "EOM")
                {
                    ConString = "Data Source=(DESCRIPTION =" +
                   "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.175)(PORT = 1521))" +
                   "(CONNECT_DATA =" +
                     "(SERVER = DEDICATED)" +
                     "(SERVICE_NAME = MISDB)));" +
                     "User Id=misread;Password=m1sR3ad$2023;Connection Timeout=600; Max Pool Size=150;";
                }
                else if (con_lbl.InnerText == "MIS (T-1)")
                {
                    ConString = "Data Source=(DESCRIPTION =" +
                    "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.175)(PORT = 1521))" +
                    "(CONNECT_DATA =" +
                      "(SERVER = DEDICATED)" +
                      "(SERVICE_NAME = FCMIS)));" +
                      "User Id=fcrhost_read;Password=fcmis_read$2023;Connection Timeout=600; Max Pool Size=150;";
                }
                else if (con_lbl.InnerText == "Production")
                {
                    ConString = "Data Source=(DESCRIPTION =" +
                     "(ADDRESS = (PROTOCOL = TCP)(HOST = fcobdxdb-scan)(PORT = 1522))" +
                     "(CONNECT_DATA =" +
                       "(SERVER = DEDICATED)" +
                       "(SERVICE_NAME = FCPROD)));" +
                       "User Id=pfms_read;Password=pFm$_Read$2023;Connection Timeout=600; Max Pool Size=150;";
                }
            }
            else
            {
                ConString = con_type.Value;
            }
            OracleConnection con = new OracleConnection(ConString);
            try
            {

                con.Open();
                con_lbl.Attributes.Add("style", "background:#92f763;");
                con.Close();

            }
            catch (Exception yy)
            {
                con_lbl.Attributes.Add("style", "background:#f7983c;");
            }
            finally
            {
                con.Close();
            }
          
        }

        protected void heavy_export(DataTable table)
        {
            //DataTable table = new DataTable();
            //table.Columns.AddRange(new[] { new DataColumn("Key"), new DataColumn("Value") });
            //foreach (string name in Request.ServerVariables)
            //    table.Rows.Add(name, Request.ServerVariables[name]);

            // This actually makes your HTML output to be downloaded as .xls file

           // string click_str = ddlreport.Items[Convert.ToInt32(Request.Form["ddlreport"].ToString())].ToString();
           // string dep = ddl_dep.Items[Convert.ToInt32(Request.Form["ddl_dep"].ToString())].ToString();
            string dep = ddl_dep.SelectedItem.Text;
            string click_str = ddlreport.SelectedItem.Text;
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

        protected void export_excel(object sender, EventArgs e)
        {
            //Code 1

            //  Response.Clear();

            //Response.AddHeader("content-disposition", "attachment;filename = FileName.xls");



            //Response.ContentType = "application/vnd.xls";

            //System.IO.StringWriter stringWrite = new System.IO.StringWriter();

            //System.Web.UI.HtmlTextWriter htmlWrite =
            //new HtmlTextWriter(stringWrite);

            //Grid_view.RenderControl(htmlWrite);

            //Response.Write(stringWrite.ToString());

            //Response.End();

            //string click_str = ddlreport.Items[Convert.ToInt32(Request.Form["ddlreport"].ToString())].ToString();
            //string dep = ddl_dep.Items[Convert.ToInt32(Request.Form["ddl_dep"].ToString())].ToString();
            string dep = ddl_dep.SelectedItem.Text;
            string click_str = ddlreport.SelectedItem.Text;
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = "" + dep + "_" + click_str + "_" + DateTime.Now + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            //Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            string style = @"<style> td { mso-number-format:\@;} </style>";
            Response.Write(style);
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            Grid_view.GridLines = GridLines.Both;
            Grid_view.HeaderStyle.Font.Bold = true;
            Grid_view.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();
        }

        protected void Generate_report(object sender, EventArgs e)
        {
           // string click_str = ddlreport.Items[Convert.ToInt32(Request.Form["ddlreport"].ToString())].ToString();
            report_gen_btn.Disabled = true;
            export_to_excel.Visible = true;
          //  string dept_name = ddl_dep.Items[Convert.ToInt32(Request.Form["ddl_dep"].ToString())].ToString();
            string dept_name = ddl_dep.SelectedItem.Text;
            string click_str = ddlreport.SelectedItem.Text;
            if (click_str != "Please Select Report")
            {
                string query = "select * from General_reports where Report_Name='" + click_str + "' and Report_Status!='3' and Dept_Name='" + dept_name + "'";
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
                DataTable dt = get_normal_data(query, con);

                string branch_ddl = "";
                if (dllocation.Enabled == true)
                {
                    branch_ddl = dllocation.Items[Convert.ToInt32(Request.Form["dllocation"].ToString())].ToString();
                }
                else
                {
                    branch_ddl = "All Branches";
                }
                int cl = 0;

                if (branch_ddl != "All Branches")
                {
                    query = dt.Rows[0]["Branch_Query"].ToString();
                    string branch = get_info("select Loc_Id from Locationtbl where Location='" + branch_ddl + "'");
                    query = query.Replace("####$$$$", branch);

                    if (frm_dat_div.Visible == true)
                    {
                        if (from_date.Text != "")
                        {
                            branch = from_date.Text;
                            query = query.Replace("#$#$#$#$", branch);
                        }
                        else
                        {
                            cl = 1;
                        }                       
                    }
                    if (to_dat_div.Visible == true)
                    {
                        if (to_date.Text != "")
                        {
                            branch = to_date.Text;
                            query = query.Replace("$#$#$#$#", branch);
                        }
                        else
                        {
                            cl = 1;
                        }
                    }
                    if (txt_inp_div.Visible == true)
                    {
                        if (txt_input_param.Text != "")
                        {
                            branch = txt_input_param.Text;
                            query = query.Replace("@@@@$$$$", branch);
                        }
                        else
                        {
                            cl = 1;
                        }
                    }
                    if (txt_inp_div2.Visible == true)
                    {
                        if (txt_input_param2.Text != "")
                        {
                            branch = txt_input_param2.Text;
                            branch = "'" + branch.Replace(",", "','") + "'";
                            query = query.Replace("$$$$@@@@", branch);
                        }
                        else
                        {
                            cl = 1;
                        }
                    }

                }
                else
                {
                    query = dt.Rows[0]["Query"].ToString();
                    string branch = "";
                    if (frm_dat_div.Visible == true)
                    {
                        if (from_date.Text != "")
                        {
                            branch = from_date.Text;
                            query = query.Replace("#$#$#$#$", branch);
                        }
                        else
                        {
                            cl = 1;
                        }
                       
                    }
                    if(to_dat_div.Visible==true)
                    {
                        if (to_date.Text != "")
                        {
                            branch = to_date.Text;
                            query = query.Replace("$#$#$#$#", branch);
                        }
                        else
                        {
                            cl = 1;
                        }
                    }
                    if (txt_inp_div.Visible == true)
                    {
                        if (txt_input_param.Text != "")
                        {
                            branch = txt_input_param.Text;
                            query = query.Replace("@@@@$$$$", branch);
                        }
                        else
                        {
                            cl = 1;
                        }
                    }
                    if (txt_inp_div2.Visible == true)
                    {
                        if (txt_input_param2.Text != "")
                        {
                            branch = txt_input_param2.Text;
                            branch = "'" + branch.Replace(",", "','") + "'";
                            query = query.Replace("$$$$@@@@", branch);
                        }
                        else
                        {
                            cl = 1;
                        }
                    }
                }

                string ConString = "";
                if (con_type.Value == "")
                {
                    if (dt.Rows[0]["MIS_PROD"].ToString() == "1")
                    {
                        ConString = "Data Source=(DESCRIPTION =" +
                    "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.175)(PORT = 1521))" +
                    "(CONNECT_DATA =" +
                      "(SERVER = DEDICATED)" +
                      "(SERVICE_NAME = FCMIS)));" +
                      "User Id=fcrhost_read;Password=fcmis_read$2023;Connection Timeout=600; Max Pool Size=150;";
                    }
                    else if (dt.Rows[0]["MIS_PROD"].ToString() == "2")
                    {
                        ConString = "Data Source=(DESCRIPTION =" +
                   "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.175)(PORT = 1521))" +
                   "(CONNECT_DATA =" +
                     "(SERVER = DEDICATED)" +
                     "(SERVICE_NAME = MISDB)));" +
                     "User Id=misread;Password=m1sR3ad$2023;Connection Timeout=600; Max Pool Size=150;";
                        /* "(SERVICE_NAME = MISDB)));" +*/
                    }
                    else
                    {
                        ConString = "Data Source=(DESCRIPTION =" +
                     "(ADDRESS = (PROTOCOL = TCP)(HOST = fcobdxdb-scan)(PORT = 1522))" +
                     "(CONNECT_DATA =" +
                       "(SERVER = DEDICATED)" +
                       "(SERVICE_NAME = FCPROD)));" +
                       "User Id=pfms_read;Password=pFm$_Read$2023;Connection Timeout=600; Max Pool Size=150;";
                    }
                }
                else
                {
                    ConString = con_type.Value;
                }

               // query = query.ToLower();

                if (schema_hid.Value != "")
                {
                    string[] p = schema_hid.Value.ToLower().Split(',');
                    for (int x = 0; x < p.Length; x++)
                    {
                        //string cc = p[x];
                        string[] p1 = p[x].Split(':');
                        //cc = p1[0];
                        //cc = p1[1];
                        query = query.Replace(p1[0], p1[1]);
                    }
                }





                if (cl == 0)
                {
                    DataTable dm = get_oracle_data(query, ConString);
                    if (dm.Rows.Count > 0)
                    {
                        if (lbl_rep_size.InnerText != "Large  (It may take more than 15 min to get full Output)")
                        {
                            Grid_view.DataSource = dm;
                            Grid_view.DataBind();
                        }
                        else
                        {
                            heavy_export(dm);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('No record for this report!!!');", true);
                        export_to_excel.Visible = false;
                    }
                    //ExportGridToCSV(dm);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('Please give necessary inputs!!!');", true);
                }
                //lbl_rep_count
                //lbl_rep_count.InnerText = dm.Rows.Count.ToString();

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('Please select Report Name!!!');", true);
                export_to_excel.Visible = false;
            }
            report_gen_btn.Disabled = false;
        }
        private void ExportGridToCSV(DataTable dt)
        {
            string output = "";
           // string click_str = ddlreport.Items[Convert.ToInt32(Request.Form["ddlreport"].ToString())].ToString();
           // string dep = ddl_dep.Items[Convert.ToInt32(Request.Form["ddl_dep"].ToString())].ToString();
            string dep = ddl_dep.SelectedItem.Text;
            string click_str = ddlreport.SelectedItem.Text;
            for (int k = 0; k < dt.Columns.Count; k++)
            {
                output = output + dt.Columns[k].ColumnName + "|";
            }

            output = output + "\r\n";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int k = 0; k < dt.Columns.Count; k++)
                {
                    output = output + dt.Rows[i][k].ToString() + "|";
                }
                output = output + "\r\n";
            }
            //Response.Output.Write(columnbind.ToString());
            //Response.Flush();
            //Response.End();
            try
            {

                Response.Clear();
                // Add header by specifying file name  
                Response.AddHeader("Content-Disposition", "attachment; filename=" + dep + "_" + click_str + ".csv");
                // Add header for content length  
                Response.AddHeader("Content-Length", output.Length.ToString());
                // Specify content type  
                Response.ContentType = "text/plain";
                Response.Output.Write(output.ToString());
                // Clearing flush  
                Response.Flush();
                // Transimiting file  
                //Response.TransmitFile(out);
                Response.End();
            }
            catch (Exception SDS)
            {

            }
            //HttpResponseBase response = HttpContext.Response;
            //response.ContentType = "text/csv";
            //response.AppendHeader("Content-Disposition", "attachment;filename=Vithal_Wadje.csv");
            //response.Write(columnbind.ToString());
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
        protected void Load_dept_name(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            string query = "select ROW_NUMBER() OVER (ORDER BY Report_Name) AS serial_number, Report_Name from General_reports where Dept_Name='Credit Monitoring' and Report_Status!='3'";
            query = "select ROW_NUMBER() OVER (ORDER BY Reporting_Departing) AS serial_number,Reporting_Departing from Reporting_Dept where Curr_stat='1'";
            DataTable dt = get_normal_data(query, con);
            ddl_dep.Items.Clear();
            ddl_dep.DataSource = dt;
            ddl_dep.DataBind();
            ddl_dep.DataTextField = "Reporting_Departing";
            ddl_dep.DataValueField = "serial_number";
            ddl_dep.DataBind();
            ddl_dep.Items.Insert(0, new ListItem("--All Departments--", "0"));
            if(DepName.Text!="")
            {
                //ddl_dep.SelectedItem.Text = DepName.Text;
                for(int i=0;i<dt.Rows.Count;i++)
                {
                    if(dt.Rows[i]["Reporting_Departing"].ToString()== DepName.Text)
                    {
                        ddl_dep.SelectedValue = dt.Rows[i]["serial_number"].ToString();
                    }
                }
                Load_dept_report(sender, e);
            }
            
        }

        protected void btn_reset(object sender, EventArgs e)
        {
            Response.Redirect("General_report2.aspx");

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

        protected void Load_dept_report(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            string click_str = ddl_dep.SelectedItem.Text;
           // string click_str = ddl_dep.Items[Convert.ToInt32(Request.Form["ddl_dep"].ToString())].ToString();
            //if (click_str != "--All Departments--")
            //{
            string query = "select ROW_NUMBER() OVER (ORDER BY Report_Name) AS serial_number, Report_Name from General_reports where Dept_Name='" + click_str + "' and Report_Status='1'";
            DataTable dt = get_normal_data(query, con);
            ddlreport.Items.Clear();           
            ddlreport.DataSource = dt;
            ddlreport.DataBind();
            ddlreport.DataTextField = "Report_Name";
            ddlreport.DataValueField = "serial_number";
            ddlreport.DataBind();
            ddlreport.Items.Insert(0, new ListItem("Please Select Report", "0"));           
            export_to_excel.Visible = false;
            if (RepName.Text != "")
            {
                ddlreport.SelectedItem.Text = RepName.Text;
                conreport_SelectedIndexChanged(sender, e);
            }
            //}
            //else
            //{
            //    ddlreport.Items.Clear();
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
                string er = "Unable to fetch Customers details due to Network issue: " + el + " : : " + query;
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('Unable to fetch Customers details due to Network issue:');", true);
            }
            finally
            {
                con.Close();
            }

            return dt;
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
                // Label1.InnerText = yy.Message;
            }
            finally
            {
                con.Close();
            }
            return dt;


        }
    }
}