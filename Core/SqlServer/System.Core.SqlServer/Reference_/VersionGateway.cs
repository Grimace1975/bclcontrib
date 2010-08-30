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
//    [SqlProcedure(Name="fn_VersionGateway")]
//    public static void VersionGateway(SqlString cLastModifyBy, SqlString cCultureId, SqlXml cStateXml, SqlString cMethod, SqlString cTable, SqlInt32 nKey, SqlSingle cP0, out SqlSingle nReturnValue2)
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

////SET ANSI_NULLS ON
////GO
////SET QUOTED_IDENTIFIER ON
////GO
////ALTER Procedure [dbo].[fn_VersionGateway](@cLastModifyBy nvarchar(100), @cCultureId nvarchar(50), @cStateXml xml, @cMethod nvarchar(50), @cTable nvarchar(100), @nKey int, @cP0 nvarchar(max), @nReturnValue2 nvarchar(100) = Null Output) As
////    --[Instinct.Attribute.ApplicationTypeVersion("1.1")]
////    --+ Metatable
////    --+	If MaxArchiveDepth = -1, archive records will not be purged
////    --+ cCommandId:
////    --+	 CopyPublishToDraft: @nKey = current Draft item Key
////    --+	 >PreviewUri: @nKey = current Publish/Draft item Key
////    --+	 PrePublish: @nKey = current Draft item Key
////    --+	 PostPublish: @nKey = current Draft item Key
////    --+	 Revert: @nKey = current Draft item Key
////    --+	 UpdateCache: @nKey = HeadKey
////    --+ @cP0 - Major | Minor | data to pass to PreviewUri
////    --+ error codes:
////    --+  0 - success
////    --+ >0 - key of new item
////    --+ -201 - PostPublish: ischeckpublish abort
////    --+ -202 - PostPublish: current item is not a draft
////    --+ -203 - PostPublish: missing pending-publish version
////    --+
////    --+ -101 - PrePublish: has pending-publish version
////    --+ -102 - PrePublish: attempting to publish non-Draft copy
////    --+ -1001 - PrePublish(vn): source and destination version are the same
////    --+ -1002 - PrePublish(vn): abort if source version is incorrect
////    --+ -1003 - PrePublish(vn): destination version type already exist
////    --+
////    --+ -401 - Revert: no archive version to undo from
////    --+ -402 - Revert: ischeckpublish abort
////    --+ -301 - CopyPublishToDraft: current item is not a draft
////    --+ -302 - CopyPublishToDraft: pulish copy does not exist
////    Set NoCount On;
////    Declare @cSql nvarchar(max);
////    Declare @cNewVersionIdSql nvarchar(max);
////    If (@cMethod In (N'CopyPublishToDraft', N'PostPublish')) Begin
////        Set @cNewVersionIdSql = N'
////        --+ 1.4. determine new version id
////        Declare @nNewVersionId decimal(18, 4);';
////        If ((@cP0 = N'Major') Or (@cMethod = N'CopyPublishToDraft')) Begin
////            Set @cNewVersionIdSql = @cNewVersionIdSql + N'
////        --+ 1.4.1 update major
////        Update {@cTable}Head Set @nNewVersionId = {@cTable}Head.LastVersionId = IsNull(Floor({@cTable}Head.LastVersionId), 0) + 1
////        From dbo.{@cTable}Head
////            Where ({@cTable}Head.[Key] = @nHeadKey)';
////        End Else If (@cP0 = N'Minor') Begin
////            Set @cNewVersionIdSql = @cNewVersionIdSql + N'
////        --+ 1.4.1 update minor
////        Update {@cTable}Head Set @nNewVersionId = {@cTable}Head.LastVersionId = IsNull({@cTable}Head.LastVersionId, 0) + (Case 
////            When (IsNull({@cTable}Head.LastVersionId, 0) - Floor(IsNull({@cTable}Head.LastVersionId, 0)) < 0.99) Then 0.01
////            Else 0
////            End)
////        From dbo.{@cTable}Head
////            Where ({@cTable}Head.[Key] = @nHeadKey);';
////        End
////    End Else Begin
////        Set @cNewVersionIdSql = N'';
////    End
////    --+
////    If (@cMethod = N'CopyPublishToDraft') Begin
////    --- COPY PUBLISH TO DRAFT ---
////        Set @cSql = N'
////        --+ NOTE: existing draft copy across all languages will be deleted
////        --+ 3.1. select head
////        Declare @nHeadKey int;
////        Select @nHeadKey = {@cTable}.{@cTable}HeadKey
////        From dbo.{@cTable}
////            Where ({@cTable}.[Key] = @nKey);
////        --+ 3.2. validate
////        If (Not Exists(
////            Select Top 1 {@cTable}.[Key]
////            From dbo.{@cTable}
////                Where ({@cTable}.[Key] = @nKey)
////                And ({@cTable}.VersionType = N''Draft'')
////        )) Begin
////            --+  3.2.1 abort if current item is not a draft
////            Set @nResult = -301;
////            Return;
////        End
////        Declare @nPublishVersionKey int;
////        Select Top 1 @nPublishVersionKey = {@cTable}.[Key]
////        From dbo.{@cTable}
////            Where ({@cTable}.{@cTable}HeadKey = @nHeadKey)
////            And ({@cTable}.VersionType = N''Publish'');
////        If (@@ROWCOUNT = 0) Begin
////            --+  3.2.2 abort if publish copy does not exist
////            Set @nResult = -302;
////            Return;
////        End
////        --+ 3.2. delete current draft
////        Delete {@cTable}
////        From dbo.{@cTable}
////            Where ({@cTable}.{@cTable}HeadKey = @nHeadKey)
////            And ({@cTable}.VersionType = N''Draft'');
////        --+ 3.3. copy to draft version
////        Declare @nReturnValue int;
////        Exec @nReturnValue = dbo.vn_{@cTable} @cLastModifyBy, @cCultureId, @cStateXml, @nPublishVersionKey, N''CopyTo'', N''Publish'', N''Draft'';
////        --+ 3.4. copy resource
////        If (@nReturnValue >= 0) Begin
////            --+ 3.4.1. version number
////            --+ 3.4.1.1. get next version number
////            ' + @cNewVersionIdSql + N'
////            --+ 3.4.1.2. update draft version number
////            Update {@cTable} Set VersionDate = GetDate(), VersionBy = @cLastModifyBy, VersionId = @nNewVersionId
////            From dbo.{@cTable}
////                Where ({@cTable}.{@cTable}HeadKey = @nHeadKey)
////                And ({@cTable}.VersionType = N''Draft'');
////            --+ 3.4.2. update Cache information in header
////            Exec @nResult = dbo.fn_VersionGateway @cLastModifyBy, @cCultureId, @cStateXml, N''UpdateCache'', @cTable, @nHeadKey, Null;
////            --+ 3.4.2. copy resource
////            Exec dbo.vn_{@cTable} @cLastModifyBy, @cCultureId, @cStateXml, @nPublishVersionKey, N''CopyResource'', N''Publish'', N''Draft'';
////            ----+ 3.4.3. log
////            --Insert dbo.VersionLog (CreateDate, CreateBy, ItemType, ItemHeadKey, Name, Method, VersionId)
////            --Select GetDate(), @cLastModifyBy, {@cTable}, @nHeadKey
////            --, , @cMethod
////            --From dbo.{@cTable}
////            --	Inner Join dbo.{@cTable}Head
////            --	On ({@cTable}.PageHeadKey = {@cTable}Head.[Key])
////            --	And ({@cTable}
////            --	;
////            Set @nResult = 0;
////            Return;
////        End
////        Set @nResult = @nReturnValue;';
////    End Else If (@cMethod = N'PreviewUri') Begin
////    --- PREVIEW URI ---
////        Set @cSql = N'
////        Exec dbo.vn_{@cTable} @cLastModifyBy, @cCultureId, @cStateXml, @nKey, N''PreviewUri'', @cP0;';
////    End Else If (@cMethod = N'PrePublish') Begin
////    --- PRE-PUBLISH ---
////        Set @cSql = N'
////        --+ 1.1. select head
////        Declare @nHeadKey int;
////        Select @nHeadKey = {@cTable}.{@cTable}HeadKey
////        From dbo.{@cTable}
////            Where ({@cTable}.[Key] = @nKey);
////        --+ 1.2. exec.expire pending-publish (5min)
////        With _PendingPublish As (
////            Select {@cTable}.{@cTable}HeadKey As HeadKey
////            From dbo.{@cTable}
////                Where ({@cTable}.{@cTable}HeadKey = @nHeadKey)
////                And ({@cTable}.VersionType = N''PendingPublish'')
////                And (DateDiff(n, {@cTable}.LastModifyDate, GetDate()) > 5)
////        )
////        Delete {@cTable}
////        From dbo.{@cTable}
////            Inner Join _PendingPublish
////            On ({@cTable}.{@cTable}HeadKey = _PendingPublish.HeadKey)
////            And ({@cTable}.VersionType = N''PendingPublish'');
////        --+ 1.4. check for pending-publish
////        If (Exists(
////            Select Top 1 {@cTable}.{@cTable}HeadKey
////            From dbo.{@cTable}
////                Where ({@cTable}.{@cTable}HeadKey = @nHeadKey)
////                And ({@cTable}.VersionType = N''PendingPublish'')
////        )) Begin
////            --+ 1.4.1. abort if pending-publish copy found
////            Set @nResult = -101;
////            Return;
////        End
////        --+
////        If (Exists(Select Top 1 {@cTable}.[Key] From dbo.{@cTable} Where ({@cTable}.[Key] = @nKey) And ({@cTable}.VersionType = N''Draft''))) Begin
////            Declare @nReturnValue int;
////            Exec @nReturnValue = dbo.vn_{@cTable} @cLastModifyBy, @cCultureId, @cStateXml, @nKey, N''CopyTo'', N''Draft'', N''PendingPublish'';
////            --+ 2. copy resource
////            If (@nReturnValue >= 0) Begin
////                Exec dbo.vn_{@cTable} @cLastModifyBy, @cCultureId, @cStateXml, @nKey, N''CopyResource'', N''Draft'', N''PendingPublish'';
////                Set @nResult = 0;
////                Return;
////            End
////            Set @nResult = @nReturnValue;
////            Return;
////        End Else Begin
////            Set @nResult = -102;
////            Return;
////        End
////        Set @nResult = 0;';
////    End Else If (@cMethod = N'PostPublish') Begin
////    --- POST PUBLISH ---
////        Set @cSql = N'
////        --+ 1.1. select head
////        Declare @nHeadKey int;
////        Select @nHeadKey = {@cTable}.{@cTable}HeadKey
////        From dbo.{@cTable}
////            Where ({@cTable}.[Key] = @nKey);
////        --+ 1.2. check for ischeckpublish, and allow abort
////        Declare @bIsCheckPublish bit;
////        Exec @bIsCheckPublish = dbo.vn_{@cTable} @cLastModifyBy, @cCultureId, @cStateXml, @nKey, N''IsCheckPublish'', Null;
////        If (@bIsCheckPublish <> Convert(bit, 0)) Begin
////            --+ remove pending-publish
////            Delete {@cTable}
////            From dbo.{@cTable}
////                Where ({@cTable}.{@cTable}HeadKey = @nHeadKey)
////                And ({@cTable}.VersionType = N''PendingPublish'');
////            Set @nResult = -201;
////            Return;
////        End
////        --+ 1.3. validate
////        If (Not Exists(
////            Select Top 1 {@cTable}.[Key]
////            From dbo.{@cTable}
////                Where ({@cTable}.[Key] = @nKey)
////                And ({@cTable}.VersionType = N''Draft'')
////        )) Begin
////            --+  1.3.1 abort if current item is not a draft
////            Set @nResult = -202;
////            Return;
////        End Else If (Not Exists(
////            Select Top 1 {@cTable}.[Key]
////            From dbo.{@cTable}
////                Where ({@cTable}.{@cTable}HeadKey = @nHeadKey)
////                And ({@cTable}.VersionType = N''PendingPublish'')
////        )) Begin
////            --+ 1.3.2. abort if pending-publish version not found
////            Set @nResult = -203;
////            Return;
////        End
////        ' + @cNewVersionIdSql + N'
////        --+ 1.7. move publish to archive
////        Update {@cTable} Set VersionDate = GetDate(), VersionBy = @cLastModifyBy
////        , VersionType = N''Archive''
////        From dbo.{@cTable}
////            Where ({@cTable}.{@cTable}HeadKey = @nHeadKey)
////            And ({@cTable}.VersionType = N''Publish'');
////        --+ 1.7. move pending-publish to publish
////        Update {@cTable} Set VersionDate = GetDate(), VersionBy = @cLastModifyBy
////        , VersionType = N''Publish''
////        From dbo.{@cTable}
////            Where ({@cTable}.{@cTable}HeadKey = @nHeadKey)
////            And ({@cTable}.VersionType = N''PendingPublish'');
////        --+ 1.5. update draft
////        Update {@cTable} Set VersionDate = GetDate(), VersionBy = @cLastModifyBy, VersionId = @nNewVersionId
////        , WorkflowStage = N''Draft''
////        From dbo.{@cTable}
////            Where ({@cTable}.{@cTable}HeadKey = @nHeadKey)
////            And ({@cTable}.VersionType = N''Draft'');
////        --+ 1.6. noop
////        --+ 1.8. determine maximum archive depth
////        Declare @nMaxArchiveDepth int;
////        If (Object_Id(N''dbo.{@cTable}Metatable'', N''U'') Is Not Null) Begin
////            Select Top 1 @nMaxArchiveDepth = {@cTable}Metatable.MaxArchiveDepth
////            From dbo.{@cTable}Metatable;
////        End
////        Set @nMaxArchiveDepth = IsNull(@nMaxArchiveDepth, 1);
////        --+ 1.9. purge expired archive
////        If (@nMaxArchiveDepth = 0) Begin
////            --+ 1.9.1 purge all archived versions
////            Delete {@cTable}
////            From dbo.{@cTable}
////                Where ({@cTable}.{@cTable}HeadKey = @nHeadKey)
////                And ({@cTable}.VersionType = N''Archive'');
////        End Else If (@nMaxArchiveDepth > 0) Begin
////            --+ 1.9.2 purge archived versions older than max archive depth
////            --+ 1.9.2.1 determine smallest archive copy version id to keep
////            Declare @nMinArchiveVersionId int;
////            With _Z As (
////                Select {@cTable}.VersionId, Row_Number() Over (Order By {@cTable}.VersionId Desc) As Depth
////                From dbo.{@cTable}
////                    Where ({@cTable}.{@cTable}HeadKey = @nHeadKey)
////                    And ({@cTable}.VersionType = N''Archive'')
////                Group By {@cTable}.VersionId
////            )
////            Select Top 1 @nMinArchiveVersionId = _Z.VersionId
////            From _Z
////                Where (_Z.Depth > @nMaxArchiveDepth)
////            Order By _Z.Depth;
////            --+ 1.9.2.3 delete all archive copies smaller that smallest version id to keep
////            Delete {@cTable}
////            From dbo.{@cTable}
////                Where ({@cTable}.{@cTable}HeadKey = @nHeadKey)
////                And ({@cTable}.VersionId <= @nMinArchiveVersionId)
////                --+ insanity check
////                And ({@cTable}.VersionType = N''Archive'');
////        End
////        --+ 2.0. update Cache information in header
////        Exec @nResult = dbo.fn_VersionGateway @cLastModifyBy, @cCultureId, @cStateXml, N''UpdateCache'', @cTable, @nHeadKey, Null;
////        If (@nResult >= 0) Begin
////            Exec dbo.vn_{@cTable} @cLastModifyBy, @cCultureId, @cStateXml, @nKey, N''OnPostPublish'', Null, Null;
////        End';
////    End Else If (@cMethod = N'Revert') Begin
////    --- REVERT ---
////        Set @cSql = N'
////        --+ 4.1. select head
////        Declare @nHeadKey int;
////        Select @nHeadKey = {@cTable}.{@cTable}HeadKey
////        From dbo.{@cTable}
////            Where ({@cTable}.[Key] = @nKey);
////        --+ 4.2. locate latest archive version
////        Declare @nArchiveVersionId decimal(18, 4);
////        Select Top 1 @nArchiveVersionId = {@cTable}.VersionId
////        From dbo.{@cTable}
////            Where ({@cTable}.{@cTable}HeadKey = @nHeadKey)
////            And ({@cTable}.VersionType = N''Archive'')
////        Order By {@cTable}.VersionId Desc;
////        --+ 4.3. abort if archive version does not exist
////        If (@nArchiveVersionId Is Null) Begin
////            Set @nResult = -401;
////            Return;
////        End
////--		--+ 4.5. check for ischeckpublish, and allow abort
////--		Declare @bIsCheckPublish bit;
////--		Exec @bIsCheckPublish = dbo.vn_{@cTable} @cLastModifyBy, @cCultureId, @cStateXml, @nKey, N''IsCheckPublish'', Null;
////--		If (@bIsCheckPublish <> Convert(bit, 0)) Begin
////--			Set @nResult = -402;
////--			Return;
////--		End
////        --+ 4.6. flag publish as pending-delete
////        Update {@cTable} Set VersionDate = GetDate(), VersionBy = @cLastModifyBy
////        , VersionType = N''PendingDelete''
////        From dbo.{@cTable}
////            Where ({@cTable}.{@cTable}HeadKey = @nHeadKey)
////            And ({@cTable}.VersionType = N''Publish'');
////        --+ 2.6. rollback archive to publish
////        Update {@cTable} Set VersionDate = GetDate(), VersionBy = @cLastModifyBy
////        , VersionType = N''Publish''
////        From dbo.{@cTable}
////            Where ({@cTable}.{@cTable}HeadKey = @nHeadKey)
////            And ({@cTable}.VersionType = N''Archive'')
////            And ({@cTable}.VersionId = @nArchiveVersionId);
////        --+ 2.7. delete pending-delete
////        Delete {@cTable}
////        From dbo.{@cTable}
////            Where ({@cTable}.{@cTable}HeadKey = @nHeadKey)
////            And ({@cTable}.VersionType = N''PendingDelete'');
////        --+ 3.8. update Cache information in header
////        Exec @nResult = dbo.fn_VersionGateway @cLastModifyBy, @cCultureId, @cStateXml, N''UpdateCache'', @cTable, @nHeadKey, Null;
////        --+
////        Set @nResult = 0;';
////    End Else if (@cMethod = N'UpdateCache') Begin
////    --- UPDATE CACHE ---
////        Set @cSql = N'
////        --+ 1.0. head key
////        Declare @nHeadKey int;
////        --+ 1.1. head record -- @nKey is head key
////        Set @nHeadKey = @nKey;
////        --+ 1.2. version cache update
////        Update {@cTable}Head Set VersionCache = (
////            Select _Version.VersionType As [@type]
////            , (
////                Select _Culture.Id As [@cultureId]
////                , IsNull(_Item.[Key], _Default.[Key]) As [@key]
////                From dbo.[Language] _Culture
////                    Left Join dbo.{@cTable} _Item
////                    On (_Culture.Id = _Item.CultureId)
////                    And (_Item.{@cTable}HeadKey = _Head.[Key])
////                    And (_Item.VersionType = _Version.VersionType)
////                    Cross Join dbo.{@cTable} _Default
////                    Where (_Default.{@cTable}HeadKey = _Head.[Key])
////                    And (_Default.VersionType = _Version.VersionType)
////                    And (_Default.CultureId = N''en-US'')
////                For Xml Path(''item''), Type
////            )
////            From dbo.{@cTable}Head _Head
////                Cross Join (
////                    Select N''Draft'' As VersionType
////                    Union All
////                    Select N''Publish'' As VersionType
////                ) _Version
////                Where (_Head.[Key] = {@cTable}Head.[Key])
////            For Xml Path (N''version''), Root(N''root''), Type
////        )
////        , ManageVersionCache = (
////            Select (
////                Select _Default.[Key]
////                From dbo.{@cTable} _Default
////                    Where (_Default.{@cTable}HeadKey = _Head.[Key])
////                    And (_Default.VersionType = N''Draft'')
////                    And (_Default.CultureId = N''en-US'')
////            ) As [@pagerKey]
////            , (
////                Select _Culture.Id As [@cultureId]
////                , IsNull(_Item.[Key], _Default.[Key]) As [@key]
////                From dbo.[Language] _Culture
////                    Left Join dbo.{@cTable} _Item
////                    On (_Culture.Id = _Item.CultureId)
////                    And (_Item.{@cTable}HeadKey = _Head.[Key])
////                    And (_Item.VersionType = N''Draft'')
////                    Cross Join dbo.{@cTable} _Default
////                    Where (_Default.{@cTable}HeadKey = _Head.[Key])
////                    And (_Default.VersionType = N''Draft'')
////                    And (_Default.CultureId = N''en-US'')
////                For Xml Path(''item''), Type
////            )
////            From dbo.{@cTable}Head _Head
////                Where (_Head.[Key] = {@cTable}Head.[Key])
////            For Xml Path (N''manage''), Root(N''root''), Type
////        )
////        From dbo.{@cTable}Head
////            Where ({@cTable}Head.[Key] = @nHeadKey);
////        Set @nResult = 0;';
////    End
////    --+ fix-up
////    Set @cSql = Replace(@cSql, N'{@cTable}', @cTable);
////    Set @cCultureId = IsNull(@cCultureId, N'en-US');
////    --+ execute
////--Print SubString(@cSql, 1, 4000);
////--Print SubString(@cSql, 4001, 8000);
////    Declare @nResult int;
////    Exec sp_executesql @cSql
////    , N'@cLastModifyBy nvarchar(100), @cCultureId nvarchar(50), @cMethod nvarchar(50), @cTable nvarchar(100), @cStateXml xml, @nKey int, @cP0 nvarchar(max), @nResult int out, @nReturnValue2 nvarchar(100) = Null Output'
////    , @cLastModifyBy, @cCultureId, @cMethod, @cTable, @cStateXml, @nKey, @cP0, @nResult Output, @nReturnValue2 Output;
////    Return @nResult;
