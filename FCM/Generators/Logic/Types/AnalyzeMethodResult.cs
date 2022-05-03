
using System.Collections.Generic;

using FCM.Generators.Output.Types;

namespace FCM.Generators.Logic.Types;

internal class AnalyzeMethodResult
{
    private List<CostumeFunction> functions;

    public List<CostumeFunction> Functions
    {
        get { return functions; }
        set { functions = value; }
    }

    private List<CostumeFlow> flows;
    public List<CostumeFlow> Flows
    {
        get { return flows; }
        set { flows = value; }
    }
}
