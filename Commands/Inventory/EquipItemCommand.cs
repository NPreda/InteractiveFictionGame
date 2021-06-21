
using System.Collections.Generic;

public class EquipItemCommand : Command
{
    EquippableQuality equippable;

    public EquipItemCommand(EquippableQuality equippable)
    {        
        this.equippable = equippable;
    }

    public override void StartCommandExecution()
    {
        Inventory.Instance.EquipItem(equippable);

        foreach(KeyValuePair<RPGStatType, int> modifier in equippable.modifiers)      //go through the modifiers dictionary and add them to the stats
        {
            RPGStatType statType = modifier.Key;
            int value = modifier.Value;
            Character.Instance.statManager.AddModifier(statType, value, equippable);
        }

        CommandExecutionComplete();
    }

}
