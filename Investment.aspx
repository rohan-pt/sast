<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Investment.aspx.cs" Inherits="BCCBExamPortal.Investment" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <link rel="icon" href="Resources/bccb_logo.png"/>
    <link rel="preconnect" href="https://fonts.googleapis.com">
      <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
      <script src="https://code.jquery.com/jquery-3.2.0.min.js"></script>
<link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
<link href="https://fonts.googleapis.com/css2?family=Ubuntu:wght@300&display=swap" rel="stylesheet">
    <link href="Resources/CSS/Investment.css?v=10" rel="stylesheet"/>
    <script src="Resources/Script/Investment.js?v=10"></script>
    <title>Investment</title>
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data" method="post">
         <%-- <asp:ScriptManager ID="scriptmanager1" runat="server"></asp:ScriptManager>
          <asp:UpdatePanel ID="updatepnl" runat="server" UpdateMode="Conditional" >

<ContentTemplate> --%>
         <asp:Label ID="sessionlbl" runat="server" Visible="false" Text="">            </asp:Label>
        <div class="fixed_menu_div">
            <img src="Resources/Vectors/Upload.jpg" class="menu_image" id="upload_btn" onclick="upload_click();"/>
           <%--  <img src="Resources/Vectors/Upload.jpg" class="menu_image"/>--%>
            <img src="Resources/Vectors/process33.jpg" class="menu_image" id="process_btn" onclick="process_click();" />
         <%--   <img src="Resources/Vectors/process44.jpg" class="menu_image" />
            <img src="Resources/Vectors/Greenbtn1X.jpg" class="menu_image" />--%>
            <img src="Resources/Vectors/REDBTN2X.jpg" class="menu_image" style="display:none;" id="reports_btn" onclick="reports_click();" />
            <input type="hidden" value="" id="hdn_dis_scr" runat="server" />
             <input type="hidden" value="" id="col_val" runat="server" />
        </div>

      <%--  <div><button id="btn_resp" runat="server" onserverclick="send_email">Ok</button></div>--%>
      <div style="width:100%;height:50px;text-align:center;line-height:50px;vertical-align:central;">
            <asp:TextBox ID="from_date" AutoPostBack="True"  runat="server" placeholder="Starting Date (mm/dd/yyyy)" Textmode="Date" ReadOnly = "false" CssClass="calender_cs_mx" OnTextChanged="check_recon_record"></asp:TextBox>
        </div>
    <div class="general_div" id="upload_section">
      

        <div class="general_div">
            <div class="tabular_summary">
                <div class="general_div" style="display:inline-block;margin-top:10px;">
                <div class="table_row">Upload of INS Master DNT File</div>
                  <div class="table_row2">
                      <asp:ImageButton CssClass="table_img" ImageUrl="Resources/Vectors/view_image.jpg" runat="server" ID="img_DNT" OnClick="INS_DNT_Fetch" />
                     <%-- <img src="" class="table_img" />--%></div>
                  <div class="table_row2">
                      <img src="Resources/Vectors/x_im.jpg" class="table_img" id="ins_done" onclick="switch_tab2();" /> 
                  </div>  
                     <div class="table_row2">
                         <asp:ImageButton CssClass="table_img" ImageUrl="Resources/Vectors/delete_para.png" runat="server" ID="ImageButton2" OnClick="delete_INS" />
                     </div>
                    </div>
                 <div class="general_div" style="display:inline-block;margin-top:10px;">
                <div class="table_row">Upload of Portfolio File</div>
                  <div class="table_row2"><asp:ImageButton CssClass="table_img" ImageUrl="Resources/Vectors/view_image.jpg" runat="server" ID="img_port" OnClick="port_display" /></div>
                  <div class="table_row2">  <img src="Resources/Vectors/x_im.jpg" class="table_img" id="port_done" onclick="switch_tab2();" /> </div>
                 <div class="table_row2"><asp:ImageButton CssClass="table_img" ImageUrl="Resources/Vectors/delete_para.png" runat="server" ID="ImageButton1" OnClick="delete_Port" /></div>
                    </div>
                 <div class="general_div" style="display:inline-block;margin-top:10px;">
                <div class="table_row">Upload of EKUBER ST File</div>
                  <div class="table_row2"><asp:ImageButton CssClass="table_img" ImageUrl="Resources/Vectors/view_image.jpg" runat="server" ID="img_ekuber" OnClick="ekuber_display" /></div>
                  <div class="table_row2">  <img src="Resources/Vectors/x_im.jpg" class="table_img" id="ekuber_st" onclick="switch_tab2();"  /> </div>
                  <div class="table_row2"><asp:ImageButton CssClass="table_img" ImageUrl="Resources/Vectors/delete_para.png" runat="server" ID="ImageButton3" OnClick="delete_eKuber" /></div>
                    </div>
                 <div class="general_div" style="display:inline-block;margin-top:10px;">
                <div class="table_row">Upload of Collateral File</div>
                  <div class="table_row2"><asp:ImageButton CssClass="table_img" ImageUrl="Resources/Vectors/view_image.jpg" runat="server" ID="img_collate" OnClick="collateral_display" /></div>
                  <div class="table_row2">  <img src="Resources/Vectors/x_im.jpg" class="table_img" id="collate_img" onclick="switch_tab2();" /> </div>
                 <div class="table_row2"><asp:ImageButton CssClass="table_img" ImageUrl="Resources/Vectors/delete_para.png" runat="server" ID="ImageButton4" OnClick="delete_Collateral" /></div>
                    </div>
                 <div class="general_div" style="display:inline-block;margin-top:10px;display:none;">
                <div class="table_row">Upload of Master File</div>
                  <div class="table_row2"><img src="Resources/Vectors/view_image.jpg" class="table_img" /></div>
                  <div class="table_row2">  <img src="Resources/Vectors/x_im.jpg" class="table_img"/> </div>
                  <div class="table_row2"><asp:ImageButton CssClass="table_img" ImageUrl="Resources/Vectors/delete_para.png" runat="server" ID="ImageButton5" OnClick="port_display" /></div>
                    </div>
            </div>


            <div class="detail_div" style="margin-bottom:20px;" id="upload_box">
                <div class="container">
  <div class="card">
    <h3>Upload File</h3>
    <div class="drop_box">     
      <p>Files Supported: Excel</p>    
          <asp:FileUpload id="FileUploadControl" runat="server" AllowMultiple="false" class="btn"  EnableViewState="true" ViewStateMode="Enabled"/>    
        <asp:HiddenField ID="hdnFileName" runat="server" />
        <input type="hidden" runat="server" id="hdn_file_upload" value="" />
        <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
    </div>

  </div>
