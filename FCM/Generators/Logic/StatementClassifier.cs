
using System.Collections.Generic;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using Microsoft.CodeAnalysis;
using FCM.Generators.Logic.Types.StatementTypes;

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
        var classifiers = new List<StatementClassifier>();

        foreach (var statement in statements)
        {
            //Alternative
            if (CanCast(statement, typeof(TryStatementSyntax)))
            {
                var alternativeStatement = new Alternative((TryStatementSyntax)statement);
                classifiers.Add(new(alternativeStatement));
            }
            //Optional
            else if (CanCast(statement, typeof(IfStatementSyntax)))
            {
                Optional OptionalStatement = new((IfStatementSyntax)statement);
                classifiers.Add(new(OptionalStatement));

            }
            else if (CanCast(statement, typeof(SwitchExpressionSyntax)))
            {
                Optional OptionalStatement = new((SwitchStatementSyntax)statement);
                classifiers.Add(new(OptionalStatement));
            }
            //Iteration
            else if (CanCast(statement, typeof(ForEachStatementSyntax)))
            {
                Iteration IterationStatement = new((ForEachStatementSyntax)statement);
                classifiers.Add(new(IterationStatement));
            }
            else if (CanCast(statement, typeof(ForStatementSyntax)))
            {
                Iteration IterationStatement = new((ForStatementSyntax)statement);
                classifiers.Add(new(IterationStatement));
            }
            else if (CanCast(statement, typeof(WhileStatementSyntax)))
            {
                Iteration IterationStatement = new((WhileStatementSyntax)statement);
                classifiers.Add(new(IterationStatement));
            }
            else if (CanCast(statement, typeof(DoStatementSyntax)))
            {
                Iteration IterationStatement = new((DoStatementSyntax)statement);
                classifiers.Add(new(IterationStatement));
            }

        }

        return classifiers;
    }

    static bool CanCast(SyntaxNode statement, Type type)
    {
        try
        {
            var s = Convert.ChangeType(statement, type);
            return true;
        }
        catch 
        {
            return false;
        }
    }

}
