@ECHO OFF

cd /D "%~dp0"

:: Get msbuild into our path
set INSTALLPATH=

if exist "%programfiles(x86)%\Microsoft Visual Studio\Installer\vswhere.exe" (
	for /F "tokens=* USEBACKQ" %%F in (`"%programfiles(x86)%\Microsoft Visual Studio\Installer\vswhere.exe" -version 16.0 -property installationPath`) do set INSTALLPATH=%%F
)

call "%INSTALLPATH%\Common7\Tools\VsDevCmd.bat"

:: Build GTA-Toolkit
cd "vendor/gta-toolkit"
	:: Clean the Solution
	dotnet sln remove ..\Toolkit.Testing\Benchmarks\Benchmarks.csproj
	dotnet sln remove ..\Toolkit.Testing\Testing.RDR2\Testing.RDR2.csproj
	dotnet sln remove ..\Toolkit.Testing\Testing.GTA5\Testing.GTA5.csproj
	dotnet sln remove _TestTools.GTA5\ExtractKeysFromDump\ExtractKeysFromDump.csproj

	if not exist "bin" mkdir "bin"
	msbuild Toolkit.sln -m -nologo -v:m -t:RageLib_GTA5 -p:Configuration=Release -p:Platform=x64 -r
	copy /y "RageLib.GTA5\bin\Release\net5.0\DirectXTex.dll" "bin\"
	copy /y "RageLib.GTA5\bin\Release\net5.0\RageLib.dll" "bin\"
	copy /y "RageLib.GTA5\bin\Release\net5.0\RageLib.GTA5.dll" "bin\"
cd ../..