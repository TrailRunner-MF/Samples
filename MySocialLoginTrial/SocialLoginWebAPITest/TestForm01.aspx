<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestForm01.aspx.cs" Inherits="SocialLoginWebAPITest.TestForm01" Theme="FreeTestPanelEarth" StyleSheetTheme="FreeTestPanelEarth" %><%@ Register assembly="EmotionSoft.ewd2.SimpleUT.WebControlSet" namespace="EmotionSoft.ewd2.SimpleUT.WebControlSet" tagprefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
        <cc1:FreeTestPanel ID="FreeTestPanel1"
            runat="server" 
            MethodSelectorStyle="DroptDownList" 
            OnAddTestClassController="FreeTestPanel1_AddTestClassController" 
            TargetMethodRepeatColumns="2" 
            TargetTestPatternRepeatColumns="2" TestFormTitle="Test form for External Logins" >
        </cc1:FreeTestPanel>
    </form>
</body>
</html>
