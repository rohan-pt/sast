<%@ page language="C#" autoeventwireup="true" codebehind="Home.aspx.cs" inherits="BCCBExamPortal.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<head runat="server">
     <link rel="icon" href="Resources/bccb_logo.png"/>
    <title>Home</title>
     <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
   <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>     
     <link href="https://fonts.googleapis.com/css?family=Playfair+Display|Raleway" rel="stylesheet"/>
    <script src="https://code.jquery.com/jquery-3.2.0.min.js"></script>
    <link rel="preconnect" href="https://fonts.googleapis.com"/>
<link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
<link href="https://fonts.googleapis.com/css2?family=Charm:wght@700&family=Rajdhani&display=swap" rel="stylesheet"/>
    <link rel="stylesheet" href="https://fonts.googleapis.com/css2?family=Ysabeau+SC"/>
      <link rel="stylesheet" href="https://fonts.googleapis.com/css2?family=Anek+Tamil"/> 
<link href="https://fonts.googleapis.com/css2?family=Charm:wght@700&family=Poppins:wght@500&family=Rajdhani&display=swap" rel="stylesheet"/>
    <link href="Resources/CSS/Menu.css?v=22" rel="stylesheet" />
    <link href="Employee.css?v=22" rel="stylesheet" />
<link href="//ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/themes/base/jquery-ui.css" rel="stylesheet"/>
<script src="//ajax.googleapis.com/ajax/libs/jquery/1.4.1/jquery.js"></script>
<script src="//ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/jquery-ui.min.js"></script>
    <script src="Resources/Script/Menu.js?v=22"></script>
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <script type="text/javascript">
        google.charts.load('current', { 'packages': ['bar'] });
        function callfive(colname, month, xx, til,div_n) {
            google.charts.setOnLoadCallback(drawChart);          
            
            function drawChart() {

                var c1 = colname.split(",");
                var data = new google.visualization.DataTable();
                data.addColumn('string', c1[0]);
                for (i = 1; i <= c1.length - 1; i++) {
                    data.addColumn('number', c1[i]);
                }


                var res = xx.split(":");
                var data1 = [];
                for (i = 0; i < res.length; i++) {
                    var temp = [];
                    var cc = res[i].split(",");
                    temp.push(cc[0]);
                    for (j = 1; j <= cc.length - 1; j++) {
                        temp.push(parseFloat(cc[j]));
                    }
                    data1.push(temp);
                }
                data.addRows(data1);

              
                var options = {
                    chart: {
                        title: til,
                        subtitle: month,
                    },
                    bars: 'vertical',
                    vAxis: { format: 'decimal' },
                    colors: ['#1b9e77', '#44d5bf']
                };

                var chart = new google.charts.Bar(document.getElementById(div_n));

                chart.draw(data, google.charts.Bar.convertOptions(options));
            }
        }
    </script>
     <script type="text/javascript">
         google.charts.load('current', { 'packages': ['corechart'] });
         function callone(xx,data1,tit) {
             google.charts.setOnLoadCallback(drawChart);
             function drawChart() {

                 var res = data1.split("-");
                 var data = new google.visualization.DataTable();
                 data.addColumn('string', 'Action');
                 data.addColumn('number', 'No of Questions');
                 data.addRows([
                     ['Correct Ans', parseInt(res[0])],
                     ['Wrong Ans', parseInt(res[1])],
                     ['No Attempt', parseInt(res[2])]                   
                 ]);
                
                 var options = {
                     title: tit
                 };

                 var chart = new google.visualization.PieChart(document.getElementById(xx));

                 chart.draw(data, options);

             }
         }
     </script>
</head>
<body>

    <form id="form1" runat="server">
    

      

         <asp:ScriptManager ID="scriptmanager1" runat="server"></asp:ScriptManager>
          <asp:UpdatePanel ID="updatepnl" runat="server" UpdateMode="Conditional" >

