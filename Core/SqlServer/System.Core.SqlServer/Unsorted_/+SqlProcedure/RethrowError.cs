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
//    [SqlProcedure(Name="fn_RethrowError")]
//    public static void RethrowError()
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

//CREATE Procedure [dbo].[fn_RethrowError] As Begin
//   Set NoCount On;
//   --+
//   If (ERROR_NUMBER() Is Null) Begin
//      Return;
//   End;
//   --+
//   Declare @ErrorMessage nvarchar(max), @ErrorNumber int, @ErrorSeverity int, @ErrorState int, @ErrorLine int, @ErrorProcedure nvarchar(128);
//   Select @ErrorNumber = ERROR_NUMBER()
//   , @ErrorSeverity = Case When ERROR_SEVERITY() > 18 Then 18 Else ERROR_SEVERITY() End
//   , @ErrorState = ERROR_STATE()
//   , @ErrorLine = ERROR_LINE(), @ErrorProcedure = IsNull(ERROR_PROCEDURE(), '-')
//   , @ErrorMessage = N'Error %d, Level %d, State %d, Procedure %s, Line %d, ' + 'Message: ' + ERROR_MESSAGE();
//   --+ raise error
//   Raiserror (@ErrorMessage, @ErrorSeverity, 1, @ErrorNumber, @ErrorSeverity, @ErrorState, @ErrorProcedure, @ErrorLine);
//End;
