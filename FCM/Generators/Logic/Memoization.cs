using System.Collections.Generic;

using FCM.Generators.Logic.Types;
using FCM.Generators.Output;

using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FCM.Generators.Logic;

internal static class Memoization
{
    public static Dictionary<MethodDeclarationSyntax, AnalyzeMethodResult> MemoMethods = new();

    public static bool HasBeenAnalyzedBefor(this MethodDeclarationSyntax method) => MemoMethods.ContainsKey(method);

    public static void AddKnownResultAnalyzesToFinalResults(this MethodDeclarationSyntax method)
    {
        if (MemoMethods.TryGetValue(method, out AnalyzeMethodResult results))
        {
            var defaultFunctions = results.Functions;

            var defaultFlows = results.Flows;

            bool[,] ArrayFlowChanges = new bool[defaultFlows.Count, 2]; //all of items = false by default

            //update Id(s)
            foreach (var defaultFunc in defaultFunctions)
            {
                var newId = DGML.Functions.Count;

                DGML.Functions.Add(
                    new()
                    {
                        Id = newId,
                        Info = defaultFunc.Info,
                        CategoryId = defaultFunc.CategoryId,
                        Group = defaultFunc.Group,
                        Lable = defaultFunc.Lable
                    });

                //update flows id 
                for (int i = 0; i < defaultFlows.Count; i++)
                {

                    var defaultflow = defaultFlows[i];

                    if (defaultflow.Source == defaultFunc.Id && ArrayFlowChanges[i, 0] == false)
                    {
                        DGML.Flows.Add(new()
                        {
                            Source = newId,
                            Target = defaultflow.Target,
                            Category = defaultflow.Category
                        });

                        ArrayFlowChanges[i, 0] = true;
                    }

                    if (defaultflow.Target == defaultFunc.Id && ArrayFlowChanges[i, 1] == false)
                    {
                        DGML.Flows.Add(new()
                        {
                            Source = defaultflow.Source,
                            Target = newId,
                            Category = defaultflow.Category
                        });

                        ArrayFlowChanges[i, 1] = true;
                    }

                }

            }
        }

        
    }
}
