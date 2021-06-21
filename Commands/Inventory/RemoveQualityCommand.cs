using UnityEngine;
using System.Collections;

public class RemoveQualityCommand : Command
{
    Quality quality;

    public RemoveQualityCommand(Quality quality)
    {        
        this.quality = quality;
    }

    public override void StartCommandExecution()
    {
        Inventory.Instance.RemoveItem(quality);
        CommandExecutionComplete();
    }

}
