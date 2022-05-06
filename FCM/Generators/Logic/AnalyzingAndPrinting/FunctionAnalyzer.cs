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
        var c =5;
        
        if (method.HasBeenAnalyzedBefor())
        {
            method.AddKnownResultAnalyzesToFinalResults();
            return;

            c = 10;
        }
        int z = c;

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