</div>

                <div style="width:100%;height:auto;margin-top:100px;">
                    <button id="btn_detect" runat="server" onserverclick="FileUploadControl_Load" type="button" class="new_detect" onclick="show_message_box();">Detect</button>
                  <%--  <button id="btn_upload" runat="server" onserverclick="FileUploadControl_Load" type="button" class="new_detect" style="display:none;">Upload</button>--%>
                </div>


            </div>
            
            <div class="detail_div" style="display:none;" id="summary_div" runat="server">



            </div>

             <div class="general_div" id="grid_div" style="height:300px;margin-top:0px;width:70%;margin-left:15%;overflow:auto;display:none;">
                  <asp:GridView   runat="server"  id="grid_for_view" CssClass="mydatagrid"  HeaderStyle-CssClass="header_mas_grid" RowStyle-CssClass="rows" >                   


                  </asp:GridView>

             </div>
              <div class="general_div" style="height:300px;margin-top:100px;width:70%;margin-left:15%;overflow:auto;display:none;" id="exp_exc">
                   <div class="export_image_div">  
                       <asp:ImageButton CssClass="table_img" ImageUrl="Resources/Vectors/Excel_img.jpg" runat="server" ID="export_exp_upload" OnClick="export_excel_upload" Enabled="false" />                       
                       <input type="hidden" value="" id="hdn_upload_stat" runat="server" />
                 </div>
                
             </div>
        </div>

        </div>
         <div class="general_div" style="display:none;" id="process_section">


             <div class="general_div" style="display:inline-block;width:70%;margin-left:15%;text-align:center;margin-top:100px;position:relative;">

                 <div class="top_image_div">
                     <div class="event_head">Portfolio File</div>
                     <img src="Resources/Vectors/suk_not.png" class="table_img" id="port_process_img"/>
                 </div>
                  <div class="top_image_div">
                       <div class="event_head">eKuber File</div>
                      <img src="Resources/Vectors/suk_not.png" class="table_img" id="ekuber_process" />
                  </div>
                   <div class="top_image_div">
                        <div class="event_head">Collateral File</div>
                     <img src="Resources/Vectors/suk_not.png" class="table_img" id="collate_process_img"/>
                 </div>

             </div>

             <div class="general_div" style="display:inline-block;width:50%;margin-left:25%;margin-top:150px;">
                <div class="button_div_e">
                    <div class="bot_label" id="lev1_lbl">LEVEL 1 REPORT</div>
                    <button class="new_button_DIS" id="level1_btn" runat="server" disabled="disabled" onserverclick="level_btn_clk"></button></div>
                    <div class="button_div_e"><button class="new_button_DIS" id="level2_btn" runat="server" disabled="disabled" onserverclick="leve2_btn_clk"></button><div class="bot_label" id="lev2_lbl">LEVEL 2 REPORT</div></div>
                    <div class="button_div_e"><button class="new_button_DIS" id="level3_btn" runat="server" disabled="disabled" onserverclick="leve3_btn_clk"></button><div class="bot_label" id="lev3_lbl">LEVEL 3 REPORT</div></div>
             </div>

             <div class="general_div" style="height:300px;margin-top:100px;width:70%;margin-left:15%;overflow:auto;">
                  <asp:GridView   runat="server" ID="Grid_view" CssClass="mydatagrid"  HeaderStyle-CssClass="header_mas_gried" RowStyle-CssClass="rows" EnableEventValidation="false"> 
                  </asp:GridView>

             </div>

               <div class="general_div" style="height:300px;margin-top:100px;width:70%;margin-left:15%;overflow:auto;">
                   <div class="export_image_div">  
                       <asp:ImageButton CssClass="table_img" ImageUrl="Resources/Vectors/Excel_img.jpg" runat="server" ID="export_to_excel8" OnClick="export_excel" Enabled="false" />                       
                       <input type="hidden" value="" id="hdn_level" runat="server" />
                 </div>
                   <div class="export_image_div" style="margin-left:10%;">  
                       <asp:ImageButton CssClass="table_img" ImageUrl="Resources/Vectors/pdfmove.png" runat="server" ID="export_to_pdf" Enabled="false" OnClick="ExportToPDF2"/>
                 </div>
                   </div>
             

             </div>

        <div style="width:100%;height:50px;border:1px solid #ff6a00;color:#ff6a00;vertical-align:central;line-height:50px;text-align:center;margin-top:200px;">
           Copyrights Ⓒ 2018 - 2024 Bassein Catholic Co-Operative Bank. All rights Reserved.
        </div>

        <div style="width:100%;height:150%;position:absolute;top:0;opacity:1;background:#d3d3d7;display:none;" id="info_div">
            <div style="width:60%;position:relative;height:400px;top:300px;left:20%;border:1px solid #b6ff00;" id="load_img">
                <img src="Resources/Vectors/load_bar.gif"  style="width:100%;position:relative;height:400px;border:1px solid #b6ff00;"/>
            </div>

        <div style="width:30%;position:absolute;height:430px;top:200px;border:1px solid #dadadd;left:35%;background:#f9fbfb;opacity:1;z-index:10;display:none;" id="msg_box">
            <div style="width:100%;background:#113c5b;color:#ffffff;text-align:center;padding-top:8px;padding-bottom:8px;">Message Box</div>
            <div style="width:100%;height:auto;margin-top:20px;"><span style="color:#000000;margin-left:10%;">File Type:  </span> <span style="color:#808080;" id="file_d_name" runat="server"> </span></div>
             <div style="width:100%;height:auto;margin-top:20px;"><span style="color:#000000;margin-left:10%;">File Rows:  </span> <span style="color:#808080;" id="file_d_rows" runat="server">0 </span></div>
             <div style="width:100%;height:auto;margin-top:20px;"><span style="color:#000000;margin-left:10%;">File Columns:  </span> <span style="color:#808080;" id="file_d_cols" runat="server">0 </span></div>
            <div style="width:100%;height:auto;margin-top:20px;"><span style="color:#000000;margin-left:10%;">Problematic Columns:  </span> <span style="color:#808080;" id="p_div" runat="server"> </span></div>
              <div style="width:100%;height:auto;margin-top:20px;"><span style="color:#000000;margin-left:10%;">Insert Count:  </span> <span style="color:#808080;" id="r_insert" runat="server">0 </span></div>
              <div style="width:100%;height:auto;margin-top:20px;"><span style="color:#000000;margin-left:10%;">Update Count:  </span> <span style="color:#808080;" id="r_update" runat="server">0 </span></div>
             <div style="width:100%;height:auto;margin-top:20px;"><span style="color:#000000;margin-left:10%;">Error Count:  </span> <span style="color:#808080;" id="r_error" runat="server">0 </span></div>
             <div style="width:100%;height:auto;margin-top:20px;"><span style="color:#000000;margin-left:10%;">Update Status:  </span> <span style="color:#808080;" id="up_status" runat="server"></span></div>
               <div style="width:100%;height:auto;margin-top:20px;"><button class="btn_box" id="btn_ms_ok" onclick="hide_box();" type="button">Ok</button><button type="button" class="btn_box" id="btn_can_ms" onclick="hide_btn_box();">Records</button><button type="button" class="btn_box" id="btn_error" onclick="error_btn_box();">Errors</button></div>
        </div>
            <div style="width:30%;position:absolute;height:430px;top:200px;border:1px solid #dadadd;left:0;background:#ffffff;opacity:1;z-index:10;display:none;overflow:auto;" id="box_m">
                <div style="width:100%;margin:0;height:auto;position:relative;opacity:1;z-index:10;align-content:center;" id="insert_box_new" runat="server"></div>
            </div>
              <div style="width:30%;position:absolute;height:430px;top:200px;border:1px solid #dadadd;left:67.5%;background:#ffffff;opacity:1;z-index:10;display:none;overflow:auto;" id="box_e">
                <div style="width:100%;margin:0;height:auto;position:relative;opacity:1;z-index:10;align-content:center;" id="error_box_div" runat="server"></div>
            </div>


</div>

        <div class="bottom_div"></div>

        <div class="user_info_div">
            <div class="head_dim">
                <img src="Resources/Vectors/inflw.png" class="small_dim" />
            </div>
            <div class="classic_headre"><b>User Code :</b> <span id="spn_usr" runat="server"></span></div>
             <div class="classic_headre"><b>User Name :</b> <span id="spn_name" runat="server"></span></div>
              <div class="classic_headre"><b>Location    :</b> <span id="spn_location" runat="server"></span></div>
        </div>
   
    <%-- </ContentTemplate>
               <Triggers>
    <asp:PostBackTrigger ControlID="btn_detect" />                 
</Triggers>
      </asp:UpdatePanel>--%>
    </form>
</body>
</html>