<ContentTemplate> 
       <img class="auto-style1" src="Resources/Vectors/bccb.jpg" id="bccb_logo" onclick="showmission();"/>
    <input type="hidden" value="" id="mis_hdn" />
        <div class="top_div">
            <div class="menu_div_icons">
                 <a href="#" title="Profile" > <img src="Resources/Vectors/users_p.jpg" class="icon_image" onclick="show_pro_block();"/></a>
               <a href="#" title="Dashboard" onclick="showdashboard();"> <img src="Resources/Vectors/db.png" class="icon_image" /></a>
                <a  href="#" title="Reports" onclick="showreports();"> <img src="Resources/Vectors/reports.png" class="icon_image"/></a>
                  <a  href="#" title="Location View" onclick="org_view();"> <img src="Resources/Vectors/LocationV.jpg" class="icon_image" /></a>
               <a  href="#" title="Settings" onclick="showsettings();"> <img src="Resources/Vectors/settings1.jpg" class="icon_image" />              
                    <span class = "number" id="spn_not_cnt" runat="server"></span></a>
                 <a  href="#" title="Notifications"> <img src="Resources/Vectors/Notification_img.jpg" class="icon_image" onclick="show_noti_block();"/></a>
                <a href="LogOut.aspx" title="Logout" ><img src="Resources/Vectors/logout.png" class="icon_image"/></a>
            </div>

        </div>

    <div class="profile_block" id="profile_block" runat="server">
        <input type="hidden" id="pro_blk_hdn" value="" />
        <div class="arrow-up"></div>
        <div class="menu_in">
            <div class="div_info"><img src="Resources/Vectors/users_p.jpg" class="icon_image_menu"/></div>
            <div class="div_info" id="emp_Name_m" runat="server"></div>
        </div>
         <div class="menu_in">
            <div class="div_info"><img src="Resources/Vectors/barcode.png" class="icon_image_menu"/></div>
            <div class="div_info" id="emp_code_m" runat="server"></div>
        </div>
         <div class="menu_in">
            <div class="div_info"><img src="Resources/Vectors/location.jpg" class="icon_image_menu"/></div>
            <div class="div_info" id="emp_loc_m" runat="server"></div>
        </div>
         <div class="menu_in">
            <div class="div_info"><img src="Resources/Vectors/desin_new.png" class="icon_image_menu"/></div>
            <div class="div_info" id="emp_des_m" runat="server"></div>
        </div>
        <div class="menu_in">
            <div class="div_info"><img src="Resources/Vectors/email_l.png" class="icon_image_menu"/></div>
            <div class="div_info" id="emp_email_m" runat="server"></div>
        </div>
        <div class="menu_in">
            <div class="div_info"><img src="Resources/Vectors/cell img.jpg" class="icon_image_menu"/></div>
            <div class="div_info" id="emp_mob_m" runat="server"></div>
        </div>
    </div>
     <input type="hidden" id="noti_blk_hdn" value="" />
    <div class="mis_vis" id="mis_vis">
        <div class="logo_text"><span class="head_txt_pr">Bassein Catholic Co-operative Bank LTD</span></div>
        <div class="mission_div">Vision</div>
        <div class="mission_fc">Exceed customer expectations by making their dreams our priority, thereby serving the community and beyond.</div>
        <div class="mission_div">Mission</div>
        <div class="mission_fc">Delight the customer by providing simplified and modernized banking solutions through superior technology and customer relationship.</div>
    </div>

    <div class="notification_menu" id="notification_menu" runat="server">
          <div class="arrow-up" style="margin-left: 70%;"></div>          

    </div>

         <asp:Label ID="sessionlbl" runat="server" Visible="false" Text=""></asp:Label>
    <input type="hidden" value="" id="hdn_lastvisit" runat="server" />
        <div style="height:auto;">
           

            <div class="dashboardmm" id="dashboard_sec">
               
               <%-- <div class="org_div">
                    <button class="org_btn" type="button" onclick="org_view();" id="org_view_btn">Location View</button>
                </div>--%>

                <div class="swip_btn">
                    <button class="sm_btn" onclick="testing_pages();">15 G/15 H</button> <img src="Resources/Vectors/p_hand.jpg" class="sm_img" />
                    <div class="sm_div">
                        Pointed Section output is yet to be confirmed by the respective department. However, you can use the form for the Printing purpose.
                    </div>
                </div>

                 <div class="broad_panel">
                 <div class="test_info">
                     <div class="fal_tes">Knowledge Test by HRD Dept.</div>
                     <div class="active_test_div" runat="server" id="active_test" >
                         <asp:Table ID="tbl_test_act_rec" runat="server" CssClass="active_test_tbl">                             

                         </asp:Table>
                        <%-- <table class="active_test_tbl" >
                             <tr>
                                 <td class="td_sr">1.</td>
                                 <td class="td_ename">General Banking Test.General Banking Test.General Banking Test.General Banking Test</td>
                                 <td class="td_btn"><button class="btn_start_test">Start Test</button></td>
                             </tr>
                                  <tr>
                                 <td class="td_sr">2.</td>
                                 <td class="td_ename">General Banking Test.General Banking Test.General Banking Test.General Banking Test</td>
                                 <td class="td_btn"><button class="btn_start_test">Start Test</button></td>
                             </tr>
                                  <tr>
                                 <td class="td_sr">3.</td>
                                 <td class="td_ename">General Banking Test.General Banking Test.General Banking Test.General Banking Test</td>
                                 <td class="td_btn"><button class="btn_start_test">Start Test</button></td>
                             </tr>
                                  <tr>
                                 <td class="td_sr">1.</td>
                                 <td class="td_ename">General Banking Test.General Banking Test.General Banking Test.General Banking Test</td>
                                 <td class="td_btn"><button class="btn_start_test">Start Test</button></td>
                             </tr>
                                  <tr>
                                 <td class="td_sr">1.</td>
                                 <td class="td_ename">General Banking Test.General Banking Test.General Banking Test.General Banking Test</td>
                                 <td class="td_btn"><button class="btn_start_test">Start Test</button></td>
                             </tr>
                         </table>--%>
                     </div>

                 </div>
                     <div class="test_info" id="view_test_info" runat="server" visible="false">
                           <div class="fal_tes_rep" id="rem_test_id" runat="server"></div>
                         <input type="hidden" runat="server" id="hdn_view_test" />
                         <div class="sum_up" style="margin-top:20px;">
                             <table class="otomon_tbl">
                                 <tr>
                                     <td class="ot_td">No of Questions</td>
                                      <td class="ot_td_22" id="no_of_q_td" runat="server">5</td>
                                      <td class="ot_td">Correct Answers</td>
                                      <td class="ot_td_22" id="cor_ans_td" runat="server">2</td>
                                 </tr>
                                  <tr>
                                     <td class="ot_td">No Attempt</td>
                                      <td class="ot_td_22" id="no_att_td" runat="server">1</td>
                                      <td class="ot_td">Wrong Answers</td>
                                      <td class="ot_td_22" id="wrong_ans_td" runat="server">2</td>
                                 </tr>                                 
                             </table>
                            
                         </div>

                     <div class="sum_up">
                             <table class="otomon_tbl">
                                 <tr>
                                     <td class="ot_td">Total Percent Obtained</td>
                                      <td class="ot_td_22" id="per_obt" runat="server">40</td>
                                     
                                 </tr>                                                     
                             </table>
                            
                         </div>
                        
                          <div style="display:inline-block;width:100%;
