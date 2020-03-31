<%@ Control Language="C#" AutoEventWireup="true" CodeFile="300_DunyayaKorpe.ascx.cs" Inherits="Detail_Contents_300_DunyayaKorpe" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:Panel ID="PnlAdd" runat="server" Style="padding: 15px; padding-left: 0px;" HorizontalAlign="Left" Width="100%">
            <asp:LinkButton ID="LnkAdd" runat="server" OnClick="LnkAdd_Click"><img class="alignMiddle" src="/pics/new.png" /> YENİ ƏLAVƏ</asp:LinkButton>
        </asp:Panel>
        <asp:Panel ID="PnlEdit" Visible="false" runat="server" CssClass="ContentRoot">
            <div class="PnlItems">
                Doğulduğu ölkə<br />
                <asp:DropDownList ID="DlistDogumCountry" runat="server" AutoPostBack="True" CssClass="form-control .col-md-6" DataTextField="Name" DataValueField="ID" OnSelectedIndexChanged="DlistCountry_SelectedIndexChanged" Width="100%">
                </asp:DropDownList>
                <br />
                <br />
                Doğulduğu şəhər/rayon<br />
                <asp:DropDownList ID="DlistDogumCity" runat="server" CssClass="form-control .col-md-6" DataTextField="Name" DataValueField="ID" Width="100%">
                </asp:DropDownList>
                <br />
                <br />
                Doğulduğu ünvan<br />
                <asp:TextBox ID="TxtDogumUnvan" runat="server" CssClass="form-control .col-md-6" Height="70px" TextMode="MultiLine" Width="100%"></asp:TextBox>
                <br />
                <br />
                Uşağın doğum tarixi<br />
                <asp:DropDownList ID="DListDogumD" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="100px">
                </asp:DropDownList>
                <asp:DropDownList ID="DListDogumM" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="250px">
                </asp:DropDownList>
                <asp:DropDownList ID="DListDogumY" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="150px">
                </asp:DropDownList>
                <br />
                <br />
                Sağlamlıq vəziyyəti<br />
                <asp:TextBox ID="TxtSaglam" runat="server" CssClass="form-control .col-md-6" Width="100%"></asp:TextBox>
                <br />
                <br />
                Atasının adı<br />
                <asp:TextBox ID="TxtAtaAd" runat="server" CssClass="form-control .col-md-6" Width="100%"></asp:TextBox>
                <br />
                <br />
                Atasının soyadı<br />
                <asp:TextBox ID="TxtAtaSoyad" runat="server" CssClass="form-control .col-md-6" Width="100%"></asp:TextBox>
                <br />
                <br />
                Atasının doğum tarixi<br />
                <asp:DropDownList ID="DListAtaTevelludD" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="100px">
                </asp:DropDownList>
                <asp:DropDownList ID="DListAtaTevelludM" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="250px">
                </asp:DropDownList>
                <asp:DropDownList ID="DListAtaTevelludY" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="150px">
                </asp:DropDownList>
                <br />
                <br />
                Atasının yaşadığı ölkə<br />
                <asp:DropDownList ID="DListAtaCountry" runat="server" AutoPostBack="True" CssClass="form-control .col-md-6" DataTextField="Name" DataValueField="ID" OnSelectedIndexChanged="DListAtaCountry_SelectedIndexChanged" Width="100%">
                </asp:DropDownList>
                <br />
                <br />
                Atasının yaşadığı şəhər/rayon<br />
                <asp:DropDownList ID="DListAtaCity" runat="server" CssClass="form-control .col-md-6" DataTextField="Name" DataValueField="ID" Width="100%">
                </asp:DropDownList>
                <br />
                <br />
                Atasının yaşadığı ünvan<br />
                <asp:TextBox ID="TxtAtaUnvan" runat="server" CssClass="form-control .col-md-6" Height="70px" TextMode="MultiLine" Width="100%"></asp:TextBox>
                <br />
                <br />
                Qeyd<br />
                <asp:TextBox ID="TxtDescription" runat="server" CssClass="form-control .col-md-6" Height="70px" TextMode="MultiLine" Width="100%"></asp:TextBox>
                <br />
                <br />
                <div class="SuccesButton">
                    <asp:LinkButton ID="LnkSuccess" runat="server" OnClientClick="SubmitLoading(this);" OnClick="LnkSuccess_Click"><img border="0" src="/pics/SaveButton.png" /></asp:LinkButton>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:LinkButton ID="LnkCancel" runat="server" OnClick="LnkCancel_Click" OnClientClick="SubmitLoading(this);"><img border="0" src="/pics/CancelButton.png" /></asp:LinkButton>
                </div>
            </div>
            <br />
            <br />
        </asp:Panel>

        <asp:GridView ID="GrdList" runat="server" BorderColor="#EAEAEA" CellPadding="4" ForeColor="#051615" Width="100%" Font-Bold="True" DataKeyNames="ID" AutoGenerateColumns="False" OnRowDeleting="GrdList_RowDeleting" OnSelectedIndexChanged="GrdList_SelectedIndexChanged">
            <Columns>

                <asp:BoundField DataField="Ss" HeaderText="№">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                </asp:BoundField>

                <asp:BoundField DataField="Unvan" HeaderText="Uşağın doğulduğu ünvan" HtmlEncode="false" />

                <asp:BoundField DataField="SaglamliqVeziyyeti" HeaderText="Sağlamlıq Vəziyyəti" />

                <asp:BoundField DataField="DogumTarixi" HeaderText="Dogum Tarixi">
                    <HeaderStyle HorizontalAlign="Center" Width="140px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Atasının soyadı və adı">
                    <ItemTemplate>
                        <%#Eval("AtaSoyad")._ToString()+" "+Eval("AtaAd")._ToString() %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="AtaUnvan" HeaderText="Atasının yaşadığı ünvan" HtmlEncode="false" />

                <asp:BoundField DataField="AtaTevellud" HeaderText="Atasının doğum tarixi">
                    <HeaderStyle HorizontalAlign="Center" Width="140px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>

                <asp:BoundField DataField="Description" HeaderText="Qeyd" />
                <asp:CommandField SelectText="&lt;span class=&quot;glyphicon glyphicon-pencil&quot; aria-hidden=&quot;true&quot;&gt;&lt;/span&gt; Ətraflı" ShowSelectButton="True">
                    <HeaderStyle Width="100px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:CommandField>
                <asp:CommandField DeleteText="&lt;span class=&quot;glyphicon glyphicon-trash&quot; aria-hidden=&quot;true&quot;&gt;&lt;/span&gt; Sil" ShowDeleteButton="True">
                    <ControlStyle ForeColor="#CC0000" />
                    <HeaderStyle Width="100px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:CommandField>

            </Columns>
            <EditRowStyle BackColor="#7C6F57" />
            <EmptyDataTemplate>
                <div class="textBox" style="margin-top: 10px; margin-bottom: 10px; border-width: 0px">
                    Məlumat tapılmadı.
                </div>
            </EmptyDataTemplate>
            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <HeaderStyle Font-Bold="True" BackColor="#F6F6F6" ForeColor="Gray" Height="40px" HorizontalAlign="Left" />
            <PagerSettings PageButtonCount="20" />
            <PagerStyle BackColor="White" CssClass="Gridpager" ForeColor="White" HorizontalAlign="Right" />
            <RowStyle CssClass="hoverLink" Height="45px" HorizontalAlign="Left" Font-Bold="False" />
            <SelectedRowStyle BackColor="#99FF99" Font-Bold="True" ForeColor="#333333" />
        </asp:GridView>
    </ContentTemplate>
</asp:UpdatePanel>
