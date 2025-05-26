<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmViewSigns.aspx.cs" Inherits="BCCBExamPortal.frmViewSigns" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<%--<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">


    <div>
    <asp:Label ID="lblImgFiles" runat="server" Text="Data in Image Files " Visible="false"></asp:Label>
    <asp:GridView ID="gvImages" runat="server" AutoGenerateColumns="false" OnRowDataBound="OnRowDataBound">
    <Columns>
        <asp:BoundField DataField="acNo" HeaderText="acNo" />
        <asp:BoundField DataField="Name" HeaderText="Name" />
        <asp:TemplateField HeaderText="Image">
            <ItemTemplate>
                <asp:Image ID="Image1" runat="server" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
        <hr />
        <asp:GridView ID="gvOmnitbl" runat="server" AutoGenerateColumns="false" >
    <Columns>
        <asp:BoundField DataField="CusNo" HeaderText="CusNo" />
        <asp:BoundField DataField="NameTitle" HeaderText="NameTitle" />
       <asp:BoundField DataField="Name" HeaderText="Name" />
        <asp:BoundField DataField="AccountNumber" HeaderText="AccountNumber" />
        <asp:BoundField DataField="NameType" HeaderText="NameType" />
    </Columns>
</asp:GridView>
    </div>
    </form>
</body>--%>

