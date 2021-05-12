// https://github.com/richgel999/bc7enc/blob/master/rgbcx.h
#define RGBCX_IMPLEMENTATION 1
// apparently rgbcx needs this
#include <cstring>
#include <cmath>
#include "rgbcx.h"

#if defined _WIN32 || defined __CYGWIN__
	#define FUCKDX_API extern "C" __declspec(dllexport)
#else
	#define FUCKDX_API extern "C" __attribute__((visibility("default")))
#endif

using Pixel = uint8_t[4];
using Block = uint8_t[8];

enum D3DFMT {
	DXT1 = 0x31545844, // BC1
	DXT3 = 0x33545844, // BC2, UNSUPPORTED D:
	DXT5 = 0x35545844, // BC3
	// https://docs.microsoft.com/en-us/windows/win32/direct3d11/texture-block-compression-in-direct3d-11#bc4-and-bc5-formats
	ATI1 = 0x31495441, // BC4
	ATI2 = 0x32495441, // BC5

	BC7 = 0x20374342 // BC7, Unsupported :(
};

int block_packed_size(D3DFMT fmt) {
	switch (fmt) {
	case DXT1: return 8;
	case DXT3: return -1;
	case DXT5: return 16;
	case ATI1: return 8;
	case ATI2: return 16;
	case BC7:  return 16;
	default:   return -1;
	}
}

bool encode_block(const Pixel quad[16], D3DFMT fmt, uint32_t level, Block block) {
	switch (fmt) {
	case DXT1:
		rgbcx::encode_bc1(level, block, (const uint8_t*)quad, true, true);
	return true;
	case DXT5:
		rgbcx::encode_bc3(level, block, (const uint8_t*)quad);
	return true;
	case ATI1:
		rgbcx::encode_bc4(block, (const uint8_t*)quad);
	return true;
	case ATI2:
		rgbcx::encode_bc5(block, (const uint8_t*)quad);
	return true;
	default:
		return false;
	}
}

FUCKDX_API bool encode(uint8_t src[], D3DFMT fmt, uint32_t level, int w, int h, uint8_t dst[]) {
	if (!rgbcx::g_initialized) { rgbcx::init(rgbcx::bc1_approx_mode::cBC1Ideal); }

	int bs = block_packed_size(fmt);
	int bw = w/4, bh = h/4;

	for (int by = 0; by < bh; by++)
	for (int bx = 0; bx < bw; bx++) {
		Pixel quad[16];
		for (int y = 0; y < 4; y++) {
			memcpy(&quad[4*y], &src[4*by*w + y*w + 4*bx], 4*sizeof(Pixel));
		}

		if (!encode_block(quad, fmt, level, &dst[bs*(bw*by + bx)])) {
			return false;
		}
	}

	return true;
}

bool decode_block(Block block, D3DFMT fmt, Pixel quad[16]) {
	switch (fmt) {
	case DXT1:
		rgbcx::unpack_bc1(block, quad, true, rgbcx::bc1_approx_mode::cBC1Ideal);
	return true;
	case DXT5:
		rgbcx::unpack_bc3(block, quad, rgbcx::bc1_approx_mode::cBC1Ideal);
	return true;
	case ATI1:
		rgbcx::unpack_bc4(block, &quad[0][0]);
	return true;
	case ATI2:
		rgbcx::unpack_bc5(block, &quad[0][0]);
	return true;
	default:
		return false;
	}
}

FUCKDX_API bool decode(uint8_t src[], D3DFMT fmt, int w, int h, Pixel dst[]) {
	if (!rgbcx::g_initialized) { rgbcx::init(rgbcx::bc1_approx_mode::cBC1Ideal); }

	int bs = block_packed_size(fmt);
	int bw = w/4, bh = h/4;

	for (int by = 0; by < bh; by++)
	for (int bx = 0; bx < bw; bx++) {
		Pixel quad[16];

		if (!decode_block(&src[bs*(bw*by + bx)], fmt, quad)) {
			return false;
		}

		for (int y = 0; y < 4; y++) {
			memcpy(&dst[4*by*w + y*w + 4*bx], &quad[4*y], 4*sizeof(Pixel));
		}
	}

	return true;
}