
using System.Collections.Generic;
using System.Linq;
using static FCM.Generators.Input.CostumeCode;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FCM.Generators.Logic.AnalyzingAndPrinting;

internal static class  GlobalInvocationsAnalyzer
{
    internal static void AnalyzeAndDrowStartingFrom(IEnumerable<InvocationExpressionSyntax> invocations)
    {
        throw new NotImplementedException();
    }


    





    public static void TryDrow(IEnumerable<SyntaxNode> startSyntaxNodes)
    {

        var IsThrereGlobalStatementSyntax = startSyntaxNodes.OfType<GlobalStatementSyntax>().Any();
        if (IsThrereGlobalStatementSyntax)
        {
            startSyntaxNodes.DrowStartingFromInputFileGlobalInvocation();
            return;
        }


        var IsThrereMethodDeclarationSyntax = startSyntaxNodes.OfType<MethodDeclarationSyntax>().Any();
        if (IsThrereMethodDeclarationSyntax)
        {
            startSyntaxNodes.DrowStartingFromInputFileTopLevelFunction();
            return;
        }

    }

    private static void DrowStartingFromInputFileGlobalInvocation(this IEnumerable<SyntaxNode> startSyntaxNodes)
    {
        var GlobalInvocations = startSyntaxNodes
                        .OfType<InvocationExpressionSyntax>();

        Enumerable.Range(0, GlobalInvocations.Count())
            .Freeze().Do(i => AnalyzeInputFileGlobalInvocation(GlobalInvocations.ElementAt(i)));
    }

    private static void AnalyzeInputFileGlobalInvocation(InvocationExpressionSyntax InputFileGlobalInvocation)
    {

        //when you get Invocation from input file must be find Equivalent from costume files
        //becouse
        //the attribute of Input File Global Invocation
        //differ from 
        //the attribute of Global Invocation in costume files 

        //for example :
        //the attribute of Input File Global Invocation "Console.WriteLine();"
        //when comming from InputFileContent and InputFilePath
        //differ from the attribute of Global Invocation "Console.WriteLine();"
        //in costume files 

        //so we will work on "Console.WriteLine();" that coming from costume files 
        //by get the Get Equivalent Invocation of "Console.WriteLine();" from from costume files

        var model = GetSemanticModelFor(InputFileGlobalInvocation);

        var Invocation = model.GetEquivalentInvocationExpressionSyntaxFor(InputFileGlobalInvocation);

        var IMethodSymbolObj = model
            .GetSymbolInfo(Invocation.Expression).Symbol 
            as IMethodSymbol;

        var orginalMethod = IMethodSymbolObj
            .DeclaringSyntaxReferences
            .FirstOrDefault().GetSyntax() 
            as MethodDeclarationSyntax;

        var orginalMethodAsString = orginalMethod.ToString();


    }

    public static void DrowStartingFromInputFileTopLevelFunction(this IEnumerable<SyntaxNode> startSyntaxNodes)
    {
        throw new NotImplementedException();
    }
}