<head>
    <title>View Signatures
    </title>
     <link rel="icon" href="Resources/bccb_logo.png"/>
    <style type="text/css">
        td {
            padding-top: .5em;
            padding-bottom: .5em;
        }

        .name_lbl{
            color:#052b39;
            font-family:'Times New Roman', Times, serif;
            font-size:15px;
        }
        .branch_lbl{
            color:#db0f13;
              font-family:'Times New Roman', Times, serif;
            font-size:15px;
        }
        .remain_lbl{
            color:#ef0f5a;
              font-family:'Times New Roman', Times, serif;
            font-size:15px;
        }
        .label {
            white-space: nowrap;
            text-align: right;
            font-family: verdana;
            font-size: 12px;
            font-weight: bold;
            color: Black;
        }

        .fields {
            font-family: verdana;
            font-size: 12px;
        }

        .label2 {
            white-space: nowrap;
            background-color: #db0f13;
            text-align: center;
            font-family: verdana;
            font-size: 13px;
            font-weight: bold;
            padding-top: 3px;
            padding-bottom: 3px;
        }

        .style8 {
            font-family: Verdana;
            color: black;
            font-weight: bold;
        }

        .DateStyle {
            background-color: #FFFFCC;
            border-color: #FFCC66;
            border-width: 1px;
            font-family: Verdana;
            font-size: 8pt;
            color: #663399;
        }

        .cal_Theme1 .ajax__calendar_container {
            background-color: #e2e2e2;
            border: solid 1px #cccccc;
        }

        .cal_Theme1 .ajax__calendar_header {
            background-color: #990000;
            font-weight: bold;
            font-size: 9pt;
            color: #FFFFCC;
        }

        .cal_Theme1 .ajax__calendar_title, .cal_Theme1 .ajax__calendar_next, .cal_Theme1 .ajax__calendar_prev {
            color: #004080;
            padding-top: 3px;
        }

        .cal_Theme1 .ajax__calendar_body {
            background-color: #e9e9e9;
            border: solid 1px #cccccc;
        }

        .cal_Theme1 .ajax__calendar_dayname {
            text-align: center;
            font-weight: bold;
            margin-bottom: 4px;
            margin-top: 2px;
        }

        .cal_Theme1 .ajax__calendar_day {
            text-align: center;
        }

        .cal_Theme1 .ajax__calendar_hover .ajax__calendar_day, .cal_Theme1 .ajax__calendar_hover .ajax__calendar_month, .cal_Theme1 .ajax__calendar_hover .ajax__calendar_year, .cal_Theme1 .ajax__calendar_active {
            color: #004080;
            font-weight: bold;
            background-color: #ffffff;
        }

        .cal_Theme1 .ajax__calendar_today {
            font-weight: bold;
        }

        .cal_Theme1 .ajax__calendar_other, .cal_Theme1 .ajax__calendar_hover .ajax__calendar_today, .cal_Theme1 .ajax__calendar_hover .ajax__calendar_title {
            color: #bbbbbb;
        }

        a {
            font-family: Georgia, "Times New Roman", Times, serif;
            font-size: 12px;
            cursor: auto;
        }

            a:link {
                color: #87B207;
            }

            a:visited {
                color: #87B207;
            }

            a:hover {
                color: #87B207;
            }

            a:active {
                color: #87B207;
                text-decoration: none;
            }
        /* This style use in NPR State section Budhasen[6/26/2014]*/ li {
            list-style-type: disc;
            color: red;
            margin-bottom: 6px;
        }

            li span {
                color: #545454;
                text-decoration: none;
                font-size: 14px;
                line-height: 20px;
            }

        span {
            color: #545454;
            font-size: 14px;
            font-family: Arial, Helvetica, sans-serif;
        }

        #NPRLink {
            cursor: pointer;
            font-weight: bold;
            text-decoration: underline;
            color: blue;
            font-size: 12px;
            font-family: Arial, Helvetica, sans-serif;
        }
        /* This style use in NPR State section*/
    </style>
    <style type="text/css">
        .processMessage {
            position: fixed;
            top: 39%;
            left: 41%;
            padding: 10px;
            width: 15%;
            z-index: 1001; /*background-color: #fff;*/
            border: outset 0px #6389ab;
        }

        .progressBackgroundFilter {
            position: fixed;
            top: 0px;
            bottom: 0px;
            left: 0px;
            right: 0px;
            overflow: hidden;
            padding: 0;
            margin: 0;
            background-color: Gray;
            filter: alpha(opacity=60);
            opacity: 0.60;
            z-index: 1000;
        }
    </style>

    <script>
	window.oncontextmenu = function () {
				return false;
			}
			$(document).keydown(function (event) {
				if (event.keyCode == 123) {
					return false;
				}
				else if ((event.ctrlKey && event.shiftKey && event.keyCode == 73) || (event.ctrlKey && event.shiftKey && event.keyCode == 74)) {
					return false;
				}
			});



		</script>
    

    <script src="../Scripts/jquery-ui.js" type="text/javascript"></script>
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/jquery.datepick.css" rel="stylesheet">
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
    <script src="../Scripts/jquery.plugin.min.js"></script>
    <script src="../Scripts/jquery.datepick.js"></script>

    <link rel="stylesheet" type="text/css" href="../Styles/default.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/component.css" />
    <script src="../Scripts/modernizr.custom.js"></script>

    <link rel="stylesheet" href="../Styles/sweetalert.css">
    <script src="../Scripts/sweetalert.min.js"></script>

    
    <script type="text/javascript" language="javascript">
        function RefreshPage1() {
            //  debugger;


            var drdBranch = document.getElementById("<%=drdBranch.ClientID %>");
        var selectedText = drdBranch.options[drdBranch.selectedIndex].innerHTML;
        var drdAcct_Type = document.getElementById("<%=drdAcct_Type.ClientID %>");
            var selectedText1 = drdAcct_Type.options[drdAcct_Type.selectedIndex].innerHTML;
            <%--var acctno = document.getElementById('<%=txtAcct_NO.ClientID%>').value;--%>
            var found = true;
            if (selectedText != '--Select Branch--') {
                if (selectedText1 == 'Select Account Type') {
                    //ShowPopup("Please Select Account Type!!!");
                    ShowPopup("Please Select Account Type!!!");
                    return false;
                }
                if (acctno == '') {
                    ShowPopup("Please Enter Acct No!!!");
                    return false;
                }
                
            }
            else {
                ShowPopup("Please Select Branch!!!");
                //sweetAlert("");
                return false;
            }
        }


    </script>
    

    


    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.dynDateTime.min.js" type="text/javascript"></script>
    <script src="../Scripts/calendar-en.min.js" type="text/javascript"></script>
    <link href="../Styles/calendar-blue.css" rel="stylesheet" type="text/css" />

    <script src="../Scripts/jquery-1.8.3.min.js"></script>
    <script src="../Scripts/jquery-ui.js"></script>
    <link href="../Styles/jquery-ui.css" rel="stylesheet" />

    <script type="text/javascript">
        function ShowPopup(message) {
            $(function () {
                $("#dialog").html(message);
                $("#dialog").dialog({
                    title: "Alert",
                    buttons: {
                        Close: function () {
                            $(this).dialog('close');
                        }
                    },
                    modal: true
                });
            });
        };
    </script>

    <div id="dialog" style="display: none">
    </div>


    <link href="../Styles/jquery-ui_8.css" rel="stylesheet" />


    

