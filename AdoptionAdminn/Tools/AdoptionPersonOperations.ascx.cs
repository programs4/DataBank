using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


public partial class AdoptionAdminn_Tools_AdoptionPersonOperations : System.Web.UI.UserControl
{
    string _TableName = "AdoptionPersons";
    string ChildID = Config._GetQueryString("id");
    string path = "/uploads/adoption/persons/";
    string errorClass = " errorClass";
    string OperationType = Config._GetQueryString("type");
    DataTable DtChildInfo;

    private void BindLists()
    {
        DListDocType.DataSource = DALC.GetDataTable("ID,Name,Series,CONCAT(Name,' (',Series,') ') as DocNumber", "DocumentTypes", "");
        DListDocType.DataValueField = "ID";
        DListDocType.DataTextField = "DocNumber";
        DListDocType.DataBind();
        DListDocType.Items.Insert(0, new ListItem("--", "-1"));

        DListEyeColor.DataSource = DALC.GetDataTable("ID,Name", "Colors", "Order By [Priority] asc, Name asc");
        DListEyeColor.DataBind();
        DListEyeColor.Items.Insert(0, new ListItem("--", "-1"));

        DListHairColor.DataSource = DListEyeColor.DataSource;
        DListHairColor.DataBind();
        DListHairColor.Items.Insert(0, new ListItem("--", "-1"));

        DListCountries.DataSource = DALC.GetDataTable("ID,Name", "Countries", "");
        DListCountries.DataBind();
        DListCountries.Items.Insert(0, new ListItem("--", "-1"));

        DListBirthRegistraionCountry.DataSource = DListCountries.DataSource;
        DListBirthRegistraionCountry.DataBind();
        DListBirthRegistraionCountry.Items.Insert(0, new ListItem("--", "-1"));

        RListAdoptionPersonsStatus.DataSource = DALC.GetDataTable("ID,Name", "AdoptionPersonsStatus", "");
        RListAdoptionPersonsStatus.DataBind();
        RListAdoptionPersonsStatus.SelectedIndex = 0;

        DListChildFirstRegistrationPlace.DataSource = DALC.GetDataTable("ID,Name", "AdoptionOrganizations", "Where AdoptionOrganizationsTypesID=20");
        DListChildFirstRegistrationPlace.DataBind();
        DListChildFirstRegistrationPlace.Items.Insert(0, new ListItem("- İcra Hakimiyyəti -", "-1"));

    }

    //baci qardash gridi
    private void BindSisterBrother(long ParentID)
    {
        GrdSisterBrotherList.DataSource = DALC.GetDataTableBySqlCommand("Select *,Concat (Soyad,' ',Ad,' ',Ata) as Fullname from AdoptionPersons where ParentID=@ParentID and IsBrotherSister=@IsBrotherSister", "ParentID,IsBrotherSister", new object[] { ParentID, true });
        GrdSisterBrotherList.DataBind();
    }

    //sekli oxu ve yenile
    private void LoadImage()
    {
        string[] File = System.IO.Directory.GetFiles(Server.MapPath(path), ChildID + ".jpg");
        if (File.Length < 1)
        {
            ImgMain.ImageUrl = "/pics/profile-img.png";
        }
        else
        {
            ImgMain.ImageUrl = path + ChildID + ".jpg";
        }
    }

