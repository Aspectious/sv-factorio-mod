using FactoryMod.Objects;
using Netcode;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;

namespace FactoryMod.Handlers;

public class InventoryUpdates
{
    IModHelper helper;
    public InventoryUpdates(IModHelper helper)
    {
        this.helper = helper;
        helper.Events.Player.InventoryChanged += onInventoryChanged;
    }

    private void onInventoryChanged(object sender, InventoryChangedEventArgs e)
    {
        foreach (StardewValley.Item item in e.Added)
        {

            Console.WriteLine("" + item.GetType());
            if (item.itemId == new NetString("FactoryMod.Inserter"))
            {

                if (item.GetType() == typeof(StardewValley.Object))
                {
                    e.Player.removeItemFromInventory(item);
                    e.Player.addItemToInventory(new Inserter(item.Stack));
                }
            }
        }
    }
}