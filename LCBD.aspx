<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LCBD.aspx.cs" Inherits="BCCBExamPortal.LCBD" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <link rel="icon" href="Resources/bccb_logo.png"/>
    <title>LCBD</title>
     <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
     <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <script src="Resources/Script/LCBD.js"></script>
     <link href="SMS.css" rel="stylesheet" />
        <link href="BGreport.css" rel="stylesheet" />
     <link href="Employee.css" rel="stylesheet" />
    <link href="LCBD.css" rel="stylesheet"/>

</head>
<body>
    <form id="form1" runat="server">
           <asp:ScriptManager ID="scriptmanager1" runat="server"></asp:ScriptManager>
          <asp:UpdatePanel ID="updatepnl" runat="server" UpdateMode="Conditional" >

<ContentTemplate> 
    <div class="Heading_sec">
    LCBD DATA Collection <asp:Label ID="sessionlbl" runat="server" Visible="false" Text=""></asp:Label>
    </div>
      <div style="width:100%;height:auto;opacity:0.80;filter: alpha(opacity=80);border-radius:20px;background-color:#1e0f18;text-align:center;"><asp:Menu ID="Menu1" runat="server" Orientation="Horizontal" CssClass="menu">
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

    <div class="general_info">
        <button type="button" id="btn_general" runat="server" class="top_btn_active"  onserverclick="button_change_act"  >Data Entry Section</button>
            <button type="button" id="btn_reports" runat="server" class="top_btn" onserverclick="button_change_act2"  >Reports and Analysis</button>
         <button type="button" id="btn_pending_lc" runat="server" class="top_btn" onserverclick="button_change_act3"  >Pending LC Details</button>
    </div>


<div class="main_container_lcbd_data" id="main_dr_div" runat="server">
    <div class="Gen_area_div">
        <div class="field_name_div">Branch :</div>
         <div class="field_data"><input type="text" value="Dadar West" disabled="disabled" class="txt_class"/></div>
    </div>
     <div class="Gen_area_div">
        <div class="field_name_div">Transaction Reference number:</div>
         <div class="field_data"><input type="text" id="txt_rrn" runat="server" class="txt_class" /></div>
          <button type="button" id="get_lcbd_data" runat="server"  onserverclick="get_lcbc_rrn_data" class="btn_sub_type_cl">Search</button>
    </div>
     <div class="Gen_area_div">
        <div class="field_name_div">Transaction Date:</div>
         <div class="field_data"><asp:TextBox ID="dt_tra" runat="server" placeholder="mm/dd/yyyy" Textmode="Date" ReadOnly = "false" CssClass="calender_cs"></asp:TextBox></div>
    </div>
    <div class="Gen_area_div">
        <div class="field_name_div">Maturity Date:</div>
         <div class="field_data"><asp:TextBox ID="dt_matu" runat="server" placeholder="mm/dd/yyyy" Textmode="Date" ReadOnly = "false" CssClass="calender_cs" OnTextChanged="get_days" AutoPostBack="true"></asp:TextBox></div>
    </div>
      <div class="Gen_area_div">
        <div class="field_name_div">Customer Name:</div>
     <div class="field_data"><asp:DropDownList runat="server" ID="ddlcustomer" CssClass="ddl_br_stlxx" AutoPostBack = "true" OnSelectedIndexChanged="ddl_namechange_react" >     
            
    </asp:DropDownList>
