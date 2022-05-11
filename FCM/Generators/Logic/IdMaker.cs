using FCM.Generators.Output;

namespace FCM.Generators.Logic;

internal static class IdMaker
{
    public static Func<int> GetId = () => DGML.Functions.Count;
    public static Func<int> GetNextId = () => DGML.Functions.Count + 1;
}
