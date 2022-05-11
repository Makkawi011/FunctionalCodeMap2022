namespace FCM.Generators.Output.Types;

enum Category
{
    Contains 
}

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

    private Category category;

    public Category Category
    {
        get { return category; }
        set { category = value; }
    }
    public CostumeFlow(int source, int target, Category category)
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
