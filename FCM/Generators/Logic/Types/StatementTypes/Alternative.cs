
using System.Collections.Generic;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FCM.Generators.Logic.Types.StatementTypes;

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

    public static AnalyzeMethodResult AnalyzeAlternativeStatement(Alternative AlternativeStatement)
    {
        AnalyzeMethodResult result = AnalyzeTryStatementSyntax((TryStatementSyntax)AlternativeStatement.Statement);

        return result;
    }

    private static AnalyzeMethodResult AnalyzeTryStatementSyntax(TryStatementSyntax statement)
    {
        AnalyzeMethodResult result = new();


        return result;
    }
}
