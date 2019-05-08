@echo off

setlocal & pushd .

:: Set variables.
set workingDir=%~dp0
set DataDllPath=%workingDir%..\src\ProductStore.Domain.Data\
set EFMigrationTool="%workingDir%\EntityFramework.6.2.0\Migrate.exe"
set EFMigrationScript=ProductStore.Domain.Data.dll /StartUpDirectory="%DataDllPath%\\bin\\debug\\" /startupConfigurationFile="%DataDllPathh%\App.config" /verbose

echo %DataDllPath%
echo %EFMigrationTool%


call %EFMigrationTool% %EFMigrationScript%
IF %ERRORLEVEL% NEQ 0 goto :Error
IF /I '%~2' == '%runOnlyOneStep%' goto :End

:Error
echo Error occurred...
cd "%workingDir%"
pause
exit /B 1

:End
echo Finished
pause
exit /B 0