<div class="popup" data-popup="popup-1">
	<div class="popup-inner">
		<h2>Period:</h2>
		<p>Till 15-08-2018.</p>
        <h2>Redemption process:</h2>
		<p> 1. Sign in to your Amazon account through your new or registered credentials<br/> 
            2. Enter the unique discount code received to your Amazon Pay account<br/> 
            3. On successful validation of the discount code Rs.150 will be added to your Amazon Pay Account<br/> 
            4. Select the product of your choice and proceed to the checkout page to use the discount amount.<br/> </p>
        <h2>Terms and Conditions:</h2>
		<p>Offers valid on first time users on Amazon <br/> 
           Denomination of the voucher Rs.150<br/> 
		Voucher valid for 1 year once added on Amazon account.<br/> <a data-popup-close="popup-1" href="#">Close</a></p>
		
		<a class="popup-close" data-popup-close="popup-1" href="#">x</a>
	</div>
</div>

    <style type="text/css">

        /* Outer */
.popup {
	width:100%;
	height:100%;
	display:none;
	position:fixed;
	top:0px;
	left:0px;
	background:rgba(0,0,0,0.75);
}

/* Inner */
.popup-inner {
	max-width:700px;
	width:90%;
	padding:40px;
	position:absolute;
	top:50%;
	left:50%;
	-webkit-transform:translate(-50%, -50%);
	transform:translate(-50%, -50%);
	box-shadow:0px 2px 6px rgba(0,0,0,1);
	border-radius:3px;
	background:#fff;
}

/* Close Button */
.popup-close {
	width:30px;
	height:30px;
	padding-top:4px;
	display:inline-block;
	position:absolute;
	top:0px;
	right:0px;
	transition:ease 0.25s all;
	-webkit-transform:translate(50%, -50%);
	transform:translate(50%, -50%);
	border-radius:1000px;
	background:rgba(0,0,0,0.8);
	font-family:Arial, Sans-Serif;
	font-size:20px;
	text-align:center;
	line-height:100%;
	color:#fff;
}

.popup-close:hover {
	-webkit-transform:translate(50%, -50%) rotate(180deg);
	transform:translate(50%, -50%) rotate(180deg);
	background:rgba(0,0,0,1);
	text-decoration:none;
}

    </style>

    <script type="text/javascript">

        $(function () {
            //----- OPEN
            $('[data-popup-open]').on('click', function (e) {
                var targeted_popup_class = jQuery(this).attr('data-popup-open');
                $('[data-popup="' + targeted_popup_class + '"]').fadeIn(350);

                e.preventDefault();
            });

            //----- CLOSE
            $('[data-popup-close]').on('click', function (e) {
                var targeted_popup_class = jQuery(this).attr('data-popup-close');
                $('[data-popup="' + targeted_popup_class + '"]').fadeOut(350);

                e.preventDefault();
            });
        });

    </script>

    <script type="text/javascript">
        var sessionTimeout = "<%= Session.Timeout %>";
        function DisplaySessionTimeout()
        {
            sessionTimeout = sessionTimeout - 1;
            
            if (sessionTimeout >= 0)
                //call the function again after 1 minute delay
                window.setTimeout("DisplaySessionTimeout()", 60000);
            else
            {
                //show message box
                alert("You have been auto-logged Off after a minute of idle time . Re-Login !!");
                window.location = "../LogIn.aspx";
            }
        }
    </script>

    

</head>

