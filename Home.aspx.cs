using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Net;
using System.Data;
using BCCBExamPortal.Models;
using System.Web.UI.HtmlControls;
using Oracle.ManagedDataAccess.Client;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace BCCBExamPortal
{
    public partial class Home : System.Web.UI.Page
    {
      
        protected void Page_Load(object sender, EventArgs e)
        {
          
            try
            {
                string id = Session["id"].ToString();
                sessionlbl.Text= Session["id"].ToString();
            }
            catch (Exception ep)
            {
                Response.Redirect("LogIn.aspx");
            }
            if (!IsPostBack)
            {
                Load_drop_down();
                audit_trails();
                get_user();
                load_notification();
                CreateSelect();
                Select1.SelectedValue = DateTime.Now.Month.ToString();
                //srSelect1.SelectedValue = DateTime.Now.Month.ToString();
                fetch_target_date();
                //display_dash_graph1();
                load_activeTest();
                load_messages();
                load_EOD_status();

                //check_pending_request();
                //load_graph();
                //dt_ship_lat_2.CustomFormat = "MM-yyyy";
                //dt_ship_lat_2.Format = DateTimePickerFormat.Custom;
                //dt_ship_lat_2.Value = DateTime.Now;

            }
            else
            {              
                check_pending_request();
                load_notification();
                load_activeTest();
                load_messages();
                //display_dash_graph1();
                load_EOD_status();
            }
          

        }

        protected void load_messages()
        {
            string query = "select * from Marquee_IntraBCCB";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            DataTable dt = get_normal_data(query, con);
            string design_div = "";
          


            query = "select * from GENERAL_REPORTS order by id desc fetch first 2 rows only";
            string ConString = "Data Source=(DESCRIPTION =" +
        "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.197)(PORT = 1522))" +
        "(CONNECT_DATA =" +
          "(SERVER = DEDICATED)" +
          "(SERVICE_NAME = FCPROD)));" +
          "User Id=APPLOGS;Password=A#9pLO7g$2o23;Connection Timeout=600; Max Pool Size=150;";

            DataTable dm = get_oracle_data(query, ConString);

            if (dm.Rows.Count > 0)
            {
                for (int i = 0; i < dm.Rows.Count; i++)
                {
                    design_div = design_div + "<div class='small_box'><div class='small_logo'> " +
"<img src='Resources/Vectors/lion.png' class='small_img_lg' /></div> " +
"<div class='small_info'>" + dm.Rows[i]["REPORT_NAME"].ToString() + " " +
"</div><div class='small_color'>New Report</div></div> ";
                }
            }

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    design_div = design_div + "<div class='small_box'><div class='small_logo'> " +
"<img src='Resources/Vectors/ivm.png' class='small_img_lg' /></div> " +
"<div class='small_info'>" + dt.Rows[i]["Line"].ToString() + " " +
"</div><div class='small_color'>Information</div></div> ";
                }
            }

            small_notice.InnerHtml = design_div;
        }

       protected void load_EOD_status()
        {
            string query = "select * from DATABASE_CONNECTION where EOD_CON='1'";
            string ConString = "Data Source=(DESCRIPTION =" +
         "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.197)(PORT = 1522))" +
         "(CONNECT_DATA =" +
           "(SERVER = DEDICATED)" +
           "(SERVICE_NAME = FCPROD)));" +
           "User Id=APPLOGS;Password=A#9pLO7g$2o23;Connection Timeout=600; Max Pool Size=150;";

            DataTable dt = get_oracle_data(query, ConString);

            query = "select * from EOD_QUERIES where Type='B'";
            DataTable dm= get_oracle_data(query, ConString);

            string result = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string Constring = dt.Rows[i]["CON_STRING"].ToString();
                string Con_Name = dt.Rows[i]["CON_NAME"].ToString();
                string[] p = dt.Rows[i]["SPLIT_STR"].ToString().Split(',');
                string op = "<div class='cub1'><div class='cub1_heading'>" + Con_Name + "</div>";

                string[] dtm = new string[3];

                for (int k = 0; k < dm.Rows.Count; k++)
                {
                    query = dm.Rows[k]["QUERY"].ToString();
                    for (int j = 0; j < p.Length; j++)
                    {
                        string[] dm1 = p[j].Split(':');
                        if (dm1.Length > 1)
                        {
                            query = query.Replace(dm1[0], dm1[1]);
                        }
                    }
                    DataTable dn = get_oracle_data(query, Constring);
                    if (dn.Rows.Count > 0)
                    {
                        dtm[k] = dn.Rows[0][0].ToString();
                    }
                    else
                    {
                        dtm[k] = "NA";
                    }
                }


                op = op + "<table class='cub1tbl'><tr> " +
"<td class='cub1td1'>System Running Date:</td><td class='cub1td2'>" + dtm[0] + "</td></tr> <tr><td class='cub1td1'>Last EOD Date:</td> " +
"<td class='cub1td2'>" + dtm[1] + "</td></tr><tr>";
                if (dtm[2] == "Completed")
                {
                    op = op + "<td class='cub1td1'>Last EOD Status:</td><td class='cub1td2'><span class='cub1spng'>" + dtm[2] + "</span></td> " +
    "</tr></table></div> ";
                }
                else
                {
                    op = op + "<td class='cub1td1'>Last EOD Status:</td><td class='cub1td2'><span class='cub1spng1'>" + dtm[2] + "</span></td> " +
  "</tr></table></div> ";
                }


                result = result + op;





            }

            eod_stuffs.InnerHtml = result;
        }

        protected void download_ans2(object sender, EventArgs e)
        {
            string query = " select * from Questiontbl where E_Id=" + hdn_view_test.Value + "";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            DataTable dt = get_normal_data(query, con);
            string textOutput = rem_test_id.InnerText + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine;

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    textOutput = textOutput + " " + (i + 1) + ". " + dt.Rows[i]["Question"].ToString()+ Environment.NewLine+ Environment.NewLine;
                    textOutput = textOutput + "A." + dt.Rows[i]["Op1"].ToString() + Environment.NewLine; 
                    textOutput = textOutput + "B." + dt.Rows[i]["Op2"].ToString() + Environment.NewLine;
                    textOutput = textOutput + "C." + dt.Rows[i]["Op3"].ToString() + Environment.NewLine;
                    textOutput = textOutput + "D." + dt.Rows[i]["Op4"].ToString() + Environment.NewLine + Environment.NewLine + Environment.NewLine;
                    textOutput = textOutput + "Correct Answer :" + dt.Rows[i]["Correct_Op"].ToString() + Environment.NewLine + Environment.NewLine + Environment.NewLine;
                }
            }
            createPDF(textOutput);

        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }

        protected void create_Text(string otText)
        {          
            Response.Clear();
            string FileName = "" + rem_test_id.InnerText + ".txt";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            Response.Charset = "";
            Response.ContentType = "text/plain";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            List<int> Size_grid = new List<int>();
            strwritter.Write(otText.ToString());
            strwritter.WriteLine();         
            Response.Write(strwritter.ToString());
            Response.End();
        }

        protected void createPDF(string opText)
        {
            Document pdfDoc = new Document(PageSize.A4, 10, 10, 10, 10);
          
            using (MemoryStream memorystream = new MemoryStream())
            {
                try
                {
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memorystream);
                    pdfDoc.Open();
                    Paragraph paragraph = new Paragraph(opText, new Font(Font.FontFamily.HELVETICA, 12));
                    paragraph.Alignment = Element.ALIGN_JUSTIFIED;
                    pdfDoc.Add(paragraph);

                    pdfDoc.Close();
                    byte[] bytes = memorystream.ToArray();
                    memorystream.Close();
                    Response.Clear();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("Content-Disposition", "attachment;filename=" + rem_test_id.InnerText + ".pdf");                 
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.BinaryWrite(bytes);
                    Response.Flush();
                    Response.End();
                }
                catch(DocumentException doc)
                {

                }
                catch (IOException doc1)
                {

                }
            }





          
            //Response.ContentType = "application/pdf";          
            //Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);

            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //StringWriter strwritter = new StringWriter();

            //HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            //Grid_view.RenderControl(htmltextwrtter);
            //StringReader sr = new StringReader(strwritter.ToString());
            //Rectangle rec = PageSize.A4;

            //Document pdfDoc = new Document(rec, 10f, 10f, 10f, 10f);
            //HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            //PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            //pdfDoc.Open();
            //htmlparser.Parse(sr);
            //pdfDoc.Close();
            //Response.Write(pdfDoc);
            //Response.End();
        }
        protected void CreateSelect()
        {            
        int currentYear = DateTime.Now.Year;
            for (int i = currentYear; i >= currentYear - 5; i--)
            {
                //yearSelect.Items.Add(i.ToString());
                select2.Items.Add(new System.Web.UI.WebControls.ListItem(i.ToString()));
                //srSelect2.Items.Add(new System.Web.UI.WebControls.ListItem(i.ToString()));
                //           < option value = "<%= i %>" ><%= i %></ option >

                //<% } %>
            }
        }

        protected void load_data_static(object sender, EventArgs e)
        {
            fetch_target_date();
        }

        protected void Select2_load_data_static(object sender, EventArgs e)
        {
            //display_dash_graph1();
        }
        protected void Select1_load_data_static(object sender, EventArgs e)
        {
            fetch_target_date();
            ScriptManager.RegisterStartupScript(this, GetType(), "showparam", "showparam();", true);
        }
        protected void casa_insert(object sender, EventArgs e)
        {
            string query = "";
            string T_code = "1001";
            string T_val = in_cat1.Value.Trim();
            if (cas_cat1.InnerText == "OK")
            {
                query = "insert into BRANCH_TARGET (branch,target_type,code,c_date,month,year,status_t,target_val) values('" + emp_loc_m.InnerText + "','" + T_code + "','" + sessionlbl.Text + "',sysdate,'" + Select1.SelectedItem.Text + "','" + select2.SelectedItem.Text + "','1','" + T_val + "')";
            }
            else
            {
                query = "update BRANCH_TARGET set e_code='" + sessionlbl.Text + "',e_date=sysdate,target_val='" + T_val + "' where branch='" + emp_loc_m.InnerText + "' and target_type='" + T_code + "' and month='" + Select1.SelectedItem.Text + "' and year='" + select2.SelectedItem.Text + "'";
            }
            insert_target(query);
            ScriptManager.RegisterStartupScript(this, GetType(), "showparam", "showparam();", true);          
        }

        protected void td_insert(object sender, EventArgs e)
        {
            string query = "";
            string T_code = "1002";
            string T_val = in_cat2.Value.Trim();
            if (cas_cat2.InnerText == "OK")
            {
                query = "insert into BRANCH_TARGET (branch,target_type,code,c_date,month,year,status_t,target_val) values('" + emp_loc_m.InnerText + "','" + T_code + "','" + sessionlbl.Text + "',sysdate,'" + Select1.SelectedItem.Text + "','" + select2.SelectedItem.Text + "','1','" + T_val + "')";
            }
            else
            {
                query = "update BRANCH_TARGET set e_code='" + sessionlbl.Text + "',e_date=sysdate,target_val='" + T_val + "' where branch='" + emp_loc_m.InnerText + "' and target_type='" + T_code + "' and month='" + Select1.SelectedItem.Text + "' and year='" + select2.SelectedItem.Text + "'";
            }
            insert_target(query);
            ScriptManager.RegisterStartupScript(this, GetType(), "showparam", "showparam();", true);
        }

        protected void ln_insert(object sender, EventArgs e)
        {
            string query = "";
            string T_code = "1003";
            string T_val = in_cat3.Value.Trim();
            if (cas_cat3.InnerText == "OK")
            {
                query = "insert into BRANCH_TARGET (branch,target_type,code,c_date,month,year,status_t,target_val) values('" + emp_loc_m.InnerText + "','" + T_code + "','" + sessionlbl.Text + "',sysdate,'" + Select1.SelectedItem.Text + "','" + select2.SelectedItem.Text + "','1','" + T_val + "')";
            }
            else
            {
                query = "update BRANCH_TARGET set e_code='" + sessionlbl.Text + "',e_date=sysdate,target_val='" + T_val + "' where branch='" + emp_loc_m.InnerText + "' and target_type='" + T_code + "' and month='" + Select1.SelectedItem.Text + "' and year='" + select2.SelectedItem.Text + "'";
            }
            insert_target(query);
            ScriptManager.RegisterStartupScript(this, GetType(), "showparam", "showparam();", true);
        }
        protected void insert_target(string query)
        {
            string ConString = "Data Source=(DESCRIPTION =" +
                    "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.197)(PORT = 1522))" +
                    "(CONNECT_DATA =" +
                      "(SERVER = DEDICATED)" +
                      "(SERVICE_NAME = FCPROD)));" +
                      "User Id=APPLOGS;Password=A#9pLO7g$2o23;Connection Timeout=600; Max Pool Size=150;";

            OracleConnection connection = new OracleConnection(ConString);
            OracleCommand command = new OracleCommand(query, connection);
            try
            {
                command.Connection.Open();
                command.ExecuteNonQuery();
                command.Connection.Close();
            }
            catch (Exception er)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('" + er.Message + "');", true);
            }
            finally
            {
                command.Connection.Close();
            }
            fetch_target_date();

        }


