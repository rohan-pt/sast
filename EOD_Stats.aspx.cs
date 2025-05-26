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
    public partial class EOD_Stats : System.Web.UI.Page
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
                //DateTime mm = System.DateTime.Now - 1;
                EOD_date.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
                Load_drop_down();
            }
            show_Abort_calender();
        }
        protected void Load_drop_down()
        {
            string query = "select distinct a.DB_ENV,b.CON_NAME from E_LOGS a inner join DATABASE_CONNECTION b on a.DB_ENV = b.ID";
            string ConString = "Data Source=(DESCRIPTION =" +
            "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.197)(PORT = 1522))" +
            "(CONNECT_DATA =" +
              "(SERVER = DEDICATED)" +
              "(SERVICE_NAME = FCPROD)));" +
              "User Id=APPLOGS;Password=A#9pLO7g$2o23;Connection Timeout=600; Max Pool Size=150;";
            DataTable dt = get_oracle_data(query, ConString);
           
            ddl_env.Items.Clear();
            ddl_env.DataSource = dt;
            ddl_env.DataBind();
            ddl_env.DataTextField = "CON_NAME";
            ddl_env.DataValueField = "DB_ENV";
            ddl_env.DataBind();
            ddl_env.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please Select EOD Environment", "0"));
        }

        private void show_Abort_calender()
        {
            if (ddl_env.SelectedValue.ToString() != "0")
            {
                string query = " with EOD_ABORT as(select  O_DATE as \"Operation Date\",count(L_TYPE) as \"Logs Count\" from E_LOGS where  DB_env='" + ddl_env.SelectedValue.ToString() + "' " +
     " group by o_date,L_TYPE) " +
     " select  " +
     " distinct to_char(\"Operation Date\",'YYYY-MM-DD') as \"Operation Date\",sum(\"Logs Count\") as \"Logs Count\" " +
       " from EOD_ABORT group by \"Operation Date\" " +
       " order by \"Operation Date\"";
                string ConString = "Data Source=(DESCRIPTION =" +
            "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.197)(PORT = 1522))" +
            "(CONNECT_DATA =" +
              "(SERVER = DEDICATED)" +
              "(SERVICE_NAME = FCPROD)));" +
              "User Id=APPLOGS;Password=A#9pLO7g$2o23;Connection Timeout=600; Max Pool Size=150;";
                DataTable dt = get_oracle_data(query, ConString);
                if (dt.Rows.Count > 0)
                {
                    string grph_builder = "";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string eod_dt = dt.Rows[i]["Operation Date"].ToString();
                        string eod_abt_cnt = dt.Rows[i]["Logs Count"].ToString();
                        grph_builder = grph_builder + "~" + eod_dt + ":" + eod_abt_cnt;
                    }
                    ScriptManager.RegisterStartupScript(this, GetType(), "EOD_Master", "load_calender('" + grph_builder.Substring(1) + "','" + ddl_env.SelectedItem.Text.ToString() + " EOD Abort Details','Section1');", true);
                }
            }
        }

        protected void cmbActivity_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = "select * from  DATABASE_CONNECTION where ID='" + ddl_env.SelectedValue + "'";
            string ConString = "Data Source=(DESCRIPTION =" +
         "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.197)(PORT = 1522))" +
         "(CONNECT_DATA =" +
           "(SERVER = DEDICATED)" +
           "(SERVICE_NAME = FCPROD)));" +
           "User Id=APPLOGS;Password=A#9pLO7g$2o23;Connection Timeout=600; Max Pool Size=150;";
            DataTable dt = get_oracle_data(query, ConString);
            if (dt.Rows.Count > 0)
            {
                constring.Text = dt.Rows[0]["CON_STRING"].ToString();
                strString.Text= dt.Rows[0]["SPLIT_STR"].ToString();
            }
            display_eod_users();
            show_Abort_calender();
            load_batch_process_logs();
            complete_process_logs();
            load_batch_timing();
            load_batch_past_logs();
            load_batch_past_logs_EOD();
            load_batch_past_logs_BOD();
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

        private void complete_process_logs()
        {
            string query = "select * from E_LOGS where DB_ENV='" + ddl_env.SelectedValue.ToString() + "' and o_date=date'" + EOD_date.Text + "' order by S_DATE asc";
            string ConString = "Data Source=(DESCRIPTION =" +
            "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.197)(PORT = 1522))" +
            "(CONNECT_DATA =" +
              "(SERVER = DEDICATED)" +
              "(SERVICE_NAME = FCPROD)));" +
              "User Id=APPLOGS;Password=A#9pLO7g$2o23;Connection Timeout=600; Max Pool Size=150;";
            DataTable dt = get_oracle_data(query, ConString);
            if (dt.Rows.Count > 0)
            {
                string tbl_data = "<table class='full_logs_tbl'><tr><td class='fl_til' colspan='2'>Complete Process Log</td></tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string data = dt.Rows[i]["LOGSDATA"].ToString();
                    int dc = 0;

                    if (dt.Rows[i]["L_TYPE"].ToString() == "D" && dt.Rows[i]["S_TYPE"].ToString() == "D")
                    {
                        dc = 1;
                    }
                    if (dc == 0)
                    {
                        string[] core_data = data.Split('|');
                        string tit = "";
                        string col = "";
                        string varF = "";
                        if (dt.Rows[i]["L_TYPE"].ToString() == "P" && dt.Rows[i]["S_TYPE"].ToString() == "B")
                        {
                            tit = "Batch Execution Details";
                            if (dt.Rows[i]["COLOR"].ToString() == "G")
                            {
                                col = "#26cb1a";
                            }
                            else if (dt.Rows[i]["COLOR"].ToString() == "R")
                            {
                                col = "#ea0e0e";
                            }
                            else if (dt.Rows[i]["COLOR"].ToString() == "Y")
                            {
                                col = "#eee30d";
                            }
                            else if (dt.Rows[i]["COLOR"].ToString() == "D")
                            {
                                col = "#4c3be1";
                            }
                            else
                            {
                                col = "#1730f3";
                            }

                            varF = "A";
                        }
                        else if (dt.Rows[i]["L_TYPE"].ToString() == "D" && dt.Rows[i]["S_TYPE"].ToString() == "B")
                        {
                            tit = "Batch Abort Details";
                            col = "#ff0000";
                            varF = "B";
                        }
                        else if (dt.Rows[i]["L_TYPE"].ToString() == "D" && dt.Rows[i]["S_TYPE"].ToString() == "S")
                        {
                            tit = "Shell Abort or Idel state Details";
                            col = "#ff0000";
                            varF = "A";
                        }
                        else if (dt.Rows[i]["L_TYPE"].ToString() == "U" && dt.Rows[i]["S_TYPE"].ToString() == "S")
                        {
                            tit = "Update Command Used by the User Deatils @ Shell ";
                            col = "#ff0000";
                            varF = "A";
                        }
                        else if (dt.Rows[i]["L_TYPE"].ToString() == "U" && dt.Rows[i]["S_TYPE"].ToString() == "")
                        {
                            tit = "Users Logs";
                            col = "#17f367";
                            varF = "B";
                        }
                        else if (dt.Rows[i]["L_TYPE"].ToString() == "U" && dt.Rows[i]["S_TYPE"].ToString() == "N")
                        {
                            tit = "Update Command Skipped by the User Deatils @ Shell ";
                            col = "#b7a11b";
                            varF = "B";
                        }
                        else
                        {
                            tit = "General Logs";
                            col = "#71716f";
                            varF = "";
                        }
                        string main_data = "<span style='color:" + col + ";'>" + tit + "</span><br/>";
                        for (int j = 0; j < core_data.Length; j++)
                        {
                            string[] micro_data = core_data[j].Split('^');
                            main_data = main_data + micro_data[0].PadRight(30, '.') + ":" + micro_data[1] + "<br/>";
                        }

                        tbl_data = tbl_data + "<tr><td class='full_logs_td'>" + dt.Rows[i]["S_DATE"].ToString() + "</td> " +
"<td class='full_logs_td_2'>" + main_data + "</td></tr>";
                    }
                    else
                    {
                        string[] macro_data = data.Split('^');
                        string tit = macro_data[0] + " @" + dt.Rows[i]["S_DATE"].ToString();

                        string[] micro_data = macro_data[1].Split('!');
                        string main_data = "<span style='color:#ea0e0e;'>" + tit + "</span><br/>";
                        string tvs = "";
                        for (int j = 1; j < micro_data.Length; j++)
                        {
                            string[] Columns = micro_data[0].Split('~');
                            string[] abs_data = micro_data[j].Split('~');
                            string mvp = "";
                            for (int m = 0; m < abs_data.Length; m++)
                            {
                                mvp = mvp + Columns[m].PadRight(30, '.') + ":" + abs_data[m] + "<br/>";
                            }
                            main_data = main_data + "<br/>" + mvp;
                        }
                        tbl_data = tbl_data + "<tr><td class='full_logs_td'>" + dt.Rows[i]["S_DATE"].ToString() + "</td> " +
 "<td class='full_logs_td_2'>" + main_data + "</td></tr>";
                    }
                }
                full_logs.InnerHtml = tbl_data + "</table>";

            }
            else
            {
                full_logs.InnerHtml = "No data to Display";
            }
        }

        private void load_batch_process_logs()
        {
            string query = "select * from E_LOGS where DB_ENV='" + ddl_env.SelectedValue + "' and o_date=date'" + EOD_date.Text + "' and L_TYPE='P'order by S_DATE asc";
            string ConString = "Data Source=(DESCRIPTION =" +
            "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.197)(PORT = 1522))" +
            "(CONNECT_DATA =" +
              "(SERVER = DEDICATED)" +
              "(SERVICE_NAME = FCPROD)));" +
              "User Id=APPLOGS;Password=A#9pLO7g$2o23;Connection Timeout=600; Max Pool Size=150;";
            DataTable dt = get_oracle_data(query, ConString);
            string result_tbl = "<table class='batch_tbl'><tr><td class='bt_th'>SR No</td><td class='bt_th'>Batch Code</td><td class='bt_th_2'>Batch Name</td><td class='bt_th_2'>Process Date Time</td></tr>";
            if (dt.Rows.Count>0)
            {
                for(int i=0;i<dt.Rows.Count;i++)
                {
                    string data = dt.Rows[i]["LOGSDATA"].ToString();
                    //Batch Code^16|Batch Category^ADHOC PURGE|Execution Time^2025-01-01 00:22:27
                    string[] m = data.Split('|');
                    string z = "";
                    for (int j = 0; j < m.Length; j++)
                    {
                        string[] l = m[j].Split('^');
                        z = z + "|" + l[1];
                    }
                    z = z.Substring(1);
                    string[] mx = z.Split('|');
                    result_tbl = result_tbl + "<tr><td class='bt_td_1'>" + (i + 1) + ".</td><td class='bt_td_1'>" + mx[0] + "</td><td class='bt_td_2'>" + mx[1] + "</td><td class='bt_td_2'>" + mx[2] + "</td></tr>";
                }
                section_88.InnerHtml = result_tbl+"</table>";
            }
            else
            {
                section_88.InnerHtml = "No Data to Display";
            }

        }

        private void load_batch_timing()
        {
            string data = "Nothing To Display:0:#4269e6";
            if (constring.Text != "")
            {               
                string query = " select \"Date Process\", \"Category Code\", \"Category Description\",\"Start Time\",\"End Time\",\"Time in Mins\",\"Time in hh: mi: ss \" " +
         " from (Select to_char(dat_process,'dd-Mon-yyyy') \"Date Process\", a.cod_proc_category \"Category Code\", b.txt_category \"Category Description\",  " +
           " to_char(min(dat_proc_start),'dd-Mon-yyyy hh24:mi:ss') \"Start Time\",  " +
           " to_char(max(dat_proc_end),'dd-Mon-yyyy hh24:mi:ss') \"End Time\",  " +
           " Trunc((max(dat_proc_end) - min(dat_proc_start))*24*60)  \"Time in Mins\",  " +
           " LPAD(trunc((max(dat_proc_end) - min(dat_proc_start)) * 24), 2, '0') || ':' ||  " +
           " LPAD((trunc((max(dat_proc_end) - min(dat_proc_start)) * 24 * 60) - trunc((max(dat_proc_end) - min(dat_proc_start)) * 24) * 60),2,'0') || ':' ||  " +
           " LPAD( round(((max(dat_proc_end) - min(dat_proc_start)) * 24 * 60 - trunc((max(dat_proc_end) - min(dat_proc_start)) * 24 * 60)) * 60,2),2,'0') \"Time in hh: mi: ss \"  " +
             " FROM fcrhost.ba_eod_history a, fcrhost.ba_eod_restart b  " +
             " WHERE a.dat_process between date'"+ EOD_date.Text + "' and date'"+ EOD_date.Text + "'  " +
             " and a.cod_proc_category = b.cod_proc_category " +
             " group by a.dat_process,a.cod_proc_category, b.txt_category)  " +
             " order by to_date(\"Start Time\",'dd-Mon-yyyy hh24:mi:ss') ";
                if (strString.Text.Trim() != "")
                {
                    string[] p = strString.Text.Split(',');
                    for (int j = 0; j < p.Length; j++)
                    {
                        string[] dm = p[j].Split(':');
                        query = query.Replace(dm[0], dm[1]);
                    }
                }
                DataTable dt = get_oracle_data(query, constring.Text);
                data = "";
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["Time in Mins"].ToString() != "0")
                        {
                            data = data + "," + dt.Rows[i]["Category Description"].ToString() + ":" + dt.Rows[i]["Time in Mins"].ToString() + ":#a92af6";
                        }
                    }
                }
                if (data != "")
                {
                    data = data.Substring(1);
                }
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "show_batch", "show_batch('" + data + "','Batch Execution time');", true);
        }


        private void load_batch_past_logs()
        {
            string data = "Nothing To Display:0";
            string batch = "";
            if (constring.Text != "")
            {
                string query = " select \"Date Process\", \"Category Code\", \"Category Description\",\"Start Time\",\"End Time\",\"Time in Mins\",\"Time in hh: mi: ss \" " +
         " from (Select to_char(dat_process,'dd-MM') \"Date Process\", a.cod_proc_category \"Category Code\", b.txt_category \"Category Description\",  " +
           " to_char(min(dat_proc_start),'dd-Mon-yyyy hh24:mi:ss') \"Start Time\",  " +
           " to_char(max(dat_proc_end),'dd-Mon-yyyy hh24:mi:ss') \"End Time\",  " +
           " Trunc((max(dat_proc_end) - min(dat_proc_start))*24*60)  \"Time in Mins\",  " +
           " LPAD(trunc((max(dat_proc_end) - min(dat_proc_start)) * 24), 2, '0') || ':' ||  " +
           " LPAD((trunc((max(dat_proc_end) - min(dat_proc_start)) * 24 * 60) - trunc((max(dat_proc_end) - min(dat_proc_start)) * 24) * 60),2,'0') || ':' ||  " +
           " LPAD( round(((max(dat_proc_end) - min(dat_proc_start)) * 24 * 60 - trunc((max(dat_proc_end) - min(dat_proc_start)) * 24 * 60)) * 60,2),2,'0') \"Time in hh: mi: ss \"  " +
             " FROM fcrhost.ba_eod_history a, fcrhost.ba_eod_restart b  " +
             " WHERE a.dat_process between date'" + EOD_date.Text + "'-15 and date'" + EOD_date.Text + "'  " +
             " and a.cod_proc_category = b.cod_proc_category and a.cod_proc_category=16" +
             " group by a.dat_process,a.cod_proc_category, b.txt_category)  " +
             " order by to_date(\"Start Time\",'dd-Mon-yyyy hh24:mi:ss') ";
                if (strString.Text.Trim() != "")
                {
                    string[] p = strString.Text.Split(',');
                    for (int j = 0; j < p.Length; j++)
                    {
                        string[] dm = p[j].Split(':');
                        query = query.Replace(dm[0], dm[1]);
                    }
                }
                DataTable dt = get_oracle_data(query, constring.Text);
                data = ""; //dt.Rows[0]["Category Description"].ToString()+":Time (In Min)";
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        batch = dt.Rows[i]["Category Description"].ToString();
                        if (dt.Rows[i]["Time in Mins"].ToString() != "0")
                        {
                            data = data + "," + dt.Rows[i]["Date Process"].ToString() + ":" + dt.Rows[i]["Time in Mins"].ToString() + ":#a92af6";
                        }
                    }
                }
                if (data != "")
                {
                    data = data.Substring(1);
                }
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "previous_logs", "previous_logs('" + data + "','Batch Execution time','" + batch + "','section_77');", true);
        }

        private void load_batch_past_logs_EOD()
        {
            string data = "Nothing To Display:0";
            string batch = "";
            if (constring.Text != "")
            {
                string query = " select \"Date Process\", \"Category Code\", \"Category Description\",\"Start Time\",\"End Time\",\"Time in Mins\",\"Time in hh: mi: ss \" " +
         " from (Select to_char(dat_process,'dd-MM') \"Date Process\", a.cod_proc_category \"Category Code\", b.txt_category \"Category Description\",  " +
           " to_char(min(dat_proc_start),'dd-Mon-yyyy hh24:mi:ss') \"Start Time\",  " +
           " to_char(max(dat_proc_end),'dd-Mon-yyyy hh24:mi:ss') \"End Time\",  " +
           " Trunc((max(dat_proc_end) - min(dat_proc_start))*24*60)  \"Time in Mins\",  " +
           " LPAD(trunc((max(dat_proc_end) - min(dat_proc_start)) * 24), 2, '0') || ':' ||  " +
           " LPAD((trunc((max(dat_proc_end) - min(dat_proc_start)) * 24 * 60) - trunc((max(dat_proc_end) - min(dat_proc_start)) * 24) * 60),2,'0') || ':' ||  " +
           " LPAD( round(((max(dat_proc_end) - min(dat_proc_start)) * 24 * 60 - trunc((max(dat_proc_end) - min(dat_proc_start)) * 24 * 60)) * 60,2),2,'0') \"Time in hh: mi: ss \"  " +
             " FROM fcrhost.ba_eod_history a, fcrhost.ba_eod_restart b  " +
             " WHERE a.dat_process between date'" + EOD_date.Text + "'-15 and date'" + EOD_date.Text + "'  " +
             " and a.cod_proc_category = b.cod_proc_category and a.cod_proc_category=1" +
             " group by a.dat_process,a.cod_proc_category, b.txt_category)  " +
             " order by to_date(\"Start Time\",'dd-Mon-yyyy hh24:mi:ss') ";
                if (strString.Text.Trim() != "")
                {
                    string[] p = strString.Text.Split(',');
                    for (int j = 0; j < p.Length; j++)
                    {
                        string[] dm = p[j].Split(':');
                        query = query.Replace(dm[0], dm[1]);
                    }
                }
                DataTable dt = get_oracle_data(query, constring.Text);
                data = ""; //dt.Rows[0]["Category Description"].ToString()+":Time (In Min)";
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        batch = dt.Rows[i]["Category Description"].ToString();
                        if (dt.Rows[i]["Time in Mins"].ToString() != "0")
                        {
                            data = data + "," + dt.Rows[i]["Date Process"].ToString() + ":" + dt.Rows[i]["Time in Mins"].ToString() + ":#a92af6";
                        }
                    }
                }
                if (data != "")
                {
                    data = data.Substring(1);
                }
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "previous_logs1", "previous_logs('" + data + "','Batch Execution time','" + batch + "','section_55');", true);
        }

        private void load_batch_past_logs_BOD()
        {
            string data = "Nothing To Display:0";
            string batch = "";
            if (constring.Text != "")
            {
                string query = " select \"Date Process\", \"Category Code\", \"Category Description\",\"Start Time\",\"End Time\",\"Time in Mins\",\"Time in hh: mi: ss \" " +
         " from (Select to_char(dat_process,'dd-MM') \"Date Process\", a.cod_proc_category \"Category Code\", b.txt_category \"Category Description\",  " +
           " to_char(min(dat_proc_start),'dd-Mon-yyyy hh24:mi:ss') \"Start Time\",  " +
           " to_char(max(dat_proc_end),'dd-Mon-yyyy hh24:mi:ss') \"End Time\",  " +
           " Trunc((max(dat_proc_end) - min(dat_proc_start))*24*60)  \"Time in Mins\",  " +
           " LPAD(trunc((max(dat_proc_end) - min(dat_proc_start)) * 24), 2, '0') || ':' ||  " +
           " LPAD((trunc((max(dat_proc_end) - min(dat_proc_start)) * 24 * 60) - trunc((max(dat_proc_end) - min(dat_proc_start)) * 24) * 60),2,'0') || ':' ||  " +
           " LPAD( round(((max(dat_proc_end) - min(dat_proc_start)) * 24 * 60 - trunc((max(dat_proc_end) - min(dat_proc_start)) * 24 * 60)) * 60,2),2,'0') \"Time in hh: mi: ss \"  " +
             " FROM fcrhost.ba_eod_history a, fcrhost.ba_eod_restart b  " +
             " WHERE a.dat_process between date'" + EOD_date.Text + "'-15 and date'" + EOD_date.Text + "'  " +
             " and a.cod_proc_category = b.cod_proc_category and a.cod_proc_category=2" +
             " group by a.dat_process,a.cod_proc_category, b.txt_category)  " +
             " order by to_date(\"Start Time\",'dd-Mon-yyyy hh24:mi:ss') ";
                if (strString.Text.Trim() != "")
                {
                    string[] p = strString.Text.Split(',');
                    for (int j = 0; j < p.Length; j++)
                    {
                        string[] dm = p[j].Split(':');
                        query = query.Replace(dm[0], dm[1]);
                    }
                }
                DataTable dt = get_oracle_data(query, constring.Text);
                data = ""; //dt.Rows[0]["Category Description"].ToString()+":Time (In Min)";
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        batch = dt.Rows[i]["Category Description"].ToString();
                        if (dt.Rows[i]["Time in Mins"].ToString() != "0")
                        {
                            data = data + "," + dt.Rows[i]["Date Process"].ToString() + ":" + dt.Rows[i]["Time in Mins"].ToString() + ":#a92af6";
                        }
                    }
                }
                if (data != "")
                {
                    data = data.Substring(1);
                }
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "previous_logs2", "previous_logs('" + data + "','Batch Execution time','" + batch + "','section_44');", true);
        }

        private void display_eod_users()
        {
            string query = " with  users as (select distinct USERCODE,to_char(s_date,'YYYY-MM-DD') as s_date from E_logs where O_DATE=date'" + EOD_date.Text + "' and DB_ENV='" + ddl_env.SelectedValue.ToString() + "')  " +
 " select *  from E_logs a    inner join users b on b.USERCODE=a.usercode and to_char(a.s_date,'YYYY-MM-DD')=b.S_DATE   where a.L_type='U'";

            string ConString = "Data Source=(DESCRIPTION =" +
           "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.197)(PORT = 1522))" +
           "(CONNECT_DATA =" +
             "(SERVER = DEDICATED)" +
             "(SERVICE_NAME = FCPROD)));" +
             "User Id=APPLOGS;Password=A#9pLO7g$2o23;Connection Timeout=600; Max Pool Size=150;";
            DataTable dt = get_oracle_data(query, ConString);
            string datain = "No EOD logs are available";
            if(dt.Rows.Count>0)
            {
                //Code^ 11290|Name^Benen Lobo|Activity ^ User Logged In into the system|IPAddress ^ - 192.168.89.101 - 10.200.1.92 - ::1|LOG In date and Time^1/6/2025 10:00:02 PM
                List<string> sr = new List<string>();
                datain = "";
                for (int i=0;i<dt.Rows.Count;i++)
                {                   
                    string[] mr = dt.Rows[i]["LOGSDATA"].ToString().Split('|');
                    string hh = "";
                    bool hk = false;
                    for(int j=0;j<2;j++)
                    {
                        string[] jr = mr[j].Split('^');
                        hh = hh + "-" + jr[1];
                        if (sr.Contains(jr[1]) == true)
                        {
                            hk = true;
                        }
                        else
                        {
                            sr.Add(jr[1]);
                        }
                    }
                    if (hk == false)
                    {
                        if (hh != "")
                        {
                            datain = datain + hh.Substring(1) + "</br>";
                        }
                    }
                   
                }
               
            }
            user_log.InnerHtml = datain;

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
    }
}