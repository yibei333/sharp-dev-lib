using SharpDevLib;
using SharpLibMarkdownDocGenerator;

var sourcePath = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory.CombinePath("../../../../")).FullName;
var exceptDirectries = new List<string> { "GenerateDoc", "SharpDevLib.Tests", "TestResults", ".vs" };
var outputDirectory = sourcePath.CombinePath(@"../doc/generate");
var addtionalMarkdownDirecotry = sourcePath.CombinePath(@"../doc/additional");
var requests = new List<GenerateRequest>();
new DirectoryInfo(sourcePath).GetDirectories().ToList().ForEach(x =>
{
    if (exceptDirectries.Contains(x.Name)) return;

    var dllPath = sourcePath.CombinePath($"{x.Name}/bin/Debug/netstandard2.0/{x.Name}.dll");
    var request = new GenerateRequest(dllPath, outputDirectory)
    {
        AddtionalMarkdownDirecotry = addtionalMarkdownDirecotry.CombinePath(x.Name),
        ExternalUrlResolver = (type) =>
        {
            if (type.Name.Equals("User")) return "https://www.google.com";
            return GenerateRequest.ResolveMicrosoftDoc(type);
        }
    };
    requests.Add(request);
});
Generator.Generate(requests);