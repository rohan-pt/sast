<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Short.aspx.cs" Inherits="BCCBExamPortal.Short" validateRequest="false" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <link rel="icon" href="Resources/bccb_logo.png"/>
     <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <link rel="preconnect" href="https://fonts.googleapis.com">
<link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
<link href="https://fonts.googleapis.com/css2?family=Kanit:ital,wght@0,100;0,200;0,300;0,400;0,500;0,600;0,700;0,800;0,900;1,100;1,200;1,300;1,400;1,500;1,600;1,700;1,800;1,900&display=swap" rel="stylesheet">
<link href="https://fonts.googleapis.com/css2?family=Ubuntu:ital,wght@0,300;0,400;0,500;0,700;1,300;1,400;1,500;1,700&display=swap" rel="stylesheet">
    <link href="https://fonts.googleapis.com/css2?family=Space+Mono:ital,wght@0,400;0,700;1,400;1,700&display=swap" rel="stylesheet">
      <link href="Admin.css" rel="stylesheet" />
    <link href="Resources/CSS/short.css" rel="stylesheet" />
    <link href="Resources/CSS/commoncss.css" rel="stylesheet" />
    <script src="Resources/Script/short.js"></script>
    <title>MINI LEDGER</title>
</head>
<body>
    <form id="form1" runat="server">
          <div class="heading">
     Administrative Panel
        <asp:Label ID="sessionlbl" runat="server" Visible="false" Text=""></asp:Label>       
    </div>
     <div class="mainblock">
         <div class="control">
              
             <div><asp:Button runat="server" Text="Upload new test" ID="btnTNT" CssClass="menubtn" OnClick="btnTNT_Click"/></div>
        <div ><asp:Button runat="server" Text="Active Tests" ID="btnAT" CssClass="menubtn" OnClick="btnAT_Click"/></div>
        <div><asp:Button runat="server" Text="Archive Tests" ID="btnATR" CssClass="menubtn" OnClick="btnATR_Click"/></div>
        <div><asp:Button runat="server" Text="Analysis" ID="btnAnalys" CssClass="menubtn" OnClick="btnAnalys_Click"/></div>
        <div><asp:Button runat="server" Text="Admin Settings" ID="btnSettings" CssClass="menubtn" OnClick="btnSettings_Click"/></div>
       <div><asp:Button runat="server" Text="Add Marquee" ID="addmarquee" CssClass="menubtn" OnClick="addmarquee_Click"  /></div>
           <div><asp:Button runat="server" Text="Employee Settings" ID="Button1" CssClass="menubtn" OnClick="Button1_Click" /></div>
             <div><asp:Button runat="server" Text="EOD APP Settings" ID="Button4" CssClass="menubtn" OnClick="EOD_Click" /></div>
       <div><asp:Button runat="server" Text="Mini Ledger Book" ID="btn_mini" CssClass="menubtn1" OnClick="mini_ledge_clk" /></div>
            <div><asp:Button runat="server" Text="General Reports" ID="generalrep_btn" CssClass="menubtn" OnClick="general_Click"  /></div>
         <div><asp:Button runat="server" Text="Graph and Table" ID="btn_rp_tbl" CssClass="menubtn" OnClick="grp_tbl_Click"  /></div>
        <div><asp:Button runat="server" Text="Users" ID="Button2" CssClass="menubtn" OnClick="general_users"  /></div>
         <div><asp:Button runat="server" Text="Log Out" ID="btnLogOut" CssClass="menubtn" OnClick="btnLogOut_Click"/></div>
        </div>
         <div class="body_div">

<div class="tab_btn_div">
    <div class="tab_box" id="tab1" onclick="show_div2();">Add Main GL</div>
    <div class="tab_box_d" id="tab2">Add Secondary GL</div>   <%--onclick="show_div1();"--%>
