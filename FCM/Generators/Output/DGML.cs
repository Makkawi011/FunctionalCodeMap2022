using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

using OpenSoftware.DgmlTools;
using OpenSoftware.DgmlTools.Builders;
using OpenSoftware.DgmlTools.Model;

using FCM.Generators.Output.Types;
using FCM.Generators.Logic;
using Microsoft.Build.Framework.XamlTypes;
using Category = OpenSoftware.DgmlTools.Model.Category;

namespace FCM.Generators.Output;

internal class DGML
{
    static void Clear() { Functions.Clear(); Flows.Clear(); Memo.Forget(); }

    public static List<CostumeFunction> Functions = new();
    public static List<CostumeFlow> Flows = new();
    private static readonly List<CostumeCategory> Categories = new()
{
        new CostumeCategory(CategoryId.Par, "Green"),
        new CostumeCategory(CategoryId.Alt, "Yellow"),
        new CostumeCategory(CategoryId.Cho, "Blue"),
        new CostumeCategory(CategoryId.Ite, "Red"),
        new CostumeCategory(CategoryId.Error, "Black")
    };

    public static byte[] GetDGMLCodeAsArrayOfBytes()
    {

        DgmlBuilder builder = new()
        {
            //convert Types ( I wrote it ) to Orginal Types ( OpenSoftware.DgmlTools.Model )

            NodeBuilders = new List<NodeBuilder> { new NodeBuilder<CostumeFunction>(CustomeFunction2Node) },
            LinkBuilders = new List<LinkBuilder> { new LinkBuilder<CostumeFlow>(CustomeFlow2Link) },
            CategoryBuilders = new List<CategoryBuilder> { new CategoryBuilder<CostumeCategory>(CreatCustomeCategory) }
        };

        DirectedGraph DG = new();

        DG = builder.Build(Functions, Flows, Categories);

        DG.GraphDirection = GraphDirection.LeftToRight;
        DG.Layout = Layout.Sugiyama;

        return ConvertDirectedGraphToByteArray(DG);
    }

    private static byte[] ConvertDirectedGraphToByteArray(DirectedGraph graph)
    {
        XmlSerializer serializer = new(typeof(DirectedGraph));
        MemoryStream memoryStream = new();

        // Serialize DirectedGraph graph to  DGML Code
        // in memoryStream (to put DGML Code in memory as stream instead of a disk or a network connection)
        // as UTF8 (VS need DGML code as byte array in utf8)

        var streamWriter = new StreamWriter(memoryStream, Encoding.UTF8);
        serializer.Serialize(streamWriter, graph);

        byte[] utf8EncodedXml = memoryStream.ToArray();


        Clear();

        return utf8EncodedXml;
    }

<<<<<<<<< Temporary merge branch 1
    

    
>>>>>>>>> Temporary merge branch 2

            Id = fun.Id.ToString(),
>>>>>>>>> Temporary merge branch 2

    private static Node CustomeFunction2Node(CostumeFunction fun)
            Category = fun.CategoryId.ToString()
        {
            Id = fun.Id,
            Label = fun.Lable,
            Description = fun.Info,
            Source = flow.Source.ToString(),
            Target = flow.Target.ToString(),
            Category = flow.CategoryId.ToString()
    private static Link CustomeFlow2Link(CostumeFlow flow)
        => new()
        {
            Source = flow.Source.ToString(),
            Id = category.Id.ToString(),
            Category = flow.Category.ToString()
        };
    private static Category CreatCustomeCategory(CostumeCategory category)
        => new()
        {
            Id = category.Id,
            Background = category.Background
        };
}