using static FCM.Generators.Logic.Helper;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using FCM.Generators.Input;
using FCM.Generators.Logic.AnalyzingAndPrinting;
using FCM.Generators.Output.Types;

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

    public static AnalyzeMethodResult AnalyzeInvocationStatement(Invocation InvocationStatement, int totalFunctionId)
    {
        AnalyzeMethodResult result = new();
        var incovation = (InvocationExpressionSyntax)InvocationStatement.Statement;

        try
        {
            var methodDecleration = incovation.GetOrginalFunction();

            FunctionAnalyzer.AnalyzeAndDrowStartingFrom(methodDecleration);

        }
        catch 
        {
            int Id = MakeNewId();

            AddFunctionToFinalResultAndTo(result, new() { Id = Id, Lable = $"{incovation}" , Info = "We could not find the Method decleration for this Invocation" });
            AddFlowToFinalResultAndTo(result, new(totalFunctionId, Id));
        }

        return result;
    }
}

