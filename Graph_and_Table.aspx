<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Graph_and_Table.aspx.cs" Inherits="BCCBExamPortal.Graph_and_Table" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <link rel="icon" href="Resources/bccb_logo.png"/>
    <title>Graph and Tables</title>
      <link href="Admin.css" rel="stylesheet" />
     <link href="Resources/CSS/commoncss.css" rel="stylesheet" />
         <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css"/>
  <link rel="stylesheet" href="/resources/demos/style.css"/>
    <script src="Resources/Script/General.js"></script>
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
  <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <link href="Resources/CSS/tableandgr.css" rel="stylesheet" />
</head>
<body>
   <form id="form1" runat="server">
   
    <div class="heading">
    BCCB Administrative Panel
        <asp:Label ID="sessionlbl" runat="server" Visible="false" Text=""></asp:Label>
        <asp:Label ID="Examlbl" runat="server" Visible="false" Text=""></asp:Label>
    </div>
    <div class="mainblock_2" style="position:relative;">
        <div class="control" style="height:600px;">
        <div><asp:Button runat="server" Text="Update Test" ID="btnTNT" CssClass="menubtn" OnClick="btnTNT_Click" /></div>
        <div ><asp:Button runat="server" Text="Active Tests" ID="btnAT" CssClass="menubtn" OnClick="btnAT_Click" /></div>
        <div><asp:Button runat="server" Text="Archive Tests" ID="btnATR" CssClass="menubtn" OnClick="btnATR_Click" /></div>
        <div><asp:Button runat="server" Text="Analysis" ID="btnAnalys" CssClass="menubtn" OnClick="btnAnalys_Click" /></div>
        <div><asp:Button runat="server" Text="Admin Settings" ID="btnSettings" CssClass="menubtn" OnClick="btnSettings_Click" /></div>
            <div><asp:Button runat="server" Text="Add Marquee" ID="addmarquee" CssClass="menubtn" OnClick="addmarquee_Click"  /></div>
           <div><asp:Button runat="server" Text="Employee Settings" ID="Button2" CssClass="menubtn" OnClick="Button1_Click" /></div>
            <div><asp:Button runat="server" Text="EOD APP Settings" ID="Button4" CssClass="menubtn" OnClick="EOD_Click" /></div>
       <div><asp:Button runat="server" Text="Mini Ledger Book" ID="btn_mini" CssClass="menubtn" OnClick="mini_ledge_clk" /></div>
             <div><asp:Button runat="server" Text="Graph and Table" ID="btn_rp_tbl" CssClass="menubtn1" OnClick="grp_tbl_Click"  /></div>
         <div><asp:Button runat="server" Text="General Reports" ID="generalrep_btn" CssClass="menubtn" OnClick="general_Click"  /></div>
              <div><asp:Button runat="server" Text="Web Settings" ID="Button3" CssClass="menubtn" OnClick="web_Click"  /></div>
        <div><asp:Button runat="server" Text="Users" ID="Button1" CssClass="menubtn" OnClick="general_users"  /></div>
          <div><asp:Button runat="server" Text="Log Out" ID="btnLogOut" CssClass="menubtn" OnClick="btnLogOut_Click" />
                           </div>
                           </div>
     
        <div class="content">
       
          <div class="main">
    <div class="card">
        <div class="title">TABLE OR GRAPH</div>
        <div class="form">
            <label><input type="radio" class="input-radio off" checked="true" name="pili" runat="server" id="tbl_chk" onchange="radio_change();" onserverchange="radio_change"/> TABLE</label>
            <label><input type="radio" class="input-radio on"  name="pili" runat="server" id="tbl_grp" onchange="radio_change();" onserverchange="radio_change"/> GRAPH</label>
        </div>
    </div>
</div> 
              <div class="main">
    <div class="card" style="width:80%;margin-left:5%;margin-top:50px;">
         <div class="main" style="margin-top:30px;">
            <textarea placeholder="Report Name" id="Rep_name" runat="server" class="textarea_cl" style="height:90px;"></textarea>
</div>
            </div>
                  </div>
         <div class="main" style="margin-top:60px;">
          <div class="card" style="width:80%;">
        <div class="title">Database Connection</div>
        <div class="form">
             <label><input type="radio" class="input-radio on" checked="true" name="db1" id="chk_mis" runat="server"/> MIS</label>
             <label><input type="radio" class="input-radio on" name="db1" id="chk_prod" runat="server"/> Production</label>
            <label><input type="radio" class="input-radio on" name="db1" id="chk_eom" runat="server"/>LAST EOM</label>
            <label><input type="radio" class="input-radio on"  name="db1" id="chk_dm" runat="server"/> DATA MART</label>
        </div>
    </div>
</div>
    
         <div class="main" style="margin-top:60px;">
          <div class="card" style="width:80%;">
        <div class="title" style="color:#ff6a00;">Report Type</div>
        <div class="form">
            <div class="boxes">
  <input type="checkbox" id="chk1" runat="server">
  <label for="chk1">CASA Reports</label>

  <input type="checkbox" id="chk2" runat="server" checked="true">
  <label for="chk2">Loan Reports </label>

  <input type="checkbox" id="chk3" runat="server">
  <label for="chk3">Term Deposit Reports</label>
</div>
 
        </div>
    </div>
