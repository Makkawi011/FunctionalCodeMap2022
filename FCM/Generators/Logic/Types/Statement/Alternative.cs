using static FCM.Generators.Logic.Helper;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using FCM.Generators.Output.Types;
using System.Linq;
using FCM.Generators.Logic.AnalyzingAndPrinting;

namespace FCM.Generators.Logic.Types.Statement;

internal class Alternative
{
    const string AlternativeStart = "Alternative Start";
    const string AlternativeEnd = "Alternative End";

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

    public static AnalyzeMethodResult AnalyzeAlternativeStatement(Alternative AlternativeStatement, int totalFunctionId)
        => AnalyzeTryStatementSyntax((TryStatementSyntax)AlternativeStatement.Statement, totalFunctionId);

    private static AnalyzeMethodResult AnalyzeTryStatementSyntax(TryStatementSyntax tryStatement, int totalFunctionId)
    {
        AnalyzeMethodResult result = new();


        int startAltId = MakeNewId();

        AddFunctionToFinalResultAndTo(result, new(startAltId, AlternativeStart , CategoryId.Alt));
        AddFlowToFinalResultAndTo(result, new(totalFunctionId, startAltId, Category.Contains));

        //_____________________________________________________
        #region Try 
        var statementsIntryBlock =
            StatementClassifier
            .Classify(tryStatement.Block.Statements);

        var tryStatementsResults = StatementAnalyzer.Analyze(statementsIntryBlock, totalFunctionId);

        AddToFinalResultAndTo(result, tryStatementsResults);

        var endAltId = MakeNewId();

        AddFlowToFinalResultAndTo(result, new(GetLastIdUsed(), endAltId));
        #endregion
        //_____________________________________________________
        #region Catches
        tryStatement
            .Catches
            .Select(Catch => new { statements = StatementClassifier.Classify(Catch.Block.Statements) })
            .ToList().ForEach(Catch =>
            {
                var chatchStatementsResults = StatementAnalyzer.Analyze(Catch.statements, totalFunctionId);

                AddToFinalResultAndTo(result, chatchStatementsResults);
              
                AddFlowToFinalResultAndTo(result, new(GetLastIdUsed(), endAltId));
            });
        #endregion
        //_____________________________________________________

        AddFunctionToFinalResultAndTo(result, new(endAltId, AlternativeEnd , CategoryId.Alt));
        AddFlowToFinalResultAndTo(result, new(totalFunctionId, endAltId, Category.Contains));

        return result;
    }
}
