using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;

public partial class Detail_Contents_110_Valideyinler : System.Web.UI.UserControl
{
    int _PersonsID = 0;

    #region Soragcalar
    //Sorgacalar

    public void Aylar()
    {
        DlistDogumD1.DataSource = DlistOlumD1.DataSource =
        DlistDogumD2.DataSource = DlistOlumD2.DataSource =
        DlistNigahD.DataSource = DlistNigahPozD.DataSource = Config.NumericList(1, 31);

        DlistDogumD1.DataBind();
        DlistOlumD1.DataBind();
        DlistDogumD2.DataBind();
        DlistOlumD2.DataBind();
        DlistNigahD.DataBind();
        DlistNigahPozD.DataBind();

        DlistDogumM1.DataSource = DlistOlumM1.DataSource =
        DlistDogumM2.DataSource = DlistOlumM2.DataSource =
        DlistNigahM.DataSource = DlistNigahPozM.DataSource = Config.MonthList();

        DlistDogumM1.DataBind();
        DlistOlumM1.DataBind();
        DlistDogumM2.DataBind();
        DlistOlumM2.DataBind();
        DlistNigahM.DataBind();
        DlistNigahPozM.DataBind();

        DlistDogumY1.DataSource = DlistOlumY1.DataSource =
        DlistDogumY2.DataSource = DlistOlumY2.DataSource =
        DlistNigahY.DataSource = DlistNigahPozY.DataSource = Config.NumericList(1980, DateTime.Now.Year, "1000");

        DlistDogumY1.DataBind();
        DlistOlumY1.DataBind();
        DlistDogumY2.DataBind();
        DlistOlumY2.DataBind();
        DlistNigahY.DataBind();
        DlistNigahPozY.DataBind();
    }

    public void Countries()
    {
        DataTable Dt = DALC.GetCountriesList();

        DlistCountry1.DataSource = DlistCountry2.DataSource = Dt;
        DlistCountry1.DataBind();
        DlistCountry2.DataBind();
        DlistCountry1.Items.Insert(0, new ListItem("--", "0"));
        DlistCountry2.Items.Insert(0, new ListItem("--", "0"));

        Regions(DlistCity1, DlistCountry1.SelectedValue);
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
        DlistVetendasliq1.DataSource = DlistVetendasliq2.DataSource = DALC.GetCitizenship();
        DlistVetendasliq1.DataBind();
        DlistVetendasliq2.DataBind();
        DlistVetendasliq1.Items.Insert(0, new ListItem("--", "0"));
        DlistVetendasliq2.Items.Insert(0, new ListItem("--", "0"));
    }

    public void BindFealiyyet()
    {
        DlistFealiyyet1.DataSource = DlistFealiyyet2.DataSource = DALC.GetPersonsRelativesWorkPositions();
        DlistFealiyyet1.DataBind();
        DlistFealiyyet2.DataBind();
        DlistFealiyyet1.Items.Insert(0, new ListItem("--", "0"));
        DlistFealiyyet2.Items.Insert(0, new ListItem("--", "0"));
    }

    #endregion

    public string ChekValideyinInfo(int PersonsRelativesTypesID)
    {
        // Əgər bazada varsa ID-ni gətirək
        string ID = DALC.GetValideyinID(_PersonsID, PersonsRelativesTypesID);

        if (ID == "-1")
            return "-1";

        if (string.IsNullOrEmpty(ID))
        {
            // Bazada yoxdursa Insert edib ID ni alaq
            int ChekID = DALC.NewBlankRelatives(_PersonsID, PersonsRelativesTypesID);

            if (ChekID < 1)
                return "-1";

            ID = ChekID._ToString();
        }

        return ID;
    }

