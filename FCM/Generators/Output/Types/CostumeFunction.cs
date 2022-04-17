namespace FCM.Generators.Output.Types;

internal class CostumeFunction
{
    private string id;

    public string Id
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
    private string category;

    public string Category
    {
        get { return category; }
        set { category = value; }
    }

    public CostumeFunction(string id, string lable, string info, string group, string category)
    {
        Id = id;
        Lable = lable;
        Info = info;
        Group = group;
        Category = category;
    }
    public CostumeFunction(string id, string lable, string category)
    {
        Id = id;
        Lable = lable;
        Category = category;
    }
}

