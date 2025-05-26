using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace BCCBExamPortal
{
    public partial class frmViewSigns : System.Web.UI.Page
    {
        string conStringPROD = ConfigurationManager.ConnectionStrings["dbconnection2"].ConnectionString;
        string conStringCBRDB = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        //string conStringMIS = ConfigurationManager.ConnectionStrings["bccbreport"].ConnectionString;

        public static string userloggedin = "";

        //SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["bccbreport"].ConnectionString);  // MIS
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection2"].ConnectionString); // PROD
        SqlConnection cncbr = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
        SqlDataReader dr;

        //public string branchCd = "";
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
                cmd1.Parameters.AddWithValue("@Page_Name", "View_Sign");
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

        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Timeout = 10;
            Page.ClientScript.RegisterStartupScript(this.GetType(),
                "onLoad", "DisplaySessionTimeout()", true);

            //Response.AddHeader("Refresh", Convert.ToString((Session.Timeout * 60) + 5));
            if (!IsPostBack)
            {
                audit_trails();
                if (Session.Count < 1)
                {
                    Response.Redirect("~/LogIn.aspx");
                }
                else
                {
                    if (Session["id"].ToString().Trim() != string.Empty && Session["id"].ToString().Trim() != null)
                    {

                        string code = Session["id"].ToString().Trim();

                        userloggedin = code;
                        // code = "SVF08492";
                        string strUserDet = updateLoginDetByUsr(code); 



                        if (strUserDet.Equals("Invalid User"))
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('User Details Not Found.');", true);
                            Response.Redirect("~/frmSignUserregistration.aspx"); // to be changed to redirect registration page.
                        }
                        else if (strUserDet.Equals("Sucessful"))
                        {

                            try
                            {

                                //if (Request.QueryString["Branch"] != null && Request.QueryString["Branch"] != string.Empty)
                                //{
                                //    
                                string branchCd = getUserOMNIBranchByCode(code);

                                try
                                {
                                    if (cn.State == ConnectionState.Open)
                                    {
                                        cn.Close();
                                    }
                                    //SqlCommand cmd1 = new SqlCommand("select PBrCode,Name from D001003 where PBrCode>2 order by PBrCode", cn);
                                    SqlCommand cmd1 = new SqlCommand("select PBrCode,Name from D001003 where PBrCode=" + branchCd + " order by Name", cn);
                                    cn.Open();
                                    SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                                    DataTable dt1 = new DataTable();
                                    sda1.Fill(dt1);
                                    cn.Close();
                                    drdBranch.DataSource = dt1;

                                    drdBranch.DataTextField = "Name";
                                    drdBranch.DataValueField = "PBrCode";
                                    drdBranch.DataBind();
                                    //drdBranch.Items.Insert(0, new ListItem("--Select Branch--", "0"));


                                    bindData(branchCd);

                                    //ddlAccountNo_SelectedIndexChanged(this, EventArgs.Empty);
                                    lbl_name.InnerText = "        " + get_info("select Employee_Name from Employee_LoginTbl where Code='" + code + "'");
                                    string branch_code = get_info("select UsrBrCode from BCCBREPORT.dbo.D002001 where  UsrCode1 like'%" + code + "%' and Status=1");
                                    if(branch_code=="")
                                    {
                                        branch_code = get_info("select UsrBrCode from BCCBREPORT.dbo.D002001 where  UsrCode1 like'%" + code + "%' and Status=1");
                                    }
                                    lbl_target.InnerText = "        " + get_info("select  DISTINCT COUNT(*) OVER () as rem_target  from tblSignFiles where branch='" + branch_code + "' and isVerified = 'N' and custNo <>'' group by CAST(acNo AS int)  having count(CAST(acNo AS int)) > 1");
                                    lbl_branch.InnerText = "         "+ get_info("select Name from D001003 where PBrCode = " + branch_code + " order by Name");
                                 
                                }
                                catch (Exception ex)
                                {

                                }

                                //}
                                //else
                                //{
                                //    FillDetails.Visible = false;
                                //}

                            }
                            catch (Exception Ex)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Could Not Process Your Request At This Moment');", true);
                            }


                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error In Processing Your Request.');", true);
                        }

                    }
                    else
                    {
                        Response.Redirect("~/frmLoginPage.aspx");
                    }
                }
            }
            else
            {
                if (Session.Count < 1)
                {
                    Response.Redirect("~/frmLoginPage.aspx");
                }
                else
                {
                    if (Session["id"].ToString().Trim() != string.Empty && Session["id"].ToString().Trim() != null)
                    {
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:MyFunction(); ", true);
                        //branchCode.Value = objUBL.getUserOMNIBranchByCode(Session["User_Code"].ToString().Trim());

                        userloggedin = Session["id"].ToString().Trim();

                      
                    }
                    else
                    {
                        Response.Redirect("~/frmLoginPage.aspx");
                    }
                }

               


            }



        }
        //{
        //    string branchCd = "";



        //    if (!IsPostBack)

        //    {
        //        if (Request.QueryString["Branch"] != null && Request.QueryString["Branch"] != string.Empty)
        //        {
        //            branchCd = Request.QueryString["Branch"];

        //            try
        //            {
        //                if (cn.State == ConnectionState.Open)
        //                {
        //                    cn.Close();
        //                }
        //                SqlCommand cmd1 = new SqlCommand("select PBrCode,Name from D001003 where PBrCode = " + branchCd + " order by Name", cn);
        //                cn.Open();
        //                SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
        //                DataTable dt1 = new DataTable();
        //                sda1.Fill(dt1);
        //                cn.Close();
        //                drdBranch.DataSource = dt1;

        //                drdBranch.DataTextField = "Name";
        //                drdBranch.DataValueField = "PBrCode";
        //                drdBranch.DataBind();
        //                //drdBranch.Items.Insert(0, new ListItem("--Select Branch--", "0"));


        //                bindData(branchCd);
        //                //ddlAccountNo_SelectedIndexChanged(this, EventArgs.Empty);


        //            }
        //            catch (Exception ex)
        //            {

        //            }

        //        }
        //        else
        //        {
        //            FillDetails.Visible = false;
        //        }


        //    }
        //}

        protected void Button1_Click(object sender, EventArgs e)
        {
            //ViewFieldSet.Visible = true;


            //    using (SqlConnection conn = new SqlConnection(conStringCBRDB))
            //    {
            //        using (SqlDataAdapter sda = new SqlDataAdapter("SELECT [acNo],[Name],[ContentType],[Data] FROM tblSignFiles where acNo='10003'", conn))
            //        {
            //            DataTable dt = new DataTable();
            //            sda.Fill(dt);
            //            gvImages.DataSource = dt;
            //            gvImages.DataBind();
            //        }

            //    }
            //    using (SqlConnection conn = new SqlConnection(conStringMIS))
            //    {
            //        using (SqlDataAdapter sda = new SqlDataAdapter("select CusNo,NameTitle,Name,LTRIM(RTRIM(LBrCode))+'/'+LTRIM(RTRIM(SUBSTRING(PrdAcctId,0,8)))+'/'+convert(varchar(8),LTRIM(RTRIM(CONVERT(INT,SUBSTRING(PrdAcctId,9,16))))) AS AccountNumber,NameType from D010153 where NameType=5 and LBrCode = 4 and PrdAcctId like 'SB%10003%'", conn))
            //        {
            //            DataTable dt = new DataTable();
            //            sda.Fill(dt);
            //            gvOmnitbl.DataSource = dt;
            //            gvOmnitbl.DataBind();
            //        }
            //    }

        }

        protected string get_info(string query)
        {
            string acc_no = "";
            try
            {
              
                string sqlcommand = query;               
                SqlConnection cnn;
                cnn = new SqlConnection(conStringCBRDB);
                cnn.Open();
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

            }
            return acc_no;
        }
        public string getUserOMNIBranchByCode(string code)
        {
            //SqlConnection ConnDB = new SqlConnection(conStringMIS); // MIS
            SqlConnection ConnDB = new SqlConnection(conStringPROD);//PROD

            string BrCode = string.Empty;
            try
            {

                SqlCommand da1 = new SqlCommand("SELECT UsrBrCode FROM D002001 where UsrCode1 like'%" + code.Trim().ToString() + "%' and Status=1", ConnDB);
                ConnDB.Open();
                SqlDataReader dr = da1.ExecuteReader();
                if (dr.HasRows == true)
                {
                    while (dr.Read())
                    {
                        BrCode = dr[0].ToString().Trim();
                    }
                }
                else
                {
                    BrCode = string.Empty;
                }
            }
            catch (Exception ZA)
            {
                throw;
            }
            finally
            {

                ConnDB.Close();
                //con.Dispose();
            }
            return BrCode;
        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = (DataRowView)e.Row.DataItem;
                string imageUrl = "data:image/jpg;base64," + Convert.ToBase64String((byte[])dr["Data"]);
                (e.Row.FindControl("Image1") as Image).ImageUrl = imageUrl;
            }
        }

        protected void ddlAccountNo_SelectedIndexChanged(object sender, EventArgs e)
        {

            rblVerify.ClearSelection();
            string accNo = ddlAccountNo.SelectedValue.ToString().Trim();
            string prdcd = drdAcct_Type.SelectedValue.ToString().Trim();
            string br = drdBranch.SelectedValue.ToString();
            if (accNo.Equals("0"))
            {
                ViewFieldSet.Visible = false;
            }
            else
            {
                ViewFieldSet.Visible = true;

                string fullomniacno = prdcd.ToString().Trim() + "      " + accNo.ToString().Trim().PadLeft(16, '0') + "00000000";

                using (SqlConnection conn = new SqlConnection(conStringCBRDB))
                {
                    //using (SqlDataAdapter sda = new SqlDataAdapter("SELECT [acNo],[Name],[ContentType],[Data] FROM tblSignFiles where prdCd='" + prdcd + "' and acNo='" + accNo + "'", conn))
                    //using (SqlDataAdapter sda = new SqlDataAdapter("SELECT [acNo],[Name],[ContentType],[Data] FROM tblSignFiles", conn))
                    string qr = "SELECT a.branch,a.prdCd,a.acNo,a.custNo,CASE     WHEN b.Longname IS NULL  THEN 'Original Signature in OMNI=>' else b.Longname end as Longname,a.ContentType,a.Data FROM tblSignFiles a full join BCCBREPORT.dbo.D009011 b on b.CustNo=a.custNo " +
 "where a.branch='" + br + "' and a.prdCd='" + prdcd + "' and a.acNo='" + accNo + "' and isVerified='N' and countInst = (select max(countInst) from [BCCB_CBR_DB].[dbo].[tblSignFiles] where  branch='" + br + "' and prdCd='" + prdcd + "' and acNo='" + accNo + "' and isVerified='N') order by a.Name";
                    //using (SqlDataAdapter sda = new SqlDataAdapter("SELECT [acNo],[Name],[ContentType],[Data] FROM tblSignFiles where branch='" + br + "' and prdCd='" + prdcd + "' and acNo='" + accNo + "' and countInst = (select max(countInst) from [BCCB_CBR_DB].[dbo].[tblSignFiles] where  branch='" + br + "' and prdCd='" + prdcd + "' and acNo='" + accNo + "' ) order by Name", conn))
                    using (SqlDataAdapter sda = new SqlDataAdapter(qr, conn))

                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        gvImages.DataSource = dt;
                        gvImages.DataBind();
                    }

                }
                //using (SqlConnection conn = new SqlConnection(conStringMIS))//MIS
                //using (SqlConnection conn = new SqlConnection(conStringPROD))
                //{
                //    using (SqlDataAdapter sda = new SqlDataAdapter("select CusNo,NameTitle,Name,LTRIM(RTRIM(LBrCode))+'/'+LTRIM(RTRIM(SUBSTRING(PrdAcctId,0,8)))+'/'+convert(varchar(8),LTRIM(RTRIM(CONVERT(INT,SUBSTRING(PrdAcctId,9,16))))) AS AccountNumber,NameType from D010153 where NameType=5 and LBrCode = '" + br + "' and PrdAcctId = '" + fullomniacno + "'", conn))
                //    {
                //        DataTable dt = new DataTable();
                //        sda.Fill(dt);
                //        gvOmnitbl.DataSource = dt;
                //        gvOmnitbl.DataBind();
                //    }
                //}
            }

            if (trrejreason.Visible == true)
            {
                trrejreason.Visible = false;
                ddlrejReason.SelectedValue = "0";
            }

        }

        public void bindData(string branchCd)
        {
            if (branchCd.Equals("0"))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Select a branch !!.');", true);
            }
            else
            {
                trAccType.Visible = true;
                trAccNo.Visible = true;

                string prdcd = drdAcct_Type.SelectedValue.ToString().Trim();

                if (cncbr.State == ConnectionState.Open)
                {
                    cncbr.Close();
                }
                SqlCommand cmd2 = new SqlCommand(" select distinct(CAST(acNo AS int)) as acNo FROM [BCCB_CBR_DB].[dbo].[tblSignFiles] where prdCd = '" + prdcd + "' and branch =" + branchCd + " and isVerified = 'N' and custNo <>''  group by CAST(acNo AS int)  having count(CAST(acNo AS int)) > 1  order by (CAST(acNo AS int))", cncbr);
                //SqlCommand cmd2 = new SqlCommand(" select acNo FROM [BCCB_CBR_DB].[dbo].[tblSignFiles]   ", cncbr);
                cncbr.Open();
                SqlDataAdapter sda2 = new SqlDataAdapter(cmd2);
                DataTable dt2 = new DataTable();
                sda2.Fill(dt2);
                cncbr.Close();
                ddlAccountNo.DataSource = dt2;

                ddlAccountNo.DataTextField = "acNo";
                ddlAccountNo.DataValueField = "acNo";
                ddlAccountNo.DataBind();
                //ddlAccountNo.Items.Insert(0, new ListItem("--Select AccountNo--", "0"));

                if (dt2.Rows.Count > 0)
                {
                    ddlAccountNo_SelectedIndexChanged(this, EventArgs.Empty);
                }
            }


        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (Session.Count < 1)
            {
                Response.Redirect("~/frmLoginPage.aspx");
            }
            else
            {

                string userloggedin = Session["id"].ToString().Trim();

                string srblVerify = rblVerify.SelectedValue.ToString();

                if (!srblVerify.Equals(string.Empty))
                {
                    string branchCd = drdBranch.SelectedValue.ToString();
                    string prdcd = drdAcct_Type.SelectedValue.ToString().Trim();
                    string accNo = ddlAccountNo.SelectedValue.ToString().Trim();

                    string rejReason = "";



                    if (srblVerify.Equals("YES"))
                    { rejReason = "All OK"; }
                    else if (srblVerify.Equals("NO"))
                    {
                        if (!ddlrejReason.SelectedValue.ToString().Trim().Equals("0"))
                        {
                            if (ddlrejReason.SelectedItem.Value == "Other")
                            {
                                rejReason = txtOtherReason.Text.Trim().ToString();
                            }
                            else
                            {
                                rejReason = ddlrejReason.SelectedValue.ToString().Trim();
                            }

                        }

                    }

                    if (!rejReason.Equals(string.Empty))
                    {
                        int rowsAffected = 0;
                        //string ConnectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
                        //using (SqlConnection connection = new SqlConnection("data source=.; database=Sample_Test_DB; integrated security=SSPI"))
                        using (SqlConnection connection = new SqlConnection(conStringCBRDB))
                        {
                            SqlCommand cmd = new SqlCommand("update tblSignFiles set isVerified = '" + srblVerify + "' , rejReason = '" + rejReason + "' , verifiedBy = '" + userloggedin + "' , isVerifiedDate = convert(varchar, getdate(), 13) where branch = '" + branchCd + "' and prdCd = '" + prdcd + "' and acNo = '" + accNo + "' and isVerified = 'N' ", connection);
                            //cmd.CommandText = "update tblSignFiles set isVerified = '" + srblVerify + "' where branch = '" + branchCd + "' and prdCd = '" + prdcd + "' and accNo = '" + accNo + "'";
                            connection.Open();
                            rowsAffected = cmd.ExecuteNonQuery();
                            connection.Close();
                        }

                        ViewFieldSet.Visible = false;
                        pnlTextBox.Visible = false;
                        txtOtherReason.Text = "";
                        bindData(drdBranch.SelectedValue.ToString());

                        string branch_code = get_info("select UsrBrCode from BCCBREPORT.dbo.D002001 where UsrCode1 like '%" + userloggedin + "%' and Status=1");
                        lbl_target.InnerText = "         " + get_info("select  DISTINCT COUNT(*) OVER () as rem_target  from tblSignFiles where branch='" + branch_code + "' and isVerified = 'N' and custNo <>'' group by CAST(acNo AS int)  having count(CAST(acNo AS int)) > 1");


                        //ddlAccountNo_SelectedIndexChanged(this, EventArgs.Empty);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Reason Not Selected !!.');", true);
                    }

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Choose Accept/Reject !!.');", true);
                }

            }


        }

        protected void drdAcct_Type_SelectedIndexChanged(object sender, EventArgs e)
        {

            ViewFieldSet.Visible = false;
            rblVerify.ClearSelection();
            pnlTextBox.Visible = false;
            txtOtherReason.Text = "";

            if (trrejreason.Visible == true)
            {
                trrejreason.Visible = false;

                ddlrejReason.SelectedValue = "0";
            }
            bindData(drdBranch.SelectedValue.ToString());
            //ddlAccountNo_SelectedIndexChanged(this, EventArgs.Empty);
        }

        protected void rblVerify_SelectedIndexChanged(object sender, EventArgs e)
        {
            string srblVerify = rblVerify.SelectedValue.ToString();

            if (!srblVerify.Equals(string.Empty))
            {
                if (srblVerify.Equals("YES"))
                {
                    trrejreason.Visible = false;
                    pnlTextBox.Visible = false;
                    txtOtherReason.Text = "";
                    ddlrejReason.SelectedValue = "0";
                }
                else if (srblVerify.Equals("NO"))
                {
                    trrejreason.Visible = true;
                    pnlTextBox.Visible = false;
                    txtOtherReason.Text = "";
                }

                else
                { }
            }
        }

        public string updateLoginDetByUsr(string userCode)
        {
            SqlConnection ConnDB = new SqlConnection(conStringCBRDB);
            try
            {

                SqlCommand cmd = new SqlCommand("Update_SCLogin", ConnDB);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Code", userCode);

                ConnDB.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                string Result = string.Empty;
                if (dr.HasRows == true)
                {
                    while (dr.Read())
                    {
                        Result = dr[0].ToString().Trim();
                    }
                }
                cmd.Dispose();
                return Result;

            }
            catch
            {
                throw;
            }
            finally
            {

                ConnDB.Close();
                //con.Dispose();
            }




        }

        protected void drdBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewFieldSet.Visible = false;
            rblVerify.ClearSelection();
            pnlTextBox.Visible = false;
            txtOtherReason.Text = "";

            if (trrejreason.Visible == true)
            {
                trrejreason.Visible = false;

                ddlrejReason.SelectedValue = "0";
            }
            bindData(drdBranch.SelectedValue.ToString());

        }

        //protected void lnkLogoutUser_Click(object sender, EventArgs e)
        //{
        //    Session.Abandon();
        //    Session.Clear();
        //    Response.Redirect("~/frmLoginPage.aspx");
        //}

        protected void ddlrejReason_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlrejReason.SelectedItem.Value == "Other")
            {
                pnlTextBox.Visible = true;
            }
            else
            {
                pnlTextBox.Visible = false;
                txtOtherReason.Text = "";
            }
        }
    }
}