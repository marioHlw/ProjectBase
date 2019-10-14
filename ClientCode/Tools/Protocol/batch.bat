SET NetBatPath = ".\Tools\Protocol\"
cd %NetBatPath%
del /q .\Out\
del /q .\Proto\
del /q ..\..\Assets\Project\Scripts\Proto\NetProto\

copy ..\..\..\ConfigFile\Proto\*.proto  .\Proto\

@for /f %%a in  ('dir /b /s .\Proto\*.proto')  do ..\pb_csharp_gen\protogen.exe -i:"%%a" -o:".\Out\%%~na.cs" 
copy .\Out\*.cs  ..\..\Assets\Project\Scripts\Proto\NetProto

::pause