</div>
 <div class="main" style="margin-top:60px;">
          <div class="card" style="width:80%;">
       <div style="width:100%;height:auto;">
  <asp:DropDownList ID="ddl_dep"  class="empfields" runat="server"> <asp:ListItem Value="0" Text="--Select Department--" Selected="True">

                                                                   </asp:ListItem>
</asp:DropDownList> 
       </div>
 
        </div>
    </div>

        <div class="main" style="margin-top:30px;">
            <textarea placeholder="Query" id="query_txt" runat="server" class="textarea_cl"></textarea>
</div>
         <div class="main" style="margin-top:30px;">
            <textarea placeholder="Query for Branch (Use ####$$$$ for branch code (Numeric))" id="query_br" runat="server" class="textarea_cl"></textarea>
</div>
         <div class="main" style="margin-top:30px; justify-content: left;
    align-items: initial;">
           <label class="rep_lbl" style="margin-left:10%;">No of Columns in the Query : <span id="no_col" runat="server"></span> </label>
</div>
         <div class="main" style="margin-top:30px; justify-content: left;
    align-items: initial;"">
        <label class="switch">
  <input type="checkbox" id="full_width" runat="server"/>
  <span class="slider round"></span>
</label>  <label class="rep_lbl" style="margin-left:2%;color:#0094ff;">100% Display width </label>
</div>
         <div class="main" style="margin-top:30px; justify-content: left;
    align-items: initial;">
           <input type="text" class="cls_rep" placeholder="New Column name (Comma Separated)" id="new_col" runat="server" />
</div>
                 <div class="main" style="margin-top:30px; justify-content: left;
    align-items: initial;">
           <input type="text" class="cls_rep" placeholder="Column Space Style %  (Comma Separated: e.g :: 25,45,20,10) (Compulsory for tables)" id="new_style" runat="server" />
</div>
         <div class="main" style="margin-top:30px;">
             <button type="button" class="btn_validate" id="val_btn" runat="server" onserverclick="validate_design"> Validate Design</button>
         </div>
         <div class="main" style="margin-top:30px; justify-content: left;
    align-items: initial;">
           <label class="rep_lbl" style="margin-left:10%;">Report Design Status : <span id="error_stat" runat="server"></span> </label>
</div>
          <div class="main" style="margin-top:60px;" id="tbl_sel_div" runat="server">
          <div class="card" style="width:80%;">
        <div class="title">Table Theme (Only for Tables)</div>
        <div class="form">
             <label><input type="radio" class="input-radio on" checked="true" name="pilih" id="chk_plane" runat="server"/> Plane</label>
             <label><input type="radio" class="input-radio on" name="pilih" id="chk_blue" runat="server"/> Blue</label>
            <label><input type="radio" class="input-radio on" name="pilih" id="chk_hot_cho" runat="server"/> Hot Chocolate</label>
            <label><input type="radio" class="input-radio on"  name="pilih" id="chk_ice_creame" runat="server"/> Ice Cream</label>
        </div>
    </div>
</div>
        <div class="main" style="margin-top:60px;display:none;" id="tbl_graph_div" runat="server">
          <div class="card" style="width:80%;">
        <div class="title">Graph Theme (for Graphs)</div>
        <div class="form">
             <label><input type="radio" class="input-radio on" checked="true" name="graphi" id="chk_pie" runat="server"/>Pie Chart</label>
             <label><input type="radio" class="input-radio on" name="graphi" id="chk_bar" runat="server"/>Bar Graph</label>
            <label><input type="radio" class="input-radio on" name="graphi" id="chk_column" runat="server"/>Column Graph</label>
            <label><input type="radio" class="input-radio on"  name="graphi" id="chk_donut" runat="server"/>Donut</label>
        </div>
    </div>
</div>
         <div class="main" style="margin-top:30px;">
            <textarea placeholder="Note (Optional)" id="txt_note" runat="server" class="textarea_cl" style="height:90px;"></textarea>
</div>
        
        <div class="main" style="margin-top:60px;">
          <div class="card" style="width:80%;">
        <div class="title">Report Status</div>
        <div class="form">
             <label><input type="radio" class="input-radio on" checked="true" name="Act" id="rep_id_a" runat="server"/> Active</label>
             <label><input type="radio" class="input-radio on" name="Act" id="rep_id_d" runat="server"/> Dormant</label>           
        </div>
    </div>
</div>
         <div class="main" style="margin-top:30px;">
             <button class="btn_validate" onclick="data_validation_create_report();" id="Create_report_btn" runat="server" onserverclick="create_report" disabled="disabled">Create Report</button>
             <input type="hidden" id="hdn_edit" runat="server" value="0" />
         </div>
        
         <div class="main" style="margin-top:30px;" id="table_div">
             <asp:Table id="ActiArchtbl" runat="server" CssClass="Fetch_tbl"></asp:Table>
             </div>
         <div class="main" style="margin-top:30px;display:none;" id="graph_div">
             <asp:Table id="ActiArchtblGraph" runat="server" CssClass="Fetch_tbl"></asp:Table>
             </div>
       
        </div> 
    </div>

        <footer>
       
        <div class="foot_one"><b>Website Design and Developed by BCCB IT Department.      Copyright© 2019,BCCB</b></div></footer>
    </form>
</body>
</html>