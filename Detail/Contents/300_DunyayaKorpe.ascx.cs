using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;

public partial class Detail_Contents_300_DunyayaKorpe : System.Web.UI.UserControl
{
    int _PersonsID = 0;
    string _TableName = "PersonsDunyayaKorpe";

    string[] _SqlSelectColumns =
                               {

                                   Config.SqlAddressColumnsFormat("DogumCountriesID", "DogumRegionsID", "DogumUnvan", "Unvan"),
                                   Config.SqlDateTimeFormat("DogumTarixi"),
                                   "SaglamliqVeziyyeti",
                                   "AtaSoyad",
                                   "AtaAd",
                                   Config.SqlDateTimeFormat("AtaTevellud"),
                                   Config.SqlAddressColumnsFormat("AtaCountriesID", "AtaRegionsID", "AtaUnvan", "AtaUnvan"),
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
        DListAtaTevelludD.DataSource = DListDogumD.DataSource = Config.NumericList(1, 31);
        DListAtaTevelludD.DataBind();
        DListDogumD.DataBind();

        DListAtaTevelludM.DataSource = DListDogumM.DataSource = Config.MonthList();
        DListAtaTevelludM.DataBind();
        DListDogumM.DataBind();

        DListAtaTevelludY.DataSource = DListDogumY.DataSource = Config.NumericList(1980, DateTime.Now.Year, "1000");
        DListAtaTevelludY.DataBind();
        DListDogumY.DataBind();
    }
    public void Countries()
    {
        DataTable Dt = DALC.GetCountriesList();

        DlistDogumCountry.DataSource = DListAtaCountry.DataSource = Dt;
        DlistDogumCountry.DataBind();
        DListAtaCountry.DataBind();
        DlistDogumCountry.Items.Insert(0, new ListItem("--", "0"));
        DListAtaCountry.Items.Insert(0, new ListItem("--", "0"));

        Regions(DlistDogumCity, DlistDogumCountry.SelectedValue);
        Regions(DListAtaCity, DListAtaCountry.SelectedValue);

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
        if (!DALC._IsPermissionTypeEdit(420))
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
        TxtDogumUnvan.Text = TxtSaglam.Text = TxtAtaAd.Text = TxtAtaSoyad.Text = TxtAtaUnvan.Text = TxtDescription.Text = "";
        DlistDogumCountry.SelectedValue = DlistDogumCity.SelectedValue = DListAtaCountry.SelectedValue = DListAtaCity.SelectedValue = "0";
        DListDogumD.SelectedValue = DListDogumM.SelectedValue = DListAtaTevelludD.SelectedValue = DListAtaTevelludM.SelectedValue = "00";
        DListAtaTevelludY.SelectedValue = DListDogumY.SelectedValue = "1000";

        GrdList.SelectedIndex = -1;
        PnlEdit.Visible = true;
    }

    protected void LnkSuccess_Click(object sender, EventArgs e)
    {
        string ID = "";
        string LogText = "";
        int HistoryStatus = 20;

        if (DlistDogumCountry.SelectedValue == "0" && DlistDogumCity.SelectedValue == "0" && string.IsNullOrEmpty(TxtDogumUnvan.Text))
        {
            Config.MsgBoxAjax("Uşağın doğulduğu yer haqqında məlumatlardan ən az biri daxil edilməlidir.", Page);
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

            LogText = "Yetkinlik yaşına çatmayan qızın dünyaya körpə gətirdiyi görə yeni məlumat əlavə edildi. № {0}";
        }
        else
        {
            ID = GrdList.SelectedDataKey["ID"]._ToString();
            LogText = "Yetkinlik yaşına çatmayan qızın dünyaya körpə gətirdiyi görə məlumatda düzəliş edildi. № {0}";
            HistoryStatus = 30;
        }

        Dictionary<string, object> Dictionary = new Dictionary<string, object>
        {
            { "OrganizationsID", DALC._GetUsersLogin.OrganizationsID },
            { "UsersID", DALC._GetUsersLogin.ID },
            { "DogumCountriesID", DlistDogumCountry.SelectedValue.NullConvert() },
            { "DogumRegionsID", DlistDogumCity.SelectedValue.NullConvert()},
            { "DogumUnvan", TxtDogumUnvan.Text.Trim() },
            { "DogumTarixi", (DListDogumY.SelectedValue + DListDogumM.SelectedValue + DListDogumD.SelectedValue).NullConvert() },
            { "SaglamliqVeziyyeti", TxtSaglam.Text.Trim() },
            { "AtaSoyad", TxtAtaSoyad.Text.Trim() },
            { "AtaAd", TxtAtaAd.Text.Trim() },
            { "AtaTevellud", (DListAtaTevelludY.SelectedValue + DListAtaTevelludM.SelectedValue + DListAtaTevelludD.SelectedValue).NullConvert() },
            { "AtaCountriesID", DListAtaCountry.SelectedValue.NullConvert() },
            { "AtaRegionsID", DListAtaCity.SelectedValue.NullConvert() },
            { "AtaUnvan", TxtAtaUnvan.Text.Trim() },
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

        DALC.UsersHistoryInsert(HistoryStatus, 420, string.Format(LogText, ID), _PersonsID);
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

        DlistDogumCountry.SelectedValue = Dt._Rows("DogumCountriesID").IsEmptyReplaceNoul();
        Regions(DlistDogumCity, DlistDogumCountry.SelectedValue);
        DlistDogumCity.SelectedValue = Dt._Rows("DogumRegionsID").IsEmptyReplaceNoul();
        TxtDogumUnvan.Text = Dt._Rows("DogumUnvan");
        DListDogumD.SelectedValue = Dt._Rows("DogumTarixi").IntDateDay();
        DListDogumM.SelectedValue = Dt._RowsObject("DogumTarixi").IntDateMonth();
        DListDogumY.SelectedValue = Dt._RowsObject("DogumTarixi").IntDateYear();
        TxtSaglam.Text = Dt._Rows("SaglamliqVeziyyeti");
        TxtAtaAd.Text = Dt._Rows("AtaAd");
        TxtAtaSoyad.Text = Dt._Rows("AtaSoyad");
        DListAtaTevelludD.SelectedValue = Dt._RowsObject("AtaTevellud").IntDateDay();
        DListAtaTevelludM.SelectedValue = Dt._RowsObject("AtaTevellud").IntDateMonth();
        DListAtaTevelludY.SelectedValue = Dt._RowsObject("AtaTevellud").IntDateYear();
        DListAtaCountry.SelectedValue = Dt._Rows("AtaCountriesID").IsEmptyReplaceNoul();
        Regions(DListAtaCity, DListAtaCountry.SelectedValue);
        DListAtaCity.SelectedValue = Dt._Rows("AtaRegionsID").IsEmptyReplaceNoul();
        TxtAtaUnvan.Text = Dt._Rows("AtaUnvan");
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
        DALC.UsersHistoryInsert(40, 420, string.Format("Yetkinlik yaşına çatmayan qızın dünyaya körpə gətirdiyi üzrə məlumat silindi. № {0}", ID), _PersonsID);
    }

    protected void DlistCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        Regions(DlistDogumCity, DlistDogumCountry.SelectedValue);
    }

    protected void LnkCancel_Click(object sender, EventArgs e)
    {
        PnlEdit.Visible = false;
    }
    protected void DListAtaCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        Regions(DListAtaCity, DListAtaCountry.SelectedValue);
    }
}