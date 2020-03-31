using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class List_Default : Page
{
    public void ClearAnket()
    {
        // Əlavə etmə siyahısını təmizləyək
        DlistPassportTypeDs.SelectedValue = DlistPassportTypesOther.SelectedValue = DlistCity.SelectedValue = DlistCity2.SelectedValue = "0";
        DlistCountry.SelectedValue = "1";
        DListGender.SelectedValue = "-1";

        DlistDogumD.SelectedValue = DlistDogumM.SelectedValue = "00";
        DlistDogumY.SelectedValue = "1000";
        TxtDocNumberDs.Text = TxtDocNumberOther.Text = txtPin.Text = txtAd.Text = txtSoyad.Text = txtAta.Text = txtUnvan.Text = txtUnvan2.Text = "";
    }

    #region Soragcalar

    //Sorgacalar
    public void PasportTypes()
    {
        DataTable Dt = DALC.GetDocumentTypesChildren();

        DlistPassportTypeDs.DataSource = Dt;
        DlistPassportTypeDs.DataBind();
        DlistPassportTypeDs.Items.Insert(0, new ListItem("--", "0"));

        DataTable DtOther = DALC.GetDocumentTypesOther();

        DlistPassportTypesOther.DataSource = DtOther;
        DlistPassportTypesOther.DataBind();
        DlistPassportTypesOther.Items.Insert(0, new ListItem("--", "0"));
    }

    public void Aylar()
    {
        DlistDogumD.DataSource = DListFltrDogD.DataSource = DListFltrOlumD.DataSource = Config.NumericList(1, 31);
        DlistDogumD.DataBind();
        DListFltrDogD.DataBind();
        DListFltrOlumD.DataBind();

        DlistDogumM.DataSource = Config.MonthList();
        DlistDogumM.DataBind();
        DListFltrDogM.DataSource = DListFltrOlumM.DataSource = Config.MonthList(false);
        DListFltrDogM.DataBind();
        DListFltrOlumM.DataBind();
        DListFltrDogM.Items.Insert(0, new ListItem("--", "00"));
        DListFltrOlumM.Items.Insert(0, new ListItem("--", "00"));

        DlistDogumY.DataSource = DListFltrDogY.DataSource = DListFltrOlumY.DataSource = Config.NumericList(1980, DateTime.Now.Year, "1000");
        DlistDogumY.DataBind();
        DListFltrDogY.DataBind();
        DListFltrOlumY.DataBind();
    }

    public void Countries()
    {
        DataTable Dt = DALC.GetCountriesList();

        DlistCountry.DataSource = Dt;
        DlistCountry.DataBind();
        DlistCountry.Items.Insert(0, new ListItem("--", "0"));
        DlistCountry.SelectedValue = "1";

        DlistCountry2.DataSource = Dt;
        DlistCountry2.DataBind();

        //Yaşadığı ünvan daimi Azərbaycan olmalıdır.
        DlistCountry2.SelectedValue = "1";
        Regions(DlistCity2, DlistCountry2.SelectedValue);
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

    public void BindCitizenship()
    {
        DlistVetendasliq.DataSource = DALC.GetCitizenship();
        DlistVetendasliq.DataBind();
        DlistVetendasliq.Items.Insert(0, new ListItem("--", "0"));
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        //Login control
        if (DALC._GetUsersLogin == null)
        {
            Config.Redirect("/?return=" + Request.Url.ToString());
            return;
        }

        //Permissions Add
        PnlAdd.Visible = DALC._IsPermissionType(10);

        if (Config._GetQueryString("search") == "1")
        {
            PnlDetailSearch.Visible = true;
            PnlImgBtn.Visible = false;
        }

        if (!IsPostBack)
        {
            PnlContentRoot.Height = Unit.Pixel(700);

            // Yeni əlavədəki sorağçalar
            PasportTypes();
            Aylar();
            Countries();
            BindCitizenship();

            // Filterdəki sorağçalar
            DListFltrRegions.DataSource = DALC.GetRegionsListByPermission(DALC._GetUsersLogin.PermissionRegions.Trim(',').Trim());
            DListFltrRegions.DataBind();

            DlistStatus.DataSource = DALC.GetPersonsStatus();
            DlistStatus.DataBind();

            DListFltrDoctType_SelectedIndexChanged(null, null);

            if (DListFltrRegions.Items.Count > 1)
                DListFltrRegions.Items.Insert(0, new ListItem("--", "0"));

            LnkOtherChild_Click(null, null);
        }
    }

    protected void BtnFilter_Click(object sender, EventArgs e)
    {
        //Count cache clear
        Cache["ListCount"] = "";
        BtnFilter.CommandArgument = "1";

        LnkOtherChild.CommandArgument = "0";
        LnkOtherChild_Click(null, null);
    }

    protected void LnkOtherChild_Click(object sender, EventArgs e)
    {
        GrdList.DataSource = null;
        GrdList.DataBind();

        LnkOtherChild.Visible = false;
        LblCount.Text = "Axtarış üzrə: 0      Səhifə üzrə: 0";
        LnkOtherChild.CommandArgument = (int.Parse(LnkOtherChild.CommandArgument) + 20)._ToString();

        string Fin = "0";
        string Soyad = "0";
        string Ad = "0";
        string Ata = "0";
        string DocNumber = "0";
        if (TxtFltrFin.Text.Trim().Length > 0)
            Fin = TxtFltrFin.Text.Trim();

        if (TxtFltrSurname.Text.Trim().Length > 0)
            Soyad = TxtFltrSurname.Text.Trim();

        if (TxtFltrName.Text.Trim().Length > 0)
            Ad = TxtFltrName.Text.Trim();

        if (TxtFltrPatronymic.Text.Trim().Length > 0)
            Ata = TxtFltrPatronymic.Text.Trim();

        if (TxtFltrDoctNumber.Text.Trim().Length > 0)
            DocNumber = TxtFltrDoctNumber.Text.Trim();

        bool Cache = true;
        // Əgər filter olunubsa Cache-de saxlamayaq
        if (BtnFilter.CommandArgument == "1")
            Cache = false;

        DALC.DataTableResult PersonsList = DALC.GetPersonsList(int.Parse(LnkOtherChild.CommandArgument),
                                                                DListFltrDoctType.SelectedValue,
                                                                DocNumber,
                                                                Fin,
                                                                Soyad,
                                                                Ad,
                                                                Ata,
                                                                DListFltrRegions.SelectedValue,
                                                                DListFltrDogY.SelectedValue,
                                                                DListFltrDogM.SelectedValue,
                                                                DListFltrDogD.SelectedValue,
                                                                DListFltrGender.SelectedValue,
                                                                DListFltrIsOlum.SelectedValue,
                                                                DListFltrOlumY.SelectedValue,
                                                                DListFltrOlumM.SelectedValue,
                                                                DListFltrOlumD.SelectedValue,
                                                                DListFltrHimaye.SelectedValue,
                                                                DListFltrHimayeTeshkilatOrShex.SelectedValue,
                                                                DListFltrHimayeMehrumY.SelectedValue,
                                                                DListFltrHimayeMehrumM.SelectedValue,
                                                                DListFltrHimayeMehrumD.SelectedValue,
                                                                DListFltrAliment.SelectedValue,
                                                                DListFltrAlimentY.SelectedValue,
                                                                DListFltrAlimentM.SelectedValue,
                                                                DListFltrAlimentD.SelectedValue,
                                                                DListFltrMuavinat.SelectedValue,
                                                                DListFltrMuavinatY.SelectedValue,
                                                                DListFltrMuavinatM.SelectedValue,
                                                                DListFltrMuavinatD.SelectedValue,
                                                                DListFltrSehiyyeQeydiyyat.SelectedValue,
                                                                DListFltrSehiyyeQeydiyyatRegion.SelectedValue,
                                                                DListFltrSehiyyeMualice.SelectedValue,
                                                                DListFltrSehiyyeMualiceRegion.SelectedValue,
                                                                DListFltrSehiyyeMualiceY.SelectedValue,
                                                                DListFltrSehiyyeMualiceM.SelectedValue,
                                                                DListFltrSehiyyeMualiceD.SelectedValue,
                                                                DListFltrSehiyyeMualiceMonitoringY.SelectedValue,
                                                                DListFltrSehiyyeMualiceMonitoringM.SelectedValue,
                                                                DListFltrSehiyyeMualiceMonitoringD.SelectedValue,
                                                                DListFltrSagamligTarixce.SelectedValue,
                                                                DListFltrSagamligTarixceNov.SelectedValue,
                                                                DListFltrIdmanNailiyyet.SelectedValue,
                                                                DListFltrIdmanNailiyyetRegion.SelectedValue,
                                                                DListFltrIdmanNailiyyetNov.SelectedValue,
                                                                DListFltrIdmanNailiyyetY.SelectedValue,
                                                                DListFltrIdmanNailiyyetM.SelectedValue,
                                                                DListFltrIdmanNailiyyetD.SelectedValue,
                                                                DListFltrTehsil.SelectedValue,
                                                                DListFltrTehsilRegion.SelectedValue,
                                                                DListFltrTehsilYer.SelectedValue,
                                                                DListFltrTehsilNailiyyet.SelectedValue,
                                                                DListFltrTehsilNailiyyetRegion.SelectedValue,
                                                                DListFltrTehsilNailiyyetNov.SelectedValue,
                                                                DListFltrTehsilNailiyyetY.SelectedValue,
                                                                DListFltrTehsilNailiyyetM.SelectedValue,
                                                                DListFltrTehsilNailiyyetD.SelectedValue,
                                                                DListFltrDunyayaKorpe.SelectedValue,
                                                                DListFltrDunyayaKorpeRegion.SelectedValue,
                                                                DListFltrDunyayaKorpeY.SelectedValue,
                                                                DListFltrDunyayaKorpeM.SelectedValue,
                                                                DListFltrDunyayaKorpeD.SelectedValue,
                                                                DListFltrHuquqPozmaQarsi.SelectedValue,
                                                                TxtFltrHuquqPozmaQarsiMadde.Text.Trim().IsEmptyReplaceNoul(),
                                                                DListFltrHuquqPozmaQarsiY.SelectedValue,
                                                                DListFltrHuquqPozmaQarsiM.SelectedValue,
                                                                DListFltrHuquqPozmaQarsiD.SelectedValue,
                                                                DListFltrHuquqPozma.SelectedValue,
                                                                DListFltrHuquqPozmaMecelle.SelectedValue,
                                                                DListFltrHuquqPozmaCezaNov.SelectedValue,
                                                                DListFltrHuquqPozmaRegion.SelectedValue,
                                                                DListFltrHuquqPozmaMonitoringY.SelectedValue,
                                                                DListFltrHuquqPozmaMonitoringM.SelectedValue,
                                                                DListFltrHuquqPozmaMonitoringD.SelectedValue,
                                                                DListFltrHuquqPozmaY.SelectedValue,
                                                                DListFltrHuquqPozmaM.SelectedValue,
                                                                DListFltrHuquqPozmaD.SelectedValue,
                                                                DListFltrTerbiyeviTedbir.SelectedValue,
                                                                DListFltrTerbiyeviTedbirRegion.SelectedValue,
                                                                DListFltrTerbiyeviTedbirY.SelectedValue,
                                                                DListFltrTerbiyeviTedbirM.SelectedValue,
                                                                DListFltrTerbiyeviTedbirD.SelectedValue,
                                                                DListFltrTerbiyeviTedbirMonitoringY.SelectedValue,
                                                                DListFltrTerbiyeviTedbirMonitoringM.SelectedValue,
                                                                DListFltrTerbiyeviTedbirMonitoringD.SelectedValue,
                                                                DListFltrMeshgulluq.SelectedValue,
                                                                DListFltrMeshQeydY.SelectedValue,
                                                                DListFltrMeshQeydM.SelectedValue,
                                                                DListFltrMeshQeydD.SelectedValue,
                                                                DListFltrMeshCixmaY.SelectedValue,
                                                                DListFltrMeshCixmaM.SelectedValue,
                                                                DListFltrMeshCixmaD.SelectedValue,
                                                                DListFltrSosialYardım.SelectedValue,
                                                                DListFltrSosialYardımY.SelectedValue,
                                                                DListFltrSosialYardımM.SelectedValue,
                                                                DListFltrSosialYardımD.SelectedValue,
                                                                DListFltrSosialXidmet.SelectedValue,
                                                                DListFltrSosialXidmetNov.SelectedValue,
                                                                DListFltrSosialXidmetY.SelectedValue,
                                                                DListFltrSosialXidmetM.SelectedValue,
                                                                DListFltrSosialXidmetD.SelectedValue,
                                                                DListFltrAsudeDusherge.SelectedValue,
                                                                DListFltrAsudeDushergeRegion.SelectedValue,
                                                                DListFltrAsudeDushergeGeldiyiY.SelectedValue,
                                                                DListFltrAsudeDushergeGeldiyiM.SelectedValue,
                                                                DListFltrAsudeDushergeGeldiyiD.SelectedValue,
                                                                DListFltrAsudeDushergeGetdiyiY.SelectedValue,
                                                                DListFltrAsudeDushergeGetdiyiM.SelectedValue,
                                                                DListFltrAsudeDushergeGetdiyiD.SelectedValue,
                                                                DlistStatus.SelectedValue,
                                                                Cache);

        if (PersonsList.Count == -1)
        {
            Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
            return;
        }

        GrdList.DataSource = PersonsList.Dt;
        GrdList.DataBind();

        LblCount.Text = "Axtarış üzrə: " + PersonsList.Count + "      Səhifə üzrə: " + GrdList.Rows.Count._ToString();

        if (PersonsList.Dt.Rows.Count > 0)
            LnkOtherChild.Visible = (GrdList.Rows.Count < PersonsList.Count);
        BtnFilter.CommandArgument = "";
    }

    protected void LnkAdd_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "JqueryModal", "$('#myModal').modal('show');", true);
        DlistCountry_SelectedIndexChanged(null, null);
        ClearAnket();
    }

    protected void BtnSavePersons_Click(object sender, EventArgs e)
    {
        //Hərehtimala yoxluyaq burdada.
        if (!DALC._IsPermissionType(10))
        {
            Config.MsgBoxAjax("Məlumat əlavə etmə icazəniz yoxdur.", Page);
            return;
        }

        if (TxtDocNumberDs.Text.Trim().Length < 1 && TxtDocNumberOther.Text.Trim().Length < 1)
        {
            Config.MsgBoxAjax("Doğum şəhadətnaməsinin nömrəsi və ya digər sənəd nömrəsi daxil edilməlidir.", Page);
            return;
        }

        if (TxtDocNumberDs.Text.Trim().Length > 0 && DlistPassportTypeDs.SelectedValue == "0")
        {
            Config.MsgBoxAjax("Doğum şəhadətnaməsinin seriyasını seçin.", Page);
            return;
        }

        if (TxtDocNumberOther.Text.Trim().Length > 0 && DlistPassportTypesOther.SelectedValue == "0")
        {
            Config.MsgBoxAjax("Digər sənəd növünü seçin.", Page);
            return;
        }

        //Ədliyyə Nazirliyi dedi rəqəm olmamalıdı, başlığında X də olanlar var.
        //if (TxtDocNumberDs.Text.Trim().Length > 0 && !TxtDocNumberDs.Text.IsNumeric())
        //{
        //    Config.MsgBoxAjax("Doğum şəhadətnaməsinin nömrəsi rəqəm tipində olmalıdır.", Page);
        //    return;
        //}

        //Ancaq AZE, MYİ, DYİ də rəqəm kontrol.
        if (DlistPassportTypesOther.SelectedValue == "10" || DlistPassportTypesOther.SelectedValue == "50" || DlistPassportTypesOther.SelectedValue == "60")
        {
            if (TxtDocNumberOther.Text.Trim().Length > 0 && !TxtDocNumberOther.Text.IsNumeric())
            {
                Config.MsgBoxAjax("Sənəd nömrəsi rəqəm tipində olmalıdır.", Page);
                return;
            }
        }

        if (txtPin.Text.Trim().Length < 5)
        {
            Config.MsgBoxAjax("Uşağa məxsus FİN minimum 5 simvoldan ibarət olmalıdır.", Page);
            return;
        }

        if (txtAd.Text.Trim().Length < 3)
        {
            Config.MsgBoxAjax("Uşağın adı mütləq qeyd olunmalıdır.", Page);
            return;
        }

        if (txtSoyad.Text.Trim().Length < 3)
        {
            Config.MsgBoxAjax("Uşağın soyadı mütləq qeyd olunmalıdır.", Page);
            return;
        }

        if (DListGender.SelectedValue == "0")
        {
            Config.MsgBoxAjax("Uşağın cinsi mütləq qeyd olunmalıdır.", Page);
            return;
        }

        if (DlistDogumY.SelectedValue == "1000")
        {
            Config.MsgBoxAjax("Doğulduğu il mütləq qeyd olunmalıdır.", Page);
            return;
        }

        //FİN üzrə bazada məlumat varmı?
        string Check = DALC.GetDbSingleValuesParams("Count(*)", "Persons", "FIN", txtPin.Text.Trim(), "", "-1");

        if (Check == "-1")
        {
            Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
            return;
        }

        if (Check != "0")
        {
            Config.MsgBoxAjax("Daxil edilmiş FİN üzrə bazada məlumat mövcuddur.", Page);
            return;
        }

        Dictionary<string, object> Dictionary = new Dictionary<string, object>
        {
            { "OrganizationsID", DALC._GetUsersLogin.OrganizationsID },
            { "UsersID", DALC._GetUsersLogin.ID },
            { "DsDocumentTypesID", DlistPassportTypeDs.SelectedValue.NullConvert() },
            { "DsNomresi", TxtDocNumberDs.Text.Trim().NullConvert() },
            { "DocumentTypesID", DlistPassportTypesOther.SelectedValue.NullConvert() },
            { "DocumentNumber", TxtDocNumberOther.Text.Trim().NullConvert() },
            { "FIN", txtPin.Text.Trim() },
            { "Soyad", txtSoyad.Text.Trim() },
            { "Ad", txtAd.Text.Trim() },
            { "Ata", txtAta.Text.Trim() },
            { "CitizenshipID", DlistVetendasliq.SelectedValue.NullConvert() },
            { "Gender", DListGender.SelectedValue.NullConvert() },
            { "DogumTarixi", (DlistDogumY.SelectedValue + DlistDogumM.SelectedValue + DlistDogumD.SelectedValue).NullConvert() },
            { "DogumQeydiyyatYeriCountriesID", DlistCountry.SelectedValue.NullConvert() },
            { "DogumQeydiyyatYeriRegionsID", DlistCity.SelectedValue.NullConvert() },
            { "DogumQeydiyyatYeriUnvan", txtUnvan.Text.Trim().NullConvert() },
            { "YasadigiUnvanCountriesID", DlistCountry2.SelectedValue.NullConvert() },
            { "YasadigiUnvanRegionsID", DlistCity2.SelectedValue.NullConvert() },
            { "YasadigiUnvan", txtUnvan2.Text.Trim().NullConvert() },
            { "Last_Dt", DateTime.Now },
            { "Last_Ip", HttpContext.Current.Request.UserHostAddress.IPToInteger() }
        };

        int ChekInsert = DALC.InsertDatabase("Persons", Dictionary);
        if (ChekInsert < 1)
        {
            Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
            return;
        }

        DALC.UsersHistoryInsert(20, 10, "Məlumat bazasına yeni uşaq əlavə edildi. № " + ChekInsert._ToString(), ChekInsert);
        ClearAnket();

        //Count cache clear
        Cache.Remove("ListCount");

        Config.MsgBoxAjax("Əməliyyat uğurla yerinə yetirildi.", Page, true);
        return;
    }

    protected void DlistCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        Regions(DlistCity, DlistCountry.SelectedValue);
    }

    protected void BtnClear_Click(object sender, EventArgs e)
    {
        ClearAnket();
    }

    protected void LnkHistory_Click(object sender, EventArgs e)
    {
        Config.Redirect("/history/?i=" + (sender as LinkButton).CommandArgument);
    }

    protected void BtnFilterClear_Click(object sender, EventArgs e)
    {
        TxtFltrDoctNumber.Text = TxtFltrFin.Text = TxtFltrSurname.Text = TxtFltrName.Text = TxtFltrPatronymic.Text = "";
        DListFltrDoctType.SelectedValue = DListFltrHimaye.SelectedValue = "0";
        if (DListFltrRegions.Items.Count > 0)
        {
            DListFltrRegions.SelectedIndex = 0;
        }

        DListFltrDogD.SelectedValue = DListFltrDogM.SelectedValue = "00";
        DListFltrDogY.SelectedValue = "1000";
        DListFltrGender.SelectedValue = "-1";

        DListFltrIsOlum.SelectedValue = "-1";
        DListFltrIsOlum_SelectedIndexChanged(null, null);

        DListFltrHimaye.SelectedValue = "0";
        DListFltrHimaye_SelectedIndexChanged(null, null);

        DListFltrAliment.SelectedValue = "0";
        DListFltrAliment_SelectedIndexChanged(null, null);

        DListFltrMuavinat.SelectedValue = "0";
        DListFltrMuavinat_SelectedIndexChanged(null, null);

        DListFltrSehiyyeQeydiyyat.SelectedValue = "0";
        DListFltrSehiyyeQeydiyyat_SelectedIndexChanged(null, null);

        DListFltrSehiyyeMualice.SelectedValue = "0";
        DListFltrSehiyyeMualice_SelectedIndexChanged(null, null);

        DListFltrSagamligTarixce.SelectedValue = "0";
        DListFltrSagamligTarixce_SelectedIndexChanged(null, null);

        DListFltrIdmanNailiyyet.SelectedValue = "0";
        DListFltrIdmanNailiyyet_SelectedIndexChanged(null, null);

        DListFltrTehsil.SelectedValue = "0";
        DListFltrTehsil_SelectedIndexChanged(null, null);

        DListFltrTehsilNailiyyet.SelectedValue = "0";
        DListFltrTehsilNailiyyet_SelectedIndexChanged(null, null);

        DListFltrDunyayaKorpe.SelectedValue = "0";
        DListFltrDunyayaKorpe_SelectedIndexChanged(null, null);

        DListFltrHuquqPozmaQarsi.SelectedValue = "0";
        DListFltrHuquqPozmaQarsi_SelectedIndexChanged(null, null);

        DListFltrHuquqPozma.SelectedValue = "0";
        DListFltrHuquqPozma_SelectedIndexChanged(null, null);

        DListFltrTerbiyeviTedbir.SelectedValue = "0";
        DListFltrTerbiyeviTedbir_SelectedIndexChanged(null, null);

        DListFltrMeshgulluq.SelectedValue = "0";
        DListFltrMeshgulluq_SelectedIndexChanged(null, null);

        DListFltrSosialYardım.SelectedValue = "0";
        DListFltrSosialYardım_SelectedIndexChanged(null, null);

        DListFltrSosialXidmet.SelectedValue = "0";
        DListFltrSosialXidmet_SelectedIndexChanged(null, null);

        DListFltrAsudeDusherge.SelectedValue = "0";
        DListFltrAsudeDusherge_SelectedIndexChanged(null, null);

    }

    protected void LnkDetailFilter_Click(object sender, EventArgs e)
    {
        PnlDetailSearch.Visible = true;
        PnlImgBtn.Visible = false;
    }

    protected void DListFltrDoctType_SelectedIndexChanged(object sender, EventArgs e)
    {
        TxtFltrDoctNumber.Enabled = true;
        TxtFltrDoctNumber.Text = "";
        if (DListFltrDoctType.SelectedValue == "0")
            TxtFltrDoctNumber.Enabled = false;
    }

    protected void DListFltrIsOlum_SelectedIndexChanged(object sender, EventArgs e)
    {
        MltOlum.ActiveViewIndex = -1;

        if (DListFltrIsOlum.Items.Count > 0)
        {
            DListFltrOlumD.SelectedValue = DListFltrOlumM.SelectedValue = "00";
            DListFltrOlumY.SelectedValue = "1000";
        }

        if (DListFltrIsOlum.SelectedValue == "1")
        {
            if (DListFltrIsOlum.Items.Count < 1)
            {
                DListFltrOlumD.DataSource = Config.NumericList(1, 31);
                DListFltrOlumD.DataBind();

                DListFltrOlumM.DataSource = Config.MonthList(false);
                DListFltrOlumM.DataBind();
                DListFltrOlumM.Items.Insert(0, new ListItem("--", "00"));

                DListFltrOlumY.DataSource = Config.NumericList(1980, DateTime.Now.Year, "1000");
                DListFltrOlumY.DataBind();
            }
            MltOlum.ActiveViewIndex = 0;
        }
    }

    protected void DListFltrHimaye_SelectedIndexChanged(object sender, EventArgs e)
    {
        MltHimaye.ActiveViewIndex = -1;

        if (DListFltrHimayeMehrumD.Items.Count > 0)
        {

            DListFltrHimayeMehrumD.SelectedValue = DListFltrHimayeMehrumM.SelectedValue = "00";
            DListFltrHimayeMehrumY.SelectedValue = "1000";
        }

        if (DListFltrHimaye.SelectedValue == "1")
        {
            if (DListFltrHimayeMehrumD.Items.Count < 1)
            {

                DListFltrHimayeMehrumD.DataSource = Config.NumericList(1, 31);
                DListFltrHimayeMehrumD.DataBind();

                DListFltrHimayeMehrumM.DataSource = Config.MonthList(false);
                DListFltrHimayeMehrumM.DataBind();
                DListFltrHimayeMehrumM.Items.Insert(0, new ListItem("--", "00"));

                DListFltrHimayeMehrumY.DataSource = Config.NumericList(1980, DateTime.Now.Year, "1000");
                DListFltrHimayeMehrumY.DataBind();

            }

            MltHimaye.ActiveViewIndex = 0;
        }
    }

    protected void DListFltrAliment_SelectedIndexChanged(object sender, EventArgs e)
    {
        MltAliment.ActiveViewIndex = -1;

        if (DListFltrAlimentD.Items.Count > 0)
        {

            DListFltrAlimentD.SelectedValue = DListFltrAlimentM.SelectedValue = "00";
            DListFltrAlimentY.SelectedValue = "1000";
        }

        if (DListFltrAliment.SelectedValue == "1")
        {
            if (DListFltrAlimentD.Items.Count < 1)
            {

                DListFltrAlimentD.DataSource = Config.NumericList(1, 31);
                DListFltrAlimentD.DataBind();

                DListFltrAlimentM.DataSource = Config.MonthList(false);
                DListFltrAlimentM.DataBind();
                DListFltrAlimentM.Items.Insert(0, new ListItem("--", "00"));

                DListFltrAlimentY.DataSource = Config.NumericList(1980, DateTime.Now.Year, "1000");
                DListFltrAlimentY.DataBind();

            }

            MltAliment.ActiveViewIndex = 0;
        }
    }

    protected void DListFltrMuavinat_SelectedIndexChanged(object sender, EventArgs e)
    {
        MltMuavinat.ActiveViewIndex = -1;

        if (DListFltrMuavinatD.Items.Count > 0)
        {

            DListFltrMuavinatD.SelectedValue = DListFltrMuavinatM.SelectedValue = "00";
            DListFltrMuavinatY.SelectedValue = "1000";
        }

        if (DListFltrMuavinat.SelectedValue == "1")
        {
            if (DListFltrMuavinatD.Items.Count < 1)
            {

                DListFltrMuavinatD.DataSource = Config.NumericList(1, 31);
                DListFltrMuavinatD.DataBind();

                DListFltrMuavinatM.DataSource = Config.MonthList(false);
                DListFltrMuavinatM.DataBind();
                DListFltrMuavinatM.Items.Insert(0, new ListItem("--", "00"));

                DListFltrMuavinatY.DataSource = Config.NumericList(1980, DateTime.Now.Year, "1000");
                DListFltrMuavinatY.DataBind();

            }

            MltMuavinat.ActiveViewIndex = 0;
        }
    }

    protected void DListFltrSehiyyeQeydiyyat_SelectedIndexChanged(object sender, EventArgs e)
    {
        MltSehiyyeQeydiyyat.ActiveViewIndex = -1;

        if (DListFltrSehiyyeQeydiyyatRegion.Items.Count > 0)
        {
            DListFltrSehiyyeQeydiyyatRegion.SelectedValue = "0";
        }

        if (DListFltrSehiyyeQeydiyyat.SelectedValue == "1")
        {
            if (DListFltrSehiyyeQeydiyyatRegion.Items.Count < 1)
            {
                DListFltrSehiyyeQeydiyyatRegion.DataSource = DALC.GetRegionsList();
                DListFltrSehiyyeQeydiyyatRegion.DataBind();
                DListFltrSehiyyeQeydiyyatRegion.Items.Insert(0, new ListItem("--", "0"));

            }

            MltSehiyyeQeydiyyat.ActiveViewIndex = 0;
        }
    }

    protected void DListFltrSehiyyeMualice_SelectedIndexChanged(object sender, EventArgs e)
    {
        MltSehiyyeMualice.ActiveViewIndex = -1;

        if (DListFltrSehiyyeMualiceRegion.Items.Count > 0)
        {
            DListFltrSehiyyeMualiceRegion.SelectedValue = "0";
            DListFltrSehiyyeMualiceD.SelectedValue = DListFltrSehiyyeMualiceM.SelectedValue = DListFltrSehiyyeMualiceMonitoringD.SelectedValue = DListFltrSehiyyeMualiceMonitoringM.SelectedValue = "00";
            DListFltrSehiyyeMualiceY.SelectedValue = DListFltrSehiyyeMualiceMonitoringY.SelectedValue = "1000";
        }

        if (DListFltrSehiyyeMualice.SelectedValue == "1")
        {
            if (DListFltrSehiyyeMualiceRegion.Items.Count < 1)
            {
                DListFltrSehiyyeMualiceRegion.DataSource = DALC.GetRegionsList();
                DListFltrSehiyyeMualiceRegion.DataBind();
                DListFltrSehiyyeMualiceRegion.Items.Insert(0, new ListItem("--", "0"));

                DListFltrSehiyyeMualiceD.DataSource = DListFltrSehiyyeMualiceMonitoringD.DataSource = Config.NumericList(1, 31);
                DListFltrSehiyyeMualiceD.DataBind();
                DListFltrSehiyyeMualiceMonitoringD.DataBind();

                DListFltrSehiyyeMualiceM.DataSource = DListFltrSehiyyeMualiceMonitoringM.DataSource = Config.MonthList(false);
                DListFltrSehiyyeMualiceM.DataBind();
                DListFltrSehiyyeMualiceMonitoringM.DataBind();
                DListFltrSehiyyeMualiceM.Items.Insert(0, new ListItem("--", "00"));
                DListFltrSehiyyeMualiceMonitoringM.Items.Insert(0, new ListItem("--", "00"));

                DListFltrSehiyyeMualiceY.DataSource = DListFltrSehiyyeMualiceMonitoringY.DataSource = Config.NumericList(1980, DateTime.Now.Year, "1000");
                DListFltrSehiyyeMualiceY.DataBind();
                DListFltrSehiyyeMualiceMonitoringY.DataBind();
            }

            MltSehiyyeMualice.ActiveViewIndex = 0;
        }
    }

    protected void DListFltrSagamligTarixce_SelectedIndexChanged(object sender, EventArgs e)
    {
        MltSagamligTarixce.ActiveViewIndex = -1;

        if (DListFltrSagamligTarixceNov.Items.Count > 0)
        {
            DListFltrSagamligTarixceNov.SelectedValue = "0";
        }

        if (DListFltrSagamligTarixce.SelectedValue == "1")
        {
            if (DListFltrSagamligTarixceNov.Items.Count < 1)
            {
                DListFltrSagamligTarixceNov.DataSource = DALC.GetSoragcaByTableName("PersonsSehiyyeTarixceNov");
                DListFltrSagamligTarixceNov.DataBind();
                DListFltrSagamligTarixceNov.Items.Insert(0, new ListItem("--", "0"));

            }

            MltSagamligTarixce.ActiveViewIndex = 0;
        }
    }

    protected void DListFltrIdmanNailiyyet_SelectedIndexChanged(object sender, EventArgs e)
    {
        MltIdmanNailiyyet.ActiveViewIndex = -1;

        if (DListFltrIdmanNailiyyetRegion.Items.Count > 0)
        {
            DListFltrIdmanNailiyyetRegion.SelectedValue = "0";
            DListFltrIdmanNailiyyetNov.SelectedValue = "0";
            DListFltrIdmanNailiyyetD.SelectedValue = DListFltrIdmanNailiyyetM.SelectedValue = "00";
            DListFltrIdmanNailiyyetY.SelectedValue = "1000";
        }

        if (DListFltrIdmanNailiyyet.SelectedValue == "1")
        {
            if (DListFltrIdmanNailiyyetRegion.Items.Count < 1)
            {
                DListFltrIdmanNailiyyetRegion.DataSource = DALC.GetRegionsList();
                DListFltrIdmanNailiyyetRegion.DataBind();
                DListFltrIdmanNailiyyetRegion.Items.Insert(0, new ListItem("--", "0"));

                DListFltrIdmanNailiyyetNov.DataSource = DALC.GetSoragcaByTableName("PersonsIdmanNov");
                DListFltrIdmanNailiyyetNov.DataBind();
                DListFltrIdmanNailiyyetNov.Items.Insert(0, new ListItem("--", "0"));

                DListFltrIdmanNailiyyetD.DataSource = Config.NumericList(1, 31);
                DListFltrIdmanNailiyyetD.DataBind();

                DListFltrIdmanNailiyyetM.DataSource = Config.MonthList(false);
                DListFltrIdmanNailiyyetM.DataBind();
                DListFltrIdmanNailiyyetM.Items.Insert(0, new ListItem("--", "00"));

                DListFltrIdmanNailiyyetY.DataSource = Config.NumericList(1980, DateTime.Now.Year, "1000");
                DListFltrIdmanNailiyyetY.DataBind();
            }

            MltIdmanNailiyyet.ActiveViewIndex = 0;
        }
    }

    protected void DListFltrTehsilNailiyyet_SelectedIndexChanged(object sender, EventArgs e)
    {
        MltTehsilNailiyyet.ActiveViewIndex = -1;

        if (DListFltrTehsilNailiyyetRegion.Items.Count > 0)
        {
            DListFltrTehsilNailiyyetRegion.SelectedValue = "0";
            DListFltrTehsilNailiyyetNov.SelectedValue = "0";
            DListFltrTehsilNailiyyetD.SelectedValue = DListFltrTehsilNailiyyetM.SelectedValue = "00";
            DListFltrTehsilNailiyyetY.SelectedValue = "1000";
        }

        if (DListFltrTehsilNailiyyet.SelectedValue == "1")
        {
            if (DListFltrTehsilNailiyyetRegion.Items.Count < 1)
            {
                DListFltrTehsilNailiyyetRegion.DataSource = DALC.GetRegionsList();
                DListFltrTehsilNailiyyetRegion.DataBind();
                DListFltrTehsilNailiyyetRegion.Items.Insert(0, new ListItem("--", "0"));

                DListFltrTehsilNailiyyetNov.DataSource = DALC.GetSoragcaByTableName("PersonsTehsilFennNov");
                DListFltrTehsilNailiyyetNov.DataBind();
                DListFltrTehsilNailiyyetNov.Items.Insert(0, new ListItem("--", "0"));

                DListFltrTehsilNailiyyetD.DataSource = Config.NumericList(1, 31);
                DListFltrTehsilNailiyyetD.DataBind();

                DListFltrTehsilNailiyyetM.DataSource = Config.MonthList(false);
                DListFltrTehsilNailiyyetM.DataBind();
                DListFltrTehsilNailiyyetM.Items.Insert(0, new ListItem("--", "00"));

                DListFltrTehsilNailiyyetY.DataSource = Config.NumericList(1980, DateTime.Now.Year, "1000");
                DListFltrTehsilNailiyyetY.DataBind();
            }

            MltTehsilNailiyyet.ActiveViewIndex = 0;
        }
    }

    protected void DListFltrTehsil_SelectedIndexChanged(object sender, EventArgs e)
    {
        MltTehsil.ActiveViewIndex = -1;

        if (DListFltrTehsilRegion.Items.Count > 0)
        {
            DListFltrTehsilRegion.SelectedValue = "0";
            DListFltrTehsilYer.SelectedValue = "0";
        }

        if (DListFltrTehsil.SelectedValue == "1")
        {
            if (DListFltrTehsilRegion.Items.Count < 1)
            {
                DListFltrTehsilRegion.DataSource = DALC.GetRegionsList();
                DListFltrTehsilRegion.DataBind();
                DListFltrTehsilRegion.Items.Insert(0, new ListItem("--", "0"));
            }

            MltTehsil.ActiveViewIndex = 0;
        }
    }

    protected void DListFltrDunyayaKorpe_SelectedIndexChanged(object sender, EventArgs e)
    {
        MltDunyayaKorpe.ActiveViewIndex = -1;

        if (DListFltrDunyayaKorpeRegion.Items.Count > 0)
        {
            DListFltrDunyayaKorpeRegion.SelectedValue = "0";
            DListFltrDunyayaKorpeD.SelectedValue = DListFltrDunyayaKorpeM.SelectedValue = "00";
            DListFltrDunyayaKorpeY.SelectedValue = "1000";
        }

        if (DListFltrDunyayaKorpe.SelectedValue == "1")
        {
            if (DListFltrDunyayaKorpeRegion.Items.Count < 1)
            {
                DListFltrDunyayaKorpeRegion.DataSource = DALC.GetRegionsList();
                DListFltrDunyayaKorpeRegion.DataBind();
                DListFltrDunyayaKorpeRegion.Items.Insert(0, new ListItem("--", "0"));

                DListFltrDunyayaKorpeD.DataSource = Config.NumericList(1, 31);
                DListFltrDunyayaKorpeD.DataBind();

                DListFltrDunyayaKorpeM.DataSource = Config.MonthList(false);
                DListFltrDunyayaKorpeM.DataBind(); ;
                DListFltrDunyayaKorpeM.Items.Insert(0, new ListItem("--", "00"));

                DListFltrDunyayaKorpeY.DataSource = Config.NumericList(1980, DateTime.Now.Year, "1000");
                DListFltrDunyayaKorpeY.DataBind();

            }

            MltDunyayaKorpe.ActiveViewIndex = 0;
        }
    }

    protected void DListFltrHuquqPozmaQarsi_SelectedIndexChanged(object sender, EventArgs e)
    {
        MltHuquqPozmaQarsi.ActiveViewIndex = -1;

        TxtFltrHuquqPozmaQarsiMadde.Text = "";
        if (DListFltrHuquqPozmaQarsiD.Items.Count > 0)
        {
            DListFltrHuquqPozmaQarsiD.SelectedValue = DListFltrHuquqPozmaQarsiM.SelectedValue = "00";
            DListFltrHuquqPozmaQarsiY.SelectedValue = "1000";
        }

        if (DListFltrHuquqPozmaQarsi.SelectedValue == "1")
        {
            if (DListFltrHuquqPozmaQarsiD.Items.Count < 1)
            {
                DListFltrHuquqPozmaQarsiD.DataSource = Config.NumericList(1, 31);
                DListFltrHuquqPozmaQarsiD.DataBind();

                DListFltrHuquqPozmaQarsiM.DataSource = Config.MonthList(false);
                DListFltrHuquqPozmaQarsiM.DataBind(); ;
                DListFltrHuquqPozmaQarsiM.Items.Insert(0, new ListItem("--", "00"));

                DListFltrHuquqPozmaQarsiY.DataSource = Config.NumericList(1980, DateTime.Now.Year, "1000");
                DListFltrHuquqPozmaQarsiY.DataBind();
            }
            MltHuquqPozmaQarsi.ActiveViewIndex = 0;
        }
    }

    protected void DListFltrTerbiyeviTedbir_SelectedIndexChanged(object sender, EventArgs e)
    {
        MltTerbiyeviTedbir.ActiveViewIndex = -1;

        if (DListFltrTerbiyeviTedbirRegion.Items.Count > 0)
        {
            DListFltrTerbiyeviTedbirRegion.SelectedValue = "0";
            DListFltrTerbiyeviTedbirD.SelectedValue = DListFltrTerbiyeviTedbirM.SelectedValue = DListFltrTerbiyeviTedbirMonitoringD.SelectedValue = DListFltrTerbiyeviTedbirMonitoringM.SelectedValue = "00";
            DListFltrTerbiyeviTedbirY.SelectedValue = DListFltrTerbiyeviTedbirMonitoringY.SelectedValue = "1000";
        }

        if (DListFltrTerbiyeviTedbir.SelectedValue == "1")
        {
            if (DListFltrTerbiyeviTedbirRegion.Items.Count < 1)
            {
                DListFltrTerbiyeviTedbirRegion.DataSource = DALC.GetRegionsList();
                DListFltrTerbiyeviTedbirRegion.DataBind();
                DListFltrTerbiyeviTedbirRegion.Items.Insert(0, new ListItem("--", "0"));

                DListFltrTerbiyeviTedbirD.DataSource = DListFltrTerbiyeviTedbirMonitoringD.DataSource = Config.NumericList(1, 31);
                DListFltrTerbiyeviTedbirD.DataBind();
                DListFltrTerbiyeviTedbirMonitoringD.DataBind();

                DListFltrTerbiyeviTedbirM.DataSource = DListFltrTerbiyeviTedbirMonitoringM.DataSource = Config.MonthList(false);
                DListFltrTerbiyeviTedbirM.DataBind();
                DListFltrTerbiyeviTedbirMonitoringM.DataBind();
                DListFltrTerbiyeviTedbirM.Items.Insert(0, new ListItem("--", "00"));
                DListFltrTerbiyeviTedbirMonitoringM.Items.Insert(0, new ListItem("--", "00"));

                DListFltrTerbiyeviTedbirY.DataSource = DListFltrTerbiyeviTedbirMonitoringY.DataSource = Config.NumericList(1980, DateTime.Now.Year, "1000");
                DListFltrTerbiyeviTedbirY.DataBind();
                DListFltrTerbiyeviTedbirMonitoringY.DataBind();
            }

            MltTerbiyeviTedbir.ActiveViewIndex = 0;
        }
    }

    protected void DListFltrHuquqPozma_SelectedIndexChanged(object sender, EventArgs e)
    {
        MltHuquqPozma.ActiveViewIndex = -1;

        if (DListFltrHuquqPozmaRegion.Items.Count > 0)
        {
            DListFltrHuquqPozmaMecelle.SelectedValue = DListFltrHuquqPozmaCezaNov.SelectedValue = DListFltrHuquqPozmaRegion.SelectedValue = "0";
            DListFltrHuquqPozmaD.SelectedValue = DListFltrHuquqPozmaM.SelectedValue = DListFltrHuquqPozmaMonitoringD.SelectedValue = DListFltrHuquqPozmaMonitoringM.SelectedValue = "00";
            DListFltrHuquqPozmaY.SelectedValue = DListFltrHuquqPozmaMonitoringY.SelectedValue = "1000";
        }

        if (DListFltrHuquqPozma.SelectedValue == "1")
        {
            if (DListFltrHuquqPozmaRegion.Items.Count < 1)
            {
                DListFltrHuquqPozmaMecelle.DataSource = DALC.GetSoragcaByTableName("PersonsHuquqpozmaMecelleNov");
                DListFltrHuquqPozmaMecelle.DataBind();
                DListFltrHuquqPozmaMecelle.Items.Insert(0, new ListItem("--", "0"));

                DListFltrHuquqPozmaCezaNov.DataSource = DALC.GetSoragcaByTableName("PersonsHuquqpozmaCezaNov");
                DListFltrHuquqPozmaCezaNov.DataBind();
                DListFltrHuquqPozmaCezaNov.Items.Insert(0, new ListItem("--", "0"));

                DListFltrHuquqPozmaRegion.DataSource = DALC.GetRegionsList();
                DListFltrHuquqPozmaRegion.DataBind();
                DListFltrHuquqPozmaRegion.Items.Insert(0, new ListItem("--", "0"));

                DListFltrHuquqPozmaD.DataSource = DListFltrHuquqPozmaMonitoringD.DataSource = Config.NumericList(1, 31);
                DListFltrHuquqPozmaD.DataBind();
                DListFltrHuquqPozmaMonitoringD.DataBind();

                DListFltrHuquqPozmaM.DataSource = DListFltrHuquqPozmaMonitoringM.DataSource = Config.MonthList(false);
                DListFltrHuquqPozmaM.DataBind();
                DListFltrHuquqPozmaMonitoringM.DataBind();
                DListFltrHuquqPozmaM.Items.Insert(0, new ListItem("--", "00"));
                DListFltrHuquqPozmaMonitoringM.Items.Insert(0, new ListItem("--", "00"));

                DListFltrHuquqPozmaY.DataSource = DListFltrHuquqPozmaMonitoringY.DataSource = Config.NumericList(1980, DateTime.Now.Year, "1000");
                DListFltrHuquqPozmaY.DataBind();
                DListFltrHuquqPozmaMonitoringY.DataBind();
            }

            MltHuquqPozma.ActiveViewIndex = 0;
        }
    }

    protected void DListFltrMeshgulluq_SelectedIndexChanged(object sender, EventArgs e)
    {
        MltMeshgulluq.ActiveViewIndex = -1;

        if (DListFltrMeshQeydD.Items.Count > 0)
        {

            DListFltrMeshQeydD.SelectedValue = DListFltrMeshQeydM.SelectedValue = DListFltrMeshCixmaD.SelectedValue = DListFltrMeshCixmaM.SelectedValue = "00";
            DListFltrMeshQeydY.SelectedValue = DListFltrMeshCixmaY.SelectedValue = "1000";
        }

        if (DListFltrMeshgulluq.SelectedValue != "0")
        {
            if (DListFltrMeshQeydD.Items.Count < 1)
            {

                DListFltrMeshQeydD.DataSource = DListFltrMeshCixmaD.DataSource = Config.NumericList(1, 31);
                DListFltrMeshQeydD.DataBind();
                DListFltrMeshCixmaD.DataBind();

                DListFltrMeshQeydM.DataSource = DListFltrMeshCixmaM.DataSource = Config.MonthList(false);
                DListFltrMeshQeydM.DataBind();
                DListFltrMeshCixmaM.DataBind();
                DListFltrMeshQeydM.Items.Insert(0, new ListItem("--", "00"));
                DListFltrMeshCixmaM.Items.Insert(0, new ListItem("--", "00"));

                DListFltrMeshQeydY.DataSource = DListFltrMeshCixmaY.DataSource = Config.NumericList(1980, DateTime.Now.Year, "1000");
                DListFltrMeshQeydY.DataBind();
                DListFltrMeshCixmaY.DataBind();
            }

            if (DListFltrMeshgulluq.SelectedValue == "1")
                MltMeshgulluq.ActiveViewIndex = 0;
            else
                MltMeshgulluq.ActiveViewIndex = 1;

        }
    }

    protected void DListFltrSosialYardım_SelectedIndexChanged(object sender, EventArgs e)
    {
        MltSosialYardım.ActiveViewIndex = -1;

        if (DListFltrSosialYardımD.Items.Count > 0)
        {

            DListFltrSosialYardımD.SelectedValue = DListFltrSosialYardımM.SelectedValue = "00";
            DListFltrSosialYardımY.SelectedValue = "1000";
        }

        if (DListFltrSosialYardım.SelectedValue == "1")
        {
            if (DListFltrSosialYardımD.Items.Count < 1)
            {

                DListFltrSosialYardımD.DataSource = Config.NumericList(1, 31);
                DListFltrSosialYardımD.DataBind();

                DListFltrSosialYardımM.DataSource = Config.MonthList(false);
                DListFltrSosialYardımM.DataBind();
                DListFltrSosialYardımM.Items.Insert(0, new ListItem("--", "00"));

                DListFltrSosialYardımY.DataSource = Config.NumericList(1980, DateTime.Now.Year, "1000");
                DListFltrSosialYardımY.DataBind();

            }

            MltSosialYardım.ActiveViewIndex = 0;
        }
    }

    protected void DListFltrSosialXidmet_SelectedIndexChanged(object sender, EventArgs e)
    {
        MltSosialXidmet.ActiveViewIndex = -1;

        if (DListFltrSosialXidmetNov.Items.Count > 0)
        {
            DListFltrSosialXidmetNov.SelectedValue = "0";
            DListFltrSosialXidmetD.SelectedValue = DListFltrSosialXidmetM.SelectedValue = "00";
            DListFltrSosialXidmetY.SelectedValue = "1000";
        }

        if (DListFltrSosialXidmet.SelectedValue == "1")
        {
            if (DListFltrSosialXidmetNov.Items.Count < 1)
            {
                DListFltrSosialXidmetNov.DataSource = DALC.GetSoragcaByTableName("PersonsSosialXidmetNov");
                DListFltrSosialXidmetNov.DataBind();
                DListFltrSosialXidmetNov.Items.Insert(0, new ListItem("--", "0"));

                DListFltrSosialXidmetD.DataSource = Config.NumericList(1, 31);
                DListFltrSosialXidmetD.DataBind();

                DListFltrSosialXidmetM.DataSource = Config.MonthList(false);
                DListFltrSosialXidmetM.DataBind();
                DListFltrSosialXidmetM.Items.Insert(0, new ListItem("--", "00"));

                DListFltrSosialXidmetY.DataSource = Config.NumericList(1980, DateTime.Now.Year, "1000");
                DListFltrSosialXidmetY.DataBind();

            }

            MltSosialXidmet.ActiveViewIndex = 0;
        }
    }

    protected void DListFltrAsudeDusherge_SelectedIndexChanged(object sender, EventArgs e)
    {
        MltAsudeDusherge.ActiveViewIndex = -1;

        if (DListFltrAsudeDushergeRegion.Items.Count > 0)
        {
            DListFltrAsudeDushergeRegion.SelectedValue = "0";
            DListFltrAsudeDushergeGetdiyiD.SelectedValue = DListFltrAsudeDushergeGetdiyiM.SelectedValue = DListFltrAsudeDushergeGeldiyiD.SelectedValue = DListFltrAsudeDushergeGeldiyiM.SelectedValue = "00";
            DListFltrAsudeDushergeGetdiyiY.SelectedValue = DListFltrAsudeDushergeGeldiyiY.SelectedValue = "1000";
        }

        if (DListFltrAsudeDusherge.SelectedValue == "1")
        {
            if (DListFltrAsudeDushergeRegion.Items.Count < 1)
            {
                DListFltrAsudeDushergeRegion.DataSource = DALC.GetRegionsList();
                DListFltrAsudeDushergeRegion.DataBind();
                DListFltrAsudeDushergeRegion.Items.Insert(0, new ListItem("--", "0"));

                DListFltrAsudeDushergeGetdiyiD.DataSource = DListFltrAsudeDushergeGeldiyiD.DataSource = Config.NumericList(1, 31);
                DListFltrAsudeDushergeGetdiyiD.DataBind();
                DListFltrAsudeDushergeGeldiyiD.DataBind();

                DListFltrAsudeDushergeGetdiyiM.DataSource = DListFltrAsudeDushergeGeldiyiM.DataSource = Config.MonthList(false);
                DListFltrAsudeDushergeGetdiyiM.DataBind();
                DListFltrAsudeDushergeGeldiyiM.DataBind();
                DListFltrAsudeDushergeGetdiyiM.Items.Insert(0, new ListItem("--", "00"));
                DListFltrAsudeDushergeGeldiyiM.Items.Insert(0, new ListItem("--", "00"));

                DListFltrAsudeDushergeGetdiyiY.DataSource = DListFltrAsudeDushergeGeldiyiY.DataSource = Config.NumericList(1980, DateTime.Now.Year, "1000");
                DListFltrAsudeDushergeGetdiyiY.DataBind();
                DListFltrAsudeDushergeGeldiyiY.DataBind();
            }

            MltAsudeDusherge.ActiveViewIndex = 0;
        }
    }

}