using System.Collections.Generic;
using System.Linq;

using FCM.Generators.Logic.Types;

using LanguageExt;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FCM.Generators.Logic.AnalyzingAndPrinting;

internal class StatementAnalyzer
{
    public static AnalyzeMethodResult Analyze(IEnumerable<SyntaxNode> statements)
    {
        AnalyzeMethodResult result = new();

        var ClassificationStatements = StatementClassifier.Classify(statements);

        return result;
    }
    public static bool AnyInvocationContainedWithinStatement(SyntaxNode statement)
    => statement.DescendantNodes().OfType<InvocationExpressionSyntax>().Any();
    public static IEnumerable<SyntaxNode> GetStatementsAsSyntaxNodesFrom
    (Either<MethodDeclarationSyntax, LocalFunctionStatementSyntax> input)
    {
        IEnumerable<SyntaxNode> statements;
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
                : ((LocalFunctionStatementSyntax)input).ExpressionBody.DescendantNodes();
        }

        return statements;
    }
    public static AnalyzeMethodResult AnalyzeStatement(StatementClassifier statement)
    {
        throw new NotImplementedException();
    }


}
