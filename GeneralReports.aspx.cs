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
    public partial class GeneralReports : System.Web.UI.Page
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
                Load_drop_down();
                // Load_dept_report();
                Load_dept_name();
            }
        }
        protected void cmbActivity_SelectedIndexChanged(object sender, EventArgs e)
        {
            Load_dept_report();
        }
        protected void conreport_SelectedIndexChanged(object sender, EventArgs e)
        {
            string click_str = ddlreport.Items[Convert.ToInt32(Request.Form["ddlreport"].ToString())].ToString();
            if (click_str != "Please Select Report")
            {
                string query = "select * from General_reports where Report_Name='" + click_str + "' and Report_Status!='3'";
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
                DataTable dt = get_normal_data(query, con);
                if(dt.Rows[0]["Big_Small"].ToString()=="1")
                {
                    lbl_rep_size.InnerText = "Small  (It should take less than 2 min to get Output)";
                }
                else if(dt.Rows[0]["Big_Small"].ToString() == "0")
                {
                    lbl_rep_size.InnerText = "Big  (It may take more than 2 min to get Output)";
                }
                else
                {
                    lbl_rep_size.InnerText = "Large  (It may take more than 15 min to get full Output)";
                }

                if(dt.Rows[0]["Is_Branch_Allowed"].ToString()=="0")
                {
                    dllocation.Enabled = false;
                }
                else
                {
                    dllocation.Enabled = true;
                }

                 query= dt.Rows[0]["Query"].ToString();
                if(query.Contains("#$#$#$#$") ==true)
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

                lbl_rep_note.InnerText = dt.Rows[0]["Note"].ToString();
            }
        }
        protected void Generate_report(object sender, EventArgs e)
        {
            string click_str = ddlreport.Items[Convert.ToInt32(Request.Form["ddlreport"].ToString())].ToString();
            report_gen_btn.Disabled = true;
            if (click_str != "Please Select Report")
            {
                string query = "select * from General_reports where Report_Name='" + click_str + "' and Report_Status!='3'";
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
                        if(from_date.Text!="")
                        {
                            branch = from_date.Text;
                            query = query.Replace("#$#$#$#$", branch);
                        }
                        else
                        {
                            cl = 1;
                        }
                        if (from_date.Text != "")
                        {
                            branch = from_date.Text;
                            query = query.Replace("$#$#$#$#", branch);
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
                        if (from_date.Text != "")
                        {
                            branch = from_date.Text;
                            query = query.Replace("$#$#$#$#", branch);
                        }
                        else
                        {
                            cl = 1;
                        }
                    }
                }
                string ConString = "Data Source=(DESCRIPTION =" +
                 "(ADDRESS = (PROTOCOL = TCP)(HOST = fcobdxdb-scan)(PORT = 1522))" +
                 "(CONNECT_DATA =" +
                   "(SERVER = DEDICATED)" +
                   "(SERVICE_NAME = FCPROD)));" +
                   "User Id=pfms_read;Password=pFm$_Read$2023;";
                if (cl == 0)
                {
                    DataTable dm = get_oracle_data(query, ConString);
                    ExportGridToCSV(dm);
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
            }
            report_gen_btn.Disabled = false;
        }
        private void ExportGridToCSV(DataTable dt)
        {
            string output = "";
            string click_str = ddlreport.Items[Convert.ToInt32(Request.Form["ddlreport"].ToString())].ToString();
            string dep = ddl_dep.Items[Convert.ToInt32(Request.Form["ddl_dep"].ToString())].ToString();
            for (int k = 0; k < dt.Columns.Count; k++)
            {
                output= output+dt.Columns[k].ColumnName + "|";
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
                Response.AddHeader("Content-Disposition", "attachment; filename="+ dep + "_"+ click_str + ".csv");
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
        protected void Load_dept_name()
        {
            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            //string query = "select ROW_NUMBER() OVER (ORDER BY Report_Name) AS serial_number, Report_Name from General_reports where Dept_Name='Credit Monitoring' and Report_Status!='3'";
            //query = "select ROW_NUMBER() OVER (ORDER BY Reporting_Departing) AS serial_number,Reporting_Departing from Reporting_Dept where Curr_stat='1'";
            //DataTable dt = get_normal_data(query, con);
            //ddl_dep.Items.Clear();
            //ddl_dep.DataSource = dt;
            //ddl_dep.DataBind();
            //ddl_dep.DataTextField = "Reporting_Departing";
            //ddl_dep.DataValueField = "serial_number";
            //ddl_dep.DataBind();
            //ddl_dep.Items.Insert(0, new ListItem("--All Departments--", "0"));

            string query = "select ROWNUM as serial_number,Reporting_Departing from Reporting_Dept where Curr_stat='1'";
            string ConString = "Data Source=(DESCRIPTION =" +
             "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.197)(PORT = 1522))" +
             "(CONNECT_DATA =" +
               "(SERVER = DEDICATED)" +
               "(SERVICE_NAME = FCPROD)));" +
               "User Id=APPLOGS;Password=A#9pLO7g$2o23;Connection Timeout=600; Max Pool Size=150;";
            DataTable dt = get_oracle_data(query, ConString);
            ddl_dep.Items.Clear();
            ddl_dep.DataSource = dt;
            ddl_dep.DataBind();
            ddl_dep.DataTextField = "Reporting_Departing";
            ddl_dep.DataValueField = "serial_number";
            ddl_dep.DataBind();
            ddl_dep.Items.Insert(0, new ListItem("--All Departments--", "0"));
        }

        protected void btn_reset(object sender, EventArgs e)
        {
            Response.Redirect("GeneralReports.aspx");
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

        protected void Load_dept_report() 
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            string click_str = ddl_dep.Items[Convert.ToInt32(Request.Form["ddl_dep"].ToString())].ToString();
            //if (click_str != "--All Departments--")
            //{
                string query = "select ROW_NUMBER() OVER (ORDER BY Report_Name) AS serial_number, Report_Name from General_reports where Dept_Name='" + click_str + "' and Report_Status!='3'";
                DataTable dt = get_normal_data(query, con);
                ddlreport.Items.Clear();
                ddlreport.DataSource = dt;
                ddlreport.DataBind();
                ddlreport.DataTextField = "Report_Name";
                ddlreport.DataValueField = "serial_number";
                ddlreport.DataBind();
                ddlreport.Items.Insert(0, new ListItem("Please Select Report", "0"));
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
            try
            {
                OracleConnection con = new OracleConnection(ConString);
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
            return dt;


        }
    }
}