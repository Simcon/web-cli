dotnet publish -o release/win10-x64 -r win10-x64 -c=Release -p:PublishSingleFile=true /p:PublishTrimmed=true /p:DebugType=None .\WebCLI\WebCLI.csproj
dotnet publish -o release/osx-x64 -r osx-x64 -c=Release -p:PublishSingleFile=true /p:PublishTrimmed=true /p:DebugType=None .\WebCLI\WebCLI.csproj
dotnet publish -o release/linux-x64 -r linux-x64 -c=Release -p:PublishSingleFile=true /p:PublishTrimmed=true /p:DebugType=None .\WebCLI\WebCLI.csproj

$compress = @{
  Path = "release/*"
  CompressionLevel = "Optimal"
  DestinationPath = "release/release.zip"
}
Compress-Archive @compress -Force