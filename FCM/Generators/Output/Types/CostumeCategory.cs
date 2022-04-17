namespace FCM.Generators.Output.Types;

internal class CostumeCategory
{
    private string id;

    public string Id
    {
        get { return id; }
        set { id = value; }
    }
    private string background;

    public string Background
    {
        get { return background; }
        set { background = value; }
    }
    public CostumeCategory(string id, string background)
    {
        Id = id;
        Background = background;
    }
}

