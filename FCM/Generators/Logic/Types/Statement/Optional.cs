using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FCM.Generators.Logic.Types.Statement;

internal class Optional
{
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

    public static AnalyzeMethodResult Analyze(Optional OptionalStatement)
    {
        throw new NotImplementedException();
    }
}
