<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SMS.aspx.cs" Inherits="BCCBExamPortal.SMS" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">   
    <title>SMS Updation Reports</title>
     <link rel="icon" href="Resources/bccb_logo.png"/>
     <link href="SMS.css" rel="stylesheet" />
     <link href="Employee.css" rel="stylesheet" />
     <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css"/>
  <link rel="stylesheet" href="/resources/demos/style.css"/>
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
  <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <script type="text/javascript">
        google.charts.load("current", { packages: ["corechart"] });
        function callone(xx) {
            //alert(xx);
            google.charts.setOnLoadCallback(drawChart);
            function drawChart() {

                  var res = xx.split(",");
                var data = new google.visualization.DataTable();
                data.addColumn('string', 'SMS form type');
                data.addColumn('number', 'Number of forms');
                data.addRows([
           ['Successfully Done', parseInt(res[0])],
           ['New Registration', parseInt(res[1])],
           ['Delete Create', parseInt(res[2])],
           ['Already Done', parseInt(res[3])],
           ['IB Customer', parseInt(res[4])],
               ['Unknown Reason', parseInt(res[5])],
               ['Double Entry Updated', parseInt(res[6])]
                ]);

                var options = {
                    title: 'SMS Update Analysis',
                    is3D: true,
                };

                var chart = new google.visualization.PieChart(document.getElementById('piechart_3d'));
                chart.draw(data, options);
            }
        }
     
    </script>
    <script>
  //function Encode() {     
  // document.getElementById('txquestion1').value =document.getElementById('txquestion1').value.replace(/</g,'&lt;').replace(/>/g,'&gt;');;
  //}
 </script>
  <script>
  $( function() {
      $("#datepicker1").datepicker();
     
  });
  $(document).ready(function () {
      $("#txtNumofQue").keydown(function (e) {
          // Allow: backspace, delete, tab, escape, enter and .
          if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
              // Allow: Ctrl+A
              (e.keyCode == 65 && e.ctrlKey === true) ||
              // Allow: home, end, left, right, down, up
              (e.keyCode >= 35 && e.keyCode <= 40)) {
              // let it happen, don't do anything
              return;
          }
          // Ensure that it is a number and stop the keypress
          if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
              e.preventDefault();
          }
      });
  });
  $(document).ready(function () {
      $("#txttime").keydown(function (e) {
          // Allow: backspace, delete, tab, escape, enter and .
          if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
              // Allow: Ctrl+A
              (e.keyCode == 65 && e.ctrlKey === true) ||
              // Allow: home, end, left, right, down, up
              (e.keyCode >= 35 && e.keyCode <= 40)) {
              // let it happen, don't do anything
              return;
          }
          // Ensure that it is a number and stop the keypress
          if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
              e.preventDefault();
          }
      });
  });
  </script>
</head>
<body>
    <form id="form1" runat="server">
         <asp:ScriptManager ID="scriptmanager1" runat="server">

</asp:ScriptManager>
         
    <div class="intro_cls">
    SMS DASHBOARD <asp:Label ID="sessionlbl" runat="server" Visible="false" Text=""></asp:Label>
    </div>
          <div style="width:100%;height:auto;opacity:0.80;filter: alpha(opacity=80);border-radius:20px;background-color:#f35ab8;text-align:center;"><asp:Menu ID="Menu1" runat="server" Orientation="Horizontal" CssClass="menu">
  <%--  <Items>
        <asp:MenuItem Text="Item 1" Value="Item 1">
            <asp:MenuItem Text="Subitem 1" Value="Subitem 1"></asp:MenuItem>
            <asp:MenuItem Text="Subitem 2" Value="Subitem 2"></asp:MenuItem>
            <asp:MenuItem Text="Subitem 3" Value="Subitem 3"></asp:MenuItem>
        </asp:MenuItem>
        <asp:MenuItem Text="Item 2" Value="Item 2">
            <asp:MenuItem Text="Subitem 1" Value="Subitem 1"></asp:MenuItem>
            <asp:MenuItem Text="Subitem 2" Value="Subitem 2"></asp:MenuItem>
            <asp:MenuItem Text="Subitem 3" Value="Subitem 3"></asp:MenuItem>
            <asp:MenuItem Text="Subitem 4" Value="Subitem 4"></asp:MenuItem>
        </asp:MenuItem>    </Items>--%>
        <%--<StaticMenuStyle  CssClass="staticdiv"  />
        <StaticMenuItemStyle CssClass="staticdiv" />
        <StaticHoverStyle CssClass="staticdiv" />
        <DynamicHoverStyle CssClass="staticdivhover" />
        <DynamicMenuStyle CssClass="staticdiv"/>
        <StaticSelectedStyle BackColor="#EEEEEE" />
        <DynamicMenuItemStyle CssClass="staticdiv" />--%>
             
            
             
            
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
<div class="div_s1"> 
    <div class="div_s11">      
        <asp:TextBox runat="server" ID="datepicker1" class="papopa" placeholder="Start Date" OnTextChanged="get_date_data" AutoPostBack="true"></asp:TextBox>
    </div>
    <div class="div_s11">  
       <div class="div_s111"><input type="text" runat="server" id="txt_search" class="txt_inp_num" placeholder="Mobile Number"/></div> 
      <div class="div_s111"><asp:ImageButton runat="server" ID="imgbtn" ImageUrl="~/Resources/search.jpg" class="img_btn" OnClick="check_mobile" AutoPostBack="true"/></div>  

    </div>
</div>
<div class="div_s1" style="margin-top:20px;"> 
    <div class="div_s11"> <asp:DropDownList runat="server" ID="ddlbranch" CssClass="ddl_br_stl" AutoPostBack = "true" >     
              <asp:ListItem Value="0" Text="--Please Select Location--" Selected="True"></asp:ListItem>
    </asp:DropDownList></div>
    <div class="div_s11">    
     <div class="div_s111"><input type="text" runat="server" id="Text1" class="txt_inp_num" placeholder="Customer Number"/></div> 
      <div class="div_s111"><asp:ImageButton runat="server" ID="ImageButton1" ImageUrl="~/Resources/search.jpg" class="img_btn" OnClick="check_cust_no" AutoPostBack="true"/></div>  
     </div>
</div>
         <asp:UpdatePanel ID="updatepnl" runat="server">

<ContentTemplate> 
    <div style="width:100%;" ><asp:Label ID="lbl_response" runat="server" CssClass="res_cl"></asp:Label></div>
<div class="div_s2">

    <div class="div_s21" id="piechart_3d" runat="server">

    </div>
     <div class="div_s21" runat="server" id="result_tbl">
         
         <%--<asp:Table class="tbl_result" id="tbl_result5" runat="server">--%>
             <%--<tr>
                 <td class="tbl_th">Mobile Number</td>
                  <td class="tbl_th">Mobile Number</td>
                  <td class="tbl_th">Mobile Number</td>
                  <td class="tbl_th">Mobile Number</td>
             </tr>
             <tr>
                 <td class="tbl_td">8888888888</td>
                  <td class="tbl_td">8888888888</td>
                  <td class="tbl_td">8888888888</td>
                  <td class="tbl_td"><span style="color:#ff0000;">888888888</span></td>
             </tr>--%>
       <%--  </asp:Table>--%>
    </div>
</div>
         </ContentTemplate>

</asp:UpdatePanel>
    </form>
</body>
</html>
