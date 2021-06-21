using UnityEngine;
using System.Collections;

public class ForceUnequipCommand : Command
{
    EquippableQuality equippable;

    public ForceUnequipCommand(EquippableQuality equippable)
    {        
        this.equippable = equippable;
    }

    public override void StartCommandExecution()
    {
        Character.Instance.statManager.RemoveAllModifiersFromSource(equippable);
        Inventory.Instance.ForceUnequipItem(equippable);
        CommandExecutionComplete();
    }

}
