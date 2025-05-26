using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Net;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using OfficeOpenXml;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.Text.RegularExpressions;

namespace BCCBExamPortal
{
    public partial class Investment : System.Web.UI.Page
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
            if (from_date.Text == "")
            {
                from_date.Text = DateTime.Today.ToString("yyyy-MM-dd");
            }

            get_user();
            if (!IsPostBack)
            {
                audit_trails();
             
            }
            if (hdnFileName.Value != null)
            {              
                FileUploadControl.Attributes["value"] = hdnFileName.Value;
            }
            string str = "";
            check_uploads(ref str);

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
        protected void get_user()
        {
            string query = "select * from Employee_LoginTbl where Code='" + sessionlbl.Text + "'";
            spn_usr.InnerText = sessionlbl.Text;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            DataTable dt = get_normal_data(query, con);
            if (dt.Rows.Count > 0)
            {
                spn_name.InnerText = dt.Rows[0]["Employee_Name"].ToString();
                spn_location.InnerText = dt.Rows[0]["Location"].ToString();
            }
            else
            {
                spn_name.InnerText = "NW Issue";
                spn_location.InnerText = "NW Issue";
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
                cmd1.Parameters.AddWithValue("@Page_Name", "SGL Recon");
                cmd1.Parameters.AddWithValue("@Usercode", Session["id"].ToString());
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
        protected void Export_to_PDF(object sender, EventArgs e)
        {
            string lev = "";
            if (hdn_level.Value == "1")
            {
                lev = "level_1";
            }
            else if (hdn_level.Value == "2")
            {
                lev = "level_2";
            }
            else if (hdn_level.Value == "3")
            {
                lev = "level_3";
            }
            Response.ContentType = "application/pdf";
            string FileName = "" +lev + "_" + DateTime.Now + ".pdf";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Grid_view.RenderControl(htmltextwrtter);
            StringReader sr = new StringReader(strwritter.ToString());
            Rectangle rec = PageSize.A0; 
            Document pdfDoc = new Document(rec, 10f, 10f, 10f, 10f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            htmlparser.Parse(sr);
            pdfDoc.Close();
            Response.Write(pdfDoc);
            Response.End();
        }

        protected void ExportToPDF2(object sender, EventArgs e)
        {
            string lev = "";
            Rectangle rec = PageSize.A0;
            float[] widths = new float[] { 20f, 40f, 40f, 20f, 20f, 30f, 40f };
            if (hdn_level.Value == "1")
            {
                lev = "level_1";
                widths = new float[] { 30f, 20f, 50f, 50f, 20f, 20f, 30f, 30f, 20f, 20f, 40f, 20f, 20f, 50f, 30f, 30f, 20f, 20f, 20f, 20f, 30f, 20f, 20f, 20f, 30f, 20f, 30f, 30f, 30f };
            }
            else if (hdn_level.Value == "2")
            {
                lev = "level_2";
                widths = new float[] { 40f, 50f, 30f, 20f, 20f, 30f, 40f, 50f, 50f };
                rec = PageSize.A1;
            }
            else if (hdn_level.Value == "3")
            {
                lev = "level_3";
                widths = new float[] { 20f, 40f, 40f, 20f, 20f, 30f, 40f };
                rec = PageSize.A2;
            }
            Document pdfDoc = new Document(rec, 5f, 5f, 5f, 0f);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            Font f = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 18, BaseColor.RED);
            Paragraph headerPara = new Paragraph("Bassein Catholic Co-Operative Bank ",f);
            headerPara.Alignment = Element.ALIGN_CENTER;
            pdfDoc.Add(new Paragraph(headerPara));
            pdfDoc.Add(new Paragraph(" "));
            pdfDoc.Add(new Paragraph(" "));
            Font f1 = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 14, BaseColor.GREEN);
            headerPara = new Paragraph("        SGL Reconcilation ",f1);
            headerPara.Alignment = Element.ALIGN_LEFT;
            pdfDoc.Add(new Paragraph(headerPara));

            headerPara = new Paragraph("        Output Level: " + hdn_level.Value);
            headerPara.Alignment = Element.ALIGN_LEFT;
            pdfDoc.Add(new Paragraph(headerPara));

            headerPara = new Paragraph("        User Code: " + Session["id"].ToString());
            headerPara.Alignment = Element.ALIGN_LEFT;
            pdfDoc.Add(new Paragraph(headerPara));


            headerPara = new Paragraph("        User Name: " + spn_name.InnerText);
            headerPara.Alignment = Element.ALIGN_LEFT;
            pdfDoc.Add(new Paragraph(headerPara));

            pdfDoc.Add(new Paragraph(" "));
            pdfDoc.Add(new Paragraph(" "));

            PdfPTable pdftable = new PdfPTable(Convert.ToInt32(col_val.Value));
            pdftable.SetWidths(widths);
            foreach (TableCell gridViewHeaderCell in Grid_view.HeaderRow.Cells)
            {
                Font font = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9);             
                PdfPCell pdfCell = new PdfPCell(new Phrase(gridViewHeaderCell.Text, font));
                pdfCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                //pdfCell.BackgroundColor = new BaseColor(Grid_view.HeaderStyle.BackColor);
                pdftable.AddCell(pdfCell);
            }
            foreach (GridViewRow gridViewRow in Grid_view.Rows)
            {
                foreach (TableCell gridViewCell in gridViewRow.Cells)
                {
                    Font font = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9);
                    PdfPCell pdfCell = new PdfPCell(new Phrase(gridViewCell.Text, font));
                    pdfCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    pdftable.AddCell(pdfCell);
                }
            }

          


            string FileName = "" + lev + "_" + DateTime.Now + ".pdf";
            pdfDoc.Add(pdftable);

