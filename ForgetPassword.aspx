<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgetPassword.aspx.cs" Inherits="BCCBExamPortal.ForgetPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <link rel="icon" href="Resources/bccb_logo.png"/>
   <title>Give My Credentials</title>  
    <link href="LogIn.css" rel="stylesheet" />
   
    <script>
        function validate(evt) {
            if (evt.keyCode != 8) {
                var theEvent = evt || window.event;
                var key = theEvent.keyCode || theEvent.which;
                key = String.fromCharCode(key);
                var regex = /[0-9]|\./;
                if (!regex.test(key)) {
                    theEvent.returnValue = false;

                    if (theEvent.preventDefault)
                        theEvent.preventDefault();
                }
            }
        }
</script>
</head>
<body>
   <center> <form runat="server">
    <div class="heading" style="border:none;">
   
    </div>

  <div class="container" style="background-color:#B1CFC4">
   <%-- <label for="uname"><b>Username</b></label>--%>
    <input runat="server" id="Username1" type="text" placeholder="Enter BCCB Login Code (only digits eg:08560)" name="uname" onkeypress='validate(event)'>
    <%--  <asp:TextBox runat="server"  ID="Username"></asp:TextBox>--%>
  <%--  <label for="psw"><b>Password</b></label>--%>
   <asp:DropDownList ID="dllocation"  class="empfields" runat="server" CssClass="dropdown"> <asp:ListItem Value="0" Text="--Please Select Location--" Selected="True"></asp:ListItem>
</asp:DropDownList>
      <%-- <asp:TextBox runat="server"  ID="Password"></asp:TextBox>--%>
   <asp:DropDownList ID="dldesignation"  class="empfields" runat="server" CssClass="dropdown"> <asp:ListItem Value="0" Text="--Please Select Location--" Selected="True"></asp:ListItem>
</asp:DropDownList>
      <div>
    <div> <div style="width:100%;margin-top:10px;"><asp:Button runat="server" Text="Give My credentials" id="Button1" CssClass="ftlogin" OnClick="Button1_Click"   /></div>

    </div>  
   
          </div>
      <div><div style="width:100%;margin-top:10px;"><asp:Button runat="server" Text="Back To Login" id="Button2" CssClass="btndisab" OnClick="Button2_Click"    /></div>
</div>
      <div style="height:50px;margin-top:20px;text-align:center;"><asp:Label ID="txtlbl" runat="server" Text=""></asp:Label></div>
       </div>
      
 
</form></center>
</body>
</html>