height:auto;">
                         <div id="ex_pie" class="ex_pie">

                         </div>
                         <div class="ans_btn">
                             <button class="ans_bt" id="download_ans" runat="server" onserverclick="download_ans2" type="button" >Download Answers</button>
                         </div>
</div>
                        

                         </div>
                      <div class="test_info" id="Exam_info" runat="server" visible="false">
                          <div class="fal_tes_rep" id="Exam_name" runat="server"></div>
                          <div class="desi_info" id="exam_start_date" runat="server"></div>
                          <div class="desi_info" id="exam_end_date" runat="server"></div>
                          <div class="desi_info" id="timing" runat="server"></div>
                          <div class="desi_info" id="s_time" runat="server"></div>                        
                          <div class="desi_info" id="q_num" runat="server"></div>
                          <div class="e_note">Please Re-login / refresh page in case 'Start Test' button is not appearing during exam available time.</div>
                      </div>

                </div>


             

               <div class="hall_view">
<div class="auth_panel">
                        <div class="auth_panel2" id="auth_view" runat="server">
                            <div class="dry_div">
                                <div class="dry_div2">Pending Stuffs</div>
                            </div>
                        
                        </div>
                         <div class="new_auth_panel" id="new_auth_panel" runat="server" style="position:relative;">
                             <div class="title_div">User Change Request</div>
                             <div class="inner_div">
                             <table class="tbl_summary">
                                 <tr>
                                 <td class="tdx1" id="code_old" runat="server"></td>
                                   <td class="tdx1" id="code_new" runat="server"></td>
                                    </tr>
                                    <tr>
                                 <td class="tdx1">Old Record</td>
                                   <td class="tdx1">New Record</td>
                                    </tr>
                                   <tr>
                                 <td class="tdx1" id="name_old" runat="server"></td>
                                   <td class="tdx1" id="name_new" runat="server"></td>
                                    </tr>
                                    <tr>
                                 <td class="tdx1" id="des_old" runat="server"></td>
                                   <td class="tdx1" id="des_new" runat="server"></td>
                                    </tr>
                                  <tr>
                                 <td class="tdx1" id="loc_old" runat="server"></td>
                                   <td class="tdx1" id="loc_new" runat="server"></td>
                                    </tr>
                                   <tr>
                                 <td class="tdx1" id="email_old" runat="server"></td>
                                   <td class="tdx1" id="email_new" runat="server"></td>
                                    </tr>
                                  <tr>
                                 <td class="tdx2" id="mob_old" runat="server"></td>
                                   <td class="tdx2" id="mob_new" runat="server"></td>
                                    </tr>
                             </table>
                                 </div>
                             <div class="title_div"><button type="button" class="apr_btn" id="aprove_req" runat="server" onserverclick="approve_customer_request">Approve</button> 
                                 <button class="bkc_btn" onclick="show_request();">Back</button></div>
                         </div>
                    </div>
                   <div class="small_notify" id="small_notice" runat="server">
                       <div class="small_box">
                           <div class="small_logo">
                               <img src="Resources/Vectors/lion.png" class="small_img_lg" />
                           </div>
                           <div class="small_info">
                               The report will provide the data for CASA Deposit/Loan Accounts opened during the period (including RD Accounts).
                           </div>
                           <div class="small_color">Recently Added</div>
                       </div>

                   </div>

               </div>

   <div class="other_stuff_div">
                    <%-- <div class="E_Heading">EOD and System state data</div>--%>
