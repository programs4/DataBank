using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;

public class DALC
{
    public static SqlConnection SqlConn
    {
        get
        {
            return new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString);
        }
    }

    public class AdministratorsInfoClass
    {
        public int ID
        {
            get;
            set;
        }
        public string Fullname
        {
            get;
            set;
        }
        public int Status
        {
            get;
            set;
        }
    }

    public class UsersInfoClass
    {
        public int ID
        {
            get;
            set;
        }
        public int OrganizationsID
        {
            get;
            set;
        }
        public string OrganizationsName
        {
            get;
            set;
        }
        public string Fullname
        {
            get;
            set;
        }
        public string PermissionRegions
        {
            get;
            set;
        }
        public string PermissionTypes
        {
            get;
            set;
        }
        public string PermissionTypesEdit
        {
            get;
            set;
        }
        public string LoginKey
        {
            get;
            set;
        }
    }

    public class DataTableResult
    {
        public int Count;
        public DataTable Dt = new DataTable();
    }

    public class Transaction
    {
        public SqlTransaction SqlTransaction = null;
        public SqlCommand Com = new SqlCommand();
    }

    public static AdministratorsInfoClass _GetAdministratorsLogin
    {
        get
        {
            if (HttpContext.Current.Session["AdminLogin"] != null)
                return (AdministratorsInfoClass)HttpContext.Current.Session["AdminLogin"];
            else
                return null;
        }
    }

    public static UsersInfoClass _GetUsersLogin
    {
        get
        {
            if (HttpContext.Current.Session["UsersLogin"] != null)
                return (UsersInfoClass)HttpContext.Current.Session["UsersLogin"];
            else
                return null;
        }
    }

    public static bool _IsPermissionType(int ID)
    {
        if (DALC._GetUsersLogin.PermissionTypes == "*")
            return true;

        return DALC._GetUsersLogin.PermissionTypes.IndexOf("," + ID.ToString() + ",") > -1;
    }

    public static bool _IsPermissionTypeEdit(int ID)
    {
        if (DALC._GetUsersLogin.PermissionTypesEdit == "*")
            return true;

        return DALC._GetUsersLogin.PermissionTypesEdit.IndexOf("," + ID.ToString() + ",") > -1;
    }

    #region Rahatlaşdıran metodlarımız

    //Bazadan tək dəyər alaq:
    public static string GetDbSingleValues(string Columns, string Table, string WhereAndOrderBy, string CatchValue = "-1")
    {
        using (SqlCommand com = new SqlCommand(String.Format("Select {0} From {1} {2}", Columns, Table, WhereAndOrderBy), SqlConn))
        {
            try
            {
                com.Connection.Open();
                return com.ExecuteScalar()._ToString();
            }
            catch (Exception er)
            {
                DALC.ErrorLogsInsert("DALC.GetDbSingleValues catch error: " + er.Message);
                return CatchValue;
            }
            finally
            {
                com.Connection.Close();
            }
        }
    }

    //Bazadan tək dəyər alaq:
    public static string GetDbSingleValuesParams(string Columns, string Table, string WhereParamsColumns, object ParamsValue, string OrderBy, string CatchValue = "-1")
    {
        using (SqlCommand com = new SqlCommand(String.Format("Select {0} From {1} Where {2}=@ParamVals {3}", Columns, Table, WhereParamsColumns, OrderBy), SqlConn))
        {
            try
            {
                com.Connection.Open();
                com.Parameters.AddWithValue("@ParamVals", ParamsValue);
                return com.ExecuteScalar()._ToString();
            }
            catch (Exception er)
            {
                DALC.ErrorLogsInsert("DALC.GetDbSingleValuesParams catch error: " + er.Message);
                return CatchValue;
            }
            finally
            {
                com.Connection.Close();
            }
        }
    }

    //Bazadan tək dəyər alaq - istənilən saytda parametr ilə:
    public static string GetDbSingleValuesMultiParams(string Columns, string Table, string[] WhereParamsColumns, object[] ParamsValue, string OrderBy, string CatchValue = "-1")
    {
        //SqlMulti params command
        SqlCommand com = new SqlCommand();

        //Mütləq value və parametr adları eyni olmalıdır.
        if (WhereParamsColumns.Length != ParamsValue.Length)
            return "";
        try
        {
            string WhereList = "1=1";

            for (int i = 0; i < WhereParamsColumns.Length; i++)
            {
                if (WhereParamsColumns[i].Length < 1)
                    continue;

                WhereList += " and " + WhereParamsColumns[i] + "=@" + WhereParamsColumns[i];
                com.Parameters.AddWithValue("@" + WhereParamsColumns[i], ParamsValue[i]);
            }

            com.CommandText = String.Format("Select {0} From {1} Where {2} {3}", Columns, Table, WhereList, OrderBy);
            com.Connection = SqlConn;


            com.Connection.Open();
            return com.ExecuteScalar()._ToString();
        }
        catch (Exception er)
        {
            ErrorLogsInsert("DALC.GetDbSingleValuesMultiParams catch error: " + er.Message);
            return CatchValue;
        }
        finally
        {
            com.Connection.Close();
        }
    }

    public static string GetSingleValuesBySqlCommand(string SqlCommand, string ParamsKeys = "", object[] ParamsValues = null, CommandType CommandType = CommandType.Text)
    {
        string[] ParamsKeysArray = ParamsKeys.Split(',');

        using (SqlCommand com = new SqlCommand(SqlCommand, SqlConn))
        {
            try
            {
                com.CommandType = CommandType;
                if (ParamsValues != null)
                {
                    if (ParamsKeysArray.Length != ParamsValues.Length)
                    {
                        throw new Exception("ParamsKeys and ParamsValues not same size.");
                    }

                    for (int i = 0; i < ParamsKeysArray.Length; i++)
                    {
                        com.Parameters.AddWithValue("@" + ParamsKeysArray[i], ParamsValues[i]);
                    }
                }
                com.Connection.Open();
                return com.ExecuteScalar()._ToString();

            }
            catch (Exception er)
            {
                ErrorLogsInsert(string.Format("DALC.GetSingleValuesBySqlCommand catch error: {0}", er.Message));
                return "-1";
            }
            finally
            {
                com.Connection.Close();
                com.Connection.Dispose();
            }
        }
    }
    //************** FOR  GET   TABLE


    //Bazadan table alaq
    public static DataTable GetDataTable(string Columns, string Table, string WhereAndOrderBy)
    {
        DataTable Dt = new DataTable();
        try
        {
            using (SqlDataAdapter Da = new SqlDataAdapter(String.Format("Select {0} From {1} {2}", Columns, Table, WhereAndOrderBy), SqlConn))
            {
                Da.Fill(Dt);
                return Dt;
            }
        }
        catch (Exception er)
        {
            DALC.ErrorLogsInsert("DALC.GetDataTable catch error: " + er.Message);
            return null;
        }
    }

    //Konkret 1 ədəd paramteri olanda.
    public static DataTable GetDataTableParams(string Columns, string Table, string WhereParamsColumns, object ParamsValue, string OrderBy)
    {
        DataTable Dt = new DataTable();
        try
        {
            using (SqlDataAdapter Da = new SqlDataAdapter(String.Format("Select {0} From {1} Where {2}=@ParamVals {3}", Columns, Table, WhereParamsColumns, OrderBy), SqlConn))
            {
                Da.SelectCommand.Parameters.AddWithValue("@ParamVals", ParamsValue);
                Da.Fill(Dt);
                return Dt;
            }
        }
        catch (Exception er)
        {
            DALC.ErrorLogsInsert("DALC.GetDataTableParams catch error: " + er.Message);
            return null;
        }
    }

    //Konkret 1 ədəd paramteri olanda.
    public static DataTable GetDataTableMultiParams(string Columns, string Table, string[] WhereParamsColumns, object[] ParamsValue, string OrderBy)
    {
        //SqlMulti params command
        SqlCommand com = new SqlCommand();

        //Mütləq value və parametr adları eyni olmalıdır.
        if (WhereParamsColumns.Length != ParamsValue.Length)
            return null;

        try
        {
            string WhereList = "1=1";

            for (int i = 0; i < WhereParamsColumns.Length; i++)
            {
                if (WhereParamsColumns[i].Length < 1)
                    continue;

                WhereList += " and " + WhereParamsColumns[i] + "=@" + WhereParamsColumns[i];
                com.Parameters.AddWithValue("@" + WhereParamsColumns[i], ParamsValue[i]);
            }

            com.CommandText = String.Format("Select {0} From {1} Where {2} {3}", Columns, Table, WhereList, OrderBy);
            com.Connection = SqlConn;

            DataTable Dt = new DataTable();
            SqlDataAdapter Da = new SqlDataAdapter(com);
            Da.Fill(Dt);

            return Dt;
        }
        catch (Exception er)
        {
            DALC.ErrorLogsInsert("DALC.GetDataTableMultiParams catch error: " + er.Message);
            return null;
        }
    }

    //Ümumi sql commanda görə datatable qaytarır.   
    public static DataTable GetDataTableBySqlCommand(string SqlCommand, string ParamsKeys = "", object[] ParamsValues = null, CommandType CommandType = CommandType.Text)
    {
        string[] ParamsKeysArray = ParamsKeys.Split(',');

        DataTable Dt = new DataTable();
        using (SqlCommand com = new SqlCommand(SqlCommand, SqlConn))
        {
            com.CommandType = CommandType;

            try
            {
                if (ParamsValues != null)
                {
                    if (ParamsKeysArray.Length != ParamsValues.Length)
                    {
                        throw new Exception("ParamsKeys and ParamsValues not same size.");
                    }

                    for (int i = 0; i < ParamsKeysArray.Length; i++)
                    {
                        com.Parameters.AddWithValue("@" + ParamsKeysArray[i], ParamsValues[i]);
                    }
                }
                com.Connection.Open();
                using (SqlDataReader Reader = com.ExecuteReader())
                {
                    Dt.Load(Reader);
                }
                return Dt;
            }
            catch (Exception er)
            {
                ErrorLogsInsert(string.Format("DALC.GetDataTableBySqlCommand catch error: {0}", er.Message));
                return null;
            }
            finally
            {
                com.Connection.Close();
            }
        }
    }

    public static int InsertDatabase(string TableName, Dictionary<string, object> Dictionary, bool IsErrorLog = true)
    {
        string Columns = "";
        string Params = "";
        SqlCommand com = new SqlCommand();

        foreach (var Item in Dictionary)
        {
            Columns += Item.Key + ",";
            Params += "@" + Item.Key + ",";
            com.Parameters.AddWithValue("@" + Item.Key, Item.Value);
        }

        try
        {
            com.Connection = SqlConn;
            com.CommandText = String.Format("Insert Into {0}({1}) Values({2}); Select SCOPE_IDENTITY();", TableName, Columns.Trim(','), Params.Trim(','));
            com.Connection.Open();
            return com.ExecuteScalar()._ToInt32();
        }
        catch (Exception er)
        {
            if (IsErrorLog)
            {
                ErrorLogsInsert(TableName + " DALC.InsertDatabase də xəta baş verdi: " + er.Message);
            }
            else
            {
                SendAdminMail(TableName, "ErrorLogsInsert-de xəta baş verdi: " + er.Message);
            }
            return -1;
        }
        finally
        {
            com.Connection.Close();
        }
    }

    public static int InsertDatabase(string TableName, string[] Key, object[] Values)
    {
        string Columns = "";
        string ParamColumns = "";

        SqlCommand com = new SqlCommand();

        for (int i = 0; i < Key.Length; i++)
        {
            Columns += Key[i] + ", ";
            ParamColumns += "@" + Key[i] + ", ";
            com.Parameters.AddWithValue("@" + Key[i], Values[i]);
        }

        Columns = Columns.Trim().Trim(',');
        ParamColumns = ParamColumns.Trim().Trim(',');
        try
        {
            com.Connection = SqlConn;
            com.CommandText = String.Format("Insert Into {0}({1}) Values({2}); Select SCOPE_IDENTITY();", TableName, Columns, ParamColumns);
            com.Connection.Open();
            return com.ExecuteScalar()._ToInt32();
        }
        catch (Exception er)
        {
            ErrorLogsInsert(TableName + " DALC.InsertDatabase catch error: " + er.Message, true);
            return 0;
        }
        finally
        {
            com.Connection.Close();
        }
    }

    /// <summary>
    /// Update Numunə: Dictionary.Add("FullName", "Tural Xasiyev");
    /// Where Numunə:  Dictionary.Add("WhereID", "1");
    /// </summary>
    public static int UpdateDatabase(string TableName, Dictionary<string, object> Dictionary)
    {
        SqlCommand com = new SqlCommand();
        string Columns = "";
        string Where = "";
        string WhereColumnsName = "";
        try
        {
            foreach (var Item in Dictionary)
            {
                if (!Item.Key.ToUpper().Contains("WHERE"))
                {
                    Columns += Item.Key + "=@w" + Item.Key + " ,";
                    com.Parameters.AddWithValue("@w" + Item.Key, Item.Value);
                }
                else
                {
                    WhereColumnsName = Item.Key.Substring(5);

                    Where += " and " + WhereColumnsName + "=@" + WhereColumnsName;
                    com.Parameters.AddWithValue("@" + WhereColumnsName, Item.Value);
                }
            }

            if (Where.Length > 0)
                Where = "1=1 " + Where;

            com.Connection = SqlConn;
            com.CommandText = String.Format("Update {0} SET {1} Where {2}", TableName, Columns.Trim(','), Where);
            com.Connection.Open();
            return com.ExecuteNonQuery();
        }
        catch (Exception er)
        {
            ErrorLogsInsert(TableName + " DALC.UpdateDatabase də xəta baş verdi: " + er.Message);
            return -1;
        }
        finally
        {
            com.Connection.Close();
        }
    }

    /// <summary>
    /// Nümunə: UpdateDatabase("Persons", new string[] { "Soyad", "Ad", "WhereID" }, new object[] { "Novruzov", "Emin", 1 })
    /// </summary>
    public static int UpdateDatabase(string TableName, string[] Key, object[] Values)
    {
        SqlCommand com = new SqlCommand();
        string Columns = "";
        string Where = "";
        string WhereColumnsName = "";
        try
        {
            for (int i = 0; i < Key.Length; i++)
            {

                if (!Key[i].ToUpper().Contains("WHERE"))
                {
                    Columns += Key[i] + "=@w" + Key[i] + " ,";
                    com.Parameters.AddWithValue("@w" + Key[i], Values[i]);
                }
                else
                {
                    WhereColumnsName = Key[i].Substring(5);

                    Where += " and " + WhereColumnsName + "=@" + WhereColumnsName;
                    com.Parameters.AddWithValue("@" + WhereColumnsName, Values[i]);
                }
            }

            if (Where.Length > 0)
                Where = "1=1" + Where;

            com.Connection = SqlConn;
            com.CommandText = String.Format("Update {0} SET {1} Where {2}", TableName, Columns.Trim(','), Where);
            com.Connection.Open();
            return com.ExecuteNonQuery();
        }
        catch (Exception er)
        {
            ErrorLogsInsert(TableName + " DALC.UpdateDatabase də xəta baş verdi: " + er.Message);
            return -1;
        }
        finally
        {
            com.Connection.Close();
        }
    }
    public static int UpdateDatabase(string TableName, Dictionary<string, object> Dictionary, Transaction Transaction, bool IsCommit = false)
    {
        int result = -1;
        StringBuilder Columns = new StringBuilder();
        StringBuilder Where = new StringBuilder();
        string WhereColumnsName = "";

        // Parametri təmizləyəkki ikinci dəfə eyni parametrlə insertə gəldikdə xəta verməsin
        Transaction.Com.Parameters.Clear();
        foreach (var Item in Dictionary)
        {
            if (!Item.Key.ToUpper().Contains("WHERE"))
            {
                if (Item.Key.ToUpper().Contains("(+)"))
                {
                    Columns.Append(string.Format("{0}={0} + @{0} ,", Item.Key.Replace("(+)", "")));
                    Transaction.Com.Parameters.AddWithValue("@" + Item.Key.Replace("(+)", ""), Item.Value);
                }
                else
                {
                    Columns.Append(string.Format("{0}=@{0} ,", Item.Key));
                    Transaction.Com.Parameters.AddWithValue("@" + Item.Key, Item.Value);
                }
            }
            else
            {
                if (Where.Length < 1)
                    Where.Append("1=1");

                if (Item.Key.ToUpper().Contains("INWHERE"))
                {
                    WhereColumnsName = Item.Key.Substring(7);
                    Where.Append(string.Format(" and {0} in(Select item from SplitString(@wIn{0},','))", WhereColumnsName));
                    Transaction.Com.Parameters.AddWithValue("@wIn" + WhereColumnsName, Item.Value);
                }
                else
                {

                    WhereColumnsName = Item.Key.Substring(5);
                    Where.Append(" and " + WhereColumnsName + "=@w" + WhereColumnsName);
                    Transaction.Com.Parameters.AddWithValue("@w" + WhereColumnsName, Item.Value);
                }
            }
        }

        try
        {
            if (Transaction.SqlTransaction == null)
            {
                Transaction.Com.Connection = SqlConn;
                Transaction.Com.Connection.Open();
                Transaction.SqlTransaction = Transaction.Com.Connection.BeginTransaction();
            }

            Transaction.Com.CommandText = String.Format("Update {0} SET {1} Where {2}", TableName, Columns.ToString().Trim(','), Where);
            Transaction.Com.Transaction = Transaction.SqlTransaction;
            result = Transaction.Com.ExecuteNonQuery();
            if (IsCommit)
            {
                Transaction.SqlTransaction.Commit();
            }

            return result;
        }
        catch (Exception er)
        {
            IsCommit = true;

            try
            {
                Transaction.SqlTransaction.Rollback();
            }
            catch (Exception erRollback)
            {
                DALC.ErrorLogsInsert("DALC.UpdateDatabase Rollback() catch error: " + erRollback.Message);
            }

            ErrorLogsInsert(string.Format("DALC.UpdateDatabase [{0}] catch error: {1}", TableName, er.Message));
            return -1;
        }
        finally
        {
            if (IsCommit)
            {
                Transaction.SqlTransaction.Dispose();
                Transaction.Com.Connection.Close();
                Transaction.Com.Connection.Dispose();
                Transaction.Com.Dispose();
            }
        }
    }

    //Toplu insert
    public static int InsertBulk(string TableName, DataTable Dt)
    {
        string Columns = "";
        string Params = "";
        SqlCommand com = new SqlCommand();
        try
        {
            for (int i = 0; i < Dt.Rows.Count; i++)
            {
                Params += "(";
                for (int j = 0; j < Dt.Columns.Count; j++)
                {
                    if (i == 0)
                        Columns += Dt.Columns[j].ColumnName + ",";
                    Params += "@P" + i.ToString() + j.ToString() + ",";
                    com.Parameters.AddWithValue("@P" + i.ToString() + j.ToString(), Dt.Rows[i][j]);
                }
                Params = Params.Trim(',') + "),";
            }
            com.Connection = SqlConn;
            com.CommandText = String.Format("Insert Into {0}({1}) Values{2}", TableName, Columns.Trim(','), Params.Trim(','));

            com.Connection.Open();
            return com.ExecuteNonQuery();
        }
        catch (Exception er)
        {
            DALC.ErrorLogsInsert(TableName + " DALC.InsertBulk methodunda xeta bas verdi." + er.Message);
            return -1;
        }
        finally
        {
            com.Connection.Close();
        }
    }
    #endregion

    // Yeni _blank yaratmaq
    public static int NewBlankInsert(string TableName, int PersonsID, bool IsDeleted = true)
    {
        Dictionary<string, object> Dictionary = new Dictionary<string, object>();
        Dictionary.Add("PersonsID", PersonsID);
        Dictionary.Add("OrganizationsID", DALC._GetUsersLogin.OrganizationsID);
        Dictionary.Add("UsersID", DALC._GetUsersLogin.ID);
        Dictionary.Add("IsDeleted", IsDeleted); //Default silinən
        Dictionary.Add("Last_Dt", DateTime.Now);
        Dictionary.Add("Last_Ip", HttpContext.Current.Request.UserHostAddress.IPToInteger());

        return InsertDatabase(TableName, Dictionary);
    }

    public static int NewBlankRelatives(int PersonsID, int PersonsRelativesTypesID)
    {
        Dictionary<string, object> Dictionary = new Dictionary<string, object>();

        Dictionary.Add("PersonsID", PersonsID);
        Dictionary.Add("OrganizationsID", DALC._GetUsersLogin.OrganizationsID);
        Dictionary.Add("UsersID", DALC._GetUsersLogin.ID);
        Dictionary.Add("PersonsRelativesTypesID", PersonsRelativesTypesID);
        Dictionary.Add("IsDeleted", 0);
        Dictionary.Add("Last_Dt", DateTime.Now);
        Dictionary.Add("Last_Ip", HttpContext.Current.Request.UserHostAddress.IPToInteger());

        return InsertDatabase("PersonsRelatives", Dictionary);
    }

    public static string GetAdminDescription(int ID)
    {
        return GetDbSingleValuesParams("Description", "Administrators", "ID", ID, "", "--");
    }

    //Standart anket formaları üçün baza sorğulama
    public static DataTable GridBindDataTable(int PersonsID, string TableName, string[] Columns, string OrderBy = "Order By ID asc")
    {
        return DALC.GetDataTableMultiParams(
          "ROW_NUMBER() over (" + OrderBy + ") as Ss,ID," + string.Join(",", Columns),
          TableName,
          new string[] { "PersonsID", "OrganizationsID", "IsDeleted" },
          new object[] { PersonsID, DALC._GetUsersLogin.OrganizationsID, false },
          OrderBy
          );
    }

    public static DataTable GetSehiyyeTarixce(int PersonsID, string TableName, string[] Columns, string PersonsSehiyyeTarixceNovID, string OrderBy = "Order By ID asc")
    {
        return DALC.GetDataTableMultiParams(
                                     "ROW_NUMBER() over (Order By ID asc) as Ss,ID," + string.Join(",", Columns),
                                     TableName,
                                     new string[] { "PersonsID", "OrganizationsID", "PersonsSehiyyeTarixceNovID", "IsDeleted" },
                                     new object[] { PersonsID, DALC._GetUsersLogin.OrganizationsID, int.Parse(PersonsSehiyyeTarixceNovID), false }, "Order By ID asc");
    }

    public static DataTable GetRegionsListByPermission(string RegionsID)
    {
        if (RegionsID == "*")
            return GetDataTable("ID,Name", "Regions", "Where CountriesID=1 Order by Name");
        else
            return GetDataTable("ID,Name", "Regions", "Where CountriesID=1 and ID in(" + RegionsID + ") Order by Name");
    }

    public static DataTableResult GetPersonsList(
        int Top,
        string DocType,
        string DocNumber,
        string Fin,
        string Soyad,
        string Ad,
        string Ata,
        string YasadigiUnvanRegionsID,
        string DogumY,
        string DogumM,
        string DogumD,
        string Gender,
        string IsOlum,
        string OlumY,
        string OlumM,
        string OlumD,
        string Himaye,
        string HimayeTeshkilatOrShexs,
        string HimayeMehrumOlmaY,
        string HimayeMehrumOlmaM,
        string HimayeMehrumOlmaD,
        string Aliment,
        string AlimentY,
        string AlimentM,
        string AlimentD,
        string Muavinat,
        string MuavinatY,
        string MuavinatM,
        string MuavinatD,
        string SehiyyeQeydiyyat,
        string SehiyyeQeydiyyatRegion,
        string SehiyyeMualice,
        string SehiyyeMualiceRegion,
        string SehiyyeMualiceY,
        string SehiyyeMualiceM,
        string SehiyyeMualiceD,
        string SehiyyeMualiceMonitoringY,
        string SehiyyeMualiceMonitoringM,
        string SehiyyeMualiceMonitoringD,
        string SehiyyeTarixce,
        string SehiyyeTarixceNov,
        string IdmanNailiyyet,
        string IdmanNailiyyetRegion,
        string IdmanNailiyyetNov,
        string IdmanNailiyyetY,
        string IdmanNailiyyetM,
        string IdmanNailiyyetD,
        string Tehsil,
        string TehsilRegion,
        string TehsilYer,
        string TehsilNailiyyet,
        string TehsilNailiyyetRegion,
        string TehsilNailiyyetNov,
        string TehsilNailiyyetY,
        string TehsilNailiyyetM,
        string TehsilNailiyyetD,
        string DunyayaKorpe,
        string DunyayaKorpeRegion,
        string DunyayaKorpeY,
        string DunyayaKorpeM,
        string DunyayaKorpeD,
        string HuquqPozmaQarsi,
        string HuquqPozmaQarsiMadde,
        string HuquqPozmaQarsiY,
        string HuquqPozmaQarsiM,
        string HuquqPozmaQarsiD,
        string HuquqPozma,
        string HuquqPozmaMecelle,
        string HuquqPozmaCezaNov,
        string HuquqPozmaRegion,
        string HuquqPozmaMonitoringY,
        string HuquqPozmaMonitoringM,
        string HuquqPozmaMonitoringD,
        string HuquqPozmaCezaBitmeY,
        string HuquqPozmaCezaBitmeM,
        string HuquqPozmaCezaBitmeD,
        string TerbiyeviTedbir,
        string TerbiyeviTedbirRegion,
        string TerbiyeviTedbirY,
        string TerbiyeviTedbirM,
        string TerbiyeviTedbirD,
        string TerbiyeviTedbirMonitoringY,
        string TerbiyeviTedbirMonitoringM,
        string TerbiyeviTedbirMonitoringD,
        string Meshgulluq,
        string MeshgulluqQeydY,
        string MeshgulluqQeydM,
        string MeshgulluqQeydD,
        string MeshgulluqCixmaY,
        string MeshgulluqCixmaM,
        string MeshgulluqCixmaD,
        string SosialYardim,
        string SosialYardimY,
        string SosialYardimM,
        string SosialYardimD,
        string SosialXidmet,
        string SosialXidmetNov,
        string SosialXidmetY,
        string SosialXidmetM,
        string SosialXidmetD,
        string AsudeDuserge,
        string AsudeDusergeRegion,
        string AsudeDusergeGeldiyiY,
        string AsudeDusergeGeldiyiM,
        string AsudeDusergeGeldiyiD,
        string AsudeDusergeGetdiyiY,
        string AsudeDusergeGetdiyiM,
        string AsudeDusergeGetdiyiD,
        string PersonsStatusID,
        bool Cache = true
        )
    {
        #region Axtarish
        DataTableResult Result = new DataTableResult();
        SqlCommand com = new SqlCommand();
        string AddWhere = " 1=1 ";

        string op1 = ">0", op2 = "<1";

        if (DocType != "0")
        {
            if (DocType != "20,30,40")
            {
                AddWhere += " and P.DocumentTypesID=@DocType and (P.DocumentNumber=@DocNumber or @DocNumber=0)";
                com.Parameters.Add("@DocType", SqlDbType.Int).Value = DocType;
            }
            else
            {
                AddWhere += " and P.DsDocumentTypesID in(20,30,40) and (P.DsNomresi=@DocNumber or @DocNumber=0)";
            }

            com.Parameters.AddWithValue("@DocNumber", DocNumber);
        }


        if (YasadigiUnvanRegionsID != "0")
        {
            AddWhere += " and P.YasadigiUnvanRegionsID=@YasadigiUnvanRegionsID";
            com.Parameters.Add("@YasadigiUnvanRegionsID", SqlDbType.Int).Value = int.Parse(YasadigiUnvanRegionsID);
        }
        else
        {
            if (DALC._GetUsersLogin.PermissionRegions != "*")
            {
                AddWhere += " and P.YasadigiUnvanRegionsID in (" + DALC._GetUsersLogin.PermissionRegions.Trim(',').Trim() + ")";
            }
        }
        #endregion

        #region ətraflı axtarış 1

        if (DogumY != "1000")
        {
            AddWhere += " and SUBSTRING(CAST(DogumTarixi as varchar),1,4)=@DogumY";
            com.Parameters.Add("@DogumY", SqlDbType.VarChar).Value = DogumY;
        }

        if (DogumM != "00")
        {
            AddWhere += " and SUBSTRING(CAST(DogumTarixi as varchar),5,2)=@DogumM";
            com.Parameters.Add("@DogumM", SqlDbType.VarChar).Value = DogumM;
        }

        if (DogumD != "00")
        {
            AddWhere += " and SUBSTRING(CAST(DogumTarixi as varchar),7,2)=@DogumD";
            com.Parameters.Add("@DogumD", SqlDbType.VarChar).Value = DogumD;
        }

        if (OlumY != "1000")
        {
            AddWhere += " and SUBSTRING(CAST(OlumTarixi as varchar),1,4)=@OlumY";
            com.Parameters.Add("@OlumY", SqlDbType.VarChar).Value = OlumY;
        }

        if (OlumM != "00")
        {
            AddWhere += " and SUBSTRING(CAST(OlumTarixi as varchar),5,2)=@OlumM";
            com.Parameters.Add("@OlumM", SqlDbType.VarChar).Value = OlumM;
        }

        if (OlumD != "00")
        {
            AddWhere += " and SUBSTRING(CAST(OlumTarixi as varchar),7,2)=@OlumD";
            com.Parameters.Add("@OlumD", SqlDbType.VarChar).Value = OlumD;
        }

        //if (Himaye != "0")
        //{
        //    AddWhere += " and (Select COUNT(*) From PersonsHimaye Where P.ID=PersonsID and IsDeleted=0)" + (Himaye == "1" ? op1 : op2);
        //}

        //if (Aliment != "0")
        //{
        //    AddWhere += " and (Select COUNT(*) From PersonsAliment Where P.ID=PersonsID and IsDeleted=0)" + (Aliment == "1" ? op1 : op2);
        //}

        //if (Muavinat != "0")
        //{
        //    AddWhere += " and (Select COUNT(*) From PersonsMuavinat Where P.ID=PersonsID and IsDeleted=0)" + (Muavinat == "1" ? op1 : op2);
        //}

        //if (SehiyyeQeydiyyat != "0")
        //{
        //    AddWhere += " and (Select COUNT(*) From PersonsSehiyyeMuessiseQeydiyyat Where P.ID=PersonsID and IsDeleted=0)" + (SehiyyeQeydiyyat == "1" ? op1 : op2);
        //}

        //if (HuquqPozma != "0")
        //{
        //    AddWhere += " and (Select COUNT(*) From PersonsHuquqpozma Where P.ID=PersonsID and IsDeleted=0)" + (HuquqPozma == "1" ? op1 : op2);
        //}

        //if (IdmanNailiyyet != "0")
        //{
        //    AddWhere += " and (Select COUNT(*) From PersonsIdmanNailiyyet Where P.ID=PersonsID and IsDeleted=0)" + (IdmanNailiyyet == "1" ? op1 : op2);
        //}

        //if (TehsilNailiyyet != "0")
        //{
        //    AddWhere += " and (Select COUNT(*) From PersonsTehsilNailiyyet Where P.ID=PersonsID and IsDeleted=0)" + (TehsilNailiyyet == "1" ? op1 : op2);
        //}
        #endregion

        #region ətraflı axtarış Himaye

        if (Himaye != "0")
        {
            string Where = "";

            if (HimayeMehrumOlmaY != "1000")
            {
                Where += " and SUBSTRING(CAST(MehrumOlmaTarixi as varchar),1,4)=@HimayeMehrumOlmaY";
                com.Parameters.Add("@HimayeMehrumOlmaY", SqlDbType.VarChar).Value = HimayeMehrumOlmaY;
            }

            if (HimayeMehrumOlmaM != "00")
            {
                Where += " and SUBSTRING(CAST(MehrumOlmaTarixi as varchar),5,2)=@HimayeMehrumOlmaM";
                com.Parameters.Add("@HimayeMehrumOlmaM", SqlDbType.VarChar).Value = HimayeMehrumOlmaM;
            }

            if (HimayeMehrumOlmaD != "00")
            {
                Where += " and SUBSTRING(CAST(MehrumOlmaTarixi as varchar),7,2)=@HimayeMehrumOlmaD";
                com.Parameters.Add("@HimayeMehrumOlmaD", SqlDbType.VarChar).Value = HimayeMehrumOlmaD;
            }

            AddWhere += @" and (Select COUNT(*) From PersonsHimaye Where P.ID=PersonsID                            
                           and (IsTeshkilat=@HimayeTeshkilatOrShexs or @HimayeTeshkilatOrShexs=-1)  
                           and IsDeleted=0" + Where + ")" + (Himaye == "1" ? op1 : op2);
            com.Parameters.Add("@HimayeTeshkilatOrShexs", SqlDbType.Int).Value = int.Parse(HimayeTeshkilatOrShexs);

        }
        #endregion

        #region ətraflı axtarış Aliment

        if (Aliment != "0")
        {
            string Where = "";

            if (AlimentY != "1000")
            {
                Where += " and SUBSTRING(CAST(AlimentBaslamaTarix as varchar),1,4)=@AlimentY";
                com.Parameters.Add("@AlimentY", SqlDbType.VarChar).Value = AlimentY;
            }

            if (AlimentM != "00")
            {
                Where += " and SUBSTRING(CAST(AlimentBaslamaTarix as varchar),5,2)=@AlimentM";
                com.Parameters.Add("@AlimentM", SqlDbType.VarChar).Value = AlimentM;
            }

            if (AlimentD != "00")
            {
                Where += " and SUBSTRING(CAST(AlimentBaslamaTarix as varchar),7,2)=@AlimentD";
                com.Parameters.Add("@AlimentD", SqlDbType.VarChar).Value = AlimentD;
            }

            AddWhere += " and (Select COUNT(*) From PersonsAliment Where P.ID=PersonsID  and IsDeleted=0" + Where + ")" + (Aliment == "1" ? op1 : op2);

        }
        #endregion

        #region ətraflı axtarış Müavinat

        if (Muavinat != "0")
        {
            string Where = "";

            if (MuavinatY != "1000")
            {
                Where += " and SUBSTRING(CAST(MuavinatinBaslamaTarixi as varchar),1,4)=@MuavinatY";
                com.Parameters.Add("@MuavinatY", SqlDbType.VarChar).Value = MuavinatY;
            }

            if (MuavinatM != "00")
            {
                Where += " and SUBSTRING(CAST(MuavinatinBaslamaTarixi as varchar),5,2)=@MuavinatM";
                com.Parameters.Add("@MuavinatM", SqlDbType.VarChar).Value = MuavinatM;
            }

            if (MuavinatD != "00")
            {
                Where += " and SUBSTRING(CAST(MuavinatinBaslamaTarixi as varchar),7,2)=@MuavinatD";
                com.Parameters.Add("@MuavinatD", SqlDbType.VarChar).Value = MuavinatD;
            }

            AddWhere += " and (Select COUNT(*) From PersonsMuavinat Where P.ID=PersonsID  and IsDeleted=0" + Where + ")" + (Muavinat == "1" ? op1 : op2);

        }
        #endregion

        #region ətraflı axtarış SehiyyeQeydiyyat

        if (SehiyyeQeydiyyat != "0")
        {

            AddWhere += @" and (Select COUNT(*) From PersonsSehiyyeMuessiseQeydiyyat Where P.ID=PersonsID                            
                           and (RegionsID=@SehiyyeQeydiyyatRegion or @SehiyyeQeydiyyatRegion=0)  
                           and IsDeleted=0)" + (SehiyyeQeydiyyat == "1" ? op1 : op2);
            com.Parameters.Add("@SehiyyeQeydiyyatRegion", SqlDbType.Int).Value = int.Parse(SehiyyeQeydiyyatRegion);

        }
        #endregion

        #region ətraflı axtarış SehiyyeMualice

        if (SehiyyeMualice != "0")
        {
            string Where = "";

            if (SehiyyeMualiceY != "1000")
            {
                Where += " and SUBSTRING(CAST(MualiceTarixi as varchar),1,4)=@SehiyyeMualiceY";
                com.Parameters.Add("@SehiyyeMualiceY", SqlDbType.VarChar).Value = SehiyyeMualiceY;
            }

            if (SehiyyeMualiceM != "00")
            {
                Where += " and SUBSTRING(CAST(MualiceTarixi as varchar),5,2)=@SehiyyeMualiceM";
                com.Parameters.Add("@SehiyyeMualiceM", SqlDbType.VarChar).Value = SehiyyeMualiceM;
            }

            if (SehiyyeMualiceD != "00")
            {
                Where += " and SUBSTRING(CAST(MualiceTarixi as varchar),7,2)=@SehiyyeMualiceD";
                com.Parameters.Add("@SehiyyeMualiceD", SqlDbType.VarChar).Value = SehiyyeMualiceD;
            }

            if (SehiyyeMualiceMonitoringY != "1000")
            {
                Where += " and SUBSTRING(CAST(MonitorinqTarixi as varchar),1,4)=@SehiyyeMualiceMonitoringY";
                com.Parameters.Add("@SehiyyeMualiceMonitoringY", SqlDbType.VarChar).Value = SehiyyeMualiceMonitoringY;
            }

            if (SehiyyeMualiceMonitoringM != "00")
            {
                Where += " and SUBSTRING(CAST(MonitorinqTarixi as varchar),5,2)=@SehiyyeMualiceMonitoringM";
                com.Parameters.Add("@SehiyyeMualiceMonitoringM", SqlDbType.VarChar).Value = SehiyyeMualiceMonitoringM;
            }

            if (SehiyyeMualiceMonitoringD != "00")
            {
                Where += " and SUBSTRING(CAST(MonitorinqTarixi as varchar),7,2)=@SehiyyeMualiceMonitoringD";
                com.Parameters.Add("@SehiyyeMualiceMonitoringD", SqlDbType.VarChar).Value = SehiyyeMualiceMonitoringD;
            }

            AddWhere += @" and (Select COUNT(*) From PersonsSehiyyeMuessiseMualice Where P.ID=PersonsID 
                           and (RegionsID=@SehiyyeMualiceRegion or @SehiyyeMualiceRegion=0)  
                           and IsDeleted=0" + Where + ")" + (SehiyyeMualice == "1" ? op1 : op2);
            com.Parameters.Add("@SehiyyeMualiceRegion", SqlDbType.Int).Value = int.Parse(SehiyyeMualiceRegion);

        }


        #endregion

        #region ətraflı axtarış SehiyyeTarixce

        if (SehiyyeTarixce != "0")
        {

            AddWhere += @" and (Select COUNT(*) From PersonsSehiyyeTarixce Where P.ID=PersonsID                            
                           and (PersonsSehiyyeTarixceNovID=@SehiyyeTarixceNov or @SehiyyeTarixceNov=0)  
                           and IsDeleted=0)" + (SehiyyeTarixce == "1" ? op1 : op2);
            com.Parameters.Add("@SehiyyeTarixceNov", SqlDbType.Int).Value = int.Parse(SehiyyeTarixceNov);

        }
        #endregion

        #region ətraflı axtarış IdmanNailiyyet

        if (IdmanNailiyyet != "0")
        {
            string Where = "";

            if (IdmanNailiyyetY != "1000")
            {
                Where += " and SUBSTRING(CAST(YarisTarixi as varchar),1,4)=@IdmanNailiyyetY";
                com.Parameters.Add("@IdmanNailiyyetY", SqlDbType.VarChar).Value = IdmanNailiyyetY;
            }

            if (IdmanNailiyyetM != "00")
            {
                Where += " and SUBSTRING(CAST(YarisTarixi as varchar),5,2)=@IdmanNailiyyetM";
                com.Parameters.Add("@IdmanNailiyyetM", SqlDbType.VarChar).Value = IdmanNailiyyetM;
            }

            if (IdmanNailiyyetD != "00")
            {
                Where += " and SUBSTRING(CAST(YarisTarixi as varchar),7,2)=@IdmanNailiyyetD";
                com.Parameters.Add("@IdmanNailiyyetD", SqlDbType.VarChar).Value = IdmanNailiyyetD;
            }


            AddWhere += @" and (Select COUNT(*) From PersonsIdmanNailiyyet Where P.ID=PersonsID                            
                           and (YarisRegionsID=@IdmanNailiyyetRegion or @IdmanNailiyyetRegion=0)  
                           and (PersonsIdmanNovID=@IdmanNailiyyetNov or @IdmanNailiyyetNov=0)  
                           and IsDeleted=0" + Where + ")" + (IdmanNailiyyet == "1" ? op1 : op2);
            com.Parameters.Add("@IdmanNailiyyetRegion", SqlDbType.Int).Value = int.Parse(IdmanNailiyyetRegion);
            com.Parameters.Add("@IdmanNailiyyetNov", SqlDbType.Int).Value = int.Parse(IdmanNailiyyetNov);
        }

        #endregion

        #region ətraflı axtarış Tehsil

        if (Tehsil != "0")
        {
            AddWhere += @" and (Select COUNT(*) From PersonsTehsilMuessise Where P.ID=PersonsID                            
                           and (MuessiseRegionsID=@TehsilRegion or @TehsilRegion=0)  
                           and (IsEvdeTehsilAlir=@TehsilYer or @TehsilYer=0)  
                           and IsDeleted=0)" + (Tehsil == "1" ? op1 : op2);
            com.Parameters.Add("@TehsilRegion", SqlDbType.Int).Value = int.Parse(TehsilRegion);
            com.Parameters.Add("@TehsilYer", SqlDbType.Int).Value = int.Parse(TehsilYer);
        }

        #endregion

        #region ətraflı axtarış TehsilNailiyyet

        if (TehsilNailiyyet != "0")
        {
            string Where = "";

            if (TehsilNailiyyetY != "1000")
            {
                Where += " and SUBSTRING(CAST(OlimpTarixi as varchar),1,4)=@TehsilNailiyyetY";
                com.Parameters.Add("@TehsilNailiyyetY", SqlDbType.VarChar).Value = TehsilNailiyyetY;
            }

            if (TehsilNailiyyetM != "00")
            {
                Where += " and SUBSTRING(CAST(OlimpTarixi as varchar),5,2)=@TehsilNailiyyetM";
                com.Parameters.Add("@TehsilNailiyyetM", SqlDbType.VarChar).Value = TehsilNailiyyetM;
            }

            if (TehsilNailiyyetD != "00")
            {
                Where += " and SUBSTRING(CAST(OlimpTarixi as varchar),7,2)=@TehsilNailiyyetD";
                com.Parameters.Add("@TehsilNailiyyetD", SqlDbType.VarChar).Value = TehsilNailiyyetD;
            }


            AddWhere += @" and (Select COUNT(*) From PersonsTehsilNailiyyet Where P.ID=PersonsID                            
                           and (OlimpRegionsID=@TehsilNailiyyetRegion or @TehsilNailiyyetRegion=0)  
                           and (PersonsTehsilFennNovID=@TehsilNailiyyetNov or @TehsilNailiyyetNov=0)  
                           and IsDeleted=0" + Where + ")" + (TehsilNailiyyet == "1" ? op1 : op2);
            com.Parameters.Add("@TehsilNailiyyetRegion", SqlDbType.Int).Value = int.Parse(TehsilNailiyyetRegion);
            com.Parameters.Add("@TehsilNailiyyetNov", SqlDbType.Int).Value = int.Parse(TehsilNailiyyetNov);
        }

        #endregion

        #region ətraflı axtarış DunyayaKorpe
        if (DunyayaKorpe != "0")
        {
            string Where = "";

            if (DunyayaKorpeY != "1000")
            {
                Where += " and SUBSTRING(CAST(DogumTarixi as varchar),1,4)=@DunyayaKorpeY";
                com.Parameters.Add("@DunyayaKorpeY", SqlDbType.VarChar).Value = DunyayaKorpeY;
            }

            if (DunyayaKorpeM != "00")
            {
                Where += " and SUBSTRING(CAST(DogumTarixi as varchar),5,2)=@DunyayaKorpeM";
                com.Parameters.Add("@DunyayaKorpeM", SqlDbType.VarChar).Value = DunyayaKorpeM;
            }

            if (DunyayaKorpeD != "00")
            {
                Where += " and SUBSTRING(CAST(DogumTarixi as varchar),7,2)=@DunyayaKorpeD";
                com.Parameters.Add("@DunyayaKorpeD", SqlDbType.VarChar).Value = DunyayaKorpeD;
            }

            AddWhere += " and (Select COUNT(*) From PersonsDunyayaKorpe Where P.ID=PersonsID and (DogumRegionsID=@DunyayaKorpeRegion or @DunyayaKorpeRegion=0)  and IsDeleted=0" + Where + ")" + (DunyayaKorpe == "1" ? op1 : op2);
            com.Parameters.Add("@DunyayaKorpeRegion", SqlDbType.Int).Value = DunyayaKorpeRegion;

        }
        #endregion

        #region ətraflı axtarış HuquqPozmaQarsi

        if (HuquqPozmaQarsi != "0")
        {
            string Where = "";

            if (HuquqPozmaQarsiY != "1000")
            {
                Where += " and SUBSTRING(CAST(QeydealinmaTarixi as varchar),1,4)=@HuquqPozmaQarsiY";
                com.Parameters.Add("@HuquqPozmaQarsiY", SqlDbType.VarChar).Value = HuquqPozmaQarsiY;
            }

            if (HuquqPozmaQarsiM != "00")
            {
                Where += " and SUBSTRING(CAST(QeydealinmaTarixi as varchar),5,2)=@HuquqPozmaQarsiM";
                com.Parameters.Add("@HuquqPozmaQarsiM", SqlDbType.VarChar).Value = HuquqPozmaQarsiM;
            }

            if (HuquqPozmaQarsiD != "00")
            {
                Where += " and SUBSTRING(CAST(QeydealinmaTarixi as varchar),7,2)=@HuquqPozmaQarsiD";
                com.Parameters.Add("@HuquqPozmaQarsiD", SqlDbType.VarChar).Value = HuquqPozmaQarsiD;
            }

            AddWhere += " and (Select COUNT(*) From PersonsHuquqpozmaQarsi Where P.ID=PersonsID and (Madde=@HuquqPozmaQarsiMadde or @HuquqPozmaQarsiMadde='0')  and IsDeleted=0" + Where + ")" + (HuquqPozmaQarsi == "1" ? op1 : op2);
            com.Parameters.Add("@HuquqPozmaQarsiMadde", SqlDbType.NVarChar).Value = HuquqPozmaQarsiMadde;

        }
        #endregion

        #region ətraflı axtarış HuquqPozma

        if (HuquqPozma != "0")
        {
            string Where = "";

            if (HuquqPozmaMonitoringY != "1000")
            {
                Where += " and SUBSTRING(CAST(MonitorinqTarixi as varchar),1,4)=@HuquqPozmaMonitoringY";
                com.Parameters.Add("@HuquqPozmaMonitoringY", SqlDbType.VarChar).Value = HuquqPozmaMonitoringY;
            }

            if (HuquqPozmaMonitoringM != "00")
            {
                Where += " and SUBSTRING(CAST(MonitorinqTarixi as varchar),5,2)=@HuquqPozmaMonitoringM";
                com.Parameters.Add("@HuquqPozmaMonitoringM", SqlDbType.VarChar).Value = HuquqPozmaMonitoringM;
            }

            if (HuquqPozmaMonitoringD != "00")
            {
                Where += " and SUBSTRING(CAST(MonitorinqTarixi as varchar),7,2)=@HuquqPozmaMonitoringD";
                com.Parameters.Add("@HuquqPozmaMonitoringD", SqlDbType.VarChar).Value = HuquqPozmaMonitoringD;
            }

            if (HuquqPozmaCezaBitmeY != "1000")
            {
                Where += " and SUBSTRING(CAST(CezaninBitdiyiTarix as varchar),1,4)=@HuquqPozmaCezaBitmeY";
                com.Parameters.Add("@HuquqPozmaCezaBitmeY", SqlDbType.VarChar).Value = HuquqPozmaCezaBitmeY;
            }

            if (HuquqPozmaCezaBitmeM != "00")
            {
                Where += " and SUBSTRING(CAST(CezaninBitdiyiTarix as varchar),5,2)=@HuquqPozmaCezaBitmeM";
                com.Parameters.Add("@HuquqPozmaCezaBitmeM", SqlDbType.VarChar).Value = HuquqPozmaCezaBitmeM;
            }

            if (HuquqPozmaCezaBitmeD != "00")
            {
                Where += " and SUBSTRING(CAST(CezaninBitdiyiTarix as varchar),7,2)=@HuquqPozmaCezaBitmeD";
                com.Parameters.Add("@HuquqPozmaCezaBitmeD", SqlDbType.VarChar).Value = HuquqPozmaCezaBitmeD;
            }

            AddWhere += @" and (Select COUNT(*) From PersonsHuquqpozma Where P.ID=PersonsID 
                           and (PersonsHuquqpozmaMecelleNovID=@HuquqPozmaMecelle or @HuquqPozmaMecelle=0) 
                           and (PersonsHuquqpozmaCezaNovID=@HuquqPozmaCezaNov or @HuquqPozmaCezaNov=0) 
                           and (MuessiseRegionsID=@HuquqPozmaRegion or @HuquqPozmaRegion=0)  
                           and IsDeleted=0" + Where + ")" + (HuquqPozma == "1" ? op1 : op2);
            com.Parameters.Add("@HuquqPozmaMecelle", SqlDbType.Int).Value = int.Parse(HuquqPozmaMecelle);
            com.Parameters.Add("@HuquqPozmaCezaNov", SqlDbType.Int).Value = int.Parse(HuquqPozmaCezaNov);
            com.Parameters.Add("@HuquqPozmaRegion", SqlDbType.Int).Value = int.Parse(HuquqPozmaRegion);

        }


        #endregion

        #region ətraflı axtarış TerbiyeviTedbir

        if (TerbiyeviTedbir != "0")
        {
            string Where = "";

            if (TerbiyeviTedbirY != "1000")
            {
                Where += " and SUBSTRING(CAST(TetbiqTarixi as varchar),1,4)=@TerbiyeviTedbirY";
                com.Parameters.Add("@TerbiyeviTedbirY", SqlDbType.VarChar).Value = TerbiyeviTedbirY;
            }

            if (TerbiyeviTedbirM != "00")
            {
                Where += " and SUBSTRING(CAST(TetbiqTarixi as varchar),5,2)=@TerbiyeviTedbirM";
                com.Parameters.Add("@TerbiyeviTedbirM", SqlDbType.VarChar).Value = TerbiyeviTedbirM;
            }

            if (TerbiyeviTedbirD != "00")
            {
                Where += " and SUBSTRING(CAST(TetbiqTarixi as varchar),7,2)=@TerbiyeviTedbirD";
                com.Parameters.Add("@TerbiyeviTedbirD", SqlDbType.VarChar).Value = TerbiyeviTedbirD;
            }

            if (TerbiyeviTedbirMonitoringY != "1000")
            {
                Where += " and SUBSTRING(CAST(MonitorinqTarixi as varchar),1,4)=@TerbiyeviTedbirMonitoringY";
                com.Parameters.Add("@TerbiyeviTedbirMonitoringY", SqlDbType.VarChar).Value = TerbiyeviTedbirMonitoringY;
            }

            if (TerbiyeviTedbirMonitoringM != "00")
            {
                Where += " and SUBSTRING(CAST(MonitorinqTarixi as varchar),5,2)=@TerbiyeviTedbirMonitoringM";
                com.Parameters.Add("@TerbiyeviTedbirMonitoringM", SqlDbType.VarChar).Value = TerbiyeviTedbirMonitoringM;
            }

            if (TerbiyeviTedbirMonitoringD != "00")
            {
                Where += " and SUBSTRING(CAST(MonitorinqTarixi as varchar),7,2)=@TerbiyeviTedbirMonitoringD";
                com.Parameters.Add("@TerbiyeviTedbirMonitoringD", SqlDbType.VarChar).Value = TerbiyeviTedbirMonitoringD;
            }

            AddWhere += " and (Select COUNT(*) From PersonsTerbiyeviTedbir Where P.ID=PersonsID and (MuessiseRegionsID=@TerbiyeviTedbirRegion or @TerbiyeviTedbirRegion=0)  and IsDeleted=0" + Where + ")" + (TerbiyeviTedbir == "1" ? op1 : op2);
            com.Parameters.Add("@TerbiyeviTedbirRegion", SqlDbType.Int).Value = int.Parse(TerbiyeviTedbirRegion);

        }
        #endregion

        #region ətraflı axtarış Məşğulluq

        //davam et
        if (Meshgulluq != "0")
        {
            string Where = "";
            if (Meshgulluq == "1")
            {

                if (MeshgulluqQeydY != "1000")
                {
                    Where += " and SUBSTRING(CAST(QeydiyyatTarixi as varchar),1,4)=@MeshgulluqQeydY";
                    com.Parameters.Add("@MeshgulluqQeydY", SqlDbType.VarChar).Value = MeshgulluqQeydY;
                }

                if (MeshgulluqQeydM != "00")
                {
                    Where += " and SUBSTRING(CAST(QeydiyyatTarixi as varchar),5,2)=@MeshgulluqQeydM";
                    com.Parameters.Add("@MeshgulluqQeydM", SqlDbType.VarChar).Value = MeshgulluqQeydM;
                }

                if (MeshgulluqQeydD != "00")
                {
                    Where += " and SUBSTRING(CAST(QeydiyyatTarixi as varchar),7,2)=@MeshgulluqQeydD";
                    com.Parameters.Add("@MeshgulluqQeydD", SqlDbType.VarChar).Value = MeshgulluqQeydD;
                }
                AddWhere += " and (Select COUNT(*) From PersonsMesgulluqVeziyyet Where P.ID=PersonsID and IsIsh=1  and IsDeleted=0" + Where + ")>0";
            }
            else
            {
                //  Where += " and IsIsh=0";
                if (MeshgulluqCixmaY != "1000")
                {
                    Where += " and SUBSTRING(CAST(IsdenCixmaTarixi as varchar),1,4)=@MeshgulluqCixmaY";
                    com.Parameters.Add("@MeshgulluqCixmaY", SqlDbType.VarChar).Value = MeshgulluqCixmaY;
                }

                if (MeshgulluqCixmaM != "00")
                {
                    Where += " and SUBSTRING(CAST(IsdenCixmaTarixi as varchar),5,2)=@MeshgulluqCixmaM";
                    com.Parameters.Add("@MeshgulluqCixmaM", SqlDbType.VarChar).Value = MeshgulluqCixmaM;
                }

                if (MeshgulluqCixmaD != "00")
                {
                    Where += " and SUBSTRING(CAST(IsdenCixmaTarixi as varchar),7,2)=@MeshgulluqCixmaD";
                    com.Parameters.Add("@MeshgulluqCixmaD", SqlDbType.VarChar).Value = MeshgulluqCixmaD;
                }
                AddWhere += @" and (Select COUNT(*) From PersonsMesgulluqVeziyyet Where P.ID=PersonsID and IsIsh=1 and IsDeleted=0" + Where + ")<1 "
                               + (Where.Length > 0 ? "and" : "or") + " ((Select COUNT(*) From PersonsMesgulluqVeziyyet Where P.ID=PersonsID and IsIsh=0  and IsDeleted=0" + Where + ")>0)";
            }

        }
        #endregion

        #region ətraflı axtarış SosialYardim

        if (SosialYardim != "0")
        {
            string Where = "";

            if (SosialYardimY != "1000")
            {
                Where += " and SUBSTRING(CAST(YardimaBaslamaTarixi as varchar),1,4)=@SosialYardimY";
                com.Parameters.Add("@SosialYardimY", SqlDbType.VarChar).Value = SosialYardimY;
            }

            if (SosialYardimM != "00")
            {
                Where += " and SUBSTRING(CAST(YardimaBaslamaTarixi as varchar),5,2)=@SosialYardimM";
                com.Parameters.Add("@SosialYardimM", SqlDbType.VarChar).Value = SosialYardimM;
            }

            if (SosialYardimD != "00")
            {
                Where += " and SUBSTRING(CAST(YardimaBaslamaTarixi as varchar),7,2)=@SosialYardimD";
                com.Parameters.Add("@SosialYardimD", SqlDbType.VarChar).Value = SosialYardimD;
            }

            AddWhere += " and (Select COUNT(*) From PersonsSosialYardim Where P.ID=PersonsID  and IsDeleted=0" + Where + ")" + (SosialYardim == "1" ? op1 : op2);

        }
        #endregion

        #region ətraflı axtarış SosialXidmet

        if (SosialXidmet != "0")
        {
            string Where = "";

            if (SosialXidmetY != "1000")
            {
                Where += " and SUBSTRING(CAST(XidmetBaslamaTarix as varchar),1,4)=@SosialXidmetY";
                com.Parameters.Add("@SosialXidmetY", SqlDbType.VarChar).Value = SosialXidmetY;
            }

            if (SosialXidmetM != "00")
            {
                Where += " and SUBSTRING(CAST(XidmetBaslamaTarix as varchar),5,2)=@SosialXidmetM";
                com.Parameters.Add("@SosialXidmetM", SqlDbType.VarChar).Value = SosialXidmetM;
            }

            if (SosialXidmetD != "00")
            {
                Where += " and SUBSTRING(CAST(XidmetBaslamaTarix as varchar),7,2)=@SosialXidmetD";
                com.Parameters.Add("@SosialXidmetD", SqlDbType.VarChar).Value = SosialXidmetD;
            }

            AddWhere += @" and (Select COUNT(*) From PersonsSosialXidmet Where P.ID=PersonsID  and IsDeleted=0
                           and (PersonsSosialXidmetNovID=@SosialXidmetNov or @SosialXidmetNov=0)" + Where + ")" + (SosialXidmet == "1" ? op1 : op2);
            com.Parameters.Add("@SosialXidmetNov", SqlDbType.Int).Value = int.Parse(SosialXidmetNov);

        }
        #endregion

        #region ətraflı axtarış AsudeDuserge

        if (AsudeDuserge != "0")
        {
            string Where = "";

            if (AsudeDusergeGeldiyiY != "1000")
            {
                Where += " and SUBSTRING(CAST(GeldiyiTarix as varchar),1,4)=@AsudeDusergeGeldiyiY";
                com.Parameters.Add("@AsudeDusergeGeldiyiY", SqlDbType.VarChar).Value = AsudeDusergeGeldiyiY;
            }

            if (AsudeDusergeGeldiyiM != "00")
            {
                Where += " and SUBSTRING(CAST(GeldiyiTarix as varchar),5,2)=@AsudeDusergeGeldiyiM";
                com.Parameters.Add("@AsudeDusergeGeldiyiM", SqlDbType.VarChar).Value = AsudeDusergeGeldiyiM;
            }

            if (AsudeDusergeGeldiyiD != "00")
            {
                Where += " and SUBSTRING(CAST(GeldiyiTarix as varchar),7,2)=@AsudeDusergeGeldiyiD";
                com.Parameters.Add("@AsudeDusergeGeldiyiD", SqlDbType.VarChar).Value = AsudeDusergeGeldiyiD;
            }

            if (AsudeDusergeGetdiyiY != "1000")
            {
                Where += " and SUBSTRING(CAST(GetdiyiTarix as varchar),1,4)=@AsudeDusergeGetdiyiY";
                com.Parameters.Add("@AsudeDusergeGetdiyiY", SqlDbType.VarChar).Value = AsudeDusergeGetdiyiY;
            }

            if (AsudeDusergeGetdiyiM != "00")
            {
                Where += " and SUBSTRING(CAST(GetdiyiTarix as varchar),5,2)=@AsudeDusergeGetdiyiM";
                com.Parameters.Add("@AsudeDusergeGetdiyiM", SqlDbType.VarChar).Value = AsudeDusergeGetdiyiM;
            }

            if (AsudeDusergeGetdiyiD != "00")
            {
                Where += " and SUBSTRING(CAST(GetdiyiTarix as varchar),7,2)=@AsudeDusergeGetdiyiD";
                com.Parameters.Add("@AsudeDusergeGetdiyiD", SqlDbType.VarChar).Value = AsudeDusergeGetdiyiD;
            }


            AddWhere += @" and (Select COUNT(*) From PersonsAsudeDuserge Where P.ID=PersonsID                            
                           and (RegionsID=@AsudeDusergeRegion or @AsudeDusergeRegion=0)  
                           and IsDeleted=0" + Where + ")" + (AsudeDuserge == "1" ? op1 : op2);
            com.Parameters.Add("@AsudeDusergeRegion", SqlDbType.Int).Value = int.Parse(AsudeDusergeRegion);
        }

        #endregion

        if (Fin != "0")
        {
            AddWhere += " and (P.FIN=@FIN or @FIN='0')";
            com.Parameters.Add("@FIN", SqlDbType.VarChar).Value = Fin;
        }

        if (Soyad != "0")
        {
            AddWhere += " and (P.Soyad Like '%'+@Soyad+'%' or @Soyad='0')";
            com.Parameters.Add("@Soyad", SqlDbType.VarChar).Value = Soyad;
        }

        if (Ad != "0")
        {
            AddWhere += " and (P.Ad Like '%'+@Ad+'%' or @Ad='0')";
            com.Parameters.Add("@Ad", SqlDbType.VarChar).Value = Ad;
        }

        if (Ata != "0")
        {
            AddWhere += " and (P.Ata Like '%'+@Ata+'%' or @Ata='0')";
            com.Parameters.Add("@Ata", SqlDbType.VarChar).Value = Ata;
        }

        if (Gender != "-1")
        {
            AddWhere += " and (P.Gender=@Gender or @Gender=-1)";
            com.Parameters.Add("@Gender", SqlDbType.Int).Value = int.Parse(Gender);
        }

        if (IsOlum != "-1")
        {
            AddWhere += " and (P.IsOlum=@IsOlum or @IsOlum=-1)";
            com.Parameters.Add("@IsOlum", SqlDbType.Int).Value = int.Parse(IsOlum);
        }

        //Status həmişə gələcək
        AddWhere += " and (P.PersonsStatusID=@PersonsStatusID)";
        com.Parameters.Add("@PersonsStatusID", SqlDbType.Int).Value = int.Parse(PersonsStatusID);

        string strCommandText = @"Select {0}
                                    From Persons  as P
	                                Left Join Countries as C on P.YasadigiUnvanCountriesID=C.ID
	                                Left Join Regions as R on P.YasadigiUnvanRegionsID = R.ID
                                    Where " + AddWhere + " {1}";

        com.Connection = SqlConn;

        if (string.IsNullOrEmpty(HttpContext.Current.Cache["ListCount"]._ToString()))
        {
            try
            {
                com.Connection.Open();
                com.CommandText = String.Format(strCommandText, "COUNT(*)", "");
                Result.Count = com.ExecuteScalar()._ToInt32();

                if (Cache == true)
                    HttpContext.Current.Cache["ListCount"] = Result.Count;
            }
            catch (Exception er)
            {
                ErrorLogsInsert("DALC.GetPersonsList for Count xəta baş verdi: " + er.Message, false);
                Result.Count = -1;
                return Result;
            }
            finally
            {
                com.Connection.Close();
            }
        }
        else
        {
            Result.Count = HttpContext.Current.Cache["ListCount"]._ToInt32();
        }

        try
        {
            com.CommandText = String.Format(strCommandText, @"Top (@Top)
                                                                    ROW_NUMBER() over (order by P.ID desc) as Ss,
	                                                                P.ID,
	                                                                P.FIN,
	                                                                P.Soyad,
	                                                                P.Ad,
	                                                                P.Ata,
	                                                                " + Config.SqlDateTimeFormat("P.DogumTarixi", "DogumTarixi") + @",
	                                                                C.Name as CountriesName,
	                                                                R.Name as RegionsName,
	                                                                P.YasadigiUnvan", "Order By ID desc");
            com.Parameters.Add("@Top", SqlDbType.Int).Value = Top;
            SqlDataAdapter Da = new SqlDataAdapter(com);
            Da.Fill(Result.Dt);
            return Result;
        }
        catch (Exception er)
        {
            ErrorLogsInsert("DALC.GetPersonsList for DataTable xətası " + er.Message, false);
            Result.Dt = null;
            return Result;
        }
    }

    public static DataTableResult GetUsersList(string Top, string OrganizationsID, string Username, string PassportNumber, string Fullname, string IsActive)
    {
        DataTableResult UsersList = new DataTableResult();
        SqlCommand com = new SqlCommand();
        string strCommandText = @"Select {0} From Users as U Left Join Organizations as O on U.OrganizationsID=O.ID
                                 Where
                                 (U.OrganizationsID=@OrganizationsID or @OrganizationsID=0) and
                                 (U.Username Like '%'+@Username+'%'  or @Username='') and
                                 (U.PassportNumber=@PassportNumber or @PassportNumber=0) and
                                 (U.Fullname Like '%'+@Fullname+'%' or @Fullname='') and U.IsActive=@IsActive";

        com.Parameters.Add("@OrganizationsID", SqlDbType.Int).Value = int.Parse(OrganizationsID);
        com.Parameters.Add("@Username", SqlDbType.VarChar).Value = Username;
        com.Parameters.Add("@PassportNumber", SqlDbType.Int).Value = int.Parse(PassportNumber);
        com.Parameters.Add("@Fullname", SqlDbType.NVarChar).Value = Fullname;
        com.Parameters.Add("@IsActive", SqlDbType.Bit).Value = int.Parse(IsActive);
        com.Connection = SqlConn;
        try
        {
            com.Connection.Open();
            com.CommandText = String.Format(strCommandText, "COUNT(U.ID)");
            UsersList.Count = com.ExecuteScalar()._ToInt32();
        }
        catch (Exception er)
        {
            ErrorLogsInsert("DALC.GetUsersList for Count xəta baş verdi: " + er.Message, false);
            UsersList.Count = -1;
            return UsersList;
        }
        finally
        {
            com.Connection.Close();
        }

        try
        {
            com.CommandText = String.Format(strCommandText, @"Top (@Top)
                                                                    ROW_NUMBER() over (order by U.ID desc) as Ss,
                                                                    O.ShortName as OrganizationsName,
                                                                    U.*,
                                                                    FORMAT(U.Add_Dt,'dd.MM.yyyy HH:mm') as Add_Dt");
            com.Parameters.Add("@Top", SqlDbType.Int).Value = int.Parse(Top);
            SqlDataAdapter Da = new SqlDataAdapter(com);
            Da.Fill(UsersList.Dt);
            return UsersList;
        }
        catch (Exception er)
        {
            ErrorLogsInsert("DALC.GetUsersList for DataTable xətası " + er.Message, false);
            UsersList.Dt = null;
            return UsersList;
        }
    }

    public static DataTable GetUsersByID(string UsersID)
    {
        return GetDataTableParams("Top 1 (Select Name From Organizations Where ID=OrganizationsID) as Orgname, *", "Users", "ID", UsersID, "");
    }

    public static string GetValideyinID(int PersonsID, int PersonsRelativesTypesID)
    {
        return GetDbSingleValuesMultiParams("ID", "PersonsRelatives", new string[] { "PersonsID", "PersonsRelativesTypesID", "IsDeleted" }, new object[] { PersonsID, PersonsRelativesTypesID, false }, "");
    }

    public static DataTable GetPersonsValideyin(int PersonsID, int PersonsRelativesTypesID)
    {
        return GetDataTableMultiParams("Top 1 *", "PersonsRelatives", new string[] { "PersonsID", "PersonsRelativesTypesID", "IsDeleted" }, new object[] { PersonsID, PersonsRelativesTypesID, false }, "");
    }

    public static DataTable GetPersonsRelativesByPersonsID(int PersonsID)
    {
        return GetDataTableParams(@"ROW_NUMBER() over (order by ID desc) as Ss,
                                  (Select Name From Countries Where ID=YasadigiUnvanCountriesID) as CountriesName,
                                  (Select Name From PersonsRelativesTypes Where ID=PersonsRelativesTypesID) as TypeName,
                                  (Select Name From PersonsRelativesMarriage Where ID=PersonsRelativesMarriageID) as Marriage,
                                  (Select Name From Regions Where ID=YasadigiUnvanRegionsID) as RegionsName,*",
                                  "PersonsRelatives", "PersonsRelativesTypesID in(30,40) and IsDeleted=0 and PersonsID", PersonsID, "Order By ID desc");
    }

    public static DataTable GetPersonsRelativesWorkPositions()
    {
        return GetDataTable("*", "PersonsRelativesWorkPositions", "Order By [Priority] asc, Name asc");
    }

    public static DataTable GetCitizenship()
    {
        return GetDataTable("*", "Citizenship", "Order By [Priority] asc, Name asc");
    }

    public static DataTable GetMarriage()
    {
        return GetDataTable("ID,Name", "PersonsRelativesMarriage", "");
    }

    public static DataTableResult GetHistory(string Top, string OrganizationsID, string UsersID, string UsersHistoryTypesID, string UsersPermissionTypesID, string PersonsID = "-1", bool Cache = true)
    {
        DataTableResult HistoryList = new DataTableResult();
        SqlCommand com = new SqlCommand();
        string strCommandText = @"Select {0} From UsersHistory UH
                                             Left Join UsersHistoryTypes UHT on UH.UsersHistoryTypesID = UHT.ID
                                             Left Join Users U on U.ID = UH.UsersID
                                             Left Join Persons P on P.ID = UH.PersonsID
                                             Where
                                             (U.OrganizationsID = @OrganizationsID or @OrganizationsID=0) and
                                             (UH.UsersID = @UsersID or @UsersID=0) and
                                             (UH.UsersHistoryTypesID = @UsersHistoryTypesID or @UsersHistoryTypesID=0) and
                                             (UH.UsersPermissionTypesID = @UsersPermissionTypesID or @UsersPermissionTypesID=0) and
                                             (P.ID=@PersonsID or @PersonsID=-1) {1}";

        com.Parameters.Add("@OrganizationsID", SqlDbType.Int).Value = int.Parse(OrganizationsID);
        com.Parameters.Add("@UsersID", SqlDbType.Int).Value = int.Parse(UsersID);
        com.Parameters.Add("@UsersHistoryTypesID", SqlDbType.Int).Value = int.Parse(UsersHistoryTypesID);
        com.Parameters.Add("@UsersPermissionTypesID", SqlDbType.Int).Value = int.Parse(UsersPermissionTypesID);
        com.Parameters.Add("@PersonsID", SqlDbType.Int).Value = int.Parse(PersonsID);
        com.Connection = SqlConn;

        if (HttpContext.Current.Session["HistoryList"] == null)
        {
            try
            {
                com.Connection.Open();
                com.CommandText = String.Format(strCommandText, "COUNT(UH.ID)", "");
                HistoryList.Count = com.ExecuteScalar()._ToInt32();

                if (Cache == true)
                    HttpContext.Current.Session["HistoryList"] = HistoryList.Count;
            }
            catch (Exception er)
            {
                ErrorLogsInsert("DALC.GetUsersList for Count xəta baş verdi: " + er.Message, false);
                HistoryList.Count = -1;
                return HistoryList;
            }
            finally
            {
                com.Connection.Close();
            }
        }
        else
        {
            HistoryList.Count = HttpContext.Current.Session["HistoryList"]._ToInt32();
        }

        try
        {
            com.CommandText = String.Format(strCommandText, @"Top (@Top) ROW_NUMBER() over (Order by UH.ID desc) as Ss,
                                                            UHT.Name as Type,
                                                            U.Fullname as UserName,
                                                            (P.Soyad +' '+ P.Ad +' '+ P.Ata) as Fullname,
                                                            Uh.LogText as LogText,
                                                            FORMAT(UH.Add_Dt,'dd.MM.yyyy hh:mm') as Date,
                                                            UH.Add_Ip as IP", "Order by UH.ID desc");
            com.Parameters.Add("@Top", SqlDbType.Int).Value = int.Parse(Top);
            SqlDataAdapter Da = new SqlDataAdapter(com);
            Da.Fill(HistoryList.Dt);
            return HistoryList;
        }
        catch (Exception er)
        {
            ErrorLogsInsert("DALC.GetUsersList for DataTable xətası " + er.Message, false);
            HistoryList.Dt = null;
            return HistoryList;
        }
    }

    public static DataTable GetHistoryTypes()
    {
        return GetDataTable("ID,Name", "UsersHistoryTypes", "Order By ID asc");
    }

    public static DataTable GetSoragcaByTableName(string TableName, string OrderBy = "Order By [Priority] asc, Name asc")
    {
        return GetDataTable("ROW_NUMBER() over (Order By [Priority] asc, Name asc) as Ss, *", TableName, OrderBy);
    }

    public static DataTable GetUserNamesByOrganizationsID(string OrganizationsID)
    {
        return GetDataTableParams("ID,Fullname", "Users", "OrganizationsID", OrganizationsID, "Order by Fullname asc");
    }

    public static DataTable GetCountriesList()
    {
        return GetDataTable("ID,Name", "Countries", "Order by ID");
    }

    public static DataTable GetDocumentTypesChildren()
    {
        return GetDataTable("ID,Series", "DocumentTypes", "Where IsChild=1 Order by ID");
    }

    public static DataTable GetDocumentTypesOther()
    {
        return GetDataTable("ID,Name", "DocumentTypes", "Where IsChild=0 Order by ID");
    }

    public static DataTable GetRegionsList(string CountryID = "1")
    {
        return GetDataTableParams("ID,Name", "Regions", "CountriesID", int.Parse(CountryID), "Order by Name");
    }

    public static DataTable GetUsersPermissionIPList(int UsersID)
    {
        return GetDataTableParams("ID,Ip", "UsersPermissionIP", "UsersID", UsersID, "Order By Add_Dt desc");
    }

    public static DataTable GetUsersPermissionTypesList()
    {
        return GetDataTable("ID,Name,Description", "UsersPermissionTypes", "Order by ID asc");
    }

    public static string GetUsersPermissionRegions(int UsersID)
    {
        return GetDbSingleValuesParams("CAST(RegionsID AS varchar)  +','", "UsersPermissionRegions", "UsersID", UsersID, "for xml path('')", "-1");
    }

    public static string GetUsersPermissionTypesID(int UsersID)
    {
        return GetDbSingleValuesParams("CAST(UsersPermissionTypesID AS varchar)  +','", "UsersPermission", "UsersID", UsersID, "for xml path('')", "-1");
    }

    public static string GetUsersPermissionTypesIDForEdit(int UsersID)
    {
        return GetDbSingleValuesParams("CAST(UsersPermissionTypesID AS varchar)  +','", "UsersPermission", "IsEdit=1 and UsersID", UsersID, "for xml path('')", "-1");
    }

    public static DataTable GetOrganizations()
    {
        return GetDataTable("ID,ShortName as Name", "Organizations", "Order By OrganizationsTypesID asc");
    }

    public static DataTable GetPersonsStatus()
    {
        return GetDataTable("ID,Name", "PersonsStatus", "Order by ID");
    }

    public static DataTable GetAccardionTypes()
    {
        string QueryAdd = "";
        SqlCommand com = new SqlCommand();

        if (DALC._GetUsersLogin.PermissionTypes != "*")
        {
            QueryAdd = @"Where ID in
                    (
                    Select UPT.UsersPermissionTypesRootID from
                    UsersPermission UP,
                    UsersPermissionTypes UPT
                    Where UP.UsersPermissionTypesID = UPT.ID and UP.UsersID = @UsersID
                    )";

            com.Parameters.Add("@UsersID", SqlDbType.Int).Value = DALC._GetUsersLogin.ID;
        }

        try
        {
            com.Connection = DALC.SqlConn;
            com.CommandText = "Select * from UsersPermissionTypesRoot " + QueryAdd + " Order by ID";
            DataTable dt = new DataTable();
            new SqlDataAdapter(com).Fill(dt);
            return dt;
        }
        catch (Exception er)
        {
            DALC.ErrorLogsInsert("DALC.GetAccardionTypes catch error: " + er.Message);
            return null;
        }
    }

    public static string GetUsersHistoryCount()
    {
        return GetDbSingleValuesParams("COUNT(*)", "UsersHistory", "PersonsID!=0 and Day(Add_Dt)", DateTime.Now.Day, "");
    }

    public static int InsertSoragca(string TableName, string SoragcaName)
    {
        return InsertDatabase(TableName, new string[] { "Name" }, new object[] { SoragcaName });
    }

    public static int DeleteUsersPermissionIP(int ID)
    {
        SqlCommand com = new SqlCommand("Delete UsersPermissionIP Where ID=@ID", SqlConn);
        com.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
        try
        {
            com.Connection.Open();
            return com.ExecuteNonQuery();
        }
        catch
        {
            DALC.ErrorLogsInsert("DALC.DeleteUsersPermissionIP xəta baş verdi ");
            return -1;
        }
        finally
        {
            com.Connection.Close();
        }
    }

    public static int DeleteList(string TableName, int ID)
    {
        SqlCommand com = new SqlCommand("Delete " + TableName + " Where ID=@ID", SqlConn);
        com.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
        try
        {
            com.Connection.Open();
            return com.ExecuteNonQuery();
        }
        catch
        {
            DALC.ErrorLogsInsert("DALC.DeleteList xəta baş verdi ");
            return -1;
        }
        finally
        {
            com.Connection.Close();
        }
    }

    public static int DeleteUsersPermission(bool IsRegionPermission, int UsersID)
    {
        SqlCommand com = new SqlCommand("Delete " + (IsRegionPermission ? "UsersPermissionRegions" : "UsersPermission") + " Where UsersID=@UsersID", SqlConn);
        com.Parameters.Add("@UsersID", SqlDbType.Int).Value = UsersID;
        try
        {
            com.Connection.Open();
            return com.ExecuteNonQuery();
        }
        catch
        {
            DALC.ErrorLogsInsert("DALC.DeleteUsersPermission xəta baş verdi ");
            return -1;
        }
        finally
        {
            com.Connection.Close();
        }
    }

    //Log Users insert
    public static void UsersHistoryInsert(int Type, int UsersPermissionTypesID, string LogText, int PersonsID = 0)
    {
        Dictionary<string, object> Dictionary = new Dictionary<string, object>();
        Dictionary.Add("UsersHistoryTypesID", Type);
        Dictionary.Add("UsersID", DALC._GetUsersLogin.ID);
        Dictionary.Add("PersonsID", PersonsID);
        Dictionary.Add("UsersPermissionTypesID", UsersPermissionTypesID);
        Dictionary.Add("LogText", LogText);
        Dictionary.Add("Add_Dt", DateTime.Now);
        Dictionary.Add("Add_Ip", HttpContext.Current.Request.UserHostAddress.IPToInteger());

        InsertDatabase("UsersHistory", Dictionary);
    }

    //Log Admin insert
    public static void AdministratorsHistoryInsert(string LogText)
    {
        Dictionary<string, object> Dictionary = new Dictionary<string, object>();
        Dictionary.Add("AdministratorsID", DALC._GetAdministratorsLogin.ID);
        Dictionary.Add("LogText", LogText);
        Dictionary.Add("Add_Dt", DateTime.Now);
        Dictionary.Add("Add_Ip", HttpContext.Current.Request.UserHostAddress.IPToInteger());

        InsertDatabase("AdministratorsHistory", Dictionary);
    }

    //Error log insert
    public static void ErrorLogsInsert(string LogText, bool IsSendMail = false)
    {
        Dictionary<string, object> Dictionary = new Dictionary<string, object>();
        Dictionary.Add("LogText", Config.SizeLimit(LogText, 990, "..."));
        Dictionary.Add("Url", Config.SizeLimit(HttpContext.Current.Request.Url.ToString(), 95, "..."));
        Dictionary.Add("Add_Dt", DateTime.Now);
        Dictionary.Add("Add_Ip", HttpContext.Current.Request.UserHostAddress.IPToInteger());

        InsertDatabase("ErrorLogs", Dictionary, false);

        //Adminə həmdə mail göndərmək lazım olduqda getsin.
        if (IsSendMail)
            DALC.SendAdminMail("DataBank", LogText);
    }

    //Send admin mail sender
    public static void SendAdminMail(string Title, string Messages)
    {
        DALC.SendMail("", Config._GetAppSettings("ErrorLogMailList"), Title, Messages + " Ip: " + HttpContext.Current.Request.UserHostAddress + " DateTime: " + DateTime.Now.ToString("dd.MM.yyyy HH:mm") + " Url: " + HttpContext.Current.Request.Url.ToString());
    }

    //Mail sender.
    public static bool SendMail(string FromMail, string MailList, string Title, string Messages)
    {
        try
        {
            if (FromMail.Length < 1)
                FromMail = Config._GetAppSettings("MailLogin");

            //Mail gonder
            MailMessage MailServer = new MailMessage(FromMail, MailList, "DataBank - " + Title, Messages);

            SmtpClient SmtpMail = new SmtpClient(Config._GetAppSettings("MailServer"));
            SmtpMail.Credentials = new NetworkCredential(Config._GetAppSettings("MailLogin"), Config._GetAppSettings("MailPassword").Decrypt());
            MailServer.BodyEncoding = Encoding.UTF8;
            MailServer.Priority = MailPriority.Normal;
            MailServer.IsBodyHtml = true;

            SmtpMail.Send(MailServer);
            return true;
        }
        catch (Exception er)
        {
            //Əgər log da error veribsə mail göndərirsə təkrar log metoduna qaytarmıyaq.
            if (Messages.IndexOf("[ErrorLogsInsert]") < 0)
                DALC.ErrorLogsInsert("DALC.SendMail catch error: " + er.Message);

            return false;
        }
    }

    // Reports shablondu
    public static DataTable GetReportsInfo()
    {
        string[] Regions = { "Bakı", "Gəncə", "Lənkəran", "Mingəçevir", "Naftalan", "Sumqayıt", "Şəki" };
        DataTable Dt = new DataTable();
        Dt.Columns.Add("Name", typeof(string));
        Dt.Columns.Add("Column1", typeof(int));
        Dt.Columns.Add("Column2", typeof(int));
        Dt.Columns.Add("Column3", typeof(int));
        Dt.Columns.Add("Column4", typeof(int));
        Dt.Columns.Add("Column5", typeof(int));
        Random r = new Random();
        for (int i = 0; i < Regions.Length - 1; i++)
        {
            Dt.Rows.Add(Regions[i], r.Next(1000, 9999), r.Next(1000, 9999), r.Next(1000, 9999), r.Next(1000, 9999), r.Next(0, 100));
        }
        return Dt;
    }
}