</div>
    </div>
      <div class="Gen_area_div" id="new_cut_div" runat="server">
        <div class="field_name_div">New Customer Name:</div>
         <div class="field_data"><input type="text" id="Cust_Name" runat="server" class="txt_class" /></div>
    </div>
     <div class="Gen_area_div">
        <div class="field_name_div">Customer Number:</div>
         <div class="field_data"><input type="text" id="Cust_num" runat="server" class="txt_class" /></div>
    </div>
     <div class="Gen_area_div">
        <div class="field_name_div">Account Number:</div>
         <div class="field_data"><input type="text" id="Acc_Num" runat="server" class="txt_class" /></div>
    </div>
      <div class="Gen_area_div">
        <div class="field_name_div">Nature of business activity:</div>
         <div class="field_data"><input type="text" id="natu_busi_act" runat="server" class="txt_class" /></div>
    </div>

      <div class="Gen_area_div">
        <div class="field_name_div">LC Reference Number:</div>
         <div class="field_data"><input type="text" id="LC_rrn" runat="server" class="txt_class" /></div>
          <button type="button" id="btn_lc_chk" runat="server"  onserverclick="get_lc_data" class="btn_sub_type_cl">Check LC</button>
         
    </div>
     <div class="Gen_area_div">
        <div class="field_name_div">LC Issuing Bank:</div>
         <div class="field_data"><input type="text" id="LC_issue_bank" runat="server" class="txt_class" /></div>
    </div> 
      <div class="Gen_area_div">
        <div class="field_name_div">LC Applicant:</div>
         <div class="field_data"><input type="text" id="LC_App" runat="server" class="txt_class" /></div>
    </div> 
     <div class="Gen_area_div">
        <div class="field_name_div">LC Beneficiary:</div>
         <div class="field_data"><input type="text" id="LC_Beni" runat="server" class="txt_class" /></div>
    </div> 
     <div class="Gen_area_div">
        <div class="field_name_div">Place of Expiry:</div>
         <div class="field_data"><input type="text" id="Pl_of_exp" runat="server" class="txt_class" /></div>
    </div>
     <div class="Gen_area_div">
        <div class="field_name_div">LC Amount:</div>
         <div class="field_data"><input type="text" id="LC_Amt" runat="server" class="txt_class" /></div>
    </div>
     <div class="Gen_area_div">
        <div class="field_name_div">Currency Code:</div>
         <div class="field_data"><input type="text" id="Currency_txt" runat="server" class="txt_class" value="INR" disabled="disabled" /></div>
    </div>
     <div class="Gen_area_div">
        <div class="field_name_div">Tenor/Drafts at:</div>
         <div class="field_data"><input type="text" id="Tenor_txt" runat="server" class="txt_class" /></div>
    </div> 
     <div class="Gen_area_div">
        <div class="field_name_div">Loading on Board/Dispatch/taking in Charge at/from:</div>
         <div class="field_data"><input type="text" id="loading_bd" runat="server" class="txt_class" /></div>
    </div> 
    <div class="Gen_area_div">
        <div class="field_name_div">For Transportation to:</div>
         <div class="field_data"><input type="text" id="trans_to" runat="server" class="txt_class" /></div>
    </div> 
    <div class="Gen_area_div">
        <div class="field_name_div">LC restricted  to the Bank / Available with..:</div>
         <div class="field_data"><textarea id="LC_rest_bnk" runat="server"  class="txt_class"></textarea></div>
    </div>   
      <div class="Gen_area_div">
        <div class="field_name_div">Latesh Shipment date:</div>
         <div class="field_data"><asp:TextBox ID="ship_date" runat="server" placeholder="mm/dd/yyyy" Textmode="Date" ReadOnly = "false" CssClass="calender_cs"></asp:TextBox></div>
    </div>
     <div class="Gen_area_div">
        <div class="field_name_div">            
            Actual Shipment date:</div>
         <div class="field_data"><asp:TextBox ID="late_ship_date_new" runat="server" placeholder="mm/dd/yyyy" Textmode="Date" ReadOnly = "false" CssClass="calender_cs"></asp:TextBox></div>
         <div class="tooltip">
    Info
    <div class="left">
        <h3>Newly Added field</h3>
        <ul>
            <li>This field came into existence as per the LCBD departmental request</li>
            <li>Effective Date 7/11/22.</li>
             <li>This field should be inputted hence forward by dept.</li>
        </ul>
        <i></i>
    </div>
