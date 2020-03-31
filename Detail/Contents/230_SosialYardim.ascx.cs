using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;

public partial class Detail_Contents_230_SosialYardim : System.Web.UI.UserControl
{
    int _PersonsID = 0;
    string _TableName = "PersonsSosialYardim";

    string[] _SqlSelectColumns =
                               {
                                   "Mebleg",
                                   "Muddet",
                                   Config.SqlDateTimeFormat("YardimaBaslamaTarixi"),
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
    public void Aylar()
    {
        DListSosialYardimD.DataSource = Config.NumericList(1, 31);
        DListSosialYardimD.DataBind();

        DListSosialYardimM.DataSource = Config.MonthList();
        DListSosialYardimM.DataBind();

        DListSosialYardimY.DataSource = Config.NumericList(1980, DateTime.Now.Year, "1000");
        DListSosialYardimY.DataBind();

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //Sadəcə alırıq. Yoxlamağa ehtiyac yoxdur. Content/Default.aspx də yoxlanılır.
        _PersonsID = Config._GetQueryString("i").QueryIdDecrypt()._ToInt32();

        if (!IsPostBack)
        {
            GridBind();
        }

        //Silinmə əməliyyatına icazə varmı?
        if (!DALC._IsPermissionTypeEdit(350))
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
        TxtMebleg.Text = TxtMuddet.Text = TxtDescription.Text = "";
        DListSosialYardimD.SelectedValue = DListSosialYardimM.SelectedValue = "00";
        DListSosialYardimY.SelectedValue = "1000";

        GrdList.SelectedIndex = -1;
        PnlEdit.Visible = true;
    }

    protected void LnkSuccess_Click(object sender, EventArgs e)
    {
        string ID = "";
        string LogText = "";
        int HistoryStatus = 20;

        TxtMebleg.Text = TxtMebleg.Text.Trim().Replace(",", ".");
        if (TxtMebleg.Text.Trim().Length < 1 || !TxtMebleg.Text.Replace(".", "").IsNumeric())
        {
            Config.MsgBoxAjax("Məbləğ mütləq rəqəm tipində qeyd olunmalıdır.", Page);
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

            LogText = "Uşağa ünvanlı sosial yardımın verilməsi haqqında yeni məlumat əlavə edildi. № {0}";
        }
        else
        {
            ID = GrdList.SelectedDataKey["ID"]._ToString();
            LogText = "Uşağa ünvanlı sosial yardımın verilməsi haqqında məlumatda düzəliş edildi. № {0}";
            HistoryStatus = 30;
        }

        Dictionary<string, object> Dictionary = new Dictionary<string, object>
        {
            { "OrganizationsID", DALC._GetUsersLogin.OrganizationsID },
            { "UsersID", DALC._GetUsersLogin.ID },
            { "Mebleg", TxtMebleg.Text.Trim() },
            { "YardimaBaslamaTarixi", (DListSosialYardimY.SelectedValue + DListSosialYardimM.SelectedValue + DListSosialYardimD.SelectedValue).NullConvert() },
            { "Muddet", TxtMuddet.Text.Trim() },
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

        DALC.UsersHistoryInsert(HistoryStatus, 350, string.Format(LogText, ID), _PersonsID);
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

        TxtMebleg.Text = Dt._Rows("Mebleg");
        TxtMuddet.Text = Dt._Rows("Muddet");
        DListSosialYardimD.SelectedValue = Dt._RowsObject("YardimaBaslamaTarixi").IntDateDay();
        DListSosialYardimM.SelectedValue = Dt._RowsObject("YardimaBaslamaTarixi").IntDateMonth();
        DListSosialYardimY.SelectedValue = Dt._RowsObject("YardimaBaslamaTarixi").IntDateYear();
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
        DALC.UsersHistoryInsert(40, 350, string.Format("Uşağa ünvanlı sosial yardımın verilməsi haqqında məlumat silindi. № {0}", ID), _PersonsID);
    }

    protected void LnkCancel_Click(object sender, EventArgs e)
    {
        PnlEdit.Visible = false;
    }
}