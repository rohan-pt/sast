<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Testing.aspx.cs" Inherits="BCCBExamPortal.Testing" EnableViewState="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
  <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <link href="Resources/CSS/Testing.css?v=2" rel="stylesheet" />
    <script src="Resources/Script/Testing.js?v=2"></script>
    <title>Knowledge Test</title>
</head>
<body>
    <form id="form1" runat="server">
         <asp:ScriptManager ID="scriptmanager1" runat="server">

</asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate> 
               
        <div class="header_solve">
          <asp:Label ID="lblExamNo" runat="server"  Visible="false" ></asp:Label>
            <asp:Label ID="sessionlbl" runat="server" Visible="false"></asp:Label>
            <svg  width="200" height="120" viewBox="0 0 200 120" style="margin-left:10%;display:inline-block;float:left;">
                 <rect x="55" y="5" height="70" width="80" stroke="black" fill="#e9e7e7" rx="7" ry="7"></rect>
                 <rect x="60" y="10" height="50" width="70" stroke="black" fill="#b6ff00"></rect>
                 <rect x="70" y="20" height="10" width="15" stroke="blue" fill="yellow"></rect>
                 <rect x="70" y="40" height="10" width="15" stroke="blue" fill="yellow"></rect>
                 
                 <circle r="2" cx="70" cy="67" fill="green" />
                 <circle r="2" cx="75" cy="67" fill="green" />
                 <circle r="2" cx="80" cy="67" fill="green" />

                 <circle r="2" cx="110" cy="67" fill="green" />
                 <circle r="2" cx="115" cy="67" fill="green" />
                 <circle r="2" cx="120" cy="67" fill="green" />
                 <rect x="87" y="74" height="15" width="15" stroke="black" fill="#e9e7e7"></rect>
                  <rect x="75" y="90" height="5" width="40" stroke="black" fill="#e9e7e7"></rect>
                 <line x1="100" y1="20" x2="120" y2="20" stroke="black"></line>
                  <line x1="100" y1="30" x2="120" y2="30" stroke="black"></line>
                 <line x1="100" y1="25" x2="120" y2="25" stroke="black"></line>


                  <line x1="100" y1="40" x2="120" y2="40" stroke="black"></line>
                  <line x1="100" y1="45" x2="120" y2="45" stroke="black"></line>
                 <line x1="100" y1="50" x2="120" y2="50" stroke="black"></line>
                       <text x="45" y="110" style="font-family: monospace; font-size:1em;fill:#145f67;">Knowledge Test</text>               
             </svg>

            <div style="display:inline-block;float:left;width:50%;height:120px;/*border:1px solid #ff0000;*/">
                <table class="tbl_data">
                    <tr>
                        <td class="td1_type">Employee Name:</td>
                        <td class="td2_type" id="Emp_name" runat="server"></td>
                        <td class="td1_type">Employee Code:</td>
                        <td class="td2_type" id="Emp_code" runat="server"></td>
                    </tr>
                     <tr>
                        <td class="td1_type">Designation:</td>
                        <td class="td2_type" id="Emp_des" runat="server"></td>
                         <td class="td1_type">     
                     <button class="square_btn_pln"></button></td>
                        <td class="td2_type" runat="server" id="tdNotVisited"></td>
                    </tr>
                     <tr>
                        <td class="td1_type">Email Id:</td>
                        <td class="td2_type" id="Emp_email" runat="server"></td>
                        <td class="td1_type"><button class="square_btn_answered"></button>
                    </td>
                        <td class="td2_type" runat="server" id="tdAns"></td>
                    </tr>
                     <tr>
                        <td class="td1_type">Location:</td>
                        <td class="td2_type" id="Emp_Location" runat="server"></td>
                         <td class="td1_type"> <button class="square_btn_not_answered"></button></td>
                        <td class="td2_type" runat="server" id="tdNotAns"></td>
                    </tr>
                </table>
            </div>
            <img src="Resources/Vectors/bccb.jpg" style="width:10%;height:100px;margin-left:5%;display:inline-block;float:left;" />

        </div>
             </ContentTemplate>

