<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Login_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Elektron Məlumat Bankı</title>
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=yes">
    <link rel="stylesheet" href="/Login/Css/Style.css">
    <script src="/Js/jquery.js"></script>
    <script src="/Js/AlertPopup.js"></script>
    <style type="text/css">
        .dlistType {
            background-position: 10px -53px !important;
            -webkit-border-radius: 3px;
            -moz-border-radius: 3px;
            -ms-border-radius: 3px;
            -o-border-radius: 3px;
            border-radius: 3px;
            -webkit-box-shadow: 0 1px 0 #fff, 0 -2px 5px rgba(0,0,0,0.08) inset;
            -moz-box-shadow: 0 1px 0 #fff, 0 -2px 5px rgba(0,0,0,0.08) inset;
            -ms-box-shadow: 0 1px 0 #fff, 0 -2px 5px rgba(0,0,0,0.08) inset;
            -o-box-shadow: 0 1px 0 #fff, 0 -2px 5px rgba(0,0,0,0.08) inset;
            box-shadow: 0 1px 0 #fff, 0 -2px 5px rgba(0,0,0,0.08) inset;
            -webkit-transition: all 0.5s ease;
            -moz-transition: all 0.5s ease;
            -ms-transition: all 0.5s ease;
            -o-transition: all 0.5s ease;
            transition: all 0.5s ease;
            border: 1px solid #c8c8c8;
            color: #777;
            font: 13px Helvetica, Arial, sans-serif;
            margin: 0 0 10px;
            padding: 15px 10px 15px 40px;
            width: 95%;
        }
    </style>
</head>
<body>
    <div id="alertMessage" class="MessageAlert" style="display: none;">
        <img id="alertImg" style="margin-bottom: -10px; height: 30px;">
        <span id="alertText">Məlumatlar yeniləndi</span>
    </div>
    <div class="container">
        <section id="content">

            <form id="AspnetForm" runat="server">
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>

                        <h1>İstifadəçi girişi</h1>

                        <div>
                            <asp:TextBox ID="TxtUsername" CssClass="username" runat="server" placeholder="İstifadəçi adı" AutoCompleteType="Disabled"></asp:TextBox>
                        </div>

                        <div>
                            <asp:TextBox ID="TxtPassword" CssClass="password" runat="server" placeholder="Şifrə" TextMode="Password" AutoCompleteType="Disabled"></asp:TextBox>
                        </div>
                        <div>
                            <asp:DropDownList ID="DlistType" runat="server" CssClass="dlistType">
                                <asp:ListItem Value="1">Uşaq məlumat bankı</asp:ListItem>
                                <asp:ListItem Value="2">Övladlığa götürmə</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div>
                            <asp:Button ID="BtnLogin" runat="server" Text="Giriş" OnClick="BtnLogin_Click"></asp:Button>
                        </div>

                        <div class="button" style="font-size: 8pt; margin-top: 85px; width: 400px; margin-left: -20px;">
                            © 2015 BÜTÜN HÜQUQLARI QORUNUR 
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </form>

        </section>
    </div>
</body>
</html>
