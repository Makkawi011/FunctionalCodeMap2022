using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;

<<<<<<< HEAD
using FCM.Generators.Logic.AnalyzingAndPrinting;
using LanguageExt;
=======
using FCM.Generators.Logic.AnalyzingAndPrinting.NonGlobalStatementsAnalyzers;
using FCM.Generators.Logic.AnalyzingAndPrinting.GlobalStatementsAnalyzers;
>>>>>>> f6b4594e0d8e2a7dd027380df2258050966cddb2

namespace FCM.Generators.Logic;

internal static class Painter
{
<<<<<<< HEAD
    public static void AnalyzeAndDrowStartingFrom(this Either<MethodDeclarationSyntax, LocalFunctionStatementSyntax> topLevelFunction)
        => FunctionAnalyzer.AnalyzeAndDrowStartingFrom(topLevelFunction);

=======
    public static void AnalyzeAndDrowStartingFrom(MethodDeclarationSyntax topLevelFunction) 
        => MethodDeclerationAnalyzer.AnalyzeAndDrowStartingFrom(topLevelFunction);

    public static void AnalyzeAndDrowStartingFrom(LocalFunctionStatementSyntax toplevelLocalFunction) 
        => LocalFunctionAnalyzer.AnalyzeAndDrowStartingFrom(toplevelLocalFunction);

>>>>>>> f6b4594e0d8e2a7dd027380df2258050966cddb2
    public static void AnalyzeAndDrowStartingFrom(IEnumerable<InvocationExpressionSyntax> invocations) 
        => GlobalInvocationsAnalyzer.AnalyzeAndDrowStartingFrom(invocations);
}