</div>
    </div>
    <div class="Gen_area_div">
        <div class="field_name_div"> Transshipment Allowed?:</div>
         <div class="field_data"><asp:CheckBox ID="trans_ship" runat="server" Text="Allowed"/></div>
    </div>
    <div class="Gen_area_div">
        <div class="field_name_div">Partial Shipment Allowed?:</div>
         <div class="field_data"><asp:CheckBox ID="parti_ship" runat="server" Text="Allowed"/></div>
    </div>   
    
    

   <div class="Gen_area_div">
        <div class="field_name_div">Whether LC restricted  to the Bank :</div>
         <div class="field_data"><asp:CheckBox ID="LC_Res" runat="server" Text="Yes" Checked="True" Enabled="False"/></div>
    </div>
     <div class="Gen_area_div">
        <div class="field_name_div">Whether NOC from regular banker of the beneficiary obtained:</div>
         <div class="field_data"><asp:CheckBox ID="CHK_Noc" runat="server" Text="Yes"/></div>
    </div>    
     <div class="Gen_area_div">
        <div class="field_name_div">Amount disbursed after discounting:</div>
         <div class="field_data"><input type="text" id="amt_dis_dis" runat="server" class="txt_class" oninput="this.value = this.value.replace(/[^0-9.]/g, '').replace(/(\..*)\./g, '$1');"/></div>
    </div>
   
     <div class="Gen_area_div">
        <div class="field_name_div">Due date of receipt of funds under discounted LC :</div>
         <div class="field_data"><asp:TextBox ID="Due_dt_res" runat="server" placeholder="mm/dd/yyyy" Textmode="Date" ReadOnly = "false" CssClass="calender_cs"  OnTextChanged="get_days" AutoPostBack="true"></asp:TextBox></div>
    </div>

     <div class="Gen_area_div">
        <div class="field_name_div">Invoice number:</div>
         <div class="field_data"><input type="text" id="txt_inv" runat="server" class="txt_class" /></div>
    </div>
     <div class="Gen_area_div">
        <div class="field_name_div">Invoice/ Bill OF Exchange Amount :</div>
         <div class="field_data"><input type="text" id="txt_amt_bill" runat="server" class="txt_class" oninput="this.value = this.value.replace(/[^0-9.]/g, '').replace(/(\..*)\./g, '$1');" onblur="cal1();"/></div>
    </div>  

    <div class="Gen_area_div">
        <div class="field_name_div">Whether funds received from  the issuing bank on the due ? :</div>
         <div class="field_data"><asp:CheckBox ID="chk_funds" runat="server" Text="Yes"/></div>
    </div>
    <div class="Gen_area_div">
        <div class="field_name_div">Whether outstanding as on March 31 :</div>
         <div class="field_data"><asp:CheckBox ID="chk_outsta" runat="server" Text="Yes"/></div>
    </div>
    <div class="Gen_area_div">
        <div class="field_name_div">Agent Name  :</div>
         <div class="field_data"><input type="text" id="agent_name" runat="server" class="txt_class" /></div>
    </div> 
     <div class="Gen_area_div">
        <div class="field_name_div">Latest negotiation date :</div>
         <div class="field_data"><asp:TextBox ID="lt_nego_dt" runat="server" placeholder="mm/dd/yyyy" Textmode="Date" ReadOnly = "false" CssClass="calender_cs"></asp:TextBox></div>
    </div>
    <div class="Gen_area_div">
        <div class="field_name_div">Payment Date for LC Discounted :</div>
         <div class="field_data"><asp:TextBox ID="pay_dt_lc_dt" runat="server" placeholder="mm/dd/yyyy" Textmode="Date" ReadOnly = "false" CssClass="calender_cs"  OnTextChanged="get_days" AutoPostBack="true"></asp:TextBox></div>
    </div> 
      <div class="Gen_area_div">
        <div class="field_name_div">No of Days   :</div>
         <div class="field_data"><input type="text"  id="num_dys" runat="server" class="txt_class" oninput="this.value = this.value.replace(/[^0-9.]/g, '').replace(/(\..*)\./g, '$1');" disabled="disabled"/></div>
    </div> 
      <div class="Gen_area_div">
        <div class="field_name_div">Rate of Interest  on LC  :</div>
         <div class="field_data"><input type="text"  id="rate_int" runat="server" class="txt_class" onblur="rate_interest_blur();" oninput="this.value = this.value.replace(/[^0-9.]/g, '').replace(/(\..*)\./g, '$1');"/></div>
    </div>
    <div class="Gen_area_div">
        <div class="field_name_div"> Rate of Margin :</div>
         <div class="field_data"><input type="text" id="rate_mar" runat="server" class="txt_class" disabled="disabled" /></div>
    </div> 
     <div class="Gen_area_div">
        <div class="field_name_div"> No of days of margin :</div>
         <div class="field_data"><input type="text"  id="no_dys_mar" runat="server" class="txt_class" value="5" disabled="disabled" oninput="this.value = this.value.replace(/[^0-9.]/g, '').replace(/(\..*)\./g, '$1');"/></div>
    </div>
     <div class="Gen_area_div">
        <div class="field_name_div"> Margin of Days [LCBDAM] :</div>
         <div class="field_data"><input type="text" id="mar_days" runat="server" class="txt_class" disabled="disabled" /></div>
    </div>
     <div class="Gen_area_div">
        <div class="field_name_div"> LC Handling charges [PL 3901] :</div>
         <div class="field_data"><input type="text" id="lc_hand_chg" runat="server" class="txt_class" value="1475"/></div>
    </div>
     <div class="Gen_area_div">
        <div class="field_name_div"> LC Advising  charges [PL 3902] :</div>
         <div class="field_data"><input type="text" id="lc_adv_chg" runat="server" class="txt_class" value="590" /></div>
    </div>
     <div class="Gen_area_div">
        <div class="field_name_div"> SFMS Charges [PL 3905] :</div>
         <div class="field_data"><input type="text" id="sfms_chg" runat="server" class="txt_class" value="" /></div>
    </div>
     <div class="Gen_area_div">
        <div class="field_name_div">Service Charge [PL 3029] :</div>
         <div class="field_data"><input type="text" id="serv_chg" runat="server" class="txt_class" value="" /></div>
    </div>
     <div class="Gen_area_div">
        <div class="field_name_div"> Courier  Charge [PL 4023]:</div>
         <div class="field_data"><input type="text" id="cour_chg" runat="server" class="txt_class" /></div>
    </div>
     <div class="Gen_area_div">
        <div class="field_name_div">  Amount received Y/N  :</div>
         <div class="field_data"><asp:CheckBox  ID="chk_amt_rec" runat="server" Text="Yes"/></div>
    </div>
     <div class="Gen_area_div">
        <div class="field_name_div">Actual Amount received  :</div>
         <div class="field_data"><input type="text" id="txt_amt_rec" runat="server" class="txt_class" oninput="this.value = this.value.replace(/[^0-9.]/g, '').replace(/(\..*)\./g, '$1');" onblur="cal1();" /> </div>
    </div>
     <div class="Gen_area_div">
        <div class="field_name_div"> Date of Payment received (DD/MM/YY):</div>
         <div class="field_data"><asp:TextBox ID="dt_pay_rec" runat="server" placeholder="mm/dd/yyyy" Textmode="Date" ReadOnly = "false" CssClass="calender_cs" OnTextChanged="get_days" AutoPostBack="true"></asp:TextBox></div>
    </div>
     <div class="Gen_area_div">
        <div class="field_name_div"> Excess/ (Less) received against Invoice  :</div>
         <div class="field_data"><input type="text" id="excess_less_in" runat="server" class="txt_class" disabled="disabled" /></div>
    </div>
     <div class="Gen_area_div">
        <div class="field_name_div">No.of days Delayed /[No Of Days Early] :</div>
         <div class="field_data"><input type="text"  id="delayed_early" runat="server" class="txt_class" oninput="this.value = this.value.replace(/[^0-9.]/g, '').replace(/(\..*)\./g, '$1');" disabled="disabled" /></div>
    </div>
     <div class="Gen_area_div">
        <div class="field_name_div"> Overdue Interest @ ROI+2 %:</div>
         <div class="field_data"><input type="text" id="over_int" runat="server" class="txt_class" disabled="disabled" /></div>
    </div>
      <div class="Gen_area_div">
        <div class="field_name_div">Interest [PL 3903] :</div>
         <div class="field_data"><input type="text"  id="Int_Pl_3903" runat="server" class="txt_class" oninput="this.value = this.value.replace(/[^0-9.]/g, '').replace(/(\..*)\./g, '$1');" disabled="disabled" /></div>
    </div> 
    <div class="Gen_area_div">
        <div class="field_name_div">Net Amount reversed to Beneficiary:</div>
         <div class="field_data"><input type="text"  id="Net_rev" runat="server" class="txt_class" oninput="this.value = this.value.replace(/[^0-9.]/g, '').replace(/(\..*)\./g, '$1');" disabled="disabled" /></div>
    </div> 
     <div class="Gen_area_div">
        <div class="field_name_div">Interest revered for early payment received:</div>
         <div class="field_data"><input type="text" id="intrev" runat="server" class="txt_class" disabled="disabled" /></div>
    </div>   
     <div class="Gen_area_div">
        <div class="field_name_div">Margin reversed to Beneficiary:</div>
         <div class="field_data"><asp:CheckBox  ID="mar_rev_ben_chk" runat="server" Text="Yes"/></div>
    </div>
     <div class="Gen_area_div">
        <div class="field_name_div">Date of Amount paid/ received to/from Beneficiary:</div>
         <div class="field_data"><asp:TextBox ID="dt_amt_paid" runat="server" placeholder="mm/dd/yyyy" Textmode="Date" ReadOnly = "false" CssClass="calender_cs"></asp:TextBox></div>
    </div>
    </div>
    <div class="main_container_lcbd_data" id="report_div" runat="server" visible="false">

        <div class="general_info1">
            <input type="text" runat="server" id="search_txt" placeholder="Search Contract Details" class="txt_class"/><button type="button" id="btn_search" runat="server" class="search_btn" onserverclick="search_item" >Search</button>
        </div>


        <div class="tbl_data" id="tbl_data_bind" runat="server">
           <%-- <table class="tbl_con_hold" id="tbl_data_bind" runat="server">
           
            </table>--%>
            
            
        </div>

        <div  class="drop_dn_div">
            <asp:DropDownList runat="server" ID="ddl_fin_yr" CssClass="ddl_br_stlxx" AutoPostBack = "true" OnSelectedIndexChanged="ddl_namechange_react" >     
            
    </asp:DropDownList>

        </div>
        <div class="graph1_div">

          <asp:Chart ID="Chart2" runat="server" CssClass="graphcss" OnLoad="Chart2_Load" Width="800px"  >
    <Titles>
        <asp:Title ShadowOffset="3" Name="Items" />
    </Titles>
   <Legends>
        <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="LCBD" LegendStyle="Row"  Font="Calibri"  />
    </Legends>
    <Series>
        <asp:Series Name="LCBD" />
    </Series>
    <ChartAreas>
        <asp:ChartArea Name="ChartArea2" BorderWidth="0" AlignmentOrientation="Horizontal" />
    </ChartAreas>
                    </asp:Chart>
        </div>
         <div class="graph1_div">

          <asp:Chart ID="Chart3" runat="server" CssClass="graphcss" OnLoad="Chart3_Load" Width="800px"  >
    <Titles>
        <asp:Title ShadowOffset="3" Name="Items" />
    </Titles>
   <Legends>
        <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="LCBD" LegendStyle="Row"  Font="Calibri"  />
    </Legends>
    <Series>
        <asp:Series Name="LCBD" />
    </Series>
    <ChartAreas>
        <asp:ChartArea Name="ChartArea2" BorderWidth="0" AlignmentOrientation="Horizontal" />
    </ChartAreas>
                    </asp:Chart>
        </div>
        <div class="report_div">
            <div class="report_heading">Contract out-standing Report</div>
             <div class="field_name_div"> Date of Report Position :</div>
         <div class="field_name_div_left"><asp:TextBox ID="report_date" runat="server" placeholder="mm/dd/yyyy" Textmode="Date" ReadOnly = "false" CssClass="calender_cs" OnTextChanged="get_days" AutoPostBack="true"></asp:TextBox></div>
               <button type="button" id="Button1" runat="server"  onserverclick="check_report1" class="btn_sub_type_cl">Check</button>
    
        </div>
         <div class="report_div">

             <div class="field_name_div"> Outstanding : </div>
         <div class="field_name_div_left"><div class="label_css" id="div_outstanding" runat="server">0</div></div>
    
        </div>
          <div class="report_div">

             <div class="field_name_div"> Contract Count : </div>
         <div class="field_name_div_left"><div class="label_css" id="contr_count" runat="server">0</div></div>
    
        </div>

        <div class="report_area">
             <div class="report_heading">Customerwise Report:</div>
            

 <div class="Gen_area_div">
        <div class="field_name_div">Customer Name:</div>
     <div class="field_data"><asp:DropDownList runat="server" ID="ddlcustomer2" CssClass="ddl_br_stlxx" AutoPostBack = "true" OnSelectedIndexChanged="ddl_namechange_react" >     
            
    </asp:DropDownList>
           </div>
