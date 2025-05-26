<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GeneralReport.aspx.cs" Inherits="BCCBExamPortal.GeneralReport" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <link rel="icon" href="Resources/bccb_logo.png"/>
     <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
     <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>    
    <link rel="preconnect" href="https://fonts.googleapis.com">
<link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
<link href="https://fonts.googleapis.com/css2?family=Nanum+Myeongjo&family=Nunito+Sans:ital,opsz,wght@0,6..12,200..1000;1,6..12,200..1000&display=swap" rel="stylesheet">
    <link href="Resources/CSS/GeneralReport.css?v=10" rel="stylesheet" />
    <script src="Resources/Script/new_general.js?v=10"></script>
    <title>General Reports</title>
</head>
<body>
    <form id="form1" runat="server">
          <asp:ScriptManager ID="scriptmanager1" runat="server"></asp:ScriptManager>
          <asp:Label ID="lbldbref" runat="server" Visible="false" Text="">  </asp:Label>         
          <asp:Label ID="sessionlbl" runat="server" Visible="false" Text="">              

            </asp:Label>

         <asp:Label ID="DepName" runat="server" Visible="false" Text=""></asp:Label>
         <asp:Label ID="RepName" runat="server" Visible="false" Text=""></asp:Label>
         <input type="hidden" id="con_type" runat="server" value="" />
           <input type="hidden" id="act_is" runat="server" value="" />
              <input type="hidden" id="schema_hid" runat="server" value="" />
        <input type="hidden" id="hidden_info_col" runat="server" value="" />
         <input type="hidden" id="hidden_col_count" runat="server" value="" />
        <div class="head_block">
            <img src="Resources/Vectors/lion.png" class="img_rep" id="lion_logo" />
            <div class="title_div">General Reports</div>
            <div class="user_info">
                <div class="hard_block">
                    <div class="left_hard" id="master_user" runat="server"></div>
                     <div class="left_hard" id="location_id" runat="server"></div>
                </div>
                 <div class="hard_block">
                    <div class="left_hard" id="report_cnt" runat="server"></div>
                     <div class="left_hard" id="database_date" runat="server"> </div>
                </div>
               
            </div>
        </div>
        <div class="master_div">
<div class="input_left">
    <div class="marshal_div" style="display:block;" >
          <asp:DropDownList ID="ddl_dep"  class="ddl_class" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cmbActivity_SelectedIndexChanged">
                   
                </asp:DropDownList> 
    </div>
     <div class="marshal_div" style="display:block;">
          <asp:DropDownList ID="dllocation"  class="ddl_class" runat="server">
                          
                </asp:DropDownList> 
    </div>
     <div class="marshal_div" style="display:block;">
          <asp:DropDownList ID="ddlreport"  class="ddl_class" runat="server" AutoPostBack="true" OnSelectedIndexChanged="conreport_SelectedIndexChanged">
                 <asp:ListItem Value="0" Text="--Please Select Report--" Selected="True">
               </asp:ListItem>       
                </asp:DropDownList> 
    </div>
      <div class="marshal_div" style="position:relative;" id="frm_dat_div" visible="false" runat="server">
          <span class="spn_cal">From Date : </span><asp:TextBox ID="from_date" runat="server" placeholder="mm/dd/yyyy" Textmode="Date" ReadOnly = "false" CssClass="calender_cs"></asp:TextBox>
      </div>
      <div class="marshal_div" style="position:relative;" id="to_dat_div" visible="false" runat="server">
          <span class="spn_cal">To Date   : </span><asp:TextBox ID="to_date" runat="server" placeholder="mm/dd/yyyy" Textmode="Date" ReadOnly = "false" CssClass="calender_cs"></asp:TextBox>
      </div>
     <div class="marshal_div" style="position:relative;" id="txt_inp_div" visible="false" runat="server">
          <span class="spn_cal">Text Input: </span><asp:TextBox ID="txt_input_param" runat="server" placeholder="General Text Input"  ReadOnly = "false" CssClass="calender_cs"></asp:TextBox>
      </div>
    <div class="marshal_div" style="position:relative;" id="txt_inp_div2" visible="false" runat="server">
          <span class="spn_cal">Numeric Input: </span><asp:TextBox ID="txt_input_param2" runat="server" placeholder="General Numeric Input"  ReadOnly = "false" CssClass="calender_cs"></asp:TextBox>
      </div>
     <div class="marshal_div" style="position:relative;" id="special_sele" runat="server" visible="false">
    <asp:DropDownList ID="ddl_special_sel"  class="ddl_class_other" runat="server">
                       
                </asp:DropDownList> 
  </div>
      <div class="marshal_div" style="position:relative;" id="exporter_div" runat="server" visible="false">
    <asp:DropDownList ID="ddl_datatype"  class="ddl_class_other" runat="server">
                        <asp:ListItem Value="0" Text="Export to Excel" Selected="True"></asp:ListItem>  
                 <asp:ListItem Value="1" Text="Export to CSV"></asp:ListItem>            
             <asp:ListItem Value="2" Text="Export to Text"></asp:ListItem>  
                </asp:DropDownList> 
  </div>
