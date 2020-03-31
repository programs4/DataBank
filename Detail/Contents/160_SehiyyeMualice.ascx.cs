using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;

public partial class Detail_Contents_160_SehiyyeMualice : System.Web.UI.UserControl
{
    int _PersonsID = 0;
    string _TableName = "PersonsSehiyyeMuessiseMualice";

    string[] _SqlSelectColumns =
                               {
                                   "Adi",
                                   Config.SqlAddressColumnsFormat("CountriesID", "RegionsID", "Unvan", "Unvan"),
                                   "Telefon",
                                   Config.SqlDateTimeFormat("MualiceTarixi"),
                                   Config.SqlDateTimeFormat("MonitorinqTarixi"),
                                   "MonitorinqNeticesi",
                                   "TibbIshcisiSoyadAd",
                                   "TibbIshcisiTelefon",
                                   "Description"
                               };


    public void GridBind()
    {
        GrdList.DataSource = DALC.GridBindDataTable(_PersonsID, _TableName, _SqlSelectColumns);
        GrdList.DataBind();

        for (int i = 0; i < GrdList.Rows.Count; i++)
        {
            ((LinkButton)GrdList.Rows[i].Cells[GrdList.Columns.Count - 1].Controls[0]).OnClientClick = "return confirm('Əminsiniz?');";
        }
    }

    #region Soragcalar
    //Sorgacalar
    public void Aylar()
    {
        DListMonitoringD.DataSource = DListMualiceD.DataSource = Config.NumericList(1, 31);
        DListMonitoringD.DataBind();
        DListMualiceD.DataBind();

        DListMonitoringM.DataSource = DListMualiceM.DataSource = Config.MonthList();
        DListMonitoringM.DataBind();
        DListMualiceM.DataBind();

        DListMonitoringY.DataSource = DListMualiceY.DataSource = Config.NumericList(1980, DateTime.Now.Year, "1000");
        DListMonitoringY.DataBind();
        DListMualiceY.DataBind();

    }

    public void Countries()
    {
        DataTable Dt = DALC.GetCountriesList();

        DlistCountry.DataSource = Dt;
        DlistCountry.DataBind();
        DlistCountry.Items.Insert(0, new ListItem("--", "0"));

        Regions(DlistCity, DlistCountry.SelectedValue);
    }

    public void Regions(DropDownList D, string CountryID)
    {
        if (CountryID == "0")
        {
            D.Enabled = false;
            D.DataSource = null;
            D.DataBind();
            D.Items.Insert(0, new ListItem("--", "0"));
            return;
        }
        else
        {
            D.Enabled = true;
            D.DataSource = DALC.GetRegionsList(CountryID);
            D.DataBind();
            D.Items.Insert(0, new ListItem("--", "0"));
        }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        //Sadəcə alırıq. Yoxlamağa ehtiyac yoxdur. Content/Default.aspx də yoxlanılır.
        _PersonsID = Config._GetQueryString("i").QueryIdDecrypt()._ToInt32();

        if (!IsPostBack)
        {
            GridBind();
        }

        //Silinmə əməliyyatına icazə varmı?
        if (!DALC._IsPermissionTypeEdit(280))
        {
            LnkAdd.Visible = false;
            PnlEdit.Visible = false;
            GrdList.Columns[GrdList.Columns.Count - 1].Visible = false;
            GrdList.Columns[GrdList.Columns.Count - 2].Visible = false;
        }
    }

    protected void LnkAdd_Click(object sender, EventArgs e)
    {
        Aylar();
        Countries();

        // Təmizləmə işləri
        TxtMuessiseAdi.Text = TxtUnvan.Text = TxtPhone.Text = TxtMonitoringNeticesi.Text = TxtTibbBacısıFullName.Text = TxtTibbBacısıPhone.Text = TxtDescription.Text = "";
        DListMonitoringD.SelectedValue = DListMonitoringM.SelectedValue = DListMualiceD.SelectedValue = DListMualiceM.SelectedValue = "00";
        DListMonitoringY.SelectedValue = DListMualiceY.SelectedValue = "1000";

        GrdList.SelectedIndex = -1;
        PnlEdit.Visible = true;
    }

