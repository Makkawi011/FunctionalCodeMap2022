using static FCM.Generators.Logic.Helper;
using FCM.Generators.Logic.AnalyzingAndPrinting;
using FCM.Generators.Output.Types;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

namespace FCM.Generators.Logic.Types.Statement;

internal class Optional
{
    const string OptionalStart = "Optional start";
    const string OptionalEnd = "Optional End";

    private SyntaxNode statement;

    public SyntaxNode Statement
    {
        get { return statement; }
        set { statement = value; }
    }
    public Optional(SwitchStatementSyntax switchStatement)
    {
        statement = switchStatement;
    }
    public Optional(IfStatementSyntax ifStatement)
    {
        statement = ifStatement;
    }

    public static AnalyzeMethodResult AnalyzeOptionalStatement(Optional Optional, int totalFunctionId)
    {
        return Optional.Statement.CanBeConsidered<IfStatementSyntax>()
            ? AnalyzeIfElseStatementSyntax((IfStatementSyntax)Optional.Statement, totalFunctionId)
            : AnalyzeSwitchStatementSyntax((SwitchStatementSyntax)Optional.Statement, totalFunctionId);

    }
    private static AnalyzeMethodResult AnalyzeIfElseStatementSyntax(IfStatementSyntax IfElseStatementSyntax, int totalFunctionId)
    {
        AnalyzeMethodResult result = new();


        int startOptId = MakeNewId();

        AddFunctionToFinalResultAndTo(result, new(startOptId, OptionalStart , CategoryId.Cho));
        AddFlowToFinalResultAndTo(result, new(totalFunctionId, startOptId, Category.Contains));

        //_____________________________________________________
        #region If 
        var statementsInIfBlock = StatementClassifier
            .Classify(IfElseStatementSyntax.Statement.DescendantNodes());

        var IfStatementsResults = StatementAnalyzer.Analyze(statementsInIfBlock, totalFunctionId);

        AddToFinalResultAndTo(result, IfStatementsResults);

        var endOptId = MakeNewId();

        AddFlowToFinalResultAndTo(result, new(GetLastIdUsed(), endOptId));
        #endregion
        //_____________________________________________________
        #region Else

        var statementsInElseBlock = StatementClassifier
            .Classify(IfElseStatementSyntax.Else.Statement.DescendantNodes());

        var ElseStatementsResults = StatementAnalyzer.Analyze(statementsInElseBlock, totalFunctionId);

        AddToFinalResultAndTo(result, ElseStatementsResults);

        AddFlowToFinalResultAndTo(result, new(GetLastIdUsed(), endOptId));

        #endregion
        //_____________________________________________________

        AddFunctionToFinalResultAndTo(result, new(endOptId, OptionalEnd , CategoryId.Cho));
        AddFlowToFinalResultAndTo(result, new(totalFunctionId, endOptId, Category.Contains));

        return result;
    }
   
    private static AnalyzeMethodResult AnalyzeSwitchStatementSyntax(SwitchStatementSyntax SwitchSyntax , int totalFunctionId)
    {

        AnalyzeMethodResult result = new();


        int startOptId = MakeNewId();

        AddFunctionToFinalResultAndTo(result, new(startOptId, OptionalStart, CategoryId.Cho));
        AddFlowToFinalResultAndTo(result, new(totalFunctionId, startOptId, Category.Contains));

        var endOptId = MakeNewId();

        AddFlowToFinalResultAndTo(result, new(GetLastIdUsed(), endOptId));

        //_____________________________________________________
        #region Cases

        SwitchSyntax
           .Sections
           .Select(section => new { statements = StatementClassifier.Classify(section.Statements) })
           .ToList().ForEach(section =>
           {
               var chatchStatementsResults = StatementAnalyzer.Analyze(section.statements, totalFunctionId);

               AddToFinalResultAndTo(result, chatchStatementsResults);

               AddFlowToFinalResultAndTo(result, new(GetLastIdUsed(), endOptId));
           });

        #endregion
        //_____________________________________________________

        AddFunctionToFinalResultAndTo(result, new(endOptId, OptionalEnd , CategoryId.Cho));
        AddFlowToFinalResultAndTo(result, new(totalFunctionId, endOptId, Category.Contains));

        return result;
    }
}