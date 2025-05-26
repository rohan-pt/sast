using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BCCBExamPortal
{
    public partial class Swiftlimit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                sessionlbl.Text = Session["id"].ToString();
                dashboard_cal();
            }
            catch (Exception ep)
            {
                Response.Redirect("LogIn.aspx");
            }
           
        }

        protected void dashboard_cal()
        {
            string LC_limit = "";
            string PCL_Lim = "";
            string LC_Act_cust = "";
            string PCL_Act_cust = "";
            string curr_date = DateTime.Now.ToShortDateString();
            string on_PCL = "";
            string on_LC = "";
            string stat_PCL = "";
            string stat_LC = "";
            string connStr = "server=10.200.1.96;database=BCCB_CBR_DB;user id=bccb; password=bccb@123";
            string connStr2 = "server=10.200.1.100;database=BCCBPROD;user id=bccb; password=bccb@123";
            //string connStr2 = "server=10.200.1.96;database=BCCBREPORT;user id=bccbreport; password=bccb#123";
            string query = "select count(*) as LC_C from Swift_limits where Module='IMPLC'";
            DataTable df = get_datatable(query, connStr);
            if (df.Rows.Count > 0)
            {
                for (int i = 0; i < df.Rows.Count; i++)
                {
                    LC_Act_cust = df.Rows[i]["LC_C"].ToString();
                }
            }
            else
            {
                LC_Act_cust = "0";
            }
            query = "select count(*) as PCL_C from Swift_limits where (Module='AEBC' or Module='PCL')";
             df = get_datatable(query, connStr);
            if (df.Rows.Count > 0)
            {
                for (int i = 0; i < df.Rows.Count; i++)
                {
                    PCL_Act_cust = df.Rows[i]["PCL_C"].ToString();
                }
            }
            else
            {
                PCL_Act_cust = "0";
            }
            //select * from D520004 where Cast(LcIssueDt as date)='2016-06-16'
            query = "select count(*) as LC_count from D520004 where Cast(LcIssueDt as date)='"+ DateTime.Now.ToString("yyyy-MM-dd") + "'";
            df = get_datatable(query, connStr2);
            if (df.Rows.Count > 0)
            {
                for (int i = 0; i < df.Rows.Count; i++)
                {
                    on_LC = df.Rows[i]["LC_count"].ToString();
                }
            }
            else
            {
                on_LC = "0";
            }

            query = "select count(*) as PCL_count from D580005 where Cast(DisbDate as date)='" + DateTime.Now.ToString("yyyy-MM-dd") + "' ";
            df = get_datatable(query, connStr2);
            if (df.Rows.Count > 0)
            {
                for (int i = 0; i < df.Rows.Count; i++)
                {
                    on_PCL = df.Rows[i]["PCL_count"].ToString();
                }
            }
            else
            {
                on_PCL = "0";
            }
            // other 
            query = "select count(*) as LC_count from D520004 where Cast(LcIssueDt as date)='" + DateTime.Now.ToString("yyyy-MM-dd") + "'  and DbtrAddCk=''";
            df = get_datatable(query, connStr2);
            if (df.Rows.Count > 0)
            {
                for (int i = 0; i < df.Rows.Count; i++)
                {
                    stat_LC = "Unauthorize : - "+ df.Rows[i]["LC_count"].ToString()+" Authorize : - "+ (Convert.ToInt32(on_LC) -Convert.ToInt32(df.Rows[i]["LC_count"].ToString()));
                }
            }
            else
            {
                stat_LC = "Authorize : -" + on_LC;
            }

          

            query = "select count(*) as PCL_count from D580005 where Cast(DisbDate as date)='" + DateTime.Now.ToString("yyyy-MM-dd") + "'  and DbtrAddCk=''";
            df = get_datatable(query, connStr2);
            if (df.Rows.Count > 0)
            {
                for (int i = 0; i < df.Rows.Count; i++)
                {
                    stat_PCL = "Unauthorize : - " + df.Rows[i]["PCL_count"].ToString() + " Authorize : - " + (Convert.ToInt32(on_PCL) - Convert.ToInt32(df.Rows[i]["PCL_count"].ToString()));
                }
            }
            else
            {
                stat_PCL = "Authorize : -" + on_PCL;
            }

            query = "select count(*) as PCL_count from D580005 where Cast(DisbDate as date)='" + DateTime.Now.ToString("yyyy-MM-dd") + "'  and DbtrAddCk=''";
            df = get_datatable(query, connStr2);
            if (df.Rows.Count > 0)
            {
                for (int i = 0; i < df.Rows.Count; i++)
                {
                    stat_PCL = "Unauthorize : - " + df.Rows[i]["PCL_count"].ToString() + " Authorize : - " + (Convert.ToInt32(on_PCL) - Convert.ToInt32(df.Rows[i]["PCL_count"].ToString()));
                }
            }
            else
            {
                stat_PCL = "Authorize : -" + on_PCL;
            }



            //select Account_number,Account_name,cast(Sanction_limit AS float)-cast(Utilized_limit AS float) as Over_the_limit,Module from Swift_limits where cast(Sanction_limit AS float)-cast(Utilized_limit AS float)>0

            query = "select Account_number,Account_name,cast(Sanction_limit AS float)-cast(Utilized_limit AS float) as Over_the_limit,Module from Swift_limits where cast(Sanction_limit AS float)-cast(Utilized_limit AS float)<0";
            df = get_datatable(query, connStr);
            if (df.Rows.Count > 0)
            {
                string tbl_react_info = "<table class='moni_tbl'>";
                for (int i = 0; i < df.Rows.Count; i++)
                {
                    tbl_react_info = tbl_react_info + "<tr><td class='report_td'>" + df.Rows[i]["Account_number"].ToString() + "</td>";
                    tbl_react_info = tbl_react_info + "<td class='report_td_2'>" + df.Rows[i]["Account_name"].ToString() + "</td>";
                    tbl_react_info = tbl_react_info + "<td class='report_td'>Limit Exceeded by</td>";
                    tbl_react_info = tbl_react_info + "<td class='report_td_2'>" + df.Rows[i]["Over_the_limit"].ToString() + "</td></tr>";
                    if(df.Rows[i]["Module"].ToString()=="PCL" || df.Rows[i]["Module"].ToString() == "AEBC")
                    {
                        PCL_CNT_flg.InnerText = "Limit Exceeded";
                       
                        PCL_CNT_flg.Attributes.Add("class", "info_tbl_td_2");
                    }
                    if (df.Rows[i]["Module"].ToString() == "IMPLC")
                    {
                        LC_CNT_flg.InnerText = "Limit Exceeded";
                        LC_CNT_flg.Attributes.Add("class", "info_tbl_td_2");
                    }
                }
                tbl_react_info = tbl_react_info + "</table>";
                reaction_div.InnerHtml = tbl_react_info;
            }
            else
            {
                reaction_div.InnerHtml = "<table class='moni_tbl'><tr><td class='report_td'>No Limits Exceeded</td></tr></table>";
                LC_CNT_flg.InnerText = "In Control";
                PCL_CNT_flg.InnerText = "In Control";
                //info_tbl_td_2
                LC_CNT_flg.Attributes.Add("class", "info_tbl_td_1");
                PCL_CNT_flg.Attributes.Add("class", "info_tbl_td_1");
            }

            LC_act_con.InnerText = LC_Act_cust;
            PCL_act_con.InnerText = PCL_Act_cust;
            LC_cur_date.InnerText = curr_date;
            PCL_cur_date.InnerText = curr_date;
            LC_Ongoing_trn.InnerText = on_LC;
            PCL_Ongoing_trn.InnerText = on_PCL;
            LC_TR_Stat.InnerText = stat_LC;
            PCL_TR_Stat.InnerText = stat_PCL;
        }

        protected void btn_home1(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }

        protected void btn_LC_Click(object sender, EventArgs e)
        {          
            pcl_limit_trac_div.Visible = false;
            lc_limit_div.Visible = true;
            moni_div.Visible = false;
            btn_LC.Attributes.Add("Class", "btn_cls_act");
            btn_Limit_Monitoring.Attributes.Add("Class", "btn_cls");
            btn_PCL.Attributes.Add("Class", "btn_cls");
            //btn_cls_act
            ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "ShowProgressBar();", true);
            main_calculation2();
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "temp2", "HideProgressBar();", true);
        }
        protected void btn_PCL_Click(object sender, EventArgs e)
        {           
            pcl_limit_trac_div.Visible = true;          
            lc_limit_div.Visible = false;
            moni_div.Visible = false;
            btn_LC.Attributes.Add("Class", "btn_cls");
            btn_Limit_Monitoring.Attributes.Add("Class", "btn_cls");
            btn_PCL.Attributes.Add("Class", "btn_cls_act");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "ShowProgressBar();", true);
            main_calculation();
           
        }
        protected void btn_Moni_Click(object sender, EventArgs e)
        {
            btn_LC.Attributes.Add("Class", "btn_cls");
            btn_Limit_Monitoring.Attributes.Add("Class", "btn_cls_act");
            btn_PCL.Attributes.Add("Class", "btn_cls");
            lc_limit_div.Visible = false;
            pcl_limit_trac_div.Visible = false;
            moni_div.Visible = true;
        }
        protected void btn_LC_rec_search(object sender, EventArgs e)
        {
            lc_infor_div.Visible = true;
            lc_tab_div.Visible = false;

            string query = "select Account_name,RTRIM(SUBSTRING(Account_number,0,8))+'/'+Convert(varchar,Convert(int,SUBSTRING(Account_number,9,16))) as Account_number,Total_Sanction_limit,Sanction_limit,Utilized_limit,Expiry_dt from Swift_limits where (Module='IMPLC') and Account_number='" + prod_type.Value.PadRight(8, ' ') + prod_num.Value.PadLeft(16, '0') + "00000000' and Position!='3'";

            get_details_search(query,"LC");
        }

        protected void btn_LC_tbl_view(object sender, EventArgs e)
        {
            lc_infor_div.Visible = false;
            lc_tab_div.Visible = true;
            string query = "select Account_name,RTRIM(SUBSTRING(Account_number,0,8))+'/'+Convert(varchar,Convert(int,SUBSTRING(Account_number,9,16))) as Account_number,Total_Sanction_limit,Sanction_limit,Utilized_limit,Expiry_dt from Swift_limits where Module='IMPLC' and Position!='3'";

            get_details(query,"LC");
        }

        protected void btn_PCL_tbl_view(object sender, EventArgs e)
        {
            PCL_info_view.Visible = false;
            pcl_tbl.Visible = true;
            string query = "select Account_name,RTRIM(SUBSTRING(Account_number,0,8))+'/'+Convert(varchar,Convert(int,SUBSTRING(Account_number,9,16))) as Account_number,Total_Sanction_limit,Sanction_limit,Utilized_limit,Expiry_dt from Swift_limits where (Module='PCL' or Module='AEBC') and Position!='3'";

            get_details(query,"PCL");
        }
        protected void btn_pcl_rec_search(object sender, EventArgs e)
        {
            PCL_info_view.Visible = true;
            pcl_tbl.Visible = false;
            //string pcl_txt1 = "";
            string query = "select Account_name,RTRIM(SUBSTRING(Account_number,0,8))+'/'+Convert(varchar,Convert(int,SUBSTRING(Account_number,9,16))) as Account_number,Total_Sanction_limit,Sanction_limit,Utilized_limit,Expiry_dt from Swift_limits where (Module='PCL' or  Module='AEBC') and Account_number='" + ddl_pcl.SelectedItem.Text.PadRight(8,' ')+ pcl_txt2.Value.PadLeft(16, '0')+ "00000000' and Position!='3'";

            get_details_search(query,"PCL");
        }

        protected void get_details_search(string query,string type)
        {
           
            string connStr = "server=10.200.1.96;database=BCCB_CBR_DB;user id=bccb; password=bccb@123";
            string tbl = "";
            DataTable df = get_datatable(query, connStr);
            if (df.Rows.Count > 0)
            {
                for (int i = 0; i < df.Rows.Count; i++)
                {
                    tbl = tbl + "<div class='textbox_col_div'><label class='information_lbl_css'> <span style='color:red;'>Customer Name : </span>" + df.Rows[i]["Account_name"].ToString() + "</label></div>";
                    tbl = tbl + "<div class='textbox_col_div'><label class='information_lbl_css'> <span style='color:red;'>Account Number : </span>" + df.Rows[i]["Account_number"].ToString() + "</label></div>";
                    tbl = tbl + "<div class='textbox_col_div'><label class='information_lbl_css'> <span style='color:red;'>Total Limit : </span>" + df.Rows[i]["Total_Sanction_limit"].ToString() + "</label></div>";
                    tbl = tbl + "<div class='textbox_col_div'><label class='information_lbl_css'> <span style='color:red;'>Sanction Limit : </span>" + df.Rows[i]["Sanction_limit"].ToString() + "</label></div>";
                   
                   
                    
                    if ((Convert.ToDouble(df.Rows[i]["Sanction_limit"].ToString()) + Convert.ToDouble(df.Rows[i]["Utilized_limit"].ToString())) > 0)
                    {
                        tbl = tbl + "<div class='textbox_col_div'><label class='information_lbl_css'> <span style='color:red;'>Utilized Limit : </span> <span style='color:black;background:#7cf61d;'>" + df.Rows[i]["Utilized_limit"].ToString() + "</span></label></div>";

                    }
                    else
                    {
                        tbl = tbl + "<div class='textbox_col_div'><label class='information_lbl_css'> <span style='color:red;'>Utilized Limit : </span><span style='color:black;background:#fc1a13;'>" + df.Rows[i]["Utilized_limit"].ToString() + "</span></label></div>";

                    }
                    tbl = tbl + "<div class='textbox_col_div'><label class='information_lbl_css'> <span style='color:red;'>Available Limit : </span>" + (Convert.ToDouble(df.Rows[i]["Sanction_limit"].ToString()) + Convert.ToDouble(df.Rows[i]["Utilized_limit"].ToString())) + "</label></div>";

                    tbl = tbl + "<div class='textbox_col_div'><label class='information_lbl_css'> <span style='color:red;'>Customer Name : </span>" + df.Rows[i]["Expiry_dt"].ToString().Substring(0, 11) + "</label></div>";

                

                }
            }
            else
            {
                tbl = "No Data to Display";
            }
            if (type == "PCL")
            {
                PCL_info_view.InnerHtml = tbl;
            }
            else
            {
                lc_infor_div.InnerHtml = tbl;
            }
        }

        protected void get_details(string query,string type)
        {
            string tbl = "<table class='result_tbl'><tr><th class='tbl_th'>Customer Name</th><th class='tbl_th'>Account Number</th><th class='tbl_th'>Total Limit</th><th class='tbl_th'>Sanctioned Limit</th><th class='tbl_th'>Limit Used</th><th class='tbl_th'>Expiry Date</th></tr>";
            string connStr = "server=10.200.1.96;database=BCCB_CBR_DB;user id=bccb; password=bccb@123";          
            
            DataTable df = get_datatable(query, connStr);
            if (df.Rows.Count > 0)
            {
                for (int i = 0; i < df.Rows.Count; i++)
                {
                    tbl = tbl + "<tr>";
                    tbl = tbl + "<th class='tbl_td'>" + df.Rows[i]["Account_name"].ToString() + "</th>";
                    tbl = tbl + "<th class='tbl_td'>" + df.Rows[i]["Account_number"].ToString() + "</th>";
                    tbl = tbl + "<th class='tbl_td'>" + df.Rows[i]["Total_Sanction_limit"].ToString() + "</th>";
                    tbl = tbl + "<th class='tbl_td'>" + df.Rows[i]["Sanction_limit"].ToString() + "</th>";      
                    if((Convert.ToDouble(df.Rows[i]["Sanction_limit"].ToString())+ Convert.ToDouble(df.Rows[i]["Utilized_limit"].ToString()))>0)
                    {
                        tbl = tbl + "<th class='tbl_td'>" + df.Rows[i]["Utilized_limit"].ToString() + "</th>";
                    }
                    else
                    {
                        tbl = tbl + "<th class='tbl_td_lal'>" + df.Rows[i]["Utilized_limit"].ToString() + "</th>";
                    }
                  
                    tbl = tbl + "<th class='tbl_td'>" + df.Rows[i]["Expiry_dt"].ToString().Substring(0,11) + "</th>";
                    tbl = tbl + "</tr>";

                }
            }
            else
            {
                tbl = "No Data to Display";
            }
            if (type == "PCL")
            {
                pcl_tbl.InnerHtml = tbl + "</table>";
            }
            else
            {
                lc_tab_div.InnerHtml = tbl + "</table>";
            }


        }

        protected void main_calculation()
        {
            string new_entry = "0";
            new_customer("PCL",ref new_entry);
            new_customer("AEBC", ref new_entry);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "temp2", "HideProgressBar();", true);
        }
        protected void main_calculation2()
        {
            string new_entry = "0";
            new_customer("IMPLC", ref new_entry);
            //new_customer("AEBC", ref new_entry);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "temp2", "HideProgressBar();", true);
        }
        protected void new_customer(string type, ref string new_entry)
        {
            //dvProgressBar.Visible = true;
           
            string connStr = "server=10.200.1.96;database=BCCB_CBR_DB;user id=bccb; password=bccb@123";
            string connStr2 = "server=10.200.1.100;database=BCCBPROD;user id=bccb; password=bccb@123";
            //string connStr2 = "server=10.200.1.96;database=BCCBREPORT;user id=bccbreport; password=bccb#123";
            string query = "select Account_number from Swift_limits where Module='"+type+"'";
            DataTable df = get_datatable(query,connStr);
            string acc_no = "";

            query = "update Swift_limits set Position='3' where Module='" + type + "'";
                          
            int p1 = execute_query(connStr, query);

            if (df.Rows.Count > 0)
            {
                for (int i = 0; i < df.Rows.Count; i++)
                {
                    if(i==0)
                    {
                        acc_no = "'" + df.Rows[i]["Account_number"].ToString() + "'";
                    }
                    else
                    {
                        acc_no = acc_no + ",'" + df.Rows[i]["Account_number"].ToString() + "'";
                    }
                   
                }
            }
            else
            {
                acc_no = "''";
            }
           if(type == "AEBC")
            {
                acc_no = acc_no.Replace("AEBC", "PCL ");
                query = "select a.PrdAcctId as Acc_Num,a.LongName as Name,a.CustNo as Customer_Num,b.SancDate as SancDate,b.ExpDate as Expiry_Date,b.TotLimitSancF as Total_Sanction_Limit,c.LimitSanc as Sanction_limit,b.EffFromDate as Effective_Date,b.DbtrAddMk as Maker,b.DbtrAddCk as Checker,b.DbtrAddMd as Maker_date,b.DbtrAddCd as Checker_Date,(c.LimitSanc*-1) as LimitBlocked from D009022 a,D009063 b,D009163 c where a.PrdAcctId   like '%PCL%' and a.PrdAcctId not in (" + acc_no + ") and a.CustNo=b.CustNo and c.LitCode='" + type + "' and b.CustNo=c.CustNo";
            }
            else if(type == "PCL")
            {              
                query = "select a.PrdAcctId as Acc_Num,a.LongName as Name,a.CustNo as Customer_Num,b.SancDate as SancDate,b.ExpDate as Expiry_Date,b.TotLimitSancF as Total_Sanction_Limit,c.LimitSanc as Sanction_limit,b.EffFromDate as Effective_Date,b.DbtrAddMk as Maker,b.DbtrAddCk as Checker,b.DbtrAddMd as Maker_date,b.DbtrAddCd as Checker_Date,a.ShdClrBalFcy as LimitBlocked from D009022 a,D009063 b,D009163 c where a.PrdAcctId   like '%" + type + "%' and a.PrdAcctId not in (" + acc_no + ") and a.CustNo=b.CustNo and c.LitCode='" + type + "' and b.CustNo=c.CustNo";                
            }
            else
            {
                query = "select a.PrdAcctId as Acc_Num,a.LongName as Name,a.CustNo as Customer_Num,b.SancDate as SancDate,b.ExpDate as Expiry_Date,b.TotLimitSancNF as Total_Sanction_Limit,c.LimitSanc as Sanction_limit,b.EffFromDate as Effective_Date,b.DbtrAddMk as Maker,b.DbtrAddCk as Checker,b.DbtrAddMd as Maker_date,b.DbtrAddCd as Checker_Date,a.ShdClrBalFcy as LimitBlocked from D009022 a,D009063 b,D009163 c where a.PrdAcctId   like '%" + type + "%' and a.PrdAcctId not in (" + acc_no + ") and a.CustNo=b.CustNo and c.LitCode='" + type + "' and b.CustNo=c.CustNo";
            }
            DataTable dt = get_datatable(query, connStr2);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string account_num = dt.Rows[i]["Acc_Num"].ToString();
                    if(type == "AEBC")
                    {
                        account_num = account_num.Replace("PCL ", "AEBC");
                    }

                    query = "insert into Swift_limits (Account_number,Account_name,Total_Sanction_limit,Sanction_limit,Sanction_dt,Effective_dt,Expiry_dt,Utilized_limit,Position,Maker,Maker_Dt,Checker,Checker_Dt,Module) " +
                        " values('" + account_num + "','" + dt.Rows[i]["Name"].ToString().Trim() + "','" + dt.Rows[i]["Total_Sanction_Limit"].ToString() + "','" + dt.Rows[i]["Sanction_Limit"].ToString() + "','" + Convert.ToDateTime(dt.Rows[i]["SancDate"].ToString()) + "','" + Convert.ToDateTime(dt.Rows[i]["Effective_Date"].ToString()) + "','" + Convert.ToDateTime(dt.Rows[i]["Expiry_Date"].ToString()) + "','" + dt.Rows[i]["LimitBlocked"].ToString() + "','1','" + dt.Rows[i]["Maker"].ToString() + "','" + Convert.ToDateTime(dt.Rows[i]["Maker_date"].ToString()) + "','" + dt.Rows[i]["Checker"].ToString() + "','" + Convert.ToDateTime(dt.Rows[i]["Checker_date"].ToString()) + "','" + type + "')";
                    int p = execute_query(connStr,query);
                }

            }
            if (acc_no != "''")
            {
                if (type == "AEBC")
                {
                    acc_no = acc_no.Replace("AEBC", "PCL ");
                    query = "select a.PrdAcctId as Acc_Num,a.LongName as Name,a.CustNo as Customer_Num,b.SancDate as SancDate,b.ExpDate as Expiry_Date,b.TotLimitSancF as Total_Sanction_Limit,c.LimitSanc as Sanction_limit,b.EffFromDate as Effective_Date,b.DbtrAddMk as Maker,b.DbtrAddCk as Checker,b.DbtrAddMd as Maker_date,b.DbtrAddCd as Checker_Date,(c.LimitSanc*-1) as LimitBlocked from D009022 a,D009063 b,D009163 c where a.PrdAcctId   like '%PCL%' and a.PrdAcctId  in (" + acc_no + ") and a.CustNo=b.CustNo and c.LitCode='" + type + "' and b.CustNo=c.CustNo";
                }
                else if (type == "PCL")
                {
                    query = "select a.PrdAcctId as Acc_Num,a.LongName as Name,a.CustNo as Customer_Num,b.SancDate as SancDate,b.ExpDate as Expiry_Date,b.TotLimitSancF as Total_Sanction_Limit,c.LimitSanc as Sanction_limit,b.EffFromDate as Effective_Date,b.DbtrAddMk as Maker,b.DbtrAddCk as Checker,b.DbtrAddMd as Maker_date,b.DbtrAddCd as Checker_Date,a.ShdClrBalFcy as LimitBlocked from D009022 a,D009063 b,D009163 c where a.PrdAcctId   like '%" + type + "%' and a.PrdAcctId  in (" + acc_no + ") and a.CustNo=b.CustNo and c.LitCode='" + type + "' and b.CustNo=c.CustNo";
                }
                else
                {
                    query = "select a.PrdAcctId as Acc_Num,a.LongName as Name,a.CustNo as Customer_Num,b.SancDate as SancDate,b.ExpDate as Expiry_Date,b.TotLimitSancNF as Total_Sanction_Limit,c.LimitSanc as Sanction_limit,b.EffFromDate as Effective_Date,b.DbtrAddMk as Maker,b.DbtrAddCk as Checker,b.DbtrAddMd as Maker_date,b.DbtrAddCd as Checker_Date,a.ShdClrBalFcy as LimitBlocked from D009022 a,D009063 b,D009163 c where a.PrdAcctId   like '%" + type + "%' and a.PrdAcctId  in (" + acc_no + ") and a.CustNo=b.CustNo and c.LitCode='" + type + "' and b.CustNo=c.CustNo";
                }
                dt = get_datatable(query, connStr2);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string account_num = dt.Rows[i]["Acc_Num"].ToString();
                        if (type == "AEBC")
                        {
                            account_num = account_num.Replace("PCL ", "AEBC");
                        }

                        query = "update Swift_limits set Total_Sanction_limit='" + dt.Rows[i]["Total_Sanction_Limit"].ToString() + "',Sanction_limit='" + dt.Rows[i]["Sanction_Limit"].ToString() + "',Sanction_dt='" + Convert.ToDateTime(dt.Rows[i]["SancDate"].ToString()) + "',Effective_dt='" + Convert.ToDateTime(dt.Rows[i]["Effective_Date"].ToString()) + "', " +
                            "Expiry_dt='" + Convert.ToDateTime(dt.Rows[i]["Expiry_Date"].ToString()) + "',Utilized_limit='" + dt.Rows[i]["LimitBlocked"].ToString() + "',Position='2' where  Account_number='" + account_num + "' and Account_name='" + dt.Rows[i]["Name"].ToString().Trim() + "' and Module='" + type + "'";
                        int p = execute_query(connStr, query);
                    }

                }
            }


          

        }

        protected int execute_query(string conString,string Query)
        {
            SqlConnection con = new SqlConnection(conString);
            try
            {
                
                SqlCommand cmd1 = new SqlCommand(Query, con);
                con.Open();
                cmd1.ExecuteNonQuery();
                cmd1.Dispose();
                con.Close();

            }
            catch(Exception rt)
            {

            }
            finally
            {
                con.Close();
            }
            return 1;
        }


        protected DataTable get_datatable(string query, string conString)
        {
            DataTable dtx = new DataTable();
            try
            {
                SqlConnection cnn;
                cnn = new SqlConnection(conString);
                cnn.Open();
                SqlCommand sqlCommand = new SqlCommand(query, cnn);
                sqlCommand.CommandTimeout = 900;
                cnn.Close();
                SqlDataAdapter sda = new SqlDataAdapter(sqlCommand);
                sda.Fill(dtx);
                cnn.Close();
            }
            catch (Exception el)
            {
                //generate_fine_log("Error in Normal data fetching using get_datatable function", "Error", el.Message);
            }
            return dtx;

        }

    }
}