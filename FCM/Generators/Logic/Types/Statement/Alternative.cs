using static FCM.Generators.Logic.IdMaker;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using FCM.Generators.Output.Types;
using System.Linq;

namespace FCM.Generators.Logic.Types.Statement;

internal class Alternative
{
    private SyntaxNode statement;

    public SyntaxNode Statement
    {
        get { return statement; }
        set { statement = value; }
    }

    public Alternative(TryStatementSyntax tryStatement)
    {
        statement = tryStatement;
    }

    public static AnalyzeMethodResult AnalyzeAlternativeStatement(Alternative AlternativeStatement , int totalFunctionId)
    {
        AnalyzeMethodResult result = AnalyzeTryStatementSyntax((TryStatementSyntax)AlternativeStatement.Statement , totalFunctionId);

        return result;
    }

    private static AnalyzeMethodResult AnalyzeTryStatementSyntax(TryStatementSyntax tryStatement, int totalFunctionId)
    {
        AnalyzeMethodResult result = new();

        int startAltId = GetId();
        result.Functions.Add(new(startAltId, "Alternative start", CategoryId.Alt));
        result.Flows.Add(new(totalFunctionId, startAltId, Category.Contains));

        var statementsIntryBlock =
            StatementClassifier
            .Classify(tryStatement.Block.Statements);

        var statementsIncatchsBlock =
            tryStatement
            .Catches
            .Select(Catch => StatementClassifier.Classify(Catch.Block.Statements));



        return result;
    }
}
