@ECHO OFF
del *.nupkg
..\packages\NuGet.CommandLine.4.3.0\tools\NuGet.exe pack Weakly.nuspec -Symbols
pause