    public void BindAtaInfromation()
    {
        DataTable Dt = DALC.GetPersonsValideyin(_PersonsID, 10);

        if (Dt == null)
        {
            Config.RedirectError();
            return;
        }

        if (Dt.Rows.Count > 0)
        {
            TxtPin1.Text = Dt._Rows("FIN");
            TxtAd1.Text = Dt._Rows("Ad");
            TxtSoyad1.Text = Dt._Rows("Soyad");
            TxtAta1.Text = Dt._Rows("Ata");

            DlistVetendasliq1.SelectedValue = Dt._Rows("CitizenshipID");
            DlistDogumD1.SelectedValue = Dt._Rows("DogumTarixi").IntDateDay();
            DlistDogumM1.SelectedValue = Dt._Rows("DogumTarixi").IntDateMonth();
            DlistDogumY1.SelectedValue = Dt._Rows("DogumTarixi").IntDateYear();
            DlistOlumD1.SelectedValue = Dt._Rows("OlumTarixi").IntDateDay();
            DlistOlumM1.SelectedValue = Dt._Rows("OlumTarixi").IntDateMonth();
            DlistOlumY1.SelectedValue = Dt._Rows("OlumTarixi").IntDateYear();

            DlistCountry1.SelectedValue = Dt._Rows("YasadigiUnvanCountriesID").IsEmptyReplaceNoul();
            Regions(DlistCity1, DlistCountry1.SelectedValue);
            DlistCity1.SelectedValue = Dt._Rows("YasadigiUnvanRegionsID").IsEmptyReplaceNoul();
            txtUnvan1.Text = Dt._Rows("YasadigiUnvan");
            DlistIsh1.SelectedValue = Dt._Rows("IsWorks").IsEmptyReplaceNoul();
            DlistFealiyyet1.SelectedValue = Dt._Rows("PersonsRelativesWorkPositionsID").IsEmptyReplaceNoul();

            DlistMigrant1.SelectedValue = Dt._Rows("IsMigrations");
            TxtAtaDescription.Text = Dt._Rows("Description");
        }
    }

    public void BindAnaInfromation()
    {
        DataTable Dt = DALC.GetPersonsValideyin(_PersonsID, 20);

        if (Dt == null)
        {
            Config.RedirectError();
            return;
        }

        if (Dt.Rows.Count > 0)
        {
            TxtPin2.Text = Dt._Rows("FIN");
            TxtAd2.Text = Dt._Rows("Ad");
            TxtSoyad2.Text = Dt._Rows("Soyad");
            TxtAta2.Text = Dt._Rows("Ata");

            DlistVetendasliq2.SelectedValue = Dt._Rows("CitizenshipID");
            DlistDogumD2.SelectedValue = Dt._Rows("DogumTarixi").IntDateDay();
            DlistDogumM2.SelectedValue = Dt._Rows("DogumTarixi").IntDateMonth();
            DlistDogumY2.SelectedValue = Dt._Rows("DogumTarixi").IntDateYear();
            DlistOlumD2.SelectedValue = Dt._Rows("OlumTarixi").IntDateDay();
            DlistOlumM2.SelectedValue = Dt._Rows("OlumTarixi").IntDateMonth();
            DlistOlumY2.SelectedValue = Dt._Rows("OlumTarixi").IntDateYear();

            DlistCountry2.SelectedValue = Dt._Rows("YasadigiUnvanCountriesID").IsEmptyReplaceNoul();
            Regions(DlistCity2, DlistCountry2.SelectedValue);
            DlistCity2.SelectedValue = Dt._Rows("YasadigiUnvanRegionsID").IsEmptyReplaceNoul();
            txtUnvan2.Text = Dt._Rows("YasadigiUnvan");
            DlistIsh2.SelectedValue = Dt._Rows("IsWorks").IsEmptyReplaceNoul();
            DlistFealiyyet2.SelectedValue = Dt._Rows("PersonsRelativesWorkPositionsID").IsEmptyReplaceNoul();

            DlistMigrant2.SelectedValue = Dt._Rows("IsMigrations").IsEmptyReplaceNoul();
            TxtAnaDescription.Text = Dt._Rows("Description");
        }
    }

    public void BindNigah()
    {
        DataTable Dt = DALC.GetDataTableParams("*", "PersonsValideyinNikah", "PersonsID", _PersonsID, "");

        if (Dt == null)
        {
            Config.RedirectError();
            return;
        }

        if (Dt.Rows.Count > 0)
        {
            DListMarriage.SelectedValue =
            DlistNigahD.SelectedValue = Dt._Rows("NigahQeydiyyatTarixi").IntDateDay();
            DlistNigahM.SelectedValue = Dt._Rows("NigahQeydiyyatTarixi").IntDateMonth();
            DlistNigahY.SelectedValue = Dt._Rows("NigahQeydiyyatTarixi").IntDateYear();
            DlistNigahPozD.SelectedValue = Dt._Rows("NigahPozulmaTarixi").IntDateDay();
            DlistNigahPozM.SelectedValue = Dt._Rows("NigahPozulmaTarixi").IntDateMonth();
            DlistNigahPozY.SelectedValue = Dt._Rows("NigahPozulmaTarixi").IntDateYear();

            if (Dt._Rows("IsMarriage") != string.Empty)
                DListMarriage.SelectedValue = ((bool)Dt._RowsObject("IsMarriage") ? "1" : "0");

            TxtDescriptionNigah.Text = Dt._Rows("Description");
        }
    }

