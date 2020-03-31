using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdoptionAdminn_Tools_UsersHistory : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DListUsersName.DataSource = DALC.GetDataTable("ID,Username", "AdoptionAdministrators", "");
            DListUsersName.DataBind();
            DListUsersName.Items.Insert(0, new ListItem("--", "-1"));
            LnkOtherApp_Click(null, null);
        }
    }

    protected void LnkOtherApp_Click(object sender, EventArgs e)
    {
        GrdHistory.DataSource = null;
        GrdHistory.DataBind();

        LnkOtherApp.Visible = false;
        LblCount.Text = "Axtarış üzrə: 0      Səhifə üzrə: 0";
        LnkOtherApp.CommandArgument = (int.Parse(LnkOtherApp.CommandArgument) + 20)._ToString();
       
        Dictionary<string, object> DictHistory = new Dictionary<string, object>();
        DictHistory.Add("AdoptionAdministratorsID", int.Parse(DListUsersName.SelectedValue));
                        
        DALC.DataTableResult AdoptionAdministratorHistory = DALC_Adoption.GetAdoptionAdministratorHistory(int.Parse(LnkOtherApp.CommandArgument), DictHistory, "");

        if (AdoptionAdministratorHistory.Count == -1)
        {
            Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
            return;
        }

        GrdHistory.DataSource = AdoptionAdministratorHistory.Dt;
        GrdHistory.DataBind();

        LblCount.Text = "Axtarış üzrə: " + AdoptionAdministratorHistory.Count + "      Səhifə üzrə: " + GrdHistory.Rows.Count._ToString();

        if (AdoptionAdministratorHistory.Dt.Rows.Count > 0)
            LnkOtherApp.Visible = (GrdHistory.Rows.Count < AdoptionAdministratorHistory.Count);

        BtnSearch.CommandArgument = "";
    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        //Count cache clear
        Session["HistoryList"] = null;
        BtnSearch.CommandArgument = "1";
        LnkOtherApp.CommandArgument = "0";
        LnkOtherApp_Click(null, null);
    }
}