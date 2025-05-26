<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Swiftlimit.aspx.cs" Inherits="BCCBExamPortal.Swiftlimit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">  
     <link rel="icon" href="Resources/bccb_logo.png"/>
    <link href="Swiftlimit.css" rel="stylesheet" />
    <link rel="preconnect" href="https://fonts.googleapis.com"/>
<link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
<link href="https://fonts.googleapis.com/css2?family=Cinzel:wght@700&display=swap" rel="stylesheet"/>
    <link href="https://fonts.googleapis.com/css2?family=Barlow:wght@200&display=swap" rel="stylesheet"/>
    <link href="https://fonts.googleapis.com/css2?family=Abel&display=swap" rel="stylesheet"/>
     <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
     <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <script src="Resources/Script/Swiftlimit.js"></script>
      <title></title>
     <script type="text/javascript">
         function ShowProgressBar() {           
          document.getElementById('dvProgressBar').style.display = 'block';//OnClientClick="javascript:ShowProgressBar()"
      }

         function HideProgressBar() {            
          document.getElementById('dvProgressBar').style.display = "none";
      }
    </script>
    
</head>
<body>

    <form id="form1" runat="server">
           <asp:ScriptManager ID="scriptmanager1" runat="server"></asp:ScriptManager>
          <asp:UpdatePanel ID="updatepnl" runat="server" UpdateMode="Conditional" >

<ContentTemplate> 
    <div class="main_container">
    <div class="menu_container">
<div class="logo_css"></div>
<div class="btn_container">
     <asp:Label ID="sessionlbl" runat="server" Visible="false" Text=""></asp:Label>
    <button type="button" class="btn_cls" id="btn_LC" runat="server"  onserverclick="btn_LC_Click">LC Limit</button>
     <button type="button" class="btn_cls_act" id="btn_Limit_Monitoring" runat="server" onserverclick="btn_Moni_Click">Limit Monitoring</button>
     <button type="button" class="btn_cls" id="btn_PCL" runat="server" onserverclick="btn_PCL_Click">PCL Limit</button>
      <button type="button" class="btn_cls" id="btn_home" runat="server" onserverclick="btn_home1">Home</button>
</div>
    </div>
<div class="report_container">
    <div class="heading_css">
<label class="head_lbl">Trade Finance Customer Limits Tracking Module</label>
    </div>

  <%--  LC Limit Section --%>
        <div class="Lod_gif_img" id="dvProgressBar" runat="server" >
        <img src="Resources/loading.gif" class="gif_img"/>
        <div class="msg_div">Synchronizing Data.....Please Wait</div> 
    </div>

<div class="Limit_tracking_div" id="lc_limit_div" runat="server" visible="false" >
    <div class="textbox_col_div">
        <input type="text" runat="server" id="prod_type" value="IMPLC" class="textbox_cl" style="width:15%;" disabled="disabled" />
         <input type="text" runat="server" id="prod_num" value="" class="textbox_cl" oninput="this.value = this.value.replace(/[^0-9.]/g, '').replace(/(\..*)\./g, '$1');"/>
        <button runat="server" id="btn_search" class="btn_search" onserverclick="btn_LC_rec_search">Search</button>
    </div>
     <div class="textbox_col_div" id="lc_infor_div" runat="server" visible="false">
   <%-- <div class="textbox_col_div">
          <label class="information_lbl_css"> <span style="color:red;">Customer Name : </span>Abhijeet Kashinath Gharat</label>

    </div>
     <div class="textbox_col_div">
          <label class="information_lbl_css"> <span style="color:red;">Account Number : </span>IMPBL   000000000000000100000000</label>

    </div>
     <div class="textbox_col_div">
          <label class="information_lbl_css"> <span style="color:red;">Sanction Limit : </span>1000000</label>

    </div>
     <div class="textbox_col_div">
          <label class="information_lbl_css"> <span style="color:red;">Utilized Limit : </span>800000</label>

    </div>
     <div class="textbox_col_div">
          <label class="information_lbl_css"> <span style="color:red;">Available Limit : </span>200000</label>

    </div>
      <div class="textbox_col_div">
          <label class="information_lbl_css"> <span style="color:red;">Limit Expiry : </span>13/07/2022</label>

    </div>--%>
         </div>
     <div class="textbox_col_div">
         <button runat="server" id="view_table" class="btn_cls_tbl" onserverclick="btn_LC_tbl_view">View Records</button>
     </div>

    <div class="table_output_css" id="lc_tab_div" runat="server" visible="false">
       <%-- <table class="result_tbl">
            <tr>
                <th class="tbl_th">Customer Name</th>
                 <th class="tbl_th">Account Number</th>
                 <th class="tbl_th">Customer Number</th>
                 <th class="tbl_th">Sanctioned Limit</th>                 
                  <th class="tbl_th">Limit Used</th>
                <th class="tbl_th">Expiry Date</th>

            </tr>
             <tr>
                <th class="tbl_td">Abhijeet Kashinath Gharat</th>
                 <th class="tbl_td">IMPBL/1</th>
                 <th class="tbl_td">78456</th>
                 <th class="tbl_td">5000000</th>                 
                  <th class="tbl_td">200000</th>
                <th class="tbl_td">13/07/2022</th>

            </tr>
             <tr>
                <th class="tbl_td">Abhijeet Kashinath Gharat</th>
                 <th class="tbl_td">IMPBL/1</th>
                 <th class="tbl_td">78456</th>
                 <th class="tbl_td">5000000</th>                 
                  <th class="tbl_td">200000</th>
                <th class="tbl_td">13/07/2022</th>

            </tr>
             <tr>
                <th class="tbl_td">Abhijeet Kashinath Gharat</th>
                 <th class="tbl_td">IMPBL/1</th>
                 <th class="tbl_td">78456</th>
                 <th class="tbl_td">5000000</th>                 
                  <th class="tbl_td">200000</th>
                <th class="tbl_td">13/07/2022</th>

            </tr>
        </table>--%>


    </div>
