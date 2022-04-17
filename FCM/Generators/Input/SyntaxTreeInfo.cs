using System.Collections.Generic;

using Microsoft.CodeAnalysis;

namespace FCM.Generators.Input;
public class SyntaxTreeInfo
{
    public SyntaxTree SyntaxTree { get; private set; }
    public SemanticModel SemanticModel { get; private set; }

    public SyntaxTreeInfo(SyntaxTree syntaxTree, SemanticModel semanticModel)
    {
        this.SyntaxTree = syntaxTree;
        this.SemanticModel = semanticModel;
    }
}