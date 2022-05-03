using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using FCM.Generators.Logic.AnalyzingAndPrinting;
using LanguageExt;

namespace FCM.Generators.Logic;

internal static class Painter
{
    public static void AnalyzeAndDrowStartingFrom(this Either<MethodDeclarationSyntax, LocalFunctionStatementSyntax> topLevelFunction)
        => FunctionAnalyzer.AnalyzeAndDrowStartingFrom(topLevelFunction);

    public static void AnalyzeAndDrowStartingFrom(IEnumerable<InvocationExpressionSyntax> invocations) 
        => GlobalInvocationsAnalyzer.AnalyzeAndDrowStartingFrom(invocations);
}
