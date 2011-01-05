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
//    [SqlProcedure(Name="fn_LoadTableUid")]
//    public static void LoadTableUid()
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

//CREATE Procedure [dbo].[fn_LoadTableUid](@cTable nvarchar(1000), @cField nvarchar(1000), @nUidLength int, @cExecIsExist nvarchar(4000) = Null) As
//   Set NoCount On
//   --+
//   Declare @cSql nvarchar(4000); Set @cSql = Replace(Replace(Replace('
//   Declare @tUid Table(Uid nvarchar(50) Primary Key)
//   Declare [t{@cTable}] Cursor For
//      Select [{@cTable}].[Key]
//      From dbo.[{@cTable}]
//         Where ([{@cTable}].[{@cField}] Is Null)
//      For Update Of [{@cTable}].[{@cField}]
//   Open [t{@cTable}]
//   --+
//   Declare @nKey int
//   Fetch Next From [t{@cTable}] Into @nKey
//   Declare @nRandomIndex int Set @nRandomIndex = Rand((DatePart(mm, GetDate()) * 100000) + (DatePart(ss, GetDate()) * 1000) + DatePart(ms, GetDate()))
//   While (@@FETCH_STATUS = 0) Begin
//      While (1 = 1) Begin
//         Declare @cUid nvarchar(100) Set @cUid = ''''
//         Declare @nLength int Set @nLength = {@nUidLength}
//         While (@nLength > 0) Begin
//            Set @cUid = @cUid + SubString(''0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ'', Convert(int, Rand() * 36) + 1, 1)
//            Set @nLength = @nLength - 1
//         End
//         If (Not Exists(
//            Select Top 1 [{@cTable}].[Key]
//            From dbo.[{@cTable}]
//               Where ([{@cTable}].[{@cField}] = @cUid)
//         )) If (Not Exists(
//            Select Top 1 tUid.Uid
//            From @tUid tUid
//               Where (tUid.Uid = @cUid)
//         )) Begin
//            {@Break}
//         End
//      End
//      --+
//      Insert @tUid (Uid)
//      Values (@cUid)
//      Update dbo.[{@cTable}] Set [{@cField}] = @cUid
//         Where Current Of [t{@cTable}]
//      Fetch Next From [t{@cTable}] Into @nKey
//   End
//   Close [t{@cTable}]
//   Deallocate [t{@cTable}]'
//   , '{@cTable}', @cTable), '{@cField}', @cField), '{@nUidLength}', @nUidLength);
//   --+
//   If (@cExecIsExist Is Null) Begin
//      Set @cSql = Replace(@cSql, '{@Break}', 'Break');
//   End Else Begin
//      Set @cSql = Replace(@cSql, '{@Break}', '
//Create Table #T (IsExist int);
//Declare @nIsExist int
//Insert #T
//   Exec @nIsExist = ' + Replace(@cExecIsExist, '?', '@cUid') + '
//Drop Table #T
//If (@nIsExist = 0) Begin
//   Break
//End');
//   End
//--Print @cSql;
//   Exec (@cSql);
//   Return;
//GO