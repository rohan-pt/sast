<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowPMScheme.aspx.cs" Inherits="BCCBExamPortal.ShowPMScheme" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link rel="icon" href="Resources/bccb_logo.png"/>
  <link href="NewComing.css" rel="stylesheet" />
    <link href="Employee.css" rel="stylesheet" />
         <link href="HotDb.css" rel="stylesheet" />
    <link rel="stylesheet" href="/Resources/Styles/bootstrap.min.css" />
    <link rel="stylesheet" href="/Resources/Styles/dataTables.bootstrap5.min.css" />
    <link rel="stylesheet" href="/Resources/Styles/CommonStyles.css" />
    <style>
        input[type=text] {
        width:20%;height:30px;color:#830945;text-align:center;margin-left:2%;background-color:#bc9ce5;font-size:large;font-family:'Times New Roman', Times, serif;
        }
    </style>
</head>
<body style="background-color:#f6b4c8;">
    <form id="form1" runat="server">
    <div>
     <div style="width:100%;height:auto;background-color:#2d3c2c;font-family:'Times New Roman', Times, serif;font-size: xx-large;text-align:center;padding:10px;border-radius:50px;color:#ffffff;">Debit Card Panel<asp:Label ID="sessionlbl" runat="server" Visible="false" Text=""></asp:Label></div>
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
        <h2 style="text-align: center">Show PM Scheme</h2>
        <div style="text-align:center;width:100%;margin-top:10px;align-content:space-between;" class="schemeTypes">
            <button id="btn_PMSBY" class="btnsoft" onclick="return selectTypeOfScheme(this)">PMSBY-RS-12</button>
            <button id="btn_PMJBY" class="btnsoft" onclick="return selectTypeOfScheme(this)">PMJJBY-RS-330</button>
        </div>
        <div style="text-align: center; width: 100%; margin-top: 30px; align-content: space-between">
            <input type="text" id="txt_custNo" placeholder="Customer Number" />
        </div>
        <div style="text-align: center; width: 100%; margin-top: 30px;">OR</div>
        <div style="text-align:center;width:100%;margin-top:30px;align-content:space-between;">

         <asp:DropDownList runat="server" ID="ddlbranch" CssClass="ddlstyle" ><asp:ListItem Value="" Text="--Please Select Location--" Selected="True"></asp:ListItem></asp:DropDownList>
<asp:DropDownList runat="server" ID="ddlaactype" CssClass="ddlstyle1">
    <asp:ListItem Value="" Text="--Please Select Account Type--"></asp:ListItem>
</asp:DropDownList>
            <input type="text" runat="server" id="txtaccno1"  style="" placeholder="Account Number"/> 
            
            <input type="text" runat="server" id="txtAppId"  style="" placeholder="Appl Id"/> 

        </div>
        <div style="text-align:center;width:100%;margin-top:30px;">
            <button id="btnfind" class="btnsoft" onclick="return getRecord()">Get Record</button>
        </div>
        <div style="text-align:center;width:100%;margin-top:30px;">

            <asp:Label runat="server" ID="lblmsg" Text="" CssClass="bobo"></asp:Label>
        </div>



     <div id="tableContainer" style="width:100%;margin-top:25px;">


    </div>


      
    </div>
         <footer>
       
        <div style="text-align:center;margin-top:100px;width:100%;height:50px;font-family:Calibri;font-size:medium;bottom:0;left:0;position:sticky;background-color:#e5d6f3;vertical-align:central;line-height:50px;color:#1b392e;font-size:medium;"><b>Website Design and Developed by BCCB IT Department.      Copyright© 2019,BCCB</b></div></footer>
    </form>
    <div id="loading"><img src="Resources/loading.gif" /></div>
    <script src="/Resources/Script/jquery.min.js"></script>
    <script src="/Resources/Script/bootstrap.min.js"></script>
    <script src="/Resources/Script/dataTables.bootstrap5.min.js"></script>
    <script src="/Resources/Script/jquery.dataTables.min.js"></script>
    <script src="/Resources/Script/ShowPMScheme.js"></script>
    <script src="/Resources/Script/CommonScripts.js"></script>
    <script>
        var pmSchemePageType = "show";
    </script>
</body>
</html>