</div>
    <div class="Limit_tracking_div"  id="pcl_limit_trac_div"  visible="false" runat="server">
    <div class="textbox_col_div">
       <%-- <input type="text" runat="server" id="pcl_txt1" value="PCL" class="textbox_cl" style="width:15%;" disabled="disabled" />--%>
      <%-- <input type="radio" runat="server" id="radio_p" checked="checked" name="rd_p" class="radio_css" />PCL
         <input type="radio" runat="server" id="radio_o"  name="rd_p" class="radio_css" />AEBC--%>
        <asp:DropDownList  ID="ddl_pcl" runat="server" CssClass="textbox_cl" AutoPostBack ="false" >     
           <asp:ListItem Value="0">PCL</asp:ListItem>

            <asp:ListItem Value="1">AEBC</asp:ListItem>
    </asp:DropDownList>

         <input type="text" runat="server" id="pcl_txt2" value="" class="textbox_cl" oninput="this.value = this.value.replace(/[^0-9.]/g, '').replace(/(\..*)\./g, '$1');"/>
        <button type="button" runat="server" id="btn_pcl_search" class="btn_search" onserverclick="btn_pcl_rec_search">Search</button>
    </div>
     <div class="textbox_col_div" id="PCL_info_view" runat="server" visible="false">
    <%--<div class="textbox_col_div">
          <label class="information_lbl_css"> <span style="color:red;">Customer Name : </span>Abhijeet Kashinath Gharat</label>
    </div>
     <div class="textbox_col_div">
          <label class="information_lbl_css"> <span style="color:red;">Account Number : </span>PCL     000000000000000100000000</label>
    </div>
     <div class="textbox_col_div">
          <label class="information_lbl_css"> <span style="color:red;">Sanction Limit : </span>1000000</label>
    </div>
     <div class="textbox_col_div">
          <label class="information_lbl_css"> <span style="color:red;">Utilized Limit : </span>800000</label>
    </div>
     <div class="textbox_col_div">
          <label class="information_lbl_css"> <span style="color:red;">Available Limit : </span>200000</label>
    </div>
      <div class="textbox_col_div">
          <label class="information_lbl_css"> <span style="color:red;">Limit Expiry : </span>13/07/2022</label>
    </div>--%>
         </div>
     <div class="textbox_col_div">
         <button type="button" runat="server" id="btn_view_pcl" class="btn_cls_tbl" onserverclick="btn_PCL_tbl_view">View Records</button>
     </div>
    <div class="table_output_css" id="pcl_tbl" runat="server" visible="false">
    <%--    <table class="result_tbl">
            <tr>
                <th class="tbl_th">Customer Name</th>
                 <th class="tbl_th">Account Number</th>
                 <th class="tbl_th">Customer Number</th>
                 <th class="tbl_th">Sanctioned Limit</th>                 
                  <th class="tbl_th">Limit Used</th>
                <th class="tbl_th">Expiry Date</th>

            </tr>
             <tr>
                <th class="tbl_td">Abhijeet Kashinath Gharat</th>
                 <th class="tbl_td">IMPBL/1</th>
                 <th class="tbl_td">78456</th>
                 <th class="tbl_td">5000000</th>                 
                  <th class="tbl_td">200000</th>
                <th class="tbl_td">13/07/2022</th>

            </tr>
             <tr>
                <th class="tbl_td">Abhijeet Kashinath Gharat</th>
                 <th class="tbl_td">IMPBL/1</th>
                 <th class="tbl_td">78456</th>
                 <th class="tbl_td">5000000</th>                 
                  <th class="tbl_td">200000</th>
                <th class="tbl_td">13/07/2022</th>

            </tr>
             <tr>
                <th class="tbl_td">Abhijeet Kashinath Gharat</th>
                 <th class="tbl_td">IMPBL/1</th>
                 <th class="tbl_td">78456</th>
                 <th class="tbl_td">5000000</th>                 
                  <th class="tbl_td">200000</th>
                <th class="tbl_td">13/07/2022</th>

            </tr>
        </table>--%>


    </div>
