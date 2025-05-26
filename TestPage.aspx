<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestPage.aspx.cs" Inherits="BCCBExamPortal.TestPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Test Panel</title>
     <link rel="icon" href="Resources/bccb_logo.png"/>
     <link href="Employee.css" rel="stylesheet" />
     <style>

#divnavigation::-webkit-scrollbar {
width: 11px;
height: 11px;
}

#divnavigation::-webkit-scrollbar-track {
border: 1px solid #6dbbe8;
border-radius: 10px;
}

#divnavigation::-webkit-scrollbar-thumb {
background:#6dbbe8;  
border-radius: 10px;
}

#divnavigation::-webkit-scrollbar-thumb:hover {
background:#afd8ef;  
}
#divansgroup::-webkit-scrollbar {
width: 11px;
height: 11px;
}

#divansgroup::-webkit-scrollbar-track {
border: 1px solid #20e141;
border-radius: 10px;
}

#divansgroup::-webkit-scrollbar-thumb {
background: #20e141;  
border-radius: 10px;
}

#divansgroup::-webkit-scrollbar-thumb:hover {
background: #95efa4;  
}

</style>
    <script>
    function startCountdown(timeLeft) {
        var interval = setInterval( countdown, 1000 );
        update();

        function countdown() {
            if ( --timeLeft > 0 ) {
                update();
            } else {
                clearInterval( interval );
                update();
                completed();
            }
        }

        function update() {
            hours   = Math.floor( timeLeft / 3600 );
            minutes = Math.floor( ( timeLeft % 3600 ) / 60 );
            seconds = timeLeft % 60;
            var v1 = '' + hours + ':' + minutes + ':' + seconds;
            document.getElementById('timeleft').innerHTML = v1;
            document.getElementById('Hidden1').value = v1;
        }

        function completed() {
            // Do whatever you need to do when the timer runs out...onload="startCountdown(200);"
            window.location = "ExamResult.aspx";
        }
    }
</script>
</head>
<body class="testbody">
    <form runat="server">
       <asp:ScriptManager ID="scriptmanager1" runat="server">

</asp:ScriptManager>
         <div class="heading" style="border:none;">
    <b>E@learning Panel</b>
        <asp:Label ID="sessionlbl" runat="server" Visible="false" Text=""></asp:Label>
    </div>
        <div runat="server" id="divmain">
             <asp:UpdatePanel ID="UpdatePanel2" runat="server">

<ContentTemplate> 
             <div style="width:auto;text-align:right;">
                <asp:Button runat="server" ID="submitbtn" Text="Submit Test" CssClass="btnsubmit1" OnClick="submitbtn_Click" Enabled="false"/>
                 
            </div>
       </ContentTemplate> 
                </asp:UpdatePanel>
            <div style="width:100%;background-color:#8cc7e8;border-radius:20px;height:25px;">
          <div runat="server" id="divempname" style="width:25%;height:20px;font-family:Cambria;font-size:large;text-align:center;color:#000;background-color:#ffffff;border-radius:20px;margin-left:12%;border:solid 2px #8cc7e8">
                Abhijeet Kashinath
            </div>
                </div>
        <div style="width:20%;height:25px;background-color:#fff;align-content:space-between; margin-top:5px;margin-left:40%;text-align:center;margin-top:1px;margin-bottom:1px;">
           
            <div style="width:100%;height:25px;text-align:center;color:#fff;background-color:#ea133a;border-radius:20px;font-family:Arial;">
         <div>Timer is up in: <span id="timeleft" runat="server"></span><input type="Hidden" id="Hidden1" value="" clientidnode="Static" runat="server"/>
             <%--<asp:Label runat="server" ID="lefttime" Text=""></asp:Label>--%></div>
         
                     </div>
            
        </div>
        
           
    <div class="testhead" runat="server" id="divEname">General Knowledge Test on Economics<asp:Label runat="server" ID="lblQnumber" Visible="false"></asp:Label>     
        <asp:Label runat="server" ID="lblExamNo" Visible="false"></asp:Label>
    </div>
            
    <asp:UpdatePanel ID="updatepnl" runat="server">