</div>
      <div class="Gen_area_div">
        <div class="field_name_div">All Contracts :</div>
         <div class="field_data"><asp:CheckBox ID="chk_all" runat="server" Text="Yes"/></div>
          <button type="button" id="Button2" runat="server"  onserverclick="check_report2" class="btn_sub_type_cl">Check</button>
    </div>
      <div class="Gen_area_div">
        <div class="report_sec_left" id="con_count" runat="server">Contracts Count:<span style="color:red;">0</span></div>
         <div class="report_sec_right" id="con_amt" runat="server">Total Amount :<span style="color:red;">0</span></div>  
    </div>
             <div class="Gen_area_div">
        <div class="report_sec_left" id="con_act" runat="server">Active Contract:<span style="color:red;">0</span></div>
         <div class="report_sec_right" id="con_matu_out" runat="server">Mature but Outstanding Amount :<span style="color:red;">0</span></div>  
    </div>
     <div >
         <div class="tbl_data3">
          <table class="tbl_con_hold_head" id="new_bind_head" runat="server">
              <tr>
                 <td class="r1_th1">Reference Number</td> 
                    <td class="r1_th2">Transaction Date</td> 
                  <td class="r1_th1">Maturity Date</td> 
                  <td class="r1_th1">Customer Name</td> 
                  <td class="r1_th1">LC RRN</td> 
                  <td class="r1_th1">Bill Of Exchange Amount</td> 
                   <td class="r1_th2">Actual Shipment date</td> 
              </tr>
       
            </table>
                 </div>
         <div class="tbl_data2" id="data_repo_1" runat="server">
           <%-- <table class="tbl_con_hold_new" id="new_bind" runat="server">
              <tr><td class="r1_td1">Reference Number</td> 
                    <td class="r1_td2">Transaction Date</td> 
                  <td class="r1_td1">Maturity Date</td> 
                  <td class="r1_td1">Customer Name</td> 
                  <td class="r1_td1">LC RRN</td> 
                  <td class="r1_td1">Bill Of Exchange Amount</td> 
                   <td class="r1_td2">Actual Shipment date</td> 
              </tr>
           
            </table>--%>
           </div> 
        </div>
  

           

        </div>

          <div class="report_area">
             <div class="report_heading">Error Data Report:</div>
               <div class="Gen_area_div_x">     
          <button type="button" id="error_report_btn" runat="server"  onserverclick="check_report3" class="btn_sub_type_er">Generate Report</button>
    </div>
               <div class="tbl_data3">
          <table class="tbl_con_hold_head" id="Table1" runat="server">
              <tr>
                 <td class="rx1_th1">Reference Number</td> 
                    <td class="rx1_th2">Transaction Date</td> 
                  <td class="rx1_th1">Maturity Date</td> 
                  <td class="rx1_th1">Customer Name</td> 
                  <td class="rx1_th1">Amount Received</td> 
                  <td class="rx1_th1">Actual Amount Received</td> 
                   <td class="rx1_th2">Actual Amount date</td> 
              </tr>
      
            </table>
                 </div>
               <div class="tbl_data2" id="final_error_repo" runat="server">
        
           </div> 

              </div>
          <div class="report_area">
             <div class="report_heading">Blunder Data Report:</div>
                <div class="Gen_area_div_x">     
          <button type="button" id="blunder_report_btn" runat="server"  onserverclick="check_report4" class="btn_sub_type_er">Generate Report</button>
    </div>
 <div class="tbl_data2" id="blunder_summary" runat="server">
        
           </div> 
              <div class="report_div_blunder" id="div_lbl" runat="server"></div>
 <div class="tbl_data2" id="blunder_data" runat="server">
        
           </div> 
              </div>
    </div>



    <div class="main_container_lcbd_data" id="pending_lc_div" runat="server" visible="false">
        <div class="pending_lc_btn_list">

            <div class="proress_btn"><button type="button" id="lc_data_refine" runat="server" class="refine_btn" onserverclick="refine_lc_data">Refine Data</button></div>
            <div class="progress_area"><div class="progress_div" ><div class="progress_div_1" id="progress_div_1" runat="server"></div></div></div>
        </div>
        

   
    <div class="field_sec">
    <asp:Label CssClass="lbl_head_master" runat="server" ID="lbl_new_rd" Visible="true">LC record to be added Manually</asp:Label>
    <label class="switch">
 <%-- <input type="checkbox" id="chk_type" runat="server" onchange=""  OnCheckedChanged="check_change_fn" >--%>
    <asp:CheckBox ID="chk_rec" runat="server" AutoPostBack="true" OnCheckedChanged="check_change_fn" />
  <span class="slider round"></span>
