local function dotnet(sln)
	targetextension ".dll"

	buildmessage 'Compiling %{file.relpath}'

	filter { "configurations:Debug" }
		buildcommands {
			table.concat({
				'dotnet build',
				'--output "%{cfg.buildtarget.directory}"',
				'"%{cfg.basedir}/'..sln..'"'
			}, " ")
		}

	filter { "configurations:Release", "system:Windows" }
		buildcommands {
			table.concat({
				'dotnet publish',
				'--output "%{cfg.buildtarget.directory}"',
				'--self-contained true',
				'-c Release',
				--'-r win-x64',
				'"%{cfg.basedir}/'..sln..'"'
			}, " ")
		}

	filter { "configurations:Release", "system:Mac" }
		buildcommands {
			table.concat({
				'dotnet publish',
				'--output "%{cfg.buildtarget.directory}"',
				'--self-contained true',
				'-c Release',
				'-r osx-x64',
				'"%{cfg.basedir}/'..sln..'"'
			}, " ")
		}
end

workspace "YTDToolio"
	configurations { "Debug", "Release" }

	location "build/"


project "YTDToolio"
	kind "Makefile"
	dependson "FuckDX"
	files {
		"ytdtoolio/**.cs", "ytdtoolio/ytdtoolio.csproj",
		"ragelib/**.cs", "ragelib/ragelib.csproj"
	}

	dotnet "ytdtoolio.sln"


project "FuckDX"
	kind "SharedLib"
	language "C++"
	cppdialect "C++17"
	files { "fuckdx/**.h", "fuckdx/**.cpp" }

	filter { "configurations:Debug" }
		defines { "DEBUG" }
		symbols "On"

	filter { "configurations:Release" }
		defines { "NDEBUG" }
		optimize "On"	

local make = premake.modules.gmake2
premake.override(make.makefile, "targetRules", function(base, prj)
	_p('SOURCES += \\')
	make.cs.listsources(prj, function(node)
		return node.relpath
	end)
	_p('')
	_p('$(TARGET): $(SOURCES)')
	_p('\t$(BUILDCMDS)')
	_p('')
	_p('clean:')
	_p('\t$(CLEANCMDS)')
	_p('')
end)