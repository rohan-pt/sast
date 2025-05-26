<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StopPMScheme.aspx.cs" Inherits="BCCBExamPortal.StopPMScheme" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
  <link href="NewComing.css" rel="stylesheet" />
    <link href="Employee.css" rel="stylesheet" />
         <link href="HotDb.css" rel="stylesheet" />

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
        <div style="text-align:center;width:100%;margin-top:30px;align-content:space-between;">

<asp:Button runat="server" ID="btn12" Text="PMSBY-RS-12"  CssClass="btnsoft" OnClick="btn12_Click"/>
            <asp:Button runat="server" ID="btn330" Text="PMJJBY-RS-330" CssClass="btnsoft" OnClick="btn330_Click"/>
        <div style="text-align:center;width:100%;margin-top:30px;align-content:space-between;">
            <asp:TextBox ID="txt_custNo" runat="server" placeholder="Customer No" style="width:20%;height:30px;color:#830945;text-align:center;margin-left:2%;background-color:#bc9ce5;font-size:large;font-family:'Times New Roman', Times, serif;"></asp:TextBox>
        </div>
        <p style="text-align: center">OR</p>
        <div style="text-align:center;width:100%;margin-top:30px;align-content:space-between;">

         <asp:DropDownList runat="server" ID="ddlbranch" CssClass="ddlstyle" AutoPostBack = "true" Height="53px" ><asp:ListItem Value="0" Text="--Please Select Location--" Selected="True"></asp:ListItem></asp:DropDownList>
<asp:DropDownList runat="server" ID="ddlaactype" CssClass="ddlstyle1" AutoPostBack = "true"></asp:DropDownList>
            <input type="text" runat="server" id="txtaccno1"  style="width:20%;height:30px;color:#830945;text-align:center;margin-left:2%;background-color:#bc9ce5;font-size:large;font-family:'Times New Roman', Times, serif;" placeholder="Account Number"/> 

        </div>

        </div>
        <div style="text-align:center;width:100%;margin-top:30px;">
            <asp:Button ID="btnfind" runat="server" Text="Get Record" cssClass="btnsoft" OnClick="btnfind_Click"/>
        </div>
        <div style="text-align:center;width:100%;margin-top:30px;">

            <asp:Label runat="server" ID="lblmsg" Text="" CssClass="bobo"></asp:Label>
        </div>



     <div style="width:100%;margin-top:25px;">
         <center>
            <asp:CheckBox ID="chk_selectAll" runat="server" Checked="false" Visible="false" Text="Select All" /> 
            &nbsp;&nbsp;&nbsp;<asp:Label ID="lbl_rowCount" runat="server" Visible="false"></asp:Label>
         </center>
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
                     <asp:BoundField HeaderText="Cust No" 
                DataField="CustNo" 
                  ItemStyle-Width="25%"></asp:BoundField>
                     <asp:BoundField HeaderText="Branch Code" 
                DataField="BrnCode" 
                  ItemStyle-Width="25%"></asp:BoundField>
                     <asp:BoundField HeaderText="Account Id" 
                DataField="AcctId" 
                  ItemStyle-Width="25%"></asp:BoundField>
                   <asp:BoundField HeaderText="Name" 
                DataField="Name" 
                  ItemStyle-Width="45%"></asp:BoundField>
                     <asp:BoundField HeaderText="Application ID" 
                DataField="ApplId" 
                  ItemStyle-Width="25%"></asp:BoundField>
                     <asp:BoundField HeaderText= "Status" 
                DataField="Stat" 
                 ItemStyle-Width="25%"></asp:BoundField>
                    
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


         <div style="text-align:center;width:100%;margin-top:30px;">
            <asp:Button ID="btnStop" runat="server" Text="Stop Record" cssClass="btnsoft" OnClick="btnStop_Click"  Visible="false"/>
        </div>
    </div>
         <footer>
       
        <div style="text-align:center;margin-top:100px;width:100%;height:50px;font-family:Calibri;font-size:medium;bottom:0;left:0;position:sticky;background-color:#e5d6f3;vertical-align:central;line-height:50px;color:#1b392e;font-size:medium;"><b>Website Design and Developed by BCCB IT Department.      Copyright© 2019,BCCB</b></div></footer>
    </form>
    <script>
        document.getElementById("chk_selectAll").addEventListener("change", function() {
            document.querySelectorAll("#Gridtbl input[type='checkbox']").forEach(chk => chk.checked = this.checked);
        });
    </script>
</body>
</html>
