using System.Collections.Generic;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using LanguageExt;

namespace FCM.Generators.Logic.AnalyzingAndPrinting;
internal class FunctionAnalyzer
{
    public static void AnalyzeAndDrowStartingFrom
        (Either<MethodDeclarationSyntax, LocalFunctionStatementSyntax> topLevelFunction) 
        => Analyze(topLevelFunction);

    private static void Analyze(Either<MethodDeclarationSyntax, LocalFunctionStatementSyntax> method)
    {

        if (method.HasBeenAnalyzedBefor())
        {
            method.AddKnownResultAnalyzesToFinalResults();
            return;
        }

        var statements = GetStatementsAsSyntaxNodes(method);
        var output = StatementAnalyzer.Analyze(statements);

        output.MemoAndAddToFinalResultsFor(method);
    }

    private static IEnumerable<SyntaxNode> GetStatementsAsSyntaxNodes
        (Either<MethodDeclarationSyntax, LocalFunctionStatementSyntax> input)
    {
        IEnumerable <SyntaxNode> statements;
        try
        {
            statements = input.IsLeft ?
                ((MethodDeclarationSyntax)input).Body.Statements
                : ((LocalFunctionStatementSyntax)input).Body.Statements;

        }
        catch
        {
            statements = input.IsLeft ?
                ((MethodDeclarationSyntax)input).ExpressionBody.DescendantNodes()
                :((LocalFunctionStatementSyntax)input).ExpressionBody.DescendantNodes();
        }

        return statements;
    }

}
