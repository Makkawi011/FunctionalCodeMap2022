using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using LanguageExt;
using System.Linq;

using static FCM.Generators.Logic.IdMaker;
using FCM.Generators.Logic.Types;

namespace FCM.Generators.Logic.AnalyzingAndPrinting;
internal class FunctionAnalyzer
{
    public static void AnalyzeAndDrowStartingFrom
        (Either<MethodDeclarationSyntax, LocalFunctionStatementSyntax> topLevelFunction)
        => Analyze(topLevelFunction);

    private static void Analyze(Either<MethodDeclarationSyntax, LocalFunctionStatementSyntax> method)
    {
        if (method.HasBeenAnalyzedBefor())
        {
            method.AddKnownResultAnalyzesToFinalResults();
            return;
        }

        var statementsContainingInvocations = 
            StatementAnalyzer.GetStatementsAsSyntaxNodesFrom(method)
            .Where(StatementAnalyzer.AnyInvocationContainedWithinStatement);

        AnalyzeMethodResult output;

        if (statementsContainingInvocations.Any())
            output = StatementAnalyzer.Analyze(statementsContainingInvocations);
        else
        {
            int Id = GetId();
            string MethodName = GetMethodName(method);
            string MethodContext = ConvertToString(method);

            output = new AnalyzeMethodResult();
            output.Functions.Add(new(Id, MethodName, MethodContext));
        }


        output.MemoAndAddToFinalResultsFor(method);
    }

    private static string GetMethodName(Either<MethodDeclarationSyntax, LocalFunctionStatementSyntax> input)
    {
        string MethodName;

        MethodName = input.IsLeft ?
            ((MethodDeclarationSyntax)input).Identifier.Text
            : ((LocalFunctionStatementSyntax)input).Identifier.Text;

        return MethodName;
    }
    private static string ConvertToString(Either<MethodDeclarationSyntax, LocalFunctionStatementSyntax> input)
    {
        string MethodContent;

        MethodContent = input.IsLeft ?
            ((MethodDeclarationSyntax)input).ToString()
            : ((LocalFunctionStatementSyntax)input).ToString();

        return MethodContent;
    }

}