</div>
<div id="main_div_ox">
             <div class="ddl_holder_div">
                 <asp:DropDownList ID="ddl_category" runat="server" CssClass="css_category" OnSelectedIndexChanged="fetch_Main_data" AutoPostBack="true">
                     <asp:ListItem Value="0" Text="--PLEASE SELECT CATEGORY OF GLs--"></asp:ListItem>
                     <asp:ListItem Value="1" Text="ASSET"></asp:ListItem>
                     <asp:ListItem Value="2" Text="LIABILITY"></asp:ListItem>
                     <asp:ListItem Value="3" Text="INCOME"></asp:ListItem>
                     <asp:ListItem Value="4" Text="EXPENDITURE"></asp:ListItem>
                     <asp:ListItem Value="5" Text="CONTIGENT ASSET"></asp:ListItem>
                     <asp:ListItem Value="6" Text="CONTIGENT LIABILITY"></asp:ListItem>
                 </asp:DropDownList>
             </div>

              <div class="ddl_holder_div">
                 <asp:DropDownList ID="ddl_currency" runat="server" CssClass="css_category">
                     <asp:ListItem Value="0" Text="--PLEASE SELECT CURRENCY TYPE--"></asp:ListItem>
                     <asp:ListItem Value="1" Text="INDIAN RUPEE"></asp:ListItem>
                     <asp:ListItem Value="2" Text="US DOLLAR"></asp:ListItem>
                     <asp:ListItem Value="3" Text="GREAT BRITAIN POUND"></asp:ListItem>
                     <asp:ListItem Value="4" Text="EURO"></asp:ListItem>                    
                 </asp:DropDownList>
             </div>

             <div class="add_new_main_gl">
                 <textarea type="text" id="txt_name" runat="server" class="newgl_txt" placeholder="Main GL Name" />
             </div>
             <div class="add_new_main_gl">
                 <textarea type="text" id="note" runat="server" class="newgl_txt" placeholder="User / Admin Note (optional)" />
             </div>
             <div class="button_grid">
                 <button type="button" class="button_1" id="add_btn" runat="server" onserverclick="insert_Main_data">ADD DATA</button>
                 <button type="button" class="button_1" id="reser_m1" runat="server" onserverclick="reset_M1_data">RESET</button>
                 <input type="hidden" id="hdn_main_gl" runat="server" />
             </div>
             <div class="view_master_tbl">
                  <asp:Table id="tbl_xx1" runat="server" CssClass="table_mst_cls">

                           </asp:Table>            
               <%--  <table class="table_mst_cls">
                     <tr>
                         <td class="std_td d1">SR NO</td>
                         <td class="std_td d2">Node Name</td>
                         <td class="std_td d3">Creator/Editor</td>
                         <td class="std_td d3">Create/Edit date</td>
                         <td class="std_td d3">AB1</td>
                            <td class="std_td d3">AB2</td>
                     </tr>
                     <tr>
                         <td class="std_td1 d1">1</td>
                         <td class="std_td1 d2">CASH HANDLING CHARGES</td>
                         <td class="std_td1 d3">Created By : 08560</td>
                         <td class="std_td1 d3">24/09/2024</td>
                         <td class="std_td1 d3"><button class="n_btn">Edit</button></td>
                         <td class="std_td1 d3"><button class="n_btn">Delete</button></td>
                     </tr>
                      <tr>
                         <td class="std_td1 d1">1</td>
                         <td class="std_td1 d2">CASH HANDLING CHARGES</td>
                         <td class="std_td1 d3">Created By : 08560</td>
                         <td class="std_td1 d3">24/09/2024</td>
                         <td class="std_td1 d3"><button class="n_btn">Edit</button></td>
                         <td class="std_td1 d3"><button class="n_btn">Delete</button></td>
                     </tr>
                      <tr>
                         <td class="std_td1 d1">1</td>
                         <td class="std_td1 d2">CASH HANDLING CHARGES</td>
                         <td class="std_td1 d3">Created By : 08560</td>
                         <td class="std_td1 d3">24/09/2024</td>
                         <td class="std_td1 d3"><button class="n_btn">Edit</button></td>
                         <td class="std_td1 d3"><button class="n_btn">Delete</button></td>
                     </tr>
                 </table>--%>

             </div>
             <div class="flying_dutch">
                 <table class="flying_tbl" id="flying_tbl" runat="server">
                     <tr><td class="tm1">Total Count</td></tr>
               <tr><td class="tm2" id="tot_cnt" runat="server"></td></tr>
                 </table>
               <%--  <div class="user_msg" id="user_msg" runat="server" >
                     Hi I am here.
                 </div>--%>
                 <div class="op_info" visible="false" id="op_info" runat="server">
                    <%-- <p class="p">Edit Mode....................: Enable</p>
                     <p class="p">Node Name....................: Cash handling chanrges</p>--%>
                 </div>

             </div>
    </div>

