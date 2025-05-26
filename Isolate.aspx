
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Isolate.aspx.cs" Inherits="BCCBExamPortal.Isolate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
  shape-outside:circle();
}   
#circle-shape-example1 .curve { 
  width: 33%; height: auto;
  min-width: 150px;
  float: right;
  margin-left:2rem; 
  border-radius: 50%;
  -webkit-shape-outside:circle();
  shape-outside:circle();
}     
    </style>
   
</head>
<body>
    <form id="form1" runat="server">


        <div>
<div id="circle-shape-example">
  <img src="/Resources/bg10.jpg" alt="A photograph of sliced kiwifruit on a while plate" class="curve"><h1>KiwiFruit</h1>

  <p>This is kiwifruit: originally called “yang tao”, “melonette” or Chinese gooseberry. Cultivated in its fuzzy variety from Chinese imports, the fruit proved popular with American military servicemen stationed in New Zealand during World War II, with commercial export to the United States starting after the end of the war. In California, the fruit was rebranded as “kiwifruit” due to its resemblance to New Zealand’s national bird. However, it is not a “kiwi”, which is also the demonym for native New Zealanders. Saying “I’m going to eat a kiwi” implies that you are either a cannibal or planning to dine on an endangered flightless bird.
      Better kiw kiw se gooseberry. Cultivated in its fuzzy variety from Chinese imports, the fruit proved popular with Ameri se gooseberry. Cultivated in its fuzzy variety from Chinese imports, the fruit proved popular with Ameri se gooseberry. Cultivated in its fuzzy variety from Chinese imports, the fruit proved popular with Ameri se gooseberry. Cultivated in its fuzzy variety from Chinese imports, the fruit proved popular with Ameri</p>
</div>
            <div id="circle-shape-example1">
  <img src="/Resources/bg10.jpg" alt="A photograph of sliced kiwifruit on a while plate" class="curve"><h1>KiwiFruit</h1>

  <p>This is kiwifruit: originally called “yang tao”, “melonette” or Chinese gooseberry. Cultivated in its fuzzy variety from Chinese imports, the fruit proved popular with American military servicemen stationed in New Zealand during World War II, with commercial export to the United States starting after the end of the war. In California, the fruit was rebranded as “kiwifruit” due to its resemblance to New Zealand’s national bird. However, it is not a “kiwi”, which is also the demonym for native New Zealanders. Saying “I’m going to eat a kiwi” implies that you are either a cannibal or planning to dine on an endangered flightless bird.
      Better kiw kiw se gooseberry. Cultivated in its fuzzy variety from Chinese imports, the fruit proved popular with Ameri se gooseberry. Cultivated in its fuzzy variety from Chinese imports, the fruit proved popular with Ameri se gooseberry. Cultivated in its fuzzy variety from Chinese imports, the fruit proved popular with Ameri se gooseberry. Cultivated in its fuzzy variety from Chinese imports, the fruit proved popular with Ameri</p>

</div>

        </div>



    </form>
</body>
</html>
