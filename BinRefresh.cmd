@echo off
xcopy Abstractions\System.Abstractions\bin\Release\System.Abstractions.* bin\ /Q /R /H /Y
::xcopy Abstractions\System.Web.Abstractions\bin\Release\System.Web.Abstractions.* bin\ /Q /R /H /Y
xcopy Core\Quality\System.Core.Quality\bin\Release\System.Core.Quality.* bin\Quality\ /Q /R /H /Y
xcopy Core\Quality\System.Core.Quality+Ninject\bin\Release\System.Core.Quality+Ninject.* bin\Quality\ /Q /R /H /Y
xcopy Core\Quality\System.Core.Quality+StructureMap\bin\Release\System.Core.Quality+StructureMap.* bin\Quality\ /Q /R /H /Y
xcopy Core\Quality\System.Core.Quality+Unity\bin\Release\System.Core.Quality+Unity.* bin\Quality\ /Q /R /H /Y
xcopy Core\Quality\System.Core.Quality+Windsor\bin\Release\System.Core.Quality+Windsor.* bin\Quality\ /Q /R /H /Y
xcopy Core\SqlServer\System.Core.SqlServer\bin\Release\System.Core.SqlServer.* bin\SqlServer\ /Q /R /H /Y
xcopy Core\SqlServer\System.Core.SqlServer+Proxy\bin\Release\System.Core.SqlServer+Proxy.* bin\SqlServer\ /Q /R /H /Y
xcopy Core\System.CoreEx\bin\Release\System.CoreEx.* bin\ /Q /R /H /Y
xcopy Core\System.DataEx\bin\Release\System.DataEx.* bin\ /Q /R /H /Y
xcopy Extents\Digital.Cms\bin\Release\Digital.Cms.* bin\ /Q /R /H /Y
xcopy Services\System.ServiceModelEx\bin\Release\System.ServiceModelEx.* bin\ /Q /R /H /Y
xcopy Web\System.Web.MvcEx\bin\Release\System.Web.MvcEx.* bin\ /Q /R /H /Y
xcopy Web\System.WebEx\bin\Release\System.WebEx.* bin\ /Q /R /H /Y

::
robocopy Bin\ "C:\_APPLICATION\AFIWEB\Library_\Reference Assemblies\BclContrib" /MIR /TEE
::
::robocopy Bin\ "C:\_APPLICATION\CITYOPWEB\Library_\Reference Assemblies\BclContrib" /MIR /TEE
::robocopy Bin\Debug\ "C:\_APPLICATION\xCITYOPWEB\Library_\Reference Assemblies\BclContrib" /MIR /TEE

