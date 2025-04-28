using StardewModdingAPI;
using StardewValley.GameData.Objects;

namespace FactoryMod.Util;

public class DataPatcher
{
    public static void doPatch(IModHelper helper)
    {
        helper.GameContent.Load<Dictionary<String, ObjectData>>("Data/Objects");
        helper.GameContent.Load<Dictionary<String, String>>("Data/CraftingRecipes");
    }
    public static void PatchObjectData(IModHelper modhelper, IAssetData asset, String ModFilePath)
    {
        var data = asset.AsDictionary<string, ObjectData>().Data;
        var modData = modhelper.ModContent.Load<Dictionary<String, ObjectData>>("assets/Data/Objects.json");
        foreach (KeyValuePair<String, ObjectData> addeditem in modData)
        {
            data.Add(addeditem.Key, addeditem.Value);
        }
    }
    public static void PatchString(IModHelper modhelper, IAssetData asset, String ModFilePath)
    {
        var data = asset.AsDictionary<String, String>().Data;
        var modData = modhelper.ModContent.Load<Dictionary<String, String>>("assets/Data/CraftingRecipes.json");
        foreach (KeyValuePair<String, string> addeditem in modData)
        {
            data.Add(addeditem.Key, addeditem.Value);
        }
    }
}