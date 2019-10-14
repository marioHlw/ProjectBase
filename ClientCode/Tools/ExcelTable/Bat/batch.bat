::运行之前请确保以下文件夹存在
::"..\Out\Data\"
::"..\Out\Config\"
::"..\Out\Proto\"
::"..\..\..\Assets\Resources\Databin\TableRes"
::"..\..\..\Assets\Scripts\Proto\Table"
del /q ..\Out\Data\
del /q ..\Out\Config\
del /q ..\Out\Proto\
del /q ..\..\..\Assets\Resources\Databin\TableRes
del /q ..\..\..\Assets\Project\Scripts\Proto\Table

python xls_deploy_tool.py
copy *.bytes ..\Out\Data\
copy *.proto ..\Out\Proto\

@for /f %%a in  ('dir /b ..\Out\Proto\*.proto')  do ..\..\pb_csharp_gen\protogen.exe -i:"%%a" -o:"..\Out\Config\%%~na.cs" 

copy ..\Out\Config\*.cs  ..\..\..\Assets\Project\Scripts\Proto\Table
copy ..\Out\Data\*.bytes  ..\..\..\Assets\Resources\Databin\TableRes

::pause

