using static FCM.Generators.Logic.Helper;
using FCM.Generators.Output.Types;

using Microsoft.CodeAnalysis;
using FCM.Generators.Output;
using System.Linq;

namespace FCM.Generators.Logic.Types.Statement;

internal class Parallel
{
    const string ParallelStart = "Parallel Start";
    const string ParallelEnd = "Parallel End";

    private SyntaxNode statement;

    public SyntaxNode Statement
    {
        get { return statement; }
        set { statement = value; }
    }
    public Parallel(SyntaxNode statement)
    {
        Statement = statement;
    }

    public static AnalyzeMethodResult AnalyzeParallelStatement(Parallel Parallel, int totalFunctionId , bool IsNewMethod)
    {
        AnalyzeMethodResult result = new();

        var LastBlock = DGML.Functions.LastOrDefault();
        if (LastBlock is not null)
            if (!IsNewMethod && LastBlock.Lable.Equals(ParallelEnd))
            {
                int startParId = DGML.Functions.Last(b => b.Lable.Equals(ParallelEnd)).Id ;
                int endParId = LastBlock.Id ;

                int Id = MakeNewId();
                AddFunctionToFinalResultAndTo(result, new() {Id = Id , Lable = Parallel.Statement.ToString() , Info = "thread function " });

                UndoLastFlowAdded(result); // added in Analyze Method in Statement Analyzer 

                AddFlowToFinalResultAndTo(result, new(totalFunctionId, Id, Category.Contains));
                AddFlowToFinalResultAndTo(result, new(startParId, Id));
                AddFlowToFinalResultAndTo(result, new(Id ,endParId));

                Re_OrderTheParallelEndBlockByMakeItInLastFunctionList(result, LastBlock);


            }
            else
            {
                int startParId = MakeNewId();

                AddFunctionToFinalResultAndTo(result, new(startParId, ParallelStart, CategoryId.Par));
                AddFlowToFinalResultAndTo(result, new(totalFunctionId, startParId, Category.Contains));

                //_____________________________________


                var endParId = MakeNewId();

                int Id = MakeNewId();
                AddFunctionToFinalResultAndTo(result, new() { Id = Id, Lable = Parallel.Statement.ToString(), Info = "thread function " });
                AddFlowToFinalResultAndTo(result, new(totalFunctionId, Id, Category.Contains));
                AddFlowToFinalResultAndTo(result, new(startParId, Id));

                AddFlowToFinalResultAndTo(result, new(Id, endParId));
                //___________________________________
                AddFunctionToFinalResultAndTo(result, new(endParId, ParallelEnd, CategoryId.Par));
                AddFlowToFinalResultAndTo(result, new(totalFunctionId, endParId, Category.Contains));

                //___________________________________
                AddFlowToFinalResultAndTo(result, new(startParId, endParId));
            }



        return result;
    }
}
