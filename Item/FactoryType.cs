using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley.ItemTypeDefinitions;

namespace FactoryMod.Item;

public class FactoryType: IItemDataDefinition
{
    public IEnumerable<string> GetAllIds()
    {
        throw new NotImplementedException();
    }

    public bool Exists(string itemId)                                                                                                                                              
    {
        throw new NotImplementedException();
    }

    public ParsedItemData GetData(string itemId)
    {
        throw new NotImplementedException();
    }

    public ParsedItemData GetErrorData(string itemId)
    {
        throw new NotImplementedException();
    }

    public StardewValley.Item CreateItem(ParsedItemData data)
    {
        throw new NotImplementedException();
    }

    public Rectangle GetSourceRect(ParsedItemData data, Texture2D texture, int spriteIndex)
    {
        throw new NotImplementedException();
    }

    public Texture2D GetErrorTexture()
    {
        throw new NotImplementedException();
    }

    public string GetErrorTextureName()
    {
        throw new NotImplementedException();
    }

    public Rectangle GetErrorSourceRect()
    {
        throw new NotImplementedException();
    }

    public string Identifier { get; }
    public string StandardDescriptor { get; }
}