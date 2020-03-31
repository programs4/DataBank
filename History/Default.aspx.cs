using System;
using System.Web.UI.WebControls;

public partial class History_Default : System.Web.UI.Page
{
    string PersonsID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //Login control
        if (DALC._GetUsersLogin == null)
        {
            Config.Redirect("/?return=" + Request.Url.ToString());
            return;
        }

        // tarixcəyə baxmağa icazəsi yoxdursa ataq qırağa
        if (!DALC._IsPermissionType(30))
        {
            Config.Redirect("/");
            return;
        }

        PersonsID = Config._GetQueryString("i").QueryIdDecrypt().Replace("-1", "");

        if (!string.IsNullOrEmpty(PersonsID) && !PersonsID.IsNumeric())
        {
            Config.RedirectMain();
            return;
        }

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
        Session["HistoryList"] = null;
        BtnSearch.CommandArgument = "1";
        LnkOtherApp.CommandArgument = "0";
        LnkOtherApp_Click(null, null);
    }

    protected void LnkOtherApp_Click(object sender, EventArgs e)
    {
        GrdHistory.DataSource = null;
        GrdHistory.DataBind();

        LnkOtherApp.Visible = false;
        LnkOtherApp.CommandArgument = (int.Parse(LnkOtherApp.CommandArgument) + 20)._ToString();

        bool Cache = true;
        // Əgər filter olunubsa Cache-de saxlamayaq
        if (BtnSearch.CommandArgument == "1")
            Cache = false;

        DALC.DataTableResult HistoryTypes = DALC.GetHistory(LnkOtherApp.CommandArgument, DListOrganization.SelectedValue, DListUsersName.SelectedValue, DListHistoryTypes.SelectedValue, DListPermissionsType.SelectedValue, PersonsID.IsEmptyReplaceNoul("-1"), Cache);

        if (HistoryTypes.Count == -1)
        {
            Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
            return;
        }

        GrdHistory.DataSource = HistoryTypes.Dt;
        GrdHistory.DataBind();

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
            DListUsersName.SelectedValue = "0";
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