<body style="background-color: #F6F6F2;">
    <form name="FillDetails" method="post" id="FillDetails" autocomplete="off" runat="server">

        <div runat="server" id="divBody" class="asp_div">

            <div style="font-size: 12px; font-family: Arial;">
                <table cellpadding="0" cellspacing="0" border="0" width="1024px" align="center" style="background-color: #FFFFFF;">
                    <tbody>
                        
                        <tr>
                            <td style="border-top-width: 1px; border-top-color: #db0f13; border-top-style: solid;border-bottom-width: 1px; border-bottom-color: #db0f13; border-bottom-style: solid; border-left-width: 1px; border-left-color: #db0f13; border-left-style: solid; border-right-width: 1px; border-right-color: #db0f13; border-right-style: solid;"
                                height="400px" valign="top">


                                <table width="100%">
                                    <tbody>
                                        <tr>
                                            <td align="center" style="font-size: 20px; color: #727272; font-weight: bold;">View Signatures Module
                                                
                                            </td>
                                             
                                                                        
                                                                         
                                                                    
                                        </tr>
                                        <tr>

                                            <td align="left">
                                                
                                            Employee Name :<label id="lbl_name" runat="server" class="name_lbl"> </label>

                                            </td>
                                        </tr>
                                          <tr>

                                            <td align="left">
                                                
                                                 Branch Name:<label id="lbl_branch" runat="server" class="branch_lbl" > </label>

                                            </td>
                                        </tr>
                                        <tr>

                                            <td align="left">
                                                
                                                 Remaining Branch Target:<label id="lbl_target" runat="server" class="remain_lbl"> </label>

                                            </td>
                                        </tr>
                                        
                                        <div runat="server" id="divAcct_Details">
                                            <tr>
                                                <td width="100%">
                                                    <fieldset style="border-color: #db0f13;">
                                                        <legend style="color: Black;" class="style8">Account Details</legend>
                                                        <table width="100%" border="0" cellspacing="2" cellpadding="2">
                                                            <tbody>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <div>

                                                                            <table width="100%">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td width="150px" style="color: Black;">Branch <span style="color: Red;">*</span>
                                                                                        </td>
                                                                                        <td>
                                                                                            <div class="select">
                                                                                                <asp:DropDownList ID="drdBranch" CssClass="drdShow" data-style="btn-primary" runat="server" Style="width: 200px;" TabIndex="1" AutoPostBack="true" OnSelectedIndexChanged="drdBranch_SelectedIndexChanged" Enabled="False"></asp:DropDownList>
                                                                                            </div>
                                                                                        </td>





                                                                                    </tr>
                                                                                    <tr id="trAccType" runat="server" visible="false">
                                                                                        <td width="150px" style="color: Black;">Account Type <span style="color: Red;">*</span>
                                                                                        </td>
                                                                                        <td>
                                                                                            <div class="select">
                                                                                                <asp:DropDownList ID="drdAcct_Type" CssClass="drdShow" runat="server" Style="width: 200px;" TabIndex="2" OnSelectedIndexChanged="drdAcct_Type_SelectedIndexChanged" AutoPostBack="true" >
                                                                                                    <%--<asp:ListItem Value="0" Selected="True">Select Account Type</asp:ListItem>--%>
                                                                                                    <asp:ListItem Text="Saving" Value="SB" Selected="True"></asp:ListItem>
                                                                                                    <asp:ListItem Text="Current" Value="CD"></asp:ListItem>
                                                                                                      <asp:ListItem Text="Cash Credit" Value="CC"></asp:ListItem>
                                                                                                      <asp:ListItem Text="Overdraft" Value="OD"></asp:ListItem>
                                                                                                     <asp:ListItem Text="Overdraft Cash Credit" Value="ODCC"></asp:ListItem>
                                                                                                     <asp:ListItem Text="Non Resident Indian" Value="NRI"></asp:ListItem>
                                                                                                     <asp:ListItem Text="SBK" Value="SBK"></asp:ListItem>
                                                                                                </asp:DropDownList>
                                                                                            </div>
                                                                                        </td>



                                                                                    </tr>
                                                                                    <tr id="trAccNo" runat="server" visible="false">
                                                                                        <td width="150px" style="color: Black;">Account No <span style="color: Red;">*</span>
                                                                                        </td>
                                                                                        <td>
                                                                                             <div class="select">
                                                                                                <asp:DropDownList ID="ddlAccountNo" runat="server" CssClass="drdShow" data-style="btn-primary" Style="width: 200px;" TabIndex="3"  AutoPostBack="true" OnSelectedIndexChanged="ddlAccountNo_SelectedIndexChanged" Enabled="False" >
                                                                                                   
                                                                                                </asp:DropDownList>
                                                                                            </div>
                                                                                        </td>
                                                                                        




                                                                                    </tr>




                                                                                </tbody>
                                                                            </table>

                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                
                                                               
                                                                


                                                            </tbody>
                                                        </table>
                                                    </fieldset>
                                                
                                                <fieldset style="border-color: #db0f13;" id="ViewFieldSet" runat="server" visible="false">
                                                        <legend style="color: Black;" class="style8">View</legend>
                                                        <table width="100%" border="0" cellspacing="2" cellpadding="2">
                                                            <tbody>

                                                                <tr><td style="color: Black;" class="style8"; align="center"> Details in Images Cropped</td>
                                                                  
                                                                </tr>
                                                                <tr><td>
                                                                 <asp:GridView ID="gvImages" runat="server" AutoGenerateColumns="false" OnRowDataBound="OnRowDataBound"
                                    CssClass="grid"  AllowPaging="True"  BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="5"  Width="100%"  PageSize="15">
    <Columns>
        <asp:BoundField DataField="branch" HeaderText="Branch" />      
         <asp:BoundField DataField="prdCd" HeaderText="Product" />
           <asp:BoundField DataField="acNo" HeaderText="Account No" />
          <asp:BoundField DataField="custNo" HeaderText="Customer No" />
         <asp:BoundField DataField="Longname" HeaderText="Customer Name" />
        <asp:TemplateField HeaderText="Image">
            <ItemTemplate>
                <asp:Image ID="Image1" runat="server" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
                                                                </td><%--</tr>
                                                                <tr>--%><%--<td style="color: Black;" class="style8"> Details in Omni </td>--%><%--</tr>
        <tr>--%>
                                                                    
                                                                    <%--<td>
                                 <asp:GridView ID="gvOmnitbl" runat="server" AutoGenerateColumns="false" 
                                    CssClass="grid"  AllowPaging="True"  BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="5"  Width="100%" PageSize="15">
    <Columns>
        <asp:BoundField DataField="CusNo" HeaderText="CusNo" />
        <asp:BoundField DataField="NameTitle" HeaderText="NameTitle" />
       <asp:BoundField DataField="Name" HeaderText="Name" />
        <asp:BoundField DataField="AccountNumber" HeaderText="AccountNumber" />
        <asp:BoundField DataField="NameType" HeaderText="NameType" />
    </Columns>
