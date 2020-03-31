using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public static class Config
{
    public static string _DefaultSystemErrorMessages = "Sistemdə yükləmə var, daha sonra təkrar cəhd edin.";

    //Bazadan integer tipli columnslarin alanda.
    public static string SqlDateTimeFormat(string ColumnsName, string Alias = "")
    {
        if (string.IsNullOrEmpty(Alias))
            Alias = ColumnsName;

        return string.Format("Replace(SubString(IsNull(cast({0} as varchar),'10000000'),7,2)+'.'+SubString(IsNull(cast({0} as varchar),'10000000'),5,2)+'.'+Replace(SubString(IsNull(cast({0} as varchar),'10000000'),1,4),'1000','0000'),'00.00.0000','') {1}", ColumnsName, Alias);
    }

    public static string SqlAddressColumnsFormat(string CountiesColumnsName, string RegionsColumnsName, string AddressColumnsName, string Alias)
    {
        return string.Format("Replace(((Select Name From Countries Where ID={0})+','+IsNull((Select Name From Regions Where ID={1}),',')),',,','')+'<br/><span class=\"addressSub\">'+ {2}+'</span>' {3}", CountiesColumnsName, RegionsColumnsName, AddressColumnsName, Alias);
    }

    //Get WebConfig.config App Key
    public static string _GetAppSettings(string KeyName)
    {
        return ConfigurationManager.AppSettings[KeyName];
    }

    //Get Querystring
    public static string _GetQueryString(string KeyName)
    {
        return HttpContext.Current.Request.QueryString[KeyName]._ToString();
    }

    //ConvertString.
    public static string _ToString(this object Value)
    {
        return Convert.ToString(Value);
    }

    //ConvertString.
    public static int _ToInt16(this object Value)
    {
        return Convert.ToInt16(Value);
    }

    //ConvertString.
    public static int _ToInt32(this object Value)
    {
        return Convert.ToInt32(Value);
    }

    //Email validator
    public static bool IsEmail(this string Mail)
    {
        const string StrRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
        @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
        @".)+))([a-zA-Z]{2,6}|[0-9]{1,3})(\]?)$";

        return (new System.Text.RegularExpressions.Regex(StrRegex)).IsMatch(Mail);
    }

    //ID file name encrypt
    public static string EncryptFilename(this string ID)
    {
        ID = ID + "_" + SHA1Special(ID + Config._GetAppSettings("UploadsFileEncryptKey"));
        ID = ID.Replace(" ", "-");
        ID = ID.Replace(",", "-");
        ID = ID.Replace("\"", "");
        ID = ID.Replace("/", "");
        ID = ID.Replace("\\", "");
        ID = ID.Replace("+", "");
        ID = ID.Replace("-", "");
        ID = ID.Replace("$", "");
        ID = ID.Replace("#", "");
        ID = ID.Replace(".", "");
        ID = ID.Replace("=", "");
        return ID;
    }

    public static string DateTimeClear(string Data, string ReplaceChar)
    {
        Data = Data.Replace(" ", ReplaceChar);
        Data = Data.Replace(",", ReplaceChar);
        Data = Data.Replace("\"", ReplaceChar);
        Data = Data.Replace("/", ReplaceChar);
        Data = Data.Replace("+", ReplaceChar);
        Data = Data.Replace("-", ReplaceChar);
        Data = Data.Replace("$", ReplaceChar);
        Data = Data.Replace("#", ReplaceChar);
        Data = Data.Replace("=", ReplaceChar);
        Data = Data.Replace("*", ReplaceChar);
        Data = Data.Replace(":", ReplaceChar);
        Data = Data.Replace(".", ReplaceChar);
        return Data;
    }
    //added by Toghrul
    public static bool CheckFileContentLength(this HttpPostedFile File, int ValueMB = 10)
    {
        if ((File.ContentLength / 1024) > ValueMB * 1000)
        {
            return false;
        }
        return true;
    }

    public static string GetExtension(this object Path)
    {
        return System.IO.Path.GetExtension(Path._ToString()).Trim('.').ToLower();
    }

    public static int IPToInteger(this string IP)
    {
        try
        {
            if (string.IsNullOrEmpty(IP))
                return 0;

            return BitConverter.ToInt32(System.Net.IPAddress.Parse(IP).GetAddressBytes(), 0);
        }
        catch
        {
            return 0;
        }
    }

    public static string IntegerToIP(this object IntegerIP)
    {
        try
        {
            return new System.Net.IPAddress(BitConverter.GetBytes(IntegerIP._ToInt32())).ToString();
        }
        catch
        {
            return "--";
        }
    }

    //Tarix təmizləyən
    public static object DateFormatClear(this string Date)
    {
        //Clear
        Date = Date.Trim();
        Date = Date.Replace(",", ".");
        Date = Date.Replace("+", ".");
        Date = Date.Replace("/", ".");
        Date = Date.Replace("-", ".");
        Date = Date.Replace("*", ".");
        Date = Date.Replace("\\", ".");
        Date = Date.Replace(" ", ".");

        if (!IsNumeric(Date.Replace(".", "")))
            return null;

        string[] DtSplit = Date.Split('.');

        if (DtSplit.Length != 3)
            return null;

        //Əgər 2050 keçərsə 1900 yazaq.
        try
        {
            //İli 2 simvol olsa yanına 20 artıq. 3 minici ilə qədər gedər :)
            if (DtSplit[2].Length == 2)
                DtSplit[2] = "20" + DtSplit[2];

            if (DtSplit[2].Length == 1)
                DtSplit[2] = "200" + DtSplit[2];

            if (DtSplit[2]._ToInt16() > 2050)
                DtSplit[2] = (DtSplit[2]._ToInt16() - 100).ToString();
        }
        catch
        {
        }

        try
        {
            DateTime Dt = new DateTime(
                int.Parse(DtSplit[2]),
                int.Parse(DtSplit[1]),
                int.Parse(DtSplit[0])
                );

            return Dt;
        }
        catch
        {
            return null;
        }
    }

    //Get WebConfig.config App Key
    public static string Split(string Value, char Char, int Index, string CatchValue)
    {
        try
        {
            return Value.Split(Char)[Index];
        }
        catch
        {
            return CatchValue;
        }
    }

    //Boşluğa görə type 1 sol tərəfi 2 sağ tərəfi. Tarix və saat 
    public static string SplitDateTime(string Value, int Type, string CatchValue)
    {
        try
        {
            if (Type == 1)
                return Value.Remove(Value.IndexOf(' ')).Trim();
            else
                return Value.Remove(0, Value.IndexOf(' ')).Trim();
        }
        catch
        {
            return CatchValue;
        }
    }

    //Açar yaradaq.
    public static string Key(int say)
    {
        Random Rnd = new Random();
        string Bind = "aqwertyuipasdfghjkzxcvbnmQAZWSXEDCRFVTGBYHNUJMKP23456789";
        string Key = "";
        for (int i = 1; i <= say; i++)
        {
            Key += Bind.Substring(Rnd.Next(Bind.Length - 1), 1);
        }
        return Key.Trim();
    }

    //Set title to url (clear latin and special simvols)
    public static string ClearTitle(this string Title)
    {
        Title = Title.ToLower().Trim().Trim('-').Trim('.').Trim();
        Title = Title.Replace("   ", " ");
        Title = Title.Replace("  ", " ");
        Title = Title.Replace(" ", "-");
        Title = Title.Replace(",", "-");
        Title = Title.Replace("\"", "");
        Title = Title.Replace("“", "");
        Title = Title.Replace("”", "");

        Title = Title.Replace("---", "-");
        Title = Title.Replace("--", "-");
        Title = Title.Replace("#", "");
        Title = Title.Replace("&", "");

        //No Latin
        Title = Title.Replace("ü", "u");
        Title = Title.Replace("ı", "i");
        Title = Title.Replace("ö", "o");
        Title = Title.Replace("ğ", "g");
        Title = Title.Replace("ə", "e");
        Title = Title.Replace("ç", "ch");
        Title = Title.Replace("ş", "sh");

        return Title;
    }

    public static string ClearSms(this string SmsText)
    {
        SmsText = SmsText.Replace("ü", "u");
        SmsText = SmsText.Replace("ı", "i");
        SmsText = SmsText.Replace("ö", "o");
        SmsText = SmsText.Replace("ğ", "g");
        SmsText = SmsText.Replace("ə", "e");
        SmsText = SmsText.Replace("ç", "ch");
        SmsText = SmsText.Replace("ş", "sh");

        SmsText = SmsText.Replace("Ü", "U");
        SmsText = SmsText.Replace("I", "I");
        SmsText = SmsText.Replace("Ö", "O");
        SmsText = SmsText.Replace("Ğ", "G");
        SmsText = SmsText.Replace("Ə", "E");
        SmsText = SmsText.Replace("Ç", "Ch");
        SmsText = SmsText.Replace("Ş", "Sh");

        return SmsText;
    }

    public static string MonthToText(this string Mont)
    {
        if (Mont == "01")
            return "Yanvar";
        if (Mont == "02")
            return "Fevral";
        if (Mont == "03")
            return "Mart";
        if (Mont == "04")
            return "Aprel";
        if (Mont == "05")
            return "May";
        if (Mont == "06")
            return "İyun";
        if (Mont == "07")
            return "İyul";
        if (Mont == "08")
            return "Avqust";
        if (Mont == "09")
            return "Sentyabr";
        if (Mont == "10")
            return "Oktyabr";
        if (Mont == "11")
            return "Noyabr";
        if (Mont == "12")
            return "Dekabr";

        return "--";
    }

    //Cümlə çox uzun olanda üç nöqtə qoyaq.
    public static string SizeLimit(string Text, int Length, string More)
    {
        if (Text.Length > Length)
            Text = Text.Substring(0, Length) + More;
        return Text;
    }

    //Numaric testi.
    public static bool IsNumeric(this object Value)
    {
        if (string.IsNullOrEmpty(Value._ToString()))
            return false;

        for (int i = 0; i < Value._ToString().Length; i++)
        {
            if ("0123456789".IndexOf(Value._ToString().Substring(i, 1)) < 0)
            {
                return false;
            }
        }
        return true;
    }

    //Ajax error message
    public static void MsgBoxAjax(string Text, Page P, bool isSuccess = false)
    {
        ScriptManager.RegisterClientScriptBlock(P, P.Page.GetType(), "PopupScript", "window.focus(); AlertPopup('" + (isSuccess ? "S" : "E") + "','" + Text + "');", true);
    }

    //SHA1Special - özəl
    public static string SHA1Special(this string Value)
    {
        byte[] result;
        System.Security.Cryptography.SHA1 ShaEncrp = new System.Security.Cryptography.SHA1CryptoServiceProvider();
        Value = String.Format("{0}{1}{0}", "CSAASADM", Value);
        byte[] buffer = new byte[Value.Length];
        buffer = Encoding.UTF8.GetBytes(Value);
        result = ShaEncrp.ComputeHash(buffer);
        return Convert.ToBase64String(result);
    }

    public static string SubString(object Value, int Start, int Length, string CatchValue = "-1")
    {
        try
        {
            return Value._ToString().Substring(Start, Length);
        }
        catch
        {
            return CatchValue;
        }
    }

    public static string IntDateDay(this object Value)
    {
        return SubString(Value, 6, 2, "00");
    }

    public static string IntDateMonth(this object Value)
    {
        return SubString(Value, 4, 2, "00");
    }

    public static string IntDateYear(this object Value)
    {
        return SubString(Value, 0, 4, "1000");
    }

    /// <summary>
    /// 19860908 --> 08.09.1986
    /// </summary>
    /// <param name="Value"></param>
    /// <returns></returns>
    public static string IntDateFormat(this object Value)
    {
        if (string.IsNullOrEmpty(Value._ToString()))
            return "";

        string Day = SubString(Value._ToString(), 6, 2, "00");
        string Month = SubString(Value._ToString(), 4, 2, "00");
        string Year = SubString(Value._ToString(), 0, 4, "1000");

        if (Year == "1000")
            Year = "0000";

        string Result = string.Format("{0}.{1}.{2}", Day, Month, Year);

        return (Result == "00.00.0000" ? "" : Result);
    }

    public static string IsEmptyReplaceNoul(this object Value, string ReplaceValue = "0")
    {
        return string.IsNullOrEmpty(Value._ToString()) ? ReplaceValue : Value._ToString();
    }

    //Səhifəni yönləndirək:
    public static void Redirect(string GetUrl)
    {
        HttpContext.Current.Response.Redirect(GetUrl, true);
        HttpContext.Current.Response.End();
    }

    public static void RedirectError()
    {
        HttpContext.Current.Response.Redirect("~/error", true);
        HttpContext.Current.Response.End();
    }

    public static void RedirectMain()
    {
        HttpContext.Current.Response.Redirect("~/list", true);
        HttpContext.Current.Response.End();
    }

    //Tarixləri dolduraq:
    public static System.Data.DataTable NumericList(int From, int To, string BlankInsert = "00")
    {
        System.Data.DataTable dt = new System.Data.DataTable();
        dt.Columns.Add("ID", typeof(string));
        dt.Columns.Add("Name", typeof(string));

        if (BlankInsert.Length > 0)
            dt.Rows.Add(BlankInsert, "--");

        for (int i = From; i <= To; i++)
        {
            dt.Rows.Add((i < 10 ? "0" + i.ToString() : i.ToString()), (i < 10 ? "0" + i.ToString() : i.ToString()));
        }
        return dt;
    }

    //Aylar
    public static System.Data.DataTable MonthList(bool IsBlankInsert = true)
    {
        System.Data.DataTable dt = new System.Data.DataTable();
        dt.Columns.Add("ID", typeof(string));
        dt.Columns.Add("Name", typeof(string));

        if (IsBlankInsert)
            dt.Rows.Add("00", "--");

        dt.Rows.Add("01", "YANVAR");
        dt.Rows.Add("02", "FEVRAL");
        dt.Rows.Add("03", "MART");
        dt.Rows.Add("04", "APREL");
        dt.Rows.Add("05", "MAY");
        dt.Rows.Add("06", "İYUN");
        dt.Rows.Add("07", "İYUL");
        dt.Rows.Add("08", "AVQUST");
        dt.Rows.Add("09", "SENTYABR");
        dt.Rows.Add("10", "OKTYABR");
        dt.Rows.Add("11", "NOYABR");
        dt.Rows.Add("12", "DEKABR");

        return dt;
    }

    public static string _Rows(this System.Data.DataTable Dt, string RowsName, int RowsIndex = 0)
    {
        return Dt.Rows[RowsIndex][RowsName]._ToString();
    }

    public static object _RowsObject(this System.Data.DataTable Dt, string RowsName, int RowsIndex = 0)
    {
        return Dt.Rows[RowsIndex][RowsName];
    }

    //Bazaya Null insert edek
    //EmtpyChar hərhansı char string gələ bilər.
    public static object NullConvert(this object Value, bool IsReturnDbNull = true)
    {
        try
        {
            if (Value == null || Value._ToString() == "" || Value._ToString() == "0" || Value._ToString() == "-1")
            {
                if (IsReturnDbNull)
                {
                    return DBNull.Value;
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return Value;
            }
        }
        catch
        {
            if (IsReturnDbNull)
            {
                return DBNull.Value;
            }
            else
            {
                return "";
            }
        }
    }

    public static string LikeFormat(this object Input, string Replace = "_")
    {
        return Regex.Replace(Input._ToString(), @"[aAeEiİoOuUəƏüÜıIöÖğĞçÇşŞ]", Replace, RegexOptions.IgnoreCase);
    }

    public static void ClearControls(this Control Panel)
    {
        foreach (Control item in Panel.Controls)
        {
            if (item.HasControls())
            {
                ClearControls(item);
            }

            if (item is TextBox)
            {
                (item as TextBox).Text = "";
            }
            else if (item is DropDownList)
            {
                (item as DropDownList).SelectedIndex = 0;
            }
            else if (item is ListBox)
            {
                ListBox LitBx = item as ListBox;
                for (int i = 0; i < LitBx.Items.Count; i++)
                {
                    LitBx.Items[i].Selected = false;
                }
            }
            else if (item is CheckBox)
            {
                (item as CheckBox).Checked = false;
            }
        }
    }

    public static void BindControls(this Control Panel, Dictionary<string, object> FilterDictionary, string TableName, bool ClearSession = false)
    {
        string SessionName = TableName;
        if (ClearSession)
        {
            HttpContext.Current.Session[SessionName] = null;
        }
        if (HttpContext.Current.Session[SessionName] != null)
        {
            if (FilterDictionary.Count < 1)
            {
                FilterDictionary = (Dictionary<string, object>)HttpContext.Current.Session[SessionName];
            }
            foreach (Control item in Panel.Controls)
            {
                if (item.HasControls())
                {
                    BindControls(item, FilterDictionary, TableName);
                }

                try
                {
                    if (item is TextBox)
                    {
                        TextBox Txt = item as TextBox;
                        Txt.Text = FilterDictionary[Txt.ID]._ToString();
                    }
                    else if (item is DropDownList)
                    {
                        DropDownList DList = item as DropDownList;
                        DList.SelectedValue = FilterDictionary[DList.ID]._ToString();
                    }
                    else if (item is ListBox)
                    {
                        ListBox LstBx = item as ListBox;
                        for (int i = 0; i < LstBx.Items.Count; i++)
                        {
                            ListItem Item = LstBx.Items[i];
                            if (FilterDictionary[LstBx.ID]._ToString().IndexOf(Item.Value) > -1)
                            {
                                Item.Selected = true;
                            }
                        }
                    }
                    else if (item is CheckBox)
                    {
                        CheckBox Check = item as CheckBox;
                        Check.Checked = (bool)FilterDictionary[Check.ID];
                    }
                }
                catch
                {
                    HttpContext.Current.Session[SessionName] = null;
                    FilterDictionary.Clear();
                    BindControls(Panel, FilterDictionary, TableName);
                }
            }
        }
        else
        {
            foreach (Control item in Panel.Controls)
            {
                if (item.HasControls())
                {
                    BindControls(item, FilterDictionary, TableName, true);
                }

                if (item is TextBox)
                {
                    TextBox Txt = item as TextBox;
                    FilterDictionary.Add(Txt.ID, Txt.Text);
                }
                else if (item is DropDownList)
                {
                    DropDownList DList = item as DropDownList;
                    FilterDictionary.Add(DList.ID, DList.SelectedValue);
                }
                else if (item is ListBox)
                {
                    ListBox LstBx = item as ListBox;
                    StringBuilder Values = new StringBuilder();
                    for (int i = 0; i < LstBx.Items.Count; i++)
                    {
                        ListItem Item = LstBx.Items[i];
                        if (Item.Selected)
                        {
                            Values.Append(Item.Value + ",");
                        }
                    }
                    FilterDictionary.Add(LstBx.ID, Values._ToString().Trim(','));
                }
                else if (item is CheckBox)
                {
                    CheckBox Check = item as CheckBox;
                    FilterDictionary.Add(Check.ID, Check.Checked);
                }

            }
            HttpContext.Current.Session[SessionName] = FilterDictionary;
        }

    }

    //Mobile number control only 9 simvol
    public static string IsMobileNumberControl(string Number, bool IsMobileOperationType)
    {
        try
        {
            Number = Number.Trim('+').TrimStart('0').Replace(" ", "").Replace("-", "").Replace("/", "").Replace(",", "").Trim().TrimStart('0');

            if (Number.Length > 9 && Number.Substring(0, 3) == "994")
                Number = Number.Substring(3);

            if (!Number.IsNumeric() || Number.Length != 9)
                return "-1";

            string Typ = Number.Substring(0, 2);
            if (IsMobileOperationType)
            {
                if (Config._GetAppSettings("MobileOperationTypes").IndexOf(Typ) < 0)
                {
                    return "-1";
                }
            }

            return Number;
        }
        catch
        {
            return "-1";
        }
    }

    public static string QueryIdEncrypt(this object ID)
    {
        return HttpContext.Current.Server.UrlEncode((DALC._GetUsersLogin.LoginKey + "#" + ID._ToString()).Encrypt());
    }

    public static string QueryIdDecrypt(this object Key)
    {
        string[] SplitResult = HttpContext.Current.Server.UrlDecode(Key._ToString()).Replace(" ", "+").Decrypt().Split('#');

        if (DALC._GetUsersLogin.LoginKey != SplitResult[0])
            return "-1";

        return SplitResult[1]._ToString();
    }
}
