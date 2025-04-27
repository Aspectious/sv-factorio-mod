using System.Text.Json;
using StardewModdingAPI;

namespace FactoryMod.Util;


public class AssetLoader
{
    public Dictionary<String, String> pathMap;
    
    public AssetLoader(Mod mod, Dictionary<String, String> pathMap)
    {
        this.pathMap = pathMap;
        foreach (KeyValuePair<String, String> pair in this.pathMap)
        {
            mod.Monitor.Log(pair.Key + ":" + pair.Value);
        }
    }

    public void InterceptAssetReq(String InternalLocation)
    {
        
    }
}