</label>   <asp:Label CssClass="lbl_head_slave" runat="server" ID="lbl_old_rec" Visible="true">Edit SFMS Record</asp:Label>
</div>

    <div class="div_x1">

 <div class="Gen_area_div" id="div_EM" runat="server">
        <div class="field_name_div">LC record to be entered Manually:</div>
     <div class="field_data"><asp:DropDownList runat="server" ID="ddl_pending_lc" CssClass="ddl_br_stlxx" AutoPostBack = "true" OnSelectedIndexChanged="ddl_namechange_react" >     
            
    </asp:DropDownList>
</div>
    </div>
 <div class="Gen_area_div" id="div_ED" runat="server" visible="false">
        <div class="field_name_div">LC Reference Number  :</div>
         <div class="field_data"><input type="text" id="lc_rrn_num" runat="server" class="txt_class" /> </div>
     <button type="button" id="btn_lc_da_search" runat="server"  onserverclick="get_lc_data_2" class="btn_sub_type_cl">Check LC</button>
    </div>


    </div>


    <div class="div_x2">
        <div class="Gen_area_div">
        <div class="field_name_div">LC Issuing Bank :</div>
         <div class="field_data"><input type="text" id="lc_issue_bnk_2" runat="server" class="txt_class" /> </div>
     
    </div>
        <div class="Gen_area_div">
        <div class="field_name_div">Date of Issue (DD/MM/YY) (31C):</div>
         <div class="field_data"><asp:TextBox ID="lc_dt_iss_2" runat="server" placeholder="mm/dd/yyyy" Textmode="Date" ReadOnly = "false" CssClass="calender_cs" OnTextChanged="get_days" AutoPostBack="true"></asp:TextBox></div>
    </div>
