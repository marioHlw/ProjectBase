@SET FILES_PATH=".\" 
@SET PROTO_PATH="..\..\ActionEvent" 
@SET COPY_PATH="E:\Pro\Project\ActionEditor\Assets\_Project\Components\ActionEditor\ActionEditor\scripts\LDataModle"

:tools\protogen.exe -h

@for /r %FILES_PATH% %%a in (*.proto) do ..\..\tools\protogen.exe -i:"%%a" -o:"%PROTO_PATH%\%%~na.cs" 

pause 