</asp:UpdatePanel>
         <div class="timer_div">Time Left : <span id="timeleft" class="normal_time"></span>
             <input type="hidden" id="Hidden1" runat="server" clientidnode="Static"  />
         </div>
         <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate> 
        <div class="exm_name" id="divEname" runat="server"></div>
        <div class="sap_div"><div class="q_sec" id="divqtrack" runat="server"></div>
            <asp:ImageButton src="Resources/Vectors/next2.jpg"  ID="back_btn" runat="server" OnClick="btnBack_Click"  CssClass="nxt_btn"/>
              <asp:ImageButton src="Resources/Vectors/next1.jpg" ID="next_btn" runat="server"  OnClick="btnNext1_Click" CssClass="nxt_btn"/>
       <%--<img src="Resources/Vectors/next2.jpg" class="nxt_btn" /> --%>
           <%-- <img src="Resources/Vectors/next1.jpg" class="nxt_btn"/>--%>
        </div>
        <div class="main_body_sec">
            <button class="fixed_btn" id="submit_btn" runat="server" visible="false" onserverclick="submit_exam" type="button">Submit Exam</button>
            <div class="question_sec_div">
                <div class="normal_question" id="divquestionname" runat="server">
                    
                </div>
                <input type="hidden" id="hdn_response" runat="server" />
                <div class="option_sec_div">
                    <div class="dum_div">A</div>
                    <label class="option_lbl" id="lbl_op1" runat="server">
                        <input type="radio" id="option1" runat="server" name="option" onchange="toggleParentClass(this,'A')"/>
                        <span runat="server" id="divop1"></span>
                    </label>
                </div>
                  <div class="option_sec_div">
                      <div class="dum_div">B</div>
                    <label class="option_lbl"  id="lbl_op2" runat="server">
                        <input type="radio" id="option2" runat="server" name="option" onchange="toggleParentClass(this,'B')"/>
                        <span runat="server" id="divop2"></span>
                    </label>
                </div>
                <div class="option_sec_div" >
                    <div class="dum_div">C</div>
                    <label class="option_lbl" id="lbl_op3" runat="server">
                        <input type="radio" id="option3" runat="server" name="option" onchange="toggleParentClass(this,'C')"/>
                        <span runat="server" id="divop3"></span>
                    </label>
                </div>
                <div class="option_sec_div" >
                    <div class="dum_div">D</div>
                    <label class="option_lbl" id="lbl_op4" runat="server">
                        <input type="radio" id="option4" runat="server" name="option" onchange="toggleParentClass(this,'D')" />
                        <span runat="server" id="divop4"></span>
                    </label>
                </div>
            </div>
            <div class="q_number_sec" id="divnavigation" runat="server" enableviewstate="true">
                <asp:PlaceHolder ID="btn_holder" runat="server" EnableViewState="true" ></asp:PlaceHolder>
              <%--  <div class="disk_div">
                    <button class="round_btn_selected">1</button>
                     <button class="round_btn_answered">2</button>
                     <button class="round_btn_not_answered">3</button>
                     <button class="round_btn_pln">4</button>
                     <button class="round_btn_pln">5</button>
                </div>
                  <div class="disk_div">
                    <button class="round_btn_selected">1</button>
                     <button class="round_btn_answered">2</button>
                     <button class="round_btn_not_answered">3</button>
                     <button class="round_btn_pln">4</button>
                     <button class="round_btn_pln">5</button>
                </div>
                  <div class="disk_div">
                    <button class="round_btn_selected">1</button>
                     <button class="round_btn_answered">2</button>
                     <button class="round_btn_not_answered">3</button>
                     <button class="round_btn_pln">4</button>
                     <button class="round_btn_pln">5</button>
                </div>
                  <div class="disk_div">
                    <button class="round_btn_selected">1</button>
                     <button class="round_btn_answered">2</button>
                     <button class="round_btn_not_answered">3</button>
                     <button class="round_btn_pln">4</button>
                     <button class="round_btn_pln">5</button>
                </div>
                 <div class="disk_div">
                    <button class="round_btn_selected">1</button>
                     <button class="round_btn_answered">2</button>
                     <button class="round_btn_not_answered">3</button>
                     <button class="round_btn_pln">4</button>
                     <button class="round_btn_pln">5</button>
                </div>
                  <div class="disk_div">
                    <button class="round_btn_selected">1</button>
                     <button class="round_btn_answered">2</button>
                     <button class="round_btn_not_answered">3</button>
                     <button class="round_btn_pln">4</button>
                     <button class="round_btn_pln">5</button>
                </div>
                  <div class="disk_div">
                    <button class="round_btn_selected">1</button>
                     <button class="round_btn_answered">2</button>
                     <button class="round_btn_not_answered">3</button>
                     <button class="round_btn_pln">4</button>
                     <button class="round_btn_pln">5</button>
                </div>
                  <div class="disk_div">
                    <button class="round_btn_selected">10</button>
                     <button class="round_btn_answered">20</button>
                     <button class="round_btn_not_answered">30</button>
                     <button class="round_btn_pln">40</button>
                     <button class="round_btn_pln">50</button>
                </div>--%>
            </div>
        </div>
        <div class="footer">
            <img src="Resources/bccb_logo.png" class="footer_logo"/>
             <svg  width="700" height="50" viewBox="0 0 700 50" style="margin-left:5%;display:inline-block;float:left;">
               <%--  <rect x="55" y="5" height="70" width="80" stroke="black" fill="#e9e7e7" rx="7" ry="7"></rect>--%>
                 <text x="45" y="30" style="font-family: 'Lucida Sans'; font-size:1.5em;fill:#70980c;">Copyrights © 2019 Bassein Catholic Co-op Bank </text>               
             </svg>
            <img src="Resources/bccb_logo.png" class="footer_logo" style="margin-left:0;"/>
        </div>
            <div class="error_handle" id="error_handle" style="display:none;">
                <div class="logo_holder">
                 <svg  width="200" height="120" viewBox="0 0 200 120" style="display:block;margin-left:40%;">
                 <rect x="55" y="5" height="70" width="80" stroke="black" fill="#e9e7e7" rx="7" ry="7"></rect>
                 <rect x="60" y="10" height="50" width="70" stroke="black" fill="#b6ff00"></rect>
                 <rect x="70" y="20" height="10" width="15" stroke="blue" fill="yellow"></rect>
                 <rect x="70" y="40" height="10" width="15" stroke="blue" fill="yellow"></rect>
                 
                 <circle r="2" cx="70" cy="67" fill="green" />
                 <circle r="2" cx="75" cy="67" fill="green" />
                 <circle r="2" cx="80" cy="67" fill="green" />

                 <circle r="2" cx="110" cy="67" fill="green" />
                 <circle r="2" cx="115" cy="67" fill="green" />
                 <circle r="2" cx="120" cy="67" fill="green" />
                 <rect x="87" y="74" height="15" width="15" stroke="black" fill="#e9e7e7"></rect>
                  <rect x="75" y="90" height="5" width="40" stroke="black" fill="#e9e7e7"></rect>
                 <line x1="100" y1="20" x2="120" y2="20" stroke="black"></line>
                  <line x1="100" y1="30" x2="120" y2="30" stroke="black"></line>
                 <line x1="100" y1="25" x2="120" y2="25" stroke="black"></line>


                  <line x1="100" y1="40" x2="120" y2="40" stroke="black"></line>
                  <line x1="100" y1="45" x2="120" y2="45" stroke="black"></line>
                 <line x1="100" y1="50" x2="120" y2="50" stroke="black"></line>
                 <text x="45" y="110" style="font-family: monospace; font-size:1em;fill:#145f67;">Knowledge Test</text>               
             </svg>
                </div>
                <div class="abs_info_holder">
                    <div class="errors_dis" id="start_instruct" runat="server">
                       <%-- <div class="header_war" style="display:inline-block;line-height:40px;background:#d9eea6; display:none;">
                             <svg  width="50" height="40" viewBox="0 0 50 40" style="display:inline-block;margin-left:10%;float:left;">
                                  <polygon points="25,5 5,35 45,35" class="triangle" /> 
                                 <line x1="25" y1="13" x2="25" y2="27" stroke="black" stroke-width="3"></line>
                                  <circle r="2" cx="25" cy="31" fill="black" />
                                 </svg>
                            
                                <span style="width:50%;display:inline-block;float:left;font-size:1.5em;font-family:'Courier New';color:#ff0000;">Network Error Warning</span>
                        </div>--%>
                        <div class="header_war" style="display:inline-block;line-height:40px;background:#94daf1;">
                        <svg  width="50" height="40" viewBox="0 0 50 40" style="display:inline-block;margin-left:10%;float:left;">
                        <circle r="15" cx="25" cy="20" stroke="black" fill="#5cdc63" />
                        <line x1="25" y1="18" x2="25" y2="33" stroke="black" stroke-width="3"></line>
                        <circle r="2" cx="25" cy="13" fill="black" />
                        </svg>                            
                        <span style="width:50%;display:inline-block;float:left;font-size:1.5em;font-family:'Courier New';color:#145f67;">Exam Information</span>
                        </div>
                        <div class="header_war war_add">
                             <svg  width="25" height="25" viewBox="0 0 25 25" style="display:inline-block;margin-left:10%;float:left;"> 
                                  <circle r="5" cx="12" cy="12" fill="black" />
                                 </svg>
                                <span style="width:80%;display:inline-block;float:left;font-size:1.3em;font-family: Calibri;color:#000000;min-height:40px; margin-left: 5%;">
                                  This is because the event handling events happen after Page Init. Of course the button has to have been created before the events can detected on it. The joy of web forms... 
                                    You MUST initialize dynamically created controls inside the OnInit() method (see MS kb post), or the page will not consider it.
                                </span>
                        </div>
                        <div class="header_war war_add">
                             <svg  width="25" height="25" viewBox="0 0 25 25" style="display:inline-block;margin-left:10%;float:left;">                               
                                
                                  <circle r="5" cx="12" cy="12" fill="black" />
                                 </svg>
                            
                                <span style="width:80%;display:inline-block;float:left;font-size:1.3em;font-family:'Courier New';color:#000000;min-height:40px; margin-left: 5%;">
                                  This is because the event handling events happen after Page Init. Of course the button has to have been created before the events can detected on it. The joy of web forms... 
                                    You MUST initialize dynamically created controls inside the OnInit() method (see MS kb post), or the page will not consider it.

                                </span>
                        </div>
                        <div class="header_war war_add">
                             <svg  width="25" height="25" viewBox="0 0 25 25" style="display:inline-block;margin-left:10%;float:left;">                               
                                
                                  <circle r="5" cx="12" cy="12" fill="black" />
                                 </svg>
                            
                                <span style="width:80%;display:inline-block;float:left;font-size:1.3em;font-family:'Courier New';color:#000000;min-height:40px; margin-left: 5%;">
                                  This is because the event handling events happen after Page Init. Of course the button has to have been created before the events can detected on it. The joy of web forms... 
                                    You MUST initialize dynamically created controls inside the OnInit() method (see MS kb post), or the page will not consider it.

                                </span>
                        </div>
                        <div class="header_war war_add">
                             <svg  width="25" height="25" viewBox="0 0 25 25" style="display:inline-block;margin-left:10%;float:left;">                               
                                
                                  <circle r="5" cx="12" cy="12" fill="black" />
                                 </svg>
                            
                                <span style="width:80%;display:inline-block;float:left;font-size:1.3em;font-family:'Courier New';color:#000000;min-height:40px; margin-left: 5%;">
                                  This is because the event handling events happen after Page Init. Of course the button has to have been created before the events can detected on it. The joy of web forms... 
                                    You MUST initialize dynamically created controls inside the OnInit() method (see MS kb post), or the page will not consider it.

                                </span>
                        </div>

                    </div>

                    <div class="header_war" style="margin-top:50px;">
                        <button type="button" class="btn_ok_clk" style="display:none;" id="btn_error_ok" onclick="redirect_home();">OK</button>
                         <button type="button" class="btn_ok_clk" onclick="start_exam();" id="btn_accept">Accept</button>
                    </div>

                </div>

            </div>


            </ContentTemplate>

</asp:UpdatePanel>    
    </form>
</body>
</html>
