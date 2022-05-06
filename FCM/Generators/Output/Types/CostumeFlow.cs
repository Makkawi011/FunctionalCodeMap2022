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

    private CategoryId categoryId;

    public CategoryId CategoryId
    {
        get { return categoryId; }
        set { categoryId = value; }
    }
    public CostumeFlow(int source, int target, CategoryId category)
    {
        Source = source;
        Target = target;
        CategoryId = category;
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
