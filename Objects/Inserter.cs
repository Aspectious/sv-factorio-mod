using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Netcode;
using StardewValley;
using StardewValley.ItemTypeDefinitions;
using Object = StardewValley.Object;

namespace FactoryMod.Objects;

[XmlInclude(typeof(Inserter))]
public class Inserter: Object
{
    /*
     * CODE SNIPPET FROM FURNITURE ROTATION CODES
     * SEE StardewValley.Objects.Furniture
    
    /*
    public Inserter SetPlacement(int x, int y, int rotations = 0)
    {
        return this.SetPlacement(new Vector2((float)x, (float)y), rotations);
    }
    public Inserter SetPlacement(Point tile, int rotations = 0)
    {
        return this.SetPlacement(Utility.PointToVector2(tile), rotations);
    }
    public Inserter SetPlacement(Vector2 tile, int rotations = 0)
    {
        this.InitializeAtTile(tile);
        for (int index = 0; index < rotations; ++index)
            this.rotate();
        return this;
    }
    */

    public override string TypeDefinitionId => "(O)";
    public override string getDescription() => "The most basic logistical machine.";
    public string QualifiedItemId = "(O)FactoryMod.Inserter";
    public override string DisplayName => "Inserter";
    public int Fragility => 0;
    public int TILE = 1;

    private int rotation;
    private Vector2 pickupTileRelPos;
    private Vector2 dropoffTileRelPos;
    public Inserter()
    {
        this.Name = "Inserter";
        this.type.Value = "Factory";
        this.bigCraftable.Value = false;
        this.canBeSetDown.Value = true;
        this.fragility.Value = 0;
    }
    public Inserter(int initialStack) : base("FactoryMod.Inserter", initialStack, false, 120, 0)
    {
        this.Name = "Inserter";
        this.type.Value = "Factory";
        this.bigCraftable.Value = false;
        this.canBeSetDown.Value = true;
        this.fragility.Value = 0;
    }

    public override bool performToolAction(Tool t)
    {
        Game1.createObjectDebris(this.itemId.Value, (int)this.TileLocation.X, (int)this.TileLocation.Y, this.Location);
        Location.objects.Remove(this.tileLocation.Value);
        
        return true;
    }

    public Inserter(Vector2 position) : base(position, "FactoryMod.Inserter")
    {
        this.Name = "Inserter";
        this.type.Value = "Factory";
        this.bigCraftable.Value = false;
        this.canBeSetDown.Value = true;
        this.fragility.Value = 0;
        this.CanBeGrabbed = true;
        this.health = 10;
        this.RecalculateBoundingBox();
    }
    public override bool checkForAction(Farmer who, bool justCheckingForActivity = false)
    {
        if (justCheckingForActivity)
            return true;
        else return false;
    }
    
    public override void draw(SpriteBatch spriteBatch, int x, int y, float alpha = 1f)
    {
        if (this.isTemporarilyInvisible)
            return;
        base.draw(spriteBatch, x, y, alpha);
        Vector2 local = Game1.GlobalToLocal(Game1.viewport, new Vector2((float) (x * 64 /*0x40*/), (float) (y * 64 /*0x40*/ -64 /*0x40*/)));
        Rectangle destinationRectangle = new Rectangle((int) local.X, (int) local.Y,64 /*0x40*/, 64 /*0x80*/);
        float layerDepth = Math.Max(0.0f, (float) ((y + 1) * 64 /*0x40*/ - 20) / 10000f) + (float) x * 1E-05f;
        ParsedItemData dataOrErrorItem = ItemRegistry.GetDataOrErrorItem(this.QualifiedItemId);
        spriteBatch.Draw(dataOrErrorItem.GetTexture(), destinationRectangle, new Rectangle?(dataOrErrorItem.GetSourceRect(1, new int?(this.ParentSheetIndex))), Color.White * alpha, 0.0f, Vector2.Zero, SpriteEffects.None, layerDepth);
    }

    private NetVector2 addnetvec2(NetVector2 vector1, NetVector2 vector2)
    {
        float x = vector1.X + vector2.X;
        float y = vector1.Y + vector2.Y;
        return new NetVector2(new Vector2(x, y));
    }

    private void doInserterItemCheck()
    {
        NetVector2 pos = this.tileLocation;
        NetVector2 input = addnetvec2(pos, new NetVector2(this.pickupTileRelPos));
        NetVector2 output = addnetvec2(pos, new NetVector2(this.pickupTileRelPos));

        if (this.Location.objects[new Vector2(input.X, input.Y)] != null)
        {
            if (this.Location.objects[new Vector2(input.X, input.Y)].DisplayName == "Chest")
            {
                Console.WriteLine("wowie Zowie");
            }
        }
    }
}