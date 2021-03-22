[![Support me on Patreon](https://img.shields.io/endpoint.svg?url=https%3A%2F%2Fshieldsio-patreon.vercel.app%2Fapi%3Fusername%3Dkngrektor%26type%3Dpatrons&style=flat)](https://patreon.com/kngrektor)

# YTD Tool

Simple tooling to list, unpack/export and pack/import textures of/to a `*.ytd` GTA V (PC) TextureDictionary file. This could be used to e.g. automate resizing the textures of a FiveM resource.

```sh
YTDToolio unpack very_cool_car.ytd
myFavoriteResizingTool very_cool_car/*
YTDToolio pack very_cool_car -d very_cool_car_small
```

## Usage

* `YTDToolio list   <.ytd file>`
* `YTDToolio unpack <.ytd file> [-d <destination folder>]`
* `YTDToolio pack   <folder>    [-d <destination .ytd file>]`

## TODO

* Switch to [Neos' RageLib fork](https://github.com/carmineos/gta-toolkit) which has several optimizations and some [CodeWalker](https://github.com/dexyfex/CodeWalker/) features backported
* Add files to existing dictionaries instead just overwriting the whole dict
* Maybe support more pixel formats? don't know if this is actually used by anyone

## Building

Requires dotnet cli and some kind of C++ compiler.
Generating project files with [premake 5](https://premake.github.io/) should be just work:tm:. If not you can build `FuckDX` manually with your C++ compiler of choice and the rest with `dotnet publish -c Release`, just make sure `FuckDX.dll` is accessible to the `dotnet publish` output.

If you use this for your FiveM server and get infinite donations it'd be nice if you [shared some of it](https://www.patreon.com/kngrektor)