<div class="eod_stuffs" id="eod_stuffs" runat="server">
   <div class="cub1">
       <div class="cub1_heading">UAT</div>
       <table class="cub1tbl">
           <tr>
               <td class="cub1td1">System Running Date:</td>
                <td class="cub1td2">12-12-2024</td>
           </tr>
            <tr>
               <td class="cub1td1">Last EOD Date:</td>
                <td class="cub1td2">11-12-2024</td>
           </tr>
           <tr>
               <td class="cub1td1">Last EOD Status:</td>
                <td class="cub1td2"><span class="cub1spng">Completed</span></td>
           </tr>
       </table>
      
   </div>
    <div class="cub1">
       <div class="cub1_heading">SIT</div>
       <table class="cub1tbl">
           <tr>
               <td class="cub1td1">System Running Date:</td>
                <td class="cub1td2">12-12-2024</td>
           </tr>
            <tr>
               <td class="cub1td1">Last EOD Date:</td>
                <td class="cub1td2">11-12-2024</td>
           </tr>
           <tr>
               <td class="cub1td1">Last EOD Status:</td>
                <td class="cub1td2"><span class="cub1spng">Completed</span></td>
           </tr>
       </table>
      
   </div>
    <div class="cub1">
       <div class="cub1_heading">PRODUCTION</div>
       <table class="cub1tbl">
           <tr>
               <td class="cub1td1">System Running Date:</td>
                <td class="cub1td2">12-12-2024</td>
           </tr>
            <tr>
               <td class="cub1td1">Last EOD Date:</td>
                <td class="cub1td2">11-12-2024</td>
           </tr>
           <tr>
               <td class="cub1td1">Last EOD Status:</td>
                <td class="cub1td2"><span class="cub1spng">Completed</span></td>
           </tr>
       </table>
      
   </div>
     <div class="cub1">
       <div class="cub1_heading">DEV DR</div>
       <table class="cub1tbl">
           <tr>
               <td class="cub1td1">System Running Date:</td>
                <td class="cub1td2">12-12-2024</td>
           </tr>
            <tr>
               <td class="cub1td1">Last EOD Date:</td>
                <td class="cub1td2">11-12-2024</td>
           </tr>
           <tr>
               <td class="cub1td1">Last EOD Status:</td>
                <td class="cub1td2"><span class="cub1spng">Completed</span></td>
           </tr>
       </table>
      
   </div>


</div>
                </div>

                 
                
             

                 

              <%--  <div class="batch_1" style="margin-top:50px;">               
                     
