@echo off  
set save_dir=..\protocol\protobuf_a5_extend\cleaner\

rem ============ write to file start for excel svn version ============
rem === write req...
set filename=%save_dir%Proto_CSReq_Cleaner.cs

del /q %filename%

echo using System;>> %filename%
echo using proto_client2access;>> %filename%
echo using proto_ff;>> %filename%
echo public class Proto_CSReq_Cleaner : IProtoCleaner>>%filename%
echo {>>%filename%
echo    public void reset(object data)>>%filename%
echo 	{>>%filename%
echo 	 Proto_CSReq pkg = (Proto_CSReq)data;>>%filename%



set req=message Proto_CSReq
set rsp=message Proto_CSRsp
set endline=message Proto_CSRspOne
set keyword="optional" 

setlocal enabledelayedexpansion
set fn=temp_.txt
set cs=%filename%
set proto=proto_cs.proto

rem ========= process req message ===============
for /f "delims=" %%i in (%proto%) do (
    set "s=%%i"
    if not "!s:message Proto_CSRsp=!" == "!s!" (set flag=) else if defined flag (
		rem echo;!s!
		rem set "m=!s!"
		echo !s! >> !fn!
		rem #get valid field and format to c# code.
		rem for /f "delims=" %%k in ('@findstr /NC:%endline% abc.txt') do (set ooo=!ooo!%%k) 
	)
    rem if not "!s:message Proto_CSReq=!" == "!s!" set "flag=1"
	if not "!s:message Proto_CSReq=!" == "!s!" ( 
		if !s! == !req! set "flag=1"
	)
)


rem #get valid field and format to c# code.
for /f "delims=" %%k in ('@findstr /C:%keyword% %fn%') do (
	set t=%%k 
	echo %%k|find "//opt">nul&&set t=1
	for /f "tokens=1-5" %%a in ("!t!") do (
		set valid=0
		if %%b==int64 (
			echo 	 	pkg.%%c=0;>>%cs% 
			set valid=1
		) 
		if %%b==int32 (
			echo 	 	pkg.%%c=0;>>%cs% 
			set valid=1
		) 
		if %%b==uint32 (
			echo 	 	pkg.%%c=0;>>%cs% 
			set valid=1
		) 
		if %%b==bool (
			echo 	 	pkg.%%c=false;>>%cs% 
			set valid=1
		) 
		if %%b==enMatchGameType (
			echo 	 	pkg.%%c=0;>>%cs% 
			set valid=1
		)
		
		if !valid!==0 (
			if not !t!==1 echo 	 	pkg.%%c=null;>>%cs%
		)
	)
) 
echo 	}>>%cs%
echo }>>%cs%
rem ========= process res message ===============
del /q %fn%

set filename=%save_dir%Proto_CSRsp_Cleaner.cs
set cs=%filename%
del /q %filename%

echo using System;>> %filename%
echo using proto_client2access;>> %filename%
echo using proto_ff;>> %filename%
echo public class Proto_CSRsp_Cleaner : IProtoCleaner>>%filename%
echo {>>%filename%
echo    public void reset(object data)>>%filename%
echo 	{>>%filename%
echo 	 Proto_CSRsp pkg = (Proto_CSRsp)data;>>%filename%


for /f "delims=" %%i in (%proto%) do (
    set "s=%%i"
    if not "!s:message Proto_CSRspOne=!" == "!s!" (set flag=) else if defined flag (
		echo !s! >> !fn!
	)
    if not "!s:message Proto_CSRsp=!" == "!s!" ( 
		if !s! == !rsp! set "flag=1"
	)
)


rem #get valid field and format to c# code.
for /f "delims=" %%k in ('@findstr /C:%keyword% %fn%') do (
	rem format to c# code 
	rem int32,int64,string 
	set t=%%k 
	echo %%k|find "//opt">nul&&set t=1
	for /f "tokens=1-5" %%a in ("!t!") do (
		set valid=0
		if %%b==int64 (
			echo 	 	pkg.%%c=0;>>%cs% 
			set valid=1
		) 
		if %%b==int32 (
			echo 	 	pkg.%%c=0;>>%cs% 
			set valid=1
		) 
		if %%b==uint32 (
			echo 	 	pkg.%%c=0;>>%cs% 
			set valid=1
		) 
		if %%b==bool (
			echo 	 	pkg.%%c=false;>>%cs% 
			set valid=1
		) 
		if %%b==enMatchGameType (
			echo 	 	pkg.%%c=0;>>%cs% 
			set valid=1
		)
		
		if !valid!==0 (
			if not !t!==1 echo 	 	pkg.%%c=null;>>%cs%
		)
	)
) 
echo 	}>>%cs%
echo }>>%cs%
del /q %fn%

pause