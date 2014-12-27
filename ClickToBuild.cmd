
if "%WindowsSdkDir%" neq "" goto build
if exist "%ProgramFiles(x86)%\Microsoft Visual Studio 12.0\VC\vcvarsall.bat" goto initialize12on64
if exist "%ProgramFiles%\Microsoft Visual Studio 12.0\VC\vcvarsall.bat" goto initialize12
if exist "%ProgramFiles(x86)%\Microsoft Visual Studio 11.0\VC\vcvarsall.bat" goto initialize11on64
if exist "%ProgramFiles%\Microsoft Visual Studio 11.0\VC\vcvarsall.bat" goto initialize11
echo "Unable to detect suitable environment. Build may not succeed."
goto build

:initialize11
call "%ProgramFiles%\Microsoft Visual Studio 11.0\VC\vcvarsall.bat" x86
goto build

:initialize11on64
call "%ProgramFiles(x86)%\Microsoft Visual Studio 11.0\VC\vcvarsall.bat" x86
goto build

:initialize12
call "%ProgramFiles%\Microsoft Visual Studio 12.0\VC\vcvarsall.bat" x86
goto build

:initialize12on64
call "%ProgramFiles(x86)%\Microsoft Visual Studio 12.0\VC\vcvarsall.bat" x86
goto build

:build
call build %~1 %~2
pause
goto end

:end