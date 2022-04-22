using FCM.Generators.Output;

using LanguageExt;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System.Collections.Generic;
using System.Linq;


namespace FCM.Generators.Logic;

internal static class AnalyzeCostumeCode
{
    public static void StartingFrom(string inputFilePath, string inputFileContent)
    {
        if (!IsNullAndEmptyFile(inputFileContent))
        {
            IEnumerable<SyntaxNode> startSyntaxNodes = SyntaxFactory
                    .ParseSyntaxTree(inputFileContent)
                    .GetRoot()
                    .DescendantNodes();

            if (AnyGlobalInvocationsOrTobLevelFunction(startSyntaxNodes))
            {
                Painter.TryDrow(inputFilePath, startSyntaxNodes);
            }
        }

    }
    private static bool IsNullAndEmptyFile(string inputFileContent)
    {
        if (! string.IsNullOrEmpty(inputFileContent))
        {
            //input file not empty
            return false;
        }
        else
        {
            DGML
                .Functions
                .Add(new("-1", "there is no code", "Error"));
            return true;
        }
    }
    private static bool AnyGlobalInvocationsOrTobLevelFunction(IEnumerable<SyntaxNode> startSyntaxNodes)
    {
        var IsThrereGlobalStatementSyntax = startSyntaxNodes.OfType<GlobalStatementSyntax>().Any();
        var IsThereInvocationExpressionSyntax = startSyntaxNodes.OfType<InvocationExpressionSyntax>().Any();
        var IsThereGlobalInvocationExpressionSyntax = IsThrereGlobalStatementSyntax && IsThereInvocationExpressionSyntax;

        var IsThrereMethodDeclarationSyntax = startSyntaxNodes.OfType<MethodDeclarationSyntax>().Any();

        if (IsThrereMethodDeclarationSyntax || IsThereGlobalInvocationExpressionSyntax) return true;
        else
        {
            DGML
                .Functions
                .Add(new("-1", "There is no global invocation and no any function definition ", "Error"));

            return false;
        }

    }
}