//        protected void display_dash_graph1()
//        {
//            string title = "Branch Performance";
//            string theme = "Account Opening for Month " + srSelect1.SelectedItem.Text;
//            string idv = "Year,Actual,Target";
//            string output = "";
//            string query = "select branch,target_type,code,e_code,c_date,e_date,month,year,status_t,target_val from BRANCH_TARGET where branch='" + emp_loc_m.InnerText + "' and year='" + srSelect2.SelectedItem.Text + "' and month='" + srSelect1.SelectedItem.Text + "'";
//            string ConString = "Data Source=(DESCRIPTION =" +
//            "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.197)(PORT = 1522))" +
//            "(CONNECT_DATA =" +
//              "(SERVER = DEDICATED)" +
//              "(SERVICE_NAME = FCPROD)));" +
//              "User Id=APPLOGS;Password=A#9pLO7g$2o23;Connection Timeout=600; Max Pool Size=150;";
//            DataTable dt = get_oracle_data(query, ConString);
//            string casa = "0", td = "0", ln = "0";
//            string casaAct = "0", tdAct = "0", ln_Act = "";

//            query = " with Account_details (Branch,AccountNumber,AccountName, " +
//" AccountOpeningDate,AccountClosingDate,AccountStatus,CustomerNumber,Type) " +
//" as ( select a.cod_cc_brn,a.cod_acct_no,a.COD_ACCT_TITLE " +
//" ,a.dat_acct_open,b.dat_txn_close,a.cod_acct_stat,a.cod_cust, " +
//" 'CH' as type  " +
//" from fcrhost.ch_acct_mast a " +
//" left join fcrhost.ch_acct_close_hist b on b.cod_acct_no=a.cod_acct_no " +
//" where  a.flg_mnt_status='A' " +
//" union " +
//" select d.cod_cc_brn,d.cod_acct_no||'-'||d.COD_DEP_NO,c.nam_cust_full, " +
//" d.DAT_DEP_DATE,d.DAT_MATURITY as dat_txn_close,d.COD_DEP_STAT,a.cod_cust, " +
//" 'TD' as type  " +
//" from fcrhost.td_acct_mast a " +
//" left join fcrhost.td_dep_mast d on a.cod_acct_no=d.cod_acct_no and d.flg_mnt_status='A' " +
//" left join fcrhost.ci_custmast c on c.cod_cust_id=a.cod_cust and c.flg_mnt_status='A' " +
//" INNER JOIN FCRHOST.TD_DEP_ADDL_DETAILS f ON a.cod_acct_no = f.cod_acct_no AND d.COD_DEP_NO = f.COD_DEP_NO " +
//" where  a.flg_mnt_status='A' and f.TYP_RENEWAL <> 'A' " +
//" union " +
//" select a.cod_cc_brn,a.cod_acct_no,c.nam_cust_full, " +
//" a.dat_acct_open,a.dat_acct_close as dat_txn_close,a.cod_acct_stat,a.cod_cust_id as cod_cust, " +
//" 'LN' as type from  " +
//" fcrhost.ln_acct_mast a " +
//" left join fcrhost.ci_custmast c on c.cod_cust_id=a.cod_cust_id and c.flg_mnt_status='A' " +
//" where  a.flg_mnt_status='A' and a.COD_CUST_ID NOT IN (1287175,1287060)) " +
//" select TYPE,count(ACCOUNTNUMBER) as Count1 from Account_details " +
//" where ACCOUNTOPENINGDATE between  trunc(to_date('" + srSelect1.SelectedItem.Text + "'||' '||" + srSelect2.SelectedItem.Text + ",'Month YYYY'),'MM') AND  last_day(add_months(trunc(to_date('" + srSelect1.SelectedItem.Text + "'||' '||" + srSelect2.SelectedItem.Text + ",'Month YYYY'),'MM'),0)) " +
//" and ACCOUNTSTATUS not in (1,5) ";
//            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
//          string brm = get_info("select Loc_Id from Locationtbl where Location='" + emp_loc_m.InnerText + "'");
//            if (brm!="2")
//            {
//                query = query + " and Branch=" + brm + "";
//            }

//            query = query +" group by TYPE";

//            ConString = "Data Source=(DESCRIPTION =" +
//            "(ADDRESS = (PROTOCOL = TCP)(HOST = fcobdxdb-scan)(PORT = 1522))" +
//            "(CONNECT_DATA =" +
//              "(SERVER = DEDICATED)" +
//              "(SERVICE_NAME = FCPROD)));" +
//              "User Id=pfms_read;Password=pFm$_Read$2023;Connection Timeout=600; Max Pool Size=150;";

//            DataTable dm = get_oracle_data(query, ConString);
//            tbl_cs_act.InnerText = "0";
//            tbl_td_act.InnerText = "0";
//            tbl_ln_act.InnerText = "0";
//            if (dm.Rows.Count > 0)
//            {
//                for (int i = 0; i < dm.Rows.Count; i++)
//                {
//                    if (dm.Rows[i]["TYPE"].ToString() == "CH")
//                    {
//                        casaAct = dm.Rows[i]["COUNT1"].ToString();
//                        tbl_cs_act.InnerText = casaAct;
//                    }

//                    if (dm.Rows[i]["TYPE"].ToString() == "TD")
//                    {
//                        tdAct = dm.Rows[i]["COUNT1"].ToString();
//                        tbl_td_act.InnerText = tdAct;
//                    }

//                    if (dm.Rows[i]["TYPE"].ToString() == "LN")
//                    {
//                        ln_Act = dm.Rows[i]["COUNT1"].ToString();
//                        tbl_ln_act.InnerText = ln_Act;

//                    }
//                }
//            }
//            tbl_sc_tar.InnerText = "0";
//            tbl_td_tar.InnerText = "0";
//            tbl_ln_tar.InnerText = "0";
//            if (dt.Rows.Count > 0)
//            {
//                for (int i = 0; i < dt.Rows.Count; i++)
//                {
//                    string px = dt.Rows[i]["target_type"].ToString();
//                    if (px == "1001")
//                    {
//                        if (dt.Rows[i]["target_val"].ToString().Trim() != "")
//                        {
//                            casa = dt.Rows[i]["target_val"].ToString().Trim();
//                            tbl_sc_tar.InnerText = casa;
//                        }

//                    }

//                    if (px == "1002")
//                    {
//                        if (dt.Rows[i]["target_val"].ToString().Trim() != "")
//                        {
//                            td = dt.Rows[i]["target_val"].ToString().Trim();
//                            tbl_td_tar.InnerText = td;
//                        }

//                    }

//                    if (px == "1003")
//                    {
//                        if (dt.Rows[i]["target_val"].ToString().Trim() != "")
//                        {
//                            ln = dt.Rows[i]["target_val"].ToString().Trim();
//                            tbl_ln_tar.InnerText = ln;
//                        }

//                    }

//                }
//            }
//            else
//            {
//                output = "Casa," + casaAct + ",0:Term Deposit," + tdAct + ",0:Loan," + ln_Act + ",0";
//            }
//            output = "Casa," + casaAct + "," + casa + ":Term Deposit," + tdAct + "," + td + ":Loan," + ln_Act + "," + ln + "";
//            System.Web.UI.HtmlControls.HtmlGenericControl secdiv = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
//            string cls = "Sec_1xx";
//            secdiv.ID = "Sec_1xx";
//            secdiv.Attributes.Add("class", cls);
//            columnchart_material.Controls.Clear();
//            columnchart_material.Controls.Add(secdiv);
          

//            ScriptManager.RegisterStartupScript(this, GetType(), "Grm888", "callfive('" + idv + "','" + theme + "','" + output + "','" + title + "','"+ cls + "');", true);
//        }

        protected void fetch_target_date()
        {
            stm_cat1.InnerHtml = "Not Set";
            stm_cat2.InnerHtml = "Not Set";
            stm_cat3.InnerHtml = "Not Set";
            stm_cat1.Attributes.Add("class", "act_tbl_cat_td_3_red");
            stm_cat2.Attributes.Add("class", "act_tbl_cat_td_3_red");
            stm_cat3.Attributes.Add("class", "act_tbl_cat_td_3_red");
            cas_cat1.InnerText = "OK";
            cas_cat2.InnerText = "OK";
            cas_cat3.InnerText = "OK";
            make_cat1.InnerHtml = "";
            make_cat2.InnerHtml = "";
            make_cat3.InnerHtml = "";
            edit_cat1.InnerHtml = "";
            edit_cat2.InnerHtml = "";
            edit_cat3.InnerHtml = "";
            in_cat1.Value = "";
            in_cat2.Value = "";
            in_cat3.Value = "";

            if (Select1.SelectedItem.Text != "")
            {

                string query = "select branch,target_type,code,e_code,c_date,e_date,month,year,status_t,target_val from BRANCH_TARGET where branch='" + emp_loc_m.InnerText + "' and year='" + select2.SelectedItem.Text + "' and month='" + Select1.SelectedItem.Text + "'";
                string ConString = "Data Source=(DESCRIPTION =" +
                "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.197)(PORT = 1522))" +
                "(CONNECT_DATA =" +
                  "(SERVER = DEDICATED)" +
                  "(SERVICE_NAME = FCPROD)));" +
                  "User Id=APPLOGS;Password=A#9pLO7g$2o23;Connection Timeout=600; Max Pool Size=150;";
                DataTable dt = get_oracle_data(query, ConString);
                mon_cat1.InnerHtml = Select1.SelectedItem.Text;
                mon_cat2.InnerHtml = Select1.SelectedItem.Text;
                mon_cat3.InnerHtml = Select1.SelectedItem.Text;
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string px = dt.Rows[i]["target_type"].ToString();
                        if (px == "1001")
                        {
                            in_cat1.Value = dt.Rows[i]["target_val"].ToString();
                            stm_cat1.InnerHtml = "Active";
                            stm_cat1.Attributes.Add("class", "act_tbl_cat_td_3_green");
                            make_cat1.InnerHtml = dt.Rows[i]["code"].ToString();
                            edit_cat1.InnerHtml = dt.Rows[i]["e_code"].ToString();
                            cas_cat1.InnerText = "EDIT";
                        }
                        else if (px == "1002")
                        {
                            in_cat2.Value = dt.Rows[i]["target_val"].ToString();
                            stm_cat2.InnerHtml = "Active";
                            stm_cat2.Attributes.Add("class", "act_tbl_cat_td_3_green");
                            make_cat2.InnerHtml = dt.Rows[i]["code"].ToString();
                            edit_cat2.InnerHtml = dt.Rows[i]["e_code"].ToString();
                            cas_cat2.InnerText = "EDIT";
                        }
                        else if (px == "1003")
                        {
                            in_cat3.Value = dt.Rows[i]["target_val"].ToString();
                            stm_cat3.InnerHtml = "Active";
                            stm_cat3.Attributes.Add("class", "act_tbl_cat_td_3_green");
                            make_cat3.InnerHtml = dt.Rows[i]["code"].ToString();
                            edit_cat3.InnerHtml = dt.Rows[i]["e_code"].ToString();
                            cas_cat3.InnerText = "EDIT";
                        }
                    }
                }


            }
            //display_dash_graph1();
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
            string loc_id = get_info("select Loc_Id from Locationtbl where Location='" + emp_loc_m.InnerText + "'");
            string ConString = "Data Source=(DESCRIPTION =" +
         "(ADDRESS = (PROTOCOL = TCP)(HOST = fcobdxdb-scan)(PORT = 1522))" +
         "(CONNECT_DATA =" +
           "(SERVER = DEDICATED)" +
           "(SERVICE_NAME = FCPROD)));" +
           "User Id=pfms_read;Password=pFm$_Read$2023;Connection Timeout=600; Max Pool Size=150;";

            //ConString = "Data Source=(DESCRIPTION =" +
            //   "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.175)(PORT = 1521))" +
            //   "(CONNECT_DATA =" +
            //     "(SERVER = DEDICATED)" +
            //     "(SERVICE_NAME = FCMIS)));" +
            //     "User Id=fcrhost_read;Password=fcmis_read$2023;Connection Timeout=600; Max Pool Size=150;";
            string query1 = "with Account_details (Branch,AccountNumber,AccountName, " +
