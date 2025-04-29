using System.Diagnostics;
using FactoryMod.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Extensions;
using StardewValley.GameData.BigCraftables;
using StardewValley.GameData.Objects;
using xTile.Dimensions;
using Object = StardewValley.Object;

namespace FactoryMod
{
    internal sealed class ModEntry : Mod
    {
        public IModHelper helper;
        public AssetLoader AL;
        
        /// <summary>
        /// Apparently it's the entry point idk dont look at me man
        /// </summary>
        /// <param name="helper">Provides the simplified API for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            this.helper = helper;
            this.AL = new AssetLoader(this, helper.ModContent.Load<Dictionary<String,String>>("assets/dictionary/TextureMap.json"));
            
            //Hijack Content loading and inject mod assets
            helper.Events.Content.AssetRequested += this.OnAssetRequested;

            // Hijact Placement and Destruction of objects
            helper.Events.World.ObjectListChanged += this.OnObjectListChanged;
            helper.Events.Input.ButtonReleased += this.OnButtonReleased;
            //helper.Events.Input.CursorMoved += this.OnCursorMoved;
            // Patch content into game
            DataPatcher.doPatch(helper);
            
        }

        private void OnButtonReleased(object sender, ButtonReleasedEventArgs e)
        {
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
                    }
                }
            }
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
                        if (cursorInGrabItem) return;
                        else
                        {
                            cursorInGrabItem = true;
                            Game1.mouseCursor = Game1.cursor_grab;
                        }
                    }
                } else if (Game1.currentLocation.Objects.ContainsKey(placewheremousewas))
                {
                    Object obj = Game1.currentLocation.Objects[placewheremousewas];
                    if (obj.ItemId == "FactoryMod.Inserter")
                    {
                        if (!cursorInGrabItem) return;
                        else {
                            cursorInGrabItem = false;
                            Game1.mouseCursor = Game1.cursor_default;
                        }
                    }
                }
                else return;
            }
        }
        */

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
        
        
        /// <summary>
        /// Injects the game's assets with our mod's changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnAssetRequested(object sender, AssetRequestedEventArgs e)
        {
            if (e.NameWithoutLocale.IsEquivalentTo("Data/Objects"))
            {
                e.Edit(asset => DataPatcher.Patch<ObjectData>(helper, asset, "assets/Data/Objects.json"));
            }
            if (e.NameWithoutLocale.IsEquivalentTo("Data/BigCraftables"))
            {
                e.Edit(asset => DataPatcher.Patch<BigCraftableData>(helper, asset, "assets/Data/BigCraftables.json"));
            }
            if (e.NameWithoutLocale.IsEquivalentTo("Data/CraftingRecipes"))
            {
                e.Edit(asset => DataPatcher.Patch<String>(helper, asset, "assets/Data/CraftingRecipes.json"));
            }
            
            foreach (KeyValuePair<String, String> entry in this.AL.AssetMap)
            {
                if (e.NameWithoutLocale.IsEquivalentTo(entry.Key))
                {
                    e.LoadFrom(() =>
                    {
                        return this.Helper.ModContent.Load<Texture2D>(entry.Value);
                    }, AssetLoadPriority.Medium);
                }
            }
        }
    }
}