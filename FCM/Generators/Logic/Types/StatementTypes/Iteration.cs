
using System.Collections.Generic;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FCM.Generators.Logic.Types.StatementTypes;

internal class Iteration
{
    private SyntaxNode statement;

    public SyntaxNode Statement
    {
        get { return statement; }
        set { statement = value; }
    }
    public Iteration(ForEachStatementSyntax ForEachstatement)
    {
        Statement = ForEachstatement;
    }
    public Iteration(ForStatementSyntax ForStatement)
    {
        Statement = ForStatement;
    }

    public Iteration(WhileStatementSyntax whileStatement) 
    {
        Statement = whileStatement; 
    }
    
    public Iteration(DoStatementSyntax doStatement) 
    {
        Statement = doStatement; 
    }

    public static AnalyzeMethodResult AnalyzeIterationStatement(Iteration IterationStatement)
    {
        throw new NotImplementedException();
    }
}
