@ECHO OFF

cd /D "%~dp0"

:: Get msbuild into our path
set INSTALLPATH=

if exist "%programfiles(x86)%\Microsoft Visual Studio\Installer\vswhere.exe" (
  for /F "tokens=* USEBACKQ" %%F in (`"%programfiles(x86)%\Microsoft Visual Studio\Installer\vswhere.exe" -version 16.0 -property installationPath`) do set INSTALLPATH=%%F
)

call "%INSTALLPATH%\Common7\Tools\VsDevCmd.bat"

:: Build FuckDX
cd "fuckdx\"
	premake5 vs2015

	cd "build\"
		msbuild FuckDX.sln /m /nologo /v:m /p:Configuration=Release

		cp "bin\Release\FuckDX.dll" "..\..\bin\"
	cd ..
	rm -r "build\"
cd ..

:: Build YTDToolio
dotnet publish -c Release -r win-x64 --self-contained true
cp "ytdtoolio\bin\Release\net5.0\win-x64\publish/YTDToolio" "bin\"