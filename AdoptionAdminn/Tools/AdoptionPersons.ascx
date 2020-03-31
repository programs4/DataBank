<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdoptionPersons.ascx.cs" Inherits="AdoptionAdminn_Tools_AdoptionPersons" %>

<div class="panel panel-default">
    <div class="panel-heading">Məlumat bankı</div>
</div>
<br />
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="row">
            <div class="col-md-2">
            </div>
            <div class="col-md-2">
            </div>
        </div>

        <asp:Panel ID="PnlFilter" DefaultButton="BtnFilter" runat="server">
            <ul class="FilterSearch">
                <li>
                    <span>Şəxsi kod:</span>
                    <asp:TextBox ID="TxtID" CssClass="form-control" runat="server"></asp:TextBox>
                </li>
                <li>
                    <span>Soyadı, adı və atasının adı:</span>
                    <asp:TextBox ID="TxtFullname" CssClass="form-control" runat="server"></asp:TextBox>
                </li>

                <li>
                    <span>Saytda göstərilənlər:</span>
                    <asp:DropDownList ID="DListIsWebPreview" runat="server" CssClass="form-control">
                        <asp:ListItem Text="--" Value="-1"></asp:ListItem>
                        <asp:ListItem Text="Saytda göstərilənlər" Value="1"></asp:ListItem>
                    </asp:DropDownList>
                </li>
                <li>
                    <span>Bacı və qardaşı olanlar:</span>
                    <asp:DropDownList ID="DListIsBrotherSister" runat="server" CssClass="form-control">
                        <asp:ListItem Text="--" Value="-1"></asp:ListItem>
                        <asp:ListItem Text="Bacı və qardaşı olanlar" Value="1"></asp:ListItem>
                    </asp:DropDownList>
                </li>
                   <li>
                    <span>Status novu:</span>
                    <asp:DropDownList ID="DListStatusType" runat="server" CssClass="form-control" OnSelectedIndexChanged="DListStatusType_SelectedIndexChanged" AutoPostBack="True">
                        <asp:ListItem Text="Müraciətlər" Value="-1"></asp:ListItem>
                        <asp:ListItem Text="Arxivlənmiş" Value="90"></asp:ListItem>
                    </asp:DropDownList>
                </li>
                  <li>
                    <span>Status:</span>
                     <asp:DropDownList ID="DListAdoptionPersonStatus" CssClass="form-control" runat="server" DataTextField="Name" DataValueField="ID"></asp:DropDownList>
                  </li>
              
                <li class="NoStyle">
                    <asp:Button ID="BtnFilter" CssClass="btn btn-default" Width="100px" Height="35px" runat="server" Text="AXTAR" Font-Bold="False" OnClick="BtnFilter_Click" OnClientClick="this.style.display='none';" />
                </li>
            </ul>
        </asp:Panel>
        <br />

      <div style="float: left; padding: 10px 10px 10px 0px; margin-top: 15px;">
                    <asp:LinkButton ID="LnkAdd" runat="server" ToolTip="Yeni istifadəçi əlavə et" OnClick="LnkAdd_Click"><img src="/pics/new1.png" class="alignMiddle" /> YENİ ƏLAVƏ</asp:LinkButton>
                </div>
        <div style="padding: 10px 10px 10px 0px; margin-top: 15px;" class="text-right">
            <asp:Label ID="LblCount" runat="server"></asp:Label>
        </div>
        <asp:GridView ID="GrdAdoptionPersonsList" runat="server" AutoGenerateColumns="False" BorderColor="#CDCDCD" BorderWidth="0px" CellPadding="4" ForeColor="#051615" Width="100%" CssClass="boxShadow" Font-Bold="True">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="Şəxsi kod">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>

                <asp:BoundField DataField="Fullname" HeaderText="Soyadı, adı və atasının adı">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left"  />
                </asp:BoundField>

                <asp:BoundField DataField="PIN" HeaderText="FİN">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>

                <asp:TemplateField HeaderText="Cinsi">
                    <ItemTemplate>
                        <img src='/Pics/Gender_<%#Eval("Gender")._ToString()=="True"?"1":"0" %>.png' title='<%#Eval("Gender")._ToString()=="True"?"Kişi":"Qadın" %>' />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:TemplateField>

                <asp:BoundField DataField="Address" HeaderText="Ünvan">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="150px" />
                </asp:BoundField>

                <asp:TemplateField HeaderText="<img src='/Pics/web.png' title='Sayt'">
                    <ItemTemplate>
                         <img style="width: 24px;" src='/Pics/check_<%#Eval("IsWebPreview")._ToString()=="True"?"1":"0" %>.png' title='' />                    
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:TemplateField>


                <asp:TemplateField HeaderText="<img src='/Pics/BrotherSister_white.png' title='Bacı və qardaş'">
                    <ItemTemplate>
                          <img style="width: 24px;" src='/Pics/check_<%#Eval("IsBrotherSister")._ToString()=="True"?"1":"0" %>.png' title='' />                  
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Əlavə olunma tarixi">
                    <ItemTemplate>
                        <%#Convert.ToDateTime(Eval("Add_Dt")).ToString("dd.MM.yyyy") %>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="140px" />
                </asp:TemplateField>

                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="LnkEdit" CommandArgument='<%#Eval("ID") %>' runat="server" OnClick="LnkEdit_Click"><span class="glyphicon glyphicon-pencil" aria-hidden="true"></span>&nbsp;Düzəlt</asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Width="100px" Font-Size="11pt" />
                </asp:TemplateField>

                  <asp:TemplateField HeaderText="<img src='/Pics/BrotherSister_white.png' title='Bacı və qardaş əlavə et'">
                    <ItemTemplate>
                        <asp:LinkButton ID="LnkAddSisterBrother" CommandArgument='<%#Eval("ID") %>' CommandName="IsBrotherSister" OnClick="LnkAddSisterBrother_Click" runat="server"><img src="/Pics/add-circle-black.png" /></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Width="70px" Font-Size="11pt" />
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


        <asp:Panel ID="PnlPager" CssClass="pager_top" runat="server">
            <ul class="pagination bootpag"></ul>
        </asp:Panel>

        <asp:HiddenField ID="HdnTotalCount" ClientIDMode="Static" runat="server" />
        <asp:HiddenField ID="HdnPageNumber" ClientIDMode="Static" Value="1" runat="server" />

    </ContentTemplate>
</asp:UpdatePanel>


<script type="text/javascript">

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
            window.location.href = '/adoptionadminn/tools/?p=adoptionpersons&pn=' + num;
        }).find('.pagination');
    }
    $(document).ready(function () {
        GetPagination($('#HdnTotalCount').val(), $('#HdnPageNumber').val());
    });
</script>
