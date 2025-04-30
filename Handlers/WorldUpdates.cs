using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
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
    }
    
    /// <summary>
    /// This lets us know when a tile/object is placed down in the world, and we can filter
    /// the results from here for just our mod.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnObjectListChanged(object sender, ObjectListChangedEventArgs e)
    {
        Console.WriteLine("Object list changed");
        foreach (KeyValuePair<Vector2,Object> obj in e.Added)
        {
            Console.WriteLine("Added object: " + obj.Value.Name);
        }
    }

}