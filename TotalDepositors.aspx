<%@ Page Language="C#" MasterPageFile="~/MasterPages/_MasterPage.Master" AutoEventWireup="true" CodeBehind="TotalDepositors.aspx.cs" Inherits="BCCBExamPortal.TotalDepositors" %>
<asp:Content ID="Head" runat="server" ContentPlaceHolderID="Head">
    <link rel="stylesheet" href="Resources/Styles/inputTypeDateStyles.css" />
    <style>
        .title {
            color: white;
            text-shadow: 2px 2px 1px rgb(0, 0, 0);
            background:  rgba(0, 0, 0, 0.5);
            display: inline-block;
            padding: 5px;
        }
        .message {
            color: white;
            background: rgba(0, 0, 0, 0.5);
            display: inline-block;
            padding: 5px;
            display: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Body" runat="server" ContentPlaceHolderID="Body">
        <div>
        <div style="width:100%;height:auto;background-color:#2d3c2c;font-family:'Times New Roman', Times, serif;font-size: xx-large;text-align:center;padding:10px;border-radius:50px;color:#ffffff;">Total Depositors<asp:Label ID="sessionlbl" runat="server" Visible="false" Text=""></asp:Label></div>
         <div style="width:100%;height:auto;opacity:0.80;filter: alpha(opacity=80);border-radius:20px;background-color:#f35ab8;text-align:center;"></div> 
        <div style="text-align:center;width:100%;margin-top:30px;align-content:space-between;">
            <h2 class="title">Total No. of Depositors as on:</h2>
            <input type="date" id="date" style="margin: auto;" />
        </div>
        <div style="text-align:center;width:100%;margin-top:30px;">
            <input type="button" value="Get Total Depositors" class="btnsoft" onclick="getRecord()" />
        </div>
        <div style="text-align:center;width:100%;margin-top:30px;">
            <h2 class="message"></h2>
        </div>
        

     <div id="tableContainer" style="width:100%;margin-top:25px;">
     </div>
   </div>
</asp:Content>
<asp:Content ID="Footer" runat="server" ContentPlaceHolderID="Footer">
    
    <script>
        function getFormattedDate()
        {
            var todayDate = new Date(), year = todayDate.getFullYear(), month = todayDate.getMonth() + 1, day = todayDate.getDate();
            if (month < 10)
                month = "0" + month;
            if (day < 10)
                day = "0" + day;
            return year + "-" + month + "-" + day;
        }
        var balanceDateEle = document.getElementById("date");
        balanceDateEle.value = getFormattedDate();

        function getRecord() {
            var date = document.querySelector("input[type=date]").value;
            ajaxCall("TotalDepositors.aspx", "AjaxCall=TotalDepositors&Date=" + date, function (data) {
                var messageEle = document.querySelector(".message");
                messageEle.style.display = "inline-block";
                messageEle.innerHTML =  GetContent(data, "####")
            })
        }
    </script>
</asp:Content>