<div class="Gen_area_div">
        <div class="field_name_div">LC Applicant (50):</div>
         <div class="field_data"><input type="text" id="lc_applicant_2" runat="server" class="txt_class" /> </div>
     
    </div>
        <div class="Gen_area_div">
        <div class="field_name_div">LC Beneficiary (59):</div>
         <div class="field_data"><input type="text" id="lc_beni_2" runat="server" class="txt_class" /> </div>
     
    </div>
         <div class="Gen_area_div">
        <div class="field_name_div">Place of Expiry (31D):</div>
         <div class="field_data"><input type="text" id="lc_place_exp_2" runat="server" class="txt_class" /> </div>
     
    </div>
    <div class="Gen_area_div">
        <div class="field_name_div"> Date of Expiry (DD/MM/YY) (31D):</div>
         <div class="field_data"><asp:TextBox ID="dt_exp_2" runat="server" placeholder="mm/dd/yyyy" Textmode="Date" ReadOnly = "false" CssClass="calender_cs" OnTextChanged="get_days" AutoPostBack="true"></asp:TextBox></div>
    </div>
         <div class="Gen_area_div">
        <div class="field_name_div">LC Amount (32B):</div>
         <div class="field_data"><input type="text" id="lc_amt_2" runat="server" class="txt_class" oninput="this.value = this.value.replace(/[^0-9.]/g, '').replace(/(\..*)\./g, '$1');"/> </div>
     
    </div>
        <div class="Gen_area_div">
        <div class="field_name_div">LC Currency (32B):</div>
     <div class="field_data"><input type="text"  id="lc_currency" runat="server" class="txt_class"  disabled="disabled" value="INR" />
