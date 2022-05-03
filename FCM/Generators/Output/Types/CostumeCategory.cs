namespace FCM.Generators.Output.Types;


public enum CategoryId
{
    Par , Alt , Cho , Ite , Error
}

internal class CostumeCategory
{
    private CategoryId id;

    public CategoryId Id
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
    public CostumeCategory(CategoryId id, string background)
    {
        Id = id;
        Background = background;
    }
}

