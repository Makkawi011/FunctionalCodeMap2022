#pragma warning disable VSTHRD002 // Avoid problematic synchronous waits


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.MSBuild;

using Solution = Microsoft.CodeAnalysis.Solution;

namespace FCM.Generators.Input;

internal static class CostumeCode
{
    public static void PrepareSyntaxTreesAndSymanticModels()
    {
        // custome solution = The solution that the user is trying this extinsion on it
        // get sln path for custome solution 
        // by using Community.VisualStudio.Toolkit library
        var slnPath = 
            VS
            .Solutions
            .GetCurrentSolutionAsync()
            .Result
            .FullPath;
        
        //now please whatch https://www.youtube.com/watch?v=_cIVa-RctcA&t=49s

        // to get information about custome solution  
        // information like documents that custome solution use it
        // so will use MSBuildWorkspace class and
        // OpenSolutionAsync function to get object of Solution 
        var workSpace = MSBuildWorkspace.Create();
        var solution =  workSpace
            .OpenSolutionAsync(slnPath)
            .Result;

        // compile all projects 
        // in custome solution and
        // get list of SyntaxTreeInfo 
        // foreach for all associated files
        //and save this infos in static list (SyntaxTreeInfos)
        Compile(solution);

    }


    //we will fill it out when we compile the solution and it will use when you need to 
    private static List<SyntaxTreeInfo> SyntaxTreeInfos;

    ///Compile : impure function becouse it (write on) fill SyntaxTreeInfos list
    private static void Compile(Solution solution)
    {
        // compile all projects 
        // in input (/Custome) solution and
        // get list of SyntaxTreeInfo 
        // if you need more information about this Function
        // go to https://www.youtube.com/watch?v=HT7k3Qm4uFY 
        // this source code https://github.com/raffaeler/dotnext2018Piter will help you

        SyntaxTreeInfos = new List<SyntaxTreeInfo> (); // static list

        var compilationTasks = solution.Projects
            .Select(s => s.GetCompilationAsync())
            .ToArray();

        var compilations = Task.WhenAll(compilationTasks)
           .Result;

        foreach (var compilation in compilationTasks.Select(t => t.Result))
        {
            using (var ms = new MemoryStream())
            {
                var res = compilation.Emit(ms);

                if (!res.Success) throw new Exception("Compilation failed in AnalysisContext");
                
            }

            foreach (var syntaxTree in compilation.SyntaxTrees)
            {
                var semanticModel = compilation.GetSemanticModel(syntaxTree, false);
                SyntaxTreeInfos.Add(new SyntaxTreeInfo(syntaxTree, semanticModel));
            }

        }
    }


    ///GetSemanticModelFor : impure function becouse it read from SyntaxTreeInfos list
    public static SemanticModel GetSemanticModelFor(SyntaxNode node)
    {
        var filePath = node.SyntaxTree.FilePath;
        var semanticModel =
            SyntaxTreeInfos  // static list
            .Where(s => s.SyntaxTree.FilePath == filePath)
            .Select(s => s.SemanticModel)
            .FirstOrDefault();

        return semanticModel;
    }

    /// <summary>
    /// when you have the semantic model and
    /// invocation from a source 
    /// and you want to fetch the equivalent invocation from the input semantic model
    /// </summary>
    /// <param name="model">the semantic model from which we will get the equivalent invocation</param>
    /// <param name="InputInvocation">a Invocation from a source </param>
    /// <returns>an equivalent invocation to input invocation 
    /// , from the input semantic model </returns>
    public static InvocationExpressionSyntax GetEquivalentInvocationExpressionSyntaxFor(this SemanticModel model , InvocationExpressionSyntax InputInvocation)
    {
       var EquivalentInvocation =  model
            .SyntaxTree
            .GetRoot()
            .DescendantNodes()
            .OfType<InvocationExpressionSyntax>()
            .FirstOrDefault(invoc => invoc.SyntaxTree.FilePath == InputInvocation.SyntaxTree.FilePath);

        return EquivalentInvocation;
    }
}


#pragma warning restore VSTHRD002 // Avoid problematic synchronous waits