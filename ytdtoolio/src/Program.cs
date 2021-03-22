#nullable enable

using System;
using System.IO;
using System.Linq;
using System.CommandLine;
using System.CommandLine.Parsing;
using System.CommandLine.Invocation;

using RageLib.Resources.GTA5;
using RageLib.Resources.GTA5.PC.Textures;
using RageLib.ResourceWrappers.GTA5.PC.Textures;
using RageLib.ResourceWrappers;


namespace ytdtoolio {
	// I **love** C# and people who write C#
	public static class CLIExt {
		public static Command WithArgument(this Command command, Argument argument) {
			command.AddArgument(argument);
			return command;
		}
		public static Command WithOption(this Command command, Option option) {
			command.AddOption(option);
			return command;
		}
		public static Argument WithValidator<T>(this Argument argument, Func<T?, string?> validator) {
			argument.AddValidator(arg => validator(arg.GetValueOrDefault<T>()));
			return argument;
		}
	}

	class YTDTool {
		static string? IsYTD(FileInfo? file) {
			if (file == null) return "But which .YTD sire?";
			if (!file.Exists) return $"I'm sorry my sire, but {file.FullName} isn't a valid file";
			using (var fs = file.OpenRead()) {
				if (fs.ReadByte() != 'R' || fs.ReadByte() != 'S' || fs.ReadByte() != 'C' || fs.ReadByte() != '7') {
					return $"It appears that {file.Name} is not a GTA V (PC) Resource7 file";
				}
			}
			return null;
		}
		static string? IsDir(DirectoryInfo? dir) {
			if (dir == null) return "But which directory sire?";
			if (!dir.Exists) return $"I'm sorry my sire, but {dir.FullName} isn't a valid directory";
			return null;
		}
		static void List(FileInfo file) {
			var ytd = new TextureDictionaryFileWrapper_GTA5_pc();
			ytd.Load(file.FullName);
			Console.WriteLine($"Loaded {file.Name}:");

			var longest = ytd.TextureDictionary.Textures
				.Select(tex => tex.Name.Length)
				.Max();
			
			foreach (var tex in ytd.TextureDictionary.Textures) {
				Console.Write($"  {tex.Name.PadRight(longest)} {tex.Width}x{tex.Height}");
				Console.WriteLine($" in {tex.Format} with {tex.MipMapLevels} levels");

				for (int i=0; i<tex.MipMapLevels; i++) {
					Console.WriteLine($"lvl {i} is {tex.GetTextureData(i+1).Length} bytes");
				}
			}
		}
		static void Unpack(FileInfo file, DirectoryInfo destination) {
			var ytd = new TextureDictionaryFileWrapper_GTA5_pc();
			ytd.Load(file.FullName);
			Console.WriteLine(file.FullName);
			
			var dir = destination?.FullName ??
				Path.Join(file.DirectoryName, Path.GetFileNameWithoutExtension(file.Name));
			new PngTextureDictionary(ytd.TextureDictionary)
				.Save(dir);
		}
		static void Pack(DirectoryInfo directory, FileInfo destination) {
			var dict = PngTextureDictionary.Load(directory.FullName)
				.ToTextureDictionary();

			var file = destination?.FullName ??
				Path.Join(directory.Parent.FullName, $"{directory.Name}.ytd");
			new ResourceFile_GTA5_pc<TextureDictionary>() {
				ResourceData = dict,
				Version = 13
			}.Save(file);
		}
		static void Main(string[] args)  {
			var root = new RootCommand("ytdtool") {
				new Command("list") {
					Handler = CommandHandler.Create<FileInfo>(List),
				}.WithArgument(
					new Argument<FileInfo>(
						"file",
						"Source GTA V (PC) TextureDictionary, probably *.ytd"
					).WithValidator<FileInfo>(IsYTD)
				),
				new Command("unpack") {
					Handler = CommandHandler.Create<FileInfo, DirectoryInfo>(Unpack),
				}.WithArgument(
					new Argument<FileInfo>(
						"file",
						"Source GTA V (PC) TextureDictionary, probably *.ytd"
					).WithValidator<FileInfo>(IsYTD)
				).WithOption(
					new Option<DirectoryInfo>(
						new string[] {"--destination", "-d"},
						"Destination folder, defaults to filename without extension"
					)
				),
				new Command("pack") {
					Handler = CommandHandler.Create<DirectoryInfo, FileInfo>(Pack),
				}.WithArgument(
					new Argument<DirectoryInfo>(
						"directory",
						"A folder with *.png and optionally *.png.json files to pack"
					).WithValidator<DirectoryInfo>(IsDir)
				).WithOption(
					new Option<FileInfo>(
						new string[] {"--destination", "-d"},
						"Destination TextureDictionary, defaults to folder name with .ytd added to it"
					)
				)
			}.Invoke(args);
		}
	}
}