"AccountOpeningDate,AccountClosingDate,AccountStatus,CustomerNumber, " +
"Type) " +
"as ( " +
"select a.cod_cc_brn,a.cod_acct_no,a.COD_ACCT_TITLE " +
",a.dat_acct_open,b.dat_txn_close,a.cod_acct_stat,a.cod_cust, " +
"'CH' as type  " +
"from fcrhost.ch_acct_mast a " +
"left join fcrhost.ch_acct_close_hist b on b.cod_acct_no=a.cod_acct_no " +
"where  a.flg_mnt_status='A' " +
"union " +
"select d.cod_cc_brn,d.cod_acct_no||'-'||d.COD_DEP_NO,c.nam_cust_full, " +
"d.DAT_DEP_DATE,d.DAT_MATURITY as dat_txn_close,d.COD_DEP_STAT,a.cod_cust, " +
"'TD' as type  " +
"from fcrhost.td_acct_mast a " +
"left join fcrhost.td_dep_mast d on a.cod_acct_no=d.cod_acct_no and d.flg_mnt_status='A' " +
"left join fcrhost.ci_custmast c on c.cod_cust_id=a.cod_cust and c.flg_mnt_status='A' " +
"where  a.flg_mnt_status='A' " +
"union " +
"select a.cod_cc_brn,a.cod_acct_no,c.nam_cust_full, " +
"a.dat_acct_open,a.dat_acct_close as dat_txn_close,a.cod_acct_stat,a.cod_cust_id as cod_cust, " +
"'LN' as type from  " +
"fcrhost.ln_acct_mast a " +
"left join fcrhost.ci_custmast c on c.cod_cust_id=a.cod_cust_id and c.flg_mnt_status='A' " +
"where  a.flg_mnt_status='A') " +
"select TYPE,count(ACCOUNTNUMBER) as Count1 from Account_details where ACCOUNTSTATUS not in (1,5) ";
            if (loc_id != "2")
            {
                query1 = query1 + "and BRANCH='" + loc_id + "' ";
            }
            query1 = query1 + " group by TYPE order by TYPE";
            DataTable dt = get_oracle_data(query1, ConString);
            string data = "";
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count == 1)
                {
                    string typ = dt.Rows[0]["TYPE"].ToString();
                    if (typ == "CH")
                    {
                        data = dt.Rows[0]["Count1"].ToString() + "-0-0";
                    }
                    if (typ == "LN")
                    {
                        data = "0-" + dt.Rows[0]["Count1"].ToString() + "-0";
                    }
                    if (typ == "TD")
                    {
                        data = "0-0-" + dt.Rows[0]["Count1"].ToString();
                    }

                }
                if (dt.Rows.Count == 2)
                {
                    string typ = dt.Rows[0]["TYPE"].ToString();
                    string typ2 = dt.Rows[1]["TYPE"].ToString();
                    if (typ == "CH" && typ2 == "LN")
                    {
                        data = dt.Rows[0]["Count1"].ToString() + "-" + dt.Rows[1]["Count1"].ToString() + "-0";
                    }
                    if (typ == "TD" && typ2 == "LN")
                    {
                        data = "0-" + dt.Rows[0]["Count1"].ToString() + "-" + dt.Rows[1]["Count1"].ToString() + "";
                    }
                    if (typ == "CH" && typ2 == "TD")
                    {
                        data = "" + dt.Rows[0]["Count1"].ToString() + "-0-" + dt.Rows[1]["Count1"].ToString();
                    }

                }
                else
                {
                    data = "" + dt.Rows[0]["Count1"].ToString() + "-" + dt.Rows[1]["Count1"].ToString() + "-" + dt.Rows[2]["Count1"].ToString();
                }


            }
            else
            {
                data = "0-0-0";
            }


            ScriptManager.RegisterStartupScript(this, GetType(), "bulb2", "callone('piechart','" + data + "','Active Accounts Count');", true);
            query1 = "with Account_details (Branch,AccountNumber,AccountName, " +
