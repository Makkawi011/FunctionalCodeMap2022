using  FCM.Generators.Output;

using LanguageExt;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System.Collections.Generic;
using System.Linq;


namespace FCM.Generators.Logic;

internal static class AnalyzeCustomeCode
{
    public static void StartingFrom(string inputFileContent , List<MemberDeclarationSyntax> members )
    {

        inputFileContent
                .IfNotEmpty()
                .IfHaveGlobalInvocationsOrTobLevelFunction()
                .TryDrowUsing(members);
    }
    private static Option<string> IfNotEmpty
        (this string inputFileContent)
    {
        if ( ! string.IsNullOrEmpty(inputFileContent)) 
            return inputFileContent;
        else
        {
            DGML
                .Functions
                .Add(new ("-1", "there is no code", "Error"));

            return Option<string>.None;
        } 
    }
    private static Option<string> IfHaveGlobalInvocationsOrTobLevelFunction
        (this Option<string> inputFileContent)
    {
        if (inputFileContent.Case is null) 
            return Option<string>.None;

        var IsThrereAnyInvocationExpressionSyntax = SyntaxFactory
                          .ParseSyntaxTree((string)inputFileContent)
                          .GetRoot()
                          .DescendantNodes()
                          .Where(node => node is MethodDeclarationSyntax or InvocationExpressionSyntax)
                          .Any();

        if (IsThrereAnyInvocationExpressionSyntax)
            return inputFileContent;
        else
        {
            DGML
                .Functions
                .Add(new("-1", "There is no global invocation and no function definition ", "Error"));

            return Option<string>.None;
        }
    
    }

  
 
}

