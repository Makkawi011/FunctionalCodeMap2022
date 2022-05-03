using FCM.Generators.Output;
using FCM.Generators.Output.Types;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System.Collections.Generic;
using System.Linq;


namespace FCM.Generators.Logic;

internal static class CostumeCodeAnalyzer
{
    public static void StartingFrom(string inputFilePath, string inputFileContent)
    {
        if (string.IsNullOrEmpty(inputFileContent))
        {
            DGML
                .Functions
                .Add(new(-1, "there is no code", CategoryId.Error));

            return;
        }


        var startSyntaxNodes = CSharpSyntaxTree
            .ParseText(inputFileContent)
            .WithFilePath(inputFilePath)
            .GetRoot().DescendantNodes();


        //The Input File have global statements type of Invocations
        var GlobalInvocations = GetAllGlobalInvocations(startSyntaxNodes);
        if (GlobalInvocations.Any())
        {
            Painter.AnalyzeAndDrowStartingFrom(GlobalInvocations);
            return;
        }

        //The Input File wasn't have global statements type of Invocations
        //but have global statement(s) type of local Function statement
        var LocalFunctionGlobalStatement = GetTopLevelLocalFunctionGlobalStatement(startSyntaxNodes);
        if (LocalFunctionGlobalStatement is not null)
        {
            Painter.AnalyzeAndDrowStartingFrom(LocalFunctionGlobalStatement);
            return;
        }

        //The Input File wasn't have global statements 
        //search on methods
        var MethodDeclaration = GetTopLevelMethodDeclarationSyntax(startSyntaxNodes);
        if (MethodDeclaration is not null)
        {
            Painter.AnalyzeAndDrowStartingFrom(MethodDeclaration);
            return;
        }

        DGML
            .Functions
            .Add(new CostumeFunction(-1, "No Global Invocation Or Local Funtion Or Method Decleration", CategoryId.Error));



    }

    #region Dealing With Global Statements

    private static IEnumerable<StatementSyntax> GetAllGlobalStatements
    (this IEnumerable<SyntaxNode> startSyntaxNodes)
    => startSyntaxNodes
         .OfType<GlobalStatementSyntax>()
         .Select(gss => gss.Statement);

    private static IEnumerable<StatementSyntax> WhereKindOf
    (this IEnumerable<StatementSyntax> CollectionOfGlobalStatementSyntax, SyntaxKind InputKind)
    => CollectionOfGlobalStatementSyntax
    .Where(gss => gss.IsKind(InputKind));

    #region Dealing With Global Invocations Functions

    //how you can Gel All Global Invocation Expression Syntax ?
    //first get all GlobalStatement
    //(by : GetAllGlobalStatements)
    //then filter statement to get just statement of kind ExpressionStatement
    //(by : WhereKindOf function with SyntaxKind.ExpressionStatement parameter)
    //then get all invocations in All ExpressionStatement
    //(by : GetAllInvocationsFromAllExpressionStatements & GetAllInvocationsInExpressionSyntax)

    private static IEnumerable<InvocationExpressionSyntax> GetAllGlobalInvocations
        (IEnumerable<SyntaxNode> startSyntaxNodes)
    {
        var GlobalInvocation = startSyntaxNodes
            .GetAllGlobalStatements()
            .WhereKindOf(SyntaxKind.ExpressionStatement)
            .GetAllInvocationsFromAllExpressionStatements();

        if (GlobalInvocation.Any()) return GlobalInvocation;
        else return Enumerable.Empty<InvocationExpressionSyntax>();
    }

    private static IEnumerable<InvocationExpressionSyntax> GetAllInvocationsFromAllExpressionStatements
        (this IEnumerable<StatementSyntax> CollectionOfGlobalStatementSyntax)
        => CollectionOfGlobalStatementSyntax
        .SelectMany(GetAllInvocationsInExpressionSyntax);

    private static IEnumerable<InvocationExpressionSyntax> GetAllInvocationsInExpressionSyntax
        (this StatementSyntax expressionStatement)
        => expressionStatement
        .DescendantNodes()
        .OfType<InvocationExpressionSyntax>();
    #endregion

    #region Dealing With Local Function Global Statement Functions
    private static LocalFunctionStatementSyntax GetTopLevelLocalFunctionGlobalStatement(IEnumerable<SyntaxNode> startSyntaxNodes)
    {
        var LocalFunction = startSyntaxNodes
            .GetAllGlobalStatements()
            .WhereKindOf(SyntaxKind.LocalFunctionStatement)
            .FirstOrDefault() as LocalFunctionStatementSyntax;

        return LocalFunction;
    }

    #endregion

    #endregion

    #region Dealing With Non Global Statements
    private static MethodDeclarationSyntax GetTopLevelMethodDeclarationSyntax(IEnumerable<SyntaxNode> syntaxNodes)
    {
        var topLevelMethod = syntaxNodes
            .OfType<MethodDeclarationSyntax>()
            .FirstOrDefault();

        return topLevelMethod;
    }
    #endregion
}

