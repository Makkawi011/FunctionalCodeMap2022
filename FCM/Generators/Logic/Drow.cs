using System.Collections.Generic;
using System.Linq;

using LanguageExt;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using FCM.Generators.Output;
using static FCM.Generators.Input.CostumeCode;

namespace FCM.Generators.Logic;

internal static class Drow
{
    public static void TryDrowUsing
      (this Option<string> inputFileContent, List<MemberDeclarationSyntax> members)
    {
        if (inputFileContent.Case is null) return;

        var startSyntaxNodes = SyntaxFactory
            .ParseSyntaxTree((string) inputFileContent)
            .GetRoot()
            .DescendantNodes();


        try
        {
            startSyntaxNodes
            .TryDrowingStartingFromGlobalInvocationUsing(members)
            .TryDrowStartingFromTopLevelFunctionUsing(members);
        }
        catch (Exception)
        {
            DGML
                 .Functions
                 .Add(new("-1", "There is no global invocation and no function definition ", "Error"));

        }

    }

    private static IEnumerable<SyntaxNode> TryDrowingStartingFromGlobalInvocationUsing
        (this IEnumerable<SyntaxNode> startSyntaxNodes, List<MemberDeclarationSyntax> members)
    {
        var FirstGlobalInvocation = startSyntaxNodes
            .OfType<InvocationExpressionSyntax>()
            .FirstOrDefault();

        if (FirstGlobalInvocation == null) return startSyntaxNodes;
        else
        {
            var model = GetSemanticModelFor(FirstGlobalInvocation);
            var tree = (IMethodSymbol) model.GetSymbolInfo(FirstGlobalInvocation).Symbol.OriginalDefinition;
            
            return startSyntaxNodes;
        }
    }
    private static void TryDrowStartingFromTopLevelFunctionUsing
        (this IEnumerable<SyntaxNode> startSyntaxNodes, List<MemberDeclarationSyntax> members)
    {

        throw new NotImplementedException();
    }

}
