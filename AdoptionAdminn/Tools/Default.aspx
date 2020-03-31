<%@ Page Language="C#" MasterPageFile="~/AdoptionAdminn/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="AdoptionAdminn_Tools_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMasterHead" runat="Server">
     $(document).ready(function () {
            CallDatePicker();
        });
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMasterBody" runat="Server">
    <asp:Panel ID="PanelControl" runat="server">
    </asp:Panel>
</asp:Content>

