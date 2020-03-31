<%@ Page Language="C#" MasterPageFile="~/Error/MasterPage.master" %>

<script runat="server">

    protected void Page_Load(object sender, EventArgs e)
    {
        Session.Clear();
        Session.RemoveAll();
        Application.Clear();
    }
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMasterHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMasterBody" runat="Server">
    <div align="left" style="color: black; background-position: 25px center; padding-left: 100px; padding-top: 20px; padding-bottom: 20px; margin-right: 30px; margin-left: 30px; background-image: url('../Pics/Warning.png'); background-repeat: no-repeat;" class="textBox">
        <strong style="font-size: 20px; color: rgb(102, 102, 102);">SESSİYA MÜDDƏTİ BİTMİŞDİR<br />
        </strong>ZƏHMƏT OLMASA SƏHİFƏNİ YENİLƏYİN
    </div>
</asp:Content>

