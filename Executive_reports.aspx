<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Executive_reports.aspx.cs" Inherits="BCCBExamPortal.Executive_reports" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">   
      <link rel="icon" href="Resources/bccb_logo.png"/>
       <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
     <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
     <script src="https://code.jquery.com/jquery-3.2.0.min.js"></script>
    <link href="Resources/CSS/Executive_report.css?v=2" rel="stylesheet" />
    <script src="Resources/Script/General.js"></script>
    <title class="title_1">Executive Reports</title>    
    <script>
        function show_gif() {
            $("#info_div").show();
        }
        function hide_gif() {
            $("#info_div").hide();
        }

    </script>
     <script type="text/javascript">
         google.charts.load("current", { packages: ["corechart"] });
         function callone(div,xx,til) {           
             google.charts.setOnLoadCallback(drawChart);
             function drawChart() {                
                 var res = xx.split(",");
                 var data = new google.visualization.DataTable();
                 data.addColumn('string', 'Customer');
                 data.addColumn('number', 'Category');
                 var data1 = [];
                 for (i = 0; i < res.length; i++) {
                     var temp = [];
                     var cc = res[i].split(":");
                     temp.push(cc[0]);
                     temp.push(parseFloat(cc[1]));
                     data1.push(temp);
                 }
                 data.addRows(data1);

                 //     data.addRows([
                 //['Successfully Done', 10000],
                 //['New Registration', 5000],
                 //['Delete Create', 67543],
                 //['Already Done', 67890],
                 //['IB Customer', 7765]            
                 //     ]);

                 var options = {
                     title: til,
                     is3D: true,
                 };

                 var chart = new google.visualization.PieChart(document.getElementById(div));
                 chart.draw(data, options);
             }
         }
     </script>
    <script type="text/javascript">
        google.charts.load("current", { packages: ["corechart"] });
        function callfour(div, xx, til) {
            google.charts.load("current", { packages: ["corechart"] });
            google.charts.setOnLoadCallback(drawChart);
            function drawChart() {
                var res = xx.split(",");
                var data = new google.visualization.DataTable();
                data.addColumn('string', 'Customer');
                data.addColumn('number', 'Category');
                var data1 = [];
                for (i = 0; i < res.length; i++) {
                    var temp = [];
                    var cc = res[i].split(":");
                    temp.push(cc[0]);
                    temp.push(parseFloat(cc[1]));
                    data1.push(temp);
                }
                data.addRows(data1);

                //var data = google.visualization.arrayToDataTable([
                //    ['Task', 'Hours per Day'],
                //    ['Work', 11],
                //    ['Eat', 2],
                //    ['Commute', 2],
                //    ['Watch TV', 2],
                //    ['Sleep', 7]
                //]);

                var options = {
                    title: til,
                    pieHole: 0.4,
                };

                var chart = new google.visualization.PieChart(document.getElementById(div));
                chart.draw(data, options);
            }
        }
    </script>
    <script type="text/javascript">
        google.charts.load("current", { packages: ['corechart'] });
        function callthree(div, xx, til) {
            google.charts.setOnLoadCallback(drawChart);
            function drawChart() {
                //var data = google.visualization.arrayToDataTable([
                //    ["Element", "Density", { role: "style" }],
                //    ["Copper", 8.94, "#80efcd"],
                //    ["Silver", 10.49, "#80efcd"],
                //    ["Gold", 19.30, "#80efcd"],
                //    ["Platinum", 21.45, "#80efcd"]
                //]);
                var res = xx.split(",");
                var data = new google.visualization.DataTable();
                data.addColumn('string', 'Customer');
                data.addColumn('number', 'Category');
                data.addColumn({ type: 'string', role: 'style' });
                var data1 = [];
                for (i = 0; i < res.length; i++) {
                    var temp = [];
                    var cc = res[i].split(":");
                    temp.push(cc[0]);
                    temp.push(parseFloat(cc[1]));
                    temp.push(cc[2]);
                    data1.push(temp);
                }
                data.addRows(data1);
               

                var view = new google.visualization.DataView(data);
                view.setColumns([0, 1,
                    {
                        calc: "stringify",
                        sourceColumn: 1,
                        type: "string",
                        role: "annotation"
                    },
                    2]);

                var options = {
                    title: til,                  
                    bar: { groupWidth: "70%" },
                    legend: { position: 'top', maxLines: 3 },
                };
                var chart = new google.visualization.ColumnChart(document.getElementById(div));
                chart.draw(view, options);
            }
        }
    </script>
     <script type="text/javascript">
         google.charts.load("current", { packages: ["corechart"] });
         function calltwo(div, xx, til) {
             google.charts.setOnLoadCallback(drawChart);
             function drawChart() {
                 var res = xx.split(",");
                 var data = new google.visualization.DataTable();
                 data.addColumn('string', 'Customer');
                 data.addColumn('number', 'Category');
                 data.addColumn({ type: 'string', role: 'style' });
                 var data1 = [];
                 for (i = 0; i < res.length; i++) {
                     var temp = [];
                     var cc = res[i].split(":");
                     temp.push(cc[0]);
                     temp.push(parseFloat(cc[1]));
                     temp.push(cc[2]);
                     data1.push(temp);
                 }
                 data.addRows(data1);

                 var view = new google.visualization.DataView(data);
                 view.setColumns([0, 1,
                     {
                         calc: "stringify",
                         sourceColumn: 1,
                         type: "string",
                         role: "annotation"
                     },
                     2]);

                 var options = {
                     title: til,
                     bar: { groupWidth: "80%" },
                     legend: { position: 'top', maxLines: 3 },
                 };
                 var chart = new google.visualization.BarChart(document.getElementById(div));
                 chart.draw(view, options);
             }
         }
         function replace(vr) {
            // alert(vr);            
             window.open(vr, "_blank");
         }
     </script>
    <script type="text/javascript">
        google.charts.load('current', { 'packages': ['corechart'] });
        function callfive(colname,div, xx, til) {
            google.charts.setOnLoadCallback(drawVisualization);

            function drawVisualization() {

                var c1 = colname.split(",");
                var data = new google.visualization.DataTable();
                data.addColumn('string', c1[0]);
                for (i = 1; i <= c1.length-1; i++) {
                    data.addColumn('number', c1[i]);
                }


                var res = xx.split(",");
                var data1 = [];
                for (i = 0; i < res.length; i++) {
                    var temp = [];
                    var cc = res[i].split(":");
                    temp.push(cc[0]);
                    for (j = 1; j <= cc.length - 1; j++) {
                        temp.push(parseFloat(cc[j]));
                    }
                    data1.push(temp);
                }
                data.addRows(data1);

                //var data = google.visualization.arrayToDataTable([
                //    ['Month', 'Bolivia', 'Ecuador', 'Madagascar', 'Papua New Guinea', 'Rwanda', 'Average'],
                //    ['2004/05', 165, 938, 522, 998, 450, 614.6],
                //    ['2005/06', 135, 1120, 599, 1268, 288, 682],
                //    ['2006/07', 157, 1167, 587, 807, 397, 623],
                //    ['2007/08', 139, 1110, 615, 968, 215, 609.4],
                //    ['2008/09', 136, 691, 629, 1026, 366, 569.6]
                //]);

                var options = {
                    title: til,
                    vAxis: { title: 'Y-Axis' },
                    hAxis: { title: 'X-Axis' },
                    seriesType: 'bars',
                    series: { 5: { type: 'line' } }
                };

                var chart = new google.visualization.ComboChart(document.getElementById(div));
                chart.draw(data, options);
            }
        }
    </script>

    <%-- <script type="text/javascript">
         google.charts.load('current', { 'packages': ['corechart'] });
         function callone(xx,data1,tit) {
             google.charts.setOnLoadCallback(drawChart);
             function drawChart() {

                 var res = data1.split("-");
                 var data = new google.visualization.DataTable();
                 data.addColumn('string', 'Type of Account');
                 data.addColumn('number', 'Number of Accounts');
                 data.addRows([
                     ['CASA', parseInt(res[0])],
                     ['LOAN', parseInt(res[1])],
                     ['TERM DEPOSITE', parseInt(res[2])]                   
                 ]);
                 //var data = google.visualization.arrayToDataTable([
                 //    ['Task', 'Hours per Day'],
                 //    ['Work', 110],
                 //    ['Eat', 20],
                 //    ['Commute', 25],
                 //    ['Watch TV', 29],
                 //    ['Sleep', 70]
                 //]);

                 var options = {
                     title: tit
                 };

                 var chart = new google.visualization.PieChart(document.getElementById(xx));

                 chart.draw(data, options);

             }
         }
     </script>--%>