    private void BindDetails()
    {
        LoadImage();

        long ParentID = Convert.ToInt64(DtChildInfo._Rows("ParentID"));
        BtnSave.Attributes.Add("data-parent", ParentID._ToString());
        if (!string.IsNullOrEmpty(ChildID) && string.IsNullOrEmpty(OperationType))
        {
            DListDocType.SelectedValue = DtChildInfo._Rows("DocumentTypesID");
            TxtDocNumber.Text = DtChildInfo._Rows("DocNumber");
            TxtDocGivenDt.Text = string.IsNullOrEmpty(DtChildInfo._Rows("DocVerilmeTarixi")) ? "" : Convert.ToDateTime(DtChildInfo._RowsObject("DocVerilmeTarixi")).ToString("dd.MM.yyyy");
            TxtPIN.Text = DtChildInfo._Rows("PIN");
            TxtSurname.Text = DtChildInfo._Rows("Soyad");
            TxtName.Text = DtChildInfo._Rows("Ad");
            TxtPatronymic.Text = DtChildInfo._Rows("Ata");
            TxtBirthDt.Text = string.IsNullOrEmpty(DtChildInfo._Rows("DogumTarixi")) ? "" : Convert.ToDateTime(DtChildInfo._RowsObject("DogumTarixi")).ToString("dd.MM.yyyy");
            DListGender.SelectedValue = DtChildInfo._Rows("Gender") == "True" ? "1" : "0";
            DListEyeColor.SelectedValue = DtChildInfo._Rows("GozColorsID");
            DListHairColor.SelectedValue = DtChildInfo._Rows("SachColorsID");
            DListCountries.SelectedValue = DtChildInfo._Rows("CountriesID");
            DListCountries_SelectedIndexChanged(null, null);
            DListRegions.SelectedValue = DtChildInfo._Rows("RegionsID");
            TxtChildLivePlace.Text = DtChildInfo._Rows("Address");
            DListChildFirstRegistrationPlace.SelectedValue = DtChildInfo._Rows("AdoptionOrganizationsID");
            TxtChildFirstRegistraionDt.Text = string.IsNullOrEmpty(DtChildInfo._Rows("IlkinUcotTarix")) ? "" : Convert.ToDateTime(DtChildInfo._RowsObject("IlkinUcotTarix")).ToString("dd.MM.yyyy");
            TxtInfoSheetNo.Text = DtChildInfo._Rows("MelumatVereqeNo");
            TxtInfoSheetDt.Text = string.IsNullOrEmpty(DtChildInfo._Rows("MelumatVereqeTarix")) ? "" : Convert.ToDateTime(DtChildInfo._RowsObject("MelumatVereqeTarix")).ToString("dd.MM.yyyy");
            DListBirthRegistraionCountry.SelectedValue = DtChildInfo._Rows("DogumQeydiyyatYeriCountriesID");
            DListBirthRegistraionCountry_SelectedIndexChanged(null, null);
            DListBirthRegistrationRegion.SelectedValue = DtChildInfo._Rows("DogumQeydiyyatYeriRegionsID");
            TxtBirthRegistraionAddress.Text = DtChildInfo._Rows("DogumQeydiyyatYeriUnvan");
            TxtInfoFather.Text = DtChildInfo._Rows("ValideynAta");
            TxtInfoMother.Text = DtChildInfo._Rows("ValideynAna");
            TxtCentralizedRegistrationPlace.Text = DtChildInfo._Rows("MerkeziOrgan");
            TxtChildHealthInfo.Text = DtChildInfo._Rows("SehhetiBarede");
            TxtCloseRelativesInfo.Text = DtChildInfo._Rows("YaxinQohumlari");
            TxtDeprivedParentCareType.Text = DtChildInfo._Rows("MehrumFormasi");
            TxtReasonGivenToFamily.Text = DtChildInfo._Rows("UsaginVerilmeEsaslari");
            TxtChildSpecialities.Text = DtChildInfo._Rows("XususiElametler");
            TxtInfoShownAzeriFamilies.Text = DtChildInfo._Rows("AileGosterilmesiBarede");
            TxtAQPDKregistrationDt.Text = string.IsNullOrEmpty(DtChildInfo._Rows("AQPDKTarixi")) ? "" : Convert.ToDateTime(DtChildInfo._RowsObject("AQPDKTarixi")).ToString("dd.MM.yyyy");
            TxtDescription.Text = DtChildInfo._Rows("Description");
            ChkIsWeb.Checked = Convert.ToBoolean(string.IsNullOrEmpty(DtChildInfo._Rows("IsWebPreview")) ? "false" : DtChildInfo._Rows("IsWebPreview"));
            RListAdoptionPersonsStatus.SelectedValue = DtChildInfo._Rows("AdoptionPersonsStatusID");
            Session["IsBrotherSister"] = DtChildInfo._Rows("IsBrotherSister");
        }

        if (Convert.ToBoolean(DtChildInfo._Rows("IsBrotherSister")) == true)
        {
            BindSisterBrother(ParentID);
        }
        else
        {
            PnlSisterBrotherList.Visible = false;
        }
    }

