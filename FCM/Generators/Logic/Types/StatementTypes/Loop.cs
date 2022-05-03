
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FCM.Generators.Logic.Types.StatementTypes;

internal class Loop
{
    private SyntaxNode statement;

    public SyntaxNode Statement
    {
        get { return statement; }
        set { statement = value; }
    }
    public Loop(ForEachStatementSyntax ForEachstatement)
    {
        Statement = ForEachstatement;
    }
    public Loop(ForStatementSyntax ForStatement)
    {
        Statement = ForStatement;
    }

    public Loop(WhileStatementSyntax whileStatement) 
    {
        Statement = whileStatement; 
    }

    public Loop(DoStatementSyntax doStatement) 
    {
        Statement = doStatement; 
    }
}
