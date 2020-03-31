using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Adminn_Tools_UsersHistory : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DListPermissionsType.DataSource = DALC.GetUsersPermissionTypesList();
            DListPermissionsType.DataBind();
            DListPermissionsType.Items.Insert(0, new ListItem("--", "0"));

            DListOrganization.DataSource = DALC.GetOrganizations();
            DListOrganization.DataBind();
            DListOrganization.Items.Insert(0, new ListItem("--", "0"));

            DListOrganization_SelectedIndexChanged(null, null);

            DListHistoryTypes.DataSource = DALC.GetHistoryTypes();
            DListHistoryTypes.DataBind();
            DListHistoryTypes.Items.Insert(0, new ListItem("--", "0"));

            LnkOtherApp_Click(null, null);
        }
    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        //Count cache clear
        Session["HistoryList"]=null;
        BtnSearch.CommandArgument = "1";
        LnkOtherApp.CommandArgument = "0";
        LnkOtherApp_Click(null, null);
    }

    protected void LnkOtherApp_Click(object sender, EventArgs e)
    {
        GrdHistory.DataSource = null;
        GrdHistory.DataBind();

        LnkOtherApp.Visible = false;
        LblCount.Text = "Axtarış üzrə: 0      Səhifə üzrə: 0";
        LnkOtherApp.CommandArgument = (int.Parse(LnkOtherApp.CommandArgument) + 20)._ToString();

        bool Cache = true;
        // Əgər filter olunubsa Cache-de saxlamayaq
        if (BtnSearch.CommandArgument == "1")
            Cache = false;

        DALC.DataTableResult HistoryTypes = DALC.GetHistory(LnkOtherApp.CommandArgument, DListOrganization.SelectedValue, DListUsersName.SelectedValue, DListHistoryTypes.SelectedValue, DListPermissionsType.SelectedValue,"-1", Cache);

        if (HistoryTypes.Count == -1)
        {
            Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
            return;
        }

        GrdHistory.DataSource = HistoryTypes.Dt;
        GrdHistory.DataBind();

        LblCount.Text = "Axtarış üzrə: " + HistoryTypes.Count + "      Səhifə üzrə: " + GrdHistory.Rows.Count._ToString();

        if (HistoryTypes.Dt.Rows.Count > 0)
            LnkOtherApp.Visible = (GrdHistory.Rows.Count < HistoryTypes.Count);

        BtnSearch.CommandArgument = "";
    }


    protected void DListOrganization_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DListOrganization.SelectedValue == "0")
        {
            DListUsersName.Enabled = false;
            DListUsersName.DataSource = null;
            DListUsersName.DataBind();
            DListUsersName.Items.Insert(0, new ListItem("--", "0"));
        }
        else
        {
            DListUsersName.Enabled = true;
            DListUsersName.DataSource = DALC.GetUserNamesByOrganizationsID(DListOrganization.SelectedValue);
            DListUsersName.DataBind();

            if (DListUsersName.Items.Count < 1)
                DListUsersName.Enabled = false;

            DListUsersName.Items.Insert(0, new ListItem("--", "0"));
        }

    }
}