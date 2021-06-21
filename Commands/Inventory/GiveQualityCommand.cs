using UnityEngine;
using System.Collections;

public class GiveQualityCommand : Command
{
    Quality quality;
    int newValue;

    public GiveQualityCommand(Quality quality, int newValue)
    {        
        this.quality = quality;
        this.newValue = newValue;
    }

    public override void StartCommandExecution()
    {
        Inventory.Instance.AddItem(quality, newValue);
        CommandExecutionComplete();
    }

}