<div id="second_data_div" style="display:none;">
    <div class="info_box_tab">
         <div class="op_info">
                     <div class="px" id="s_mode" runat="server">Mode.........: Add New GL</div>
                     <div class="px" id="s_curr" runat="server">Currency Code: INR</div>
                     <div class="px" id="n_name" runat="server">Node Name....: Cash handling chanrges</div>             
                 </div>
    </div>
     <div class="add_new_main_gl">
                 <textarea type="text" id="txtarea_gl_logic" runat="server" class="newgl_txt" placeholder="GL Logic (eg. GL Code + GL Code - GL Code)" onkeypress='return isNumberKey(event);' />
             </div>
     <div class="add_new_main_gl">
                 <textarea type="text" id="txtarea_gl_name" runat="server" class="newgl_txt" placeholder="GL Name" />
             </div>
             <div class="add_new_main_gl">
                 <textarea type="text" id="txtarea_note" runat="server" class="newgl_txt" placeholder="User / Admin Note (optional)" />
             </div>
     <div class="button_grid">
         <input type="hidden" runat="server" id="hdn_sub_id" />
         <input type="hidden" runat="server" id="sub_currency" />
         <input type="hidden" runat="server" id="sub_s_id" />
                 <button type="button" class="button_1" id="add_sub_data" runat="server" onserverclick="insert_sub_data">ADD DATA</button> 
         <button type="button" id="reset_sub_data" runat="server" onserverclick="reset_btn_sub" class="button_1">RESET</button>
             </div>
     <div class="view_master_tbl">
           <asp:Table id="ActiArchtbl" runat="server" CssClass="table_mst_cls">

                           </asp:Table>            


            <%--     <table class="table_mst_cls">
                     <tr>
                         <td class="std_td d1">SR NO</td>
                         <td class="std_td d2">GL Name</td>
                         <td class="std_td d3">Creator/Editor</td>
                         <td class="std_td d3">Create/Edit date</td>
                         <td class="std_td d3">AB1</td>
                            <td class="std_td d3">AB2</td>
                     </tr>
                     <tr>
                         <td class="std_td1 d1">1</td>
                         <td class="std_td1 d2">CASH HANDLING CHARGES</td>
                         <td class="std_td1 d3">Created By : 08560</td>
                         <td class="std_td1 d3">24/09/2024</td>
                         <td class="std_td1 d3"><button class="n_btn">Edit</button></td>
                         <td class="std_td1 d3"><button class="n_btn">Delete</button></td>
                     </tr>
                      <tr>
                         <td class="std_td1 d1">1</td>
                         <td class="std_td1 d2">CASH HANDLING CHARGES</td>
                         <td class="std_td1 d3">Created By : 08560</td>
                         <td class="std_td1 d3">24/09/2024</td>
                         <td class="std_td1 d3"><button class="n_btn">Edit</button></td>
                         <td class="std_td1 d3"><button class="n_btn">Delete</button></td>
                     </tr>
                      <tr>
                         <td class="std_td1 d1">1</td>
                         <td class="std_td1 d2">CASH HANDLING CHARGES</td>
                         <td class="std_td1 d3">Created By : 08560</td>
                         <td class="std_td1 d3">24/09/2024</td>
                         <td class="std_td1 d3"><button class="n_btn">Edit</button></td>
                         <td class="std_td1 d3"><button class="n_btn">Delete</button></td>
                     </tr>
                 </table>--%>

             </div>
     <div class="flying_dutch">
                <%-- <table class="flying_tbl">
                     <tr><td class="tm1">Total Count</td></tr>
               <tr><td class="tm2">100</td></tr>
                 </table>--%>
                 <div class="op_info" id="sbu_info_desk" runat="server">
                     <%--<p class="p">Edit Mode....................: Enable</p>
                     <p class="p">Node Name....................: Cash handling chanrges</p>--%>
                 </div>

             </div>
</div>

         </div>

         </div>
         <footer>
       
        <div style="text-align:center;margin-top:100px;width:100%;height:50px;font-family:Calibri;font-size:medium;bottom:0;left:0;position:sticky;background-color:#e5d6f3;vertical-align:central;line-height:50px;color:#1b392e;font-size:medium;"><b>Website Design and Developed by BCCB IT Department.      Copyright© 2019,BCCB</b></div>

         </footer>
    </form>
</body>
</html>
