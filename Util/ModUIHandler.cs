using StardewValley;
using StardewValley.Menus;

namespace FactoryMod.Util;

public class ModUIHandler
{
    public static void ShowUI(IClickableMenu menu)
    {
        Game1.activeClickableMenu = menu;
    }
}