"AccountOpeningDate,AccountClosingDate,AccountStatus,CustomerNumber, " +
"Type) " +
"as ( " +
"select a.cod_cc_brn,a.cod_acct_no,a.COD_ACCT_TITLE " +
",a.dat_acct_open,b.dat_txn_close,a.cod_acct_stat,a.cod_cust, " +
"'CH' as type  " +
"from fcrhost.ch_acct_mast a " +
"left join fcrhost.ch_acct_close_hist b on b.cod_acct_no=a.cod_acct_no " +
"where  a.flg_mnt_status='A' " +
"union " +
"select d.cod_cc_brn,d.cod_acct_no||'-'||d.COD_DEP_NO,c.nam_cust_full, " +
"d.DAT_DEP_DATE,d.DAT_MATURITY as dat_txn_close,d.COD_DEP_STAT,a.cod_cust, " +
"'TD' as type  " +
"from fcrhost.td_acct_mast a " +
"left join fcrhost.td_dep_mast d on a.cod_acct_no=d.cod_acct_no and d.flg_mnt_status='A' " +
"left join fcrhost.ci_custmast c on c.cod_cust_id=a.cod_cust and c.flg_mnt_status='A' " +
"where  a.flg_mnt_status='A' " +
"union " +
"select a.cod_cc_brn,a.cod_acct_no,c.nam_cust_full, " +
"a.dat_acct_open,a.dat_acct_close as dat_txn_close,a.cod_acct_stat,a.cod_cust_id as cod_cust, " +
"'LN' as type from  " +
"fcrhost.ln_acct_mast a " +
"left join fcrhost.ci_custmast c on c.cod_cust_id=a.cod_cust_id and c.flg_mnt_status='A' " +
"where  a.flg_mnt_status='A') " +
"select TYPE,count(ACCOUNTNUMBER) as Count1 from Account_details where ACCOUNTOPENINGDATE between last_day(add_months(sysdate,-2))+1 and last_day(add_months(sysdate,-1)) and ACCOUNTSTATUS not in (1,5) ";
            if (loc_id != "2")
            {
                query1 = query1 + "and BRANCH='" + loc_id + "' ";
            }

            query1 = query1 + "group by TYPE order by TYPE";
            DataTable dn = get_oracle_data(query1, ConString);
            if (dn.Rows.Count > 0)
            {
                if (dn.Rows.Count == 1)
                {
                    string typ = dn.Rows[0]["TYPE"].ToString();
                    if (typ == "CH")
                    {
                        data = dn.Rows[0]["Count1"].ToString() + "-0-0";
                    }
                    if (typ == "LN")
                    {
                        data = "0-" + dn.Rows[0]["Count1"].ToString() + "-0";
                    }
                    if (typ == "TD")
                    {
                        data = "0-0-" + dn.Rows[0]["Count1"].ToString();
                    }

                }
                if (dn.Rows.Count == 2)
                {
                    string typ = dn.Rows[0]["TYPE"].ToString();
                    string typ2 = dn.Rows[1]["TYPE"].ToString();
                    if (typ == "CH" && typ2 == "LN")
                    {
                        data = dn.Rows[0]["Count1"].ToString() + "-" + dn.Rows[1]["Count1"].ToString() + "-0";
                    }
                    if (typ == "TD" && typ2 == "LN")
                    {
                        data = "0-" + dn.Rows[0]["Count1"].ToString() + "-" + dn.Rows[1]["Count1"].ToString() + "";
                    }
                    if (typ == "CH" && typ2 == "TD")
                    {
                        data = "" + dn.Rows[0]["Count1"].ToString() + "-0-" + dn.Rows[1]["Count1"].ToString();
                    }

                }
                else
                {
                    data = "" + dn.Rows[0]["Count1"].ToString() + "-" + dn.Rows[1]["Count1"].ToString() + "-" + dn.Rows[2]["Count1"].ToString();
                }
            }
            else
            {
                data = "0-0-0";
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "bulb3", "callone('piechart2','" + data + "','New Account Opened Last Month');", true);
        }

       
        //protected DataTable generate_notification()
        //{
        //    string query = "select * from reporting_notification where usercode='" + sessionlbl.Text + "' and n_stat='1'";
        //    string ConString = "Data Source=(DESCRIPTION =" +
        //      "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.197)(PORT = 1522))" +
        //      "(CONNECT_DATA =" +
        //        "(SERVER = DEDICATED)" +
        //        "(SERVICE_NAME = FCPROD)));" +
        //        "User Id=APPLOGS;Password=A#9pLO7g$2o23;Connection Timeout=600; Max Pool Size=150;";
        //    DataTable dt = get_oracle_data(query, ConString);
        //    DataTable datat = new DataTable();
        //    datat.Columns.Add("User_Code");
        //    datat.Columns.Add("Notification");
        //    datat.Columns.Add("Is_report");
        //    datat.Columns.Add("Link");
        //    datat.Columns.Add("RAN_ID");
        //    datat.Columns.Add("frequency");
        //    for (int i=0;i<dt.Rows.Count;i++)
        //    {
        //        string frequency = dt.Rows[i]["notification_frequency"].ToString();
        //        DateTime t1 = Convert.ToDateTime(dt.Rows[i]["NOTIFICATION_START_DATE"].ToString());

        //        if(frequency== "Daily")
        //        {
        //            if(t1<=System.DateTime.Now)
        //            {
        //                DataRow dtrow = datat.NewRow();
        //                dtrow["User_Code"] = dt.Rows[i]["USERCODE"].ToString();
        //                dtrow["Notification"] = dt.Rows[i]["NOTIFICATION_NAME"].ToString();
        //                dtrow["Is_report"] = dt.Rows[i]["IS_REPORT"].ToString();
        //                dtrow["Link"] = dt.Rows[i]["NOTIFICATION_LINK"].ToString();
        //                dtrow["RAN_ID"] = dt.Rows[i]["RAN_ID"].ToString();
        //                dtrow["frequency"] = dt.Rows[i]["notification_frequency"].ToString();
        //                datat.Rows.Add(dtrow);
        //            }
        //        }
        //        else if(frequency == "Weekly")
        //        {
        //            double t = (t1 - System.DateTime.Now).TotalDays;
        //            if (Convert.ToInt32(t) % 7==0)
        //            {
        //                DataRow dtrow = datat.NewRow();
        //                dtrow["User_Code"] = dt.Rows[i]["USERCODE"].ToString();
        //                dtrow["Notification"] = dt.Rows[i]["NOTIFICATION_NAME"].ToString();
        //                dtrow["Is_report"] = dt.Rows[i]["IS_REPORT"].ToString();
        //                dtrow["Link"] = dt.Rows[i]["NOTIFICATION_LINK"].ToString();
        //                dtrow["RAN_ID"] = dt.Rows[i]["RAN_ID"].ToString();
        //                dtrow["frequency"] = dt.Rows[i]["notification_frequency"].ToString();
        //                datat.Rows.Add(dtrow);
        //            }
        //        }
        //        else if (frequency == "Fortnightly")
        //        {
        //            double t = (t1 - System.DateTime.Now).TotalDays;
        //            if (Convert.ToInt32(t) % 15 == 0)
        //            {
        //                DataRow dtrow = datat.NewRow();
        //                dtrow["User_Code"] = dt.Rows[i]["USERCODE"].ToString();
        //                dtrow["Notification"] = dt.Rows[i]["NOTIFICATION_NAME"].ToString();
        //                dtrow["Is_report"] = dt.Rows[i]["IS_REPORT"].ToString();
        //                dtrow["Link"] = dt.Rows[i]["NOTIFICATION_LINK"].ToString();
        //                dtrow["RAN_ID"] = dt.Rows[i]["RAN_ID"].ToString();
        //                dtrow["frequency"] = dt.Rows[i]["notification_frequency"].ToString();
        //                datat.Rows.Add(dtrow);
        //            }
        //        }
        //        else if (frequency == "Monthly")
        //        {
        //            double t = (t1 - System.DateTime.Now).TotalDays;
        //            if (Convert.ToInt32(t) % 30 == 0)
        //            {
        //                DataRow dtrow = datat.NewRow();
        //                dtrow["User_Code"] = dt.Rows[i]["USERCODE"].ToString();
        //                dtrow["Notification"] = dt.Rows[i]["NOTIFICATION_NAME"].ToString();
        //                dtrow["Is_report"] = dt.Rows[i]["IS_REPORT"].ToString();
        //                dtrow["Link"] = dt.Rows[i]["NOTIFICATION_LINK"].ToString();
        //                dtrow["RAN_ID"] = dt.Rows[i]["RAN_ID"].ToString();
        //                dtrow["frequency"] = dt.Rows[i]["notification_frequency"].ToString();
        //                datat.Rows.Add(dtrow);
        //            }
        //        }
        //        else if (frequency == "Quarterly")
        //        {
        //            double t = (t1 - System.DateTime.Now).TotalDays;
        //            if (Convert.ToInt32(t) % 90 == 0)
        //            {
        //                DataRow dtrow = datat.NewRow();
        //                dtrow["User_Code"] = dt.Rows[i]["USERCODE"].ToString();
        //                dtrow["Notification"] = dt.Rows[i]["NOTIFICATION_NAME"].ToString();
        //                dtrow["Is_report"] = dt.Rows[i]["IS_REPORT"].ToString();
        //                dtrow["Link"] = dt.Rows[i]["NOTIFICATION_LINK"].ToString();
        //                dtrow["RAN_ID"] = dt.Rows[i]["RAN_ID"].ToString();
        //                dtrow["frequency"] = dt.Rows[i]["notification_frequency"].ToString();
        //                datat.Rows.Add(dtrow);
        //            }
        //        }
        //        else if (frequency == "Half Yearly")
        //        {
        //            double t = (t1 - System.DateTime.Now).TotalDays;
        //            if (Convert.ToInt32(t) % 180 == 0)
        //            {
        //                DataRow dtrow = datat.NewRow();
        //                dtrow["User_Code"] = dt.Rows[i]["USERCODE"].ToString();
        //                dtrow["Notification"] = dt.Rows[i]["NOTIFICATION_NAME"].ToString();
        //                dtrow["Is_report"] = dt.Rows[i]["IS_REPORT"].ToString();
        //                dtrow["Link"] = dt.Rows[i]["NOTIFICATION_LINK"].ToString();
        //                dtrow["RAN_ID"] = dt.Rows[i]["RAN_ID"].ToString();
        //                dtrow["frequency"] = dt.Rows[i]["notification_frequency"].ToString();
        //                datat.Rows.Add(dtrow);
        //            }
        //        }
        //        else if (frequency == "Yearly")
        //        {
        //            double t = (t1 - System.DateTime.Now).TotalDays;
        //            if (Convert.ToInt32(t) % 365 == 0)
        //            {
        //                DataRow dtrow = datat.NewRow();
        //                dtrow["User_Code"] = dt.Rows[i]["USERCODE"].ToString();
        //                dtrow["Notification"] = dt.Rows[i]["NOTIFICATION_NAME"].ToString();
        //                dtrow["Is_report"] = dt.Rows[i]["IS_REPORT"].ToString();
        //                dtrow["Link"] = dt.Rows[i]["NOTIFICATION_LINK"].ToString();
        //                dtrow["RAN_ID"] = dt.Rows[i]["RAN_ID"].ToString();
        //                dtrow["frequency"] = dt.Rows[i]["notification_frequency"].ToString();
        //                datat.Rows.Add(dtrow);
        //            }
        //        }
        //    }
        //    return datat;
        //}



        protected void check_pending_request()
        {
            auth_view.InnerHtml = "";
            notification_menu.Controls.Clear();
            //HtmlGenericControl div_ped = new HtmlGenericControl("div");
            //div_ped.Attributes.Add("class", "dry_div");
            //HtmlGenericControl dry_div2 = new HtmlGenericControl("div");
            //dry_div2.Attributes.Add("class", "dry_div2"); //dry_div2
            //dry_div2.InnerText = "Pending Stuffs and Notifications";
            //div_ped.Controls.Add(dry_div2);
            HtmlGenericControl div_ped2 = new HtmlGenericControl("div");
            div_ped2.Attributes.Add("class", "dry_div2");
            div_ped2.InnerText = "";
            string query = "Select S_No,Code,cast(R_Date as date) R_Date  from Employee_ChangeRequest where R_Sat='R'and Auth_Id='" + sessionlbl.Text + "'";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            DataTable dt = get_normal_data(query, con);
            //auth_view.Controls.Add(dry_div2);
            //auth_view.Controls.Add(div_ped);
            auth_view.Controls.Add(div_ped2);
            if(hdn_lastvisit.Value!="")
            {
                query = "select * from Daily_Reporting_Notifications where TO_CHAR(N_GEN_DATE,'yyyy-mm-dd') <=TO_CHAR(SYSDATE,'yyyy-mm-dd') and TO_CHAR(N_GEN_DATE,'yyyy-mm-dd') >TO_CHAR(SYSDATE-" + hdn_lastvisit.Value + ",'yyyy-mm-dd')  and user_code=" + sessionlbl.Text;
            }
            else
            {
                query = "select * from Daily_Reporting_Notifications where TO_CHAR(N_GEN_DATE,'yyyy-mm-dd') <=TO_CHAR(SYSDATE,'yyyy-mm-dd') and TO_CHAR(N_GEN_DATE,'yyyy-mm-dd') >=TO_CHAR(SYSDATE-7,'yyyy-mm-dd')  and user_code=" + sessionlbl.Text;
            }

            string ConString = "Data Source=(DESCRIPTION =" +
              "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.197)(PORT = 1522))" +
              "(CONNECT_DATA =" +
                "(SERVER = DEDICATED)" +
                "(SERVICE_NAME = FCPROD)));" +
                "User Id=APPLOGS;Password=A#9pLO7g$2o23;Connection Timeout=600; Max Pool Size=150;";
            DataTable dm = get_oracle_data(query, ConString);


            if (dt.Rows.Count > 0)
            {
                div_ped2.Visible = false;
                for (int i=0;i<dt.Rows.Count;i++)
                {
                    HtmlGenericControl wet_div = new HtmlGenericControl("div");
                    wet_div.Attributes.Add("class", "wet_div");
                    HtmlGenericControl wet_div2 = new HtmlGenericControl("div");
                    wet_div2.Attributes.Add("class", "wet_div2");
                    wet_div2.InnerText = "User Profile Change Request";

                    HtmlGenericControl wet_div3 = new HtmlGenericControl("div");
                    wet_div3.Attributes.Add("class", "wet_div3");
                    //wet_div3_1

                    HtmlGenericControl wet_div3_1 = new HtmlGenericControl("div");
                    wet_div3_1.Attributes.Add("class", "wet_div3_1");
                    wet_div3_1.InnerText = dt.Rows[i]["Code"].ToString();

                    HtmlGenericControl wet_div3_2 = new HtmlGenericControl("div");
                    wet_div3_2.Attributes.Add("class", "wet_div3_2");
                    wet_div3_2.InnerText = Convert.ToDateTime(dt.Rows[i]["R_Date"]).ToShortDateString();

                    HtmlGenericControl wet_div3_2_1 = new HtmlGenericControl("div");
                    wet_div3_2_1.Attributes.Add("class", "wet_div3_2");
                    Button btn = new Button();
                    btn.ID = dt.Rows[i]["S_No"].ToString() + "_" + i.ToString()+"_View";
                    btn.CssClass = "view_btn";
                    btn.Text = "View";
                    btn.Click += new EventHandler(view_customer_request);
                    wet_div3_2_1.Controls.Add(btn);

                    HtmlGenericControl wet_div3_2_2 = new HtmlGenericControl("div");
                    wet_div3_2_2.Attributes.Add("class", "wet_div3_2");
                    Button btn2 = new Button();
                    btn2.ID = dt.Rows[i]["S_No"].ToString() + "_" + i.ToString() + "_Reject";
                    btn2.CssClass = "rej_btn";
                    btn2.Text = "Reject";                    
                    btn2.Click += new EventHandler(reject_user_requset);
                    wet_div3_2_2.Controls.Add(btn2);

                    wet_div3.Controls.Add(wet_div3_1);
                    wet_div3.Controls.Add(wet_div3_2);
                    wet_div3.Controls.Add(wet_div3_2_1);
                    wet_div3.Controls.Add(wet_div3_2_2);

                    wet_div.Controls.Add(wet_div2);
                    wet_div.Controls.Add(wet_div3);

                    auth_view.Controls.Add(wet_div);
                }
            }
            else
            {
                HtmlGenericControl wet_div = new HtmlGenericControl("div");
                wet_div.Attributes.Add("class", "wet_div");
                HtmlGenericControl wet_div2 = new HtmlGenericControl("div");
                wet_div2.Attributes.Add("class", "wet_div2");
                wet_div2.InnerText = "No request";
                wet_div.Controls.Add(wet_div2);
                auth_view.Controls.Add(wet_div);
            }
            
            if (dm.Rows.Count > 0)
            {
                for (int i = 0; i < dm.Rows.Count; i++)
                {
                    //HtmlGenericControl wet_div = new HtmlGenericControl("div");
                    //wet_div.Attributes.Add("class", "wet_div_N");
                    //HtmlGenericControl wet_div2 = new HtmlGenericControl("div");
                    //wet_div2.Attributes.Add("class", "wet_div2");
                    //HtmlGenericControl wet_divg2 = new HtmlGenericControl("div");
                    //wet_divg2.Attributes.Add("class", "wet_div_mx"); //N_GEN_DATE
                    //wet_divg2.InnerHtml = "Date:- "+ Convert.ToDateTime(dm.Rows[i]["N_GEN_DATE"].ToString()).ToShortDateString();
                    //wet_div2.InnerHtml = "<b>Notification</b> :- " + dm.Rows[i]["Notification"].ToString(); 

                    //HtmlGenericControl wet_div3 = new HtmlGenericControl("div");
                    //wet_div3.Attributes.Add("class", "wet_div3_mx");
                    ////wet_div3_1

                    //HtmlGenericControl wet_div3_1 = new HtmlGenericControl("div");

                    //string p = "-";    

                    //HtmlGenericControl wet_div3_2_1 = new HtmlGenericControl("div");
                    //wet_div3_2_1.Attributes.Add("class", "wet_div3div4_11");

                    //if (dm.Rows[i]["Link"].ToString() != "")
                    //{
                    //    if (dm.Rows[i]["Is_report"].ToString() == "N")
                    //    {
                    //        p = dm.Rows[i]["Link"].ToString();
                    //        wet_div3_1.Attributes.Add("class", "wet_div3div4_1");
                    //        wet_div3_1.InnerText = p;
                    //        wet_div3.Controls.Add(wet_div3_1);
                    //    }
                    //    else
                    //    {
                    //        p = "<span style='color:red;float:left;'>Report Frequency : </span> " + dm.Rows[i]["frequency"].ToString();//frequency
                    //        wet_div3_1.Attributes.Add("class", "wet_div3div4_81");
                    //        wet_div3_1.InnerHtml = p;
                    //        wet_div3.Controls.Add(wet_div3_1);


                    //        Button btnm = new Button();
                    //        btnm.ID = dm.Rows[i]["RAN_ID"].ToString() + "_" + i.ToString() + "_View";
                    //        btnm.CssClass = "ok_btn_x";
                    //        btnm.Text = "View Report";
                    //        btnm.Attributes.Add("onclick", "replace('" + dm.Rows[i]["Link"].ToString().Replace('~','&') + "');");
                    //        wet_div3_2_1.Controls.Add(btnm);
                    //    }
                    //}


                    //wet_div3.Controls.Add(wet_div3_2_1);
                    //wet_div3.Controls.Add(wet_divg2);

                    //wet_div.Controls.Add(wet_div2);
                    //wet_div.Controls.Add(wet_div3);

                    //auth_view.Controls.Add(wet_div);
                    if (dm.Rows[i]["Is_report"].ToString() == "Y")
                    {                       
                        HtmlGenericControl notific_song = new HtmlGenericControl("div");//notific_song
                        notific_song.Attributes.Add("class", "notific_song");
                        string h = dm.Rows[i]["Link"].ToString();
                        notific_song.Attributes.Add("onclick", "replace('" + dm.Rows[i]["Link"].ToString().Replace('~', '&') + "');");
                        HtmlGenericControl image_flex_div = new HtmlGenericControl("div");//image_flex_div
                        image_flex_div.Attributes.Add("class", "image_flex_div");
                        HtmlGenericControl not_deta_con = new HtmlGenericControl("div");//not_deta_con
                        not_deta_con.Attributes.Add("class", "not_deta_con");
                        not_deta_con.Attributes.Add("style", "min-height:50px;");
                        HtmlGenericControl not_header = new HtmlGenericControl("div");//not_header
                        not_header.Attributes.Add("class", "not_header");
                        HtmlGenericControl not_header1 = new HtmlGenericControl("div");//not_header
                        not_header1.Attributes.Add("class", "not_header");
                        HtmlGenericControl not_header2 = new HtmlGenericControl("div");//not_header
                        not_header2.Attributes.Add("class", "not_header");

                        not_header.InnerText = dm.Rows[i]["Notification"].ToString();

                        HtmlGenericControl span_xf = new HtmlGenericControl("span");//span_xf
                        span_xf.Attributes.Add("class", "span_xf");
                        span_xf.InnerText = "Frequency: ";
                        not_header1.Controls.Add(span_xf);
                        HtmlGenericControl span_xf11 = new HtmlGenericControl("span");
                        span_xf11.InnerText= dm.Rows[i]["frequency"].ToString();
                        not_header1.Controls.Add(span_xf11);

                        HtmlGenericControl span_xf1 = new HtmlGenericControl("span");//span_xf
                        span_xf1.Attributes.Add("class", "span_xf");
                        span_xf1.InnerText = "Date: ";
                        not_header2.Controls.Add(span_xf1);
                        HtmlGenericControl span_xf22 = new HtmlGenericControl("span");
                        span_xf22.InnerText = Convert.ToDateTime(dm.Rows[i]["N_GEN_DATE"].ToString()).ToShortDateString();
                        not_header2.Controls.Add(span_xf22);
                        

                        HtmlGenericControl note_img = new HtmlGenericControl("img");//notific_song
                        note_img.Attributes.Add("class", "note_img");
                        note_img.Attributes.Add("src", "Resources/Vectors/lion.png");

                        image_flex_div.Controls.Add(note_img);
                        notific_song.Controls.Add(image_flex_div);
                        not_deta_con.Controls.Add(not_header);
                        not_deta_con.Controls.Add(not_header1);
                        not_deta_con.Controls.Add(not_header2);
                        notific_song.Controls.Add(not_deta_con);
                        notification_menu.Controls.Add(notific_song);
                    }
                    else
                    {
                        HtmlGenericControl notific_song = new HtmlGenericControl("div");//notific_song
                        notific_song.Attributes.Add("class", "notific_song");
                        notific_song.Attributes.Add("style", "height:150px;");                       
                        HtmlGenericControl notific_song_raja = new HtmlGenericControl("div");//notific_song_raja
                        notific_song_raja.Attributes.Add("class", "notific_song_raja");                        
                        HtmlGenericControl image_flex_div = new HtmlGenericControl("div");//image_flex_div
                        image_flex_div.Attributes.Add("class", "image_flex_div");
                        HtmlGenericControl not_deta_con = new HtmlGenericControl("div");//not_deta_con
                        not_deta_con.Attributes.Add("class", "not_deta_con");
                        not_deta_con.Attributes.Add("style", "min-height:50px;");
                        HtmlGenericControl not_header = new HtmlGenericControl("div");//not_header
                        not_header.Attributes.Add("class", "not_header");
                        HtmlGenericControl not_header1 = new HtmlGenericControl("div");//not_header
                        not_header1.Attributes.Add("class", "not_header");
                        HtmlGenericControl not_header2 = new HtmlGenericControl("div");//not_header
                        not_header2.Attributes.Add("class", "not_header");

                        not_header.InnerText = dm.Rows[i]["Notification"].ToString();

                        HtmlGenericControl span_xf = new HtmlGenericControl("span");//span_xf
                        span_xf.Attributes.Add("class", "span_xf");
                        span_xf.InnerText = "Frequency: ";
                        not_header1.Controls.Add(span_xf);
                        HtmlGenericControl span_xf11 = new HtmlGenericControl("span");
                        span_xf11.InnerText = dm.Rows[i]["frequency"].ToString();
                        not_header1.Controls.Add(span_xf11);

                        HtmlGenericControl span_xf1 = new HtmlGenericControl("span");//span_xf
                        span_xf1.Attributes.Add("class", "span_xf");
                        span_xf1.InnerText = "Date: ";
                        not_header2.Controls.Add(span_xf1);
                        HtmlGenericControl span_xf22 = new HtmlGenericControl("span");
                        span_xf22.InnerText = Convert.ToDateTime(dm.Rows[i]["N_GEN_DATE"].ToString()).ToShortDateString();
                        not_header2.Controls.Add(span_xf22);


                        HtmlGenericControl note_img = new HtmlGenericControl("img");//notific_song
                        note_img.Attributes.Add("class", "note_img");
                        note_img.Attributes.Add("src", "Resources/Vectors/Notification_img.jpg");

                        image_flex_div.Controls.Add(note_img);
                        notific_song_raja.Controls.Add(image_flex_div);
                        not_deta_con.Controls.Add(not_header);
                        not_deta_con.Controls.Add(not_header1);
                        not_deta_con.Controls.Add(not_header2);
                        notific_song_raja.Controls.Add(not_deta_con);

                        HtmlGenericControl not_header4 = new HtmlGenericControl("div");//not_header
                        not_header4.Attributes.Add("class", "not_header_nr");
                        not_header4.Attributes.Add("style", "overflow:hidden;");
                        not_header4.InnerText = dm.Rows[i]["Link"].ToString().Replace('~', '&');
                        notific_song.Controls.Add(notific_song_raja);
                        notific_song.Controls.Add(not_header4);
                        notification_menu.Controls.Add(notific_song);
                    }

                }
                spn_not_cnt.InnerText = dm.Rows.Count.ToString();
            }
            else
            {
                HtmlGenericControl notific_song = new HtmlGenericControl("div");//notific_song
                notific_song.Attributes.Add("class", "notific_song");
                notific_song.InnerText = "No Notification found!!!";
                notification_menu.Controls.Add(notific_song);
            }
            HtmlGenericControl arrow_up = new HtmlGenericControl("div");//notific_song
            arrow_up.Attributes.Add("class", "arrow-up");
            arrow_up.Attributes.Add("style", "margin-left:70%;");
            notification_menu.Controls.Add(arrow_up);

        }

        protected void approve_customer_request(object sender, EventArgs e)
        {
           

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            con.Open();
            try
            {
                SqlCommand cmd1 = new SqlCommand("UPDATE Employee_LoginTbl SET Employee_Name=@Employee_Name,Designation=@Designation,Email_Id=@Email_Id,Location=@Location,MobileNumber=@MobileNumber WHERE  Code=@Code ", con);
                string subtime = Convert.ToString(System.DateTime.Now);
                cmd1.Parameters.AddWithValue("@Code", code_old.InnerText);
                cmd1.Parameters.AddWithValue("@Employee_Name", name_old.InnerText);
                cmd1.Parameters.AddWithValue("@Designation", des_new.InnerText);
                cmd1.Parameters.AddWithValue("@Email_Id", email_new.InnerText);
                cmd1.Parameters.AddWithValue("@Location", loc_new.InnerText);
                cmd1.Parameters.AddWithValue("@MobileNumber", mob_new.InnerText);
                cmd1.ExecuteNonQuery();
                cmd1.Dispose();


                SqlCommand cmd2 = new SqlCommand("UPDATE Employee_ChangeRequest SET R_Sat=@R_Sat WHERE Code=@Code and R_Sat='R' ", con);               
                cmd2.Parameters.AddWithValue("@Code", code_old.InnerText);
                cmd2.Parameters.AddWithValue("@R_Sat", "A");
                cmd2.ExecuteNonQuery();
                cmd2.Dispose();
                lblerror2.InnerText = "Done.";
            }
            catch (Exception ep)
            {
                lblerror2.InnerText = "Problem In Network. Unable to Update.";
                ScriptManager.RegisterStartupScript(this, typeof(string), "script2", "showsettings();", true);
            }
            finally
            {
                con.Close();
            }
            get_user();
            ScriptManager.RegisterStartupScript(this, typeof(string), "m1", "callone('Done!!');", true);
        }

        protected void view_customer_request(object sender, EventArgs e)
        {
            Button senderButton = sender as Button;
            string id = senderButton.ID;
            string[] id_1 = id.Split('_');
            string query = "Select *  from Employee_ChangeRequest where R_Sat='R'and Auth_Id='" + sessionlbl.Text + "' and S_No='" + id_1[0] + "'";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            DataTable dt = get_normal_data(query, con);           
            if (dt.Rows.Count > 0)
            {               
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //Employee_Name,Designation,Email_Id,Location,MobileNumber
                    code_new.InnerText = dt.Rows[i]["Code"].ToString();
                    name_new.InnerText = dt.Rows[i]["Employee_Name"].ToString();
                    des_new.InnerText = dt.Rows[i]["Designation"].ToString();
                    loc_new.InnerText= dt.Rows[i]["Location"].ToString();
                    mob_new.InnerText= dt.Rows[i]["MobileNumber"].ToString();
                    email_new.InnerText= dt.Rows[i]["Email_Id"].ToString();
                }
            }

            query = "Select *  from Employee_LoginTbl where  Code='" + code_new.InnerText + "' ";
            dt = get_normal_data(query, con);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //Employee_Name,Designation,Email_Id,Location,MobileNumber
                    code_old.InnerText = dt.Rows[i]["Code"].ToString();
                    name_old.InnerText = dt.Rows[i]["Employee_Name"].ToString();
                    if(name_old.InnerText!= name_new.InnerText)
                    {
                        name_old.Attributes.Add("class", "tdx2");
                        name_new.Attributes.Add("class", "tdx2");
                    }
                    else
                    {
                        name_old.Attributes.Add("class", "tdx1");
                        name_new.Attributes.Add("class", "tdx1");
                    }
                    des_old.InnerText = dt.Rows[i]["Designation"].ToString();
                    if (des_old.InnerText != des_new.InnerText)
                    {
                        des_old.Attributes.Add("class", "tdx2");
                        des_new.Attributes.Add("class", "tdx2");
                    }
                    else
                    {
                        des_old.Attributes.Add("class", "tdx1");
                        des_new.Attributes.Add("class", "tdx1");
                    }
                    loc_old.InnerText = dt.Rows[i]["Location"].ToString();
                    if (loc_old.InnerText != loc_new.InnerText)
                    {
                        loc_old.Attributes.Add("class", "tdx2");
                        loc_new.Attributes.Add("class", "tdx2");
                    }
                    else
                    {
                        loc_old.Attributes.Add("class", "tdx1");
                        loc_new.Attributes.Add("class", "tdx1");
                    }
                    mob_old.InnerText = dt.Rows[i]["MobileNumber"].ToString();
                    if (mob_old.InnerText != mob_new.InnerText)
                    {
                        mob_old.Attributes.Add("class", "tdx2");
                        mob_new.Attributes.Add("class", "tdx2");
                    }
                    else
                    {
                        mob_old.Attributes.Add("class", "tdx1");
                        mob_new.Attributes.Add("class", "tdx1");
                    }
                    email_old.InnerText = dt.Rows[i]["Email_Id"].ToString();
                    if (email_old.InnerText != email_new.InnerText)
                    {
                        email_old.Attributes.Add("class", "tdx2");
                        email_new.Attributes.Add("class", "tdx2");
                    }
                    else
                    {
                        email_old.Attributes.Add("class", "tdx1");
                        email_new.Attributes.Add("class", "tdx1");
                    }
                }

                //show_view_data()
                ScriptManager.RegisterStartupScript(this, typeof(string), "script2_d", "show_view_data();", true);
            }
            check_pending_request();

        }

        protected void redirect(object sender, EventArgs e)
        {

        }
        protected void reject_user_requset(object sender, EventArgs e)
        {
         
            Button senderButton = sender as Button;
            string id = senderButton.ID;
            string[] id_1 = id.Split('_');

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            con.Open();
            try
            {
                SqlCommand cmd1 = new SqlCommand("UPDATE Employee_ChangeRequest SET R_Sat=@R_Sat WHERE Auth_Id=@Code and R_Sat='R' and S_No='" + id_1[0] + "' ", con);
                string subtime = Convert.ToString(System.DateTime.Now);
                cmd1.Parameters.AddWithValue("@Code", Session["id"].ToString());
                cmd1.Parameters.AddWithValue("@R_Sat", "T");
                cmd1.ExecuteNonQuery();
                cmd1.Dispose();
                lblerror2.InnerText = "Done.";
            }
            catch (Exception ep)
            {
                lblerror2.InnerText = "Problem In Network. Unable to Update Password.";
                ScriptManager.RegisterStartupScript(this, typeof(string), "script2", "showsettings();", true);
            }
            finally
            {
                con.Close();
            }
            check_pending_request();
            ScriptManager.RegisterStartupScript(this, typeof(string), "m1", "callone('Done!!');", true);
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

        protected void load_activeTest()
        {
            string query = " ;with question_tbl as (select distinct E_Id from Questiontbl), " +
" instruction_tbl as (select distinct E_ID from Test_Instruction_tbl), " +
" active_test as ( " +
" select  " +
" a.* " +
" from ExamNametbl a  " +
" inner join question_tbl b on  a.E_Id=b.E_Id " +
" inner join instruction_tbl c on c.E_ID=a.E_Id " +
" where Active=1) " +
" select distinct " +
"  a.TestName,a.E_Id,  " +
"  a.NumQue, a.start_time,a.end_time  , " +
"  cast(a.StartDate as date) as StartDate,   " +
"  cast(a.ClosingDate as date) as ClosingDate,  " +
"  b.Extra2  " +
"  from active_test a  " +
"  left join EmpExam_RecTbl b on a.E_Id=b.E_ID  and b.Employee_Code='" + sessionlbl.Text + "'   " +
"  where Active='1' ";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            DataTable dt = get_normal_data(query, con);
            tbl_test_act_rec.Rows.Clear();
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TableRow tr = new TableRow();

                    TableCell td = new TableCell();
                    td.CssClass = "td_sr";                   
                    td.Text = Convert.ToString(i + 1);

                    TableCell td1 = new TableCell();
                    td1.CssClass = "td_ename";                  
                    td1.Text = dt.Rows[i]["TestName"].ToString();


                    DateTime dt1 = Convert.ToDateTime(dt.Rows[i]["StartDate"].ToString());
                    DateTime dt2 = Convert.ToDateTime(dt.Rows[i]["ClosingDate"].ToString());
                    DateTime dt3 = System.DateTime.Now;
                    TableCell td4 = new TableCell();
                    td4.CssClass = "td_btn";                    
                    Button bt1 = new Button();
                    string[] timer_s = dt.Rows[i]["start_time"].ToString().Split(':');
                    string[] timer_e = dt.Rows[i]["end_time"].ToString().Split(':');
                    DateTime start_time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Convert.ToInt32(timer_s[0]), Convert.ToInt32(timer_s[1]), 0);
                    DateTime end_time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Convert.ToInt32(timer_e[0]), Convert.ToInt32(timer_e[1]), 0);
                    bt1.CssClass = "btn_start_test";
                    if (Convert.ToDateTime(dt3.ToShortDateString()) >= dt1 && Convert.ToDateTime(dt3.ToShortDateString()) <= dt2 && DateTime.Now>= start_time && DateTime.Now <= end_time)
                    {
                        if (dt.Rows[i]["Extra2"].ToString() == "1")
                        {
                            bt1.Text = "View Test";
                            bt1.Click += new EventHandler(view_test);
                        }
                        else if (Convert.ToDateTime(dt3.ToShortDateString()) > dt2)
                        {
                            bt1.Text = "View Test";
                            bt1.Click += new EventHandler(view_test);
                        }
                        else
                        {
                            bt1.Text = "Start Test";
                            bt1.CssClass = "btn_start_test_2";
                            bt1.Click += new EventHandler(start_test);
                        }
                    }
                    else if(Convert.ToDateTime(dt3.ToShortDateString()) >= dt1 && Convert.ToDateTime(dt3.ToShortDateString()) <= dt2 && DateTime.Now >= start_time && DateTime.Now > end_time)
                    {
                        bt1.Text = "View Test";
                        bt1.Click += new EventHandler(view_test);
                    }
                    else if (Convert.ToDateTime(dt3.ToShortDateString()) > dt1 && Convert.ToDateTime(dt3.ToShortDateString()) > dt2)
                    {
                        bt1.Text = "View Test";
                        bt1.Click += new EventHandler(view_test);
                    }
                    else
                    {
                        bt1.Text = "Coming Soon";
                        bt1.Click += new EventHandler(coming_soon);
                    }
                   
                    bt1.Attributes.Add("runat", "server");
                    //bt1.Attributes["type"] = "button";
                    bt1.ID = "Edit_" + dt.Rows[i]["E_Id"].ToString() + "_" + dt.Rows[i]["NumQue"].ToString();
                  
                    td4.Controls.Add(bt1); 

                    tr.Controls.Add(td);
                    tr.Controls.Add(td1);                  
                    tr.Controls.Add(td4);                  
                    tbl_test_act_rec.Rows.Add(tr);

                }

            }
            else
            {
                TableRow tr = new TableRow();
                TableCell td = new TableCell();
                td.CssClass = "td_sr";
                td.Text = "No Active Test Available!!";
                tr.Controls.Add(td);
                tbl_test_act_rec.Rows.Add(tr);
            }
        }


        protected void load_notification()
        {
            string query = "select * from reporting_notification where usercode='" + sessionlbl.Text + "' and n_stat='1'";
            string ConString = "Data Source=(DESCRIPTION =" +
              "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.197)(PORT = 1522))" +
              "(CONNECT_DATA =" +
                "(SERVER = DEDICATED)" +
                "(SERVICE_NAME = FCPROD)));" +
                "User Id=APPLOGS;Password=A#9pLO7g$2o23;Connection Timeout=600; Max Pool Size=150;";
            DataTable dt = get_oracle_data(query, ConString);
            if (dt.Rows.Count > 0)
            {
                ActiArchtbl.Rows.Clear();
                TableRow tRow = new TableRow();
                TableCell tb = new TableCell();
                tb.CssClass = "tab";
                tb.Style.Add("width", "5%");
                tb.Text = "<b>SR No</b>";

                TableCell tb3 = new TableCell();
                tb3.CssClass = "tab";
                tb3.Style.Add("width", "45%");
                tb3.Text = "<b>Notification</b>";

                TableCell tb4 = new TableCell();
                tb4.CssClass = "tab";
                tb4.Style.Add("width", "25%");
                tb4.Text = "<b>Frequency</b>";

                TableCell tb5 = new TableCell();
                tb5.CssClass = "tab";
                tb5.Style.Add("width", "10%");
                tb5.Text = "<b>Edit</b>";
                TableCell tb6 = new TableCell();
                tb6.CssClass = "tab";
                tb6.Style.Add("width", "10%");
                tb6.Text = "<b>Stop</b>";

                tRow.Controls.Add(tb);
                tRow.Controls.Add(tb3);
                tRow.Controls.Add(tb4);
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

                    TableCell td1 = new TableCell();
                    td1.CssClass = "celltab";
                    td1.Style.Add("width", "45%");
                    td1.Text = dt.Rows[i]["notification_name"].ToString();

                    TableCell td2 = new TableCell();
                    td2.CssClass = "celltab";
                    td2.Style.Add("width", "25%");
                    td2.Text = dt.Rows[i]["notification_frequency"].ToString(); ;

                    TableCell td4 = new TableCell();
                    td4.CssClass = "celltab";
                    td4.Style.Add("width", "10%");
                    Button bt1 = new Button();
                    bt1.Text = "Edit";
                    bt1.CssClass = "cellbtn";
                    bt1.Attributes.Add("runat", "server");
                    bt1.Attributes["type"] = "button";
                    bt1.ID = "Edit_" + dt.Rows[i]["ran_id"].ToString();
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
                    bt2.ID = "Del_" + dt.Rows[i]["ran_id"].ToString();
                    bt2.Click += new EventHandler(bt2_Click);
                    td5.Controls.Add(bt2);


                    tr.Controls.Add(td);
                    tr.Controls.Add(td1);
                    tr.Controls.Add(td2);
                    tr.Controls.Add(td4);
                    tr.Controls.Add(td5);
                    ActiArchtbl.Rows.Add(tr);

                }
            }
        }

        protected void coming_soon(object sender, EventArgs e)
        {
            Button b = sender as Button;
            string[] btnId = b.ID.Split('_');

            string query = "select * from ExamNametbl where E_Id=" + btnId[1].ToString() + "";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            DataTable dt = get_normal_data(query, con);
            if (dt.Rows.Count > 0)
            {
                Exam_name.InnerText = dt.Rows[0]["TestName"].ToString();
                exam_start_date.InnerHtml = "<span class='bold_stm'>Start Date        : </span>" + Convert.ToDateTime(dt.Rows[0]["StartDate"].ToString()).ToShortDateString();
                exam_end_date.InnerHtml = "<span class='bold_stm'>End Date         : </span>" + Convert.ToDateTime(dt.Rows[0]["ClosingDate"].ToString()).ToShortDateString();
                timing.InnerHtml = "<span class='bold_stm'>Exam Duration           : </span>" + dt.Rows[0]["Altime"].ToString()+" min";
                s_time.InnerHtml = "<span class='bold_stm'>Exam Available Time   : </span>" + dt.Rows[0]["start_time"].ToString() + " to " + dt.Rows[0]["end_time"].ToString();
                //e_time.InnerHtml = "<span class='bold_stm'>Exam Duration           : </span>" + dt.Rows[0]["end_time"].ToString() + " min";
                q_num.InnerHtml = "<span class='bold_stm'>Number of Question : </span>" + dt.Rows[0]["NumQue"].ToString();               
            }
            Exam_info.Visible = true;
            view_test_info.Visible = false;
            ScriptManager.RegisterStartupScript(this, GetType(), "bulb2", "callone('ex_pie','" + cor_ans_td.InnerText + "-" + wrong_ans_td.InnerText + "-" + no_att_td.InnerText + "','Exam Analysis');", true);
          
        }

        protected void view_test(object sender, EventArgs e)
        {
            string Exam_no = "";
            Button b = sender as Button;
            string[] btnId = b.ID.Split('_');

            string query = " select " +
" b.Employee_Code, " +
" a.E_Id, " +
" a.TestName, " +
" cast(a.StartDate as date) as StartDate, " +
" cast(a.ClosingDate as date) as ClosingDate, " +
" COUNT(case when(c.Extra1=2) then c.Extra1 end ) as Answered, " +
" COUNT(case when(c.Extra1=1) then c.Extra1 end ) as Not_Answered, " +
" COUNT(case when(c.Extra1=0) then c.Extra1 end ) as Not_Attempted, " +
" b.Extra1, " +
" b.Extra2, " +
" b.Exam_ID " +
" from ExamNametbl a  " +
" left join EmpExam_RecTbl b on a.E_Id=b.E_ID " +
" left join EmpExam_Responsetbl c on b.Exam_ID=c.Exam_ID " +
" where a.Active='1' and b.Employee_Code='" + Session["id"].ToString() + "' and a.E_Id='" + btnId[1].ToString() + "' " +
" group by b.Employee_Code, " +
" a.E_Id, " +
" a.TestName,a.StartDate,a.ClosingDate,b.Extra1, " +
" b.Extra2, " +
" b.Exam_ID ";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            DataTable dt = get_normal_data(query, con);
            download_ans.Visible = true;
            if (dt.Rows.Count > 0)
            {
                rem_test_id.InnerText = dt.Rows[0]["TestName"].ToString();
                no_of_q_td.InnerText = (Convert.ToDouble(dt.Rows[0]["Answered"].ToString()) + Convert.ToDouble(dt.Rows[0]["Not_Answered"].ToString()) + Convert.ToDouble(dt.Rows[0]["Not_Attempted"].ToString())).ToString();
                no_att_td.InnerText = (Convert.ToDouble(dt.Rows[0]["Not_Answered"].ToString()) + Convert.ToDouble(dt.Rows[0]["Not_Attempted"].ToString())).ToString();
                cor_ans_td.InnerText = Math.Round((Convert.ToDouble(dt.Rows[0]["Extra1"].ToString()) * Convert.ToDouble(no_of_q_td.InnerText) / 100),0).ToString();
                wrong_ans_td.InnerText = (Convert.ToDouble(no_of_q_td.InnerText) - Convert.ToDouble(cor_ans_td.InnerText) - Convert.ToDouble(no_att_td.InnerText)).ToString();
                per_obt.InnerText = dt.Rows[0]["Extra1"].ToString();
                DateTime dt1 = Convert.ToDateTime(dt.Rows[0]["StartDate"].ToString());
                DateTime dt2 = Convert.ToDateTime(dt.Rows[0]["ClosingDate"].ToString());
                DateTime dt3 = System.DateTime.Now;
                if(Convert.ToDateTime(dt3.ToShortDateString()) <= dt2 && Convert.ToDateTime(dt3.ToShortDateString()) >= dt1)
                {
                    download_ans.Visible = false;
                }
            }
            else
            {
                rem_test_id.InnerText = "Test is not attempted by the User.";
                no_of_q_td.InnerText = "0";
                no_att_td.InnerText = "0";
                cor_ans_td.InnerText = "0";
                wrong_ans_td.InnerText = "0";
                per_obt.InnerText = "0";
            }
            hdn_view_test.Value = btnId[1].ToString();
            Exam_info.Visible = false;
            view_test_info.Visible = true;          
            ScriptManager.RegisterStartupScript(this, GetType(), "bulb2", "callone('ex_pie','" + cor_ans_td.InnerText + "-" + wrong_ans_td.InnerText + "-" + no_att_td.InnerText + "','Exam Analysis');", true);
         
        }

        protected void start_test(object sender, EventArgs e)
        {
            string Exam_no = "";
            Button b = sender as Button;
            string[] btnId = b.ID.Split('_');
            string query = "select * from EmpExam_RecTbl where E_ID=" + btnId[1] + " and Employee_Code='" + sessionlbl.Text + "'";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            DataTable dt = get_normal_data(query, con);
            if(dt.Rows.Count==0)
            {
                try
                {
                    con.Open();
                    query = "insert into EmpExam_RecTbl (E_ID,Employee_Code,Num_que_attep,Num_que_Visited,Extra1,Extra2) values('" + btnId[1] + "','" + sessionlbl.Text + "','0','0','0','0')";
                    SqlCommand cmd1 = new SqlCommand(query, con);
                    cmd1.ExecuteNonQuery();
                    cmd1.Dispose();
                }
                catch(Exception rt)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "script1", "callone('" + rt.Message.ToString() + "');", true);
                }
                finally
                {
                    con.Close();
                }

            }
            else
            {
                Exam_no = dt.Rows[0]["Exam_ID"].ToString();
            }
            if(Exam_no=="")
            {
                query = "select * from EmpExam_RecTbl where E_ID=" + btnId[1] + " and Employee_Code='" + sessionlbl.Text + "'";
                DataTable dm = get_normal_data(query, con);
                Exam_no = dm.Rows[0]["Exam_ID"].ToString();
            }
            query = "select * from EmpExam_Responsetbl where Exam_ID='" + Exam_no + "'";
            DataTable dh = get_normal_data(query, con);
            int lf = 0;
            if (dh.Rows.Count==0)
            {
                int p = Convert.ToInt32(btnId[2]);
               
                for(int i=1;i<=p;i++)
                {
                    try
                    {
                        con.Open();
                        query = "insert into EmpExam_Responsetbl (Exam_ID,QNumber,Time_Stamp,EmpResponse,Extra1) values('" + Exam_no + "','" + i + "','0','X','0')";
                        SqlCommand cmd1 = new SqlCommand(query, con);
                        cmd1.ExecuteNonQuery();
                        cmd1.Dispose();
                    }
                    catch (Exception rt)
                    {
                        lf = 1;                       
                    }
                    finally
                    {
                        con.Close();
                    }

                }             

            }

            if (lf == 1)
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "script1", "callone('Getting Some issues in the Exam Creation! Please call Application Admin.');", true);
            }
            else
            {
                Session["E_No"] = btnId[1];
                Session["Exam_No"] = Exam_no;
                Response.Redirect("Testing.aspx");
            }

        }
        protected void bt1_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            string[] btnId = b.ID.Split('_');
            string query = "select * from reporting_notification where ran_id='" + btnId[1] + "_" + btnId[2] + "'";
            string ConString = "Data Source=(DESCRIPTION =" +
             "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.197)(PORT = 1522))" +
             "(CONNECT_DATA =" +
               "(SERVER = DEDICATED)" +
               "(SERVICE_NAME = FCPROD)));" +
               "User Id=APPLOGS;Password=A#9pLO7g$2o23;Connection Timeout=600; Max Pool Size=150;";
            DataTable dt = get_oracle_data(query, ConString);
            if (dt.Rows.Count > 0)
            {
                btn_notify.InnerText = "Edit Notify";
                hdn_edit.Value = dt.Rows[0]["ran_id"].ToString();
                area_not.Value = dt.Rows[0]["notification_name"].ToString();
                ddl_frequency.SelectedIndex = ddl_frequency.Items.IndexOf(ddl_frequency.Items.FindByText(dt.Rows[0]["notification_frequency"].ToString()));               
                DateTime fr = Convert.ToDateTime(dt.Rows[0]["NOTIFICATION_START_DATE"].ToString());
                from_date.Text= fr.ToString("yyyy-MM-dd");
                
                area_link.Value = dt.Rows[0]["NOTIFICATION_LINK"].ToString();
                if(dt.Rows[0]["IS_REPORT"].ToString()=="Y")
                {
                    area_link.Disabled = true;
                }
                else
                {
                    area_link.Disabled = false;
                }
                if(dt.Rows[0]["IS_EMAIL"].ToString()=="Y")
                {
                    chk_email.Checked = true;
                }
                else
                {
                    chk_email.Checked = false;
                }          
              
            }
            else
            {
                btn_notify.InnerText = "Notify Me";
                hdn_edit.Value = "0";
                area_link.Disabled = false;
            }
            ScriptManager.RegisterStartupScript(this, typeof(string), "script299", "showsettingsx2();", true);
        }

        protected void get_user()
        {
            string query = "select * from Employee_LoginTbl where Code='" + sessionlbl.Text + "'";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            DataTable dt = get_normal_data(query, con);
            if (dt.Rows.Count > 0)
            {
                emp_mob_m.InnerText = dt.Rows[0]["MobileNumber"].ToString();
                emp_email_m.InnerText = dt.Rows[0]["Email_Id"].ToString();
                emp_des_m.InnerText = dt.Rows[0]["Designation"].ToString();
                emp_loc_m.InnerText = dt.Rows[0]["Location"].ToString();
                emp_code_m.InnerText = dt.Rows[0]["Code"].ToString();
                emp_Name_m.InnerText = dt.Rows[0]["Employee_Name"].ToString();

                code_r_in.Value = dt.Rows[0]["Code"].ToString(); ;
                name_r_in.Value = dt.Rows[0]["Employee_Name"].ToString();
                email_r_in.Value = dt.Rows[0]["Email_Id"].ToString();
                mobile_r_in.Value = dt.Rows[0]["MobileNumber"].ToString();

                ddllocation.SelectedItem.Text = dt.Rows[0]["Location"].ToString();
                ddl_designation.SelectedItem.Text = dt.Rows[0]["Designation"].ToString();
                Load_dept_emp();
              
            }
            else
            {
                emp_mob_m.InnerText = "";
                emp_email_m.InnerText = "";
                emp_des_m.InnerText = "";
                emp_loc_m.InnerText = "";
                emp_code_m.InnerText = "";
                emp_Name_m.InnerText = "";
            }
            query = " ;with visit_date(last_visit) as(select top 1 Visit_date_time  as Last_Visit_date from Audit_trails_Intra where Usercode=08560 and cast (Visit_date_time as date)!=cast(SYSDATETIME() as date) order by Visit_date_time desc) " +
" select DATEDIFF(day,last_visit,SYSDATETIME()) as p  from visit_date";
            DataTable dm = get_normal_data(query, con);
            if (dm.Rows.Count > 0)
            {
                hdn_lastvisit.Value = dm.Rows[0]["p"].ToString();
            }
            else
            {
                hdn_lastvisit.Value = "";
            }
            check_request();
            check_pending_request();           

        }
        protected void check_request()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            string query = "Select * from Employee_ChangeRequest where R_Sat='R'";
            DataTable dt = get_normal_data(query, con);
            if (dt.Rows.Count > 0)
            {
                btn_req.InnerText = "Edit Request";
            }
            else
            {
                btn_req.InnerText = "Request";
            }

        }


        protected void Load_drop_down()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            string query = "Select * from Locationtbl order by Location asc";
            DataTable dt = get_normal_data(query, con);
            ddllocation.Items.Clear();
            ddllocation.DataSource = dt;
            ddllocation.DataBind();
            ddllocation.DataTextField = "Location";
            ddllocation.DataValueField = "SRNO";
            ddllocation.DataBind();
            ddllocation.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Please Select Location--", "0"));

            query = "select SRNO,Designation from Employee_des order by Designation asc"; ;
            dt = get_normal_data(query, con);

            ddl_designation.Items.Clear();
            ddl_designation.DataSource = dt;
            ddl_designation.DataBind();
            ddl_designation.DataTextField = "Designation";
            ddl_designation.DataValueField = "SRNO";
            ddl_designation.DataBind();
            ddl_designation.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Please Select Designation--", "0"));
        }

        protected void cmbActivity_SelectedIndexChanged(object sender, EventArgs e)
        {
            Load_dept_emp();
            ScriptManager.RegisterStartupScript(this, typeof(string), "script1", "showsettings();", true);
        }


        protected void Load_dept_emp()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            string query = "select ROW_NUMBER() over (partition by Location order by Employee_Name ) as SRNO,Employee_Name from Employee_LoginTbl where Location='" + ddllocation.SelectedItem.Text + "' and  Ep2=1";
            DataTable dt = get_normal_data(query, con);
            ddlactAutho.Items.Clear();
            ddlactAutho.DataSource = dt;
            ddlactAutho.DataBind();
            ddlactAutho.DataTextField = "Employee_Name";
            ddlactAutho.DataValueField = "SRNO";
            ddlactAutho.DataBind();
            ddlactAutho.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Activity Authorization Employee--", "0"));
        }

        protected void insert_request_RR(object sender, EventArgs e)
        {
            string employee_code = code_r_in.Value;
            string employee_name = name_r_in.Value;
            string employee_email = email_r_in.Value;
            string employee_mob = mobile_r_in.Value;
            string employee_des = ddl_designation.SelectedItem.Text;
            string employee_loc = ddllocation.SelectedItem.Text;
            string employee_auth = ddlactAutho.SelectedItem.Text;

            if (employee_name != "" && employee_email != "" && employee_mob != "" && employee_des != "--Please Select Designation--" && employee_loc != "--Please Select Location--" && employee_auth != "--Activity Authorization Employee--")
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
                string query = "select Code from Employee_LoginTbl where Employee_Name='" + employee_auth + "'";
                DataTable dt = get_normal_data(query, con);
                employee_auth = dt.Rows[0]["Code"].ToString();
                if (btn_req.InnerText == "Request")
                {
                    request_to_table(employee_code, employee_name, employee_email, employee_mob, employee_des, employee_loc, employee_auth);
                    check_request();
                }
                else
                {
                    update_request(employee_code);
                    request_to_table(employee_code, employee_name, employee_email, employee_mob, employee_des, employee_loc, employee_auth);
                    check_request();
                }

            }
            check_pending_request();
            ScriptManager.RegisterStartupScript(this, typeof(string), "script2", "showsettings();", true);

        }

        protected void check_new_password(object sender, EventArgs e)
        {
            string new_password = new_passwd.Value;
            string con_password = con_new_passwd.Value;
            if(new_password== con_password && new_password!="" && con_password!="")
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
                con.Open();
                try
                {

                    SqlCommand cmd1 = new SqlCommand("UPDATE [Employee_LoginTbl] SET [Passcode]=@NewPasscode WHERE ([Code]=@Code)", con);
                    string subtime = Convert.ToString(System.DateTime.Now);
                    cmd1.Parameters.AddWithValue("@Code", Session["id"].ToString());                  
                    cmd1.Parameters.AddWithValue("@NewPasscode", AesOperation.EncryptString(new_password, AesOperation.Key, AesOperation.IV));
                    cmd1.ExecuteNonQuery();
                    cmd1.Dispose();
                    lblerror2.InnerText = "Your Password is Correctly Updated.";
                    new_passwd.Value= "";
                    con_new_passwd.Value= "";

                }
                catch (Exception ep)
                {
                    lblerror2.InnerText = "Problem In Network. Unable to Update Password.";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "script2", "showsettings();", true);
                }
                finally
                {
                    con.Close();
                }
            }
            else
            {
                lblerror2.InnerText = "Error in input!!!";
            }
            ScriptManager.RegisterStartupScript(this, typeof(string), "script2", "showsettings();", true);
        }
        protected void update_request(string employee_code)
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            con.Open();
            try
            {
                string dtc = DateTime.Now.ToString("yyyy-MM-dd");
                DateTime dx = System.DateTime.Now;
                string query1 = "update Employee_ChangeRequest set R_Sat='E',C_Date=@C_Date where Code=@Code and  R_Sat='R' ";
                SqlCommand cmd1 = new SqlCommand(query1, con);
                cmd1.Parameters.AddWithValue("@Code", employee_code);
                cmd1.Parameters.AddWithValue("@C_Date", System.DateTime.Now);
                cmd1.ExecuteNonQuery();
                cmd1.Dispose();
            }
            catch (Exception rt)
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "script1", "callone('Error in Operation!!!');", true);
            }
            finally
            {
                con.Close();
            }
        }


        protected void Create_notification(object sender, EventArgs e)
        {

            if (from_date.Text.ToString() != "" && area_not.Value != "" && ddl_frequency.SelectedItem.ToString() != "--Notification Frequency--")
            {
                try
                {
                    string p = from_date.Text.ToString();
                    if (chk_email.Checked == true)
                    {
                        p = "Y";
                    }
                    else
                    {
                        p = "N";
                    }

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

                    string query = "insert into reporting_notification (usercode,n_date_create,notification_name,notification_start_date,notification_link,notification_frequency,is_report,n_edit_date,is_email,is_sms,n_stat,ran_id) " +
        "values('" + sessionlbl.Text + "', SYSDATE, '" + area_not.Value + "',DATE'" + from_date.Text.ToString() + "', '" + area_link.Value + "', '" + ddl_frequency.SelectedItem.ToString() + "', 'N', sysdate, '" + p + "', 'N', '1','" + ran + "')";
                    if (btn_notify.InnerText != "Notify Me")
                    {
                        query = "update reporting_notification set notification_name='" + area_not.Value + "',notification_start_date=DATE'" + from_date.Text.ToString() + "',notification_link= '" + area_link.Value + "',notification_frequency='" + ddl_frequency.SelectedItem.ToString() + "',n_edit_date=sysdate,is_email='" + p + "' where ran_id='" + hdn_edit.Value + "'";
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
                    load_notification();
                    chk_email.Checked = false;
                    area_not.Value = "";
                    from_date.Text = "";
                    ddl_frequency.SelectedValue = "0";
                    area_link.Value = "";
                    hdn_edit.Value = "0";
                    area_link.Disabled = false;
                    ScriptManager.RegisterStartupScript(this, typeof(string), "script2", "showsettingsx1('Notification Added / Updated.');", true);
                }
                catch(Exception er)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "script89", "callone('Error in Operation While Data Insert!!!');", true);
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "script201", "showsettingsx1('One or more Inputs is/are missing!!!!');", true);
            }
        }


        protected void bt2_ok_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            string[] btnId = b.ID.Split('_');
            string query = "update Daily_Reporting_Notifications set NOT_STAT='0' where RAN_ID = '" + btnId[0] + "_" + btnId[1] + "'";
            string ConString = "Data Source=(DESCRIPTION =" +
             "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.197)(PORT = 1522))" +
             "(CONNECT_DATA =" +
               "(SERVER = DEDICATED)" +
               "(SERVICE_NAME = FCPROD)));" +
               "User Id=APPLOGS;Password=A#9pLO7g$2o23;Connection Timeout=600; Max Pool Size=150;";
            OracleConnection connection = new OracleConnection(ConString);
            OracleCommand command = new OracleCommand(query, connection);
            command.Connection.Open();
            try
            {
                command.ExecuteNonQuery();
                command.Connection.Close();
                check_pending_request();
            }

            catch (Exception ep)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('Something went wrong!!!');", true);
            }
            finally
            {
                command.Connection.Close();
            }          

        }


        protected void bt2_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            string[] btnId = b.ID.Split('_');
            string query = "update reporting_notification set N_STAT='0' where RAN_ID = '" + btnId[1] + "_" + btnId[2] + "'";
            string ConString = "Data Source=(DESCRIPTION =" +
             "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.197)(PORT = 1522))" +
             "(CONNECT_DATA =" +
               "(SERVER = DEDICATED)" +
               "(SERVICE_NAME = FCPROD)));" +
               "User Id=APPLOGS;Password=A#9pLO7g$2o23;Connection Timeout=600; Max Pool Size=150;";
            OracleConnection connection = new OracleConnection(ConString);
            OracleCommand command = new OracleCommand(query, connection);
            command.Connection.Open();
            try
            {              
                command.ExecuteNonQuery();
                command.Connection.Close();
               
                load_notification(); 
            }

            catch (Exception ep)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('Something went wrong!!!');", true);
            }
            finally
            {
                command.Connection.Close();
            }

            ScriptManager.RegisterStartupScript(this, typeof(string), "script299", "showsettingsx2();", true);

        }
        protected void request_to_table(string employee_code, string employee_name, string employee_email, string employee_mob, string employee_des, string employee_loc, string employee_auth)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            con.Open();
            try
            {
                string dtc = DateTime.Now.ToString("yyyy-MM-dd");
                DateTime dx = System.DateTime.Now;
                string query1 = "insert into Employee_ChangeRequest (Code,Employee_Name,Designation,Email_Id,Location,MobileNumber,Auth_Id,R_Date,R_Sat)" +
                    "  values(@Code,@Employee_Name,@Designation,@Email_Id,@Location,@MobileNumber,@Auth_Id,@R_Date,@R_Sat)";
                SqlCommand cmd1 = new SqlCommand(query1, con);
                cmd1.Parameters.AddWithValue("@Code", employee_code);
                cmd1.Parameters.AddWithValue("@Employee_Name", employee_name);
                cmd1.Parameters.AddWithValue("@Designation", employee_des);
                cmd1.Parameters.AddWithValue("@Email_Id", employee_email);
                cmd1.Parameters.AddWithValue("@Location", employee_loc);
                cmd1.Parameters.AddWithValue("@MobileNumber", employee_mob);
                cmd1.Parameters.AddWithValue("@Auth_Id", employee_auth);
                cmd1.Parameters.AddWithValue("@R_Date", System.DateTime.Now);
                cmd1.Parameters.AddWithValue("@R_Sat", "R");
                cmd1.ExecuteNonQuery();
                cmd1.Dispose();
            }
            catch (Exception rt)
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "script1", "callone('Error in Operation!!!');", true);
            }
            finally
            {
                con.Close();
            }
          
        }



        protected void audit_trails()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            int flag = 0;
            con.Open();
            try
            {
                string strHostName = System.Net.Dns.GetHostName();
                //IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName()); <-- Obsolete
                IPHostEntry ipHostInfo = Dns.GetHostEntry(strHostName);

                IPAddress ipAddress = ipHostInfo.AddressList[0];

                SqlCommand cmd1 = new SqlCommand("insert into Audit_trails_Intra  (Usercode,Page_Name,Visit_date_time,Is_there,IP_Address) values(@Usercode,@Page_Name,@Visit_date_time,@Is_there,@IP_Address)", con);
                string subtime = Convert.ToString(System.DateTime.Now);
                cmd1.Parameters.AddWithValue("@Visit_date_time", System.DateTime.Now);
                cmd1.Parameters.AddWithValue("@Is_there", '1');
                cmd1.Parameters.AddWithValue("@Page_Name", "Home");
                cmd1.Parameters.AddWithValue("@IP_Address", ipAddress.ToString());
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


        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("MainDashBoard.aspx?Menu=0");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("EmpDashBoard.aspx");
        }

        protected void btnchangepassword_Click(object sender, EventArgs e)
        {
            Response.Redirect("ChangePassword.aspx");
        }

        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("LogIn.aspx");
        }

        protected void btnhelp_Click(object sender, EventArgs e)
        {
            Response.Redirect("HelpPage.aspx");
        }

        protected void btncircular_Click(object sender, EventArgs e)
        {
            string url = "http://10.150.1.172:8083/DashBoard/frmBranchView.aspx";
            Response.Write("<script type='text/javascript'>window.open('" + url + "');</script>");
        }

        protected void executive_report(object sender, EventArgs e)
        {
            Response.Redirect("Executive_reports.aspx");
        }

    }
}