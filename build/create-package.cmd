@ECHO OFF
del *.nupkg
..\packages\NuGet.CommandLine.2.8.1\tools\NuGet.exe pack Weakly.nuspec -Symbols
pause
