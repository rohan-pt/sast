<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SVG.aspx.cs" Inherits="BCCBExamPortal.SVG" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        .small{
            font: 1em sans-serif;
            stroke:#808080;
         /*   color:#b6ff00*/
        }
    .new_path {
    stroke-width: 2;
    stroke-dasharray: 1000;
    stroke-dashoffset: 1000;
    animation: dash 20s linear forwards;
}

@keyframes dash {
    to {
        stroke-dashoffset: 0;
    }
}
    </style>
</head>
<body>
    <form id="form1" runat="server">
       <%-- <svg style="display:none" version="1.1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink">
  <symbol id="umbrella" viewBox="0 0 596 597">
    <title>Umbrella</title>
    <desc>Umbrella icon</desc>--%>
   
     <%--   <path class="shaft" d="M260.4,335.7 L260.4,478 C260.4,543.1 313.4,596.1 378.5,596.1 C443.6,596.1 496.6,543.1 496.6,478 C496.6,457.5 479.9,440.8 459.4,440.8 C438.9,440.8 422.2,457.5 422.2,478 C422.2,502.2 402.7,521.7 378.5,521.7 C354.3,521.7 334.8,502.2 334.8,478 L334.8,335.7 L260.4,335.7 L260.4,335.7 Z"></path>
<path class="fabric" d="M558,335.7 C578.5,335.7 595.2,319 595.2,298.5 L595.2,294.8 C593.4,132 460.4,0.9 297.6,0.9 L297.6,0.9 C133.9,0.9 0,134.8 0,298.5 C0,319 16.7,335.7 37.2,335.7 L558,335.7 L558,335.7 Z M77.2,261.3 C94.9,156.2 187,75.3 297.6,75.3 C408.2,75.3 500.4,156.2 518,261.3 L77.2,261.3 L77.2,261.3 Z"></path>
 --%>
<%--      </symbol>
</svg>--%>
       
  <svg width="50" height="50" viewBox="0 0 50 50 ">   
      <rect x="0" y="0" height="50" width="50" stroke="#808080" fill="#b6ff00" rx="5"></rect>
      <text x="12" y="25" class="small">CR</text>
      <line x1="10" y1="33" x2="38" y2="33" stroke="#808080"></line>
      <line x1="10" y1="39" x2="38" y2="39" stroke="#808080"></line>
       <line x1="10" y1="45" x2="38" y2="45" stroke="#808080"></line>
  </svg> 
    </form>
</body>
</html>
