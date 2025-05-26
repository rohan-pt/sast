<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestAns.aspx.cs" Inherits="BCCBExamPortal.TestAns" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link rel="icon" href="Resources/bccb_logo.png"/>
     <link href="Employee.css" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css?family=Amatic+SC|Amiri|Archivo+Narrow|Dancing+Script|Overpass|Pathway+Gothic+One|Playball" rel="stylesheet"/>
</head>
<body style="background-color:#ffffff;">
    <form id="form1" runat="server">
         <div class="heading">
    <b>Online Test Dash Board</b>
        <asp:Label ID="sessionlbl" runat="server" Visible="false" Text=""></asp:Label>
    </div>
        <div style="text-align:right;align-content:space-between;"><asp:Button runat="server" ID="Button1" Text="Main Panel"  CssClass="btnchange" OnClick="Button1_Click" /><asp:Button runat="server" ID="btnchangepassword" Text="Change Password" OnClick="btnchangepassword_Click" CssClass="btnchange"/><asp:Button ID="btnLogOut" runat="server" Text="Log Out" OnClick="btnLogOut_Click" CssClass="btnchange" /></div>
   
        <div style="text-align:center;width:auto;margin-left:35%;">
            <table style="text-align:left;width:30%;font-family:'Times New Roman', Times, serif;background-color:darksalmon;">
                <tr>
                    <td style="width:80%;color:#0f3d0f"> Correct Answer </td>
                     <td style="width:20%;background-color:#53bae4;"> </td>
                </tr>
                  <tr>
                    <td style="width:80%;color:#0f3d0f">Your Respose</td>
                     <td style="width:20%;background-color:#aa92d8"></td>
                </tr>
                  <tr>
                    <td style="width:80%;color:#0f3d0f">For Correct Response</td>
                     <td style="width:20%; background-color:#cb7804;"></td>
                </tr>
                 
            </table>

        </div>


        <div id="divexaminfo" runat="server" style="text-align:center;padding:10px;width:100%;background-color:#ceb94a;color:white;font-family:'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif;font-size:x-large;border-radius:30px;margin-top:20px;">

            BCCB Test Random Select Exam 
        </div>



      <div id="divmain" runat="server" style="width:100%;text-align:center;">

        <%--  <div>
          <div id="question" runat="server" class="finansQ" >
              (1) BCCB Internal exam test question random select for testing ?

          </div>
          
          <div id="Div1" runat="server" style="width:40%;text-align:center;margin-left:30%;padding:10px;background-color:#a3e4a5;color:#192e0c;font-family:'Times New Roman', Times, serif;font-size:larger;border-radius:50px;border:solid 1px white;">
               Option 1

          </div>
           <div id="Div2" runat="server" style="width:40%;text-align:center;margin-left:30%;padding:10px;background-color:#a3e4a5;color:#192e0c;font-family:'Times New Roman', Times, serif;font-size:larger;border-radius:50px;border:solid 1px white;">
               Option 2

          </div>
 <div id="Div3" runat="server" style="width:40%;text-align:center;margin-left:30%;padding:10px;background-color:#efc46a;color:#192e0c;font-family:'Times New Roman', Times, serif;font-size:larger;border-radius:50px;border:solid 1px white;">
               Option 3

          </div>
           <div id="Div4" runat="server" style="width:40%;text-align:center;margin-left:30%;padding:10px;background-color:#a3e4a5;color:#192e0c;font-family:'Times New Roman', Times, serif;font-size:larger;border-radius:50px;border:solid 1px white;">
               Option 4

          </div>
          </div>
          
          <div>
          <div id="Div5" runat="server" style="width:70%;text-align:center;margin-left:15%;padding:10px;background-color:#757ee8;color:#e4d912;font-family:'Times New Roman', Times, serif;font-size:larger;border-radius:50px;margin-top:20px;border:solid 1px white;">
              (1) BCCB Internal exam test question random select for testing ?

          </div>
          
          <div id="Div6" runat="server" style="width:40%;text-align:center;margin-left:30%;padding:10px;background-color:#a3e4a5;color:#192e0c;font-family:'Times New Roman', Times, serif;font-size:larger;border-radius:50px;border:solid 1px white;">
              (1) Option 1

          </div>
           <div id="Div7" runat="server" style="width:40%;text-align:center;margin-left:30%;padding:10px;background-color:#a3e4a5;color:#192e0c;font-family:'Times New Roman', Times, serif;font-size:larger;border-radius:50px;border:solid 1px white;">
              (1) Option 2

          </div>
 <div id="Div8" runat="server" style="width:40%;text-align:center;margin-left:30%;padding:10px;background-color:#efc46a;color:#192e0c;font-family:'Times New Roman', Times, serif;font-size:larger;border-radius:50px;border:solid 1px white;">
              (1) Option 3

          </div>
           <div id="Div9" runat="server" style="width:40%;text-align:center;margin-left:30%;padding:10px;background-color:#a3e4a5;color:#192e0c;font-family:'Times New Roman', Times, serif;font-size:larger;border-radius:50px;border:solid 1px white;">
              (1) Option 4

          </div>
          </div>--%>
      </div>  
         <footer>
       
        <div style="text-align:center;margin-top:100px;width:100%;height:50px;font-family:Calibri;font-size:medium;bottom:0;left:0;position:sticky;background-color:#e5d6f3;vertical-align:central;line-height:50px;color:#1b392e;font-size:medium;"><b>Website Design and Developed by BCCB IT Department.      Copyright© 2019,BCCB</b></div></footer>
        
         
    </form>
</body>
</html>
