// this file for Single File Generator and his work

using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.TextTemplating.VSHost;

using FCM.Generators.Input;
using FCM.Generators.Logic;
using FCM.Generators.Output;
using System.IO;

namespace FCM.Generators;

class DGMLFileGenerator : BaseCodeGeneratorWithSite
{
    public const string Name = nameof(DGMLFileGenerator);

    public const string Description = "Functhional Code Map Generator";

    public override string GetDefaultExtension() => ".dgml";

    protected override byte[] GenerateCode(string inputFileName, string inputFileContent)
    {

        CostumeCode
            .PrepareSyntaxTreesAndSymanticModels();

        CostumeCodeAnalyzer
            .StartingFrom(InputFilePath() , inputFileContent);

        return 
            DGML
            .GetDGMLCodeAsArrayOfBytes(); 
    }

    private new string InputFilePath()
    {
        ThreadHelper.ThrowIfNotOnUIThread();
        var item = GetService(typeof(EnvDTE.ProjectItem)) as EnvDTE.ProjectItem;
        return item?.FileNames[1];
    }
 
}

