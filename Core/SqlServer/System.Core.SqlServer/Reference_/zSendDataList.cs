////using Microsoft.SqlServer.Server;
//using System.Data.SqlTypes;
//using System.Data.SqlClient;
//using System.Data;
//using Microsoft.SqlServer.Server;
///// <summary>
///// StoredProcedures
///// </summary>
//[Instinct.Attribute.InstinctTypeVersion("1.1")]
//public partial class StoredProcedures
//{
//    //+ [CLR] http://msdn.microsoft.com/en-us/library/ms131094.aspx
//    [SqlProcedure(Name="fn_Pager")]
//    public static void Pager()
//    {
//        using (SqlConnection connection = new SqlConnection("context connection=true"))
//        {
//            SqlPipe pipe = SqlContext.Pipe;
//            pipe.Send("Starting..");
//            //+ by position?
//            SqlParameter lastNameParameter = new SqlParameter("@cLastModifyBy", SqlDbType.NVarChar);
//            SqlParameter nameParam = new SqlParameter("@cCultureId", SqlDbType.NVarChar);
//            //+
//            SqlCommand command = new SqlCommand("INSERT Sales.Currency (CurrencyCode, Name, ModifiedDate) VALUES(@CurrencyCode, @Name)", connection);
//            connection.Open();
//            pipe.ExecuteAndSend(command);
//            connection.Close();
//        }
//        nReturnValue2 = 0;
//    }
//};

