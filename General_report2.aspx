<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="General_report2.aspx.cs" Inherits="BCCBExamPortal.General_report2" %>

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
<link href="https://fonts.googleapis.com/css2?family=Frank+Ruhl+Libre:wght@300&display=swap" rel="stylesheet">
    <link href="https://fonts.googleapis.com/css2?family=Gupter&display=swap" rel="stylesheet">
    <link href="Resources/CSS/General.css?v=10" rel="stylesheet" />
    <script src="Resources/Script/General.js?v=10"></script>
    <title>General Reports</title>
     <style type="text/css">        
     </style>
</head>
<body style="margin:0;">
    <form id="form1" runat="server">
       <asp:ScriptManager ID="scriptmanager1" runat="server"></asp:ScriptManager>
        
        <img class="auto-style1" src="Resources/Vectors/lion.png" id="lion_logo" onclick=""/>
       
        <div class="floating_div" id="flt_div">
          
              <asp:UpdatePanel ID="updatepnl" runat="server" UpdateMode="Conditional" >

<ContentTemplate> 

            <div class="search_header"><label class="general_search">GENERAL SEARCH</label></div>
            <div class="search_box_div">
                <input type="text" runat="server" id="txt_search" class="cls_input_txt" value="" />
                <button type="button" class="search_btn_cls" id="btn_search" runat="server" onserverclick="search_report">Search</button>
            </div>
            <div class="search_content" id="report_display" runat="server"></div>
             </ContentTemplate> 
    </asp:UpdatePanel>
            <div class="footer_search_summary">
               <div class="summary_div"><label class="lbl_summary">Total Ready Reports :   </label> <label class="lbl_sum_num" id="report_cnt" runat="server">90   </label></div> 
                <div class="summary_div"><button class="btn_summary" runat="server" id="btn_dw" onserverclick="download_cert">DOWNLOAD</button></div>
            </div>
        </div>
     
       <div class="header">
            <label class="header_css">
            GENERAL REPORTS</label>
            <asp:Label ID="sessionlbl" runat="server" Visible="false" Text="">              

            </asp:Label>
            <asp:Label ID="DepName" runat="server" Visible="false" Text=""></asp:Label>
                    <asp:Label ID="RepName" runat="server" Visible="false" Text=""></asp:Label>
           <input type="hidden" id="con_type" runat="server" value="" />
              <input type="hidden" id="schema_hid" runat="server" value="" />
        </div>
        <div class="user_div">
            <div class="user_name" id="master_user" runat="server">

            </div>

        </div>
    <div class="main_body">
      
          
                <div class="content_div">
              <asp:DropDownList ID="ddl_dep"  class="ddl_location_next" runat="server" OnSelectedIndexChanged="cmbActivity_SelectedIndexChanged" AutoPostBack="true">
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
              <asp:DropDownList ID="ddlreport"  class="ddl_location_next_3 " runat="server"  OnSelectedIndexChanged="conreport_SelectedIndexChanged" AutoPostBack="true">
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
               <div class="content_div" runat="server" id="txt_inp_div" visible="false">
<div class="field_name_div">Related Input : </div>
         <div class="field_data"><asp:TextBox   ID="txt_input_param" runat="server" placeholder="Required Text / Numeric input"  ReadOnly = "false" CssClass="calender_cs"></asp:TextBox></div>
              </div>
          <div class="content_div" runat="server" id="txt_inp_div2" visible="false">
<div class="field_name_div">Related Input : </div>
         <div class="field_data"><asp:TextBox   ID="txt_input_param2" runat="server" placeholder="Required Text / Numeric input "  ReadOnly = "false" CssClass="calender_cs"></asp:TextBox></div>
              </div>
                  <div class="content_div" runat="server" id="button_panel" visible="false">
                      <button type="button" class="report_btn" id="report_gen_btn" runat="server" onserverclick="Generate_report">Generate Report</button>
                         <button class="report_btn" id="reset_btn" runat="server" onserverclick="btn_reset">Reset</button>
                  </div>
        <div class="classic_stone">
               
            <table class="tbl_summary">
                <tr>
                    <td class="titu">Report Size</td>
                    <td class="titu2" id="lbl_rep_size" runat="server"></td>
                    <td class="titu">Author</td>
                    <td class="titu2" id="author_name" runat="server"></td>
                </tr>
                 <tr>
                    <td class="titu">Instructions</td>
                    <td class="titu2" id="lbl_rep_note" runat="server"></td>
                    <td class="titu">Note</td>
                    <td class="titu2" id="lbl_note" runat="server"></td>
                </tr>
                 <tr>
                    <td class="titu">Connected To</td>
                    <td class="titu2" id="con_lbl" runat="server"></td>
                    <td class="titu">Creation Date</td>
                    <td class="titu2" runat="server" id="creation_date"></td>
                </tr>
            </table>





            </div>
         <div class="content_div">
       <button class="notify_btn" id="notify" runat="server" onserverclick="notify_report" visible="false">Create Notification</button>
                    </div>
                 <div class="content_div">
       <button class="report_btn" id="export_to_excel" runat="server" onserverclick="export_excel" visible="false">
           Export to Excel
       </button>
                    </div>
               <div class="grid_view_result_div">
                
                 <asp:GridView   runat="server"  id="Grid_view" CssClass="mydatagrid" 
 HeaderStyle-CssClass="header_mas_gried" RowStyle-CssClass="rows" ></asp:GridView>  

               </div>
         
 


   
 </div>
          <div class="foterr" >
            <label class="footer_css">Developed By BCCB-IT</label>
        </div>
        <div class="master_message_container" id="master_page">
              
            <div class="visible_info">
                  <img class="message_logo" src="Resources/Vectors/lion.png" onclick=""/>
                <div class="spl_cls" style="font-weight:bold;">Special Information</div>
                <div class="spl_cls_small" style="color:#c75a0d;">General Reporting Section is running on Activity Mode. In this environment all reports are being routed to a special connection mentioned below.</div>
                <div class="spl_cls_small" style="text-align:left;">
                    <div class="in_cls_spl" style="width:30%;margin-left:5%;color:#0094ff;font-weight:bold;">Connection Name: </div> <div class="in_cls_spl" style="width:30%; color:#134219;" id="con_name_d" runat="server">sdsdfsadf</div>
                </div>
                  <div class="spl_cls_small" style="text-align:left;">
                    <div class="in_cls_spl" style="width:30%;margin-left:5%;color:#0094ff;font-weight:bold;">Activity Reason: </div> <div class="in_cls_spl" style="width:30%;color:#134219;" id="act_reason" runat="server"></div>
                     
                </div>
                 <div class="spl_cls_small" style="text-align:left;">
                    <div class="in_cls_spl" style="width:30%;margin-left:5%;color:#0094ff;font-weight:bold;">Database Type: </div> <div class="in_cls_spl" style="width:30%;color:#134219;" id="db_type" runat="server"></div>
                     
                </div>
                 <div class="spl_cls" style="font-weight:bold;margin-bottom:30px;"><button type="button" class="btn_spl_msg" onclick="hide_master();">OK</button></div>
            </div>

        </div>

    </form>
</body>
</html>
