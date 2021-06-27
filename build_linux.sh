cd "$(dirname $0)"

mkdir -p "bin"

# Build FuckDX
cd "fuckdx/"
	premake5 gmake2

	cd "build/"
		make config=release

		cp "bin/Release/libFuckDX.so" "../../bin/"
	cd ..
	rm -r "build/"
cd ..

# Build YTDToolio
dotnet publish -c Release -r linux-x64 --self-contained true --nologo
cp "ytdtoolio/bin/Release/net5.0/linux-x64/publish/YTDToolio" "bin/"