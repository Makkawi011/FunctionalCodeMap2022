using System.Collections.Generic;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using Microsoft.CodeAnalysis;
using FCM.Generators.Logic.Types.Statement;
using FCM.Generators.Logic.AnalyzingAndPrinting;
using System.Linq;
using FCM.Generators.Input;
using LanguageExt;
using Optional = FCM.Generators.Logic.Types.Statement.Optional;

namespace FCM.Generators.Logic.Types;

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


        foreach (var statement in statements.Where(StatementAnalyzer.AnyInvocationContainedWithinStatement))
        {
            
            //Alternative
            if (statement.CanBeConsidered<TryStatementSyntax>())
            {
                var alternativeStatement = new Alternative((TryStatementSyntax)statement);
                classifiers.Add(new(alternativeStatement));
            }
            //Optional
            else if (statement.CanBeConsidered<IfStatementSyntax>())
            {
                Optional OptionalStatement = new((IfStatementSyntax)statement);
                classifiers.Add(new(OptionalStatement));

            }
            else if (statement.CanBeConsidered<SwitchExpressionSyntax>())
            {
                Optional OptionalStatement = new((SwitchStatementSyntax)statement);
                classifiers.Add(new(OptionalStatement));
            }
            //Iteration
            else if (statement.CanBeConsidered<ForEachStatementSyntax>())
            {
                Iteration IterationStatement = new((ForEachStatementSyntax)statement);
                classifiers.Add(new(IterationStatement));
            }
            else if (statement.CanBeConsidered<ForStatementSyntax>())
            {
                Iteration IterationStatement = new((ForStatementSyntax)statement);
                classifiers.Add(new(IterationStatement));
            }
            else if (statement.CanBeConsidered<WhileStatementSyntax>())
            {
                Iteration IterationStatement = new((WhileStatementSyntax)statement);
                classifiers.Add(new(IterationStatement));
            }
            else if (statement.CanBeConsidered<DoStatementSyntax>())
            {
                Iteration IterationStatement = new((DoStatementSyntax)statement);
                classifiers.Add(new(IterationStatement));
            }
            else //Parallel and NonParllel Invocation
            {
                try
                {
                    var Invocation = ((ExpressionStatementSyntax)statement)
                    .DescendantNodes()
                    .OfType<InvocationExpressionSyntax>()
                    .SingleOrDefault();

                    //parallel invocation
                    if (IsParallelInvocation(Invocation))
                    {
                        Parallel ParallelStatement = new(Invocation);
                        classifiers.Add(new(ParallelStatement));
                    }
                    //NonParllel Invocation
                    else
                    {
                        Invocation InvocationStatement = new(Invocation);
                        classifiers.Add(new(InvocationStatement));

                    }
                }
                catch { /*ignore , statement have invocation but we cannot Classify this statement */}
                
            }



        }

        return classifiers;
    }
    static bool IsParallelInvocation(InvocationExpressionSyntax invocation)
    {
        
        var model = invocation.GetSemanticModel();

        var Invocation = model.GetEquivalentInvocationExpressionSyntaxFor(invocation);

        var IMethodSymbolObj = model
            .GetSymbolInfo(Invocation.Expression).Symbol
            as IMethodSymbol;


        return IMethodSymbolObj.ConstructedFrom.ToString() 
            == "System.Threading.Thread.Start()";

    }
    
}
