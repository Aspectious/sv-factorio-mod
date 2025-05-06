using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley;

namespace FactoryMod.Item;

public class GantryStructure : StardewValley.Item
{
    public override void drawInMenu(SpriteBatch spriteBatch, Vector2 location, float scaleSize, float transparency, float layerDepth,
        StackDrawType drawStackNumber, Color color, bool drawShadow)
    {
        throw new NotImplementedException();
    }

    public override string getDescription()
    {
        throw new NotImplementedException();
    }

    public override bool isPlaceable()
    {
        throw new NotImplementedException();
    }

    protected override StardewValley.Item GetOneNew()
    {
        throw new NotImplementedException();
    }

    public override int maximumStackSize()
    {
        throw new NotImplementedException();
    }

    public override string TypeDefinitionId { get; }

    public override string DisplayName
    {
        get
        {
            return "Gantry Structure";
        }
    }
}