
using System.Collections.Generic;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using Microsoft.CodeAnalysis;
using FCM.Generators.Logic.Types.Statement;
using FCM.Generators.Logic.AnalyzingAndPrinting;
using System.Linq;

namespace FCM.Generators.Logic;

internal class StatementClassifier
{
    private dynamic statement;

    public dynamic Statement
    {
        get { return statement; }
        set { statement = value; }
    }

    public StatementClassifier(Alternative statement)
    {
        this.statement = statement;
    }
    public StatementClassifier(Invocation statement)
    {
        this.statement = statement;
    }
    public StatementClassifier(Iteration statement)
    {
        this.statement = statement;
    }
    public StatementClassifier(Optional statement)
    { 
        this.statement = statement;
    }
    public StatementClassifier(Parallel statement)
    {
        this.statement = statement;
    }
    public static IEnumerable<StatementClassifier> Classify(IEnumerable<SyntaxNode> statements)
    {
        statements = statements.Where(StatementAnalyzer.AnyInvocationContainedWithinStatement);

        var classifiers = new List<StatementClassifier>();

        foreach (var statement in statements)
        {
            //Alternative
            if (CanCast<TryStatementSyntax>(statement))
            {
                var alternativeStatement = new Alternative((TryStatementSyntax)statement);
                classifiers.Add(new(alternativeStatement));
            }
            //Optional
            else if (CanCast<IfStatementSyntax>(statement))
            {
                Optional OptionalStatement = new((IfStatementSyntax)statement);
                classifiers.Add(new(OptionalStatement));

            }
            else if (CanCast<SwitchExpressionSyntax>(statement))
            {
                Optional OptionalStatement = new((SwitchStatementSyntax)statement);
                classifiers.Add(new(OptionalStatement));
            }
            //Iteration
            else if (CanCast<ForEachStatementSyntax>(statement))
            {
                Iteration IterationStatement = new((ForEachStatementSyntax)statement);
                classifiers.Add(new(IterationStatement));
            }
            else if (CanCast<ForStatementSyntax>(statement))
            {
                Iteration IterationStatement = new((ForStatementSyntax)statement);
                classifiers.Add(new(IterationStatement));
            }
            else if (CanCast<WhileStatementSyntax>(statement))
            {
                Iteration IterationStatement = new((WhileStatementSyntax)statement);
                classifiers.Add(new(IterationStatement));
            }
            else if (CanCast<DoStatementSyntax>(statement))
            {
                Iteration IterationStatement = new((DoStatementSyntax)statement);
                classifiers.Add(new(IterationStatement));
            }

        }

        return classifiers;
    }

    static bool CanCast<T>(object statement)
    {
        try
        {
            var s = (T)statement;
            return true;
        }
        catch 
        {
            return false;
        }
    }

}
