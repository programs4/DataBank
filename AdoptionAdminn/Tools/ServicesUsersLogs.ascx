<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ServicesUsersLogs.ascx.cs" Inherits="AdoptionAdminn_Tools_ServicesUsersLogs" %>
<div class="panel panel-default">
    <div class="panel-heading">
        <asp:Literal ID="LtrTitle" runat="server">Xidmət istifadəçilərinin izlənməsi</asp:Literal>
    </div>
</div>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <ul class="FilterSearch">
            <li>FIN:
                            <br />
                <asp:TextBox ID="TxtPin" CssClass="form-control" runat="server"></asp:TextBox>
            </li>

            <li>İcra Hakimiyyəti:
                            <br />
                <asp:DropDownList ID="DListOrganizations" CssClass="form-control" runat="server" DataTextField="Name" DataValueField="ID" Width="250px"></asp:DropDownList></li>

            <li>Uçot nömrəsi:
                            <br />
                <asp:TextBox ID="TxtRegisterNo" CssClass="form-control" runat="server"></asp:TextBox>
            </li>
            <li>Kontent:
                            <br />
                <asp:TextBox ID="TxtContent" CssClass="form-control" runat="server"></asp:TextBox>
            </li>

            <li class="NoStyle">
                <asp:Button ID="BtnSearch" runat="server" CssClass="btn btn-default" Width="100px" Height="35px" Text="AXTAR" Font-Bold="False" OnClick="BtnSearch_Click" OnClientClick="this.style.display='none';" />
            </li>
        </ul>
        <div style="float: right; padding: 5px;">
            <asp:Label ID="LblCount" runat="server" Font-Bold="False" Text="--"></asp:Label>
        </div>
        <asp:GridView ID="GrdUserLogs" runat="server" AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderWidth="0px" CellPadding="4" ForeColor="#051615" Width="100%" CssClass="boxShadow" Font-Bold="True" Style="margin-top: 15px">
            <Columns>
                <asp:TemplateField HeaderText="S/s">
                    <ItemTemplate>
                        <%# Eval("RowIndex") %>
                    </ItemTemplate>
                    <ItemStyle Width="50px" />
                </asp:TemplateField>

                <asp:BoundField DataField="PIN" HeaderText="FİN">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                </asp:BoundField>

                <asp:BoundField DataField="AutentType" HeaderText="Giriş növü">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" Width="120px" />
                </asp:BoundField>

                <asp:BoundField DataField="AdoptionOrganizations" HeaderText="İcra Hakimiyyəti">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>

                <asp:BoundField DataField="RegisterNo" HeaderText="Uçot nömrəsi">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>

                <asp:TemplateField HeaderText="Uçot tarixi">
                    <ItemTemplate>
                        <%# Convert.ToDateTime(Eval("RegisterDate")).ToString("dd.MM.yyyy") %>
                    </ItemTemplate>
                    <ItemStyle Width="110px" />
                </asp:TemplateField>

                <asp:BoundField DataField="SearchParams" HeaderText="Əməliyyatlar" HtmlEncode="False">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>

                <asp:BoundField DataField="Add_Dt" HeaderText="Tarix">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="140px" />
                </asp:BoundField>

                <asp:TemplateField HeaderText="IP">
                    <ItemTemplate>
                        <%# Eval ("Add_Ip").IntegerToIP() %>
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
            <HeaderStyle Font-Bold="True" BackColor="#364150" ForeColor="White" Height="40px" />
            <PagerSettings PageButtonCount="20" />
            <PagerStyle BackColor="White" CssClass="Gridpager" ForeColor="White" HorizontalAlign="Right" />
            <RowStyle CssClass="hoverLink" Height="45px" HorizontalAlign="Center" Font-Bold="False" />
            <SelectedRowStyle BackColor="#99FF99" Font-Bold="True" ForeColor="#333333" />
        </asp:GridView>
        <asp:Panel ID="PnlPager" CssClass="pager_top row pagination-row" Style="text-align: center;" runat="server">
            <ul class="pagination bootpag"></ul>
        </asp:Panel>

        <asp:HiddenField ID="HdnTotalCount" ClientIDMode="Static" runat="server" />
        <asp:HiddenField ID="HdnPageNumber" ClientIDMode="Static" Value="1" runat="server" />
    </ContentTemplate>
</asp:UpdatePanel>

<script>
    function GetPagination(t, p) {
        $('.pager_top').bootpag({
            total: t,
            page: p,
            maxVisible: 15,
            leaps: true,
            firstLastUse: true,
            first: '<span aria-hidden="true">&larr;</span>',
            last: '<span aria-hidden="true">&rarr;</span>',
            wrapClass: 'pagination',
            activeClass: 'active',
            disabledClass: 'disabled',
            nextClass: 'next',
            prevClass: 'prev',
            lastClass: 'last',
            firstClass: 'first',

        }).on("page", function (event, num) {
            window.location.href = '/adoptionadminn/tools/?p=servicesuserslogs&pn=' + num;
        }).find('.pagination');
    }

    $(document).ready(function () {
        GetPagination($('#HdnTotalCount').val(), $('#HdnPageNumber').val());
        $(".progress-inner").each(function () {
            $(this).animate({ width: $(this).attr('data-percent') + '%' }, 'slow');
        });
    })
</script>
