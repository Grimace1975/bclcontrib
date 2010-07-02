@echo off
xcopy System.Abstractions\bin\Release\System.Abstractions.* bin\ /Q /R /H /Y
xcopy System.Web.Abstractions\bin\Release\System.Web.Abstractions.* bin\ /Q /R /H /Y
xcopy System.DataEx\bin\Release\System.DataEx.* bin\ /Q /R /H /Y
xcopy System.CoreEx\bin\Release\System.CoreEx.* bin\ /Q /R /H /Y
xcopy System.ServiceModelEx\bin\Release\System.ServiceModelEx.* bin\ /Q /R /H /Y
xcopy System.Web.MvcEx\bin\Release\System.Web.MvcEx.* bin\ /Q /R /H /Y
xcopy System.WebEx\bin\Release\System.WebEx.* bin\ /Q /R /H /Y
xcopy Digital.Cms\bin\Release\Digital.Cms.* bin\ /Q /R /H /Y
:: SqlServer
xcopy System.CoreEx.SqlServer\bin\Release\System.CoreEx.SqlServer.* bin\SqlServer\ /Q /R /H /Y
xcopy System.CoreEx.SqlServer.Proxy\bin\Release\System.CoreEx.SqlServer.Proxy.* bin\SqlServer\ /Q /R /H /Y
:: IOC
xcopy System.CoreEx.Unity\bin\Release\System.CoreEx.Unity.* bin\Ioc\ /Q /R /H /Y


:: Debug
xcopy System.Abstractions\bin\Debug\System.Abstractions.* bin\Debug\ /Q /R /H /Y
xcopy System.Web.Abstractions\bin\Debug\System.Web.Abstractions.* bin\Debug\ /Q /R /H /Y
xcopy System.DataEx\bin\Debug\System.DataEx.* bin\Debug\ /Q /R /H /Y
xcopy System.CoreEx\bin\Debug\System.CoreEx.* bin\Debug\ /Q /R /H /Y
xcopy System.ServiceModelEx\bin\Debug\System.ServiceModelEx.* bin\Debug\ /Q /R /H /Y
xcopy System.Web.MvcEx\bin\Debug\System.Web.MvcEx.* bin\Debug\ /Q /R /H /Y
xcopy System.WebEx\bin\Debug\System.WebEx.* bin\Debug\ /Q /R /H /Y
xcopy Digital.Cms\bin\Debug\Digital.Cms.* bin\Debug\ /Q /R /H /Y
:: SqlServer
xcopy System.CoreEx.SqlServer\bin\Debug\System.CoreEx.SqlServer.* bin\Debug\SqlServer\ /Q /R /H /Y
xcopy System.CoreEx.SqlServer.Proxy\bin\Debug\System.CoreEx.SqlServer.Proxy.* bin\Debug\SqlServer\ /Q /R /H /Y


::
::robocopy Bin\ "C:\_APPLICATION\AFIWEB\Library_\Reference Assemblies\BclContrib" /MIR /TEE
::
robocopy Bin\ "C:\_APPLICATION\CITYOPWEB\Library_\Reference Assemblies\BclContrib" /MIR /TEE
::robocopy Bin\Debug\ "C:\_APPLICATION\xCITYOPWEB\Library_\Reference Assemblies\BclContrib" /MIR /TEE

