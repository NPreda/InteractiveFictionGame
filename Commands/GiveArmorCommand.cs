using UnityEngine;
using System.Collections;

public class GiveArmorCommand : Command
{
    private ArmorInfo armorInfo = new ArmorInfo();

    public GiveArmorCommand(ITarget source, ITarget target, int value)
    {        
        this.armorInfo.source = source;
        this.armorInfo.target = target;
        this.armorInfo.value = value;
    }

    public override void StartCommandExecution()
    {
        this.armorInfo.target.OnArmor(armorInfo);
        CommandExecutionComplete();
    }

}
