using UnityEngine;
using System.Collections;

public class UnequipItemCommand : Command
{
    EquippableQuality equippable;

    public UnequipItemCommand(EquippableQuality equippable)
    {        
        this.equippable = equippable;
    }

    public override void StartCommandExecution()
    {
        Inventory.Instance.UnequipItem(equippable);
        Character.Instance.statManager.RemoveAllModifiersFromSource(equippable);

        CommandExecutionComplete();
    }

}
