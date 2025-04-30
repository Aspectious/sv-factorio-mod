using System.Diagnostics;
using FactoryMod.Handlers;
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
        public WorldUpdates U_WORLD;
        public InputUpdates U_INPUT;
        
        /// <summary>
        /// Apparently it's the entry point idk dont look at me man
        /// </summary>
        /// <param name="helper">Provides the simplified API for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            this.helper = helper;
            this.AL = new AssetLoader(this, helper.ModContent.Load<Dictionary<String,String>>("assets/dictionary/TextureMap.json"));
            this.U_WORLD = new WorldUpdates(helper);
            this.U_INPUT = new InputUpdates(helper);
            //Hijack Content loading and inject mod assets
            helper.Events.Content.AssetRequested += this.OnAssetRequested;

            //helper.Events.Input.CursorMoved += this.OnCursorMoved;
            // Patch content into game
            DataPatcher.doPatch(helper);
            
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