            pdfDoc.Add(new Paragraph(" "));
            pdfDoc.Add(new Paragraph(" "));
            pdfDoc.Add(new Paragraph(" "));
            pdfDoc.Add(new Paragraph(" "));
            pdfDoc.Add(new Paragraph(" "));
            pdfDoc.Add(new Paragraph(" "));
            Font f2 = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 16, BaseColor.BLACK);
            headerPara = new Paragraph("                                                                  Chief Manager ", f2);
            headerPara.Alignment = Element.ALIGN_LEFT;
            pdfDoc.Add(new Paragraph(headerPara));

            pdfDoc.Close();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename="+ FileName + "");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Write(pdfDoc);
            Response.End();
        }


        public override void VerifyRenderingInServerForm(Control control)
        {
          
        }

        protected void check_recon_record(object sender, EventArgs e)
        {
            string str = "";
            grid_for_view.DataSource = null;
            grid_for_view.DataBind();
            Grid_view.DataSource = null;
            Grid_view.DataBind();
            export_to_excel8.Enabled = false;           
            export_to_pdf.Enabled = false;
            hdn_upload_stat.Value = "1";
            check_uploads(ref str);
        }

        protected void port_display(object sender, EventArgs e)
        {
            string query = "select * from portfolio where upload_date=date'" + from_date.Text + "' and port_stat='1'";
            string filename = "Portfolio File";
            hdn_upload_stat.Value = "2";
            fetch_uploadedrecord(query, filename);
        }

        protected void ekuber_display(object sender, EventArgs e)
        {
            string query = "select * from ekuber_st where upload_date=date'" + from_date.Text + "' and EKUBER_STAT='1'";
            string filename = "Ekuber File";
            hdn_upload_stat.Value = "3";
            fetch_uploadedrecord(query, filename);
        }

        protected void collateral_display(object sender, EventArgs e)
        {
            string query = "select * from COLLATERAL_TBL where upload_date=date'" + from_date.Text + "' and COLLAT_STAT='1'";
            string filename = "Collateral File";
            hdn_upload_stat.Value = "4";
            fetch_uploadedrecord(query, filename);
        }

        protected void level_btn_clk(object sender, EventArgs e)
        {
            Grid_view.DataSource = null;
            Grid_view.DataBind();
             DataTable dt= level_1_output();
            Grid_view.DataSource = dt;
            Grid_view.DataBind();
            export_to_excel8.Enabled = true;
            export_to_pdf.Enabled = true;
            ScriptManager.RegisterStartupScript(this, GetType(), "level1_cxxl", "process_click_after_btn(1);", true);
          
        }

        protected void leve2_btn_clk(object sender, EventArgs e)
        {
            Grid_view.DataSource = null;
            Grid_view.DataBind();
            DataTable dt = level_2_output();
            Grid_view.DataSource = dt;
            Grid_view.DataBind();
            export_to_excel8.Enabled = true;
            export_to_pdf.Enabled = true;
            ScriptManager.RegisterStartupScript(this, GetType(), "level1_cxxl", "process_click_after_btn(2);", true);
        }

        protected void leve3_btn_clk(object sender, EventArgs e)
        {
            Grid_view.DataSource = null;
            Grid_view.DataBind();
            DataTable dt = level_3_output();
            Grid_view.DataSource = dt;
            Grid_view.DataBind();
            export_to_excel8.Enabled = true;
            export_to_pdf.Enabled = true;
            ScriptManager.RegisterStartupScript(this, GetType(), "level1_cxxl", "process_click_after_btn(3);", true);
        }
        protected void fetch_uploadedrecord(string query,string filename)
        {
          
            string ConString = "Data Source=(DESCRIPTION =" +
              "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.197)(PORT = 1522))" +
              "(CONNECT_DATA =" +
                "(SERVER = DEDICATED)" +
                "(SERVICE_NAME = FCPROD)));" +
                "User Id=APPLOGS;Password=A#9pLO7g$2o23;Connection Timeout=600; Max Pool Size=150;";
            DataTable dt = get_oracle_data(query, ConString);
            grid_for_view.DataSource = null;
            grid_for_view.DataBind();
            summary_div.InnerHtml = "";
            if (dt.Rows.Count > 0)
            {
                string str = "<table class='summary_of_file_tbl'>";
                str = str + "<tr><td class='sam_td_left'>File Name:</td><td class='sam_td_right'>"+ filename + "</td>";
                str = str + "<td class='sam_td_left'>Upload Status:</td><td class='sam_td_right'>Records Found</td></tr>";

                str = str + "<tr><td class='sam_td_left'>Total Record:</td><td class='sam_td_right'>" + dt.Rows.Count + "</td>";
                str = str + "<td class='sam_td_left'>File Type:</td><td class='sam_td_right'>Daily Upload File</td></tr>";

                str = str + "<tr><td class='sam_td_left'>Upload Code:</td><td class='sam_td_right'>" + dt.Rows[0]["upload_code"] + "</td>";
                str = str + "<td class='sam_td_left'>Upload Date:</td><td class='sam_td_right'>" + dt.Rows[0]["upload_date"] + "</td></tr>";

                str = str + "<tr><td class='sam_td_left'>Edit Code:</td><td class='sam_td_right'>" + dt.Rows[0]["edit_code"] + "</td>";
                str = str + "<td class='sam_td_left'>Edit Date:</td><td class='sam_td_right'>" + dt.Rows[0]["EDIT_DATE"] + "</td></tr>";

                summary_div.InnerHtml = str + "</table>";
                export_exp_upload.Enabled = true;
            }
            else
            {
                string str = "<table class='summary_of_file_tbl'>";
                str = str + "<tr><td class='sam_td_left'>File Name:</td><td class='sam_td_right'>" + filename + "</td>";
                str = str + "<td class='sam_td_left'>Upload Status:</td><td class='sam_td_right'>Records Not Found</td></tr>";

                str = str + "<tr><td class='sam_td_left'>Total Record:</td><td class='sam_td_right'>0</td>";
                str = str + "<td class='sam_td_left'>File Type:</td><td class='sam_td_right'>Daily Upload File</td></tr>";

                str = str + "<tr><td class='sam_td_left'>Upload Code:</td><td class='sam_td_right'>No record Found</td>";
                str = str + "<td class='sam_td_left'>Upload Date:</td><td class='sam_td_right'>No Record Found</td></tr>";

                str = str + "<tr><td class='sam_td_left'>Edit Code:</td><td class='sam_td_right'></td>";
                str = str + "<td class='sam_td_left'>Edit Date:</td><td class='sam_td_right'></td></tr>";
                summary_div.InnerHtml = str + "</table>";
                export_exp_upload.Enabled = false;
            }

            grid_for_view.DataSource = dt;
            grid_for_view.DataBind();           
            ScriptManager.RegisterStartupScript(this, GetType(), "switch_tab", "switch_tab();", true);
        }

        private DataTable level_3_output()
        {
            string query = "with book_val(ISIN_book,Security_name_book,Value_book) as(select " +
" b.ISIN_NO,b.SECURITY_NAME,a.CURR_BAL " +
" from portfolio a left join isin_master b on a.name=b.SCRIP_DESC where a.UPLOAD_DATE=date'" + from_date.Text + "' and ISIN_STAT='1' and PORT_STAT='1'), " +
" level2_recon(ISIN_BOOK,SECURITY_NAME_BOOK,VALUE_BOOK,ISIN_MATCH,NAME_MATCH,DIFFERENCE,ISIN_CODE,NOMENCLATURE,QTY_OF_SECURITIES) " +
" as( " +
" select  distinct" +
" a.ISIN_BOOK, " +
" a.SECURITY_NAME_BOOK, " +
" a.VALUE_BOOK, " +
" case when (a.ISIN_BOOK=b.ISIN_CODE) then 'TRUE' else 'FALSE' end as ISIN_MATCH, " +
"  case when (a.SECURITY_NAME_BOOK=c.NOMENCLATURE) then 'TRUE' else 'FALSE' end as NAME_MATCH, " +
"   (a.VALUE_BOOK-b.QTY_OF_SECURITIES) as difference, " +
" b.ISIN_CODE, " +
" c.NOMENCLATURE, " +
" b.QTY_OF_SECURITIES  " +
" from book_val a " +
" left join ekuber_st b on b.ISIN_CODE=a.ISIN_BOOK and b.UPLOAD_DATE= date'" + from_date.Text + "'" +
" left join ekuber_st c on c.NOMENCLATURE=a.SECURITY_NAME_BOOK and c.UPLOAD_DATE= date'" + from_date.Text + "'" +
" where b.EKUBER_STAT='1' and c.EKUBER_STAT='1') " +
" select  " +
" a.ISIN_BOOK, " +
" a.SECURITY_NAME_BOOK, " +
" a.DIFFERENCE as Level2_Difference, " +
" nvl((a.DIFFERENCE-b.QTY_OF_SECURITIES),0) as Difference,  nvl(b.ISIN,0) as ISIN,  nvl(b.SECURITY_NAME,0) as SECURITY_NAME,  nvl(b.QTY_OF_SECURITIES,0) as QTY_OF_SECURITIES " +
" from level2_recon a " +
" left join collateral_tbl b on a.ISIN_BOOK=b.ISIN and b.UPLOAD_DATE=date'" + from_date.Text + "'" +
" where DIFFERENCE!=0 and b.COLLAT_STAT='1' order by a.ISIN_BOOK";
            string ConString = "Data Source=(DESCRIPTION =" +
           "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.197)(PORT = 1522))" +
           "(CONNECT_DATA =" +
             "(SERVER = DEDICATED)" +
             "(SERVICE_NAME = FCPROD)));" +
             "User Id=APPLOGS;Password=A#9pLO7g$2o23;Connection Timeout=600; Max Pool Size=150;";
            DataTable dt = get_oracle_data(query, ConString);
            col_val.Value = dt.Columns.Count.ToString();
            return dt;
        }
        private DataTable level_2_output()
        {
            string query= "with book_val(ISIN_book,Security_name_book,Value_book) as(select " +
" b.ISIN_NO,b.SECURITY_NAME,a.CURR_BAL " +
" from portfolio a left join isin_master b on a.name=b.SCRIP_DESC where a.UPLOAD_DATE=date'" + from_date.Text + "' and ISIN_STAT='1' and PORT_STAT='1') " +
" select distinct" +
" a.ISIN_BOOK, " +
" a.SECURITY_NAME_BOOK, " +
" a.VALUE_BOOK, " +
" case when (a.ISIN_BOOK=b.ISIN_CODE) then 'TRUE' else 'FALSE' end as ISIN_MATCH, " +
"  case when (a.SECURITY_NAME_BOOK=c.NOMENCLATURE) then 'TRUE' else 'FALSE' end as NAME_MATCH, " +
"   (a.VALUE_BOOK-b.QTY_OF_SECURITIES) as difference, " +
" b.ISIN_CODE, " +
" c.NOMENCLATURE, " +
" b.QTY_OF_SECURITIES  " +
" from book_val a " +
" left join ekuber_st b on b.ISIN_CODE=a.ISIN_BOOK and b.UPLOAD_DATE= date'" + from_date.Text + "'" +
" left join ekuber_st c on c.NOMENCLATURE=a.SECURITY_NAME_BOOK and c.UPLOAD_DATE= date'" + from_date.Text + "'" +
" where b.EKUBER_STAT='1' and c.EKUBER_STAT='1'";
            string ConString = "Data Source=(DESCRIPTION =" +
           "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.197)(PORT = 1522))" +
           "(CONNECT_DATA =" +
             "(SERVER = DEDICATED)" +
             "(SERVICE_NAME = FCPROD)));" +
             "User Id=APPLOGS;Password=A#9pLO7g$2o23;Connection Timeout=600; Max Pool Size=150;";
            DataTable dt = get_oracle_data(query, ConString);
            col_val.Value = dt.Columns.Count.ToString();
            return dt;
        }

        private DataTable level_1_output()
        {
            string query = " select a.SCRIP_CODE,a.FACE_VALUE,a.SCRIP_DESC,a.NAME,a.CURRENCY_CODE,substr(a.MATURITY_DATE,0,10) as MATURITY_DATE,a.BOOK_VALUE, " +
" a.CURR_BAL,a.PORTFOLIO_CODE,a.ACCR_DISC,a.INSTR_DESC,a.INSTR_TYPE,a.MAINTAINED_IN,a.ENTITY_LONG_NAME,a.REPO_SALES,substr(a.POS_DATE,0,10) as POS_DATE,a.OBB_PURCHASE, " +
" a.OBB_SALE,a.NO_OF_DAYS,a.CURRENCY,a.REPEQ_FV,a.REPEQ_BV,a.REP_CURR,a.YIELD,a.RATE1,substr(a.INT_DUE_DT,0,10) as INT_DUE_DT,a.SCRIP_ID,b.ISIN_NO,b.SECURITY_NAME " +
" from portfolio a left join isin_master b on a.name=b.SCRIP_DESC where a.UPLOAD_DATE=date'" + from_date.Text + "' and PORT_STAT='1' and ISIN_STAT='1'";

            string ConString = "Data Source=(DESCRIPTION =" +
             "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.197)(PORT = 1522))" +
             "(CONNECT_DATA =" +
               "(SERVER = DEDICATED)" +
               "(SERVICE_NAME = FCPROD)));" +
               "User Id=APPLOGS;Password=A#9pLO7g$2o23;Connection Timeout=600; Max Pool Size=150;";
            DataTable dt = get_oracle_data(query, ConString);
            col_val.Value = dt.Columns.Count.ToString();
            return dt;
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
           if(hdn_level.Value=="1")
            {
                level_btn_clk(sender, e);
            }
           else if (hdn_level.Value == "2")
            {
                leve2_btn_clk(sender, e);
            }
            else if (hdn_level.Value == "3")
            {
                leve3_btn_clk(sender, e);
            }
            Grid_view.PageIndex = e.NewPageIndex;
            Grid_view.DataBind();
        }



        protected void export_excel(object sender, EventArgs e)
        {           
            string lev = "";
            if (hdn_level.Value == "1")
            {
                lev = "level_1";
            }
            else if (hdn_level.Value == "2")
            {
                lev = "level_2";
            }
            else if (hdn_level.Value == "3")
            {
                lev = "level_3";
            }

            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = "" + lev + "_" + DateTime.Now + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            string style = @"<style> td { mso-number-format:\@;} </style>";
            Response.Write(style);
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            Grid_view.GridLines = GridLines.Both;
            Grid_view.HeaderStyle.Font.Bold = true;
            Grid_view.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();
        }




        protected void export_excel_upload(object sender, EventArgs e)
        {          
                string lev = "";
            if (hdn_upload_stat.Value == "1")
            {
                lev = "INIS_MASTER";
            }
            else if (hdn_upload_stat.Value == "2")
            {
                lev = "PORT_MASTER";
            }
            else if (hdn_upload_stat.Value == "3")
            {
                lev = "EKUBER_MASTER";
            }
            else if (hdn_upload_stat.Value == "4")
            {
                lev = "COLLATERAL_MASTER";
            }

            Response.Clear();
                Response.Buffer = true;
                Response.ClearContent();
                Response.ClearHeaders();
                Response.Charset = "";
                string FileName = "" + lev + "_" + DateTime.Now + ".xls";
                StringWriter strwritter = new StringWriter();
                HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.ms-excel";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            string style = @"<style> td { mso-number-format:\@;} </style>";
            Response.Write(style);
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            grid_for_view.GridLines = GridLines.Both;
            grid_for_view.HeaderStyle.Font.Bold = true;
            grid_for_view.RenderControl(htmltextwrtter);
                Response.Write(strwritter.ToString());
            Response.End();
        }


        protected void INS_DNT_Fetch(object sender, EventArgs e)
        {
            hdn_upload_stat.Value = "1";
            string query = "select * from isin_master where ISIN_STAT='1'";
            string ConString = "Data Source=(DESCRIPTION =" +
              "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.197)(PORT = 1522))" +
              "(CONNECT_DATA =" +
                "(SERVER = DEDICATED)" +
                "(SERVICE_NAME = FCPROD)));" +
                "User Id=APPLOGS;Password=A#9pLO7g$2o23;Connection Timeout=600; Max Pool Size=150;";
            DataTable dt = get_oracle_data(query, ConString);
            grid_for_view.DataSource = null;
            grid_for_view.DataBind();
            summary_div.InnerHtml = "";
            if (dt.Rows.Count > 0)
            {
                string str = "<table class='summary_of_file_tbl'>";
                str = str + "<tr><td class='sam_td_left'>File Name:</td><td class='sam_td_right'>INS Master DNT</td>";
                str = str + "<td class='sam_td_left'>Upload Status:</td><td class='sam_td_right'>Records Found</td></tr>";
                str = str + "<tr><td class='sam_td_left'>Total Record:</td><td class='sam_td_right'>" + dt.Rows.Count + "</td>";
                str = str + "<td class='sam_td_left'>File Type:</td><td class='sam_td_right'>Master File</td></tr>";
                summary_div.InnerHtml = str + "</table>";
                export_exp_upload.Enabled = true;
            }
            else
            {
                string str = "<table class='summary_of_file_tbl'>";
                str = str + "<tr><td class='sam_td_left'>File Name:</td><td class='sam_td_right'>INS Master DNT</td>";
                str = str + "<td class='sam_td_left'>Upload Status:</td><td class='sam_td_right'>No Records Found</td></tr>";
                str = str + "<tr><td class='sam_td_left'>Total Record:</td><td class='sam_td_right'>0</td>";
                str = str + "<td class='sam_td_left'>File Type:</td><td class='sam_td_right'>Master File</td></tr>";
                summary_div.InnerHtml = str + "</table>";
                export_exp_upload.Enabled = false;
            }
            grid_for_view.DataSource = dt;
            grid_for_view.DataBind();
            ScriptManager.RegisterStartupScript(this, GetType(), "switch_tab", "switch_tab();", true);
        }

        protected void check_uploads(ref string pass)
        {
            pass = "";
            string query = "select * from isin_master where ISIN_STAT='1'";
            string ConString = "Data Source=(DESCRIPTION =" +
              "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.197)(PORT = 1522))" +
              "(CONNECT_DATA =" +
                "(SERVER = DEDICATED)" +
                "(SERVICE_NAME = FCPROD)));" +
                "User Id=APPLOGS;Password=A#9pLO7g$2o23;Connection Timeout=600; Max Pool Size=150;";
            DataTable dt = get_oracle_data(query, ConString);
            if (dt.Rows.Count > 0)
            {
                pass = "1";
            }
            else
            {
                pass = "0";
            }

            query = "select * from portfolio where upload_date=date'" + from_date.Text + "' and port_stat='1'";//COLLATERAL_TBL
            dt = get_oracle_data(query, ConString);
            if (dt.Rows.Count > 0)
            {
                pass = pass+",1";
            }
            else
            {
                pass = pass+",0";
            }
            query = "select * from ekuber_st where upload_date=date'" + from_date.Text + "' and ekuber_stat='1'";
            dt = get_oracle_data(query, ConString);
            if (dt.Rows.Count > 0)
            {
                pass = pass + ",1";
            }
            else
            {
                pass = pass + ",0";
            }
            query = "select * from COLLATERAL_TBL where upload_date=date'" + from_date.Text + "' and collat_stat='1'";
            dt = get_oracle_data(query, ConString);
            if (dt.Rows.Count > 0)
            {
                pass = pass + ",1";
            }
            else
            {
                pass = pass + ",0";
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "change_image", "change_image('" + pass + "');", true);
        }

        protected void check_uploads2(ref string pass)
        {
            pass = "";
            string query = "select * from isin_master where ISIN_STAT='1'";
            string ConString = "Data Source=(DESCRIPTION =" +
              "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.197)(PORT = 1522))" +
              "(CONNECT_DATA =" +
                "(SERVER = DEDICATED)" +
                "(SERVICE_NAME = FCPROD)));" +
                "User Id=APPLOGS;Password=A#9pLO7g$2o23;Connection Timeout=600; Max Pool Size=150;";
            DataTable dt = get_oracle_data(query, ConString);
            if (dt.Rows.Count > 0)
            {
                pass = "1";
            }
            else
            {
                pass = "0";
            }

            query = "select * from portfolio where upload_date=date'" + from_date.Text + "' and port_stat='1'";//COLLATERAL_TBL
            dt = get_oracle_data(query, ConString);
            if (dt.Rows.Count > 0)
            {
                pass = pass + ",1";
            }
            else
            {
                pass = pass + ",0";
            }
            query = "select * from ekuber_st where upload_date=date'" + from_date.Text + "' and ekuber_stat='1'";
            dt = get_oracle_data(query, ConString);
            if (dt.Rows.Count > 0)
            {
                pass = pass + ",1";
            }
            else
            {
                pass = pass + ",0";
            }
            query = "select * from COLLATERAL_TBL where upload_date=date'" + from_date.Text + "' and collat_stat='1'";
            dt = get_oracle_data(query, ConString);
            if (dt.Rows.Count > 0)
            {
                pass = pass + ",1";
            }
            else
            {
                pass = pass + ",0";
            }
          
        }


        protected void send_email(object sender, EventArgs e)
        {
            try
            {

                string HostAdd = "10.200.1.95";// ConfigurationManager.AppSettings["Host"].ToString();
                string FromEmailid = "donotreply@bccbinfo.co.in";// ConfigurationManager.AppSettings["FromMail"].ToString();
                string Pass = "BC-L0Ca!";//ConfigurationManager.AppSettings["Password"].ToString();

                //creating the object of MailMessage  
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(FromEmailid); //From Email Id  
                mailMessage.Subject = "System Generated password for BCCB Intranet Admin Log In"; //Subject of Email  

                mailMessage.Body = "Hello Admin<br/>Your Credentials for BCCB Intranet website for Admin Log In<br/>" +
                        "Your Code : xxxxxx <br/>Your Password : xxxxxx <br/><h4>Thank you.</h4>"; //body or message of Email  
                mailMessage.IsBodyHtml = true;
                string email = "abhijeet.gharat@bccb.co.in";
                mailMessage.To.Add(new MailAddress(email)); //adding multiple TO Email Id  

                SmtpClient smtp = new SmtpClient();  // creating object of smptpclient  
                smtp.Host = HostAdd;              //host of emailaddress for example smtp.gmail.com etc  

                //network and security related credentials  

                smtp.EnableSsl = false;
                NetworkCredential NetworkCred = new NetworkCredential();
                NetworkCred.UserName = mailMessage.From.Address;
                NetworkCred.Password = Pass;
                smtp.UseDefaultCredentials = false;

                smtp.Credentials = NetworkCred;
                smtp.Port = 25;
                //WriteErrorLog("Email sent in progress");
                smtp.Send(mailMessage); //sending Email  
               // txtlbl.Text = "Password is sent in Email to Email-id " + email + ".";

            }
            catch (Exception en)
            {
               // txtlbl.Text = "Problem in sending Email. Please Contact to Head Office.";
            }
        }

        protected void fix_grid_view()
        {
            string query = "select * from ots_pending_data";
           string ConString = "Data Source=(DESCRIPTION =" +
             "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.197)(PORT = 1522))" +
             "(CONNECT_DATA =" +
               "(SERVER = DEDICATED)" +
               "(SERVICE_NAME = FCPROD)));" +
               "User Id=APPLOGS;Password=A#9pLO7g$2o23;Connection Timeout=600; Max Pool Size=150;";
            DataTable dt = get_oracle_data(query, ConString);
            Grid_view.DataSource = dt;
            Grid_view.DataBind();
        }

        protected void delete_INS(object sender, EventArgs e)
        {
            delete_data("isin_master");
        }

        protected void delete_Collateral(object sender, EventArgs e)
        {
            delete_data("COLLATERAL_TBL");
        }
        protected void delete_Port(object sender, EventArgs e)
        {
            delete_data("portfolio");
        }

        protected void delete_eKuber(object sender, EventArgs e)
        {
            delete_data("ekuber_st");
        }

        private void delete_data(string table_name)
        {
            string ConString = "Data Source=(DESCRIPTION =" +
                   "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.197)(PORT = 1522))" +
                   "(CONNECT_DATA =" +
                     "(SERVER = DEDICATED)" +
                     "(SERVICE_NAME = FCPROD)));" +
                     "User Id=APPLOGS;Password=A#9pLO7g$2o23;Connection Timeout=600; Max Pool Size=150;";
            string query = "", success_txt = "<table class='success_message_tbl'><tr><td class='success_mes_th' style='width:10%;'> SR No</td><td class='success_mes_th' style='width:40%;'>Deleted for</td><td class='success_mes_th' style='width:50%;'>No of Rows Deleted</td></tr>";
            string error_msg = "<table class='success_message_tbl'><tr><td class='success_mes_th' style='width:10%;'>SR No</td><td class='success_mes_th' style='width:90%;'>Error</td></tr>";
            if (table_name == "isin_master")
            {
                query = "update isin_master set ISIN_STAT='0' where to_char(UPLOAD_DATE ,'yyyy-MM-dd')='" + from_date.Text + "' and ISIN_STAT='1'";
            }
            else if (table_name == "COLLATERAL_TBL")
            {
                query = "update COLLATERAL_TBL set COLLAT_STAT='0' where to_char(UPLOAD_DATE ,'yyyy-MM-dd')='" + from_date.Text + "' and COLLAT_STAT='1'";
            }
            else if (table_name == "portfolio")
            {
                query = "update portfolio set PORT_STAT='0' where to_char(UPLOAD_DATE ,'yyyy-MM-dd')='" + from_date.Text + "' and PORT_STAT='1'";
            }
            else if (table_name == "ekuber_st")
            {
                query = "update ekuber_st set EKUBER_STAT='0' where to_char(UPLOAD_DATE ,'yyyy-MM-dd')='" + from_date.Text + "' and EKUBER_STAT='1'";
            }
            int r = 0, error = 0;
            try
            {
                OracleConnection connection = new OracleConnection(ConString);
                OracleCommand command = new OracleCommand(query, connection);
                command.Connection.Open();
                r = command.ExecuteNonQuery();
                command.Connection.Close();
                success_txt = success_txt + "<tr><td class='success_mes_td' style='width:10%;'>" +  1 + "</td><td class='success_mes_td' style='width:40%;'>Delete Data for Upload Date '" + from_date.Text + "' </td><td class='success_mes_td' style='width:50%; background: #bff39c;'>"+ r + " Records deleted</td></tr>";

            }
            catch (Exception rt)
            {
                error = 1;
                error_msg = error_msg + "<tr><td class='erro_mes_td' style='width:10%;'>" + error + "</td><td class='erro_mes_td' style='width:90%;'> <b>Query:</b> " + query + "<br><br><b>ERROR:</b> " + rt.Message + "</td></tr>";
            }
            r_insert.InnerText = "0";
            r_update.InnerText = r.ToString();
            r_error.InnerText = error.ToString();
            if (error == 0)
            {
                up_status.InnerText = "Successful";
            }
            else
            {
                up_status.InnerText = "Complete Fail";
            }
            string pass = "";
            insert_box_new.InnerHtml = success_txt + "</table>";
            error_box_div.InnerHtml = error_msg + "</table>";
            check_uploads2(ref pass);           
            ScriptManager.RegisterStartupScript(this, GetType(), "detail_message", "show_summary_box_new('" + pass + "');", true);
        }

        private void upload_data(ExcelWorksheet worksheet,int k)
        {
            string query = "", success_txt= "<table class='success_message_tbl'><tr><td class='success_mes_th' style='width:10%;'> SR No</td><td class='success_mes_th' style='width:40%;'>ISIN No</td><td class='success_mes_th' style='width:50%;'>Security Name</td></tr>";
            string error_msg = "<table class='success_message_tbl'><tr><td class='success_mes_th' style='width:10%;'>SR No</td><td class='success_mes_th' style='width:90%;'>Error</td></tr>";
            if(k==1)
            {
                int columns = worksheet.Dimension.Columns;
                int rows = worksheet.Dimension.Rows;
                DataTable table = new DataTable();
                table.Columns.Add("isin_no");
                table.Columns.Add("scrip_desc");
                table.Columns.Add("mat_date");
                table.Columns.Add("security_name");               
                for (int j = 0; j < rows-1; j++)
                {                   
                    DataRow new_rw = table.NewRow();
                    for (int i = 0; i < columns; i++)
                    {                       
                        string test = worksheet.Cells[1, (i + 1)].Value?.ToString();
                        if (test.Trim()== "scrip_desc")
                        {
                            new_rw["scrip_desc"] = worksheet.Cells[(j + 2), (i + 1)].Value?.ToString();
                        }
                        if (test.Trim() == "mat_date")
                        {
                            double d = double.Parse(worksheet.Cells[(j + 2), (i + 1)].Value?.ToString());
                            DateTime conv = DateTime.FromOADate(d);
                            new_rw["mat_date"] = conv.ToString("yyyy-MM-dd");
                        }
                        if (test.Trim() == "ISIN no")
                        {
                            new_rw["isin_no"] = worksheet.Cells[(j + 2), (i + 1)].Value?.ToString();
                        }
                        if (test.Trim() == "Security Name")
                        {
                            new_rw["security_name"] = worksheet.Cells[(j + 2), (i + 1)].Value?.ToString();
                        }

                       // sArray[i] = worksheet.Cells[1, (i + 1)].Value?.ToString();
                    }
                    table.Rows.Add(new_rw);
                }

                int insert = 0, upload = 0, error = 0;
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    query = "select * from isin_master where isin_no='" + table.Rows[i]["isin_no"].ToString() + "' and ISIN_STAT='1'";
                    string ConString = "Data Source=(DESCRIPTION =" +
                      "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.197)(PORT = 1522))" +
                      "(CONNECT_DATA =" +
                        "(SERVER = DEDICATED)" +
                        "(SERVICE_NAME = FCPROD)));" +
                        "User Id=APPLOGS;Password=A#9pLO7g$2o23;Connection Timeout=600; Max Pool Size=150;";
                    DataTable dt = get_oracle_data(query, ConString);

                    try
                    {
                        if (dt.Rows.Count == 0)
                        {
                            insert++;
                            query = "insert into isin_master (isin_no,scrip_desc,mat_date,security_name,upload_code,upload_date,isin_stat) " +
                                "values('" + table.Rows[i]["isin_no"].ToString() + "','" + table.Rows[i]["scrip_desc"].ToString() + "',date'" + table.Rows[i]["mat_date"].ToString() + "','" + table.Rows[i]["security_name"].ToString() + "','" + sessionlbl.Text + "',sysdate,'1')";
                            success_txt = success_txt + "<tr><td class='success_mes_td' style='width:10%;'>" + (i + 1) + "</td><td class='success_mes_td' style='width:40%;'>" + table.Rows[i]["isin_no"].ToString() + "</td><td class='success_mes_td' style='width:50%; background: #bff39c;'>" + table.Rows[i]["security_name"].ToString() + "</td></tr>";
                        }
                        else
                        {
                            upload++;
                            query = "update isin_master set scrip_desc='" + table.Rows[i]["scrip_desc"] + "',mat_date=date'" + table.Rows[i]["mat_date"] + "',security_name='" + table.Rows[i]["security_name"] + "',edit_code='" + sessionlbl.Text + "',edit_date=sysdate  where isin_no='" + table.Rows[i]["isin_no"].ToString() + "'";
                            success_txt = success_txt + "<tr><td class='update_mes_td' style='width:10%;'>" + (i + 1) + "</td><td class='update_mes_td' style='width:40%;'>" + table.Rows[i]["isin_no"].ToString() + "</td><td class='update_mes_td' style='width:50%;  background: #e8af73;'>" + table.Rows[i]["security_name"].ToString() + "</td></tr>";

                        }

                        OracleConnection connection = new OracleConnection(ConString);
                        OracleCommand command = new OracleCommand(query, connection);
                        command.Connection.Open();
                        command.ExecuteNonQuery();
                        command.Connection.Close();

                    }
                    catch (Exception rt)
                    {
                        error++;
                        error_msg = error_msg + "<tr><td class='erro_mes_td' style='width:10%;'>"+ error + "</td><td class='erro_mes_td' style='width:90%;'> <b>Query:</b> " + query+"<br><br><b>ERROR:</b> "+rt.Message+"</td></tr>";
                        //ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('" + rt.Message + "');", true);
                    }
                }

                r_insert.InnerText = insert.ToString();
                r_update.InnerText = upload.ToString();
                r_error.InnerText = error.ToString();
                if(error==0)
                {
                    up_status.InnerText = "Successful";
                }
                else if(error == table.Rows.Count)
                {
                    up_status.InnerText = "Complete Fail";
                }
                else
                {
                    up_status.InnerText = "Partial Upload";
                }
            }
            else if(k==2)
            {
                int columns = worksheet.Dimension.Columns;
                int rows = worksheet.Dimension.Rows;
                DataTable table = new DataTable();
                table.Columns.Add("scrip_code");
                table.Columns.Add("face_value");
                table.Columns.Add("scrip_desc");
                table.Columns.Add("name");
                table.Columns.Add("currency_code");
                table.Columns.Add("maturity_date");
                table.Columns.Add("book_value");
                table.Columns.Add("curr_bal");
                table.Columns.Add("portfolio_code");
                table.Columns.Add("accr_disc");
                table.Columns.Add("instr_desc");
                table.Columns.Add("instr_type");
                table.Columns.Add("maintained_in");
                table.Columns.Add("entity_long_name");
                table.Columns.Add("repo_sales");
                table.Columns.Add("pos_date");
                table.Columns.Add("obb_purchase");
                table.Columns.Add("obb_sale");
                table.Columns.Add("no_of_days");
                table.Columns.Add("currency");
                table.Columns.Add("repeq_fv");
                table.Columns.Add("repeq_bv");
                table.Columns.Add("rep_curr");
                table.Columns.Add("yield");
                table.Columns.Add("rate1");
                table.Columns.Add("int_due_dt");
                table.Columns.Add("scrip_id");
                for (int j = 0; j < rows - 1; j++)
                {
                    DataRow new_rw = table.NewRow();
                    for (int i = 0; i < columns; i++)
                    {
                        string test = worksheet.Cells[1, (i + 1)].Value?.ToString();
                        string val = worksheet.Cells[(j + 2), (i + 1)].Value?.ToString();
                        //if (val != "")
                        //{
                        //    new_rw[test.Trim()] = val.Trim();
                        //}
                        //else
                        //{
                            new_rw[test.Trim()] = val;
                        //}
                    }
                    table.Rows.Add(new_rw);
                }

                int insert = 0, upload = 0, error = 0;
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    query = "select * from portfolio where scrip_desc='" + table.Rows[i]["scrip_desc"].ToString() + "' and upload_date=date'"+ from_date.Text + "' and PORT_STAT='1'";
                    string ConString = "Data Source=(DESCRIPTION =" +
                      "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.197)(PORT = 1522))" +
                      "(CONNECT_DATA =" +
                        "(SERVER = DEDICATED)" +
                        "(SERVICE_NAME = FCPROD)));" +
                        "User Id=APPLOGS;Password=A#9pLO7g$2o23;Connection Timeout=600; Max Pool Size=150;";
                    DataTable dt = get_oracle_data(query, ConString);

                    try
                    {
                        if (dt.Rows.Count == 0)
                        {
                            insert++;
                            query = "insert into portfolio (scrip_code,face_value,scrip_desc,name,currency_code,maturity_date,book_value,curr_bal,portfolio_code,accr_disc,instr_desc,instr_type,maintained_in,entity_long_name,repo_sales,pos_date,obb_purchase,obb_sale,no_of_days,currency,repeq_fv,repeq_bv,rep_curr,yield,rate1,int_due_dt,scrip_id,upload_code,upload_date,port_stat) " +
                                "values('" + table.Rows[i]["scrip_code"].ToString() + "','" + table.Rows[i]["face_value"].ToString() + "','" + table.Rows[i]["scrip_desc"].ToString() + "','" + table.Rows[i]["name"].ToString() + "','" + table.Rows[i]["currency_code"].ToString() + "','" + table.Rows[i]["maturity_date"].ToString() + "','" + table.Rows[i]["book_value"].ToString() + "','" + table.Rows[i]["curr_bal"].ToString() + "','" + table.Rows[i]["portfolio_code"].ToString() + "','" + table.Rows[i]["accr_disc"].ToString() + "','" + table.Rows[i]["instr_desc"].ToString() + "','" + table.Rows[i]["instr_type"].ToString() + "','" + table.Rows[i]["maintained_in"].ToString() + "','" + table.Rows[i]["entity_long_name"].ToString() + "','" + table.Rows[i]["repo_sales"].ToString() + "','" + table.Rows[i]["pos_date"].ToString() + "','" + table.Rows[i]["obb_purchase"].ToString() + "','" + table.Rows[i]["obb_sale"].ToString() + "','" + table.Rows[i]["no_of_days"].ToString() + "','" + table.Rows[i]["currency"].ToString() + "','" + table.Rows[i]["repeq_fv"].ToString() + "','" + table.Rows[i]["repeq_bv"].ToString() + "','" + table.Rows[i]["rep_curr"].ToString() + "','" + table.Rows[i]["yield"].ToString() + "','" + table.Rows[i]["rate1"].ToString() + "','" + table.Rows[i]["int_due_dt"].ToString() + "','" + table.Rows[i]["scrip_id"].ToString() + "','" + sessionlbl.Text + "',date'" + from_date.Text + "','1')";
                            success_txt = success_txt + "<tr><td class='success_mes_td' style='width:10%;'>" + (i + 1) + "</td><td class='success_mes_td' style='width:40%;'>" + table.Rows[i]["scrip_code"].ToString() + "</td><td class='success_mes_td' style='width:50%; background: #bff39c;'>" + table.Rows[i]["scrip_desc"].ToString() + "</td></tr>";
                        }
                        else
                        {
                            upload++;
                            query = "update portfolio set scrip_code='" + table.Rows[i]["scrip_code"].ToString() + "',face_value='" + table.Rows[i]["face_value"].ToString() + "',name='" + table.Rows[i]["name"].ToString() + "',currency_code='" + table.Rows[i]["currency_code"].ToString() + "',maturity_date='" + table.Rows[i]["maturity_date"].ToString() + "',book_value='" + table.Rows[i]["book_value"].ToString() + "',curr_bal='" + table.Rows[i]["curr_bal"].ToString() + "',portfolio_code='" + table.Rows[i]["portfolio_code"].ToString() + "',accr_disc='" + table.Rows[i]["accr_disc"].ToString() + "',instr_desc='" + table.Rows[i]["instr_desc"].ToString() + "',instr_type='" + table.Rows[i]["instr_type"].ToString() + "',maintained_in='" + table.Rows[i]["maintained_in"].ToString() + "',entity_long_name='" + table.Rows[i]["entity_long_name"].ToString() + "',repo_sales='" + table.Rows[i]["repo_sales"].ToString() + "',pos_date='" + table.Rows[i]["pos_date"].ToString() + "',obb_purchase='" + table.Rows[i]["obb_purchase"].ToString() + "',obb_sale='" + table.Rows[i]["obb_sale"].ToString() + "',no_of_days='" + table.Rows[i]["no_of_days"].ToString() + "',currency='" + table.Rows[i]["currency"].ToString() + "',repeq_fv='" + table.Rows[i]["repeq_fv"].ToString() + "',repeq_bv='" + table.Rows[i]["repeq_bv"].ToString() + "',rep_curr='" + table.Rows[i]["rep_curr"].ToString() + "',yield='" + table.Rows[i]["yield"].ToString() + "',rate1='" + table.Rows[i]["rate1"].ToString() + "',int_due_dt='" + table.Rows[i]["int_due_dt"].ToString() + "',scrip_id='" + table.Rows[i]["scrip_id"].ToString() + "',edit_code='" + sessionlbl.Text + "',EDIT_DATE=sysdate  where scrip_desc='" + table.Rows[i]["scrip_desc"].ToString() + "' and port_stat='1' and upload_date=date'" + from_date.Text + "'";
                            success_txt = success_txt + "<tr><td class='update_mes_td' style='width:10%;'>" + (i + 1) + "</td><td class='update_mes_td' style='width:40%;'>" + table.Rows[i]["scrip_code"].ToString() + "</td><td class='update_mes_td' style='width:50%;  background: #e8af73;'>" + table.Rows[i]["scrip_desc"].ToString() + "</td></tr>";
                        }

                        OracleConnection connection = new OracleConnection(ConString);
                        OracleCommand command = new OracleCommand(query, connection);
                        command.Connection.Open();
                        command.ExecuteNonQuery();
                        command.Connection.Close();

                    }
                    catch (Exception rt)
                    {
                        error++;
                        error_msg = error_msg + "<tr><td class='erro_mes_td' style='width:10%;'>" + error + "</td><td class='erro_mes_td' style='width:90%;'> <b>Query:</b> " + query + "<br><br><b>ERROR:</b> " + rt.Message + "</td></tr>";
                        //ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('" + rt.Message + "');", true);
                    }
                }

                r_insert.InnerText = insert.ToString();
                r_update.InnerText = upload.ToString();
                r_error.InnerText = error.ToString();
                if (error == 0)
                {
                    up_status.InnerText = "Successful";
                }
                else if (error == table.Rows.Count)
                {
                    up_status.InnerText = "Complete Fail";
                }
                else
                {
                    up_status.InnerText = "Partial Upload";
                }
            }
            else if (k == 3)
            {
                int columns = worksheet.Dimension.Columns;
                int rows = worksheet.Dimension.Rows;
                DataTable table = new DataTable();
                table.Columns.Add("ISIN_Code");
                table.Columns.Add("Nomenclature");
                table.Columns.Add("Qty_Of_Securities");              
                for (int j = 0; j < rows - 1; j++)
                {
                    DataRow new_rw = table.NewRow();
                    for (int i = 0; i < columns; i++)
                    {
                        string test = worksheet.Cells[1, (i + 1)].Value?.ToString();
                        if (test.Trim() == "ISIN Code")
                        {
                            new_rw["ISIN_Code"] = worksheet.Cells[(j + 2), (i + 1)].Value?.ToString();
                        }                       
                        if (test.Trim() == "Nomenclature")
                        {
                            new_rw["Nomenclature"] = worksheet.Cells[(j + 2), (i + 1)].Value?.ToString();
                        }
                        if (test.Trim() == "Qty Of Securities")
                        {
                            new_rw["Qty_Of_Securities"] = worksheet.Cells[(j + 2), (i + 1)].Value?.ToString();
                        }

                        // sArray[i] = worksheet.Cells[1, (i + 1)].Value?.ToString();
                    }
                    table.Rows.Add(new_rw);
                }

                int insert = 0, upload = 0, error = 0;
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    query = "select * from ekuber_st where ISIN_Code='" + table.Rows[i]["ISIN_Code"].ToString() + "' and upload_date=date'" + from_date.Text + "' and EKUBER_STAT='1'";
                    string ConString = "Data Source=(DESCRIPTION =" +
                      "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.197)(PORT = 1522))" +
                      "(CONNECT_DATA =" +
                        "(SERVER = DEDICATED)" +
                        "(SERVICE_NAME = FCPROD)));" +
                        "User Id=APPLOGS;Password=A#9pLO7g$2o23;Connection Timeout=600; Max Pool Size=150;";
                    DataTable dt = get_oracle_data(query, ConString);

                    try
                    {
                        if (dt.Rows.Count == 0)
                        {
                            insert++;
                            query = "insert into ekuber_st (ISIN_Code,Nomenclature,Qty_Of_Securities,upload_code,upload_date,ekuber_stat) " +
                                "values('" + table.Rows[i]["ISIN_Code"].ToString() + "','" + table.Rows[i]["Nomenclature"].ToString() + "','" + table.Rows[i]["Qty_Of_Securities"].ToString() + "','" + sessionlbl.Text + "',date'" + from_date.Text + "','1')";
                            success_txt = success_txt + "<tr><td class='success_mes_td' style='width:10%;'>" + (i + 1) + "</td><td class='success_mes_td' style='width:40%;'>" + table.Rows[i]["ISIN_Code"].ToString() + "</td><td class='success_mes_td' style='width:50%; background: #bff39c;'>" + table.Rows[i]["Qty_Of_Securities"].ToString() + "</td></tr>";
                        }
                        else
                        {
                            upload++;
                            query = "update ekuber_st set Nomenclature='" + table.Rows[i]["Nomenclature"] + "',Qty_Of_Securities='" + table.Rows[i]["Qty_Of_Securities"] + "',edit_code='" + sessionlbl.Text + "',edit_date=sysdate  where ISIN_Code='" + table.Rows[i]["ISIN_Code"].ToString() + "' and upload_date=date'" + from_date.Text + "' and ekuber_stat='1' ";
                            success_txt = success_txt + "<tr><td class='update_mes_td' style='width:10%;'>" + (i + 1) + "</td><td class='update_mes_td' style='width:40%;'>" + table.Rows[i]["ISIN_Code"].ToString() + "</td><td class='update_mes_td' style='width:50%;  background: #e8af73;'>" + table.Rows[i]["Qty_Of_Securities"].ToString() + "</td></tr>";

                        }

                        OracleConnection connection = new OracleConnection(ConString);
                        OracleCommand command = new OracleCommand(query, connection);
                        command.Connection.Open();
                        command.ExecuteNonQuery();
                        command.Connection.Close();

                    }
                    catch (Exception rt)
                    {
                        error++;
                        error_msg = error_msg + "<tr><td class='erro_mes_td' style='width:10%;'>" + error + "</td><td class='erro_mes_td' style='width:90%;'> <b>Query:</b> " + query + "<br><br><b>ERROR:</b> " + rt.Message + "</td></tr>";
                        //ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('" + rt.Message + "');", true);
                    }
                }

                r_insert.InnerText = insert.ToString();
                r_update.InnerText = upload.ToString();
                r_error.InnerText = error.ToString();
                if (error == 0)
                {
                    up_status.InnerText = "Successful";
                }
                else if (error == table.Rows.Count)
                {
                    up_status.InnerText = "Complete Fail";
                }
                else
                {
                    up_status.InnerText = "Partial Upload";
                }
            }
            else if (k == 4)
            {
                int columns = worksheet.Dimension.Columns;
                int rows = worksheet.Dimension.Rows;
                DataTable table = new DataTable();
                table.Columns.Add("isin");
                table.Columns.Add("security_name");
                table.Columns.Add("collateral_description");
                table.Columns.Add("qty_of_securities");

          
                for (int j = 0; j < rows - 1; j++)
                {
                    DataRow new_rw = table.NewRow();
                    for (int i = 0; i < columns; i++)
                    {
                        string test = worksheet.Cells[1, (i + 1)].Value?.ToString();
                        if (test.Trim() == "Collateral Description")
                        {
                            new_rw["collateral_description"] = worksheet.Cells[(j + 2), (i + 1)].Value?.ToString();
                        }
                        if (test.Trim() == "Security Name")
                        {
                            new_rw["security_name"] = worksheet.Cells[(j + 2), (i + 1)].Value?.ToString();
                        }
                        if (test.Trim() == "ISIN")
                        {
                            new_rw["isin"] = worksheet.Cells[(j + 2), (i + 1)].Value?.ToString();
                        }
                        if (test.Trim() == "Qty Of Securities")
                        {
                            new_rw["qty_of_securities"] = worksheet.Cells[(j + 2), (i + 1)].Value?.ToString();
                        }
                        
                    }
                    table.Rows.Add(new_rw);
                }

                int insert = 0, upload = 0, error = 0;
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    query = "select * from COLLATERAL_TBL where isin='" + table.Rows[i]["isin"].ToString() + "' and upload_date=date'" + from_date.Text + "' and COLLAT_STAT='1'";
                    string ConString = "Data Source=(DESCRIPTION =" +
                      "(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.197)(PORT = 1522))" +
                      "(CONNECT_DATA =" +
                        "(SERVER = DEDICATED)" +
                        "(SERVICE_NAME = FCPROD)));" +
                        "User Id=APPLOGS;Password=A#9pLO7g$2o23;Connection Timeout=600; Max Pool Size=150;";
                    DataTable dt = get_oracle_data(query, ConString);

                    try
                    {
                        if (dt.Rows.Count == 0)
                        {
                            insert++;
                            query = "insert into COLLATERAL_TBL (isin,security_name,collateral_description,qty_of_securities,upload_code,upload_date,collat_stat) " +
                                "values('" + table.Rows[i]["isin"].ToString() + "','" + table.Rows[i]["security_name"].ToString() + "','" + table.Rows[i]["collateral_description"].ToString() + "','" + table.Rows[i]["qty_of_securities"].ToString() + "','" + sessionlbl.Text + "',date'" + from_date.Text + "','1')";
                            success_txt = success_txt + "<tr><td class='success_mes_td' style='width:10%;'>" + (i + 1) + "</td><td class='success_mes_td' style='width:40%;'>" + table.Rows[i]["isin"].ToString() + "</td><td class='success_mes_td' style='width:50%; background: #bff39c;'>" + table.Rows[i]["security_name"].ToString() + "</td></tr>";
                        }
                        else
                        {
                            upload++;
                            query = "update COLLATERAL_TBL set security_name='" + table.Rows[i]["security_name"] + "',collateral_description='" + table.Rows[i]["collateral_description"] + "',qty_of_securities='" + table.Rows[i]["qty_of_securities"] + "',edit_code='" + sessionlbl.Text + "',edit_date=sysdate  where isin='" + table.Rows[i]["isin"].ToString() + "' and upload_date=date'" + from_date.Text + "' and collat_stat='1'";
                            success_txt = success_txt + "<tr><td class='update_mes_td' style='width:10%;'>" + (i + 1) + "</td><td class='update_mes_td' style='width:40%;'>" + table.Rows[i]["isin"].ToString() + "</td><td class='update_mes_td' style='width:50%;  background: #e8af73;'>" + table.Rows[i]["security_name"].ToString() + "</td></tr>";
                        }

                        OracleConnection connection = new OracleConnection(ConString);
                        OracleCommand command = new OracleCommand(query, connection);
                        command.Connection.Open();
                        command.ExecuteNonQuery();
                        command.Connection.Close();

                    }
                    catch (Exception rt)
                    {
                        error++;
                        error_msg = error_msg + "<tr><td class='erro_mes_td' style='width:10%;'>" + error + "</td><td class='erro_mes_td' style='width:90%;'> <b>Query:</b> " + query + "<br><br><b>ERROR:</b> " + rt.Message + "</td></tr>";
                        //ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('" + rt.Message + "');", true);
                    }
                }

                r_insert.InnerText = insert.ToString();
                r_update.InnerText = upload.ToString();
                r_error.InnerText = error.ToString();
                if (error == 0)
                {
                    up_status.InnerText = "Successful";
                }
                else if (error == table.Rows.Count)
                {
                    up_status.InnerText = "Complete Fail";
                }
                else
                {
                    up_status.InnerText = "Partial Upload";
                }
            }

            insert_box_new.InnerHtml = success_txt + "</table>";
            error_box_div.InnerHtml = error_msg + "</table>";
        }
        protected void detect_upload_file()
        {          
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            if (FileUploadControl.HasFile)
            {
                string filePath = Server.MapPath("~/") + FileUploadControl.FileName;
                FileUploadControl.SaveAs(filePath);
                ViewState["FilePath"] = filePath;
                hdnFileName.Value = filePath;
                try
                {
                    HttpPostedFile file = FileUploadControl.PostedFile;
                    
                    if (Path.GetExtension(file.FileName).Equals(".xlsx",StringComparison.InvariantCultureIgnoreCase))
                    {
                        using (ExcelPackage package=new ExcelPackage(file.InputStream))
                        {
                            ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                            int columns = worksheet.Dimension.Columns;
                            int rows = worksheet.Dimension.Rows;
                            file_d_rows.InnerText = rows.ToString();
                            file_d_cols.InnerText = columns.ToString();
                            string[] sArray = new string[columns];
                            for(int i=0;i<columns;i++)
                            {
                                sArray[i] = worksheet.Cells[1, (i+1)].Value?.ToString();
                            }
                            int k = 0;
                            identify_file(sArray,columns,ref k);

                            if(k!=0)
                            {
                                upload_data(worksheet, k);
                            }
                            string pass = "";
                            check_uploads(ref pass);
                            ScriptManager.RegisterStartupScript(this, GetType(), "detail_message", "show_summary_box_new('" + pass + "');", true);
                            // string cellvalue = worksheet.Cells[1, 1].Value?.ToString();
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('Not An Excel File!!!');", true);
                    }
                }
                catch(Exception rt)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('" + rt.Message + "');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('No File Detected!!!');", true);
            }
        }

        private void identify_file(string[] x, int column, ref int k)
        {
            k = 0;
            if (column == 4)
            {
                var up_col = new List<string>()
                    {
                        "scrip_desc",
                        "mat_date",
                        "ISIN no",
                        "Security Name"
                    };
                               
                var col_col = new List<string>()
                {
                    "Collateral Description",
                        "Security Name",
                        "ISIN",
                        "Qty Of Securities"
                };

                for (int i = 0; i < column; i++)
                {
                    if (up_col.Contains(x[i].Trim()) == true)
                    {
                        up_col.Remove(x[i].Trim());
                    }
                    if (col_col.Contains(x[i].Trim()) == true)
                    {
                        col_col.Remove(x[i].Trim());
                    }
                }

                if (up_col.Count == 0)
                {
                    file_d_name.InnerText = "INS Master DNT";
                    p_div.InnerText = "-";
                    k = 1;
                }
                else if (col_col.Count == 0)
                {
                    file_d_name.InnerText = "Collateral File";
                    p_div.InnerText = "-";
                    k = 4;
                }
                else
                {
                    file_d_name.InnerText = "Not Identified";

                    for (int i = 0; i < up_col.Count; i++)
                    {
                        if (i == 0)
                        {
                            p_div.InnerText = up_col[i].ToString();
                        }
                        else
                        {
                            p_div.InnerText = p_div.InnerText + " , " + up_col[i].ToString();
                        }
                    }

                }



                //ScriptManager.RegisterStartupScript(this, GetType(), "new_message", "show_message_box();", true);
            }
            else if (column == 27)
            {
                var up_col = new List<string>()
                    {
                        "scrip_code",
"face_value",
"scrip_desc",
"name",
"currency_code",
"maturity_date",
"book_value",
"curr_bal",
"portfolio_code",
"accr_disc",
"instr_desc",
"instr_type",
"maintained_in",
"entity_long_name",
"repo_sales",
"pos_date",
"obb_purchase",
"obb_sale",
"no_of_days",
"currency",
"repeq_fv",
"repeq_bv",
"rep_curr",
"yield",
"rate1",
"int_due_dt",
"scrip_id"
                };
                for (int i = 0; i < column; i++)
                {
                    if (up_col.Contains(x[i].Trim()) == true)
                    {
                        up_col.Remove(x[i].Trim());
                    }
                }

                if (up_col.Count == 0)
                {
                    file_d_name.InnerText = "Portfolio File";
                    p_div.InnerText = "-";
                    k = 2;
                }
                else
                {
                    file_d_name.InnerText = "Not Identified";

                    for (int i = 0; i < up_col.Count; i++)
                    {
                        if (i == 0)
                        {
                            p_div.InnerText = up_col[i].ToString();
                        }
                        else
                        {
                            p_div.InnerText = p_div.InnerText + " , " + up_col[i].ToString();
                        }
                    }

                }
            }
            else if (column == 3)
            {
                var up_col = new List<string>()
                    {                   
                        "ISIN Code",
                        "Nomenclature",
                        "Qty Of Securities"
                    };
                for (int i = 0; i < column; i++)
                {
                    if (up_col.Contains(x[i].Trim()) == true)
                    {
                        up_col.Remove(x[i].Trim());
                    }
                }

                if (up_col.Count == 0)
                {
                    file_d_name.InnerText = "EKUBER ST File";
                    p_div.InnerText = "-";
                    k = 3;
                }
                else
                {
                    file_d_name.InnerText = "Not Identified";

                    for (int i = 0; i < up_col.Count; i++)
                    {
                        if (i == 0)
                        {
                            p_div.InnerText = up_col[i].ToString();
                        }
                        else
                        {
                            p_div.InnerText = p_div.InnerText + " , " + up_col[i].ToString();
                        }
                    }

                }



                //ScriptManager.RegisterStartupScript(this, GetType(), "new_message", "show_message_box();", true);
            }
            else
            {
                k = 0;
                file_d_name.InnerText = "Not Identified";

                for (int i = 0; i < x.Length; i++)
                {
                    if (i == 0)
                    {
                        p_div.InnerText = x[i].Trim();
                    }
                    else
                    {
                        p_div.InnerText = p_div.InnerText + " , " + x[i].Trim();
                    }
                }
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
                dr.Dispose();
                cmd.Dispose();
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

        protected void FileUploadControl_Load(object sender, EventArgs e)
        {
            detect_upload_file();
        }
    }
}