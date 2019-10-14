@SET FILES_PATH=".\" 
@SET PROTO_PATH="..\..\ActionEvent\" 
rem @SET COPY_PATH="E:\Pro\Project\ActionEditor\Assets\_Project\Components\ActionEditor\ActionEditor\scripts\LDataModle"

..\..\tools\ProtoGen.exe
@for /r %FILES_PATH% %%a in (*.proto) do ..\..\tools\ProtoGen.exe "%%a" -output_directory=%PROTO_PATH% --proto_path=%cd% --include_imports=%cd%
pause 