using StardewModdingAPI;
using StardewValley.GameData.BigCraftables;
using StardewValley.GameData.Objects;

namespace FactoryMod.Util;

public class DataPatcher
{
    public static void doPatch(IModHelper helper)
    {
        helper.GameContent.Load<Dictionary<String, ObjectData>>("Data/Objects");
        helper.GameContent.Load<Dictionary<String, String>>("Data/CraftingRecipes");
        helper.GameContent.Load<Dictionary<String, BigCraftableData>>("Data/BigCraftables");
    }
        public static void Patch<T>(IModHelper modhelper, IAssetData asset, String ModFilePath)
    {
        var data = asset.AsDictionary<String, T>().Data;
        var modData = modhelper.ModContent.Load<Dictionary<String, T>>(ModFilePath);
        foreach (KeyValuePair<String, T> addeditem in modData)
        {
            data.Add(addeditem.Key, addeditem.Value);
        }
    }
}