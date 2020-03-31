using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;

public partial class Detail_Contents_320_HuquqMuraciet : System.Web.UI.UserControl
{
    int _PersonsID = 0;
    string _TableName = "PersonsHuquqMuraciet";

    string[] _SqlSelectColumns =
                               {
                                   "Sebeb",
                                   Config.SqlDateTimeFormat("IcraatTarixi"),
                                   Config.SqlDateTimeFormat("XitamTarixi"),
                                   "Neticesi",
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
        DListIcraatTarixiD.DataSource = DListXitamTarixiD.DataSource = Config.NumericList(1, 31);
        DListIcraatTarixiD.DataBind();
        DListXitamTarixiD.DataBind();

        DListIcraatTarixiM.DataSource = DListXitamTarixiM.DataSource = Config.MonthList();
        DListIcraatTarixiM.DataBind();
        DListXitamTarixiM.DataBind();

        DListIcraatTarixiY.DataSource = DListXitamTarixiY.DataSource = Config.NumericList(1980, DateTime.Now.Year, "1000");
        DListIcraatTarixiY.DataBind();
        DListXitamTarixiY.DataBind();
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
        if (!DALC._IsPermissionTypeEdit(440))
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

        // Təmizləmə işləri
        TxtSebeb.Text = TxtDescription.Text = "";
        DListIcraatTarixiD.SelectedValue = DListIcraatTarixiM.SelectedValue = DListXitamTarixiD.SelectedValue = DListXitamTarixiM.SelectedValue = "00";
        DListIcraatTarixiY.SelectedValue = DListXitamTarixiY.SelectedValue = "1000";

        GrdList.SelectedIndex = -1;
        PnlEdit.Visible = true;
    }

    protected void LnkSuccess_Click(object sender, EventArgs e)
    {
        string ID = "";
        string LogText = "";
        int HistoryStatus = 20;

        if (TxtSebeb.Text.Trim().Length < 1)
        {
            Config.MsgBoxAjax("Müraciətin səbəbi mütləq qeyd olunmalıdır.", Page);
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

            LogText = "Uşağın hüquqları ilə bağlı qəbul edilmiş ərizə, şikayət və müraciətlərin haqqında yeni məlumat əlavə edildi. № {0}";
        }
        else
        {
            ID = GrdList.SelectedDataKey["ID"]._ToString();
            LogText = "Uşağın hüquqları ilə bağlı qəbul edilmiş ərizə, şikayət və müraciətlərin haqqında məlumatda düzəliş edildi. № {0}";
            HistoryStatus = 30;
        }

        Dictionary<string, object> Dictionary = new Dictionary<string, object>
        {
            { "OrganizationsID", DALC._GetUsersLogin.OrganizationsID },
            { "UsersID", DALC._GetUsersLogin.ID },
            { "Sebeb", TxtSebeb.Text.Trim() },
            { "IcraatTarixi", (DListIcraatTarixiY.SelectedValue + DListIcraatTarixiM.SelectedValue + DListIcraatTarixiD.SelectedValue).NullConvert() },
            { "XitamTarixi", (DListXitamTarixiY.SelectedValue + DListXitamTarixiM.SelectedValue + DListXitamTarixiD.SelectedValue).NullConvert() },
            { "Neticesi", TxtNetice.Text.Trim() },
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

        DALC.UsersHistoryInsert(HistoryStatus, 440, string.Format(LogText, ID), _PersonsID);
        Config.MsgBoxAjax("Əməliyyat uğurla yerinə yetirildi.", Page, true);
        PnlEdit.Visible = false;
        GridBind();
    }

    protected void GrdList_SelectedIndexChanged(object sender, EventArgs e)
    {
        PnlEdit.Visible = true;
        Aylar();

        DataTable Dt = DALC.GetDataTableParams("*", _TableName, "ID", GrdList.SelectedDataKey["ID"]._ToString(), "");

        if (Dt == null || Dt.Rows.Count < 1)
        {
            Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
            return;
        }

        TxtSebeb.Text = Dt._Rows("Sebeb");
        DListIcraatTarixiD.SelectedValue = Dt._RowsObject("IcraatTarixi").IntDateDay();
        DListIcraatTarixiM.SelectedValue = Dt._RowsObject("IcraatTarixi").IntDateMonth();
        DListIcraatTarixiY.SelectedValue = Dt._RowsObject("IcraatTarixi").IntDateYear();

        DListXitamTarixiD.SelectedValue = Dt._RowsObject("XitamTarixi").IntDateDay();
        DListXitamTarixiM.SelectedValue = Dt._RowsObject("XitamTarixi").IntDateMonth();
        DListXitamTarixiY.SelectedValue = Dt._RowsObject("XitamTarixi").IntDateYear();
        TxtNetice.Text = Dt._Rows("Neticesi");
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
        DALC.UsersHistoryInsert(40, 440, string.Format("Uşağın hüquqları ilə bağlı qəbul edilmiş ərizə, şikayət və müraciətlər haqqında məlumat silindi. № {0}", ID), _PersonsID);
    }

    protected void LnkCancel_Click(object sender, EventArgs e)
    {
        PnlEdit.Visible = false;
    }
}