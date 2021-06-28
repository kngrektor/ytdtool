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

* Add files to existing dictionaries instead just overwriting the whole dict
* Maybe support more pixel formats? don't know if this is actually used by anyone

## Development

1. Run the `dev_*` script appropiate for your system.
2. Open the `YTDToolio.sln` file using `Visual Studio 2019`

## Building

1. Run the `dev_*` script appropiate for your system (if not already done).
2. Run the `build_*` script appropiate for your system.

If you use this for your FiveM server and get infinite donations it'd be nice if you [shared some of it](https://www.patreon.com/kngrektor)
