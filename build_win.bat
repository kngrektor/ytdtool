@ECHO OFF

cd /D "%~dp0"

:: Get msbuild into our path
set INSTALLPATH=

if exist "%programfiles(x86)%\Microsoft Visual Studio\Installer\vswhere.exe" (
	for /F "tokens=* USEBACKQ" %%F in (`"%programfiles(x86)%\Microsoft Visual Studio\Installer\vswhere.exe" -version 16.0 -property installationPath`) do set INSTALLPATH=%%F
)

call "%INSTALLPATH%\Common7\Tools\VsDevCmd.bat"

if not exist "bin" mkdir "bin"

:: Build FuckDX
cd "fuckdx\"
	premake5 vs2019

	cd "build\"
		msbuild FuckDX.sln -m -nologo -v:m -p:Configuration=Release

		copy /y "bin\Release\FuckDX.dll" "..\..\bin\"
	cd ..

	rd /s /q "build\"
cd ..

:: Build YTDToolio
dotnet publish -c Release -r win-x64 --self-contained true
copy /y "ytdtoolio\bin\Release\net5.0\win-x64\publish\YTDToolio.exe" "bin\"