<ContentTemplate> 

    <div runat="server" id="divqtrack" class="examqno">Question 1 of 10</div>
    <div runat="server" id="divquestionname" class="examquestion" onmousedown='return false;' onselectstart='return false;'>What is Current REPO rate set by RBI?</div>
    <div class="radiodiv">
        <div class="block2" runat="server" id="divansgroup">
    <div style="width:90%;border:solid 1px #f3e5e5;"> <%--<div class="qnum">A</div>--%><div class="qnum">
        <asp:RadioButton runat="server" Text="A" ID="rado1" GroupName="Save" OnCheckedChanged="rado1_CheckedChanged" AutoPostBack="true"/></div><div class="radioin" runat="server" id="divop1"><%--<asp:RadioButton runat="server" Text="7.5" ID="radop1" GroupName="Save"/>--%></div></div>
       <div style="width:90%;border:solid 1px #f3e5e5;"> <%--<div class="qnum">B</div>--%><div class="qnum"><asp:RadioButton runat="server"  Text="B" ID="radop2" GroupName="Save" OnCheckedChanged="radop2_CheckedChanged" AutoPostBack="true"/></div><div class="radioin" runat="server" id="divop2"> </div></div>
       <div style="width:90%;border:solid 1px #f3e5e5;"> <%--<div class="qnum">C</div>--%><div class="qnum"><asp:RadioButton runat="server"  Text="C" ID="radop3" GroupName="Save" OnCheckedChanged="radop3_CheckedChanged" AutoPostBack="true"/></div> <div class="radioin" runat="server" id="divop3"> </div></div>
       <div style="width:90%;border:solid 1px #f3e5e5;"> <%--<div class="qnum">D</div>--%> <div class="qnum"><asp:RadioButton runat="server"  Text="D" ID="radop4" GroupName="Save" OnCheckedChanged="radop4_CheckedChanged" AutoPostBack="true"/></div><div class="radioin" runat="server" id="divop4">  </div></div>
        </div>
         
       <div class="block3" runat="server" id="divnavigation">
          <%--  <div>

        
           <button class="newexbtn">1</button>
             <button class="newexbtn">2</button>
            <button class="newexbtn">3</button>
                 <button class="newexbtn">4</button>
                </div>
          --%>
          
         
   </div>
        </div>
        <div style="margin-top:5px;width:100%;height:80px;">
        <div class="conpanel" >
<div style="text-align:right;display:inline-block;height:60px;width:40%;float:left;"> <asp:ImageButton runat="server" ID="btnBack" src="Resources/nextx1.png" CssClass="rounded" OnClick="btnBack_Click" AlternateText="Back" /> </div>
 


         

     <div style="height:60px;width:40%;display:inline-block;"> <asp:ImageButton runat="server" ID="btnNext1" src="Resources/nextx.png" CssClass="rounded" style="margin-left:70px;" OnClick="btnNext1_Click"  AlternateText="Next"/></div>
            


</div>

      
        <div style="width:30%;height:80px;display:inline-block;"><table style="width:100%; table-layout:fixed;">
            <tr><td style="height:20px;background-color:green;width:5%;"></td><td  style="height:20px;width:85%;font-family:Cambria;"> Questions Answered:</td><td style="height:20px;width:10%;" runat="server" id="tdAns"> 0</td></tr>
            <tr><td style="height:20px;background-color:blue;width:5%;"></td><td style="height:20px;width:85%;font-family:Cambria;">Questions Not Answered:</td><td style="height:20px;width:10%;" runat="server" id="tdNotAns"> 1</td></tr>
             <tr><td style="height:20px;background-color:#8a791b;;width:5%;"></td><td style="height:20px;width:85%;font-family:Cambria;">Questions Not Visited:</td><td style="height:20px;width:10%;" runat="server" id="tdNotVisited"> 9</td></tr>
            
             </table>
        <%-- <div style="width:100%;height:30px;">
           
            <div  style="width:90%;display:inline-block;background-color:green;float:left;height:30px">Question Answered</div>
            <div style="width:10%;height:30px">hh</div>
      </div>--%>
           <%--<div style="width:5%;display:inline-block;float:left;background-color:green;"></div>--%>
           <%-- <div  style="width:100px;display:inline-block;background-color:blue;">Question Answered</div>
            <div style="width:10px;display:inline-block;float:left;"></div>--%>
  
              </div>
          </div>
     </ContentTemplate>

</asp:UpdatePanel>
            </div>

         <footer>
       
        <div style="text-align:center;margin-top:100px;width:100%;height:50px;font-family:Calibri;font-size:medium;bottom:0;left:0;position:sticky;background-color:#e5d6f3;vertical-align:central;line-height:50px;color:#1b392e;font-size:medium;"><b>Website Design and Developed by BCCB IT Department.      Copyright© 2019,BCCB</b></div></footer>
        </form>
  
</body>
</html>
