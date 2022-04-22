using System.Collections.Generic;
using System.Linq;

using LanguageExt;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using FCM.Generators.Output;
using static FCM.Generators.Input.CostumeCode;
using System;

namespace FCM.Generators.Logic;

internal static class Painter
{
    private static string InputFilePath;

    public static void TryDrow(string inputFilePath, IEnumerable<SyntaxNode> startSyntaxNodes)
    {
        InputFilePath = inputFilePath;

        var IsThrereGlobalStatementSyntax = startSyntaxNodes.OfType<GlobalStatementSyntax>().Any();
        if (IsThrereGlobalStatementSyntax)
        {
            startSyntaxNodes.DrowStartingFromInputFileGlobalInvocation();
            return;
        }


        var IsThrereMethodDeclarationSyntax = startSyntaxNodes.OfType<MethodDeclarationSyntax>().Any();
        if (IsThrereMethodDeclarationSyntax)
        {
            startSyntaxNodes.DrowStartingFromInputFileTopLevelFunction();
            return;
        }

    }

    private static void DrowStartingFromInputFileGlobalInvocation(this IEnumerable<SyntaxNode> startSyntaxNodes)
    {
        var GlobalInvocations = startSyntaxNodes
                        .OfType<InvocationExpressionSyntax>();

        Enumerable.Range(0, GlobalInvocations.Count())
            .Freeze().Do(i => AnalyzeInputFileGlobalInvocation(GlobalInvocations.ElementAt(i)));
    }

    private static void AnalyzeInputFileGlobalInvocation(InvocationExpressionSyntax invocationExpressionSyntax)
    {
        var model = GetSemanticModelFor(InputFilePath);

        var EquivalentInvocation = model
            .SyntaxTree
            .GetRoot()
            .DescendantNodes()
            .OfType<InvocationExpressionSyntax>()
            .FirstOrDefault(invoc => invoc.FullSpan == invocationExpressionSyntax.FullSpan);

        var IMethodSymbolObj = model.GetSymbolInfo(EquivalentInvocation.Expression).Symbol as IMethodSymbol;

        var orginalMethod = IMethodSymbolObj.DeclaringSyntaxReferences.FirstOrDefault().GetSyntax() as MethodDeclarationSyntax;

        var orginalMethodAsString = orginalMethod.ToString();


    }

    public static void DrowStartingFromInputFileTopLevelFunction(this IEnumerable<SyntaxNode> startSyntaxNodes)
    {
        throw new NotImplementedException();
    }
}