</div>
    </div>
 <div class="Gen_area_div">
        <div class="field_name_div">Latest Date of Shipment (DD/MM/YY) (44C):</div>
         <div class="field_data"><asp:TextBox ID="dt_ship_lat_2" runat="server" placeholder="mm/dd/yyyy" Textmode="Date" ReadOnly = "false" CssClass="calender_cs" OnTextChanged="get_days" AutoPostBack="true"></asp:TextBox></div>
    </div>
        <div class="Gen_area_div">
        <div class="field_name_div">Positive Tolerence (39A):</div>
         <div class="field_data"><input type="text"  id="pos_tol" runat="server" class="txt_class" oninput="this.value = this.value.replace(/[^0-9.]/g, '').replace(/(\..*)\./g, '$1');" value="0"/></div>
    </div> 
        <div class="Gen_area_div">
        <div class="field_name_div">Negative Tolerence (39A):</div>
         <div class="field_data"><input type="text"  id="neg_tol" runat="server" class="txt_class" oninput="this.value = this.value.replace(/[^0-9.]/g, '').replace(/(\..*)\./g, '$1');"  value="0" /></div>
    </div> 
<div class="Gen_area_div">
        <div class="field_name_div">Available With .. By ..  (41A):</div>
         <div class="field_data"><input type="text" id="lc_avail_with" runat="server" class="txt_class" /> </div>
     
    </div>
