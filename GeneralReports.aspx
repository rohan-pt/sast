<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GeneralReports.aspx.cs" Inherits="BCCBExamPortal.GeneralReports" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <link rel="icon" href="Resources/bccb_logo.png"/>
     <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
     <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
   <link rel="preconnect" href="https://fonts.googleapis.com">
<link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
<link href="https://fonts.googleapis.com/css2?family=Tourney:wght@400&display=swap" rel="stylesheet">
    <link href="https://fonts.googleapis.com/css2?family=Bungee+Spice&display=swap" rel="stylesheet">
   <link href="https://fonts.googleapis.com/css2?family=Bebas+Neue&display=swap" rel="stylesheet">
    <link href="Resources/CSS/General.css" rel="stylesheet" />
    <script src="Resources/Script/General.js"></script>
    <title>General Reports</title>
</head>
<body>
    <form id="form1" runat="server">
        <%-- <asp:ScriptManager ID="scriptmanager1" runat="server"></asp:ScriptManager>
          <asp:UpdatePanel ID="updatepnl" runat="server" UpdateMode="Conditional" >
<ContentTemplate> --%>
       <div class="header">
            <label class="header_css">GENERAL REPORTS</label>
            <asp:Label ID="sessionlbl" runat="server" Visible="false" Text=""></asp:Label>
        </div>
    <div class="main_body">
      
          <div class="body_msg">
                <div class="content_div">
              <asp:DropDownList ID="ddl_dep"  class="ddl_location" runat="server" OnSelectedIndexChanged="cmbActivity_SelectedIndexChanged" AutoPostBack="true">
               <asp:ListItem Value="0" Text="--All Departments--" Selected="True">
               </asp:ListItem>
                </asp:DropDownList> 
</div>
              <div class="content_div">
              <asp:DropDownList ID="dllocation"  class="ddl_location" runat="server">
               <asp:ListItem Value="0" Text="--All Branches--" Selected="True">
               </asp:ListItem>
                </asp:DropDownList> 
</div>
               <div class="content_div">
              <asp:DropDownList ID="ddlreport"  class="ddl_location" runat="server"  OnSelectedIndexChanged="conreport_SelectedIndexChanged" AutoPostBack="true">
               <asp:ListItem Value="0" Text="--Please Select Report--" Selected="True">
               </asp:ListItem>
                </asp:DropDownList> 
</div>

              <div class="content_div"  runat="server" id="frm_dat_div" visible="false">
<div class="field_name_div">From Date:</div>
         <div class="field_data"><asp:TextBox ID="from_date" runat="server" placeholder="mm/dd/yyyy" Textmode="Date" ReadOnly = "false" CssClass="calender_cs"></asp:TextBox></div>
              </div>
                     <div class="content_div" runat="server" id="to_dat_div" visible="false">
<div class="field_name_div">To Date:</div>
         <div class="field_data"><asp:TextBox ID="to_date" runat="server" placeholder="mm/dd/yyyy" Textmode="Date" ReadOnly = "false" CssClass="calender_cs"></asp:TextBox></div>
              </div>
                  <div class="content_div">
                      <button type="button" class="report_btn" id="report_gen_btn" runat="server" onserverclick="Generate_report">Generate Report</button>
                         <button class="report_btn" id="reset_btn" runat="server" onserverclick="btn_reset">Reset</button>
                  </div>
                <div class="content_div">
          <label class="title_lbl">Report Size :</label>  <label class="col_lbl" id="lbl_rep_size" runat="server"> </label>
                    </div>

                <div class="content_div">
          <label class="title_lbl">Note :</label>  <label class="con_lbl" id="lbl_rep_note" runat="server"> </label>
                    </div>

          </div>
    </div>


     <div class="foterr" >
            <label class="footer_css">Developed By BCCB-IT</label>
        </div>
   <%--   </ContentTemplate>
    
                            <Triggers> 
                    <asp:AsyncPostBackTrigger ControlID="report_gen_btn" runat="server"/>
                </Triggers>
                </asp:UpdatePanel>--%>
    </form>
</body>
</html>
