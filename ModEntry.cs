using FactoryMod.Item;
using FactoryMod.Util;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
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
            
            helper.Events.Content.AssetRequested += this.OnAssetRequested;
            
            StardewValley.Item testItem = new StardewValley.Object("testItem", 1);
            helper.Events.Input.ButtonPressed += this.OnButtonPressed;
        }
        private void OnAssetRequested(object sender, AssetRequestedEventArgs e)
        {
            foreach (KeyValuePair<String, String> pair in AL.pathMap)
            {
                if (e.NameWithoutLocale.IsEquivalentTo(pair.Key))
                {
                    e.Edit(asset =>
                    {
                        if (pair.Value.EndsWith(".png"))
                        {
                            var editor = asset.AsImage();
                            IRawTextureData sourceImage = this.Helper.ModContent.Load<IRawTextureData>(pair.Value);
                            editor.PatchImage(sourceImage, targetArea: new Rectangle(300, 100, 200, 200));
                        } else if (pair.Value.EndsWith(".json"))
                        {
                            var editor = asset.AsDictionary<string, string>();
                            editor.Data[pair.Key] = pair.Value;
                        }

                    });
                }
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