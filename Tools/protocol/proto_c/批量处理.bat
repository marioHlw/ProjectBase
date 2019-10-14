tools\ProtoGen.exe "proto_client2access.proto" -output_directory=temp\ --proto_path=.\ --include_imports=%cd% 
tools\ProtoGen.exe "proto_common.proto" -output_directory=temp\ --proto_path=.\ --include_imports=%cd%
tools\ProtoGen.exe "proto_cs_cmd.proto" -output_directory=temp\ --proto_path=.\ --include_imports=%cd%
tools\ProtoGen.exe "proto_cs.proto" -output_directory=temp\ --proto_path=.\ --include_imports=%cd%

copy /y temp\ProtoClient2Access.cs csharp\ProtoClient2Access.cs
copy /y temp\ProtoCommon.cs csharp\ProtoCommon.cs
copy /y temp\ProtoCs.cs csharp\ProtoCs.cs
copy /y temp\ProtoCsCmd.cs csharp\ProtoCsCmd.cs

del /s /Q temp\
pause 




::%cd%为当前目录，而%~dp0为脚本自身目录