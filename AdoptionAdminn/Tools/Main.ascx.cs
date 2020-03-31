using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;

public partial class AdoptionAdminn_Tools_Main : UserControl
{
    private void BindContent()
    {
        string ContentID;
        DataTable DtContent = DALC.GetDataTableParams("*", "Contents", "ID", 1, "");
        if (DtContent.Rows.Count < 1)
        {
            Config.MsgBoxAjax("Əməliyyat zamanı xəta baş verdi.", Page, false);
            return;
        }
        ContentID = DtContent._Rows("ID");
        TxtContentTitle.Text = DtContent._Rows("Title");
        TxtContent.Text = DtContent._Rows("ContentText");
        BtnSaveContent.CommandArgument = ContentID;

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TxtDescription.Text = DALC.GetDbSingleValuesParams("Description", "AdoptionAdministrators", "ID", DALC_Adoption._GetAdoptionAdministratorsLogin.ID, "", "--");
            BindContent();
        }
    }

    protected void BtnSaveNote_Click(object sender, EventArgs e)
    {
        Dictionary<string, object> Dictionary = new Dictionary<string, object>();
        Dictionary.Add("Description", TxtDescription.Text);
        Dictionary.Add("WhereID", DALC_Adoption._GetAdoptionAdministratorsLogin.ID);

        int Chek = DALC.UpdateDatabase("AdoptionAdministrators", Dictionary);
        if (Chek < 1)
        {
            Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page, false);
            return;
        }

        Config.MsgBoxAjax("Məlumat yeniləndi.", Page, true);
    }

    protected void BtnConfirm_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "TabChange", "$(document).ready(function(){$('#password-tab').click();});", true);
        if (TxtOldPassword.Text.Length < 1)
        {
            Config.MsgBoxAjax("Köhnə şifrəni daxil edin.", Page);
            return;
        }

        if (TxtNewPassword.Text.Length < 4)
        {
            Config.MsgBoxAjax("Yeni şifrə ən az 4 simvoldan ibarət olmalıdır.", Page);
            return;
        }

        if (TxtNewPassword.Text != TxtBackPassword.Text)
        {
            Config.MsgBoxAjax("Yeni şifrələr uyğun gəlmir!", Page);
            return;
        }

        if (Config.SHA1Special(TxtOldPassword.Text) != DALC.GetDbSingleValuesParams("Password", "AdoptionAdministrators", "ID", DALC_Adoption._GetAdoptionAdministratorsLogin.ID, "", ""))
        {
            Config.MsgBoxAjax("Köhnə şifrəniz yalnışdır", Page);
            return;
        }

        Dictionary<string, object> Dictionary = new Dictionary<string, object>();
        Dictionary.Add("WhereID", DALC_Adoption._GetAdoptionAdministratorsLogin.ID);
        Dictionary.Add("Password", Config.SHA1Special(TxtNewPassword.Text));

        int ChekUpdate = DALC.UpdateDatabase("AdoptionAdministrators", Dictionary);

        if (ChekUpdate < 1)
        {
            Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
        }
        else
        {
            DALC_Adoption.AdoptionAdministratorsHistoryInsert("Şifrəsini dəyişdi. Köhnə şifrə: " + TxtOldPassword.Text.Trim());
            Config.MsgBoxAjax("Əməliyyat uğurla yerinə yetirildi.", Page, true);
        }
    }

    protected void BtnSaveContent_Click(object sender, EventArgs e)
    {
        Dictionary<string, object> DictContent = new Dictionary<string, object>();
        DictContent.Add("Title", TxtContentTitle.Text);
        DictContent.Add("ContentText", TxtContent.Text);
        DictContent.Add("Add_Dt", DateTime.Now);
        DictContent.Add("Update_Dt", DateTime.Now);
        DictContent.Add("WhereID", int.Parse(BtnSaveContent.CommandArgument));

        int Chek = DALC.UpdateDatabase("Contents", DictContent);
        if (Chek < 1)
        {
            Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page, false);
            return;
        }

        Config.MsgBoxAjax("Məlumat yeniləndi.", Page, true);
    }
}