</div>
<div class="input_left">
     <div class="marshal_div" style="text-align:center;" visible="false" id="moving_logo" runat="server">
         <img src="Resources/Vectors/lion.png" class="logo_img" id="logo_r" />
     </div>
     <div class="marshal_div" style="position:relative;" id="size_info" runat="server" visible="false">
         <div class="hover_div" id="report_size_r">Report Size</div>
         <div class="marshal_div_marine" id="size_int" runat="server"></div>
     </div>
    <%-- <div class="marshal_div" style="position:relative;margin-top:50px;" visible="false" id="instructions_div" runat="server">
         <div class="hover_div">Instructions</div>
         <div class="marshal_div_marine" >Ad hock arrangement for generating file upload total count.</div>
     </div>--%>
     <div class="marshal_div" style="position:relative;margin-top:50px;" id="note_div" runat="server" visible="false">
         <div class="hover_div" id="note_r">Note</div>
         <div class="marshal_div_marine" id="lbl_rep_note" runat="server"></div>
     </div>
     <div class="marshal_div" style="position:relative;margin-top:50px;" id="conect_div" runat="server" visible="false">
         <div class="hover_div" style="background:#9ef2cd;padding:4px;" id="con_lbl_2" runat="server">Connected To</div>
         <div class="marshal_div_marine" id="con_lbl" runat="server" ></div>
     </div>
     <div class="marshal_div" style="position:relative;margin-top:50px;"  id="date_div" runat="server" visible="false">
         <div class="hover_div" id="create_r">Creation Date:</div>
         <div class="marshal_div_marine" id="creation_date" runat="server" ></div>
     </div>
     <div class="marshal_div" style="position:relative;margin-top:50px;"  id="tester" runat="server" visible="false">
       <table class="table_class">
           <tr>
               <td class="trm_test">Data Engineer : </td>
                <td class="trm_test_l" id="sf_engi" runat="server">-</td>
           </tr>
            <tr>
               <td class="trm_test">MIS Team Report Tester : </td>
                <td class="trm_test_l" id="mis_tester" runat="server">-</td>
           </tr>
            <tr>
               <td class="trm_test">Departmental Report Tester : </td>
                <td class="trm_test_l" id="dept_tester" runat="server">-</td>
           </tr>
       </table>
         </div>

</div>
             
        </div>
         <div class="marshal_div">
        <button class="button-6" type="button" id="generate_rpt" runat="server"  onserverclick="Generate_report">Generate Report</button>
             <button class="button-6" role="button" id="reset_btn" runat="server" onserverclick="reset">Reset</button>
           <%--   <button class="button-6" role="button" id="btn_export" runat="server" style="background:#b5f7f1;color:#0094ff;" visible="false">Export To Excel</button>--%>
              <button class="button-6" role="button" id="notify" runat="server" visible="false" onserverclick="notify_report">Notify</button>
    </div>

          <div class="grid_view_result_div">
                
                 <asp:GridView   runat="server"  id="Grid_view" CssClass="mydatagrid" 
 HeaderStyle-CssClass="header_mas_gried" RowStyle-CssClass="rows" ></asp:GridView>  

               </div>
         <div class="foterr" >
            <label class="footer_css"><b>Website Design and Developed by BCCB IT Department.      Copyright© 2019,BCCB</b></label>
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

         <div class="message_div" id="msg_div" style="display:none;">
            <div class="message_box">
                <div class="message_head">
                   ⓘ Information
                </div>
                <div class="message_info" id="msg_line">                   
                </div>
                <div class="message_info" style="text-align:center;"><button class="create_btn_rep" style="margin-left:0;border-radius:40px;margin-top:8px;" onclick="hide_msg();" type="button">OK</button></div>
            </div>

        </div>

         <div class="floating_div" id="flt_div">
          
              <asp:UpdatePanel ID="updatepnl" runat="server" UpdateMode="Conditional" >

<ContentTemplate> 

            <div class="search_header">  <img class="message_logo_2" src="Resources/Vectors/lion.png" onclick=""/>
                <label class="general_search">GENERAL SEARCH</label>  
                <img class="message_logo_2" src="Resources/Vectors/lion.png" onclick=""/></div>
            <div class="search_box_div">
                <input type="text" runat="server" id="txt_search" class="cls_input_txt" value="" />
                <button type="button" class="search_btn_cls" id="btn_search" runat="server" onserverclick="search_report">Search</button>
            </div>
            <div class="search_content" id="report_display" runat="server">

            </div>
             </ContentTemplate> 
    </asp:UpdatePanel>
            <div class="footer_search_summary">
            <%--   <div class="summary_div"><label class="lbl_summary">Total Ready Reports :   </label> <label class="lbl_sum_num" id="report_cnt" runat="server">90   </label></div> --%>
                <div class="summary_div"><button class="btn_summary" runat="server" id="btn_dw" onserverclick="download_cert">DOWNLOAD</button></div>
            </div>
        </div>

        <div class="data_exporter" id="data_exporter" runat="server" visible="false">
        <asp:ImageButton CssClass="export_img" ImageUrl="Resources/Vectors/Excel_img.jpg" runat="server" ID="img_Export_im" OnClick="export_excel" />       
        <asp:ImageButton CssClass="export_img" ImageUrl="Resources/Vectors/csvicon.png" runat="server" ID="csv_export_im" OnClick="Export_to_csv" />    
              <asp:ImageButton CssClass="export_img" ImageUrl="Resources/Vectors/pdficon.png" runat="server" ID="pdf_export_im" OnClick="Export_to_PDF" />    
              <asp:ImageButton CssClass="export_img" ImageUrl="Resources/Vectors/texticon.png" runat="server" ID="text_export_im" OnClick="Export_to_Text" />              
        </div>

        <div class="loading_section" id="loader">
            <div class="load_holder">
                <img src="Resources/Vectors/im_loader.gif" class="inside_image" />
                <img src="Resources/Vectors/lion.png" class="inside_lion" />
                <div class="akshar">Loading...Please Wait</div>
            </div>
        </div>




    </form>
</body>
</html>
