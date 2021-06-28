@ECHO OFF

cd /D "%~dp0"

:: Get msbuild into our path
set INSTALLPATH=

if exist "%programfiles(x86)%\Microsoft Visual Studio\Installer\vswhere.exe" (
	for /F "tokens=* USEBACKQ" %%F in (`"%programfiles(x86)%\Microsoft Visual Studio\Installer\vswhere.exe" -version 16.0 -property installationPath`) do set INSTALLPATH=%%F
)

call "%INSTALLPATH%\Common7\Tools\VsDevCmd.bat"

:: Build GTA-Toolkit
cd "vendor\gta-toolkit"
	if not exist Toolkit.sln cd ..\.. & git submodule init & git submodule update & cd "vendor\gta-toolkit"

	:: Clean the Solution
	dotnet sln remove "..\Toolkit.Testing\Benchmarks\Benchmarks.csproj"
	dotnet sln remove "..\Toolkit.Testing\Testing.RDR2\Testing.RDR2.csproj"
	dotnet sln remove "..\Toolkit.Testing\Testing.GTA5\Testing.GTA5.csproj"
	dotnet sln remove "_TestTools.GTA5\ExtractKeysFromDump\ExtractKeysFromDump.csproj"
	dotnet sln remove "RageLib.GTA5.UnitTests\RageLib.GTA5.UnitTests.csproj"

	dotnet sln remove "Libraries\DirectXTex\DirectXTex.vcxproj"
	dotnet remove "RageLib\RageLib.csproj" reference "..\Libraries\DirectXTex\DirectXTex.vcxproj"

	if exist "RageLib\Helpers\DDSIO.cs" del "RageLib\Helpers\DDSIO.cs"
	if exist "RageLib\Helpers\TextureCompression.cs" del "RageLib\Helpers\TextureCompression.cs"
	if exist "RageLib\Helpers\TextureConvert.cs" del "RageLib\Helpers\TextureConvert.cs"

	if exist "bin" rd /s /q "bin\"
	if not exist "bin" mkdir "bin"

	dotnet publish "RageLib.GTA5\RageLib.GTA5.csproj" -c Release -r win-x64 --self-contained true --nologo
	copy /y "RageLib.GTA5\bin\Release\net5.0\win-x64\RageLib.dll" "bin\"
	copy /y "RageLib.GTA5\bin\Release\net5.0\win-x64\RageLib.GTA5.dll" "bin\"
cd ..\..