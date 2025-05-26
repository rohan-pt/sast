<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TermDepositeInt.aspx.cs" Inherits="BCCBExamPortal.TermDepositeInt" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <link rel="icon" href="Resources/bccb_logo.png"/>
     <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
     <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
   <link rel="preconnect" href="https://fonts.googleapis.com"/>
    <link href="Resources/CSS/BranchOperations.css" rel="stylesheet" />
    <script src="Resources/Script/TermDep.js"></script>
    <script src="Resources/Script/General.js"></script>
    <title>TD Interest Certificate</title>
</head>
<body>
    <form id="form1" runat="server">    
     
        <div class="Container_Div">         

            <div class="Content_Div">
                 <div class="heading_report"> Term Deposite Interest Certificate</div>
                <div class="data_input">
                    <input type="text" runat="server" id="txt_account_number" placeholder="Customer Number" class="txt_acc" />

                </div>               
                  <div class="data_input">

                      <button id="btn_fetch_data" runat="server" class="btn_data" onclick="validatefield();" onserverclick="fetch_info"  >OK</button>

                  </div>
               
               
                  <div class="data_input">
                   <div class="display_info">Customer Name : </div> <div class="display_info2" id="ft_acc_name" runat="server">-</div>
                      </div>
                  <div class="data_input">
                   <div class="display_info" style="height:100px;line-height:100px;vertical-align:central;">Customer Address : </div> <div class="display_info2" style="height:100px;" id="ft_cust_addr" runat="server">
                      <%-- <label runat="server" id="lbl_add">-</label>--%>
                       <textarea id="txt_ar_add" runat="server" disabled="disabled" class="text_asr "></textarea>
                             -                                          </div>
                      </div>
              
              
                 
                 

                  <div class="data_input">
                      <button id="Print_cert" runat="server" class="btn_data" onserverclick="download_cert">Download Certificate</button>
                  </div>
            </div>

        </div>

        
    </form>
</body>
</html>
