@ECHO OFF

cd /D "%~dp0"

:: Get msbuild into our path
set INSTALLPATH=

if exist "%programfiles(x86)%\Microsoft Visual Studio\Installer\vswhere.exe" (
	for /F "tokens=* USEBACKQ" %%F in (`"%programfiles(x86)%\Microsoft Visual Studio\Installer\vswhere.exe" -version 16.0 -property installationPath`) do set INSTALLPATH=%%F
)

call "%INSTALLPATH%\Common7\Tools\VsDevCmd.bat"

:: Build GTA-Toolkit
cd "gta-toolkit\"
	mkdir "bin"
	msbuild Toolkit.sln -m -nologo -v:m -p:Configuration=Release -r
	copy /y "RageLib.GTA5\bin\x64\Release\net5.0\RageLib.dll" "bin\"
	copy /y "RageLib.GTA5\bin\x64\Release\net5.0\RageLib.GTA5.dll" "bin\"
cd ..