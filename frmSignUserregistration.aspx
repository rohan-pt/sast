<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmSignUserregistration.aspx.cs" Inherits="BCCBExamPortal.frmSignUserregistration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <link rel="icon" href="Resources/bccb_logo.png"/>
    <link href="Employee.css" rel="stylesheet" />
      <link href="Signcss.css" rel="stylesheet" />
     <script>
      function callone(x) {
          alert(x);         
      }
     </script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
       <asp:ScriptManager ID="scriptmanager1" runat="server"></asp:ScriptManager>
          <asp:UpdatePanel ID="updatepnl" runat="server">
<ContentTemplate> 
     <div class="sign_user_heading">
  USER ENROLLMENT
    </div>
      <asp:Label ID="sessionlbl" runat="server" Visible="false" Text=""></asp:Label>
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

    </asp:Menu>

    </div> 

    <div class="main_container_box">

        <div class="left_container">
             <div class="general_info">
                This section is for the enrollment of Employees who are assign the work of <span class="dark_span">Signature Verification</span>.
            </div>
            <div class="general_info">
                In case you are not able to receive OTP then please email on <span class="red_span">abhijeet.gharat@bccb.co.in</span>  for technical support.
            </div>
        </div>
         <div class="right_container">
             <div class="label_heading">
                Request for Enrollment
             </div>
             <div class="text_holder">
                 <input type="text" class="text_css" placeholder="Mobile Number" id="txt_mobilenumber" runat="server" />

             </div>
               <div class="text_holder">
                 <input type="text" class="text_css" placeholder="OTP" id="txt_otp" runat="server" disabled="disabled" />

             </div>
             <div class="btn_holder">
                 <button type="button" class="btn_submit_class" runat="server" id="btn_enroll" onserverclick="generate_OTP">Generate OTP</button>
             </div>
               <div class="text_holder">
                <label id="lbl_msg" runat="server" visible="false" class="lbl_class" ></label>

             </div>
        </div>



    </div>


      <div class="ft_BG1">BCCB IT Dept. </div>

    </ContentTemplate>
</asp:UpdatePanel>
      

    </form>
</body>
</html>
