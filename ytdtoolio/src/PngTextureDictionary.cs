#nullable enable

using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

using RageLib.Resources.Common;
using RageLib.Resources.GTA5.PC.Textures;
using RageLib.ResourceWrappers;
using RageLib.GTA5.ResourceWrappers.PC.Textures;

namespace ytdtoolio {
	class PngTextureDictionary {
		Dictionary<string, PngTexture> pngs;

		private PngTextureDictionary(Dictionary<string, PngTexture> pngs) {
			this.pngs = pngs;
		}
		public PngTextureDictionary(ITextureDictionary dict) {
			pngs = dict.Textures
				.ToDictionary(
					tex => tex.Name,
					tex => new PngTexture(tex)
				);
		}

		public void Save(string dir) {
			Console.WriteLine($"Saving dictionary to {dir}");
			Directory.CreateDirectory(dir);
			foreach (var (name, png) in pngs) {
				png.Save(Path.Join(dir, $"{name}.png"));
			}
		}

		public static PngTextureDictionary Load(string dir) {
			Console.WriteLine($"Loading dictionary from {dir}");
			return new PngTextureDictionary(
				Directory.EnumerateFiles(dir, "*.png")
					.ToDictionary(
						path => {
							var filename = Path.GetRelativePath(dir, path);
							return filename.Remove(filename.Length - 4);
						},
						path => PngTexture.Load(path)
					)
			);
		}

		public PgDictionary64<TextureDX11> ToTextureDictionary() {
			// This too is a mess
			var dict = new PgDictionary64<TextureDX11>();
			dict.Values.Entries = new ResourcePointerArray64<TextureDX11>();
			var w = new TextureDictionaryWrapper_GTA5_pc(dict);

			foreach (var (name, png) in pngs) {
				w.Textures.Add(png.ToTexture(name));
			}

			w.UpdateClass();
			return dict;
		}
	}
}