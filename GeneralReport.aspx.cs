using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BCCBExamPortal.Models;
using Oracle.ManagedDataAccess.Client;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.Text.RegularExpressions;

namespace BCCBExamPortal
{
    public partial class GeneralReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //sessionlbl.Text = "08560";
            try
            {
                sessionlbl.Text = Session["id"].ToString();
                try
                {
                    DepName.Text = Request.QueryString["D"];
                    RepName.Text = Request.QueryString["R"];
                    if (DepName.Text == null)
                    {
                        DepName.Text = "";
                    }
                    if (RepName.Text == null)
                    {
                        RepName.Text = "";
                    }
                }
                catch (Exception ep)
                {
                    DepName.Text = "";
                    RepName.Text = "";
                }
            }
            catch (Exception ep)
            {
                Response.Redirect("LogIn.aspx");
            }
            if (!IsPostBack)
            {
                audit_trails();
                check_env_type();
                Load_drop_down();
                Load_dept_name(sender, e);              
            }           
            get_user();
        }

        protected void check_notifybutton()
        {
            string query = "select * from reporting_notification where notification_name='" + ddlreport.SelectedItem.Text + "' and usercode='" + sessionlbl.Text + "' and IS_REPORT='Y' and n_stat='1'";
            //string ConString = "Data Source=(DESCRIPTION =" +
            //                               "(ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.29.36)(PORT = 1521))" +
            //                               "(CONNECT_DATA =" +
            //                               "(SERVER = DEDICATED)" +
            //                               "(SERVICE_NAME = FCPROD_DR.subnetdb1.vcndb.oraclevcn.com)));" +
            //                               "User Id=APPLOGS;Password=A#9pLO7g$2o23;";
            string ConString = "Data Source=(DESCRIPTION =" +
              "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.197)(PORT = 1522))" +
              "(CONNECT_DATA =" +
                "(SERVER = DEDICATED)" +
                "(SERVICE_NAME = FCPROD)));" +
                "User Id=APPLOGS;Password=A#9pLO7g$2o23;Connection Timeout=600; Max Pool Size=150;";
            DataTable dt = get_oracle_data(query, ConString);
            if (dt.Rows.Count > 0)
            {
                notify.InnerText = "Stop Notification";
                notify.Attributes.Add("class", "button-7");
            }
            else
            {
                notify.InnerText = "Create Notification";
                notify.Attributes.Add("class", "button-6");
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
                    query = "update reporting_notification set n_stat='0'  where notification_name='" + ddlreport.SelectedItem.Text + "' and usercode='" + sessionlbl.Text + "'";
                    s1 = "Notification has been stopped.";
                }
                //string ConString = "Data Source=(DESCRIPTION =" +
                //                         "(ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.29.36)(PORT = 1521))" +
                //                         "(CONNECT_DATA =" +
                //                         "(SERVER = DEDICATED)" +
                //                         "(SERVICE_NAME = FCPROD_DR.subnetdb1.vcndb.oraclevcn.com)));" +
                //                         "User Id=APPLOGS;Password=A#9pLO7g$2o23;";
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
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('" + s1 + "');", true);
            }
            catch (Exception er)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('Error in the operation!!!');", true);
            }


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
                cmd1.Parameters.AddWithValue("@Page_Name", "General_report2");
                cmd1.Parameters.AddWithValue("@Usercode", sessionlbl.Text);
                cmd1.ExecuteNonQuery();
                cmd1.Dispose();

            }
            catch (Exception ep)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('" + ep.Message + "');", true);
            }
            finally
            {
                con.Close();
            }
        }
        protected void get_user()
        {
            string query = "select * from Employee_LoginTbl where Code='" + sessionlbl.Text + "'";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            DataTable dt = get_normal_data(query, con);
            if (dt.Rows.Count > 0)
            {
                master_user.InnerText = dt.Rows[0]["Employee_Name"].ToString();
                location_id.InnerText = dt.Rows[0]["Location"].ToString();
            }
            else
            {
                master_user.InnerText = "NW Issue";
                location_id.InnerText = "NW Issue";
            }

            query = "select COUNT(*) as Reports from General_reports";
            //string ConString = "Data Source=(DESCRIPTION =" +
            //                             "(ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.29.36)(PORT = 1521))" +
            //                             "(CONNECT_DATA =" +
            //                             "(SERVER = DEDICATED)" +
            //                             "(SERVICE_NAME = FCPROD_DR.subnetdb1.vcndb.oraclevcn.com)));" +
            //                             "User Id=APPLOGS;Password=A#9pLO7g$2o23;";
            string ConString = "Data Source=(DESCRIPTION =" +
           "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.197)(PORT = 1522))" +
           "(CONNECT_DATA =" +
             "(SERVER = DEDICATED)" +
             "(SERVICE_NAME = FCPROD)));" +
             "User Id=APPLOGS;Password=A#9pLO7g$2o23;Connection Timeout=600; Max Pool Size=150;";
            DataTable dm = get_oracle_data(query, ConString);           
            if (dm.Rows.Count > 0)
            {
                report_cnt.InnerText ="Total Reports Available : " + dm.Rows[0]["Reports"].ToString();
            }
            else
            {
                report_cnt.InnerText = "NW ISSUE";
            }

        }
        protected void search_report(object sender, EventArgs e)
        {
            string query = "select distinct Dept_Name,Report_Name,Report_Build_date as Report_Build_date from General_reports  where (upper(Report_Name) like '%" + txt_search.Value.Trim().ToUpper() + "%' or upper(Dept_Name) like '%" + txt_search.Value.Trim().ToUpper() + "%') and Report_Status='1'  order by Dept_Name";
            string ConString = "Data Source=(DESCRIPTION =" +
            "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.197)(PORT = 1522))" +
            "(CONNECT_DATA =" +
              "(SERVER = DEDICATED)" +
              "(SERVICE_NAME = FCPROD)));" +
              "User Id=APPLOGS;Password=A#9pLO7g$2o23;Connection Timeout=600; Max Pool Size=150;";
            //string ConString = "Data Source=(DESCRIPTION =" +
            //                             "(ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.29.36)(PORT = 1521))" +
            //                             "(CONNECT_DATA =" +
            //                             "(SERVER = DEDICATED)" +
            //                             "(SERVICE_NAME = FCPROD_DR.subnetdb1.vcndb.oraclevcn.com)));" +
            //                             "User Id=APPLOGS;Password=A#9pLO7g$2o23;";
            OracleConnection con = new OracleConnection(ConString);
            DataTable dm = get_oracle_data(query, ConString);
            string output = "";
            string new_dept_name = "";
            for (int i = 0; i < dm.Rows.Count; i++)
            {
                string dept_name = dm.Rows[i]["Dept_Name"].ToString();
                if (new_dept_name != dept_name)
                {
                    output = output + "<div class='search_and_dep'>" + dm.Rows[i]["Dept_Name"].ToString() + "</div>";
                    new_dept_name = dept_name;
                }

                output = output + "<div class='search_and_rep'> <a class='a_ref_class' href='GeneralReport.aspx?D=" + dm.Rows[i]["Dept_Name"].ToString() + "&R=" + dm.Rows[i]["Report_Name"].ToString() + "'>" + dm.Rows[i]["Report_Name"].ToString() + "</a></div>";

            }
            report_display.InnerHtml = output;
            // ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage5", " show_search();", true);

        }

        protected void download_cert3(object sender, EventArgs e)
        {
            string output = "                                   *************************";
            output = output + Environment.NewLine + Environment.NewLine + "                                   BASSEIN CATHOLIC CO-OP BANK LTD. (SCHEDULED BANK)";
            StringBuilder sb = new StringBuilder();

            string ConString = "Data Source=(DESCRIPTION =" +
            "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.197)(PORT = 1522))" +
            "(CONNECT_DATA =" +
              "(SERVER = DEDICATED)" +
              "(SERVICE_NAME = FCPROD)));" +
              "User Id=APPLOGS;Password=A#9pLO7g$2o23;Connection Timeout=600; Max Pool Size=150;";
            //string ConString = "Data Source=(DESCRIPTION =" +
            //                             "(ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.29.36)(PORT = 1521))" +
            //                             "(CONNECT_DATA =" +
            //                             "(SERVER = DEDICATED)" +
            //                             "(SERVICE_NAME = FCPROD_DR.subnetdb1.vcndb.oraclevcn.com)));" +
            //                             "User Id=APPLOGS;Password=A#9pLO7g$2o23;";
            string query = "select a.*,b.CON_NAME,b.CON_STRING from General_reports a left join database_connection b on b.ID=a.MIS_PROD where a.Report_Status='1' ";
            OracleConnection con = new OracleConnection(ConString);
            DataTable dm = get_oracle_data(query, ConString);
            for (int i = 0; i < dm.Rows.Count; i++)
            {
               
                query = AesOperation.DecryptString(dm.Rows[i]["Query"].ToString(), AesOperation.Key, AesOperation.IV);

                output = output + Environment.NewLine + "SR No : " + (i + 1) + " " + dm.Rows[i]["Report_Name"].ToString()  + Environment.NewLine;
                string para = "";
               string[] words = query.Split(' ');
                for(int j=0;j<words.Length;j++)
                {
                    if(words[j].Contains("fcrhost.") || words[j].Contains("FCRHOST."))
                    {
                        string[] xf = words[j].Split('.');
                        para = para + Environment.NewLine + xf[1];
                    }
                    if (words[j].Contains("ubshost.") || words[j].Contains("UBSHOST."))
                    {
                        string[] xf = words[j].Split('.');
                        para = para + Environment.NewLine + xf[1];
                    }
                    if (words[j].Contains("fc3gl.") || words[j].Contains("FC3GL."))
                    {
                        string[] xf = words[j].Split('.');
                        para = para + Environment.NewLine + xf[1];
                    }

                }             

                output = output + "Tables:" + Environment.NewLine + para + Environment.NewLine;
            }



            output = output + Environment.NewLine + Environment.NewLine + "                                   © copyright - 2024 BASSEIN CATHOLIC CO-OP BANK LTD. (SCHEDULED BANK)";

            sb.Append(output);
            sb.Append("\r\n");

            string text = sb.ToString();

            Response.Clear();
            Response.ClearHeaders();

            Response.AppendHeader("Content-Length", text.Length.ToString());
            Response.ContentType = "text/plain";
            Response.AppendHeader("Content-Disposition", "attachment;filename=\"Report Summary Tables.txt\"");

            Response.Write(text);
            Response.End();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage5", " show_search();", true);
        }

        protected void download_cert2(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            string output = "                                   ****************Ready Reports*********";
            output = output + Environment.NewLine + Environment.NewLine + "                                   BASSEIN CATHOLIC CO-OP BANK LTD. (SCHEDULED BANK)";

            string ConString = "Data Source=(DESCRIPTION =" +
             "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.197)(PORT = 1522))" +
             "(CONNECT_DATA =" +
               "(SERVER = DEDICATED)" +
               "(SERVICE_NAME = FCPROD)));" +
               "User Id=APPLOGS;Password=A#9pLO7g$2o23;Connection Timeout=600; Max Pool Size=150;";

            //string ConString = "Data Source=(DESCRIPTION =" +
            //                             "(ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.29.36)(PORT = 1521))" +
            //                             "(CONNECT_DATA =" +
            //                             "(SERVER = DEDICATED)" +
            //                             "(SERVICE_NAME = FCPROD_DR.subnetdb1.vcndb.oraclevcn.com)));" +
            //                             "User Id=APPLOGS;Password=A#9pLO7g$2o23;";
            string query = "select a.*,b.CON_NAME,b.CON_STRING from General_reports a left join database_connection b on b.ID=a.MIS_PROD where a.Report_Status='1' ";
            OracleConnection con = new OracleConnection(ConString);
            DataTable dm = get_oracle_data(query, ConString);

            string new_dept_name = "";
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
                output = output + Environment.NewLine + Environment.NewLine + AesOperation.DecryptString(dm.Rows[i]["Query"].ToString(), AesOperation.Key, AesOperation.IV) + Environment.NewLine;

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
        protected void download_cert(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            string output = "                                   ****************Ready Reports*********";
            output = output + Environment.NewLine + Environment.NewLine + "                                   BASSEIN CATHOLIC CO-OP BANK LTD. (SCHEDULED BANK)";

            string query = "select Dept_Name,Report_Name,Report_Build_date  as Report_Build_date from General_reports order by Dept_Name";

            string ConString = "Data Source=(DESCRIPTION =" +
            "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.197)(PORT = 1522))" +
            "(CONNECT_DATA =" +
              "(SERVER = DEDICATED)" +
              "(SERVICE_NAME = FCPROD)));" +
              "User Id=APPLOGS;Password=A#9pLO7g$2o23;Connection Timeout=600; Max Pool Size=150;";
            //string ConString = "Data Source=(DESCRIPTION =" +
            //                             "(ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.29.36)(PORT = 1521))" +
            //                             "(CONNECT_DATA =" +
            //                             "(SERVER = DEDICATED)" +
            //                             "(SERVICE_NAME = FCPROD_DR.subnetdb1.vcndb.oraclevcn.com)));" +
            //                             "User Id=APPLOGS;Password=A#9pLO7g$2o23;";
            string new_dept_name = "";
            OracleConnection con = new OracleConnection(ConString);
            DataTable dm = get_oracle_data(query, ConString);
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
                output = output + Environment.NewLine + dm.Rows[i]["Report_Name"].ToString() + "     ===> Report Build Date - " + dm.Rows[i]["Report_Build_date"].ToString();
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
        protected void show_report(object sender, EventArgs e)
        {
            //frm_div.Visible = true;
            //to_div.Visible = true;
            //input_txt.Visible = true;
            //input_num.Visible = true;
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

        private DataTable get_oracle_data2(string query, string ConString)
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
                lbldbref.Text = "";
            }
            catch (Exception yy)
            {
                lbldbref.Text = "Exception";
            }
            finally
            {
                con.Close();
            }
            return dt;
        }

        protected void Load_dept_name(object sender, EventArgs e)
        {
            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            //string query = "'";
            //query = "select ROW_NUMBER() OVER (ORDER BY Reporting_Departing) AS serial_number,Reporting_Departing from Reporting_Dept where Curr_stat='1'";
            //DataTable dt = get_normal_data(query, con);
            //ddl_dep.Items.Clear();
            //ddl_dep.DataSource = dt;
            //ddl_dep.DataBind();
            //ddl_dep.DataTextField = "Reporting_Departing";
            //ddl_dep.DataValueField = "serial_number";
            //ddl_dep.DataBind();
            //ddl_dep.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--All Departments--", "0"));

            string query = "select ROWNUM as serial_number,Reporting_Departing from Reporting_Dept where Curr_stat='1' order by Reporting_Departing";
            string ConString = "Data Source=(DESCRIPTION =" +
             "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.197)(PORT = 1522))" +
             "(CONNECT_DATA =" +
               "(SERVER = DEDICATED)" +
               "(SERVICE_NAME = FCPROD)));" +
               "User Id=APPLOGS;Password=A#9pLO7g$2o23;Connection Timeout=600; Max Pool Size=150;";
            //string ConString = "Data Source=(DESCRIPTION =" +
            //                             "(ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.29.36)(PORT = 1521))" +
            //                             "(CONNECT_DATA =" +
            //                             "(SERVER = DEDICATED)" +
            //                             "(SERVICE_NAME = FCPROD_DR.subnetdb1.vcndb.oraclevcn.com)));" +
            //                             "User Id=APPLOGS;Password=A#9pLO7g$2o23;";
            DataTable dt = get_oracle_data(query, ConString);
            ddl_dep.Items.Clear();
            ddl_dep.DataSource = dt;
            ddl_dep.DataBind();
            ddl_dep.DataTextField = "Reporting_Departing";
            ddl_dep.DataValueField = "serial_number";
            ddl_dep.DataBind();
            ddl_dep.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--All Departments--", "0"));

            if (DepName.Text != "")
            {
                //ddl_dep.SelectedItem.Text = DepName.Text;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["Reporting_Departing"].ToString() == DepName.Text)
                    {
                        ddl_dep.SelectedValue = dt.Rows[i]["serial_number"].ToString();
                    }
                }
                Load_dept_report(sender, e);
            }

        }
        protected void cmbActivity_SelectedIndexChanged(object sender, EventArgs e)
        {
            reset(sender, e);
        }

        protected void reset(object sender, EventArgs e)
        {
            DepName.Text = "";
            RepName.Text = "";
            Load_dept_report(sender, e);
            Grid_view.DataSource = null;
            Grid_view.DataBind();
            con_lbl.InnerText = "";
            lbl_rep_note.InnerText = "";
            size_int.InnerText = "";
            creation_date.InnerText = "";
            con_lbl_2.Attributes.Add("style", "background:white;");
            frm_dat_div.Visible = false;
            to_dat_div.Visible = false;
            txt_inp_div.Visible = false;
            txt_inp_div2.Visible = false;
            notify.Visible = false;
            moving_logo.Visible = false;
            size_info.Visible = false;
            note_div.Visible = false;
            conect_div.Visible = false;
            date_div.Visible = false;
            data_exporter.Visible = false;
            exporter_div.Visible = false;
            tester.Visible = false;
            //btn_export.Visible = false;
            database_date.InnerText = "";
        }

        protected void Load_dept_report(object sender, EventArgs e)
        {
            string ConString = "Data Source=(DESCRIPTION =" +
            "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.197)(PORT = 1522))" +
            "(CONNECT_DATA =" +
              "(SERVER = DEDICATED)" +
              "(SERVICE_NAME = FCPROD)));" +
              "User Id=APPLOGS;Password=A#9pLO7g$2o23;Connection Timeout=600; Max Pool Size=150;";
            //string ConString = "Data Source=(DESCRIPTION =" +
            //                             "(ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.29.36)(PORT = 1521))" +
            //                             "(CONNECT_DATA =" +
            //                             "(SERVER = DEDICATED)" +
            //                             "(SERVICE_NAME = FCPROD_DR.subnetdb1.vcndb.oraclevcn.com)));" +
            //                             "User Id=APPLOGS;Password=A#9pLO7g$2o23;";
            string click_str = ddl_dep.SelectedItem.Text;
            string query = "select ROW_NUMBER() OVER (ORDER BY Report_Name) AS serial_number, Report_Name from General_reports where Dept_Name='" + click_str + "' and Report_Status='1'";
            OracleConnection con = new OracleConnection(ConString);
            DataTable dt = get_oracle_data(query, ConString);
            ddlreport.Items.Clear();
            ddlreport.DataSource = dt;
            ddlreport.DataBind();
            ddlreport.DataTextField = "Report_Name";
            ddlreport.DataValueField = "serial_number";
            ddlreport.DataBind();
            ddlreport.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please Select Report", "0"));
            // export_to_excel.Visible = false;
            if (RepName.Text != "")
            {
                ddlreport.SelectedItem.Text = RepName.Text;
                conreport_SelectedIndexChanged(sender, e);
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
            dllocation.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All Branches", "0"));
        }

        private void show_special_selection(string specialText)
        {
            ddl_special_sel.Items.Clear();
            string ConString = con_type.Value;
            string query = specialText;
            List<string> selection = new List<string>();
            if (specialText.Contains("$%^&") == true)
            {
                string[] p = query.Split(':');
                query = p[1].ToString();
                OracleConnection con = new OracleConnection(ConString);
                DataTable dt = get_oracle_data(query, ConString);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            selection.Add(dt.Rows[i][j].ToString());
                        }
                    }
                }
            }
            else
            {
                string[] p = query.Split(':');
                query = p[1].ToString();
                p = query.Split(',');
                for (int j = 0; j < p.Length; j++)
                {
                    selection.Add(p[j].ToString());
                }

            }
            ddl_special_sel.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please Select Input", "0"));
            for (int i = 1; i <= selection.Count; i++)
            {
                ddl_special_sel.Items.Insert(i, new System.Web.UI.WebControls.ListItem(selection[i - 1].ToString(), i.ToString()));
            }


        }

        protected void conreport_SelectedIndexChanged(object sender, EventArgs e)
        {
            exporter_div.Visible = false;
            data_exporter.Visible = false;
            Grid_view.DataSource = null;
            Grid_view.DataBind();
            //btn_export.Visible = false;

            string ConString = "Data Source=(DESCRIPTION =" +
            "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.197)(PORT = 1522))" +
            "(CONNECT_DATA =" +
              "(SERVER = DEDICATED)" +
              "(SERVICE_NAME = FCPROD)));" +
              "User Id=APPLOGS;Password=A#9pLO7g$2o23;Connection Timeout=600; Max Pool Size=150;";
            //string ConString = "Data Source=(DESCRIPTION =" +
            //                             "(ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.29.36)(PORT = 1521))" +
            //                             "(CONNECT_DATA =" +
            //                             "(SERVER = DEDICATED)" +
            //                             "(SERVICE_NAME = FCPROD_DR.subnetdb1.vcndb.oraclevcn.com)));" +
            //                             "User Id=APPLOGS;Password=A#9pLO7g$2o23;";
            string dept_name = ddl_dep.SelectedItem.Text;
            string click_str = ddlreport.SelectedItem.Text;
            if (click_str != "Please Select Report")
            {
                string query = "select a.*,b.CON_NAME,b.CON_STRING from General_reports a left join database_connection b on b.ID=a.MIS_PROD where ltrim(rtrim(a.Report_Name))='" + click_str.Trim() + "' and a.Report_Status='1' and a.Dept_Name='" + dept_name + "'";
                OracleConnection con = new OracleConnection(ConString);
                DataTable dt = get_oracle_data(query, ConString);
                if (dt.Rows[0]["Big_Small"].ToString() == "1")
                {
                    size_int.InnerText = "Small  (It should take less than 2 min to get Output)";                   
                }
                else if (dt.Rows[0]["Big_Small"].ToString() == "0")
                {
                    size_int.InnerText = "Big (It may take more than 2 min to get Output)";                   
                }
                else
                {
                    exporter_div.Visible = true;
                    size_int.InnerText = "Large  (It may take more than 15 min to get full Output)";                   
                }

                if (dt.Rows[0]["Is_Branch_Allowed"].ToString() == "0")
                {
                    dllocation.Enabled = false;
                    dllocation.CssClass = "ddl_class";
                }
                else
                {
                    dllocation.Enabled = true;
                }

                query = dt.Rows[0]["Query"].ToString();
                query = AesOperation.DecryptString(query, AesOperation.Key, AesOperation.IV);
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
                if (query.Contains("@@@@$$$$") == true)
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
                if (act_is.Value == "")
                {
                    con_lbl.InnerText = dt.Rows[0]["CON_NAME"].ToString();
                    con_type.Value = dt.Rows[0]["CON_STRING"].ToString();
                    
                }
                else
                {
                    con_lbl.InnerText = con_name_d.InnerText;
                    lbl_rep_note.InnerText = act_reason.InnerText;
                }

                if (con_lbl.InnerText == "MIS (T-1)")
                {
                    query = "select * from fcrhost.mis_refresh_date";
                    lbldbref.Text = "";
                    DataTable df = get_oracle_data2(query, con_type.Value);
                    if (df.Rows.Count == 0)
                    {
                        lbldbref.Text = "Exception";
                    }
                    else
                    {
                        lbldbref.Text = "";
                    }
                }

                if (query.Contains("%%%%$$$$") == true)
                {
                    show_special_selection(dt.Rows[0]["SpecialQuery"].ToString());
                    special_sele.Visible = true;
                }
                else
                {
                    special_sele.Visible = false;
                }

                string pr = dt.Rows[0]["CREATOR_USER"].ToString();
                if(dt.Rows[0]["EDITOR_USER"].ToString()!="")
                {
                    pr = dt.Rows[0]["EDITOR_USER"].ToString();
                }
                query = "select * from Admin_LoginTbl where AD_Code='"+ pr + "'";
                SqlConnection conv = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
                DataTable dx = get_normal_data(query, conv);
                if(dx.Rows.Count>0)
                {
                    sf_engi.InnerText = dx.Rows[0]["Extra2"].ToString();
                }
                else
                {
                    sf_engi.InnerText = "Network Issue";
                }

                dept_tester.InnerText= dt.Rows[0]["ReportTester"].ToString();
                mis_tester.InnerText = dt.Rows[0]["MISTester"].ToString();

                creation_date.InnerText = dt.Rows[0]["Report_Build_date"].ToString();
                check_connection();
                moving_logo.Visible = true;
                notify.Visible = true;
                size_info.Visible = true;
                check_notifybutton();
                //instructions_div.Visible = true;
                note_div.Visible = true;
                conect_div.Visible = true;
                date_div.Visible = true;
                tester.Visible = true;
                //ScriptManager.RegisterStartupScript(this, GetType(), "remove_anim", "remove_anim();", true);
            }
            else
            {
                con_lbl.InnerText = "";
                lbl_rep_note.InnerText = "";
                size_int.InnerText = "";
                creation_date.InnerText = "";
                con_lbl_2.Attributes.Add("style", "background:white;");
                frm_dat_div.Visible = false;
                to_dat_div.Visible = false;
                txt_inp_div.Visible = false;
                txt_inp_div2.Visible = false;
                notify.Visible = false;
                moving_logo.Visible = false;
                size_info.Visible = false;
                //instructions_div.Visible = false;
                note_div.Visible = false;
                conect_div.Visible = false;
                date_div.Visible = false;
                tester.Visible = false;
                dept_tester.InnerText = "";
                mis_tester.InnerText = "";
                //btn_export.Visible = false;
            }
        }
        protected void check_env_type()
        {
            con_type.Value = "";
            //string ConString = "Data Source=(DESCRIPTION =" +
            //                             "(ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.29.36)(PORT = 1521))" +
            //                             "(CONNECT_DATA =" +
            //                             "(SERVER = DEDICATED)" +
            //                             "(SERVICE_NAME = FCPROD_DR.subnetdb1.vcndb.oraclevcn.com)));" +
            //                             "User Id=APPLOGS;Password=A#9pLO7g$2o23;";
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
            if (dt.Rows.Count > 0)
            {
                con_name_d.InnerText = dt.Rows[0]["CON_NAME"].ToString();
                act_reason.InnerText = dt.Rows[0]["REASON"].ToString();
                con_type.Value = dt.Rows[0]["CON_STRING"].ToString();
                db_type.InnerText = dt.Rows[0]["CON_TYPE"].ToString();
                schema_hid.Value = dt.Rows[0]["SPLIT_STR"].ToString();
                act_is.Value = "1";
                if (con_name_d.InnerText == "MIS (T-1)")
                {
                    query = "select * from fcrhost.mis_refresh_date";
                    lbldbref.Text = "";
                    DataTable df = get_oracle_data2(query, con_type.Value);
                    if (df.Rows.Count == 0)
                    {
                        lbldbref.Text = "Exception";
                    }
                    else
                    {
                        lbldbref.Text = "";
                    }
                }

                ScriptManager.RegisterStartupScript(this, GetType(), "showmaster", "show_master();", true);
            }
            else
            {
                con_name_d.InnerText = "";
                act_reason.InnerText = "";
                con_type.Value = "";
                db_type.InnerText = "";
                schema_hid.Value = "";
                act_is.Value = "";
                ScriptManager.RegisterStartupScript(this, GetType(), "hidemaster", "hide_master();", true);
            }

        }
        protected void check_connection()
        {
            string ConString = con_type.Value;
            OracleConnection con = new OracleConnection(ConString);
            try
            {
                con.Open();
                con_lbl_2.Attributes.Add("style", "background:#92f763;");
                con.Close();
                string query = "SELECT TO_CHAR(MAX(DAT_POST),'DD-MM-YYYY') as Live_date FROM FCRHOST.BA_CASH_DENM";
                DataTable dt = get_oracle_data(query, ConString);                
                database_date.InnerText = "Database Date :" + dt.Rows[0]["Live_date"].ToString();
            }
            catch (Exception yy)
            {
                con_lbl_2.Attributes.Add("style", "background:#f7983c;");
            }
            finally
            {
                con.Close();
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
        protected void Generate_report(object sender, EventArgs e)
        {
            //ScriptManager.RegisterStartupScript(this, GetType(), "remove_anim", "remove_anim();", true);
            generate_rpt.Disabled = true;
            //btn_export.Visible = true;
            //  string dept_name = ddl_dep.Items[Convert.ToInt32(Request.Form["ddl_dep"].ToString())].ToString();
            string dept_name = ddl_dep.SelectedItem.Text;
            string click_str = ddlreport.SelectedItem.Text;
            if (lbldbref.Text == "")
            {
                if (click_str != "Please Select Report")
                {
                    //string ConString = "Data Source=(DESCRIPTION =" +
                    //                         "(ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.29.36)(PORT = 1521))" +
                    //                         "(CONNECT_DATA =" +
                    //                         "(SERVER = DEDICATED)" +
                    //                         "(SERVICE_NAME = FCPROD_DR.subnetdb1.vcndb.oraclevcn.com)));" +
                    //                         "User Id=APPLOGS;Password=A#9pLO7g$2o23;";
                    string ConString = "Data Source=(DESCRIPTION =" +
                "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.197)(PORT = 1522))" +
                "(CONNECT_DATA =" +
                  "(SERVER = DEDICATED)" +
                  "(SERVICE_NAME = FCPROD)));" +
                  "User Id=APPLOGS;Password=A#9pLO7g$2o23;Connection Timeout=600; Max Pool Size=150;";

                    string query = "select a.*,b.CON_NAME,b.CON_STRING from General_reports a left join database_connection b on b.ID=a.MIS_PROD where ltrim(rtrim(a.Report_Name))='" + click_str.Trim() + "' and a.Report_Status='1' and a.Dept_Name='" + dept_name + "'";
                    OracleConnection con = new OracleConnection(ConString);
                    DataTable dt = get_oracle_data(query, ConString);

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
                        query = AesOperation.DecryptString(query, AesOperation.Key, AesOperation.IV);
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

                        if (special_sele.Visible == true)
                        {
                            if (ddl_special_sel.SelectedValue != "0")
                            {
                                branch = ddl_special_sel.SelectedItem.Text.ToString();
                                query = query.Replace("%%%%$$$$", branch);
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
                        query = AesOperation.DecryptString(query, AesOperation.Key, AesOperation.IV);
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
                        if (special_sele.Visible == true)
                        {
                            if (ddl_special_sel.SelectedValue != "0")
                            {
                                branch = ddl_special_sel.SelectedItem.Text.ToString();
                                query = query.Replace("%%%%$$$$", branch);
                            }
                            else
                            {
                                cl = 1;
                            }
                        }
                    }
                    ConString = con_type.Value;

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
                        hidden_info_col.Value = "";
                        hidden_col_count.Value = "";
                        //ScriptManager.RegisterStartupScript(this, GetType(), "hide_wait", "hide_wait();", true);
                        if (dm.Rows.Count > 0)
                        {
                            for (int i = 0; i < dm.Columns.Count; i++)
                            {
                                if (i == 0)
                                {
                                    hidden_info_col.Value = dm.Columns[i].ColumnName;
                                }
                                else
                                {
                                    hidden_info_col.Value = hidden_info_col.Value + "," + dm.Columns[i].ColumnName;
                                }

                            }
                            hidden_col_count.Value = dm.Columns.Count.ToString();
                            if (size_int.InnerText != "Large  (It may take more than 15 min to get full Output)")
                            {
                                data_exporter.Visible = true;
                                Grid_view.DataSource = dm;
                                Grid_view.DataBind();
                            }
                            else
                            {
                                data_exporter.Visible = false;
                                if (ddl_datatype.SelectedValue == "0")
                                {
                                    heavy_export(dm);
                                }
                                else if (ddl_datatype.SelectedValue == "1")
                                {
                                    heavy_csv(dm);
                                }
                                else if (ddl_datatype.SelectedValue == "2")
                                {
                                    heavy_text(dm);
                                }

                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('No record for this report!!!');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('Please give necessary inputs!!!');", true);
                    }

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('Please select Report Name!!!');", true);
                    //btn_export.Visible = false;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('Database Refresh is in progress. Please wait!!!');", true);
            }
            generate_rpt.Disabled = false;           
        }

        protected void export_excel(object sender, EventArgs e)
        {
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

        protected void Export_to_csv(object sender, EventArgs e)
        {
            string dep = ddl_dep.SelectedItem.Text;
            string click_str = ddlreport.SelectedItem.Text;
            Response.Clear();
            Response.Buffer = true;
            string FileName = "" + dep + "_" + click_str + "_" + DateTime.Now + ".csv";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            Response.Charset = "";
            Response.ContentType = "text/csv";
            StringBuilder sb = new StringBuilder();
            sb.Append(hidden_info_col.Value + ',');
            sb.Append("\r\n");
            for (int i = 0; i < Grid_view.Rows.Count; i++)
            {
                for (int k = 0; k < Grid_view.Rows[i].Cells.Count; k++)
                {
                    string mk = Grid_view.Rows[i].Cells[k].Text;
                    var step1 = Regex.Replace(mk, @"<[^>]+>|&nbsp;", "").Trim();
                    var step2 = Regex.Replace(step1, @"\s{2,}", " ");
                    mk = step2.ToString();
                    sb.Append(mk.Replace(",","") + ',');
                }
                sb.Append("\r\n");
            }
            Response.Output.Write(sb.ToString());
            Response.Flush();
            Response.End();
        }

        private void heavy_csv(DataTable dt)
        {
            string dep = ddl_dep.SelectedItem.Text;
            string click_str = ddlreport.SelectedItem.Text;
            Response.Clear();
            Response.Buffer = true;
            string FileName = "" + dep + "_" + click_str + "_" + DateTime.Now + ".csv";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            Response.Charset = "";
            Response.ContentType = "text/csv";
            StringBuilder sb = new StringBuilder();
            sb.Append(hidden_info_col.Value + ',');
            sb.Append("\r\n");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int k = 0; k < dt.Columns.Count; k++)
                {
                    string mk = dt.Rows[i][k].ToString();
                    var step1 = Regex.Replace(mk, @"<[^>]+>|&nbsp;", "").Trim();
                    var step2 = Regex.Replace(step1, @"\s{2,}", " ");
                    mk = step2.ToString();
                    sb.Append(mk.Replace(",", "") + ',');
                }
                sb.Append("\r\n");
            }
            Response.Output.Write(sb.ToString());
            Response.Flush();
            Response.End();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }

      
        protected void Export_to_PDF(object sender, EventArgs e)
        {
            string dep = ddl_dep.SelectedItem.Text;
            string click_str = ddlreport.SelectedItem.Text;
            Response.ContentType = "application/pdf";
            string FileName = "" + dep + "_" + click_str + "_" + DateTime.Now + ".pdf";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Grid_view.RenderControl(htmltextwrtter);
            StringReader sr = new StringReader(strwritter.ToString());
            Rectangle rec = PageSize.A4;
            int p = Convert.ToInt32(hidden_col_count.Value);
            if(p > 20)
            {
                rec = PageSize.A0;
            }
            else if(p < 20 && p > 16)
            {
                rec = PageSize.A1;
            }
            else if (p < 16 && p > 12)
            {
                rec = PageSize.A2;
            }
            else if (p < 12 && p > 8)
            {
                rec = PageSize.A3;
            }
            else if (p < 8 && p > 4)
            {
                rec = PageSize.A4;
            }
            Document pdfDoc = new Document(rec, 10f, 10f, 10f, 10f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            htmlparser.Parse(sr);
            pdfDoc.Close();
            Response.Write(pdfDoc);
            Response.End();
        }

        protected void Export_to_Text(object sender, EventArgs e)
        {
            string dep = ddl_dep.SelectedItem.Text;
            string click_str = ddlreport.SelectedItem.Text;
            Response.Clear();
            string FileName = "" + dep + "_" + click_str + "_" + DateTime.Now + ".txt";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            Response.Charset = "";
            Response.ContentType = "text/plain";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            List<int> Size_grid = new List<int>();

            for (int i = 0; i < Grid_view.Rows.Count; i++)
            {
                if (i == 0)
                {
                    for (int k = 0; k < Grid_view.Rows[i].Cells.Count; k++)
                    {
                        Size_grid.Add(Grid_view.Rows[i].Cells[k].Text.Length);
                    }
                }
                else
                {
                    for (int k = 0; k < Grid_view.Rows[i].Cells.Count; k++)
                    {
                        if (Size_grid[k] < Grid_view.Rows[i].Cells[k].Text.Length)
                        {
                            Size_grid[k] = Grid_view.Rows[i].Cells[k].Text.Length;
                        }
                    }
                }

            }

            string[] mx = hidden_info_col.Value.Split(',');           
                for (int k = 0; k < Size_grid.Count; k++)
                {
                if (Size_grid[k] < mx[k].Length)
                {
                    Size_grid[k] = mx[k].Length;
                }
                strwritter.Write(mx[k].PadRight(Size_grid[k] + 4));
                }
            
            strwritter.WriteLine();

            for (int i = 0; i < Grid_view.Rows.Count; i++)
            {
                for (int k = 0; k < Grid_view.Rows[i].Cells.Count; k++)
                {
                    string mk = Grid_view.Rows[i].Cells[k].Text;
                    var step1 = Regex.Replace(mk, @"<[^>]+>|&nbsp;", "").Trim();
                    var step2 = Regex.Replace(step1, @"\s{2,}", " ");
                    mk = step2.ToString();
                    strwritter.Write(mk.PadRight(Size_grid[k] + 4));
                }
                strwritter.WriteLine();
            }
            Response.Write(strwritter.ToString());
            Response.End();
        }

        private void heavy_text(DataTable dt)
        {
            string dep = ddl_dep.SelectedItem.Text;
            string click_str = ddlreport.SelectedItem.Text;
            Response.Clear();
            string FileName = "" + dep + "_" + click_str + "_" + DateTime.Now + ".txt";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            Response.Charset = "";
            Response.ContentType = "text/plain";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            List<int> Size_grid = new List<int>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i == 0)
                {
                    for (int k = 0; k < dt.Columns.Count; k++)
                    {
                        Size_grid.Add(dt.Rows[i][k].ToString().Length);
                    }
                }
                else
                {
                    for (int k = 0; k < dt.Columns.Count; k++)
                    {
                        if (Size_grid[k] < dt.Rows[i][k].ToString().Length)
                        {
                            Size_grid[k] = dt.Rows[i][k].ToString().Length;
                        }
                    }
                }

            }


            string[] mx = hidden_info_col.Value.Split(',');
            for (int k = 0; k < Size_grid.Count; k++)
            {
                if (Size_grid[k] < mx[k].Length)
                {
                    Size_grid[k] = mx[k].Length;
                }
                strwritter.Write(mx[k].PadRight(Size_grid[k] + 4));
            }
            strwritter.WriteLine();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int k = 0; k < dt.Columns.Count; k++)
                {
                    string mk = dt.Rows[i][k].ToString();
                    var step1 = Regex.Replace(mk, @"<[^>]+>|&nbsp;", "").Trim();
                    var step2 = Regex.Replace(step1, @"\s{2,}", " ");
                    mk = step2.ToString();
                    strwritter.Write(mk.PadRight(Size_grid[k] + 5));
                }
                strwritter.WriteLine();
            }

            Response.Write(strwritter.ToString());
            Response.End();
        }
    }
}