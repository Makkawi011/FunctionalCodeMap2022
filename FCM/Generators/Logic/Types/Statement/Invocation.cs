using Microsoft.CodeAnalysis;

namespace FCM.Generators.Logic.Types.Statement;

internal class Invocation
{
    private SyntaxNode statement;

    public SyntaxNode Statement
    {
        get { return statement; }
        set { statement = value; }
    }
    public Invocation(SyntaxNode statement)
    {
        Statement = statement;
    }

    public static AnalyzeMethodResult AnalyzeInvocationStatement(Invocation InvocationStatement)
    {
        throw new NotImplementedException();
    }
}

