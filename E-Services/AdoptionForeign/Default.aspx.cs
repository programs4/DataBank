using System;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class E_Services_AdoptionForeign_Default : System.Web.UI.Page
{
    string lang = "";
    string ImportantIcon = "<span style=\"color: red;\">*</span>";
    private void BindLists()
    {
        DListBirthPlace.DataSource = DALC.GetDataTable("ID,Name", "Countries", "");
        DListBirthPlace.DataBind();
        DListBirthPlace.Items.Insert(0, new ListItem(" -- ", "-1"));


        DListNationality.DataSource = DALC.GetCitizenship();
        DListNationality.DataBind();
        DListNationality.Items.Insert(0, new ListItem(" -- ", "-1"));
    }

    #region checkLang
    private void BindLanguage(string Lang)
    {
        switch (Lang)
        {
            case "az":
                break;
            case "en":
                LblName.Text = "Name:" + ImportantIcon;
                LblSurname.Text = "Surname:" + ImportantIcon;
                LblPatronymic.Text = "Patronymic:";
                LblBirthDate.Text = "Birth date:" + ImportantIcon;
                LblBirthPlace.Text = "Birth Place:" + ImportantIcon;
                LblCause.Text = "Reason for adopting a child:";
                LblChildAge.Text = "Age:" + ImportantIcon;
                LblChildGender.Text = "Gender:" + ImportantIcon;
                LblCurrentResidence.Text = "Current Residence";
                LblEducation.Text = "Education:";
                LblEmail.Text = "Email:" + ImportantIcon;
                LblGender.Text = "Gender:" + ImportantIcon;
                LblHealthStatus.Text = "Health Status:";
                LblMarriedStatus.Text = "Married Status:";
                LblMobileNumber.Text = "Mobile Number:" + ImportantIcon;
                LblNationality.Text = "Nationality:" + ImportantIcon;
                LblPhoneNumber.Text = "Phone Number:";
                LblRegisteredAddress.Text = "Registered Address:";
                BtnConfirm.Text = "Confirm:";
                LtrSubTitle.Text = "Signs of the child to be adopted";
                LblDocIDTitle.Text = "Copy of ID card:";
                LblWorkTitle.Text = "Workplace document:";
                LblMaritalStatusTitle.Text = "<b>Document confirming marital status</b>" + "<br/>" + " (copy of marriage certificate or marriage certificate breach)";
                break;
            case "ru":
                LblName.Text = "Name:" + ImportantIcon;
                LblSurname.Text = "Surname:" + ImportantIcon;
                LblPatronymic.Text = "Patronymic:";
                LblBirthDate.Text = "Birth date:" + ImportantIcon;
                LblBirthPlace.Text = "Birth Place:" + ImportantIcon;
                LblCause.Text = "Reason for adopting a child:";
                LblChildAge.Text = "Age:" + ImportantIcon;
                LblChildGender.Text = "Gender:" + ImportantIcon;
                LblCurrentResidence.Text = "Current Residence";
                LblEducation.Text = "Education:";
                LblEmail.Text = "Email:" + ImportantIcon;
                LblGender.Text = "Gender:" + ImportantIcon;
                LblHealthStatus.Text = "Health Status:";
                LblMarriedStatus.Text = "Married Status:";
                LblMobileNumber.Text = "Mobile Number:" + ImportantIcon;
                LblNationality.Text = "Nationality:" + ImportantIcon;
                LblPhoneNumber.Text = "Phone Number:";
                LblRegisteredAddress.Text = "Registered Address:";
                BtnConfirm.Text = "Confirm:";
                LtrSubTitle.Text = "Signs of the child to be adopted";
                LblDocIDTitle.Text = "Copy of ID card:";
                LblWorkTitle.Text = "Workplace document:";
                LblMaritalStatusTitle.Text = "<b>Document confirming marital status</b>" + "<br/>" + " (copy of marriage certificate or marriage certificate breach)";
                break;
            default:
                Config.RedirectError();
                break;
        }
    }
    #endregion

    //TextBox errorlar
    public void ErrorMessagesTextbox(TextBox T, string Text)
    {
        if (T != null)
        {
            T.Focus();
            T.CssClass += " error";
        }

        T.Focus();
        PnlAlert.Visible = true;
        LtrAlert.Text = Text;
        return;
    }

    //Droplist errorlar
    public void ErrorMessagesDropList(DropDownList D, string Text)
    {
        if (D != null)
        {
            D.Focus();
            D.CssClass += " error";
        }

        PnlAlert.Visible = true;
        LtrAlert.Text = Text;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        lang = Config._GetQueryString("lang");

        if (!IsPostBack)
        {
            BindLists();

            if (string.IsNullOrEmpty(lang))
            {
                Config.Redirect("/e-services/adoptionforeign/?lang=az");
                return;
            }

            BindLanguage(lang);
        }
    }

    protected void BtnConfirm_Click(object sender, EventArgs e)
    {
        lang = Config._GetQueryString("lang");
        string ErrorMessage = "";
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "CallDatePicker", "CallDatePicker();", true);
        TxtName.CssClass = TxtSurname.CssClass = TxtMobileNumber.CssClass = TxtEmail.CssClass = "form-control form-inputs";
        TxtBirthDate.CssClass = "form-control form_datetime form-inputs";
        DListBirthPlace.CssClass = DListGender.CssClass = DListNationality.CssClass = DListChildAge.CssClass = DListChildGender.CssClass = "form-control input-filter form-inputs";
        PnlAlert.Visible = false;

        #region Validations
        try
        {
            dynamic capthaResult = Newtonsoft.Json.Linq.JObject.Parse(DALC_Adoption.WebRequestMethod(string.Format(Config._GetAppSettings("GoogleCapthaValid").Decrypt(), Request["g-recaptcha-response"]._ToString(), Request.UserHostAddress), string.Empty));
            if (!(bool)capthaResult.success)
            {
                if (lang == "az")
                {
                    ErrorMessage = "Təhlükəsizlik qutusunu işarələyin.";
                }
                else if (lang == "ru")
                {
                    ErrorMessage = "Təhlükəsizlik qutusunu işarələyin.";
                }
                else if (lang == "en")
                {
                    ErrorMessage = "Please check the security box.";
                }
                PnlAlert.Visible = true;
                LtrAlert.Text = ErrorMessage;
                return;
            }
        }
        catch (Exception er)
        {
            DALC.ErrorLogsInsert(string.Format("Google captcha catch error TableName: {0} count xəta: {1}", "AdoptionForeign", er.Message));
            Response.Write(Config._DefaultSystemErrorMessages);
            Response.End();
            return;
        }

        if (string.IsNullOrEmpty(TxtName.Text))
        {
            if (lang == "az")
            {
                ErrorMessage = "Adı qeyd edin.";
            }
            else if (lang == "ru")
            {
                ErrorMessage = "Adı qeyd edin.";
            }
            else if (lang == "en")
            {
                ErrorMessage = "Please type the name.";
            }
            ErrorMessagesTextbox(TxtName, ErrorMessage);
            return;
        }

        if (string.IsNullOrEmpty(TxtSurname.Text))
        {
            if (lang == "az")
            {
                ErrorMessage = "Soyadı qeyd edin.";
            }
            else if (lang == "ru")
            {
                ErrorMessage = "Soyadı qeyd edin.";
            }
            else if (lang == "en")
            {
                ErrorMessage = "Please type the surname.";
            }
            ErrorMessagesTextbox(TxtSurname, ErrorMessage);
            return;
        }

        if (DListGender.SelectedValue == "-1")
        {
            if (lang == "az")
            {
                ErrorMessage = "Cins seçilməyib.";
            }
            else if (lang == "ru")
            {
                ErrorMessage = "Cins seçilməyib.";
            }
            else if (lang == "en")
            {
                ErrorMessage = "Cins seçilməyib.";
            }
            ErrorMessagesDropList(DListGender, ErrorMessage);
            return;
        }

        if (string.IsNullOrEmpty(TxtBirthDate.Text))
        {
            if (lang == "az")
            {
                ErrorMessage = "Doğum tarixini qeyd edin.";
            }
            else if (lang == "ru")
            {
                ErrorMessage = "Doğum tarixini qeyd edin.";
            }
            else if (lang == "en")
            {
                ErrorMessage = "Doğum tarixini qeyd edin.";
            }
            ErrorMessagesTextbox(TxtBirthDate, ErrorMessage);
            return;
        }

        if (Config.DateFormatClear(TxtBirthDate.Text) == null)
        {
            if (lang == "az")
            {
                ErrorMessage = "Doğum tarixini düzgün formatda daxil edin.";
            }
            else if (lang == "ru")
            {
                ErrorMessage = "Doğum tarixini düzgün formatda daxil edin.";
            }
            else if (lang == "en")
            {
                ErrorMessage = "Doğum tarixini düzgün formatda daxil edin.";
            }
            ErrorMessagesTextbox(TxtBirthDate, ErrorMessage);
            return;
        }

        if (DListBirthPlace.SelectedValue == "-1")
        {
            if (lang == "az")
            {
                ErrorMessage = "Doğum yerini qeyd edin.";
            }
            else if (lang == "ru")
            {
                ErrorMessage = "Doğum yerini qeyd edin.";
            }
            else if (lang == "en")
            {
                ErrorMessage = "Doğum yerini qeyd edin.";
            }
            ErrorMessagesDropList(DListBirthPlace, ErrorMessage);
            return;
        }

        if (DListNationality.SelectedValue == "-1")
        {
            if (lang == "az")
            {
                ErrorMessage = "Vətəndaşlığı qeyd edin.";
            }
            else if (lang == "ru")
            {
                ErrorMessage = "Vətəndaşlığı qeyd edin.";
            }
            else if (lang == "en")
            {
                ErrorMessage = "Vətəndaşlığı qeyd edin.";
            }
            ErrorMessagesDropList(DListNationality, ErrorMessage);
            return;
        }

        if (string.IsNullOrEmpty(TxtMobileNumber.Text))
        {
            if (lang == "az")
            {
                ErrorMessage = "Əlaqə nömrəsini qeyd edin.";
            }
            else if (lang == "ru")
            {
                ErrorMessage = "Əlaqə nömrəsini qeyd edin.";
            }
            else if (lang == "en")
            {
                ErrorMessage = "Əlaqə nömrəsini qeyd edin.";
            }
            ErrorMessagesTextbox(TxtMobileNumber, ErrorMessage);
            return;
        }

        if (!Config.IsNumeric(TxtMobileNumber.Text))
        {
            if (lang == "az")
            {
                ErrorMessage = "Əlaqə nömrəsini düzgün formatda daxil edin.";
            }
            else if (lang == "ru")
            {
                ErrorMessage = "Əlaqə nömrəsini düzgün formatda daxil edin.";
            }
            else if (lang == "en")
            {
                ErrorMessage = "Əlaqə nömrəsini düzgün formatda daxil edin.";
            }
            ErrorMessagesTextbox(TxtMobileNumber, ErrorMessage);
            return;
        }

        if (string.IsNullOrEmpty(TxtEmail.Text))
        {
            if (lang == "az")
            {
                ErrorMessage = "Elektron ünvanı qeyd edin.";
            }
            else if (lang == "ru")
            {
                ErrorMessage = "Elektron ünvanı qeyd edin.";
            }
            else if (lang == "en")
            {
                ErrorMessage = "Elektron ünvanı qeyd edin.";
            }
            ErrorMessagesTextbox(TxtEmail, ErrorMessage);
            return;
        }

        if (!Config.IsEmail(TxtEmail.Text))
        {
            if (lang == "az")
            {
                ErrorMessage = "Elektron ünvanı düzgün formatda daxil edin.";
            }
            else if (lang == "ru")
            {
                ErrorMessage = "Elektron ünvanı düzgün formatda daxil edin.";
            }
            else if (lang == "en")
            {
                ErrorMessage = "Elektron ünvanı düzgün formatda daxil edin.";
            }
            ErrorMessagesTextbox(TxtEmail, ErrorMessage);
            return;
        }

        if (DListChildAge.SelectedValue == "-1")
        {
            if (lang == "az")
            {
                ErrorMessage = "Uşağın yaşını qeyd edin.";
            }
            else if (lang == "ru")
            {
                ErrorMessage = "Uşağın yaşını qeyd edin.";
            }
            else if (lang == "en")
            {
                ErrorMessage = "Uşağın yaşını qeyd edin.";
            }
            ErrorMessagesDropList(DListChildAge, ErrorMessage);
            return;
        }

        if (DListChildGender.SelectedValue == "-1")
        {
            if (lang == "az")
            {
                ErrorMessage = "Uşağın cinsini qeyd edin.";
            }
            else if (lang == "ru")
            {
                ErrorMessage = "Uşağın cinsini qeyd edin.";
            }
            else if (lang == "en")
            {
                ErrorMessage = "Uşağın cinsini qeyd edin.";
            }
            ErrorMessagesDropList(DListChildGender, ErrorMessage);
            return;
        }

        #endregion

        string GeneratedHtml = string.Format(LblName.Text.Replace(ImportantIcon, "") + TxtName.Text + "{0}" + LblSurname.Text.Replace(ImportantIcon, "") + TxtSurname.Text + "{0}" + LblPatronymic.Text + TxtPatronymic.Text +
            "{0}" + LblGender.Text.Replace(ImportantIcon, "") + DListGender.SelectedItem.Text + "{0}" + LblBirthDate.Text.Replace(ImportantIcon, "") + Config.DateFormatClear(TxtBirthDate.Text) +
            "{0}" + LblBirthPlace.Text.Replace(ImportantIcon, "") + DListBirthPlace.SelectedItem.Text + "{0}" + LblNationality.Text.Replace(ImportantIcon, "") + DListNationality.SelectedItem.Text +
            "{0}" + LblRegisteredAddress.Text.Replace(ImportantIcon, "") + TxtRegisteredAddress.Text + "{0}" + LblCurrentResidence.Text.Replace(ImportantIcon, "") + TxtCurrentResidence.Text +
            "{0}" + LblPhoneNumber.Text.Replace(ImportantIcon, "") + TxtPhoneNumber.Text + "{0}" + LblMobileNumber.Text.Replace(ImportantIcon, "") + TxtMobileNumber.Text +
            "{0}" + LblEmail.Text.Replace(ImportantIcon, "") + TxtEmail.Text + "{0}" + LblEducation.Text.Replace(ImportantIcon, "") + DListEducation.SelectedItem.Text +
            "{0}" + LblMarriedStatus.Text.Replace(ImportantIcon, "") + DListMarriedStatus.SelectedItem.Text + "<br/>" + "<strong>" + LtrSubTitle.Text + "</strong><br/>" +
            LblChildAge.Text.Replace(ImportantIcon, "") + DListChildAge.SelectedItem.Text + "{0}" + LblChildGender.Text.Replace(ImportantIcon, "") + DListChildGender.SelectedItem.Text +
            "{0}" + LblHealthStatus.Text.Replace(ImportantIcon, "") + DListHealthStatus.SelectedItem.Text + "{0}" + LblCause.Text.Replace(ImportantIcon, "") + TxtCause.Text, ",<br/>");
    }
}