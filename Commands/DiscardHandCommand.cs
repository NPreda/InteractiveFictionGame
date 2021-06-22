using UnityEngine;
using System.Collections;

public class DiscardHandCommand : Command
{
    public  DiscardHandCommand()
    {}

    public override void StartCommandExecution()
    {
        SceneControl.Instance.combatSystem.DiscardHand();
        DelayAction(0.5f);
        CommandExecutionComplete();
    }

}