<div class="Gen_area_div">
        <div class="field_name_div">Place of Dispatch (44A):</div>
         <div class="field_data"><input type="text" id="lc_pl_dis" runat="server" class="txt_class" /> </div>
     
    </div>
        <div class="Gen_area_div">
        <div class="field_name_div">Place of Delivery (44B):</div>
         <div class="field_data"><input type="text" id="lc_pl_del" runat="server" class="txt_class" /> </div>
     
    </div>
        <div class="Gen_area_div">
        <div class="field_name_div">Partial Shipment (43P):</div>
         <div class="field_data"><asp:CheckBox ID="chk_par_ship" runat="server" Text="Yes"/></div>
    </div>
        <div class="Gen_area_div">
        <div class="field_name_div">Transhipment (43T):</div>
         <div class="field_data"><asp:CheckBox ID="tra_ship_2" runat="server" Text="Yes"/></div>
    </div>
         <div class="Gen_area_div">
        <div class="field_name_div">Shipment Period (44D):</div>
         <div class="field_data"><input type="text"  id="ship_perio_2" runat="server" class="txt_class" oninput="this.value = this.value.replace(/[^0-9.]/g, '').replace(/(\..*)\./g, '$1');" disabled="disabled" /></div>
    </div> 
        <div class="Gen_area_div">
        <div class="field_name_div">Description of Goods and/or Services (45A):</div>
        <div class="field_data"><input type="text" id="goods_serv_txt" runat="server" class="txt_class" /> </div>
              </div>
         <div class="Gen_area_div">
        <div class="field_name_div">Port of Loading(44E):</div>
        <div class="field_data"><input type="text" id="port_load_txt" runat="server" class="txt_class" /> </div>
              </div>
         <div class="Gen_area_div">
        <div class="field_name_div">Port of Discharge(44F):</div>
        <div class="field_data"><input type="text" id="port_dis_txt" runat="server" class="txt_class" /> </div>
              </div>
        <div  class="Gen_area_div" style="text-align:center;">
             <button type="button" id="submit_lc_chk" runat="server"  onserverclick="add_lc_data" class="btn_sub_type_cl" >Edit</button>
            <input type="hidden" runat="server" value="" id="hdn_lc_rrn" />
        </div>

    </div>

 </div>


    <div class="fooetr_div">Developed By BCCB IT</div>
   

    <button type="button" class="float" id="lcbd_submit" runat="server" onserverclick="on_btn_submit" onclick="validatefield();" disabled="disabled">ADD</button>
   <label id="lbl_id_hdn" runat="server" visible="false"></label>
      <label id="Label1" runat="server" visible="true"></label>
    </ContentTemplate>
      </asp:UpdatePanel>
    </form>
  
</body>
</html>