<asp:DropDownList id="srSelect1" runat="server" OnTextChanged="Select2_load_data_static" AutoPostBack="true" CssClass="drp_down">
        <asp:ListItem Value="1">January</asp:ListItem>
       <asp:ListItem Value="2">February</asp:ListItem>
    <asp:ListItem Value="3">March</asp:ListItem>
    <asp:ListItem Value="4">April</asp:ListItem>
    <asp:ListItem Value="5">May</asp:ListItem>
    <asp:ListItem Value="6">June</asp:ListItem>
   <asp:ListItem Value="7">July</asp:ListItem>
       <asp:ListItem Value="8">August</asp:ListItem>
    <asp:ListItem Value="9">September</asp:ListItem>
    <asp:ListItem Value="10">October</asp:ListItem>
    <asp:ListItem Value="11">November</asp:ListItem>
    <asp:ListItem Value="12">December</asp:ListItem>
    </asp:DropDownList>
         <asp:DropDownList id="srSelect2" runat="server" OnTextChanged="Select2_load_data_static" AutoPostBack="true" CssClass="drp_down"></asp:DropDownList>

                   
                    </div>--%>
            <%--    <div style="width:100%;height:300px;display:block;"> 
                    <div id="columnchart_material" runat="server" style="width: 40%; height: 300px;margin-left:9%;display:inline-block;float:left;">
                    </div>
                    <div  style="width: 40%; height: 300px;margin-left:9%;display:inline-block;">
                        <div class="batch_1">
                        <table class="tbl_summary_n">
                            <tr><td class="mx_cor" style="width:50%;">Category</td>
                                <td class="mx_cor" style="width:25%;">Actual</td>
                                <td class="mx_cor" style="width:25%;">Target</td>
                            </tr>
                             <tr><td class="mx_cor_x" style="width:50%;">Casa Accounts</td>
                                <td class="mx_cor_x" style="width:25%;" id="tbl_cs_act" runat="server">0</td>
                                <td class="mx_cor_x" style="width:25%;" id="tbl_sc_tar" runat="server">0</td>
                            </tr>
                             <tr><td class="mx_cor_x" style="width:50%;">Term Deposite Accounts</td>
                                <td class="mx_cor_x" style="width:25%;" id="tbl_td_act" runat="server">0</td>
                                <td class="mx_cor_x" style="width:25%;" id="tbl_td_tar" runat="server">0</td>
                            </tr>
                             <tr><td class="mx_cor_x" style="width:50%;">Term loan Accounts</td>
                                <td class="mx_cor_x" style="width:25%;" id="tbl_ln_act" runat="server">0</td>
                                <td class="mx_cor_x" style="width:25%;" id="tbl_ln_tar" runat="server">0</td>
                            </tr>
                        </table>
                            </div>
                         <div class="batch_1">
                             <div class="note_div_sec"><span style="font-weight:bold;color:#ff0000;">Note :</span> If Target values are not appearing then kindly go to the <span style="font-weight:bold;color:#21459b;">Analytics</span> section and set the Target values for the parameters.</div>

                         </div>
                    </div>
                </div>--%>
               
                
              
              
              
        
                </div>
            <div class="dashboard2" id="reports_sec">

                <div class="card_holder">
                    <div class="card1">
                       <a href="GeneralReport.aspx" target="_blank"> <img src="Resources/Vectors/lion.png"  class="card_img"/>
                        <div class="Hover_text">General Reports</div></a>
                    </div>
                     <div class="card2">
                         <a href="EOD_Stats.aspx" target="_blank"><img src="Resources/Vectors/EOD_logo.jpg" class="card_img" />
                         <div class="Hover_text2">EOD Logs Analysis</div></a>
                   </div>
                </div>

                <div class="card_holder">
                     <div class="card1">
                        <a href="Executive_reports.aspx" target="_blank"><img src="Resources/Vectors/graphs.png" class="card_img" />
                        <div class="Hover_text">Executive Reports</div></a>
                    </div>
                    
                     <div class="card2">
                         <a href="BankBranch.aspx" target="_blank"><img src="Resources/Vectors/bank operations.png" class="card_img" />
                         <div class="Hover_text2">Branch Operations</div></a>
                   </div>
                </div>
                <div class="card_holder">
                    <div class="card1">
                        <a href="Clearing_Reports.aspx" target="_blank"><img src="Resources/Vectors/Clearing logo.png" class="card_img" />
                        <div class="Hover_text">Clearing Reports</div></a>
                    </div>
                     <div class="card2">
                         <a href="Investment.aspx" target="_blank"><img src="Resources/Vectors/inflw.png" class="card_img" />
                         <div class="Hover_text2">Treasury SGL Recon</div></a>
                   </div>
                </div>
                 <div class="card_holder">
                   <div class="card1">
                        <a href="OFSS_Knowledge_Base.aspx" target="_blank"><img src="Resources/Vectors/tables.png" class="card_img" />
                        <div class="Hover_text">OFSS Tables</div></a>
                    </div>
                     <div class="card2">
                         <a href="cibil.aspx" target="_blank"><img src="Resources/Vectors/cibil.jpg" class="card_img" />
                         <div class="Hover_text2">CIBIL Data Request</div></a>
                   </div>
                </div>
                <div class="card_holder">
                     <div class="card1">
                         <a href="Booster.aspx" target="_blank"><img src="Resources/Vectors/ADF.jpg" class="card_img" />
                         <div class="Hover_text2">Booster Reports</div></a>
                   </div>
                </div>
            </div>           
              <div class="dashboard2" id="settings_p">
                  <div class="setting_p">
                      <div class="card_s">
                          <div class="card_part1">
                              <div class="upper_banner2">Personal Settings</div>
                              <div class="container_class">
                                  <div class="input_text_xl">
                                      <input type="text" class="input_text_in" value="" placeholder="Code" id="code_r_in" runat="server" disabled="disabled" />
                                  </div>
                                   <div class="input_text_xl">
                                      <input type="text" class="input_text_in" value="" placeholder="Name" id="name_r_in" runat="server"  />
                                  </div>
                                   <div class="input_text_xl">
                                      <input type="text" class="input_text_in" value="" placeholder="Email" id="email_r_in" runat="server"  />
                                  </div>
                                   <div class="input_text_xl">
                                      <input type="text" class="input_text_in" value="" placeholder="Mobile" id="mobile_r_in" runat="server"  />
                                  </div>
                                  <div class="input_text_xl">
                    <asp:DropDownList ID="ddl_designation"  class="user_input" runat="server" > 
                        <asp:ListItem Value="0" Text="--Please Select Designation--" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="1" Text="CSR"></asp:ListItem>
                         <asp:ListItem Value="2" Text="Manager"></asp:ListItem>
                     </asp:DropDownList></div>
                     <div class="input_text_xl">
                    <asp:DropDownList ID="ddllocation"  class="user_input" runat="server" OnSelectedIndexChanged="cmbActivity_SelectedIndexChanged" AutoPostBack="true"> 
                        <asp:ListItem Value="0" Text="--Please Select Location--" Selected="True" ></asp:ListItem>
                        <asp:ListItem Value="1" Text="CSR"></asp:ListItem>
                         <asp:ListItem Value="2" Text="Manager"></asp:ListItem>
