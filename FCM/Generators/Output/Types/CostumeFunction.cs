namespace FCM.Generators.Output.Types;

internal class CostumeFunction
{
    private int id;

    public int Id
    {
        get { return id; }
        set { id = value; }
    }
    private string lable;

    public string Lable
    {
        get { return lable; }
        set { lable = value; }
    }
    private string info;

    public string Info
    {
        get { return info; }
        set { info = value; }
    }
    private string group;

    public string Group
    {
        get { return group; }
        set { group = value; }
    }
    private CategoryId categoryId;

    public CategoryId CategoryId
    {
        get { return categoryId; }
        set { categoryId = value; }
    }

    public CostumeFunction(int id, string lable, string info, string group, CategoryId categoryId)
    {
        Id = id;
        Lable = lable;
        Info = info;
        Group = group;
        CategoryId = categoryId;
    }
    public CostumeFunction(int id, string lable, CategoryId categoryId)
    {
        Id = id;
        Lable = lable;
        CategoryId = categoryId;
    }
    public CostumeFunction()
    {

    }
}

