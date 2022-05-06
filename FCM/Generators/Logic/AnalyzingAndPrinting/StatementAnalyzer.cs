using System.Collections.Generic;

using FCM.Generators.Logic.Types;
using FCM.Generators.Logic.Types.StatementTypes;
using Optional = FCM.Generators.Logic.Types.StatementTypes.Optional;
using LanguageExt;
using Microsoft.CodeAnalysis;
using System;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FCM.Generators.Logic.AnalyzingAndPrinting;

internal class StatementAnalyzer
{
    public static AnalyzeMethodResult Analyze(IEnumerable<SyntaxNode> statements)
    {
        AnalyzeMethodResult result = new ();
        var ClassificationStatements = StatementClassifier.Classify(statements);

        return result;
    }
    public static AnalyzeMethodResult AnalyzeStatement(SyntaxNode statement)
    {
        throw new NotImplementedException();
    }


}
