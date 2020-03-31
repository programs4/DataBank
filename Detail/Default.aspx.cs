using System;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Detail_Default : Page
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

        PersonsID = Config._GetQueryString("i").QueryIdDecrypt();

        if (!PersonsID.IsNumeric())
        {
            Config.RedirectMain();
            return;
        }

        string Check = DALC.GetDbSingleValuesParams("Count(ID)", "Persons", "ID", PersonsID, "", "-1");

        if (Check == "-1" || Check == "0")
        {
            Config.RedirectError();
            return;
        }

        // tarixcəyə icazəsi varmi yoxdumu baxaq
        LtrHistoryIcon.Visible = LnkHistory.Visible = DALC._IsPermissionType(30);

        if (!IsPostBack)
        {
            RptItems.DataSource = DALC.GetAccardionTypes();
            RptItems.DataBind();
        }
    }

    protected void PnlContent_Load(object sender, EventArgs e)
    {
        Panel P = ((Panel)sender);
        P.Controls.Add(Page.LoadControl("Contents/" + P.CssClass + ".ascx"));
    }

    protected void LnkHistory_Click(object sender, EventArgs e)
    {
        Config.Redirect("/history/?i=" + PersonsID);
    }
}