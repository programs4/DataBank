using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

public partial class AdoptionAdminn_Tools_AdoptionPersons : System.Web.UI.UserControl
{
    string _TableName = "AdoptionPersons";
    Dictionary<string, object> FilterDictionary = new Dictionary<string, object>();
    string AdoptionPersonsStatusID = "-1";
    private void BindNeeders()
    {
        GrdAdoptionPersonsList.DataSource = null;
        GrdAdoptionPersonsList.DataBind();

        PnlFilter.BindControls(FilterDictionary, _TableName);
        DListStatusType_SelectedIndexChanged(null, null);
        FilterDictionary = new Dictionary<string, object>()
        {
             {"Ap.ID",TxtID.Text},
             {"CONCAT(Soyad,' ',Ad,' ',Ata)(LIKE)",TxtFullname.Text},
             {"IsWebPreview",DListIsWebPreview.SelectedValue},
             {"IsBrotherSister",DListIsBrotherSister.SelectedValue},
             {"AdoptionPersonsStatusID",int.Parse(AdoptionPersonsStatusID)}
        };

        if (DListStatusType.SelectedIndex == 0)
        {
            FilterDictionary.Add("AdoptionPersonsStatusID(NOTIN)", 90);
        }

        int PageNum;
        int RowNumber = 16;

        if (!int.TryParse(Config._GetQueryString("pn"), out PageNum))
        {
            PageNum = 1;
        }

        HdnPageNumber.Value = PageNum.ToString();

        DALC.DataTableResult AdoptionPersonsResult = DALC_Adoption.GetAdoptionPersons(FilterDictionary, PageNum, RowNumber);

        if (AdoptionPersonsResult.Count == -1)
        {
            return;
        }

        if (AdoptionPersonsResult.Dt.Rows.Count < 1 && PageNum > 1)
        {
            Config.Redirect(string.Format("/adoptionadminn/tools/?p=adoptionpersons&pn={0}", PageNum - 1));
        }

        LblCount.Text = string.Format("Axtarış üzrə nəticə: {0}", AdoptionPersonsResult.Count.ToString());

        int Total_Count = AdoptionPersonsResult.Count % RowNumber > 0 ? (AdoptionPersonsResult.Count / RowNumber) + 1 : AdoptionPersonsResult.Count / RowNumber;
        HdnTotalCount.Value = Total_Count.ToString();

        PnlPager.Visible = AdoptionPersonsResult.Count > RowNumber;

        GrdAdoptionPersonsList.DataSource = AdoptionPersonsResult.Dt;
        GrdAdoptionPersonsList.DataBind();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DListAdoptionPersonStatus.DataSource = DALC_Adoption.GetAdoptionPersonsStatus();
            DListAdoptionPersonStatus.DataBind();
            DListAdoptionPersonStatus.Items.Insert(0, new ListItem("--", "-1"));
            BindNeeders();
        }
    }

    protected void LnkEdit_Click(object sender, EventArgs e)
    {
        Config.Redirect(string.Format("/adoptionadminn/tools/?p=adoptionpersonoperations&id={0}", (sender as LinkButton).CommandArgument));
    }

    protected void BtnFilter_Click(object sender, EventArgs e)
    {
        PnlFilter.BindControls(FilterDictionary, _TableName, true);
        Cache.Remove(_TableName._ToString());
        Config.Redirect("/adoptionadminn/tools/?p=adoptionpersons&pn=1");
    }

    protected void LnkAdd_Click(object sender, EventArgs e)
    {
        Config.Redirect("/adoptionadminn/tools/?p=adoptionpersonoperations");
    }

    protected void LnkAddSisterBrother_Click(object sender, EventArgs e)
    {
        Config.Redirect(string.Format("/adoptionadminn/tools/?p=adoptionpersonoperations&id={0}&type={1}", (sender as LinkButton).CommandArgument, (sender as LinkButton).CommandName));
    }

    protected void DListStatusType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DListStatusType.SelectedValue == "90")
        {
            DListAdoptionPersonStatus.SelectedIndex = 0;
            DListAdoptionPersonStatus.Enabled = false;
            AdoptionPersonsStatusID = "90";
        }
        else
        {
            DListAdoptionPersonStatus.Enabled = true;
            AdoptionPersonsStatusID = DListAdoptionPersonStatus.SelectedValue;
        }
    }

}