using FCM.Generators.Output;
using FCM.Generators.Output.Types;
using System.Collections.Generic;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

namespace FCM.Generators.Logic.AnalyzingAndPrinting.NonGlobalStatementsAnalyzers;
internal class MethodDeclerationAnalyzer
{
    static List<CostumeFunction> MethodCostumeFunctions = new();
    static List<CostumeFlow> MethodCostumeFlows = new();
    static void Clear() { MethodCostumeFunctions.Clear(); MethodCostumeFlows.Clear(); }

    public static void AnalyzeAndDrowStartingFrom(MethodDeclarationSyntax topLevelFunction) => AnalyzeFunction(topLevelFunction);

    private static void AnalyzeFunction(MethodDeclarationSyntax method)
    {
        Clear();

        try
        {
            var StatementsBody = method.Body.Statements;
            AnalyzeFunctionIfItContainsBody(StatementsBody);
        }
        catch (Exception)
        {
            var ExpressionBody = method.ExpressionBody;
            AnalyzeArrowFunction(ExpressionBody);
        }
    }

    private static void AnalyzeFunctionIfItContainsBody(SyntaxList<StatementSyntax> statementsBody)
    {
        
    }
    private static void AnalyzeArrowFunction(ArrowExpressionClauseSyntax expressionBody)
    {
        throw new NotImplementedException();
    }
}
