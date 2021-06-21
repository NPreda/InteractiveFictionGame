using UnityEngine;
using System.Collections;

public class TriggerLossStateCommand : Command
{

    public  TriggerLossStateCommand()
    {
    }

    public override void StartCommandExecution()
    {
        SceneControl.Instance.combatSystem.LoseState();
        CommandExecutionComplete();
    }

}
