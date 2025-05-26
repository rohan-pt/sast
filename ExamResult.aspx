<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExamResult.aspx.cs" Inherits="BCCBExamPortal.ExamResult" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
      <link rel="icon" href="Resources/bccb_logo.png"/>
    <title></title>
     <link href="Employee.css" rel="stylesheet" />
    <script>

            function PrintDiv() {
                var divToPrint = document.getElementById('divmain');
                var popupWin = window.open('', '_blank', 'width=1000,height=800,location=no,left=200px');
                popupWin.document.open();
                popupWin.document.write('<html><head><title>Print Content</title> <link href="Employee.css" rel="stylesheet" /></head><body onload="window.print()">' + divToPrint.innerHTML + '</body></html>');
                popupWin.document.close();
            }
         </script>

</head>
<body style="background-color:#aff5dd;">
    <form id="form1" runat="server">
          <div class="header_solve">
    <svg  width="200" height="120" viewBox="0 0 200 120" style="margin-left:10%;display:inline-block;float:left;">
                 <rect x="55" y="5" height="70" width="80" stroke="black" fill="#e9e7e7" rx="7" ry="7"></rect>
                 <rect x="60" y="10" height="50" width="70" stroke="black" fill="#b6ff00"></rect>
                 <rect x="70" y="20" height="10" width="15" stroke="blue" fill="yellow"></rect>
                 <rect x="70" y="40" height="10" width="15" stroke="blue" fill="yellow"></rect>
                 
                 <circle r="2" cx="70" cy="67" fill="green" />
                 <circle r="2" cx="75" cy="67" fill="green" />
                 <circle r="2" cx="80" cy="67" fill="green" />

                 <circle r="2" cx="110" cy="67" fill="green" />
                 <circle r="2" cx="115" cy="67" fill="green" />
                 <circle r="2" cx="120" cy="67" fill="green" />
                 <rect x="87" y="74" height="15" width="15" stroke="black" fill="#e9e7e7"></rect>
                  <rect x="75" y="90" height="5" width="40" stroke="black" fill="#e9e7e7"></rect>
                 <line x1="100" y1="20" x2="120" y2="20" stroke="black"></line>
                  <line x1="100" y1="30" x2="120" y2="30" stroke="black"></line>
                 <line x1="100" y1="25" x2="120" y2="25" stroke="black"></line>


                  <line x1="100" y1="40" x2="120" y2="40" stroke="black"></line>
                  <line x1="100" y1="45" x2="120" y2="45" stroke="black"></line>
                 <line x1="100" y1="50" x2="120" y2="50" stroke="black"></line>
                       <text x="45" y="110" style="font-family: monospace; font-size:1em;fill:#145f67;">Knowledge Test</text>               
             </svg>
                
              <svg width="400" height="120" viewBox="0 0 400 120" style="margin-left:10%;display:inline-block;float:left;">
                  <text x="45" y="50" style="font-family: Algerian; font-size:3em;fill:#0d3d5e;">Test Summary</text>               
              </svg>
             <asp:Label ID="lblExamNo" Visible="false" runat="server"></asp:Label>
             <asp:Label ID="sessionlbl" Visible="false" runat="server"></asp:Label>

    </div>
    <div runat="server" id="divmain" style="text-align:center;background-color:#aff5dd;display:block;">
   <%-- <asp:Table runat="server" ID="tblresult"></asp:Table>--%>     

        <table style="width:50%;margin-left:330px;margin-top:10px;">
         

            <tr>
               <td class="tdpart1">Employee Name</td><td class="tdpart2" runat="server" id="tdname">Abhijeet Kashinath Gharat</td>
            </tr>
            <tr><td class="tdpart1">Employee Code</td><td class="tdpart2" runat="server" id="tdempcode">08560</td></tr>
            <tr><td class="tdpart1">Location</td><td class="tdpart2" runat="server" id="tdloc">Head Office</td></tr>
            <tr><td class="tdpart1">Exam Name</td><td class="tdpart2" runat="server" id="tdtestname">Random Testing</td></tr>
            <tr><td class="tdpart1">Total Questions </td><td runat="server" id="tdqnum" class="tdpart2">10</td></tr>
            <tr><td class="tdpart1">Questions Attempted</td><td runat="server" id="tdatt" class="tdpart2">10</td></tr>
            <tr><td class="tdpart1">Correct Answers</td><td runat="server" id="tdmarks" class="tdpart2">5</td></tr>
            <tr><td class="tdpart1">Percentage Obtained</td><td runat="server" id="tdpercentage" class="tdpart2">50%</td></tr>

        </table>


    </div>


      

        <div style="text-align:center;margin-top:20px;">
            <asp:Button runat="server" ID="btnok" Text="OK" CssClass="minimal_xl" OnClick="btnok_Click" />
             <%--<input type="button" onclick="PrintDiv();" value="Print" />--%>
          <%--  <asp:Button runat="server" ID="btnprint" Text="Print" CssClass="minimal" OnClientClick="PrintDiv()"/>--%>
        </div>
         <div class="footer">
            <img src="Resources/bccb_logo.png" class="footer_logo" style="border-radius:40px;"/>
             <svg  width="700" height="50" viewBox="0 0 700 50" style="margin-left:5%;display:inline-block;float:left;">
               <%--  <rect x="55" y="5" height="70" width="80" stroke="black" fill="#e9e7e7" rx="7" ry="7"></rect>--%>
                 <text x="45" y="30" style="font-family: 'Lucida Sans'; font-size:1.5em;fill:#70980c;">Copyrights © 2019 Bassein Catholic Co-op Bank </text>               
             </svg>
            <img src="Resources/bccb_logo.png" class="footer_logo" style="margin-left:0;border-radius:40px;"/>
        </div>
       
            </form>
</body>
</html>