</div>

    <div class="Limit_tracking_div" id="moni_div" runat="server">

        <div class="textbox_col_div">
       
<table class="moni_tbl" >
    <tr>
        <td class="info_tbl_td"><span style="color:red;">LC Limits  </span></td>
        <td class="info_tbl_td_1" id="LC_CNT_flg" runat="server">In Control</td>
        <td class="info_tbl_td"><span style="color:red;">PCL Limits  </span></td>
        <td class="info_tbl_td_1" id="PCL_CNT_flg" runat="server">In Control</td>
    </tr>
     <tr>
        <td class="info_tbl_td"><span style="color:red;">Active Customer  </span></td>
        <td class="info_tbl_td_1" id="LC_act_con" runat="server">5</td>
        <td class="info_tbl_td"><span style="color:red;">Active Customer  </span></td>
        <td class="info_tbl_td_1" id="PCL_act_con" runat="server">25</td>
    </tr>
    <tr>
        <td class="info_tbl_td"><span style="color:red;">Current date  </span></td>
        <td class="info_tbl_td_1" id="LC_cur_date" runat="server">16/07/2022</td>
        <td class="info_tbl_td"><span style="color:red;">Current date   </span></td>
        <td class="info_tbl_td_1" id="PCL_cur_date" runat="server">16/07/2022</td>
    </tr>
    <tr>
        <td class="info_tbl_td"><span style="color:red;">Ongoing transactions  </span></td>
        <td class="info_tbl_td_1" id="LC_Ongoing_trn" runat="server">2</td>
        <td class="info_tbl_td"><span style="color:red;">Ongoing transactions  </span></td>
        <td class="info_tbl_td_1" id="PCL_Ongoing_trn" runat="server">1</td>
    </tr>
    <tr>
        <td class="info_tbl_td"><span style="color:red;">Status  </span></td>
        <td class="info_tbl_td_1" id="LC_TR_Stat" runat="server">Authorize : 1 / Unauthorize : 1</td>
        <td class="info_tbl_td"><span style="color:red;">Status  </span></td>
        <td class="info_tbl_td_1" id="PCL_TR_Stat" runat="server">Authorize : 0 / Unauthorize : 1</td>
    </tr>
</table>

        </div>
         <div class="textbox_col_div">
               <table class="moni_tbl">
                   <tr><td class="heading_td">
                       Report: Accounts crossing the Sanctioned limit 
                       </td></tr>
               </table>
         </div>
         <div class="textbox_col_div" id="reaction_div" runat="server">
             <table class="moni_tbl">
                 <tr>
                     <td class="report_td">
                         IMPLC/1
                     </td>
                      <td class="report_td_2">
                         Repute Engineering
                     </td>
                      <td class="report_td">
                         Limit Exceeded by
                     </td>
                      <td class="report_td_2">
                         300000
                     </td>
                 </tr>

             </table>

         </div>

    </div>

   


</div>


    </div>
    
       
      
   

     </ContentTemplate> 
    </asp:UpdatePanel>
   
    </form>
</body>
</html>
