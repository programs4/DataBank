using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;

public partial class Detail_Contents_270_Huquqpozma : System.Web.UI.UserControl
{
    int _PersonsID = 0;
    string _TableName = "PersonsHuquqpozma";

    string[] _SqlSelectColumns =
                               {
                                   "(Select Name From PersonsHuquqpozmaMecelleNov Where ID=PersonsHuquqpozmaMecelleNovID) as MecelleNov",
                                   "MehkemeAdi",
                                   "Madde",
                                   "MehkemeQerari",
                                   "MehkemeQerarininSayi",
                                   Config.SqlDateTimeFormat("MehkemeQerarininTarixi"),
                                   "(Select Name From PersonsHuquqpozmaCezaNov Where ID=PersonsHuquqpozmaCezaNovID) as CezaNov",
                                   "MuessiseAdi",
                                   Config.SqlAddressColumnsFormat("MuessiseCountriesID", "MuessiseRegionsID", "MuessiseUnvan", "Unvan"),
                                   Config.SqlDateTimeFormat("MonitorinqTarixi"),
                                   "MonitorinqNeticesi",
                                   "(Select Name From PersonsHuquqpozmaMecburiTedbirNov Where ID=PersonsHuquqpozmaMecburiTedbirNovID) as MecburiTedbirNov",
                                   "(Select Name From PersonsHuquqpozmaTenbehNov Where ID=PersonsHuquqpozmaTenbehNovID) as TenbehNov",
                                   "(Select Name From PersonsHuquqpozmaAzadEdilmeSebebNov Where ID=PersonsHuquqpozmaAzadEdilmeSebebNovID) as AzadEdilmeSebebNov",
                                   Config.SqlDateTimeFormat("CezaninBitdiyiTarix"),
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
        DListQerarD.DataSource = DListMonitoringD.DataSource = DListCezaBitmeD.DataSource = Config.NumericList(1, 31);
        DListMonitoringD.DataBind();
        DListCezaBitmeD.DataBind();
        DListQerarD.DataBind();

        DListQerarM.DataSource = DListMonitoringM.DataSource = DListCezaBitmeM.DataSource = Config.MonthList();
        DListMonitoringM.DataBind();
        DListCezaBitmeM.DataBind();
        DListQerarM.DataBind();

        DListQerarY.DataSource = DListMonitoringY.DataSource = Config.NumericList(1980, DateTime.Now.Year, "1000");
        DListMonitoringY.DataBind();
        DListQerarY.DataBind();
        DListCezaBitmeY.DataSource = Config.NumericList(1980, DateTime.Now.AddYears(15).Year, "1000");
        DListCezaBitmeY.DataBind();
    }

    public void BindTypes()
    {
        DListMecelle.DataSource = DALC.GetSoragcaByTableName("PersonsHuquqpozmaMecelleNov");
        DListMecelle.DataBind();
        DListMecelle.Items.Insert(0, new ListItem("--", "0"));

        DListCezaNov.DataSource = DALC.GetSoragcaByTableName("PersonsHuquqpozmaCezaNov");
        DListCezaNov.DataBind();
        DListCezaNov.Items.Insert(0, new ListItem("--", "0"));

        DListCezaNov.DataSource = DALC.GetSoragcaByTableName("PersonsHuquqpozmaCezaNov");
        DListCezaNov.DataBind();
        DListCezaNov.Items.Insert(0, new ListItem("--", "0"));

        DListTerbiyeviTedbir.DataSource = DALC.GetSoragcaByTableName("PersonsHuquqpozmaMecburiTedbirNov");
        DListTerbiyeviTedbir.DataBind();
        DListTerbiyeviTedbir.Items.Insert(0, new ListItem("--", "0"));

        DListTenbehNov.DataSource = DALC.GetSoragcaByTableName("PersonsHuquqpozmaTenbehNov");
        DListTenbehNov.DataBind();
        DListTenbehNov.Items.Insert(0, new ListItem("--", "0"));

        DListCezaAzad.DataSource = DALC.GetSoragcaByTableName("PersonsHuquqpozmaAzadEdilmeSebebNov");
        DListCezaAzad.DataBind();
        DListCezaAzad.Items.Insert(0, new ListItem("--", "0"));
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
        if (!DALC._IsPermissionTypeEdit(390))
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
        BindTypes();

        // Təmizləmə işləri
        TxtMehkemeAd.Text = TxtMadde.Text = TxtMehkemeQerari.Text = TxtMehkemeQerarSayi.Text = TxtCezaMuessise.Text = TxtUnvan.Text = TxtMonitoringNeticesi.Text = TxtDescription.Text = "";
        DListMecelle.SelectedValue = DListCezaNov.SelectedValue = DlistCountry.SelectedValue = DlistCity.SelectedValue = DListTerbiyeviTedbir.SelectedValue = DListTenbehNov.SelectedValue = DListCezaAzad.SelectedValue = "0";
        DListQerarD.SelectedValue = DListQerarM.SelectedValue = DListCezaBitmeD.SelectedValue = DListCezaBitmeM.SelectedValue = DListMonitoringD.SelectedValue = DListMonitoringM.SelectedValue = "00";
        DListQerarY.SelectedValue = DListMonitoringY.SelectedValue = DListCezaBitmeY.SelectedValue = "1000";

        GrdList.SelectedIndex = -1;
        PnlEdit.Visible = true;
    }

