using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdoptionAdminn_Tools_Users : System.Web.UI.UserControl
{
    void ShowModal()
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "JqueryModal", "$('#myModal').modal('show');", true);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //PnlPermission.Height = Unit.Pixel(700);
            PnlAdministrators.Height = Unit.Pixel(700);

            DListPermissions.DataSource = DALC_Adoption.GetAdoptionAdministratorsStatus();
            DListPermissions.DataBind();
            DListPermissions.Items.Insert(0, new ListItem("--", "0"));

            LnkOtherApp_Click(null, null);
        }
    }

    protected void BtnFilter_Click(object sender, EventArgs e)
    {
        LnkOtherApp.CommandArgument = "0";
        LnkOtherApp_Click(null, null);
    }

    protected void BtnSaveUsers_Click(object sender, EventArgs e)
    {

        // Əgər yeni istifadəçi əlavə edilirsə login passwordu yoxlayaq
        if (string.IsNullOrEmpty(BtnSaveUsers.CommandArgument))
        {
            TxtUsername.Text = TxtUsername.Text.Trim();

            if (TxtUsername.Text.Length < 4)
            {
                ShowModal();
                TxtUsername.Focus();
                Config.MsgBoxAjax("İstifadəçi adı minimum 4 simvoldan ibarət olmalıdır.", Page);
                return;
            }

            if (TxtUsername.Text.Length > 25)
            {
                ShowModal();
                TxtUsername.Focus();
                Config.MsgBoxAjax("İstifadəçi adı maksimum 25 simvoldan ibarət olmalıdır.", Page);
                return;
            }

            string LoginAllowChars = "qazwsxedcrfvtgbyhnujmikolpQAZWSXEDCRFVTGBYHNUJMIKOLP0123456789._";
            for (int i = 0; i < TxtUsername.Text.Length; i++)
            {
                //İlk və son simvollar ancaq rəqəm hərf olsun.
                if (i == 0)
                {
                    if (TxtUsername.Text.Substring(i, 1) == "." || TxtUsername.Text.Substring(i, 1) == "_")
                    {
                        ShowModal();
                        TxtUsername.Focus();
                        Config.MsgBoxAjax("İstifadəçi adının ilk simvolu rəqəm və ya hərf tipli olmalıdır.", Page);
                        return;
                    }
                }

                if (LoginAllowChars.IndexOf(TxtUsername.Text.Substring(i, 1)) < 0)
                {
                    ShowModal();
                    TxtUsername.Focus();
                    Config.MsgBoxAjax("İstifadəçi adının tərkibində yalnız rəqəm (0-9), hərf (a-z), nöqtə (.) və alt xətt (_) qəbul edilir.", Page);
                    return;
                }

                if (i == (TxtUsername.Text.Length - 1))
                {
                    if (TxtUsername.Text.Substring(i, 1) == "." || TxtUsername.Text.Substring(i, 1) == "_")
                    {
                        ShowModal();
                        TxtUsername.Focus();
                        Config.MsgBoxAjax("İstifadəçi adının son simvolu rəqəm və ya hərf tipli olmalıdır.", Page);
                        return;
                    }
                }
            }

            //Login varmı:
            string LoginCount = DALC.GetDbSingleValuesParams("Count(*)", "AdoptionAdministrators", "IsActive=1 and Username", TxtUsername.Text, "", "-1");

            //Əgər mənfi bir olarsa serverdə yüklənmə var.
            if (LoginCount == "-1")
            {
                ShowModal();
                Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
                return;
            }

            //Əgər mənfi bir olmasa keçir bura və əgər sıfır deyilsə deməli bazada var 1 yada daha çoxdu
            if (LoginCount != "0")
            {
                ShowModal();
                Config.MsgBoxAjax("Bu istifadəçi adı artıq qeydiyyatdan keçirilib.", Page);
                return;
            }

            if (TxtPass.Text.Length < 4)
            {
                ShowModal();
                Config.MsgBoxAjax("İstifadəçi şifrəsi ən az 4 simvoldan ibarət olmalıdır.", Page);
                return;
            }
            if (TxtPass.Text.Length > 20)
            {
                ShowModal();
                Config.MsgBoxAjax("İstifadəçi şifrəsi maksimum 20 simvoldan ibarət olmalıdır.", Page);
                return;
            }
        }

        if (TxtFullname.Text.Length < 1)
        {
            ShowModal();
            Config.MsgBoxAjax("Soyadı, adı və atasının adını daxil edin.", Page);
            return;
        }

        if (!TxtEmail.Text.IsEmail())
        {
            ShowModal();
            Config.MsgBoxAjax("Elektron poçt ünvanını düzgün formatda daxil edin.", Page);
            return;
        }

        if (TxtContacts.Text.Trim().Length < 1)
        {
            ShowModal();
            Config.MsgBoxAjax("Telefon nömrəsini daxil edin.", Page);
            return;
        }

        if (!string.IsNullOrEmpty(BtnSaveUsers.CommandArgument))
        {
            Dictionary<string, object> DictUpdate = new Dictionary<string, object>();
            DictUpdate.Add("Username", TxtUsername.Text.Trim());
            DictUpdate.Add("Department", TxtDepartment.Text.Trim());
            DictUpdate.Add("Position", TxtPosition.Text.Trim());
            DictUpdate.Add("Fullname", TxtFullname.Text.Trim());
            DictUpdate.Add("Email", TxtEmail.Text.Trim());
            DictUpdate.Add("Contacts", TxtContacts.Text.Trim());
            DictUpdate.Add("PermissionIP", TxtPermissionIP.Text.Trim());
            DictUpdate.Add("AdoptionAdministratorsStatusID", int.Parse(DListPermissions.SelectedValue));
            DictUpdate.Add("Description", TxtDescription.Text.Trim());
            DictUpdate.Add("IsActive", ChkIsActive.Checked);
            DictUpdate.Add("Add_Dt", DateTime.Now);
            DictUpdate.Add("Add_Ip", Request.UserHostAddress.IPToInteger());
            DictUpdate.Add("WhereID", int.Parse(BtnSaveUsers.CommandArgument));

            int Chek = DALC.UpdateDatabase("AdoptionAdministrators", DictUpdate);
            if (Chek < 1)
            {
                ShowModal();
                Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
                return;
            }

            DALC_Adoption.AdoptionAdministratorsHistoryInsert("İstifadəçinin məlumatları yeniləndi. AdminID: " + BtnSaveUsers.CommandArgument);
        }
        else
        {
            Dictionary<string, object> Dictionary = new Dictionary<string, object>();
            Dictionary.Add("Username", TxtUsername.Text.Trim());
            Dictionary.Add("Password", Config.SHA1Special(TxtPass.Text));
            Dictionary.Add("Department", TxtDepartment.Text.Trim());
            Dictionary.Add("Position", TxtPosition.Text.Trim());
            Dictionary.Add("Fullname", TxtFullname.Text.Trim());
            Dictionary.Add("Email", TxtEmail.Text.Trim());
            Dictionary.Add("Contacts", TxtContacts.Text.Trim());
            Dictionary.Add("PermissionIP", TxtPermissionIP.Text.Trim());
            Dictionary.Add("AdoptionAdministratorsStatusID", int.Parse(DListPermissions.SelectedValue));
            Dictionary.Add("Description", TxtDescription.Text.Trim());
            Dictionary.Add("IsActive", ChkIsActive.Checked);
            Dictionary.Add("Add_Dt", DateTime.Now);
            Dictionary.Add("Add_Ip", Request.UserHostAddress.IPToInteger());

            int AdminID = DALC.InsertDatabase("AdoptionAdministrators", Dictionary);
            if (AdminID < 1)
            {
                ShowModal();
                Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
                return;
            }
            DALC_Adoption.AdoptionAdministratorsHistoryInsert("Yeni istifadəçi əlavə edildi. AdminID: " + AdminID.ToString());
        }

        LnkOtherApp_Click(null, null);
        Config.MsgBoxAjax("Əməliyyat uğurla yerinə yetirildi.", Page, true);
    }

    protected void BtnResetPassword_Click(object sender, EventArgs e)
    {
        string ResetPassword = Config.Key(6);
        Dictionary<string, object> Dictionary = new Dictionary<string, object>();
        Dictionary.Add("Password", ResetPassword.SHA1Special());
        Dictionary.Add("WhereID", int.Parse(BtnSaveUsers.CommandArgument));

        int Chek = DALC.UpdateDatabase("AdoptionAdministrators", Dictionary);
        if (Chek < 1)
        {
            Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
            return;
        }

        DALC_Adoption.AdoptionAdministratorsHistoryInsert(BtnSaveUsers.CommandArgument + " № li İstifadəçinin şifərsi dəyişdirildi(Reset Password). Yeni şifrə: " + ResetPassword.Substring(0, 3) + "***");
        Config.MsgBoxAjax("İstfifadəçi şifrəsi yeniləndi. ", Page, true);
        LblPass.Text = "Yeni şifrə: <b>" + ResetPassword + "</b>";
    }

    protected void LnkAdd_Click(object sender, EventArgs e)
    {
        LtrModalHeader1.Text = "Yeni istifadəçi";
        BtnSaveUsers.CommandArgument = "";
        PnlResetPassword.Visible = false;
        TxtUsername.Enabled = true;
        PnlPassword.Visible = true;
        TxtUsername.Text = TxtPass.Text = TxtFullname.Text = TxtEmail.Text = TxtContacts.Text = TxtDescription.Text = "";
        ShowModal();
    }

    protected void LnkOtherApp_Click(object sender, EventArgs e)
    {
        GrdList.DataSource = null;
        GrdList.DataBind();

        LnkOtherApp.Visible = false;
        LblCount.Text = "Axtarış üzrə: 0      Səhifə üzrə: 0";
        LnkOtherApp.CommandArgument = (int.Parse(LnkOtherApp.CommandArgument) + 20)._ToString();


        Dictionary<string, object> Dictionary = new Dictionary<string, object>()
        {
            {"Username(LIKE)",TxtFilterUsername.Text },
            {"Fullname(LIKE)", TxtFilterFullname.Text.Replace("i","İ").Replace("ı","I").ToUpper()},
            {"IsActive" , int.Parse(DListFilterStatus.SelectedValue)}
        };

        DALC.DataTableResult AdoptionAdministrators = DALC_Adoption.GetAdoptionAdministrator(int.Parse(LnkOtherApp.CommandArgument), Dictionary, "");

        if (AdoptionAdministrators.Count == -1)
        {
            Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
            return;
        }

        GrdList.DataSource = AdoptionAdministrators.Dt;
        GrdList.DataBind();

        LblCount.Text = "Axtarış üzrə: " + AdoptionAdministrators.Count + "      Səhifə üzrə: " + GrdList.Rows.Count._ToString();

        if (AdoptionAdministrators.Dt.Rows.Count > 0)
            LnkOtherApp.Visible = (GrdList.Rows.Count < AdoptionAdministrators.Count);
    }

    protected void LnkEdit_Click(object sender, EventArgs e)
    {
        ShowModal();
        LtrModalHeader1.Text = "Düzəliş";
        PnlResetPassword.Visible = true;
        ChkIsActive.Enabled = true;
        string AdminID = BtnSaveUsers.CommandArgument = (sender as LinkButton).CommandArgument;
        DataTable Dt = DALC_Adoption.GetAdministratorByID(AdminID._ToInt32());
        if (Dt == null)
        {
            Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
            return;
        }

        if (Dt.Rows.Count < 1)
        {
            Config.MsgBoxAjax("Məlumat tapılmadı.", Page);
            return;
        }

        TxtUsername.Enabled = false;
        PnlPassword.Visible = false;
        TxtUsername.Text = Dt._Rows("Username");
        TxtDepartment.Text = Dt._Rows("Department");
        TxtPosition.Text = Dt._Rows("Position");
        TxtFullname.Text = Dt._Rows("Fullname");
        TxtEmail.Text = Dt._Rows("Email");
        TxtContacts.Text = Dt._Rows("Contacts");
        TxtDescription.Text = Dt._Rows("Description");
        TxtPermissionIP.Text = Dt._Rows("PermissionIP");
        ChkIsActive.Checked = Convert.ToBoolean(Dt._Rows("IsActive"));
        if (string.IsNullOrEmpty(DListPermissions.SelectedValue))
        {
            DListPermissions.SelectedIndex = 0;
        }
        else
        {
            DListPermissions.SelectedValue = Dt._Rows("AdoptionAdministratorsStatusID");
        }
        LblPass.Text = "";
    }

}