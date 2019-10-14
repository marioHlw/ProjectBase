@echo off  
rem 直接运行脚本 请设置该目录为项目scripts路径，编辑器运行会自动设置。
rem set save_dir=D:\Hunter\Assets\_Project\Scripts
rem 项目svn协议目录
set src_dir=https://192.168.0.128/svn/Hunter/Tools/Protocol/common  
echo %src_dir%  
set keyword="Last Changed Rev"  
for /f "delims=" %%i in ('svn info %src_dir% ^| findstr /C:%keyword%') do (
set rev=%%i  
)
rem echo %rev%  
set VAT=%rev:Last Changed Rev:=%
set rev=%VAT: =%
rem echo %rev%

rem 项目svn协议目录
set src_dir2=https://192.168.0.128/svn/Hunter/Tools/Protocol/proto_cs  
@echo %src_dir2%  
for /f "delims=" %%i in ('svn info %src_dir2% ^| findstr /C:%keyword%') do (
@set rev2=%%i  
)
rem @echo %rev2%  
@set VAT2=%rev2:Last Changed Rev: =%
@set rev2=%VAT2%
rem @echo cs %rev2%
rem @echo common %rev%

if %rev2% gtr %rev% set rev=%rev2%
@echo %rev%

set filename=%save_dir%\protocol_version.cs

del /q %filename%

echo using UnityEngine;>> %filename%
echo using System.Collections; >> %filename%

echo public class Protocol_version >> %filename%
echo { >> %filename%
echo public const uint clientProtoVersion=%rev%; >> %filename%

echo }>>%filename%
echo on
svn cleanup
svn commit -m "protocol version changed." %filename%
echo protocol version is: %rev%

rem add excel config svn check.
@echo off  
set src_dir3=https://192.168.0.128/svn/Hunter/Tools/ExcelTool/excelRes
echo %src_dir3%  
set keyword="Last Changed Rev"  
for /f "delims=" %%i in ('svn info %src_dir3% ^| findstr /C:%keyword%') do (
set rev3=%%i  
)

set VAT3=%rev3:Last Changed Rev: =%
set rev3=%VAT3: =%
echo %rev3%

rem ============ write to file start for excel svn version ============
set filename=%save_dir%\configdata_version.cs

del /q %filename%

echo using UnityEngine;>> %filename%
echo using System.Collections; >> %filename%

echo public class configdata_version >> %filename%
echo { >> %filename%
echo public const uint clientConfigDataVersion=%rev3%; >> %filename%

echo }>>%filename%
echo on
svn cleanup
svn commit -m "update configdata version." %filename%
echo now the last configdata version is: %rev3%
rem ============== write to file end for excel svn version =============

pause 