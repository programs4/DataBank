<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Main_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMasterHead" runat="Server">

    <script>
        $(document).ready(function () {

            var objs = [];
            var objsN = [];
            var objsF = [];
            step = 1000;

            $('.count').each(function () {
                objs.push($(this));
                objsN.push(parseInt($(this).html()));
                objsF.push(parseInt($(this).html()) % step);

            });

            $('.count').html("0");

            i = 0;
            j = 0;

            var CountTimer = setInterval(function () {
                i = i + step;
                k = 0;
                $('.count').each(function () {

                    if (i <= objsN[j]) { objs[j].html(i); } else {
                        k++;
                        if (i + objsF[j] - step == objsN[j]) objs[j].html(objsN[j]);
                    }

                    if ($('.count').size() == j + 1) j = 0;
                    else
                        j++;
                });

                if (k == $('.count').size()) clearInterval(CountTimer);

            }, 1);

        })

    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMasterBody" runat="Server">
    <div style="width: 97%; margin-left: 20px; margin-bottom: 40px; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #CCCCCC;">
        <asp:Label ID="Label3" Font-Size="24pt" runat="server" Text="Statistik məlumatlar" Font-Names="Calibri" ForeColor="Gray"></asp:Label><br />
        <asp:Label ID="Label4" Font-Size="10pt" runat="server" Text="Məlumat bankı üzrə ümumi statistik məlumatlar" ForeColor="Gray"></asp:Label>
    </div>
    <div>
        <div class="statistics" style="background-image: url('/pics/report/report1.png');">
            <asp:Label ID="LblCount1" CssClass="info count" runat="server" Text="1508298"></asp:Label>
            <asp:Label ID="Label6" CssClass="info" runat="server" Text="Məlumat Bankına"></asp:Label>
            <asp:Label ID="Label5" CssClass="info" runat="server" Text="olan ümumi uşaq sayı"></asp:Label>
        </div>

        <div class="statistics" style="background-image: url('/pics/report/report2.png');">
            <asp:Label ID="LblCount2" CssClass="info count" runat="server" Text="3245"></asp:Label>
            <asp:Label ID="Label8" CssClass="info" runat="server" Text="Sistemdən istifadə edən"></asp:Label>
            <asp:Label ID="Label9" CssClass="info" runat="server" Text="istifadəçi sayı"></asp:Label>
        </div>
        <div class="statistics" style="background-image: url('/pics/report/report3.png');">
            <asp:Label ID="LblCount3" CssClass="info count" runat="server" Text="523"></asp:Label>
            <asp:Label ID="Label10" CssClass="info" runat="server" Text="Bu ay üçün"></asp:Label>
            <asp:Label ID="Label11" CssClass="info" runat="server" Text="sistemdən istifadə sayı"></asp:Label>
        </div>
        <div class="statistics" style="background-image: url('/pics/report/report4.png');">
            <asp:Label ID="LblCount4" CssClass="info count" runat="server" Text="25"></asp:Label>
            <asp:Label ID="Label12" CssClass="info" runat="server" Text="Son 24 saat ərzində"></asp:Label>
            <asp:Label ID="Label13" CssClass="info" runat="server" Text="sistemdən istifadə sayı"></asp:Label>
        </div>
    </div>

    <div style="position: relative; margin-top: 220px; width: 97%; margin-left: 20px;">
        <asp:Label ID="Label1" Font-Size="24pt" runat="server" Text="İstifadəçi tarixcəsi" Font-Names="Calibri" ForeColor="Gray"></asp:Label><br />
        <asp:Label ID="Label2" Font-Size="10pt" runat="server" Text="Mövcud qurum üzrə istifadəçi tarixcəsi" ForeColor="Gray"></asp:Label>

        <asp:GridView ID="GrdHistory" runat="server" AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderWidth="0px" CellPadding="4" ForeColor="#051615" Width="100%" Font-Bold="True">
            <Columns>
                <asp:BoundField DataField="LogText" ShowHeader="false">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Date" HeaderText="Tarix">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="140px" />
                </asp:BoundField>

                <asp:TemplateField HeaderText="IP">
                        <ItemTemplate>
                            <%# Eval("IP").IntegerToIP() %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="140px" />
                    </asp:TemplateField>
            </Columns>
            <EditRowStyle BackColor="#7C6F57" />
            <EmptyDataTemplate>
                <div class="textBox" style="margin-top: 10px; margin-bottom: 10px; border-width: 0px">
                    Məlumat tapılmadı.
                </div>
            </EmptyDataTemplate>
            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <HeaderStyle Font-Bold="True" BackColor="White" ForeColor="White" Height="40px" />
            <PagerSettings PageButtonCount="20" />
            <PagerStyle BackColor="White" CssClass="Gridpager" ForeColor="White" HorizontalAlign="Right" />
            <RowStyle Height="45px" HorizontalAlign="Center" Font-Bold="False" />
            <SelectedRowStyle BackColor="#99FF99" Font-Bold="True" ForeColor="#333333" />
        </asp:GridView>
    </div>
</asp:Content>

