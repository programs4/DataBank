using System;
using System.Collections.Generic;
using System.Data;
using System.Web;

public partial class Detail_Contents_310_Olum : System.Web.UI.UserControl
{
    int _PersonsID = 0;
    string _TableName = "Persons";

    #region Soragcalar
    //Sorgacalar
    public void Aylar()
    {
        DListOlumD.DataSource = Config.NumericList(1, 31);
        DListOlumD.DataBind();

        DListOlumM.DataSource = Config.MonthList();
        DListOlumM.DataBind();

        DListOlumY.DataSource = Config.NumericList(1980, DateTime.Now.Year, "1000");
        DListOlumY.DataBind();

    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        //Sadəcə alırıq. Yoxlamağa ehtiyac yoxdur. Content/Default.aspx də yoxlanılır.
        _PersonsID = Config._GetQueryString("i").QueryIdDecrypt()._ToInt32();

        //Silinmə əməliyyatına icazə varmı?
        if (!DALC._IsPermissionTypeEdit(430))
        {
            PnlEdit.Visible = false;
        }

        if (!IsPostBack)
        {
            Aylar();

            //Bind forms
            DataTable Dt = DALC.GetDataTableParams("TOP 1 *", "Persons", "ID", _PersonsID, "");

            if (Dt == null || Dt.Rows.Count < 1)
            {
                Config.RedirectError();
                return;
            }

            DListOlum.SelectedValue = Dt._Rows("IsOlum").IsEmptyReplaceNoul("False");
            TxtOlumQeydiyyat.Text = Dt._Rows("OlumQeydiyyati");
            DListOlumD.SelectedValue = Dt._Rows("OlumTarixi").IntDateDay();
            DListOlumM.SelectedValue = Dt._Rows("OlumTarixi").IntDateMonth();
            DListOlumY.SelectedValue = Dt._Rows("OlumTarixi").IntDateYear();
        }
    }

    protected void LnkSuccess_Click(object sender, EventArgs e)
    {
        string LogText = "Uşağın ölümünün haqqında məlumatda dəyişiklik edildi. № {0}";
        int HistoryStatus = 30;

        Dictionary<string, object> Dictionary = new Dictionary<string, object>
        {
            { "OrganizationsID", DALC._GetUsersLogin.OrganizationsID },
            { "UsersID", DALC._GetUsersLogin.ID },
            { "IsOlum", DListOlum.SelectedValue.NullConvert() },
            { "OlumQeydiyyati", TxtOlumQeydiyyat.Text },
            { "OlumTarixi", (DListOlumY.SelectedValue + DListOlumM.SelectedValue + DListOlumD.SelectedValue).NullConvert() },
            { "Last_Dt", DateTime.Now },
            { "Last_Ip", HttpContext.Current.Request.UserHostAddress.IPToInteger() },
            { "WhereID", _PersonsID }
        };

        int ChekUpdate = DALC.UpdateDatabase(_TableName, Dictionary);
        if (ChekUpdate < 1)
        {
            Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
            return;
        }

        DALC.UsersHistoryInsert(HistoryStatus, 430, string.Format(LogText, _PersonsID), _PersonsID);
        Config.MsgBoxAjax("Əməliyyat uğurla yerinə yetirildi.", Page, true);
    }
}