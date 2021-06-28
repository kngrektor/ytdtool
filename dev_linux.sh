#!/bin/bash
cd "$(dirname $0)"

# Build GTA-Toolkit
cd "vendor/gta-toolkit"
	if [ ! -f Toolkit.sln ]; then
		cd ../..
		git submodule init
		git submodule update
		cd "vendor/gta-toolkit"
	fi

	# Clean the Solution
	dotnet sln remove "../Toolkit.Testing/Benchmarks/Benchmarks.csproj"
	dotnet sln remove "../Toolkit.Testing/Testing.RDR2/Testing.RDR2.csproj"
	dotnet sln remove "../Toolkit.Testing/Testing.GTA5/Testing.GTA5.csproj"
	dotnet sln remove "_TestTools.GTA5/ExtractKeysFromDump/ExtractKeysFromDump.csproj"
	dotnet sln remove "RageLib.GTA5.UnitTests/RageLib.GTA5.UnitTests.csproj"

	dotnet sln remove "Libraries/DirectXTex/DirectXTex.vcxproj"
	dotnet remove "RageLib/RageLib.csproj" reference "../Libraries/DirectXTex/DirectXTex.vcxproj"

	[ -f "RageLib/Helpers/DDSIO.cs" ] && rm -f "RageLib/Helpers/DDSIO.cs"
	[ -f "RageLib/Helpers/TextureCompression.cs" ] && rm -f "RageLib/Helpers/TextureCompression.cs"
	[ -f "RageLib/Helpers/TextureConvert.cs" ] && rm -f "RageLib/Helpers/TextureConvert.cs"

	rm -rf "bin/"
	mkdir "bin"

	dotnet publish "RageLib.GTA5/RageLib.GTA5.csproj" -c Release -r linux-x64 --self-contained true --nologo
	cp "RageLib.GTA5/bin/Release/net5.0/linux-x64/RageLib.dll" "bin/"
	cp "RageLib.GTA5/bin/Release/net5.0/linux-x64/RageLib.GTA5.dll" "bin/"
cd ../..