    //sekilleri yukle
    private void BindImage()
    {
        if (!FileUploadFiles.HasFile)
            return;

        DateTime Date = DateTime.Now;
        string AllowType = "-jpg-jpeg-png-";
        string FileType = FileUploadFiles.PostedFile.FileName.GetExtension();

        string FileName = Config.ClearTitle(System.IO.Path.GetFileNameWithoutExtension(FileUploadFiles.PostedFile.FileName)) + "." + FileType;

        string SavePath = path + ChildID + ".jpg";

        if (AllowType.IndexOf(FileType) < 0)
        {
            Config.MsgBoxAjax("Faylın növü uyğun gəlmir. Yalnız : " + AllowType, Page);
            return;
        }

        if (!FileUploadFiles.PostedFile.CheckFileContentLength())
        {
            Config.MsgBoxAjax("Faylın həcmi ən çox 10 MB ola bilər.", Page);
            return;
        }

        try
        {
            FileUploadFiles.SaveAs(Server.MapPath(SavePath));
        }
        catch
        {
            DALC.ErrorLogsInsert("AdoptionPersonOperations-da şəkili yukledikde xeta bash verdi.");
            return;
        }
    }

    //inputlari temizlemek
    private void ClearInputs()
    {
        DListBirthRegistraionCountry.SelectedIndex = DListBirthRegistrationRegion.SelectedIndex = DListCountries.SelectedIndex = DListDocType.SelectedIndex = DListEyeColor.SelectedIndex = DListGender.SelectedIndex = DListHairColor.SelectedIndex = DListRegions.SelectedIndex = DListChildFirstRegistrationPlace.SelectedIndex = 0;
        TxtAQPDKregistrationDt.Text = TxtBirthDt.Text = TxtBirthRegistraionAddress.Text = TxtCentralizedRegistrationPlace.Text = TxtChildFirstRegistraionDt.Text =
        TxtChildHealthInfo.Text = TxtChildLivePlace.Text = TxtChildSpecialities.Text = TxtCloseRelativesInfo.Text = TxtDeprivedParentCareType.Text = TxtDescription.Text = TxtDocGivenDt.Text = TxtDocNumber.Text =
        TxtInfoFather.Text = TxtInfoMother.Text = TxtInfoSheetDt.Text = TxtInfoSheetNo.Text = TxtInfoShownAzeriFamilies.Text = TxtName.Text = TxtPatronymic.Text = TxtPIN.Text = TxtReasonGivenToFamily.Text = TxtSurname.Text = string.Empty;
    }

