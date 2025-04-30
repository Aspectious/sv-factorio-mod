using StardewValley;
using StardewValley.Menus;

namespace FactoryMod.UI;

public class InserterUI : IClickableMenu
{
    public InserterUI()
    {

        this.allClickableComponents = new List<ClickableComponent>();
        this.addComponents();
        base.initialize(150, 150,200,200,true);
        Game1.player.Halt();
    }
    public void addComponents() {
        this.allClickableComponents.Add((ClickableComponent)(IScreenReadable)new OptionsCheckbox("Sus", 1, 50, 50));
    }
}