</asp:GridView>

</td>--%></tr>

                                                                     <tr align="center">
                                                                                        <td style="color: Black;align-content:center" colspan="2" align="center">
                                                                                            <div class="select">
                                                                                    <asp:RadioButtonList ID="rblVerify" AutoPostBack="true" runat="server" RepeatDirection = "Horizontal" TabIndex="4" 
                                                                                         OnSelectedIndexChanged="rblVerify_SelectedIndexChanged" >
                                                                                        <asp:ListItem Text="Accept" Value="YES" ></asp:ListItem>
                                                                                        
                                                                                        <asp:ListItem Text="Reject" Value="NO"></asp:ListItem>
    
                                                                                    </asp:RadioButtonList>   </div>
                                                                                        </td></tr><tr id="trrejreason" runat="server" visible="false">
                                                                                        <td style="color: Black;align-content:center" colspan="2" align="center">
                                                                                             <div class="select">
                                                                                                <asp:DropDownList ID="ddlrejReason" CssClass="drdShow" runat="server" Style="width: 200px;" TabIndex="5" AutoPostBack="true" OnSelectedIndexChanged="ddlrejReason_SelectedIndexChanged"  >
                                                                                                    <asp:ListItem Value="0" Text="--Select Reason--" Selected="True"></asp:ListItem>
                                                                                                    <asp:ListItem Text="Not Cropped Properly" Value="Not Cropped Properly"></asp:ListItem>
                                                                                                    <asp:ListItem Text="Customer Number Mismatch" Value="Customer Number Mismatch"></asp:ListItem>
                                                                                                    <asp:ListItem Text="Signature Sequence Mismatch" Value="Signature Sequence Mismatch"></asp:ListItem>
                                                                                                    <asp:ListItem Text="Other" Value="Other"></asp:ListItem>
                                                                                                </asp:DropDownList>
                                                                                            </div>
                                                                                            
                                                                                        </td>

                                                                                                  </tr>
                                                                <tr><td colspan="2" align="center"><asp:Panel ID="pnlTextBox" runat="server" Visible="false">
                                                                                                 Reason : - 
                                                                                                 <asp:TextBox ID="txtOtherReason" runat="server" MaxLength="20" />
                                                                                            </asp:Panel></td></tr>       
                                                                     <td id="tdSubmit" runat="server" align="center" colspan="2">
                                                                                <%--here--%>
                                                                          
                                                                          <asp:Button  ID="btnUpdate" runat="server" Text="Submit" 
                                                                    Style=" height: 35px; width: 104px; background-color: #db0f13; color: White; font-weight: bold; font-size: 17px;" 
                                                                              TabIndex="6" OnClick="btnUpdate_Click" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please wait...';" />
                                                                            </td>
                                                                        </tr>
                            
                                                                
                                                                                                </tbody>
                                                        </table>
                                                    </fieldset>
                                                
                                                </td>
                                            </tr>

                                        
                                        </div>

                                       


                     


                                    </tbody>
                                </table>
                            </td>
                        </tr>


                        


                    </tbody>
                </table>
            </div>


        </div>
    </form>
</body>

</html>
