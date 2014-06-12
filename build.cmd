if "%~1"=="" build build.proj Build
if "%~2"=="" build %~1 Build
msbuild /t:%~2 %~1 /fl /flp:logfile=build.log

