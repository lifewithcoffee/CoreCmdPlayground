@echo off

::dotnet run %*

:: =========================================================================
SET DEBUG_PATH=%CD%\bin\x64\Debug\net7.0
dotnet %DEBUG_PATH%\CoreCmdPlayground.CefSharp.dll %*

::SET Platform=x64
::dotnet run %*

:: =========================================================================
:: or set the debug path to system path, then use the following statement to
:: replace the above one:
:: 
:: dotnet %~dp0corecmdplayground.dll %*
@echo on