</head>
<body>
    <form id="form1" runat="server">
         <div class="top_header">
             Executive Reports
               <asp:Label ID="sessionlbl" runat="server" Visible="false" Text="">              

            </asp:Label>
              <asp:Label ID="branch_id_pl" runat="server" Visible="false" Text="">              

            </asp:Label>
        </div>
        <div style="width:100%;height:35px;">
             <button class="hh3" id="bck_hmbtn" runat="server" onserverclick="home_redirect">HOME</button>
            <div class="hh1" id="e_name" runat="server">Name : Abhijeet Gharat</div>
            <div class="hh2" id="Bran" runat="server">Branch : pp</div>          
        </div>
        <div class="div_mn">
     <%-- <input type="checkbox" id="chk1" name="weekday-1" value="Friday" runat="server" checked="true">
  <label for="chk1">CASA Reports</label>

           <input type="checkbox" id="chk2" name="weekday-1" runat="server" value="Friday" >
  <label for="chk2">Loan Reports</label>


           <input type="checkbox" id="chk3" name="weekday-1" runat="server" value="Friday" >
  <label for="chk3">Term Deposit Reports</label>--%>

 <asp:DropDownList ID="ddl_dep"  class="empfields" runat="server"> <asp:ListItem Value="0" Text="--All Branches--" Selected="True"></asp:ListItem>
