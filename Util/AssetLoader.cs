using System.Text.Json;
using StardewModdingAPI;

namespace FactoryMod.Util;


public class AssetLoader
{
    public Dictionary<String, String> AssetMap;
    
    public AssetLoader(Mod mod, Dictionary<String, String> pathMap)
    {
        this.AssetMap = pathMap;
        foreach (KeyValuePair<String, String> pair in this.AssetMap)
        {
            mod.Monitor.Log(pair.Key + ":" + pair.Value);
        }
    }

    public void InterceptAssetReq(String InternalLocation)
    {
        
    }
}