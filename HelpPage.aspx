<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HelpPage.aspx.cs" Inherits="BCCBExamPortal.HelpPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
         <link rel="icon" href="Resources/bccb_logo.png"/>
    <title>Help</title>
        <%-- <link href="Employee.css" rel="stylesheet" />
    <link href="Help.css" rel="stylesheet" />--%>
        <link href="https://fonts.googleapis.com/css?family=Amatic+SC|Amiri|Archivo+Narrow|Dancing+Script|Overpass|Pathway+Gothic+One|Playball" rel="stylesheet"/>
    <style>
   @import url(https://fonts.googleapis.com/css?family=Open+Sans);
   #circle-shape-example1 { 
  font-family: Open Sans, sans-serif; 
  margin: 2rem; 
}
 #circle-shape-example1 p{ 
  line-height: 1.8; 
}
#circle-shape-example { 
  font-family: Open Sans, sans-serif; 
  margin: 2rem; 
}
#circle-shape-example p { 
  line-height: 1.8; 
}
#circle-shape-example .curve { 
  width: 33%; height: auto;
  min-width: 150px;
  float: left;
  margin-right:2rem; 
  border-radius: 50%;
  -webkit-shape-outside:circle();
  shape-outside:circle(100%);
  font-family: 'Pathway Gothic One', sans-serif;
}   
#circle-shape-example2 .curve1 { 
  width: 40%; height: auto;
  min-width: 170px;
  float: left;
  margin-right:2rem; 
  border-radius: 45%;
  -webkit-shape-outside:circle();
  shape-outside:circle(100%);
 font-family: 'Archivo Narrow', sans-serif;
}   
#circle-shape-example .curve1 { 
  width: 40%; height: auto;
  min-width: 170px;
  float: left;
  margin-right:2rem; 
  border-radius: 45%;
  -webkit-shape-outside:circle();
  shape-outside:circle(100%);
 font-family: 'Archivo Narrow', sans-serif;
}   

#circle-shape-example3 .curve { 
  width: 33%; height: auto;
  min-width: 150px;
  float: left;
  margin-left:2rem; 
  border-radius: 50%;
  -webkit-shape-outside:circle();
  shape-outside:circle(120%);
  font-family: 'Archivo Narrow', sans-serif;
  margin-left:100px;

}  
p{
 font-family: 'Archivo Narrow', sans-serif;
}   
#circle-shape-example1 .curve { 
  width: 33%; height: auto;
  min-width: 150px;
  float: right;
  margin-left:2rem; 
  border-radius: 50%;
  -webkit-shape-outside:circle();
  shape-outside:circle();
  font-family: 'Archivo Narrow', sans-serif;
}     
    </style>
   
</head>

<body style="background-color: #ccf3bc;">
 
    <form id="form1" runat="server">
          <div style=" text-align: center;
    width:auto;
    background:url(/Resources/BCCBLOGO1.bmp) no-repeat;
    background-size: contain;
    height:73px;
     font-family: Cambria ;
      font-size:40px;
      vertical-align: middle;
      line-height:73px;
    background-color:#e7f5b9;
    color:#8b27e4;
    border-radius:45px;">
 <b>BCCB Intranet</b>
        <asp:Label ID="sessionlbl" runat="server" Visible="false" Text=""></asp:Label>
    </div>
<%--<div style="text-align:right;align-content:space-between;"><asp:Button runat="server" ID="btnHome" Text="Home"  CssClass="btnchange" OnClick="btnHome_Click" /><asp:Button runat="server" ID="btnchangepassword" Text="Change Password" OnClick="btnchangepassword_Click" CssClass="btnchange"/><asp:Button ID="btnLogOut" runat="server" Text="Log Out" OnClick="btnLogOut_Click" CssClass="btnchange" /></div>
 --%>

        <div>
<div id="circle-shape-example">
  <img src="/Resources/gm.jpg" alt="A photograph of sliced kiwifruit on a while plate" class="curve"><h1 style="color:#538b01; font-weight:bold; font-style:italic;">General Information about Intranet Website.</h1>

<p>1 ) You suppose to change password after first Login.</p><p>2 ) If your Location is not matching / or you gets transferred to new branch, you should email to Human Resourse Department (HRD) to change Location.</p>
            <p> 3 ) Similarly, for any change, in profile, one should contact to Human Resourse Department (HRD).</p>
            <p> 4 ) If you dont have Email of your own, please contact to Head Office.</p>
            <p> 5 ) Study the website and its different modules for few days.</p>

