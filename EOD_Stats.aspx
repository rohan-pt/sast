<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EOD_Stats.aspx.cs" Inherits="BCCBExamPortal.EOD_Stats" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
     <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <link href="Resources/CSS/EOD_Stat.css" rel="stylesheet" />
    <script src="Resources/Script/EOD_Stats.js"></script>
    <title>EOD statistics</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="sessionlbl" runat="server" Visible="false" Text=""></asp:Label>
           <asp:Label ID="constring" runat="server" Visible="false" Text=""></asp:Label>
          <asp:Label ID="strString" runat="server" Visible="false" Text=""></asp:Label>
         <div class="title1">
             <div class="title2">EOD Statistics </div>
             <div class="drop_down_div">
                 <asp:DropDownList ID="ddl_env" runat="server" CssClass="drop_down" AutoPostBack="true" OnSelectedIndexChanged="cmbActivity_SelectedIndexChanged">

                 </asp:DropDownList>
             </div>
             <div class="drop_down_div"><asp:TextBox ID="EOD_date" runat="server" placeholder="mm/dd/yyyy" Textmode="Date" ReadOnly ="false" AutoPostBack="true" CssClass="calender_cs" OnTextChanged="cmbActivity_SelectedIndexChanged"></asp:TextBox></div>
         </div>
         <div class="section00" id="Section1">

        </div>
        <div class="section0">
            
            <div class="Section1">
                <div class="Section1" style="width:100%;min-height:100px;" >
                    <table class="classic_table">
                        <tr>
                            <td class="taskmst">Taskmaster</td>
                        </tr>
                        <tr>
                            <td class="taskmstname" id="user_log" runat="server">
                           <%-- Abhijeet Kashinath Gharat - 08560<br/>
                            Abhijeet Kashinath Gharat - 08560--%></td>
                        </tr>
                    </table>
                </div>
             <div class="Section1"  style="width:100%;" id="full_logs" runat="server">
         

                 
             </div>
 <div class="Section1" style="width:100%;" id="section_88" runat="server">
               
            </div>

                </div>
            
              <div class="Section1"   id="Div3" runat="server">
                    <div class="Section1" style="width:100%;" id="section_66" runat="server">
               
            </div>
                  
                  <div class="Section1" style="width:100%;" id="section_77" runat="server">
               
            </div>
                   <div class="Section1" style="width:100%;" id="section_55" runat="server">
               
            </div>
                   <div class="Section1" style="width:100%;" id="section_44" runat="server">
               
            </div>
                 
                   </div>
        
        </div>
               
    </form>
</body>
</html>
