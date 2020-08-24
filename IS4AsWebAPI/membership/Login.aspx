<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="membership.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv="Content-Script-Type" content="text/javascript" />
    <meta http-equiv="Content-Style-Type" content="text/css" />
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta name="viewport" content="width=device-width,initial-scale=1.0,minimum-scale=1.0,user-scalable=no" />
    <meta name="format-detection" content="telephone=no, email=no, address=no" />

    <title>Login</title>

    <link rel="stylesheet"  href="assets/bootstrap/css/bootstrap.min.css" />
    <link rel="stylesheet"  href="assets/css/basic.element.css" />
    <link rel="stylesheet"  href="assets/css/customize.css" />
    <script type="text/javascript" src="assets/js/jquery-2.2.4.min.js"></script>

</head>
<body>
  <form id="form1" runat="server" class="form-horizontal">

<section class="main-area mt-20">                         
     <div runat="server" id="loginArea" visible="true" style="width:90%;padding-left:2em;">

            <div class="field-descr" id="LoginGuidance" runat="server"></div>

             <div class="mt-20 errorMessage" runat="server" id="ErrorMessageArea" visible="false">
                <asp:Label runat="server" ID="ErrorMessage"></asp:Label>
            </div>

            <div class="row form-group mt-20" runat="server" id="formLoginID">
  
                <div class="col-lg-3 col-xs-12"><label class="control-label">Login ID</label></div>
                <asp:textbox id="txtLoginID" runat="server" class="form-control" MaxLength="500" tabIndex="1" placeholder="Login ID (Mail Address)"></asp:textbox>
                                 
            </div>

            <div class="row form-group mt-20" runat="server" id="formPassword">
                <div class="col-lg-3 col-xs-12"><label class="control-label">Password</label></div>

                <asp:textbox TextMode="password" class="form-control" id="txtPassword" maxlength="500" tabIndex="2" placeholder="Password" 
                    autocomplete="off" runat="server"/>
            </div>

                                   
            <div class="col-lg-6">                      
                <div class="field-descr maxw-280">
                    <div class="formList">
                    <asp:CheckBox id="chkRememberme" runat="server" Text="Remember Me?" />
                    </div>
                </div>                                     
            </div>  

            <div class="row form-group">
                <div class="col-lg-offset-4 col-lg-4 col-xs-offset-2 col-xs-8">
                    <asp:Button CssClass="btn btn-warning btn-block size-lg" id="BtnLogin" tabIndex="3" 
                        runat="server" onclick="BtnLogin_Click" Text="Login"/>
                </div>
            </div>
       
        </div><!--/.login-area -->
     
           
</section><!--/.main-area -->

    </form>
</body>
</html>
