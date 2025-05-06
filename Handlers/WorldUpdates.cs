using FactoryMod.Objects;
using Microsoft.Xna.Framework;
using Netcode;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using Object = StardewValley.Object;

namespace FactoryMod.Handlers;

/// <summary>
/// This class has all the event handlers needed to handle every change (of tiles) in the world,
/// such as players putting an object down, destroying an object, and more.
/// Also handles assigning the  changes in tile textures, like animations,
/// as well as creation and deletion of entities.
/// </summary>
/// 
public class WorldUpdates
{
    public WorldUpdates(IModHelper ModHelper)
    {
        // Inject our code's handlers into the games' handlers.
        ModHelper.Events.World.ObjectListChanged += this.OnObjectListChanged;
        ModHelper.Events.World.DebrisListChanged += this.OnDebrisListChanged;
    }

    private void OnDebrisListChanged(object sender, DebrisListChangedEventArgs e)
    {
        Console.WriteLine("Debris list changed");

    }

    private int prevInserterKey;
    /// <summary>
    /// This lets us know when a tile/object is placed down in the world, and we can filter
    /// the results from here for just our mod.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnObjectListChanged(object sender, ObjectListChangedEventArgs e)
    {
        Console.WriteLine("Object list changed");
        Console.WriteLine("Added: " + e.Added.Count());
        Console.WriteLine("Sender: " + sender);
        if (e.Added.Count() < 1) return;
        else
        {
            foreach (KeyValuePair<Vector2, Object> obj in e.Added)
            {
                if (obj.Value.itemId == new NetString("FactoryMod.Inserter"))
                {
                    Console.WriteLine(obj.GetType());
                    Console.WriteLine(e.Location.objects[obj.Key].GetType());
                    if (prevInserterKey == 0) {
                        
                        e.Location.objects[obj.Key] = new Inserter(obj.Key);
                        prevInserterKey = 1;
                    }
                    else
                    {
                        prevInserterKey = 0;
                    }
                }
            }
        }
        
        
        /*
         /// DEPRECATED CODE
         
         
        foreach (KeyValuePair<Vector2,Object> obj in e.Added)
        {
            Console.WriteLine("Added object: " + obj.Value.Name);

            /// Replace Item Inserter with Tile Model
            if (obj.Value.itemId == new NetString("FactoryMod.Inserter"))
            {
                e.Location.Objects[obj.Key].
            }
        }

        foreach (KeyValuePair<Vector2, Object> obj in e.Removed)
        {
            Console.WriteLine("Removed object: " + obj.Value.Name + "At " + obj.Key);
            /// Upon Removal of Tile Inserter, Replace tile with Item Version
            if (obj.Value.itemId == new NetString("FactoryMod.InserterTile"))
            {
                Console.WriteLine("woo");
                Console.WriteLine(obj.Key.X + "," + obj.Key.Y + "");
                Debris newdeb = new Debris(ItemRegistry.Create<Object>("FactoryMod.Inserter", 1), obj.Key, obj.Key);
                e.Location.Objects.Remove(obj.Key);
                e.Location.debris.Add(newdeb);
            }
        }
        */
    }

    
}