if exist "%ProgramFiles(x86)%\MSBuild\12.0\bin" goto msbuildx86
if exist "%ProgramFiles%\MSBuild\12.0\bin" goto msbuild

echo "Unable to detect suitable environment. Check if msbuild is installed."
exit 1

:msbuildx86
set msbuildpath=%ProgramFiles(x86)%\MSBuild\12.0\bin\
goto build

:msbuild
set msbuildpath=%ProgramFiles%\MSBuild\12.0\bin\
goto build

:build
"%msbuildpath%msbuild.exe" /t:Build build\build.proj /fl /flp:logfile=build.log