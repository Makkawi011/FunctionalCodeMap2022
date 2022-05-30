using System.Collections.Generic;
using System.Linq;

using FCM.Generators.Logic.Types;
using FCM.Generators.Output;
using FCM.Generators.Output.Types;

using Microsoft.CodeAnalysis;

namespace FCM.Generators.Logic;

internal static class Helper
{
    public static readonly Queue<int> UnVisitedMethods = new();

    public static Func<int> MakeNewId = () => DGML.Functions.Count;
    public static Func<int> MakeNewIdPlus1 = () => DGML.Functions.Count + 1;
    public static Func<int> GetLastIdUsed = () => DGML.Functions.Last().Id;

    public static void AddFunctionToFinalResultAndTo(AnalyzeMethodResult results , CostumeFunction function)
    {
        results.Functions.Add(function);
        DGML.Functions.Add(function);  
    }
    public static void AddFlowToFinalResultAndTo(AnalyzeMethodResult results, CostumeFlow flow)
    {
        results.Flows.Add(flow);
        DGML.Flows.Add(flow);
    }


    public static void AddToFinalResultAndTo(AnalyzeMethodResult results , AnalyzeMethodResult piceOfResults)
    {
        DGML.Functions.AddRange(piceOfResults.Functions);
        DGML.Flows.AddRange(piceOfResults.Flows);

        results.Functions.AddRange(piceOfResults.Functions);
        results.Flows.AddRange(piceOfResults.Flows);
    }
    public static void UndoLastFlowAdded(AnalyzeMethodResult result)
    {
        result.Flows.RemoveAt(result.Flows.Count -1);
        DGML.Flows.RemoveAt(DGML.Flows.Count - 1);
    }

    public static void Re_OrderTheParallelEndBlockByMakeItInLastFunctionList(AnalyzeMethodResult results, CostumeFunction lastBlock )
    {    
        results.Functions.Remove(lastBlock);
        results.Functions.Add(lastBlock);

        DGML.Functions.Remove(lastBlock);
        DGML.Functions.Add(lastBlock);

    }


    public static bool CanBeConsidered<T>(this SyntaxNode statement)
    {
        try
        {
            return statement
                .DescendantNodes()
                .OfType<T>()
                .Any();
        }
        catch
        {
            return false;
        }
    }


}
