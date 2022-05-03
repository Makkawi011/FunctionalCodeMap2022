using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using FCM.Generators.Logic.AnalyzingAndPrinting.NonGlobalStatementsAnalyzers;
using FCM.Generators.Logic.AnalyzingAndPrinting.GlobalStatementsAnalyzers;

namespace FCM.Generators.Logic;

internal static class Painter
{
    public static void AnalyzeAndDrowStartingFrom(MethodDeclarationSyntax topLevelFunction) 
        => MethodDeclerationAnalyzer.AnalyzeAndDrowStartingFrom(topLevelFunction);

    public static void AnalyzeAndDrowStartingFrom(LocalFunctionStatementSyntax toplevelLocalFunction) 
        => LocalFunctionAnalyzer.AnalyzeAndDrowStartingFrom(toplevelLocalFunction);

    public static void AnalyzeAndDrowStartingFrom(IEnumerable<InvocationExpressionSyntax> invocations) 
        => GlobalInvocationsAnalyzer.AnalyzeAndDrowStartingFrom(invocations);
}