    public void PermissionSet(Panel P, LinkButton L, int PermissionID)
    {
        if (DALC._IsPermissionType(PermissionID))
        {
            P.Visible = true;

            //Edit imkani
            if (!DALC._IsPermissionTypeEdit(PermissionID))
            {
                P.Enabled = false;
                L.Visible = false;
            }
        }
        else
        {
            P.Visible = false;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //Sadəcə alırıq. Yoxlamağa ehtiyac yoxdur. Content/Default.aspx də yoxlanılır.
        _PersonsID = Config._GetQueryString("i").QueryIdDecrypt()._ToInt32();

        //Permissions
        PermissionSet(PnlAta, LnkSuccess1, 140);
        PermissionSet(PnlAna, LnkSuccess5, 150);

        PermissionSet(PnlAtaYasadigi, LnkSuccess2, 160);
        PermissionSet(PnlAnaYasadigi, LnkSuccess6, 170);

        PermissionSet(PnlAtaMesgulluq, LnkSuccess3, 210);
        PermissionSet(PnlAnaMesgulluq, LnkSuccess7, 230);

        PermissionSet(PnlAtaMiqrant, LnkSuccess4, 220);
        PermissionSet(PnlAnaMiqrant, LnkSuccess8, 240);

        PermissionSet(PnlNigah, LnkSuccess8, 200);

        if (!IsPostBack)
        {
            Aylar();
            Countries();
            BindCitizenship();
            BindFealiyyet();
            BindAtaInfromation();
            BindAnaInfromation();
            BindNigah();
        }
    }

    int UsersPermissionTypesID = 0;
    protected void LnkSuccess1_Click(object sender, EventArgs e)
    {
        string RelativType = (sender as LinkButton).CommandArgument;

        //Elave edek ve ya olanin id alaq
        string ID = ChekValideyinInfo(int.Parse(RelativType));

        if (string.IsNullOrEmpty(ID) || ID == "-1")
        {
            Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
            return;
        }

        Dictionary<string, object> Dictionary = new Dictionary<string, object>();

        if (RelativType == "10")
        {
            UsersPermissionTypesID = 140;

            // Burada məlumatlari yeniləyək
            Dictionary.Add("FIN", TxtPin1.Text.Trim().NullConvert());
            Dictionary.Add("Soyad", TxtSoyad1.Text.Trim().NullConvert());
            Dictionary.Add("Ad", TxtAd1.Text.Trim().NullConvert());
            Dictionary.Add("Ata", TxtAta1.Text.Trim().NullConvert());
            Dictionary.Add("CitizenshipID", DlistVetendasliq1.SelectedValue.NullConvert());
            Dictionary.Add("DogumTarixi", (DlistDogumY1.SelectedValue + DlistDogumM1.SelectedValue + DlistDogumD1.SelectedValue).NullConvert());
            Dictionary.Add("OlumTarixi", (DlistOlumY1.SelectedValue + DlistOlumM1.SelectedValue + DlistOlumD1.SelectedValue).NullConvert());
            Dictionary.Add("Description", TxtAtaDescription.Text.Trim());
            Dictionary.Add("WhereID", int.Parse(ID));
        }
        else
        {
            UsersPermissionTypesID = 150;

            //Burada məlumatlari yeniləyək
            Dictionary.Add("FIN", TxtPin2.Text.Trim().NullConvert());
            Dictionary.Add("Soyad", TxtSoyad2.Text.Trim().NullConvert());
            Dictionary.Add("Ad", TxtAd2.Text.Trim().NullConvert());
            Dictionary.Add("Ata", TxtAta2.Text.Trim().NullConvert());
            Dictionary.Add("CitizenshipID", DlistVetendasliq2.SelectedValue.NullConvert());
            Dictionary.Add("DogumTarixi", (DlistDogumY2.SelectedValue + DlistDogumM2.SelectedValue + DlistDogumD2.SelectedValue).NullConvert());
            Dictionary.Add("OlumTarixi", (DlistOlumY2.SelectedValue + DlistOlumM2.SelectedValue + DlistOlumD2.SelectedValue).NullConvert());
            Dictionary.Add("Description", TxtAnaDescription.Text.Trim());
            Dictionary.Add("WhereID", int.Parse(ID));
        }

        Dictionary.Add("OrganizationsID", DALC._GetUsersLogin.OrganizationsID);
        Dictionary.Add("UsersID", DALC._GetUsersLogin.ID);
        Dictionary.Add("Last_Dt", DateTime.Now);
        Dictionary.Add("Last_Ip", HttpContext.Current.Request.UserHostAddress.IPToInteger());

        int Chek = DALC.UpdateDatabase("PersonsRelatives", Dictionary);
        if (Chek < 1)
        {
            Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
            return;
        }

        Config.MsgBoxAjax("Əməliyyat uğurla yerinə yetirildi.", Page, true);
        DALC.UsersHistoryInsert(30, UsersPermissionTypesID, string.Format("{0} vətəndaşlığı, doğum və ölüm tarixi haqqında məlumat yeniləndi.", RelativType.Replace("10", "Atasının").Replace("20", "Anasının")), _PersonsID);
    }

    protected void LnkSuccess2_Click(object sender, EventArgs e)
    {
        string RelativType = (sender as LinkButton).CommandArgument;
        string ID = ChekValideyinInfo(int.Parse(RelativType));

        if (string.IsNullOrEmpty(ID) || ID == "-1")
        {
            Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
            return;
        }
        Dictionary<string, object> Dictionary = new Dictionary<string, object>();

        if (RelativType == "10")
        {
            UsersPermissionTypesID = 160;
            // Burada məlumatlari yeniləyək       
            Dictionary.Add("YasadigiUnvanCountriesID", DlistCountry1.SelectedValue.NullConvert());
            Dictionary.Add("YasadigiUnvanRegionsID", DlistCity1.SelectedValue.NullConvert());
            Dictionary.Add("YasadigiUnvan", txtUnvan1.Text.Trim());
            Dictionary.Add("WhereID", int.Parse(ID));
        }
        else
        {
            UsersPermissionTypesID = 170;
            // Burada məlumatlari yeniləyək       
            Dictionary.Add("YasadigiUnvanCountriesID", DlistCountry2.SelectedValue.NullConvert());
            Dictionary.Add("YasadigiUnvanRegionsID", DlistCity2.SelectedValue.NullConvert());
            Dictionary.Add("YasadigiUnvan", txtUnvan2.Text.Trim());
            Dictionary.Add("WhereID", int.Parse(ID));
        }

        Dictionary.Add("OrganizationsID", DALC._GetUsersLogin.OrganizationsID);
        Dictionary.Add("UsersID", DALC._GetUsersLogin.ID);
        Dictionary.Add("Last_Dt", DateTime.Now);
        Dictionary.Add("Last_Ip", HttpContext.Current.Request.UserHostAddress.IPToInteger());

        int Chek = DALC.UpdateDatabase("PersonsRelatives", Dictionary);
        if (Chek < 1)
        {
            Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
            return;
        }

        Config.MsgBoxAjax("Əməliyyat uğurla yerinə yetirildi.", Page, true);
        DALC.UsersHistoryInsert(30, UsersPermissionTypesID, string.Format("{0} yaşadığı ünvan haqqında məlumat yeniləndi.", RelativType.Replace("10", "Atasının").Replace("20", "Anasının")), _PersonsID);
    }

    protected void LnkSuccess3_Click(object sender, EventArgs e)
    {
        string RelativType = (sender as LinkButton).CommandArgument;
        string ID = ChekValideyinInfo(int.Parse(RelativType));

        if (string.IsNullOrEmpty(ID) || ID == "-1")
        {
            Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
            return;
        }

        Dictionary<string, object> Dictionary = new Dictionary<string, object>();
        if (RelativType == "10")
        {
            UsersPermissionTypesID = 210;
            // Burada məlumatlari yeniləyək       
            Dictionary.Add("IsWorks", DlistIsh1.SelectedValue.NullConvert());
            Dictionary.Add("PersonsRelativesWorkPositionsID", DlistFealiyyet1.SelectedValue.NullConvert());
            Dictionary.Add("WhereID", int.Parse(ID));
        }
        else
        {
            UsersPermissionTypesID = 230;
            // Burada məlumatlari yeniləyək       
            Dictionary.Add("IsWorks", DlistIsh2.SelectedValue.NullConvert());
            Dictionary.Add("PersonsRelativesWorkPositionsID", DlistFealiyyet2.SelectedValue.NullConvert());
            Dictionary.Add("WhereID", int.Parse(ID));
        }

        Dictionary.Add("OrganizationsID", DALC._GetUsersLogin.OrganizationsID);
        Dictionary.Add("UsersID", DALC._GetUsersLogin.ID);
        Dictionary.Add("Last_Dt", DateTime.Now);
        Dictionary.Add("Last_Ip", HttpContext.Current.Request.UserHostAddress.IPToInteger());

        int Chek = DALC.UpdateDatabase("PersonsRelatives", Dictionary);
        if (Chek < 1)
        {
            Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
            return;
        }

        Config.MsgBoxAjax("Əməliyyat uğurla yerinə yetirildi.", Page, true);
        DALC.UsersHistoryInsert(30, UsersPermissionTypesID, string.Format("{0} məşğulluq vəziyyəti haqqında məlumat yeniləndi.", RelativType.Replace("10", "Atasının").Replace("20", "Anasının")), _PersonsID);
    }

    protected void LnkSuccess4_Click(object sender, EventArgs e)
    {
        string RelativType = (sender as LinkButton).CommandArgument;
        string ID = ChekValideyinInfo(int.Parse(RelativType));

        if (string.IsNullOrEmpty(ID) || ID == "-1")
        {
            Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
            return;
        }

        Dictionary<string, object> Dictionary = new Dictionary<string, object>();
        if (RelativType == "10")
        {
            UsersPermissionTypesID = 220;
            // Burada məlumatlari yeniləyək
            Dictionary.Add("IsMigrations", DlistMigrant1.SelectedValue.NullConvert());
            Dictionary.Add("WhereID", int.Parse(ID));
        }
        else
        {
            UsersPermissionTypesID = 240;
            // Burada məlumatlari yeniləyək
            Dictionary.Add("IsMigrations", DlistMigrant2.SelectedValue.NullConvert());
            Dictionary.Add("WhereID", int.Parse(ID));
        }

        Dictionary.Add("OrganizationsID", DALC._GetUsersLogin.OrganizationsID);
        Dictionary.Add("UsersID", DALC._GetUsersLogin.ID);
        Dictionary.Add("Last_Dt", DateTime.Now);
        Dictionary.Add("Last_Ip", HttpContext.Current.Request.UserHostAddress.IPToInteger());

        int Chek = DALC.UpdateDatabase("PersonsRelatives", Dictionary);
        if (Chek < 1)
        {
            Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
            return;
        }

        Config.MsgBoxAjax("Əməliyyat uğurla yerinə yetirildi.", Page, true);
        DALC.UsersHistoryInsert(30, UsersPermissionTypesID, string.Format("{0} əmək miqrantı olub-olmaması haqqında məlumat yeniləndi.", RelativType.Replace("10", "Atasının").Replace("20", "Anasının")), _PersonsID);
    }

    protected void LnkSuccess9_Click(object sender, EventArgs e)
    {
        string ID = DALC.GetDbSingleValuesParams("ID", "PersonsValideyinNikah", "PersonsID", _PersonsID, "");
        if (ID == "-1")
        {
            Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
            return;
        }

        if (string.IsNullOrEmpty(ID))
        {
            int ChekID = DALC.NewBlankInsert("PersonsValideyinNikah", _PersonsID, false);
            if (ChekID < 1)
            {
                Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
                return;
            }
            ID = ChekID._ToString();
        }

        //Burada məlumatlari yeniləyək
        Dictionary<string, object> Dictionary = new Dictionary<string, object>
        {
            { "OrganizationsID", DALC._GetUsersLogin.OrganizationsID },
            { "UsersID", DALC._GetUsersLogin.ID },
            { "NigahQeydiyyatTarixi", (DlistNigahY.SelectedValue + DlistNigahM.SelectedValue + DlistNigahD.SelectedValue).NullConvert() },
            { "NigahPozulmaTarixi", (DlistNigahPozY.SelectedValue + DlistNigahPozM.SelectedValue + DlistNigahPozD.SelectedValue).NullConvert() },
            { "IsMarriage", DListMarriage.SelectedValue.NullConvert() },
            { "Description", TxtDescriptionNigah.Text.Trim() },
            { "WhereID", int.Parse(ID) },
            { "Last_Dt", DateTime.Now },
            { "Last_Ip", HttpContext.Current.Request.UserHostAddress.IPToInteger() }
        };

        int Chek = DALC.UpdateDatabase("PersonsValideyinNikah", Dictionary);
        if (Chek < 1)
        {
            Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
            return;
        }

        Config.MsgBoxAjax("Əməliyyat uğurla yerinə yetirildi.", Page, true);
        DALC.UsersHistoryInsert(30, 200, "Valideynlərinin nikah vəziyyəti haqqında məlumatda dəyişiklik edildi.", _PersonsID);
    }

    protected void DlistCountry1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Regions(DlistCity1, DlistCountry1.SelectedValue);
    }

    protected void DlistCountry2_SelectedIndexChanged(object sender, EventArgs e)
    {
        Regions(DlistCity2, DlistCountry2.SelectedValue);
    }
}