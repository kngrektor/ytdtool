workspace "FuckDX"
	configurations { "Debug", "Release" }

	location "build/"

	project "FuckDX"
		kind "SharedLib"
		language "C++"
		cppdialect "C++17"
		files { "**.h", "**.cpp" }

		filter { "configurations:Debug" }
			defines { "DEBUG" }
			symbols "On"

		filter { "configurations:Release" }
			defines { "NDEBUG" }
			optimize "On"