    protected void LnkSuccess_Click(object sender, EventArgs e)
    {
        string ID = "";
        string LogText = "";
        int HistoryStatus = 20;

        if (TxtMuessiseAdi.Text.Trim().Length < 1)
        {
            Config.MsgBoxAjax("Müəssisə adı mütləq qeyd olunmalıdır.", Page);
            return;
        }

        if (GrdList.SelectedIndex == -1)
        {
            ID = DALC.NewBlankInsert(_TableName, _PersonsID).ToString();
            if (int.Parse(ID) < 1)
            {
                Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
                return;
            }
            LogText = "Müalicə aldığı səhiyyə müəssisəsi üzrə yeni məlumat əlavə edildi. № {0}";
        }
        else
        {
            ID = GrdList.SelectedDataKey["ID"]._ToString();
            LogText = "Müalicə aldığı səhiyyə müəssisəsi üzrə məlumatda düzəliş edildi. № {0}";
            HistoryStatus = 30;
        }

        Dictionary<string, object> Dictionary = new Dictionary<string, object>
        {
            { "OrganizationsID", DALC._GetUsersLogin.OrganizationsID },
            { "UsersID", DALC._GetUsersLogin.ID },
            { "Adi", TxtMuessiseAdi.Text.Trim() },
            { "CountriesID", DlistCountry.SelectedValue.NullConvert() },
            { "RegionsID", DlistCity.SelectedValue.NullConvert() },
            { "Unvan", TxtUnvan.Text.Trim() },
            { "Telefon", TxtPhone.Text.Trim() },
            { "MualiceTarixi", (DListMualiceY.SelectedValue + DListMualiceM.SelectedValue + DListMualiceD.SelectedValue).NullConvert() },
            { "MonitorinqTarixi", (DListMonitoringY.SelectedValue + DListMonitoringM.SelectedValue + DListMonitoringD.SelectedValue).NullConvert() },
            { "MonitorinqNeticesi", TxtMonitoringNeticesi.Text.Trim() },
            { "TibbIshcisiSoyadAd", TxtTibbBacısıFullName.Text.Trim() },
            { "TibbIshcisiTelefon", TxtTibbBacısıPhone.Text.Trim() },
            { "Description", TxtDescription.Text.Trim() },
            { "IsDeleted", 0 },
            { "Last_Dt", DateTime.Now },
            { "Last_Ip", HttpContext.Current.Request.UserHostAddress.IPToInteger() },
            { "WhereID", int.Parse(ID) }
        };

        int ChekUpdate = DALC.UpdateDatabase(_TableName, Dictionary);
        if (ChekUpdate < 1)
        {
            Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
            return;
        }

        DALC.UsersHistoryInsert(HistoryStatus, 280, string.Format(LogText, ID), _PersonsID);
        Config.MsgBoxAjax("Əməliyyat uğurla yerinə yetirildi.", Page, true);
        PnlEdit.Visible = false;
        GridBind();
    }

    protected void GrdList_SelectedIndexChanged(object sender, EventArgs e)
    {
        PnlEdit.Visible = true;
        Aylar();
        Countries();

        DataTable Dt = DALC.GetDataTableParams("*", _TableName, "ID", GrdList.SelectedDataKey["ID"]._ToString(), "");

        if (Dt == null || Dt.Rows.Count < 1)
        {
            Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
            return;
        }

        TxtMuessiseAdi.Text = Dt._Rows("Adi");
        DlistCountry.SelectedValue = Dt._Rows("CountriesID").IsEmptyReplaceNoul();
        Regions(DlistCity, DlistCountry.SelectedValue);
        DlistCity.SelectedValue = Dt._Rows("RegionsID").IsEmptyReplaceNoul();
        TxtUnvan.Text = Dt._Rows("Unvan");
        TxtPhone.Text = Dt._Rows("Telefon");
        DListMualiceD.SelectedValue = Dt._RowsObject("MualiceTarixi").IntDateDay();
        DListMualiceM.SelectedValue = Dt._RowsObject("MualiceTarixi").IntDateMonth();
        DListMualiceY.SelectedValue = Dt._RowsObject("MualiceTarixi").IntDateYear();
        DListMonitoringD.SelectedValue = Dt._RowsObject("MonitorinqTarixi").IntDateDay();
        DListMonitoringM.SelectedValue = Dt._RowsObject("MonitorinqTarixi").IntDateMonth();
        DListMonitoringY.SelectedValue = Dt._RowsObject("MonitorinqTarixi").IntDateYear();
        TxtMonitoringNeticesi.Text = Dt._Rows("MonitorinqNeticesi");
        TxtTibbBacısıFullName.Text = Dt._Rows("TibbIshcisiSoyadAd");
        TxtTibbBacısıPhone.Text = Dt._Rows("TibbIshcisiTelefon");
        TxtDescription.Text = Dt._Rows("Description");
        PnlEdit.Visible = true;
    }

    protected void GrdList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int ID = GrdList.DataKeys[e.RowIndex]["ID"]._ToInt32();
        Dictionary<string, object> Dictionary = new Dictionary<string, object>();
        Dictionary.Add("OrganizationsID", DALC._GetUsersLogin.OrganizationsID);
        Dictionary.Add("UsersID", DALC._GetUsersLogin.ID);
        Dictionary.Add("IsDeleted", 1);
        Dictionary.Add("Last_Dt", DateTime.Now);
        Dictionary.Add("Last_Ip", HttpContext.Current.Request.UserHostAddress.IPToInteger());
        Dictionary.Add("WhereID", ID);

        int ChekUpdate = DALC.UpdateDatabase(_TableName, Dictionary);
        if (ChekUpdate < 1)
        {
            Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
            return;
        }
        PnlEdit.Visible = false;
        GrdList.Rows[e.RowIndex].Visible = false;
        DALC.UsersHistoryInsert(40, 280, string.Format("Müalicə aldığı səhiyyə müəssisəsi üzrə məlumat silindi. № {0}", ID), _PersonsID);
    }

    protected void LnkCancel_Click(object sender, EventArgs e)
    {
        PnlEdit.Visible = false;
    }

    protected void DlistCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        Regions(DlistCity, DlistCountry.SelectedValue);
    }
}