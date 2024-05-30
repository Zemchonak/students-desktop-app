<Query Kind="Program" />

void Main()
{
		string outputPath = Path.Combine("D:\\Git\\students-desktop-app\\src\\output.txt");
		string[] extensions = new[] { ".cs", ".csproj", ".xaml" };
		string startFolder = "D:\\Git\\students-desktop-app\\src";
		var files = Directory.GetFiles(startFolder, "*.*", SearchOption.AllDirectories).Where(file => extensions.Any(file.ToLower().EndsWith))
			.Where(file => !ExcludedFolders.Any(file.ToLower().Contains));
		using (StreamWriter outputFile = new StreamWriter(outputPath))
		{
			foreach (var file in files)
			{
				string content = File.ReadAllText(file); outputFile.WriteLine(file);
				outputFile.WriteLine(content); outputFile.WriteLine();
			}
		}
}
static readonly string[] ExcludedFolders = { "bin", "obj", "Properties" };

// You can define other methods, fields, classes and namespaces here