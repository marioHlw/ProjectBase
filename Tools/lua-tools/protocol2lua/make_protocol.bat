@echo off
set TOOL_PATH=..\tool
set PY_PATH=..\tool\Python27
set PATH=%TOOL_PATH%;%PY_PATH%;%PATH%
set PROTO_SRC=.\protocol

for %%i in (%PROTO_SRC%\*.proto) do (  
    echo gen lua from %%i ...
    protoc.exe --plugin=protoc-gen-lua="..\tool\protoc-gen-lua\plugin\build.bat" -I=%PROTO_SRC% --lua_out=.\output %%i      
    if not %errorlevel% == 0 (
        color E
        pause
        exit
    )
) 

pause
