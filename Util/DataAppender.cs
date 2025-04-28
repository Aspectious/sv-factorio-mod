using StardewModdingAPI;
using StardewValley.GameData.Objects;

namespace FactoryMod.Util;

public class DataAppender
{
    public static void AppendFile(IModHelper modhelper, IAssetData asset, String ModFilePath)
    {
        var data = asset.AsDictionary<string, ObjectData>().Data;
        var modData = modhelper.ModContent.Load<Dictionary<String, ObjectData>>("assets/Data/Objects.json");
        foreach (KeyValuePair<String, ObjectData> addeditem in modData)
        {
            data.Add(addeditem.Key, addeditem.Value);
        }
    }
}