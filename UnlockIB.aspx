<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UnlockIB.aspx.cs" Inherits="BCCBExamPortal.UnlockIB" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="NewComing.css" rel="stylesheet" />
    <link href="Employee.css" rel="stylesheet" />
</head>
<body style="background:url(/Resources/inter.jpg) no-repeat center center fixed;-webkit-background-size: cover;-moz-background-size: cover; -o-background-size: cover; background-size: cover;background-size: 100% 100%;">
    <form id="form1" runat="server">
    <div>
     <div style="width:100%;height:auto;background-color:#2d3c2c;font-family:'Times New Roman', Times, serif;font-size: xx-large;text-align:center;padding:10px;border-radius:50px;color:#ffffff;opacity:0.80;filter: alpha(opacity=80);">Debit Card Panel<asp:Label ID="sessionlbl" runat="server" Visible="false" Text=""></asp:Label></div>
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
        <div style="width:100%;height:50px;text-align:center;margin-top:100px;">
            <input type="text" runat="server" id="txtcardnumber"  style="width:20%;height:20px;color:#ff0000;text-align:center;opacity:0.90;filter: alpha(opacity=90);" placeholder="Customer Number"/>
            <asp:Button ID="Button1" runat="server" Text="OK" CssClass="btndetail" OnClick="Button1_Click" />
            <asp:Button ID="Button2" runat="server" Text="Reset" CssClass="btndetail" OnClick="Button2_Click"  />

        </div>
        <div style="text-align:center;"> <asp:Label runat="server" ID="lblmsg" Text="" Visible="false"></asp:Label></div>
        <div style="width:100%;height:auto;margin-top:30px;">
            <table style="text-align:center;width:50%;height:auto;margin-left:25%;font-family:Narkisim;font-size:larger;opacity:0.90;filter: alpha(opacity=90);"> <tr>
                  <td style="width:100%;background-color:#726729;color:white;">Customer's Name</td>
              </tr>
               <tr>
                  <td style="width:100%;height:30px;color:#467853;" runat="server" id="tdname">-</td>
              </tr></table>
           
          <table style="text-align:center;width:50%;height:auto;margin-left:25%;font-family:Narkisim;opacity:0.90;filter: alpha(opacity=90);" >
              
              <tr>
                  <td style="width:50%;background-color:#726729;color:white;" >Customer Number</td>
                  <td style="width:50%;background-color:#726729;color:white;">Mobile Number</td>
              </tr>
               <tr>
                  <td style="width:50%;height:30px;color:#467853;" runat="server" id="tdcustnum">-</td>
                  <td style="width:50%;height:30px;color:#467853;" runat="server" id="tdmobno">-</td>
              </tr>
                <tr>
                  <td style="width:50%;background-color:#726729;color:white;" >Status</td>
                  <td style="width:50%;background-color:#726729;color:white;">Login Id Status</td>
              </tr>
               <tr>
                  <td style="width:50%;height:30px;color:#467853;" runat="server" id="tdstatus">-</td>
                  <td style="width:50%;height:30px;color:#467853;" runat="server" id="tdlogstat">-</td>
              </tr>
          </table>

        </div>
        <div style="text-align:center;margin-top:30px;">
            <asp:Button runat="server" CssClass="btnunlock" ID="btnunblock" Text="Unblock IB" Visible="false" OnClick="btnunblock_Click"/>

        </div>
    </div>
         <footer>
       
        <div style="text-align:center;margin-top:100px;width:100%;height:50px;font-family:Calibri;font-size:medium;bottom:0;left:0;position:sticky;background-color:#e5d6f3;vertical-align:central;line-height:50px;color:#1b392e;font-size:medium;"><b>Website Design and Developed by BCCB IT Department.      Copyright© 2019,BCCB</b></div></footer>
    </form>
</body>
</html>
