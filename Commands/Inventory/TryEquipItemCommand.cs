using UnityEngine;
using System.Collections;

public class TryEquipItemCommand : Command
{
    EquippableQuality equippable;

    public TryEquipItemCommand(EquippableQuality equippable)
    {        
        this.equippable = equippable;
    }

    public override void StartCommandExecution()
    {
        EquippableQuality currentlyEquipped = Inventory.Instance.ReturnEquippedType(equippable.equipmentType);
        
        if(currentlyEquipped == null || currentlyEquipped.isBonded == false)      
        {
            new EquipItemCommand(equippable).AddToQueue();
        }else{
            new UnequipItemCommand(currentlyEquipped).AddToQueue();
            new EquipItemCommand(equippable).AddToQueue();
        }  

        CommandExecutionComplete();
    }

}
