using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;

public partial class Detail_Contents_240_Aliment : System.Web.UI.UserControl
{
    int _PersonsID = 0;
    string _TableName = "PersonsAliment";

    string[] _SqlSelectColumns =
                               {
                                   "Mebleg",
                                   "Muddet",
                                   Config.SqlDateTimeFormat("AlimentBaslamaTarix"),
                                   "VerilmeArdicilligiBarede",
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
        DListFltrAlimentD.DataSource = Config.NumericList(1, 31);
        DListFltrAlimentD.DataBind();

        DListFltrAlimentM.DataSource = Config.MonthList();
        DListFltrAlimentM.DataBind();

        DListFltrAlimentY.DataSource = Config.NumericList(1980, DateTime.Now.Year, "1000");
        DListFltrAlimentY.DataBind();

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
        if (!DALC._IsPermissionTypeEdit(360))
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
        TxtMebleg.Text = TxtMuddet.Text = TxtVerilmeArdicilligi.Text = TxtDescription.Text = "";
        DListFltrAlimentD.SelectedValue = DListFltrAlimentM.SelectedValue = "00";
        DListFltrAlimentY.SelectedValue = "1000";

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

            LogText = "Uşağa təyin edilmiş aliment üzrə yeni məlumat əlavə edildi. № {0}";
        }
        else
        {
            ID = GrdList.SelectedDataKey["ID"]._ToString();
            LogText = "Uşağa təyin edilmiş aliment üzrə məlumatda düzəliş edildi. № {0}";
            HistoryStatus = 30;
        }

        Dictionary<string, object> Dictionary = new Dictionary<string, object>
        {
            { "OrganizationsID", DALC._GetUsersLogin.OrganizationsID },
            { "UsersID", DALC._GetUsersLogin.ID },
            { "Mebleg", TxtMebleg.Text.Trim() },
            { "Muddet", TxtMuddet.Text.Trim() },
            { "VerilmeArdicilligiBarede", TxtVerilmeArdicilligi.Text.Trim() },
            { "AlimentBaslamaTarix", (DListFltrAlimentY.SelectedValue + DListFltrAlimentM.SelectedValue + DListFltrAlimentD.SelectedValue).NullConvert() },
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

        DALC.UsersHistoryInsert(HistoryStatus, 360, string.Format(LogText, ID), _PersonsID);
        Config.MsgBoxAjax("Əməliyyat uğurla yerinə yetirildi.", Page, true);
        PnlEdit.Visible = false;
        GridBind();
    }

    protected void GrdList_SelectedIndexChanged(object sender, EventArgs e)
    {
        Aylar();

        DataTable Dt = DALC.GetDataTableParams("*", _TableName, "ID", GrdList.SelectedDataKey["ID"]._ToString(), "");

        if (Dt == null || Dt.Rows.Count < 1)
        {
            Config.MsgBoxAjax(Config._DefaultSystemErrorMessages, Page);
            return;
        }

        TxtMebleg.Text = Dt._Rows("Mebleg");
        TxtMuddet.Text = Dt._Rows("Muddet");
        TxtVerilmeArdicilligi.Text = Dt._Rows("VerilmeArdicilligiBarede");
        DListFltrAlimentD.SelectedValue = Dt._RowsObject("AlimentBaslamaTarix").IntDateDay();
        DListFltrAlimentM.SelectedValue = Dt._RowsObject("AlimentBaslamaTarix").IntDateMonth();
        DListFltrAlimentY.SelectedValue = Dt._RowsObject("AlimentBaslamaTarix").IntDateYear();
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
        DALC.UsersHistoryInsert(40, 360, string.Format("Uşağa təyin edilmiş aliment üzrə məlumat silindi. № {0}", ID), _PersonsID);
    }

    protected void LnkCancel_Click(object sender, EventArgs e)
    {
        PnlEdit.Visible = false;
    }
}