</asp:DropDownList></div>
                                   <div class="input_text_xl">
                    <asp:DropDownList ID="ddlactAutho"  class="user_input" runat="server" > <asp:ListItem Value="0" Text="--Activity Authorization Employee--" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="1" Text="CSR"></asp:ListItem>
                         <asp:ListItem Value="2" Text="Manager"></asp:ListItem>
</asp:DropDownList></div>
                                    <div class="input_text_xl">
                                   <%--  <asp:Button ID="btn_req1" runat="server" Text="Request" BorderStyle="Solid" ToolTip="Submit" cssClass="button_req" onclick="insert_request_RR" /> --%>

                                    <button type="submit" class="button_req" id="btn_req" runat="server" onclick="check_data();"  onserverclick="insert_request_RR">Request</button>
                                  </div>
                              </div>
                          </div>
                          <div class="card_part1">

                               
                              <div class="upper_banner2">Change Password</div>
                              <div class="container_class">
                                  <div class="input_text_xl">
                                      <input type="password" class="input_text_in" value="" id="new_passwd" runat="server" placeholder="New Password" />
                                  </div>
                                   <div class="input_text_xl">
                                      <input type="password" class="input_text_in" value="" id="con_new_passwd" runat="server" placeholder="Confirm Password" />
                                  </div>
                                 
                                  <div class="input_text_xl">
                                    <button class="button_req" onclick="check_password();" runat="server" onserverclick="check_new_password">OK</button>
                                  </div>
                                   <div class="input_text_xl" id="lblerror2" runat="server" style="color:blue;font-family:'Courier New';" >
                                       <%--<label id="lblerror" runat="server" class="lbl_error_class"></label>--%>
                                       </div>
                              </div>
                          


                          </div>



                      </div>
                       <div class="card_s" id="mid1">
                            <div class="card_part1">
                                 <div class="upper_banner2">Notification Settings</div>
                                   <div class="container_class">
                                  <div class="input_text_xl" style="height:100px;">
                                 <textarea id="area_not" runat="server" placeholder="New Notification Detail" class="input_text_in" style="height:100px;"></textarea>
                                  </div>
                                  <div class="input_text_xl" style="margin-top:30px;">
                                         <asp:DropDownList ID="ddl_frequency"  class="user_input" runat="server" > <asp:ListItem Value="0" Text="--Notification Frequency--" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Daily"></asp:ListItem>
                         <asp:ListItem Value="2" Text="Weekly"></asp:ListItem>
                                              <asp:ListItem Value="3" Text="Fortnightly"></asp:ListItem>
                                              <asp:ListItem Value="4" Text="Monthly"></asp:ListItem>
                                              <asp:ListItem Value="5" Text="Quarterly"></asp:ListItem>
                                              <asp:ListItem Value="7" Text="Half Yearly"></asp:ListItem>
                                             <asp:ListItem Value="8" Text="Yearly"></asp:ListItem>
