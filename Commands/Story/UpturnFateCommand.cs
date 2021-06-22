using UnityEngine;
using System.Collections;

public class UpturnFateCommand : Command
{

    public UpturnFateCommand()
    {        
    }

    public override void StartCommandExecution()
    {
        int newValue = UnityEngine.Random.Range(0, 100);
        Inventory.Instance.AddItem("twist_of_fate", newValue);
        CommandExecutionComplete();
    }

}
