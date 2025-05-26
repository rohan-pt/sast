<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HotDB.aspx.cs" Inherits="BCCBExamPortal.HotDB" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <link rel="icon" href="Resources/bccb_logo.png"/>
    <title></title>
     <link href="HotDb.css" rel="stylesheet" />
     <link href="Employee.css" rel="stylesheet" />
</head>
<body style="background:url(/Resources/bgsky.jpg) no-repeat center center fixed;-webkit-background-size: cover;-moz-background-size: cover; -o-background-size: cover; background-size: cover;background-size: 100% 100%;">
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
    <div style="width:100%;height:70px;text-align:center;align-content:space-between;opacity: 0.8;filter: alpha(opacity=80);margin-top:100px;">

        <asp:Button runat="server" ID="btnacc" Text="Account" CssClass="btnfuse" OnClick="btnacc_Click" />
           <asp:Button runat="server" ID="btnmob" Text="Mobile" CssClass="btnfuse" OnClick="btnmob_Click" />
           <asp:Button runat="server" ID="btncard" Text="Card" CssClass="btnfuse" OnClick="btncard_Click" />

    </div>
    <div runat="server" id="divmob" style="opacity: 0.8;filter: alpha(opacity=80);width:100%;text-align:center;margin-top:30px;" visible="false">

        <input type="text" runat="server" id="txtmob"  style="width:20%;height:20px;color:#ff0000;text-align:center;" placeholder="Mobile Number"/>
    </div>
     <div runat="server" id="divcard" visible="false" style="opacity: 0.8;filter: alpha(opacity=80);width:100%;text-align:center;margin-top:30px;">

        <input type="text" runat="server" id="txtcard"  style="width:20%;height:20px;color:#ff0000;text-align:center;" placeholder="Card Number"/>
    </div>
     <div runat="server" id="divacc" visible="false" style="opacity: 0.8;filter: alpha(opacity=80);width:100%;text-align:center;margin-top:30px;">
       <div style="margin-top:20px;"><asp:DropDownList runat="server" ID="ddlbranch" CssClass="ddl" AutoPostBack = "true" ><asp:ListItem Value="0" Text="--Please Select Location--" Selected="True"></asp:ListItem></asp:DropDownList></div>
         <div style="margin-top:20px;"><asp:DropDownList runat="server" ID="ddlaactype" CssClass="ddl" AutoPostBack = "true"></asp:DropDownList></div>
         
         
           <div style="margin-top:20px;"> <input type="text" runat="server" id="txtaccno"  style="width:20%;height:20px;color:#ff0000;text-align:left;" placeholder="Card Number"/></div>
       



    </div>
    <div style="text-align:center;margin-top:40px;">
        <asp:Button runat="server" ID="btndetail" Text=" Card Details " Visible="false" CssClass="btnfuse" OnClick="btndetail_Click" Width="130px"/>

    </div>
     <div style="text-align:center;margin-top:40px;">
        <asp:Button runat="server" ID="btnBlock" Text=" Hot Mark Card " Visible="false" CssClass="glow" OnClick="btnBlock_Click"/>

    </div>
     <div style="text-align:center;margin-top:20px;margin-bottom:20px;width:100%;">


            <asp:Label runat="server" ID="lblerror" Text="" CssClass="lolo"></asp:Label>

        </div>
    <div>
        <asp:GridView ID="Gridtbl" runat="server" CssClass="grid" AutoGenerateColumns="false" HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White">
               <HeaderStyle BackColor="#00BCD4"  ForeColor="White" />            
               <Columns>
                     <asp:TemplateField HeaderText="Select Data" ItemStyle-Width="5%">  
                    <EditItemTemplate>  
                        <asp:CheckBox ID="CheckBox1" runat="server" />  
                    </EditItemTemplate>  
                    <ItemTemplate>  
                        <asp:CheckBox ID="CheckBox1" runat="server" />  
                    </ItemTemplate>  
                </asp:TemplateField>  
                   <asp:BoundField HeaderText="Name" 
                DataField="LongName" 
                  ItemStyle-Width="29%"></asp:BoundField>
                     <asp:BoundField HeaderText="Card No" 
                DataField="CardId" 
                  ItemStyle-Width="21%"></asp:BoundField>
                     <asp:BoundField HeaderText="Card Status" 
                DataField="CardStatus" 
                 ItemStyle-Width="20%"></asp:BoundField>
                     <asp:BoundField HeaderText="Account No" 
                DataField="AcctNum" 
                  ItemStyle-Width="15%"></asp:BoundField>
                     <asp:BoundField HeaderText="Branch" 
                DataField="LBrCode" 
                 ItemStyle-Width="10%"></asp:BoundField>
          <%--  <asp:TemplateField HeaderText="Name" HeaderStyle-Width="25%" ItemStyle-Width="25%">
                <ItemTemplate>
                  <%#Eval("LongName") %>
                </ItemTemplate>--%>
               <%--</asp:TemplateField>
           <asp:TemplateField HeaderText="Card No" HeaderStyle-Width="25%" ItemStyle-Width="25%">
                <ItemTemplate>
                    <%#Eval("CardId") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Card Status" HeaderStyle-Width="20%" ItemStyle-Width="20%">
                <ItemTemplate>
                    <%#Eval("CardStatus") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Account No" HeaderStyle-Width="20%" ItemStyle-Width="20%">
                 <ItemTemplate>
                    <%#Eval("AcctNum") %>
                 </ItemTemplate>
             </asp:TemplateField>
             <asp:TemplateField HeaderText="Branch" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                 <ItemTemplate>
                     <%#Eval("LBrCode") %>
                 </ItemTemplate>
             </asp:TemplateField>--%>
          
             
             </Columns>   

        </asp:GridView>

       

        <%--<table style="width:70%;margin-left:15%;text-align:center;opacity:0.80;filter: alpha(opacity=80);background-color:black;border:solid 1px #ffffff;color:aqua;">
            <tr>
                 <td class="tdtbl" style="width:8%">Select</td>
                <td class="tdtbl" style="width:25%">Card number</td>
                 <td class="tdtbl" style="width:10%">Card Status</td>
                 <td class="tdtbl" style="width:10%">Branch Code</td>
                <td class="tdtbl" style="width:10%">Account Number</td>
                 <td class="tdtbl" style="width:37%">Name</td>
            </tr>
             <tr>
                  <td class="tdtbl" style="width:8%"><asp:CheckBox runat="server" ID="chk" Text="1"   /></td>
                 <td class="tdtbl" style="width:25%">6076485100613016</td>
                 <td class="tdtbl" style="width:10%">ACTIVE</td>
                 <td class="tdtbl" style="width:10%">8</td>
                <td class="tdtbl" style="width:10%">SB/18569</td>
                 <td class="tdtbl" style="width:37%">ABHIJEET KASHINATH GHARAT</td>
                
            </tr>
        </table>--%>

    </div>

  </div> 
 <footer>
       
        <div style="text-align:center;margin-top:100px;width:100%;height:50px;font-family:Calibri;font-size:medium;bottom:0;left:0;position:sticky;background-color:#e5d6f3;vertical-align:central;line-height:50px;color:#1b392e;font-size:medium;"><b>Website Design and Developed by BCCB IT Department.      Copyright© 2019,BCCB</b></div></footer>
    </form>
</body>
</html>
