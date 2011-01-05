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
//    /// <summary>
//    /// Versions the gateway.
//    /// </summary>
//    /// <param name="currencyCode">The currency code.</param>
//    /// <param name="name">The name.</param>
//    //--+ Metatable
//    //--+	If MaxArchiveDepth = -1, archive records will not be purged
//    //--+ cCommandId:
//    //--+	 CopyPublishToDraft: @nKey = current Draft item Key
//    //--+	 >PreviewUri: @nKey = current Publish/Draft item Key
//    //--+	 PrePublish: @nKey = current Draft item Key
//    //--+	 PostPublish: @nKey = current Draft item Key
//    //--+	 Revert: @nKey = current Draft item Key
//    //--+	 UpdateCache: @nKey = HeadKey
//    //--+ @cP0 - Major | Minor | data to pass to PreviewUri
//    //--+ error codes:
//    //--+  0 - success
//    //--+ >0 - key of new item
//    //--+ -201 - PostPublish: ischeckpublish abort
//    //--+ -202 - PostPublish: current item is not a draft
//    //--+ -203 - PostPublish: missing pending-publish version
//    //--+
//    //--+ -101 - PrePublish: has pending-publish version
//    //--+ -102 - PrePublish: attempting to publish non-Draft copy
//    //--+ -1001 - PrePublish(vn): source and destination version are the same
//    //--+ -1002 - PrePublish(vn): abort if source version is incorrect
//    //--+ -1003 - PrePublish(vn): destination version type already exist
//    //--+
//    //--+ -401 - Revert: no archive version to undo from
//    //--+ -402 - Revert: ischeckpublish abort
//    //--+ -301 - CopyPublishToDraft: current item is not a draft
//    //--+ -302 - CopyPublishToDraft: pulish copy does not exist
//    //+ [CLR] http://msdn.microsoft.com/en-us/library/ms131094.aspx
//    [SqlProcedure(Name="fn_IdentityGateway")]
//    public static void IdentityGateway(SqlString cLastModifyBy, SqlString cCultureId, SqlXml cStateXml, SqlString cMethod, SqlString cTable, SqlInt32 nKey, SqlSingle cP0, out SqlSingle nReturnValue2)
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

//CREATE PROCEDURE [dbo].[fn_IdentityGateway](@cReturnId nvarchar(500) = Null Output, @cReturnErrorText nvarchar(1000) = Null Output, @cLastModifyBy nvarchar(100), @nShard int, @cCultureId nvarchar(50), @cMethod nvarchar(50), @cTable nvarchar(100), @nKey int, @cId nvarchar(500), @cName nvarchar(500), @cXml xml = Null) As
//   --[Instinct.Attribute.ApplicationTypeVersion("1.0")]
//   Set NoCount On;
//   Declare @nReturnValue int;
//   --+
//   Declare @cSql nvarchar(max);
//   If (@cMethod = N'Validate') Begin
//   --- VALIDATE ---
//      Set @cSql = N'
//--+ validate table
//If (Exists(
//   Select Top 1 {@cTable}.[Key]
//   From dbo.{@cTable}
//      Where ({@cTable}.Id = @cId)
//      And ({@cTable}.[Key] <> @nKey)
//)) Begin
//   Select @nReturnValue = -1, @cReturnErrorText = N''Item already assigned'';
//   Return;
//End
//Select @nReturnValue = 0, @cReturnErrorText = Null;
//Return;';
//   End Else If (@cMethod = N'VersionedValidate') Begin
//   --- VERSIONED VALIDATE ---
//      Set @cSql = N'
//--+ validate versioned table
//If (Exists(
//   Select Top 1 {@cTable}.[Key]
//   From dbo.{@cTable}
//      Inner Join dbo.{@cTable}Head 
//      On ({@cTable}Head.[Key] = {@cTable}.{@cTable}HeadKey)
//      And ({@cTable}Head.[Key] <> @nKey)
//      Where ({@cTable}.Id = @cId)
//      And ({@cTable}.VersionType In (N''Publish'', N''Draft''))
//)) Begin
//   Select @nReturnValue = -1, @cReturnErrorText = N''Item already assigned'';
//   Return;
//End
//Select @nReturnValue = 0, @cReturnErrorText = Null;
//Return;';
//   End Else If (@cMethod = N'VersionedCustom') Begin
//   --- VERSIONED VALIDATE ---
//      Set @cSql = N'
//Exec @nReturnValue = [dbo].[id_{@cTable}] @nShard, @cCultureId, N''VersionedValidate'', @nKey, @cId, @cXml, @cReturnErrorText Output;
//Return;';
//   End
//   --+ fix-up
//   Set @cSql = Replace(@cSql, N'{@cTable}', @cTable);
//   --+ create identity
//   If (NullIf(@cId, N'') Is Null) Begin
//      If (NullIf(@cName, N'') Is Null) Begin
//         Set @cReturnErrorText = N'InvalidOperation';
//         Return -1;
//      End
//      Select @cId = [dbo].[fn_CreateId~cName](@cName);
//      --+ try ids
//      Declare @cTryId nvarchar(500); 
//      Declare @nTryIdCount int;
//      Select @cTryId = @cId, @nTryIdCount = 2;
//      While (@nTryIdCount < 10) Begin
//         --+ execute
//         Exec sp_executesql @cSql
//         , N'@nReturnValue int Output, @cReturnErrorText nvarchar(1000) = Null Output, @cLastModifyBy nvarchar(100), @nShard int, @cCultureId nvarchar(50), @cMethod nvarchar(50), @cTable nvarchar(100), @nKey int, @cId nvarchar(500), @cXml xml'
//         , @nReturnValue Output, @cReturnErrorText Output, @cLastModifyBy, @nShard, @cCultureId, @cMethod, @cTable, @nKey, @cTryId, @cXml;
//         If (@nReturnValue >= 0) Begin
//            Set @cReturnId = @cTryId;
//            Return @nReturnValue;
//         End
//         Select @cTryId = @cId + Convert(nvarchar(10), @nTryIdCount), @nTryIdCount = @nTryIdCount + 1;
//      End
//      Return @nReturnValue;
//   End
//   --+ execute
//   Exec sp_executesql @cSql
//   , N'@nReturnValue int Output, @cReturnErrorText nvarchar(1000) = Null Output, @cLastModifyBy nvarchar(100), @nShard int, @cCultureId nvarchar(50), @cMethod nvarchar(50), @cTable nvarchar(100), @nKey int, @cId nvarchar(500), @cXml xml'
//   , @nReturnValue Output, @cReturnErrorText Output, @cLastModifyBy, @nShard, @cCultureId, @cMethod, @cTable, @nKey, @cId, @cXml;
//   Set @cReturnId = @cId;
//   Return @nReturnValue;
//GO
