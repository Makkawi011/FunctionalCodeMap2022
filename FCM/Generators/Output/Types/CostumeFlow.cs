namespace FCM.Generators.Output.Types;
internal class CostumeFlow
{
    private int source;

    public int Source
    {
        get { return source; }
        set { source = value; }
    }
    private int target;

    public int Target
    {
        get { return target; }
        set { target = value; }
    }

    private int category;

    public int Category
    {
        get { return category; }
        set { category = value; }
    }
    public CostumeFlow(int source, int target, int category)
    {
        Source = source;
        Target = target;
        Category = category;
    }
    public CostumeFlow(int source, int target)
    {
        Source = source;
        Target = target;
    }
    public CostumeFlow()
    {

    }
}
