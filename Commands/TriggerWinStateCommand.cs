using UnityEngine;
using System.Collections;

public class TriggerWinStateCommand : Command
{

    public  TriggerWinStateCommand()
    {
    }

    public override void StartCommandExecution()
    {
        SceneControl.Instance.combatSystem.WinState();
        CommandExecutionComplete();
    }

}
