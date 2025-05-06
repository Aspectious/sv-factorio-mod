using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using Object = StardewValley.Object;

namespace FactoryMod.Handlers;

/// <summary>
/// This class handles all the events related to inputs, such as users hovering over an item,
/// clicking on a certain object or building, as well as any keyboard presses that need to be
/// assigned to a function.
/// </summary>
public class InputUpdates
{
    public InputUpdates(IModHelper ModHelper)
    {
        // Inject our code's handlers into the games' handlers.
        ModHelper.Events.Input.ButtonReleased += this.OnButtonReleased;
    }

    /// <summary>
    /// This currently is setup to handle clicking on an inserter, bringing up an empty popup menu.
    /// TODO: Look up reference material in decompiled libraries under StardewValley.Menu
    /// Especially IClickableMenu and Options Classes
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnButtonReleased(object sender, ButtonReleasedEventArgs e)
    {
        /*
        if (e.Button != SButton.MouseRight) return;
        Vector2 TilePosition = e.Cursor.Tile;
        if (Game1.currentLocation == null) return;
        if (Game1.currentLocation.Objects == null) return;
        if (Game1.currentLocation.Objects.Length > 0)
        {
            if (Game1.currentLocation.Objects.ContainsKey(TilePosition))
            {
                Object obj = Game1.currentLocation.Objects[TilePosition];
                if (obj.ItemId == "FactoryMod.Inserter")
                {
                    Console.WriteLine("Inserter Clicked On!");
                    Util.ModUIHandler.ShowUI(new UI.InserterUI());
                }
            }
        }
        */
    }
    
    
    /*

     TODO: Find a more efficient way to do this
     
    private bool cursorInGrabItem = false;
    private void OnCursorMoved(object sender, CursorMovedEventArgs e)
    {
        Vector2 placewheremouseis = e.NewPosition.Tile;
        Vector2 placewheremousewas = e.OldPosition.Tile;

        if (Game1.currentLocation == null) return;
        if (Game1.currentLocation.Objects == null) return;
        if (Game1.currentLocation.Objects.Length > 0)
        {
            if (Game1.currentLocation.Objects.ContainsKey(placewheremouseis))
            {
                Object obj = Game1.currentLocation.Objects[placewheremouseis];
                if (obj.ItemId == "FactoryMod.Inserter")
                {
                        cursorInGrabItem = true;
                        Game1.mouseCursor = Game1.cursor_grab;
                }
            }
        }
    }
    */
}