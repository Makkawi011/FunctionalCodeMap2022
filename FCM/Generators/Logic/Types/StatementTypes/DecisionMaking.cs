using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FCM.Generators.Logic.Types.StatementTypes;

internal class DecisionMaking
{
    private SyntaxNode statement;

    public SyntaxNode Statement
    {
        get { return statement; }
        set { statement = value; }
    }

    public DecisionMaking(IfStatementSyntax ifStatement)
    {
        statement = ifStatement;
    }
    public DecisionMaking(ElseClauseSyntax elseClause)
    {
        statement = elseClause;
    }
    public DecisionMaking(SwitchStatementSyntax switchStatement)
    {
        statement = switchStatement;
    }
}
