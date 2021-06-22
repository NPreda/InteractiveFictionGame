using UnityEngine;
using System.Collections;

public class DrawCombatCardCommand : Command
{

    public DrawCombatCardCommand()
    {        

    }

    public override void StartCommandExecution()
    {
        SceneControl.Instance.combatSystem.DrawCardToHand();
        DelayAction(0.5f);
        CommandExecutionComplete();
    }

}