    protected void LnkSuccess_Click(object sender, EventArgs e)
    {
        string ID = "";
        string LogText = "";
        int HistoryStatus = 20;

        if (TxtMehkemeAd.Text.Trim().Length < 1)
        {
            Config.MsgBoxAjax("İşə baxmış məhkəmənin adı mütləq qeyd olunmalıdır.", Page);
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
            LogText = "Hüquq pozmalar üzrə yeni məlumat əlavə edildi. № {0}";
        }
        else
        {
            ID = GrdList.SelectedDataKey["ID"]._ToString();
            LogText = "Hüquq pozmalar üzrə məlumatda düzəliş edildi. № {0}";
            HistoryStatus = 30;
        }

        Dictionary<string, object> Dictionary = new Dictionary<string, object>
        {
            { "OrganizationsID", DALC._GetUsersLogin.OrganizationsID },
            { "UsersID", DALC._GetUsersLogin.ID },
            { "PersonsHuquqpozmaMecelleNovID", DListMecelle.SelectedValue.NullConvert() },
            { "MehkemeAdi", TxtMehkemeAd.Text.Trim() },
            { "Madde", TxtMadde.Text.Trim() },
            { "MehkemeQerari", TxtMehkemeQerari.Text.Trim() },
            { "MehkemeQerarininSayi", TxtMehkemeQerarSayi.Text.Trim() },
            { "MehkemeQerarininTarixi", (DListQerarY.SelectedValue + DListQerarM.SelectedValue + DListQerarD.SelectedValue).NullConvert() },
            { "PersonsHuquqpozmaCezaNovID", DListCezaNov.SelectedValue.NullConvert() },
            { "MuessiseAdi", TxtCezaMuessise.Text.Trim() },
            { "MuessiseCountriesID", DlistCountry.SelectedValue.NullConvert() },
            { "MuessiseRegionsID", DlistCity.SelectedValue.NullConvert() },
            { "MuessiseUnvan", TxtUnvan.Text.Trim() },
            { "MonitorinqNeticesi", TxtMonitoringNeticesi.Text.Trim() },
            { "MonitorinqTarixi", (DListMonitoringY.SelectedValue + DListMonitoringM.SelectedValue + DListMonitoringD.SelectedValue).NullConvert() },
            { "PersonsHuquqpozmaMecburiTedbirNovID", DListTerbiyeviTedbir.SelectedValue.NullConvert() },
            { "PersonsHuquqpozmaTenbehNovID", DListTenbehNov.SelectedValue.NullConvert() },
            { "PersonsHuquqpozmaAzadEdilmeSebebNovID", DListCezaAzad.SelectedValue.NullConvert() },
            { "CezaninBitdiyiTarix", (DListCezaBitmeY.SelectedValue + DListCezaBitmeM.SelectedValue + DListCezaBitmeD.SelectedValue).NullConvert() },
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

        DALC.UsersHistoryInsert(HistoryStatus, 390, string.Format(LogText, ID), _PersonsID);
        Config.MsgBoxAjax("Əməliyyat uğurla yerinə yetirildi.", Page, true);
        PnlEdit.Visible = false;
        GridBind();
    }

    protected void GrdList_SelectedIndexChanged(object sender, EventArgs e)
    {
        PnlEdit.Visible = true;
        Aylar();
        Countries();
        BindTypes();

        DataTable Dt = DALC.GetDataTableParams("*", _TableName, "ID", GrdList.SelectedDataKey["ID"]._ToString(), "");

        if (Dt == null || Dt.Rows.Count < 1)
        {
            Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
            return;
        }

        DListMecelle.SelectedValue = Dt._Rows("PersonsHuquqpozmaMecelleNovID").IsEmptyReplaceNoul(); ;
        TxtMehkemeAd.Text = Dt._Rows("MehkemeAdi");
        TxtMadde.Text = Dt._Rows("Madde");
        TxtMehkemeQerari.Text = Dt._Rows("MehkemeQerari");
        TxtMehkemeQerarSayi.Text = Dt._Rows("MehkemeQerarininSayi");
        DListQerarD.SelectedValue = Dt._RowsObject("MehkemeQerarininTarixi").IntDateDay();
        DListQerarM.SelectedValue = Dt._RowsObject("MehkemeQerarininTarixi").IntDateMonth();
        DListQerarY.SelectedValue = Dt._RowsObject("MehkemeQerarininTarixi").IntDateYear();
        DListCezaNov.SelectedValue = Dt._Rows("PersonsHuquqpozmaCezaNovID").IsEmptyReplaceNoul(); ;
        TxtCezaMuessise.Text = Dt._Rows("MuessiseAdi");
        DlistCountry.SelectedValue = Dt._Rows("MuessiseCountriesID").IsEmptyReplaceNoul();
        Regions(DlistCity, DlistCountry.SelectedValue);
        DlistCity.SelectedValue = Dt._Rows("MuessiseRegionsID").IsEmptyReplaceNoul();
        TxtUnvan.Text = Dt._Rows("MuessiseUnvan");
        DListMonitoringD.SelectedValue = Dt._RowsObject("MonitorinqTarixi").IntDateDay();
        DListMonitoringM.SelectedValue = Dt._RowsObject("MonitorinqTarixi").IntDateMonth();
        DListMonitoringY.SelectedValue = Dt._RowsObject("MonitorinqTarixi").IntDateYear();
        TxtMonitoringNeticesi.Text = Dt._Rows("MonitorinqNeticesi");
        DListTerbiyeviTedbir.SelectedValue = Dt._Rows("PersonsHuquqpozmaMecburiTedbirNovID").IsEmptyReplaceNoul(); ;
        DListTenbehNov.SelectedValue = Dt._Rows("PersonsHuquqpozmaTenbehNovID").IsEmptyReplaceNoul(); ;
        DListCezaAzad.SelectedValue = Dt._Rows("PersonsHuquqpozmaAzadEdilmeSebebNovID").IsEmptyReplaceNoul(); ;
        DListCezaBitmeD.SelectedValue = Dt._RowsObject("CezaninBitdiyiTarix").IntDateDay();
        DListCezaBitmeM.SelectedValue = Dt._RowsObject("CezaninBitdiyiTarix").IntDateMonth();
        DListCezaBitmeY.SelectedValue = Dt._RowsObject("CezaninBitdiyiTarix").IntDateYear();
        TxtDescription.Text = Dt._Rows("Description");
        PnlEdit.Visible = true;
    }

    protected void GrdList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int ID = GrdList.DataKeys[e.RowIndex]["ID"]._ToInt32();

        Dictionary<string, object> Dictionary = new Dictionary<string, object>
        {
            { "OrganizationsID", DALC._GetUsersLogin.OrganizationsID },
            { "UsersID", DALC._GetUsersLogin.ID },
            { "IsDeleted", 1 },
            { "Last_Dt", DateTime.Now },
            { "Last_Ip", HttpContext.Current.Request.UserHostAddress.IPToInteger() },
            { "WhereID", ID }
        };

        int ChekUpdate = DALC.UpdateDatabase(_TableName, Dictionary);
        if (ChekUpdate < 1)
        {
            Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
            return;
        }
        PnlEdit.Visible = false;
        GrdList.Rows[e.RowIndex].Visible = false;
        DALC.UsersHistoryInsert(40, 390, string.Format("Hüquq pozmalar üzrə məlumat silindi. № {0}", ID._ToString()), _PersonsID);
    }

    protected void DlistCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        Regions(DlistCity, DlistCountry.SelectedValue);
    }

    protected void LnkCancel_Click(object sender, EventArgs e)
    {
        PnlEdit.Visible = false;
    }
}