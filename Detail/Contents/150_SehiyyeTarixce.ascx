<%@ Control Language="C#" AutoEventWireup="true" CodeFile="150_SehiyyeTarixce.ascx.cs" Inherits="Detail_Contents_150_SehiyyeTarixce" %>
<asp:UpdatePanel ID="UpdatePanel1"  runat="server">
    <ContentTemplate>
        <asp:Panel ID="PnlAdd" runat="server" Style="padding: 15px; padding-left: 0px;" HorizontalAlign="Left" Width="100%">
            <asp:LinkButton ID="LnkAdd" runat="server" OnClick="LnkAdd_Click"><img class="alignMiddle" src="/pics/new.png" /> YENİ ƏLAVƏ</asp:LinkButton>
        </asp:Panel>
        
                <asp:Panel ID="PnlEdit" Visible="false" runat="server" CssClass="ContentRoot">
                    <div class="PnlItems">
                        Növü<br />
                        <asp:DropDownList ID="DListTarixceNov" runat="server" CssClass="form-control" DataTextField="Name" DataValueField="ID" Width="250px" AutoPostBack="True" OnSelectedIndexChanged="DListTarixceNov_SelectedIndexChanged">
                        </asp:DropDownList>
                        <br />
                        <br />
                        Adı
                <asp:TextBox ID="TxtAdi" CssClass="form-control .col-md-6" Width="100%" runat="server"></asp:TextBox>
                        <br />
                        <br />
                        <asp:Panel ID="PnlSaglamlıq" runat="server">
                            Sağlamlıq imkanları<br />
                            <asp:TextBox ID="TxtSaglamlıq" runat="server" CssClass="form-control .col-md-6" Height="70px" TextMode="MultiLine" Width="100%"></asp:TextBox>
                            <br />
                            <br />
                        </asp:Panel>
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

                <ul id="myTab" class="nav nav-pills" role="tablist" style="background-color: #F9F9F9; padding: 5px; border: 1px solid #E4E4E4;">

                    <li role="presentation" class="active"><a href="#rec" id="rec-tab" role="tab" data-toggle="tab" aria-controls="rec" aria-expanded="false">
                        <span class="glyphicon glyphicon-pencil"></span>&nbsp;&nbsp;Xəstəlikləri</a></li>

                    <li role="presentation" class=""><a href="#history" role="tab" id="history-tab" data-toggle="tab" aria-controls="history" aria-expanded="true"><span class="glyphicon glyphicon-list-alt"></span>&nbsp;&nbsp;Peyvəndləri</a></li>

                </ul>
                <div id="myTabContent" class="tab-content" style="padding: 10px 0px 10px 3px; margin-top: 5px">

                    <div role="tabpanel" class="tab-pane fade active in" id="rec" aria-labelledby="rec-tab">
                        <asp:GridView ID="GrdList1" runat="server" BorderColor="#EAEAEA" CellPadding="4" ForeColor="#051615" Width="100%" Font-Bold="True" DataKeyNames="ID" AutoGenerateColumns="False" OnRowDeleting="GrdList1_RowDeleting" OnSelectedIndexChanged="GrdList1_SelectedIndexChanged">
                            <Columns>
                                <asp:BoundField DataField="Ss" HeaderText="№">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Adi" HeaderText="Xəstəliyin adı" />
                                <asp:BoundField DataField="SaglamliqImkanlari" HeaderText="Sağlamlıq imkanları" HtmlEncode="False" />
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
                    </div>


                    <div role="tabpanel" class="tab-pane fade" id="history" aria-labelledby="history-tab">
                        <asp:GridView ID="GrdList2" runat="server" BorderColor="#EAEAEA" CellPadding="4" ForeColor="#051615" Width="100%" Font-Bold="True" DataKeyNames="ID" AutoGenerateColumns="False" OnRowDeleting="GrdList2_RowDeleting" OnSelectedIndexChanged="GrdList2_SelectedIndexChanged">
                            <Columns>
                                <asp:BoundField DataField="Ss" HeaderText="№">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Adi" HeaderText="Peyvəndin adı" />
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
                    </div>

                </div>
           
    </ContentTemplate>
</asp:UpdatePanel>
