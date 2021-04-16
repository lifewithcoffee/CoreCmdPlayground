@echo off

::dotnet run %*

:: =========================================================================
SET DEBUG_PATH=%CD%\bin\Debug\net5.0
dotnet %DEBUG_PATH%\corecmdplayground.dll %*

:: =========================================================================
:: or set the debug path to system path, then use the following statement to
:: replace the above one:
:: 
:: dotnet %~dp0corecmdplayground.dll %*
@echo on