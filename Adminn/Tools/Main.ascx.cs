using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Adminn_Tools_Main : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TxtDescription.Text = DALC.GetAdminDescription(DALC._GetAdministratorsLogin.ID);
        }
    }

    protected void BtnSaveNote_Click(object sender, EventArgs e)
    {
        Dictionary<string, object> Dictionary = new Dictionary<string, object>();
        Dictionary.Add("Description", TxtDescription.Text);
        Dictionary.Add("WhereID", DALC._GetAdministratorsLogin.ID);

        int Chek = DALC.UpdateDatabase("Administrators", Dictionary);
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
            Config.MsgBoxAjax("Yeni şifərləriniz uyğun gəlmir!", Page);
            return;
        }

        if (Config.SHA1Special(TxtOldPassword.Text) != DALC.GetDbSingleValuesParams("Password", "Administrators", "ID", DALC._GetAdministratorsLogin.ID, "", ""))
        {
            Config.MsgBoxAjax("Köhnə şifrəniz yalnışdır", Page);
            return;
        }

        Dictionary<string, object> Dictionary = new Dictionary<string, object>();
        Dictionary.Add("WhereID", DALC._GetAdministratorsLogin.ID);
        Dictionary.Add("Password", Config.SHA1Special(TxtNewPassword.Text));

        int ChekUpdate = DALC.UpdateDatabase("Administrators", Dictionary);

        if (ChekUpdate < 1)
        {
            Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
        }
        else
        {
            DALC.AdministratorsHistoryInsert("Şifrəsini dəyişdi. Köhnə şifrə: " + TxtOldPassword.Text.Trim());
            Config.MsgBoxAjax("Əməliyyat uğurla yerinə yetirildi.", Page, true);
        }
    }
}