//ALTER Procedure [dbo].[fn_Pager](@cXml xml
//, @cSql nvarchar(max)
//, @cSelectList nvarchar(max)
//, @cOverClause nvarchar(max)
//, @cWithCte nvarchar(max) = Null
//, @cPreExecuteSql nvarchar(max) = Null
//, @nP0 int = Null, @nP1 int = Null, @nP2 int = Null, @nP3 int = Null, @nP4 int = Null
//, @cP0 nvarchar(max) = Null, @cP1 nvarchar(max) = Null, @cP2 nvarchar(max) = Null, @cP3 nvarchar(max) = Null, @cP4 nvarchar(max) = Null
//, @cP5 nvarchar(max) = Null, @cP6 nvarchar(max) = Null, @cP7 nvarchar(max) = Null, @cP8 nvarchar(max) = Null, @cP9 nvarchar(max) = Null
//, @bP0 bit = Null, @bP1 bit = Null, @bP2 bit = Null
//, @dP0 datetime = Null, @dP1 datetime = Null, @dP2 datetime = Null
//, @nMaxPageSize int = 100
//, @bIsDebug bit = 0) As
//    --[Instinct.Attribute.InstinctTypeVersion("1.1")]
//    Set NoCount On;
//    --+
//    If (Left(@cSql, 2) = Char(13) + Char(10)) Begin
//        Set @cSql = SubString(@cSql, 3, Len(@cSql));
//    End;
//    Set @cWithCte = NullIf(Rtrim(@cWithCte), N'');
//    --+
//    Declare @nPageSize int;
//    Declare @nCurrentPage int;
//    Declare @cOrderBy nvarchar(max);
//    --+
//    Set ARITHABORT ON;
//    --+ <root><pager pageSize="" currentPage="" orderBy="" maxPageSize="" /></Root>
//    Declare @bIsExport bit; Set @bIsExport = @cXml.exist('/root/@export');
//    Select @nPageSize = T.row.value(N'@pageSize', N'int'), @nCurrentPage = T.row.value(N'@currentPage', N'int')
//    , @cOrderBy = T.row.value(N'@orderBy', N'nvarchar(4000)'), @nMaxPageSize = IsNull(NullIf(T.row.value(N'@maxPageSize', N'int'), N''), @nMaxPageSize)
//    From @cXml.nodes('/root/pager') T(row);
//    --+
//    Set @nPageSize = IsNull(@nPageSize, 0);
//    Set @nCurrentPage = IsNull((Case When (@nCurrentPage < 1) Then 1 Else @nCurrentPage End), 1);
//    Set @cOrderBy = IsNull(@cOrderBy, N'');
//    If (Len(@cOrderBy) > 0) Begin
//        Set @cOrderBy = N' Order By ' + @cOrderBy;
//    End;
//    If ((@nMaxPageSize > 0) And ((@nPageSize <= 0) Or (@nPageSize >= @nMaxPageSize))) Begin
//        Set @nPageSize = @nMaxPageSize;
//    End;
//    --+
//    Declare @nStartRowId int;
//    Declare @nEndRowId int;
//    Select @nStartRowId = @nPageSize * (@nCurrentPage -  1) + 1
//    , @nEndRowId = @nPageSize * @nCurrentPage
//    , @cPreExecuteSql = IsNull(@cPreExecuteSql, N'');
//    If (Left(@cSql, 19) = N'Select @cSelectList') Begin
//        Set @cSql = Substring(@cSql, 20, Len(@cSql));
//        Declare @cExecSql nvarchar(max);
//        --+
//        If (@nMaxPageSize > 0) Begin
//            Set @cExecSql = @cPreExecuteSql + N'
//With ' + (Case When @cWithCte Is Null Then N'' Else @cWithCte + N', ' End) + N' _Page As (
//    Select Top (@nEndRowId) ROW_NUMBER() Over (' + IsNull(NullIf(@cOrderBy, N''), @cOverClause) + N') As _RowId, ' + @cSelectList + N' ' + @cSql + N' ' + @cOrderBy + N')
//Select _Page.*
//From _Page
//    Where (_Page._RowId >= @nStartRowId)
//Order By _Page._RowId';
//        End Else Begin
//            Set @cExecSql = @cPreExecuteSql + N'
//' + (Case When @cWithCte Is Null Then N'' Else N'With ' + @cWithCte End) + N'
//Select ROW_NUMBER() Over (' + IsNull(NullIf(@cOrderBy, N''), @cOverClause) + N') As _RowId, ' + @cSelectList + N' ' + @cSql + N' ' + @cOrderBy;
//        End;
//If (@bIsDebug = Convert(bit, 1)) Begin
//    Print N'Declare @nStartRowId int; Set @nStartRowId = ' + Convert(nvarchar(10), @nStartRowId) + N';'
//    Print N'Declare @nEndRowId int; Set @nEndRowId = ' + Convert(nvarchar(10), @nEndRowId) + N';'
//    Print N'Declare @nP0 int; Set @nP0 = ' + (Case When @nP0 Is Null Then N'Null' Else Convert(nvarchar(10), @nP0) End) + N';'
//    Print N'Declare @nP1 int; Set @nP1 = ' + (Case When @nP1 Is Null Then N'Null' Else Convert(nvarchar(10), @nP1) End) + N';'
//    Print N'Declare @nP2 int; Set @nP2 = ' + (Case When @nP2 Is Null Then N'Null' Else Convert(nvarchar(10), @nP2) End) + N';'
//    Print N'Declare @nP3 int; Set @nP3 = ' + (Case When @nP3 Is Null Then N'Null' Else Convert(nvarchar(10), @nP3) End) + N';'
//    Print N'Declare @nP4 int; Set @nP4 = ' + (Case When @nP4 Is Null Then N'Null' Else Convert(nvarchar(10), @nP4) End) + N';'
//    Print N'Declare @cP0 nvarchar(max); Set @cP0 = ' + (Case When @cP0 Is Null Then N'Null' Else N'N''' + @cP0 + N'''' End) + N';'
//    Print N'Declare @cP1 nvarchar(max); Set @cP1 = ' + (Case When @cP1 Is Null Then N'Null' Else N'N''' + @cP1 + N'''' End) + N';'
//    Print N'Declare @cP2 nvarchar(max); Set @cP2 = ' + (Case When @cP2 Is Null Then N'Null' Else N'N''' + @cP2 + N'''' End) + N';'
//    Print N'Declare @cP3 nvarchar(max); Set @cP3 = ' + (Case When @cP3 Is Null Then N'Null' Else N'N''' + @cP3 + N'''' End) + N';'
//    Print N'Declare @cP4 nvarchar(max); Set @cP4 = ' + (Case When @cP4 Is Null Then N'Null' Else N'N''' + @cP4 + N'''' End) + N';'
//    Print N'Declare @cP5 nvarchar(max); Set @cP5 = ' + (Case When @cP5 Is Null Then N'Null' Else N'N''' + @cP5 + N'''' End) + N';'
//    Print N'Declare @cP6 nvarchar(max); Set @cP6 = ' + (Case When @cP6 Is Null Then N'Null' Else N'N''' + @cP6 + N'''' End) + N';'
//    Print N'Declare @cP7 nvarchar(max); Set @cP7 = ' + (Case When @cP7 Is Null Then N'Null' Else N'N''' + @cP7 + N'''' End) + N';'
//    Print N'Declare @cP8 nvarchar(max); Set @cP8 = ' + (Case When @cP8 Is Null Then N'Null' Else N'N''' + @cP8 + N'''' End) + N';'
//    Print N'Declare @cP9 nvarchar(max); Set @cP9 = ' + (Case When @cP9 Is Null Then N'Null' Else N'N''' + @cP9 + N'''' End) + N';'
//    Print N'Declare @bP0 bit; Set @bP0 = ' + (Case When @bP0 Is Null Then N'Null' When @bP0 = Convert(bit, 1) Then N'1' Else N'0' End) + N';'
//    Print N'Declare @bP1 bit; Set @bP1 = ' + (Case When @bP1 Is Null Then N'Null' When @bP1 = Convert(bit, 1) Then N'1' Else N'0' End) + N';'
//    Print N'Declare @bP2 bit; Set @bP2 = ' + (Case When @bP2 Is Null Then N'Null' When @bP2 = Convert(bit, 1) Then N'1' Else N'0' End) + N';'
//    Print N'Declare @dP0 datetime; Set @dP0 = ' + (Case When @dP0 Is Null Then N'Null' Else N'''' + Convert(nvarchar(50), @dP0, 121) + N'''' End) + N';'
//    Print N'Declare @dP1 datetime; Set @dP1 = ' + (Case When @dP1 Is Null Then N'Null' Else N'''' + Convert(nvarchar(50), @dP1, 121) + N'''' End) + N';'
//    Print N'Declare @dP2 datetime; Set @dP2 = ' + (Case When @dP2 Is Null Then N'Null' Else N'''' + Convert(nvarchar(50), @dP2, 121) + N'''' End) + N';'
//    Print Substring(@cExecSql, 1, 4000)
//    Print Substring(@cExecSql, 4001, 8000)
//    Print Substring(@cExecSql, 8001, 12000)
//End;
//--+ select sql
//        Insert #Pager
//        Exec sp_executesql @cExecSql, N'@nPageSize int, @nCurrentPage int, @cOrderBy nvarchar(max), @nStartRowId int, @nEndRowId int, @nP0 int, @nP1 int, @nP2 int, @nP3 int, @nP4 int, @cP0 nvarchar(max), @cP1 nvarchar(max), @cP2 nvarchar(max), @cP3 nvarchar(max), @cP4 nvarchar(max), @cP5 nvarchar(max), @cP6 nvarchar(max), @cP7 nvarchar(max), @cP8 nvarchar(max), @cP9 nvarchar(max), @bP0 bit, @bP1 bit, @bP2 bit, @dP0 datetime, @dP1 datetime, @dP2 datetime'
//            , @nPageSize, @nCurrentPage, @cOrderBy, @nStartRowId, @nEndRowId, @nP0, @nP1, @nP2, @nP3, @nP4, @cP0, @cP1, @cP2, @cP3, @cP4, @cP5, @cP6, @cP7, @cP8, @cP9, @bP0, @bP1, @bP2, @dP0, @dP1, @dP2;
//        Declare @nRowCount int Set @nRowCount = @@ROWCOUNT;
//            --+ rowcount
//        If (@nMaxPageSize > 0) Begin
//            Set @cExecSql = @cPreExecuteSql + N'
//' + (Case When @cWithCte Is Null Then N'' Else N'With ' + @cWithCte End) + N'
//Select [RowCount] = Count(*) ' + @cSql;
//--+ count sql
//If (@bIsDebug = Convert(bit, 1)) Begin
//    Print Substring(@cExecSql, 1, 4000);
//    Print Substring(@cExecSql, 4001, 8000);
//End;
//            Exec sp_executesql @cExecSql, N'@nPageSize int, @nCurrentPage int, @cOrderBy nvarchar(max), @nStartRowId int, @nEndRowId int, @nP0 int, @nP1 int, @nP2 int, @nP3 int, @nP4 int, @cP0 nvarchar(max), @cP1 nvarchar(max), @cP2 nvarchar(max), @cP3 nvarchar(max), @cP4 nvarchar(max), @cP5 nvarchar(max), @cP6 nvarchar(max), @cP7 nvarchar(max), @cP8 nvarchar(max), @cP9 nvarchar(max), @bP0 bit, @bP1 bit, @bP2 bit, @dP0 datetime, @dP1 datetime, @dP2 datetime'
//                , @nPageSize, @nCurrentPage, @cOrderBy, @nStartRowId, @nEndRowId, @nP0, @nP1, @nP2, @nP3, @nP4, @cP0, @cP1, @cP2, @cP3, @cP4, @cP5, @cP6, @cP7, @cP8, @cP9, @bP0, @bP1, @bP2, @dP0, @dP1, @dP2;
//        End Else Begin
//            Select [RowCount] = @nRowCount;
//        End;
//    End Else Begin
//        RAISERROR (N'DualSelect must use @cSelectList', 16, 1);
//    End;
//    Return;