</asp:DropDownList>
                                  </div>
                                         <div class="input_text_xl" style="margin-top:30px;">                             
                                       Starting Date :   <asp:TextBox ID="from_date" runat="server" placeholder="Starting Date (mm/dd/yyyy)" Textmode="Date" ReadOnly = "false" CssClass="calender_cs"></asp:TextBox>
                                  </div>
                                        <div class="input_text_xl" style="height:100px;" >                                  
                                      <textarea placeholder="Direct Link (Optional)" class="input_text_in" style="height:100px;" id="area_link" runat="server"></textarea>
                                  </div>
                                        <div class="input_text_xl" style="margin-top:30px;">
                                           <label  style="display:inline-block;">Notify on Email : </label>   <input type="checkbox" class="input_text_in"  style="display:inline-block;width:30%;padding:5px;border:1px solid #ff0000" id="chk_email" runat="server" />                                            
                                         </div>
                                         <div class="input_text_xl" style="margin-top:30px;">
                                    <button type="button" class="button_req" onclick="check_password();" runat="server" onserverclick="Create_notification" id="btn_notify">Notify Me</button>
                                  </div>
                                       <div class="input_text_xl">
                                           <label id="rec"></label>
                                           <input type="hidden" id="hdn_edit" runat="server" value="0" />
                                       </div> 


                                  </div>


                                </div>
                             <div class="card_part1" style="overflow:auto;">

                           <asp:Table id="ActiArchtbl" runat="server" CssClass="report_table"></asp:Table>

                           </div>
                           </div>
                     



                     <div class="bottom_image_div">
                <img src="Resources/Vectors/wood.jpg" class="bottom_img" />
            </div>
                  </div>
              </div>
    
            <div class="dashboard2" id="analytics_div">
              <div class="banking_analysis"><div class="eight">  Analytical Params for Head Office
</div></div>
                

                   <%-- <div class="banking_analysis"><span class="banking_heading">Analytical Params for Head Office</span></div>
                    <div class="banking_analysis"><span class="banking_subheading">Set your Monthly Targets here and keep the track of your monthly achievements keep the track of your monthly achievements.</span></div>--%>

                    <div class="batch_1">               
                     
<asp:DropDownList id="Select1" runat="server" OnTextChanged="Select1_load_data_static" AutoPostBack="true" CssClass="drp_down">
        <asp:ListItem Value="1">January</asp:ListItem>
       <asp:ListItem Value="2">February</asp:ListItem>
    <asp:ListItem Value="3">March</asp:ListItem>
    <asp:ListItem Value="4">April</asp:ListItem>
    <asp:ListItem Value="5">May</asp:ListItem>
    <asp:ListItem Value="6">June</asp:ListItem>
   <asp:ListItem Value="7">July</asp:ListItem>
       <asp:ListItem Value="8">August</asp:ListItem>
    <asp:ListItem Value="9">September</asp:ListItem>
    <asp:ListItem Value="10">October</asp:ListItem>
    <asp:ListItem Value="11">November</asp:ListItem>
    <asp:ListItem Value="12">December</asp:ListItem>
    </asp:DropDownList>
         <asp:DropDownList id="select2" runat="server" OnTextChanged="Select1_load_data_static" AutoPostBack="true" CssClass="drp_down"></asp:DropDownList>

                   
                    </div>

                <div class="batch_1">

                    <table class="activity_tbl">
                     
                        <tr><th class="act_tbl_cat_3">Icon</th>
                            <th class="act_tbl_cat_2">Category</th>
                            <th class="act_tbl_cat_3">Month</th>
                            <th class="act_tbl_cat_2">Target Figure</th>
                            <th class="act_tbl_cat_3">Status</th>
                            <th class="act_tbl_cat_3">Set By</th>
                            <th class="act_tbl_cat_3">Edit By</th>
                             <th class="act_tbl_cat_3">-</th>
                        </tr>
                        <tr><td class="act_tbl_cat_td_3"><img src="Resources/Vectors/saving_fx.png" height="30" width="30"/></td><td class="act_tbl_cat_td_1">CASA Account Target</td>
                            <td class="act_tbl_cat_td_3" id="mon_cat1" runat="server"></td>
                            <td class="act_tbl_cat_td_2"><input type="text" id="in_cat1" runat="server" class="in_cat" onkeypress='validate(event)' /></td>
                            <td class="act_tbl_cat_td_3_red" id="stm_cat1" runat="server"></td>
                            <td class="act_tbl_cat_td_3" id="make_cat1" runat="server"></td>
                            <td class="act_tbl_cat_td_3" id="edit_cat1" runat="server"></td>
                             <td class="act_tbl_cat_td_3"><button class="btn_cls_mn" id="cas_cat1" runat="server" onserverclick="casa_insert" type="button">OK</button></td>
                        </tr>
                        <tr><td class="act_tbl_cat_td_3"><img src="Resources/Vectors/fd_x.png" height="30" width="30"/></td><td class="act_tbl_cat_td_1">Term Deposite Account Target</td>
                            <td class="act_tbl_cat_td_3"  id="mon_cat2" runat="server"></td>
                            <td class="act_tbl_cat_td_2"><input type="text" id="in_cat2" runat="server" class="in_cat"  onkeypress='validate(event)'/></td>
                            <td class="act_tbl_cat_td_3_green" id="stm_cat2" runat="server"></td>
                            <td class="act_tbl_cat_td_3" id="make_cat2" runat="server"></td>
                            <td class="act_tbl_cat_td_3"  id="edit_cat2" runat="server"></td>
                             <td class="act_tbl_cat_td_3"><button class="btn_cls_mn" id="cas_cat2" runat="server" onserverclick="td_insert" type="button">Edit</button></td>
                        </tr>
                         <tr><td class="act_tbl_cat_td_3"><img src="Resources/Vectors/loan_x.png" height="30" width="30"/></td><td class="act_tbl_cat_td_1">Term Loan Account Target</td>
                            <td class="act_tbl_cat_td_3"  id="mon_cat3" runat="server"></td>
                            <td class="act_tbl_cat_td_2"><input type="text" id="in_cat3" runat="server" class="in_cat" onkeypress='validate(event)' /></td>
                            <td class="act_tbl_cat_td_3_green" id="stm_cat3" runat="server"></td>
                            <td class="act_tbl_cat_td_3" id="make_cat3" runat="server"></td>
                            <td class="act_tbl_cat_td_3"  id="edit_cat3" runat="server"></td>
                             <td class="act_tbl_cat_td_3"><button class="btn_cls_mn" id="cas_cat3" runat="server" onserverclick="ln_insert" type="button">Edit</button></td>
                        </tr>
                    </table>
                </div>


            </div>


        </div>
        <%--  <div style="width:100%;height:auto;opacity:0.80;filter: alpha(opacity=80);background-color:#1e0f18;text-align:center;"><asp:Menu ID="Menu1" runat="server" Orientation="Horizontal" CssClass="menu">
            
              <LevelMenuItemStyles>
    <asp:MenuItemStyle CssClass="level1"/>
    <asp:MenuItemStyle CssClass="level2"/>
    <asp:MenuItemStyle CssClass="level3" />
  </LevelMenuItemStyles>
  
  <StaticHoverStyle CssClass="hoverstyle"/>
  
  <LevelSubMenuStyles>
    <asp:SubMenuStyle CssClass="sublevel1" />
  </LevelSubMenuStyles>

    </asp:Menu></div> 


        <div id="divmar1" Visible="false" runat="server" style="margin-top:20px;height:100px;background-color:white;"><marquee direction="left">