</div>
            <div id="circle-shape-example1">
  <img src="/Resources/ee.jpeg" alt="A photograph of sliced kiwifruit on a while plate" class="curve"><h1 style="color:#e63496; font-weight:bold; font-style:italic;">E@learning (Online Test)</h1>

 <p>1 ) Here Employees will undergoes knowledge testing.</p><p>2 ) These tests are taken by our Human Resource Department (HRD) to test and enhance knowledge of Staff.</p>
            <p> 3 ) Answers are available after test's deadline date.</p>
            <p> 4 ) There may occured problem while live test, because of connectivity issue or something else, in these cases you should leave test page. Your last state will be saved and from there you can restart.  </p>
</div>

        </div>
         <div>
<div id="circle-shape-example2">
  <img src="/Resources/cbb.png" alt="A photograph of sliced kiwifruit on a while plate" class="curve1"><h1 style="color:#26a3eb; font-weight:bold; font-style:italic;">Pos Charge-back Portal</h1>

<p>1 ) Instrument number of transaction in OMNI system is known as Reference Number (RRN).</p>
            
            <p>2 ) Employees will come to know up-to-date status about transactions and charge-backs, so that they can clarify it to customers properly.</p>
            <p> 3 ) You can raise charge-back by clicking Raise button.</p>
             <p> 5 ) If RRN number is not found in record, you can <b>seperately Raise it as POS transaction</b>, which button is visible there.</p>
           
            <p> 6 ) You should sent Scanned copy throught e-mail, on e-mail address <b>alisha.pereira@bccb.co.in </b>.</p>
        </div>


</div>
            <div id="circle-shape-example3">
  <img src="/Resources/activity.jpg" alt="A photograph of sliced kiwifruit on a while plate" class="curve"><h1 style="color:#ff6a00; font-weight:bold; font-style:italic;">Other Normal Activity</h1>

 <p>1 ) Facility to Unblock Internet Banking, HOT-Mark ATM Card , Cash Position of ATM is also provided.</p>
 <p>2 ) You just need to Input properly to get details and then work on it.</p>
                <p>3 ) Remember for activity you do your code is registed in system .</p>
           
</div>

        </div>
        <footer>
       
        <div style="text-align:center;margin-top:100px;width:100%;height:50px;font-family:Calibri;font-size:medium;bottom:0;left:0;position:sticky;background-color:#e5d6f3;vertical-align:central;line-height:50px;color:#1b392e;font-size:medium;"><b>Website Design and Developed by BCCB IT Department.      Copyright© 2019,BCCB</b></div></footer>
      
        
      <%--   <div style="width:100%;">
        <div style="margin-top:10px;text-align:center;font-family:'Times New Roman', Times, serif;font-size:xx-large;color:brown;">
    
        General Information</div>
        <div style="background-color:white;border-radius:50px;">
         <h2>General Information about Intranet Website.</h2><p>1 ) You suppose to change password after first Login.</p><p>2 ) If your Location is not matching / or you gets transferred to new branch, you should email to Human Resourse Department (HRD) to change Location.</p>
            <p> 3 ) Similarly, for any change, in profile, one should contact to Human Resourse Department (HRD).</p>
            <p> 4 ) If you dont have Email of your own, please contact to Head Office.</p>
            <p> 5 ) Study the website and its different modules for few days.</p>
        </div>


         <div style="text-align:center;font-family:'Times New Roman', Times, serif;font-size:xx-large;color:brown;margin-top:20px;">
    
        E@learning (Online Test)</div>
        <div style="background-color:white;border-radius:50px;">
         <h2>Online Knowledge Test </h2><p>1 ) Here Employees will undergoes knowledge testing.</p><p>2 ) These tests are taken by our Human Resource Department (HRD) to test and enhance knowledge of Staff.</p>
            <p> 3 ) Answers are available after test's deadline date.</p>
            <p> 4 ) There may occured problem while live test, because of connectivity issue or something else, in these cases you should leave test page. Your last state will be saved and from there you can restart.  </p>
        </div>


    <div style="text-align:center;font-family:'Times New Roman', Times, serif;font-size:xx-large;color:brown;margin-top:20px;">
    
        Pos Charge-back Portal</div>
        <div style="background-color:white;border-radius:50px;">
         <h2>This Portal related to Branches</h2>
             <p>1 ) Instrument number of transaction in OMNI system is known as Reference Number (RRN).</p>
            <p>2 ) Here all POS Transaction charge-backs are raised and its track is maintained.</p>
            <p>3 ) Employees will come to know updated status about transactions, so that they can clearify it to customers properly.</p>
            <p> 4 ) You can Raise the transaction complaint to Head Office by clicking raise button and keep close track of it.</p>
             <p> 5 ) Head Office has transaction status reports up to a day before yesterday. If RRN number is not found in record, you can seperately Raise it as POS transaction, which button is visible there.</p>
            <p> 6 ) Other things are self explanatory </p>
            <p> 7 ) You should sent Scanned copy throught e-mail, on e-mail address <b>alisha.pereira@bccb.co.in </b>.</p>
        </div>




       
       </div>--%>
       
    </form>
</body>
</html>