</asp:DropDownList> 
             <asp:DropDownList ID="dllocation"  class="empfields" runat="server"> <asp:ListItem Value="0" Text="--All Branches--" Selected="True"></asp:ListItem>
</asp:DropDownList> 

            <button type="button" class="btn_apply" id="btn_apply" runat="server" onserverclick="load_data" onclick="show_gif();" >Apply</button>
        </div>
        <%-- <div class="message_div">
                    <div  class="pi_chart1">
                 <button class="dv_btn0">Redirect</button>
                <button class="dv_btn">Download</button>
                        <div  id="piechart" class="pi_chartx"></div>
                    </div>
                      <div id="piechart2" class="pi_chart1">

                    </div>
                </div>--%>
        <div class="all_report" id="all_report" runat="server">
        <div class="report_container">
            <div class="section1">
                <div class="title_of_rep_plain">Report Name</div>
                 <button class="dv_btn0">Redirect</button>
                <button class="dv_btn">Download</button>
                <table class="table_plain">
                    <tr>
                        <td class="td_plain_th">table header 1</td>
                         <td class="td_plain_th">table header 2</td>
                         <td class="td_plain_th">table header 3</td>
                         <td class="td_plain_th">table header 4</td>
                    </tr>
                     <tr>
                        <td class="td_plain_td">table info 1</td>
                         <td class="td_plain_td">table info 2</td>
                         <td class="td_plain_td">table info 3</td>
                         <td class="td_plain_td">table info 4</td>
                    </tr>
                     <tr>
                        <td class="td_plain_td">table info 1</td>
                         <td class="td_plain_td">table info 2</td>
                         <td class="td_plain_td">table info 3</td>
                         <td class="td_plain_td">table info 4</td>
                    </tr>
                     <tr>
                        <td class="td_plain_td">table info 1</td>
                         <td class="td_plain_td">table info 2</td>
                         <td class="td_plain_td">table info 3</td>
                         <td class="td_plain_td">table info 4</td>
                    </tr>
                     <tr>
                        <td class="td_plain_td">table info 1</td>
                         <td class="td_plain_td">table info 2</td>
                         <td class="td_plain_td">table info 3</td>
                         <td class="td_plain_td">table info 4</td>
                    </tr>
                     <tr>
                        <td class="td_plain_td">table info 1</td>
                         <td class="td_plain_td">table info 2</td>
                         <td class="td_plain_td">table info 3</td>
                         <td class="td_plain_td">table info 4</td>
                    </tr>
                     <tr>
                        <td class="td_plain_td">table info 1</td>
                         <td class="td_plain_td">table info 2</td>
                         <td class="td_plain_td">table info 3</td>
                         <td class="td_plain_td">table info 4</td>
                    </tr>
                     <tr>
                        <td class="td_plain_td">table info 1</td>
                         <td class="td_plain_td">table info 2</td>
                         <td class="td_plain_td">table info 3</td>
                         <td class="td_plain_td">table info 4</td>
                    </tr>
                     <tr>
                        <td class="td_plain_td">table info 1</td>
                         <td class="td_plain_td">table info 2</td>
                         <td class="td_plain_td">table info 3</td>
                         <td class="td_plain_td">table info 4</td>
                    </tr>
                     <tr>
                        <td class="td_plain_td">table info 1</td>
                         <td class="td_plain_td">table info 2</td>
                         <td class="td_plain_td">table info 3</td>
                         <td class="td_plain_td">table info 4</td>
                    </tr>
                     <tr>
                        <td class="td_plain_td">table info 1</td>
                         <td class="td_plain_td">table info 2</td>
                         <td class="td_plain_td">table info 3</td>
                         <td class="td_plain_td">table info 4</td>
                    </tr>
                     <tr>
                        <td class="td_plain_td">table info 1</td>
                         <td class="td_plain_td">table info 2</td>
                         <td class="td_plain_td">table info 3</td>
                         <td class="td_plain_td">table info 4</td>
                    </tr>
                     <tr>
                        <td class="td_plain_td">table info 1</td>
                         <td class="td_plain_td">table info 2</td>
                         <td class="td_plain_td">table info 3</td>
                         <td class="td_plain_td">table info 4</td>
                    </tr>
                     <tr>
                        <td class="td_plain_td">table info 1</td>
                         <td class="td_plain_td">table info 2</td>
                         <td class="td_plain_td">table info 3</td>
                         <td class="td_plain_td">table info 4</td>
                    </tr>
                     <tr>
                        <td class="td_plain_td">table info 1</td>
                         <td class="td_plain_td">table info 2</td>
                         <td class="td_plain_td">table info 3</td>
                         <td class="td_plain_td">table info 4</td>
                    </tr>
                     <tr>
                        <td class="td_plain_td">table info 1</td>
                         <td class="td_plain_td">table info 2</td>
                         <td class="td_plain_td">table info 3</td>
                         <td class="td_plain_td">table info 4</td>
                    </tr>
                </table>
               
            </div>
             <div class="section2"></div>
        </div>
          </div>
        <div style="width:100%;height:100%;position:absolute;top:0;opacity:1;background:#fcfaf5;display:none;" id="info_div">
             <div style="width:60%;position:relative;height:400px;top:100px;left:20%;" id="load_img">
                <img src="Resources/Vectors/loaderf1.gif"  style="width:100%;position:relative;height:400px;"/>
            </div>
        </div>
         <footer>
            <div class="new_footer"><b>Copyright© 2019,BCCB-IT</b></div>
        </footer>
    </form>
</body>
</html>
