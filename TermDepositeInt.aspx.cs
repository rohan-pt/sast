using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Oracle.ManagedDataAccess.Client;

namespace BCCBExamPortal
{
    public partial class TermDepositeInt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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

        protected void download_cert(object sender, EventArgs e)
        {
            string acc_num = txt_account_number.Value.Trim();
            StringBuilder sb = new StringBuilder();
            string output = "Addresss  : HEAD OFFICE, CATHOLIC BANK";
            output = output + Environment.NewLine + "            BUILDING, PAPDY NAKA,PAPDY";
            output = output + Environment.NewLine + "            VASAI WEST DIST PALGHAR            Pin Code  :   401207"; // City Code : VAS
            output = output + Environment.NewLine + "City Code : VAS";
            output = output + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine+"                                   ****************Term Deposite Interest Certificate*********";
         
            string ConString = "Data Source=(DESCRIPTION =" +
               "(ADDRESS = (PROTOCOL = TCP)(HOST = fcobdxdb-scan)(PORT = 1522))" +
               "(CONNECT_DATA =" +
                 "(SERVER = DEDICATED)" +
                 "(SERVICE_NAME = FCPROD)));" +
                 "User Id=pfms_read;Password=pFm$_Read$2023;Connection Timeout=600; Max Pool Size=150;";



            //    ConString = "Data Source=(DESCRIPTION =" +
            //"(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.175)(PORT = 1521))" +
            //"(CONNECT_DATA =" +
            //  "(SERVER = DEDICATED)" +
            //  "(SERVICE_NAME = FCMIS)));" +
            //  "User Id=fcrhost_read;Password=fcmis_read$2023;Connection Timeout=600; Max Pool Size=150;";
            //string query = "select nam_branch,TXT_CC_ADDR1,TXT_CC_ADDR2,TXT_CC_ADDR3,NAM_CC_CITY,NAM_CC_STATE,TXT_CC_ZIP,NAM_CC_COUNTRY,COD_FIN_INST_ID from FCRBRN.ba_cc_brn_mast where cod_cc_brn=" + ft_brn.InnerText + "";

            //DataTable dm = get_oracle_data(query, ConString);

           // output = output + Environment.NewLine + "                                   " + dm.Rows[0]["nam_branch"].ToString() + Environment.NewLine + "                                   " + dm.Rows[0]["TXT_CC_ADDR1"].ToString() + Environment.NewLine + "                                   " + dm.Rows[0]["TXT_CC_ADDR3"].ToString();
           // output = output + Environment.NewLine + "                                   " + dm.Rows[0]["NAM_CC_CITY"].ToString() + ", " + "                                   " + dm.Rows[0]["NAM_CC_STATE"].ToString();
          //  output = output + Environment.NewLine + "                                   " + "PIN : - " + dm.Rows[0]["TXT_CC_ZIP"].ToString();
          //  output = output + Environment.NewLine + "                                   " + "IFSC : - " + dm.Rows[0]["COD_FIN_INST_ID"].ToString();
            output = output + Environment.NewLine + Environment.NewLine + "                                                          " + "Date " + DateTime.Now.ToString("dd /MM/yyyy");
            output = output + Environment.NewLine + "           " + ft_acc_name.InnerText;
            output = output + Environment.NewLine + txt_ar_add.Value;

            output = output + Environment.NewLine + Environment.NewLine + "---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------";
            output = output + Environment.NewLine + Environment.NewLine + "Sub : Interest received on your Term Deposite ";
            output = output + Environment.NewLine + Environment.NewLine + "---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------";

            output = output + Environment.NewLine + Environment.NewLine + "Dear Sir/Madam,";
            output = output + Environment.NewLine + "    This is to certify that the following interest amounts have ";
            output = output + Environment.NewLine + "been accrued and/or have been paid on the undermentioned Term Deposits ";
           // output = output + Environment.NewLine + "been accrued and/or have been paid on the undermentioned Term Deposits ";
            DateTime now = DateTime.Now;
            var startDate = new DateTime(now.Year, 4, 1);
            int x = 0;
            if (now.Month - 1 != 0)
            {
                x = now.Month - 1;
            }
            else
            {
                x = 12;
            }
            var lastmonth = new DateTime(now.Year, x, 1);
            var enddate = lastmonth.AddMonths(1).AddDays(-1);
            output = output + Environment.NewLine + "for the period from " + startDate.ToShortDateString() + " to " + enddate.ToShortDateString() + "";


            string query1 = " with RD_Accounts (Branch,AccountNumber,AccountStat,ProductName,StartDate,MaturityDate,CloseDate,Balance,InterestRate,CCY) " +
" as " +
" ( " +
" select " +
" a.COD_CC_BRN,a.COD_ACCT_NO,a.COD_ACCT_STAT,b.NAM_PRODUCT,a.DAT_ACCT_OPEN,a.dat_maturity,d.DAT_TXN_CLOSE,a.bal_book,a.RAT_INT_RD,a.COD_CCY " +
" from fcrhost.ch_acct_mast a " +
" inner join fcrhost.ch_prod_mast b on b.FLG_RD='R' and a.COD_PROD=b.COD_PROD and b.flg_mnt_status='A' " +
" left join fcrhost.ch_acct_close_hist d on d.COD_ACCT_NO=a.COD_ACCT_NO  " +
" where a.flg_mnt_status='A' and a.cod_cust="+ acc_num + " " +
" ), " +
" TD_Accounts (Branch,AccountNumber,COD_DEP_NO,AccountStat,ProductName,StartDate,MaturityDate,CloseDate,Balance,InterestRate,CCY) " +
" as  " +
" ( " +
" select  " +
" a.COD_CC_BRN, " +
" a.COD_ACCT_NO,b.COD_DEP_NO, " +
" b.COD_DEP_STAT, " +
" c.NAM_PRODUCT, " +
" b.DAT_DEP_DATE, " +
" b.DAT_MATURITY, " +
" b.DAT_CLOSE, " +
" b.BAL_PRINCIPAL, " +
" b.RAT_DEP_INT, " +
" a.COD_CCY " +
" from " +
"  fcrhost.td_acct_mast a " +
" left join fcrhost.td_dep_mast b on a.COD_ACCT_NO=b.COD_ACCT_NO and b.flg_mnt_status='A' " +
" left join fcrhost.td_prod_mast c on c.COD_PROD=a.COD_PROD and c.flg_mnt_status='A' " +
" where a.flg_mnt_status='A' and a.cod_cust="+ acc_num + " " +
" ) " +
" select " +
" a.Branch,cast(a.AccountNumber as varchar2(25)) as AccountNumber,a.AccountStat,a.ProductName,TO_CHAR(a.StartDate, 'DD-MM-YYYY') as StartDate,TO_CHAR(a.MaturityDate, 'DD-MM-YYYY') as MaturityDate,TO_CHAR(a.CloseDate, 'DD-MM-YYYY') as CloseDate,a.Balance,a.InterestRate, " +
" nvl(sum(b.AMT_TXN_FCY),0) as Interest_Amt,d.NAM_CCY_SHORT " +
" from RD_Accounts a " +
" left join FCRHOST.xf_stcap_gl_txns_mmdd b on b.cod_acct_no=a.AccountNumber and b.COD_DRCR='C' and b.COD_TXN_MNEMONIC='5042' " +
" left join fcrhost.ba_ccy_code d on d.COD_CCY=a.CCY  " +
" where b.DAT_TXN_POSTING between date'2023-04-01' and date'2024-03-31' " +
" group by a.Branch,a.AccountNumber,a.AccountStat,a.ProductName,a.StartDate,a.MaturityDate,a.CloseDate,a.Balance,a.InterestRate,d.NAM_CCY_SHORT " +
" union " +
" select  " +
" a.Branch,cast(a.AccountNumber||'-'||a.COD_DEP_NO as varchar2(25)) as AccountNumber,a.AccountStat,a.ProductName,TO_CHAR(a.StartDate, 'DD-MM-YYYY') as StartDate,TO_CHAR(a.MaturityDate, 'DD-MM-YYYY') as MaturityDate,TO_CHAR(a.CloseDate, 'DD-MM-YYYY') as CloseDate,a.Balance,a.InterestRate, " +
" nvl(sum(b.AMT_TXN_ACY),0) as Interest_Amt,d.NAM_CCY_SHORT " +
" from TD_Accounts a " +
" left join FCRHOST.td_audit_trail b on b.COD_ACCT_NO=a.AccountNumber and b.COD_DEP_NO=a.COD_DEP_NO and b.TXT_TXN_DESC='Interest Accrual' and b.cod_drcr='C' and b.DAT_VALUE between date'2023-04-01' and date'2024-03-31' " +
" left join fcrhost.ba_ccy_code d on d.COD_CCY=a.CCY " +
" group by a.Branch,a.AccountNumber,a.AccountStat,a.ProductName,a.StartDate,a.MaturityDate,a.CloseDate,a.Balance,a.InterestRate,d.NAM_CCY_SHORT,a.COD_DEP_NO " +
" order by STARTDATE asc";
            output = output + Environment.NewLine + Environment.NewLine + "---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------";
            output = output + Environment.NewLine + Environment.NewLine + "|SR NO|BRANCH|ACCOUNT             |BALANCE             |START          |MATURITY       |INTEREST    |TDS AMOUNT  |INTEREST  |CERTIFICATE    | ";
            output = output + Environment.NewLine + Environment.NewLine + "|     |      |                    |                    |DATE           |DATE           |            |            |RATE      |CLOSE DATE     | ";
            int flg = 0;
            DataTable dt = get_oracle_data(query1, ConString);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                  //  ft_acc_no.InnerText = dt.Rows[0]["Account_Number"].ToString() + "-" + dt.Rows[0]["Deposite_Number"].ToString();
                    output = output + Environment.NewLine + Environment.NewLine + "|"+(i+1).ToString().PadLeft(5,' ')+ "|"+ dt.Rows[i]["Branch"].ToString().PadLeft(6, ' ')+"|" + dt.Rows[i]["AccountNumber"].ToString().PadRight(20, ' ') + "|" + dt.Rows[i]["Balance"].ToString().PadLeft(20, ' ') + "|" + dt.Rows[i]["StartDate"].ToString().PadLeft(15, ' ') + "|" + dt.Rows[i]["MaturityDate"].ToString().PadLeft(15, ' ') + "|" + dt.Rows[i]["Interest_Amt"].ToString().PadLeft(12, ' ') + "|" + dt.Rows[i]["Interest_Amt"].ToString().PadLeft(12, ' ') + "|" + dt.Rows[i]["InterestRate"].ToString().PadLeft(10, ' ') + "|" + dt.Rows[i]["CloseDate"].ToString().PadLeft(15, ' ') + "| ";
                }

            }
            else
            {
                flg = 1;
                //ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('No such account Exist!!!');", true);
            }






            output = output + Environment.NewLine + Environment.NewLine + "---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------";
          //  output = output + Environment.NewLine + "Account No                                    Balance                                    Int Applied                                    Int Provided                                    ";
          //  output = output + Environment.NewLine + Environment.NewLine + ft_acc_no.InnerText + "                           " + ft_acc_bal.InnerText + "                                    " + ft_acc_int_rec.InnerText + "                                    " + ft_acc_int_rec.InnerText + "";
            //output = output + Environment.NewLine + Environment.NewLine + ft_acc_curr.InnerText + " " + NumberToWords(Convert.ToInt32(ft_acc_bal.InnerText));
            output = output + Environment.NewLine + Environment.NewLine + Environment.NewLine + "---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------";
            output = output + Environment.NewLine + "Thanking You,                                                                                                                      Your sincerely";
            output = output + Environment.NewLine;
            output = output + Environment.NewLine;
            output = output + Environment.NewLine;
            output = output + Environment.NewLine + "Signature of A/C. Holder                                                                                                       Authorised Signatory.";
            if (flg == 0)
            {
                sb.Append(output);
                sb.Append("\r\n");

                string text = sb.ToString();

                Response.Clear();
                Response.ClearHeaders();

                Response.AppendHeader("Content-Length", text.Length.ToString());
                Response.ContentType = "text/plain";
                Response.AppendHeader("Content-Disposition", "attachment;filename=\"output.txt\"");

                Response.Write(text);
                Response.End();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('No record Exist!!!');", true);
            }
        }

        public string NumberToWords(int number)
        {
            if (number == 0)
                return "Zero";

            if (number < 0)
                return "Minus " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 100000) > 0)
            {
                words += NumberToWords(number / 100000) + " Lac ";
                number %= 100000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " Thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " Hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                var unitsMap = new[] { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
                var tensMap = new[] { "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words;
        }


        protected void fetch_info(object sender, EventArgs e)
        {
            string acc_num = txt_account_number.Value.Trim();
          //  string receipt = txt_receipt_number.Value.Trim();
            if (acc_num != "")
            {
                try
                {
                    string ConString = "Data Source=(DESCRIPTION =" +
                 "(ADDRESS = (PROTOCOL = TCP)(HOST = fcobdxdb-scan)(PORT = 1522))" +
                 "(CONNECT_DATA =" +
                   "(SERVER = DEDICATED)" +
                   "(SERVICE_NAME = FCPROD)));" +
                   "User Id=pfms_read;Password=pFm$_Read$2023;Connection Timeout=600; Max Pool Size=150;";



                    //    ConString = "Data Source=(DESCRIPTION =" +
                    //"(ADDRESS = (PROTOCOL = TCP)(HOST = 10.200.1.175)(PORT = 1521))" +
                    //"(CONNECT_DATA =" +
                    //  "(SERVER = DEDICATED)" +
                    //  "(SERVICE_NAME = FCMIS)));" +
                    //  "User Id=fcrhost_read;Password=fcmis_read$2023;Connection Timeout=600; Max Pool Size=150;";

                    string query1 = "select NAM_CUST_FULL,TXT_CUSTADR_ADD1,TXT_CUSTADR_ADD2,NAM_CUSTADR_CITY,NAM_CUSTADR_STATE,NAM_CUSTADR_CNTRY,TXT_CUSTADR_ZIP from fcrhost.ci_custmast where cod_cust_id=" + acc_num + "";

                    DataTable dt = get_oracle_data(query1, ConString);
                    if (dt.Rows.Count > 0)
                    {
                      
                        ft_acc_name.InnerText = dt.Rows[0]["NAM_CUST_FULL"].ToString();                     
                        txt_ar_add.Value = "           " + dt.Rows[0]["TXT_CUSTADR_ADD1"].ToString() + Environment.NewLine + "           " + dt.Rows[0]["TXT_CUSTADR_ADD2"].ToString() + Environment.NewLine + "           " + "City :-" + dt.Rows[0]["NAM_CUSTADR_CITY"].ToString() + Environment.NewLine + "           " + "State :-" + dt.Rows[0]["NAM_CUSTADR_STATE"].ToString() + Environment.NewLine + "           " + "PIN :-" + dt.Rows[0]["TXT_CUSTADR_ZIP"].ToString();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('No such account Exist!!!');", true);
                    }
                }
                catch (Exception ep)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('Unable to create record because of some Exceptions!!!');", true);
                }
                finally
                {
                    //on.Close();
                }

            }
         


        }

    }
}