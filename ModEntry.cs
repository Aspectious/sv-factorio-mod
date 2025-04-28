using FactoryMod.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Extensions;
using StardewValley.GameData.Objects;
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
            this.AL = new AssetLoader(this, helper.ModContent.Load<Dictionary<String,String>>("assets/dictionary/PathMap.json"));
            
            //Hijack Content loading and inject mod assets
            helper.Events.Content.AssetRequested += this.OnAssetRequested;

            // Patch content into game
            DataPatcher.doPatch(helper);
            
            
            StardewValley.Item testItem = new StardewValley.Object("FactoryMod.testItem", 1);
            helper.Events.Input.ButtonPressed += this.OnButtonPressed;
        }
        private void OnAssetRequested(object sender, AssetRequestedEventArgs e)
        {
            if (e.NameWithoutLocale.IsEquivalentTo("Data/Objects"))
            {
                e.Edit(asset => DataPatcher.PatchObjectData(helper, asset, "assets/Data/Objects.json"));
            }
            if (e.NameWithoutLocale.IsEquivalentTo("Data/CraftingRecipes"))
            {
                e.Edit(asset => DataPatcher.PatchString(helper, asset, "assets/Data/CraftingRecipes.json"));
            }

            if (e.NameWithoutLocale.IsEquivalentTo("Item/testItem"))
            {
                e.LoadFrom(() =>
                {
                    return this.Helper.ModContent.Load<Texture2D>("assets/Item/testItem.png");
                }, AssetLoadPriority.Medium);
            }

        }
        
        /// <summary>Raised after the player presses a button on the keyboard, controller, or mouse.</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event data.</param>
        private void OnButtonPressed(object? sender, ButtonPressedEventArgs e)
        {
            // ignore if player hasn't loaded a save yet
            if (!Context.IsWorldReady)
                return;

            // print button presses to the console window
            this.Monitor.Log($"{Game1.player.Name} pressed {e.Button}.", LogLevel.Debug);
        }
    }
}