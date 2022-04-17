// this file for Single File Generator and his work

using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.TextTemplating.VSHost;

using FCM.Generators.Input;
using FCM.Generators.Logic;
using FCM.Generators.Output;

namespace FCM.Generators;

class DGMLFileGenerator : BaseCodeGeneratorWithSite
{
    public const string Name = nameof(DGMLFileGenerator);

    public const string Description = "Functhional Code Map Generator";

    public override string GetDefaultExtension() => ".dgml";

    protected override byte[] GenerateCode(string inputFileName, string inputFileContent)
    {

        List<MemberDeclarationSyntax> members = 
            CostumeCode
            .GetMembers()
           ;

        AnalyzeCustomeCode
            .StartingFrom(inputFileContent, members);

        return 
            DGML
            .GetDGMLCodeAsArrayOfBytes(); 
    }

 
}