    public void ErrorMessages(TextBox T, string Text)
    {
        if (T != null)
        {
            T.Focus();
            T.CssClass += errorClass;
        }
        PnlAlert.Visible = true;
        LtrError.Text = Text;
        return;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PnlNotification.Visible = (Config._GetQueryString("success") == "ok");

            PnlAlert.Visible = false;

            string fullname = "";
            if (!string.IsNullOrEmpty(ChildID))
            {
                DtChildInfo = DALC.GetDataTableParams("*", _TableName, "ID", ChildID._ToInt32(), "");
                fullname = DtChildInfo._Rows("Soyad") + " " + DtChildInfo._Rows("Ad") + " " + DtChildInfo._Rows("Ata");
            }

            BindLists();

            if (string.IsNullOrEmpty(ChildID))
            {
                LtrTitle.Text = "Yeni";
                PnlSisterBrotherList.Visible = false;
                PnlAddToExisting.Visible = false;
            }
            else if (!string.IsNullOrEmpty(OperationType))
            {
                LtrTitle.Text = "Qardaş və bacı əlavə et(" + fullname + " - " + ChildID + " )";
                BindDetails();
                PnlAddToExisting.Visible = false;
            }
            else
            {
                LtrTitle.Text = "Düzəliş (" + fullname + " - " + ChildID + " )";
                BindDetails();
            }
        }
    }

    protected void DListCountries_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DListCountries.SelectedIndex != 0)
        {
            DListRegions.DataSource = DALC.GetDataTableParams("ID,Name", "Regions", "CountriesID", DListCountries.SelectedValue, "");
            DListRegions.DataBind();
            DListRegions.Items.Insert(0, new ListItem("--", "-1"));
        }
    }

    protected void DListBirthRegistraionCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DListBirthRegistraionCountry.SelectedIndex != 0)
        {
            DListBirthRegistrationRegion.DataSource = DALC.GetDataTableParams("ID,Name", "Regions", "CountriesID", DListBirthRegistraionCountry.SelectedValue, "");
            DListBirthRegistrationRegion.DataBind();
            DListBirthRegistrationRegion.Items.Insert(0, new ListItem("--", "-1"));
        }
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        PnlAlert.Visible = PnlNotification.Visible = false;
        TxtDocNumber.CssClass = TxtSurname.CssClass = TxtName.CssClass = TxtPatronymic.CssClass = TxtPIN.CssClass = TxtDocGivenDt.CssClass = TxtBirthDt.CssClass = TxtChildFirstRegistraionDt.CssClass = TxtAQPDKregistrationDt.CssClass = TxtInfoSheetDt.CssClass = "form-control";

        //vacib xanalarin yoxlanilmasi
        #region Validation

        if (string.IsNullOrEmpty(TxtDocNumber.Text) || !Config.IsNumeric(TxtDocNumber.Text))
        {
            ErrorMessages(TxtDocNumber, "Sənədin nömrəsini daxil edin.");
            return;
        }
        if (string.IsNullOrEmpty(TxtSurname.Text))
        {
            ErrorMessages(TxtSurname, "Soyad daxil edin.");
            return;
        }
        if (string.IsNullOrEmpty(TxtName.Text))
        {
            ErrorMessages(TxtName, "Ad daxil edin.");
            return;
        }

        if (string.IsNullOrEmpty(TxtPatronymic.Text))
        {
            ErrorMessages(TxtPatronymic, "Ata adı daxil edin.");
            return;
        }

        if (!string.IsNullOrEmpty(TxtPIN.Text) && DListDocType.SelectedValue != "100" && TxtPIN.Text.Length != 7)
        {
            ErrorMessages(TxtPIN, "FİN 7 simvoldan cox ola bilməz.");
            return;
        }

        if (TxtPIN.Text.Length > 10)
        {
            ErrorMessages(TxtPIN, "FİN 10 simvoldan çox ola bilməz.");
            return;
        }
        if (Config.DateFormatClear(TxtDocGivenDt.Text) == null && !string.IsNullOrEmpty(TxtDocGivenDt.Text))
        {
            ErrorMessages(TxtDocGivenDt, "Tarix formatı düzgün deyil.");
            return;
        }
        if (Config.DateFormatClear(TxtBirthDt.Text) == null && !string.IsNullOrEmpty(TxtBirthDt.Text))
        {
            ErrorMessages(TxtBirthDt, "Tarix formatı düzgün deyil.");
            return;
        }
        if (Config.DateFormatClear(TxtChildFirstRegistraionDt.Text) == null && !string.IsNullOrEmpty(TxtChildFirstRegistraionDt.Text))
        {
            ErrorMessages(TxtChildFirstRegistraionDt, "Tarix formatı düzgün deyil.");
            return;
        }
        if (Config.DateFormatClear(TxtAQPDKregistrationDt.Text) == null && !string.IsNullOrEmpty(TxtAQPDKregistrationDt.Text))
        {
            ErrorMessages(TxtAQPDKregistrationDt, "Tarix formatı düzgün deyil.");
            return;
        }
        if (Config.DateFormatClear(TxtInfoSheetDt.Text) == null && !string.IsNullOrEmpty(TxtInfoSheetDt.Text))
        {
            ErrorMessages(TxtInfoSheetDt, "Tarix formatı düzgün deyil.");
            return;
        }
        #endregion

        Dictionary<string, object> DictAdoptionPersons = new Dictionary<string, object>()
        {
            {"DocumentTypesID",DListDocType.SelectedValue=="-1"? 0 : int.Parse(DListDocType.SelectedValue)},
            {"DocNumber",Convert.ToInt32(TxtDocNumber.Text)},
            {"DocVerilmeTarixi",string.IsNullOrEmpty(TxtDocGivenDt.Text)?DBNull.Value:Config.DateFormatClear(TxtDocGivenDt.Text)},
            {"PIN",TxtPIN.Text.ToUpper()},
            {"Soyad",TxtSurname.Text},
            {"Ad",TxtName.Text},
            {"Ata",TxtPatronymic.Text},
            {"Dogumtarixi",string.IsNullOrEmpty(TxtBirthDt.Text)?DBNull.Value:Config.DateFormatClear(TxtBirthDt.Text)},
            {"Gender",DListGender.SelectedValue},
            {"GozColorsID",DListEyeColor.SelectedValue=="-1"? 0 :int.Parse(DListEyeColor.SelectedValue)},
            {"SachColorsID",DListHairColor.SelectedValue=="-1"? 0 :int.Parse(DListHairColor.SelectedValue)},
            {"CountriesID",DListCountries.SelectedValue=="-1"? 0 :int.Parse(DListCountries.SelectedValue)},
            {"RegionsID",DListRegions.SelectedValue=="-1"? 0 :int.Parse(string.IsNullOrEmpty(DListRegions.SelectedValue)?"-1":DListRegions.SelectedValue)},
            {"Address",TxtChildLivePlace.Text},
            {"AdoptionOrganizationsID",DListChildFirstRegistrationPlace.SelectedValue=="-1"? 0 :int.Parse(DListChildFirstRegistrationPlace.SelectedValue)},
            {"IlkinUcotTarix",string.IsNullOrEmpty(TxtChildFirstRegistraionDt.Text)?DBNull.Value:Config.DateFormatClear(TxtChildFirstRegistraionDt.Text)},
            {"MelumatVereqeNo",TxtInfoSheetNo.Text},
            {"MelumatVereqeTarix",string.IsNullOrEmpty(TxtInfoSheetDt.Text)?DBNull.Value:Config.DateFormatClear(TxtInfoSheetDt.Text)},
            {"DogumQeydiyyatYeriCountriesID",DListBirthRegistraionCountry.SelectedValue=="-1"? 0 :int.Parse(DListBirthRegistraionCountry.SelectedValue)},
            {"DogumQeydiyyatYeriRegionsID",int.Parse(string.IsNullOrEmpty(DListBirthRegistrationRegion.SelectedValue)?"-1":DListBirthRegistrationRegion.SelectedValue)},
            {"DogumQeydiyyatYeriUnvan",TxtBirthRegistraionAddress.Text},
            {"ValideynAta",TxtInfoFather.Text},
            {"ValideynAna",TxtInfoMother.Text},
            {"MerkeziOrgan",TxtCentralizedRegistrationPlace.Text},
            {"SehhetiBarede",TxtChildHealthInfo.Text},
            {"YaxinQohumlari",TxtCloseRelativesInfo.Text},
            {"MehrumFormasi",TxtDeprivedParentCareType.Text},
            {"UsaginVerilmeEsaslari",TxtReasonGivenToFamily.Text},
            {"XususiElametler",TxtChildSpecialities.Text},
            {"AileGosterilmesiBarede",TxtInfoShownAzeriFamilies.Text},
            {"AQPDKTarixi",string.IsNullOrEmpty(TxtAQPDKregistrationDt.Text)?DBNull.Value:Config.DateFormatClear(TxtAQPDKregistrationDt.Text)},
            {"Description",TxtDescription.Text},
            {"IsWebPreview",Convert.ToBoolean(ChkIsWeb.Checked)},
            {"AdoptionPersonsStatusID",RListAdoptionPersonsStatus.SelectedValue },
        };

        //yeni elave
        if (string.IsNullOrEmpty(ChildID))
        {
            DictAdoptionPersons.Add("ParentID", DateTime.Now.Ticks);
            DictAdoptionPersons.Add("IsBrotherSister", false);
            DictAdoptionPersons.Add("Add_Dt", DateTime.Now);
            DictAdoptionPersons.Add("Update_Dt", DateTime.Now);
            DictAdoptionPersons.Add("Add_Ip", Request.UserHostAddress.IPToInteger());
            DictAdoptionPersons.Add("Update_Ip", Request.UserHostAddress.IPToInteger());

            int InsertID = DALC.InsertDatabase(_TableName, DictAdoptionPersons);
            if (InsertID < 1)
            {
                ErrorMessages(null, Config._DefaultSystemErrorMessages);
                return;
            }
        }
        //qardas ve baci elave etmek
        else if (!string.IsNullOrEmpty(OperationType))
        {
            DictAdoptionPersons.Add("ParentID", Convert.ToInt64(BtnSave.Attributes["data-parent"]));
            DictAdoptionPersons.Add("IsBrotherSister", true);
            DictAdoptionPersons.Add("Add_Dt", DateTime.Now);
            DictAdoptionPersons.Add("Update_Dt", DateTime.Now);
            DictAdoptionPersons.Add("Add_Ip", Request.UserHostAddress.IPToInteger());
            DictAdoptionPersons.Add("Update_Ip", Request.UserHostAddress.IPToInteger());

            int InsertID = DALC.InsertDatabase(_TableName, DictAdoptionPersons);
            if (InsertID < 1)
            {
                ErrorMessages(null, Config._DefaultSystemErrorMessages);
                return;
            }
            int UpdateID = DALC.UpdateDatabase(_TableName, new string[] { "IsBrotherSister", "WhereID" }, new object[] { true, int.Parse(ChildID) });
            if (UpdateID < 1)
            {
                ErrorMessages(null, Config._DefaultSystemErrorMessages);
                return;
            }
            ClearInputs();
        }

        //duzelis etmek
        else
        {
            bool IsBrotherSister = Convert.ToBoolean(Session["IsBrotherSister"]);

            DictAdoptionPersons.Add("ParentID", Convert.ToInt64(BtnSave.Attributes["data-parent"]));
            DictAdoptionPersons.Add("IsBrotherSister", IsBrotherSister);
            DictAdoptionPersons.Add("Update_Dt", DateTime.Now);
            DictAdoptionPersons.Add("Update_Ip", Request.UserHostAddress.IPToInteger());
            DictAdoptionPersons.Add("WHEREID", int.Parse(ChildID));

            int UpdateID = DALC.UpdateDatabase(_TableName, DictAdoptionPersons);
            if (UpdateID < 1)
            {
                ErrorMessages(null, Config._DefaultSystemErrorMessages);
                return;
            }
        }
        BindSisterBrother(Convert.ToInt64(BtnSave.Attributes["data-parent"]));

        BindImage();
        LoadImage();

        Config.Redirect("?p=adoptionpersonoperations&success=ok&id=" + Config._GetQueryString("id"));
    }

    protected void GrdSisterBrotherList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string SelectedRow = GrdSisterBrotherList.DataKeys[e.Row.RowIndex].Values["ID"].ToString();

            if (SelectedRow == ChildID)
            {
                e.Row.BackColor = System.Drawing.Color.FromArgb(1, 76, 175, 80);
                LinkButton LnkRemove = (LinkButton)e.Row.FindControl("LnkRemove");
                LnkRemove.Visible = false;
            }

        }
    }

    //baci qardasliqdan cixarmaq
    protected void LnkRemove_Click(object sender, EventArgs e)
    {
        Dictionary<string, object> DictChildList = new Dictionary<string, object>()
        {
            {"ParentID",DateTime.Now.Ticks },
            {"IsBrotherSister",false },
            {"WhereID",int.Parse((sender as LinkButton).CommandArgument) }
        };

        int CheckDelete = DALC.UpdateDatabase(_TableName, DictChildList);
        if (CheckDelete < 1)
        {
            Config.MsgBoxAjax("Sistemdə xəta baş verdi.", Page);
            return;
        }

        DataTable DtCheckChildBrotherSister = DALC.GetDataTableParams("*", _TableName, "ParentID", Convert.ToInt64(BtnSave.Attributes["data-parent"]), "");
        if (DtCheckChildBrotherSister.Rows.Count < 2)
        {
            Dictionary<string, object> DictChildBrotherSister = new Dictionary<string, object>()
            {
                {"IsBrotherSister",false },
                {"WhereID",int.Parse(ChildID) }
            };

            int CheckDelete2 = DALC.UpdateDatabase(_TableName, DictChildBrotherSister);
            if (CheckDelete2 < 1)
            {
                Config.MsgBoxAjax("Sistemdə xəta baş verdi.", Page);
                return;
            }
            PnlSisterBrotherList.Visible = false;
        }
        BindSisterBrother(Convert.ToInt64(BtnSave.Attributes["data-parent"]));
        BtnSearch_Click(null, null);
    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        PnlError.Visible = PnlResultSearch.Visible = PnlSuccess.Visible = false;
        if (!string.IsNullOrEmpty(TxtChildID.Text) && Config.IsNumeric(TxtChildID.Text))
        {
            if (TxtChildID.Text == ChildID)
            {
                PnlError.Visible = true;
                LtrErrorText.Text = "Axtardığınız şəxs hal-hazırda seçdiyiniz şəxsdir.";
                return;
            }
            DataTable DtChildList = DALC.GetDataTableParams("*,Concat (Soyad,' ',Ad,' ',Ata) as Fullname", _TableName, "ID", int.Parse(TxtChildID.Text), "");

            if (DtChildList.Rows.Count > 0)
            {
                PnlResultSearch.Visible = true;
                LblChildName.Text = DtChildList._Rows("Fullname");
                bool IsBrotherSister = Convert.ToBoolean(DtChildList._Rows("IsBrotherSister"));
                if (IsBrotherSister)
                {
                    LnkAddBrotherSister.Visible = false;
                    PnlError.Visible = true;
                    LtrErrorText.Text = "Bu istifadəçi artıq başqa istifadəçinin qardaş(bacı) siyahısında mövcuddur.";
                }
                else
                {
                    LnkAddBrotherSister.Visible = true;
                    PnlError.Visible = false;
                    LnkAddBrotherSister.CommandArgument = DtChildList._Rows("ID");
                }
            }
            else
            {
                PnlError.Visible = true;
                LtrErrorText.Text = "Axtarışa uyğun nəticə tapılmadı.";
            }
        }
    }

    protected void LnkAddBrotherSister_Click(object sender, EventArgs e)
    {
        long ParentID = Convert.ToInt64(BtnSave.Attributes["data-parent"]);
        Dictionary<string, object> DictUpdateAddingChild = new Dictionary<string, object>()
        {
            {"ParentID",ParentID },
            {"IsBrotherSister",true },
            {"WhereID",int.Parse(LnkAddBrotherSister.CommandArgument) }
        };

        DALC.Transaction Transaction = new DALC.Transaction();
        int CheckUpdate = DALC.UpdateDatabase(_TableName, DictUpdateAddingChild, Transaction);
        if (CheckUpdate < 1)
        {
            Config.MsgBoxAjax("Sistemdə xəta baş verdi.", Page);
            return;
        }

        Dictionary<string, object> DictUpdateCurrentChild = new Dictionary<string, object>()
        {
            {"IsBrotherSister",true },
            {"WhereID",int.Parse(ChildID) }
        };

        int CheckUpdate2 = DALC.UpdateDatabase(_TableName, DictUpdateCurrentChild, Transaction, true);
        if (CheckUpdate2 < 1)
        {
            Config.MsgBoxAjax("Sistemdə xəta baş verdi.", Page);
            return;
        }

        BindSisterBrother(ParentID);
        PnlSuccess.Visible = true;
        LnkAddBrotherSister.Visible = false;
        PnlSisterBrotherList.Visible = true;
    }
}