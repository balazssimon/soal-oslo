set GAC_DIR=c:\Program Files\Microsoft SDKs\Windows\v8.1A\bin\NETFX 4.5.1 Tools
set OSLO_DIR=c:\Program Files\Microsoft Oslo\1.0\bin

"%GAC_DIR%\gacutil.exe" /i "%OSLO_DIR%\System.Dataflow.dll"
"%GAC_DIR%\gacutil.exe" /i "%OSLO_DIR%\Microsoft.M.dll"
"%GAC_DIR%\gacutil.exe" /i "%OSLO_DIR%\Xaml.dll"
"%GAC_DIR%\gacutil.exe" /i "%OSLO_DIR%\Microsoft.Oslo.Internal.dll"

"%GAC_DIR%\gacutil.exe" /i "Sb.OsloExtensions.dll"

reg import Install32.reg
