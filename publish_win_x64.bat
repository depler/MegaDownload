@echo off

for %%f in (*.csproj) do (
	set project=%%f
	goto build
)

:error
echo Project not found in current folder
exit /b 1

:build
dotnet.exe publish %project% --configuration "Release" --runtime "win-x64" --output "bin_win_x64" --self-contained -p:PublishSingleFile=true -p:EnableCompressionInSingleFile=true -p:PublishTrimmed=true -p:DebugType=None