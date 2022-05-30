using FCM.Generators.Output.Types;
using static FCM.Generators.Logic.Helper;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using FCM.Generators.Logic.AnalyzingAndPrinting;
using System.Collections.Generic;

namespace FCM.Generators.Logic.Types.Statement;

internal class Iteration
{
    const string IterationStart = "Iteration Start";
    const string IterationEnd = "Iteration End";

    private SyntaxNode statement;

    public SyntaxNode Statement
    {
        get { return statement; }
        set { statement = value; }
    }
    public Iteration(ForStatementSyntax ForStatement)
    {
        Statement = ForStatement;
    }
    public Iteration(ForEachStatementSyntax ForEachstatement)
    {
        Statement = ForEachstatement;
    }

    public Iteration(WhileStatementSyntax whileStatement) 
    {
        Statement = whileStatement; 
    }
    
    public Iteration(DoStatementSyntax doStatement) 
    {
        Statement = doStatement; 
    }

    public static AnalyzeMethodResult AnalyzeIterationStatement(Iteration Iteration, int totalFunctionId)
    {
        if (Iteration.statement.CanBeConsidered<ForStatementSyntax>())
        {
            return AnalyzeForStatementSyntax((ForStatementSyntax)Iteration.Statement, totalFunctionId);
        }
        else if (Iteration.statement.CanBeConsidered<ForEachStatementSyntax>())
        {
            return AnalyzeForEachStatementSyntax((ForEachStatementSyntax)Iteration.Statement, totalFunctionId);
        }
        else if (Iteration.statement.CanBeConsidered<WhileStatementSyntax>())
        {
            return AnalyzeWhileStatementSyntax((WhileStatementSyntax)Iteration.Statement, totalFunctionId);
        }
        else //if(Iteration.statement.CanBeConsidered<DoStatementSyntax>())
        {
            return AnalyzeDoStatementSyntax((DoStatementSyntax)Iteration.Statement, totalFunctionId);
        }
    }

    private static AnalyzeMethodResult AnalyzeForStatementSyntax(ForStatementSyntax ForStatementSyntax, int totalFunctionId)
    => AnalyzeLOOPStatementSyntax(ForStatementSyntax.Statement.DescendantNodes(), totalFunctionId);

    private static AnalyzeMethodResult AnalyzeForEachStatementSyntax(ForEachStatementSyntax ForEachStatementSyntax, int totalFunctionId)
    => AnalyzeLOOPStatementSyntax(ForEachStatementSyntax.Statement.DescendantNodes() , totalFunctionId);

    private static AnalyzeMethodResult AnalyzeWhileStatementSyntax(WhileStatementSyntax WhileStatementSyntax, int totalFunctionId)
    => AnalyzeLOOPStatementSyntax(WhileStatementSyntax.Statement.DescendantNodes(), totalFunctionId);

    private static AnalyzeMethodResult AnalyzeDoStatementSyntax(DoStatementSyntax DoStatementSyntax, int totalFunctionId)
    => AnalyzeLOOPStatementSyntax(DoStatementSyntax.Statement.DescendantNodes(), totalFunctionId);

    private static AnalyzeMethodResult AnalyzeLOOPStatementSyntax(IEnumerable<SyntaxNode> StatementsInLoop, int totalFunctionId)
    {
        AnalyzeMethodResult result = new();

        int startIterId = MakeNewId();

        AddFunctionToFinalResultAndTo(result, new(startIterId, IterationStart, CategoryId.Ite));
        AddFlowToFinalResultAndTo(result, new(totalFunctionId, startIterId, Category.Contains));

        //_____________________________________
        var statementsInForBlock = StatementClassifier
            .Classify(StatementsInLoop);

        var ForStatementsResults = StatementAnalyzer.Analyze(statementsInForBlock, totalFunctionId);

        AddToFinalResultAndTo(result, ForStatementsResults);

        var endIterId = MakeNewId();

        AddFlowToFinalResultAndTo(result, new(GetLastIdUsed(), endIterId));
        //___________________________________
        AddFunctionToFinalResultAndTo(result, new(endIterId, IterationEnd, CategoryId.Ite));
        AddFlowToFinalResultAndTo(result, new(totalFunctionId, endIterId, Category.Contains));

        return result;
    }


}
