@echo off
SET DEBUG_PATH=%CD%\bin\Debug\netcoreapp2.2
SET ASPNETCORE_ENVIRONMENT=Development

dotnet %DEBUG_PATH%\corecmdplayground.dll %*

:: or set the debug path to system path, then use the following statement to
:: replace the above one:
:: 
:: dotnet %~dp0corecmdplayground.dll %*
@echo on