<div style="display:inline-block;float:left;"><img src="Resources/anim123.gif" style="width:100px;height:100px;" /></div><div style="display:inline-block;"><asp:Label ID="Mar1" runat="server" Font-Bold="True" Font-Names="Arial Black"

ForeColor="#109AC1" Text="News" CssClass="duck" Visible="false"></asp:Label></div>
</marquee></div>
          <div id="divmar2" Visible="false" runat="server" style="margin-top:10px;height:100px;background-color:white;"><marquee direction="left"><div style="display:inline-block;float:left;"><img src="Resources/cycle2.gif" style="width:100px;height:100px;" /></div><div style="display:inline-block;">
<asp:Label ID="Mar2" runat="server" Font-Bold="True" Font-Names="Arial Black"

ForeColor="Black" Text="News" CssClass="duck" Visible="false"></asp:Label></div>
</marquee></div>
          <div id="divmar3" Visible="false" runat="server" style="margin-top:10px;height:100px;background-color:#fbfbf1;"><marquee direction="left" value="Slower"><div style="display:inline-block;float:left;"><img src="Resources/tiger.gif" style="width:100px;height:100px;" /></div><div style="display:inline-block;">
<asp:Label ID="Mar3" CssClass="duck" runat="server" Font-Bold="True" Font-Names="Arial Black"

ForeColor="#109AC1" Text="News" Visible="false"></asp:Label></div>
</marquee></div>
    <div style="margin-top:100px;text-align:center;align-content:space-between;">
    
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="POS CB" CssClass="homebtn" Visible="false"/>
    
        <%--<asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="E@learning" CssClass="homebtn" />--%>  <%--Enabled="false" BackColor="Yellow"--%>

        <%--  </div>--%>

        <footer>

            <div class="new_footer"><b>Copyright© 2019,BCCB-IT</b></div>
        </footer>
      <%-- </ContentTemplate>
      </asp:UpdatePanel>--%>

     </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="download_ans"></asp:PostBackTrigger>
                </Triggers>
      </asp:UpdatePanel>
    </form>
</body>
</html>
