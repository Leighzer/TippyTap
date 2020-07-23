dotnet publish -c Release -o ./dist/win-x86 --runtime win-x86 --self-contained true -p:PublishSingleFile=true 
dotnet publish -c Release -o ./dist/win-x64 --runtime win-x64 --self-contained true -p:PublishSingleFile=true 
dotnet publish -c Release -o ./dist/win-arm --runtime win-arm --self-contained true -p:PublishSingleFile=true 
dotnet publish -c Release -o ./dist/osx-x64 --runtime osx-x64 --self-contained true -p:PublishSingleFile=true 
dotnet publish -c Release -o ./dist/linux-x64 --runtime linux-x64 --self-contained true -p:PublishSingleFile=true 
dotnet publish -c Release -o ./dist/linux-arm --runtime linux-arm --self-contained true -p:PublishSingleFile=true

