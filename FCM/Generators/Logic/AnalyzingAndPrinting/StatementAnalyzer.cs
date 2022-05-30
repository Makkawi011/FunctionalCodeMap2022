using System.Collections.Generic;
using System.Linq;
using static FCM.Generators.Logic.Helper;
using FCM.Generators.Logic.Types;
using FCM.Generators.Logic.Types.Statement;
using FCM.Generators.Output;

using LanguageExt;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using Optional = FCM.Generators.Logic.Types.Statement.Optional;

namespace FCM.Generators.Logic.AnalyzingAndPrinting;

internal class StatementAnalyzer
{
    public static AnalyzeMethodResult Analyze(IEnumerable<SyntaxNode> statements, int totalFunctionId)
    {
        IEnumerable<StatementClassifier> ClassificationStatements = StatementClassifier.Classify(statements);

        AnalyzeMethodResult result = Analyze(ClassificationStatements, totalFunctionId);

        return result;
    }

    public static AnalyzeMethodResult Analyze(IEnumerable<StatementClassifier> ClassificationStatements, int totalFunctionId)
    {

        AnalyzeMethodResult Output = new();

        bool IsNewMethod = true;

        foreach (var classif in ClassificationStatements)
        {
            AnalyzeMethodResult StatementResult = null;

            if (!IsNewMethod && DGML.Functions.Count >= 2)
            {
                int NextStatementId = MakeNewId();
                int PreviousStatementId = GetLastIdUsed();
                AddFlowToFinalResultAndTo(StatementResult, new(PreviousStatementId, NextStatementId));
            }


            StatementResult = classif.Statement.GetType() switch
            {
                Alternative => Alternative.AnalyzeAlternativeStatement(classif.Statement, totalFunctionId),
                Invocation => Invocation.AnalyzeInvocationStatement(classif.Statement, totalFunctionId),
                Iteration => Iteration.AnalyzeIterationStatement(classif.Statement, totalFunctionId),
                Optional => Optional.AnalyzeOptionalStatement(classif.Statement, totalFunctionId),
                _ => Parallel.AnalyzeParallelStatement(classif.Statement, totalFunctionId, IsNewMethod)
            };

            Output.Functions.AddRange(StatementResult.Functions);
            Output.Flows.AddRange(StatementResult.Flows);

            IsNewMethod = false;
        }

        return Output;
        
    }



    public static bool AnyInvocationContainedWithinStatement(SyntaxNode statement)
    => statement.DescendantNodes().OfType<InvocationExpressionSyntax>().Any();
    public static IEnumerable<SyntaxNode> GetStatementsAsSyntaxNodesFrom
    (Either<MethodDeclarationSyntax, LocalFunctionStatementSyntax> input)
    {
        IEnumerable<SyntaxNode> statements;
        try
        {
            statements = input.IsLeft ?
                ((MethodDeclarationSyntax)input).Body.Statements
                : ((LocalFunctionStatementSyntax)input).Body.Statements;

        }
        catch
        {
            statements = input.IsLeft ?
                ((MethodDeclarationSyntax)input).ExpressionBody.DescendantNodes()
                : ((LocalFunctionStatementSyntax)input).ExpressionBody.DescendantNodes();
        }

        return statements;
    }
   
}
