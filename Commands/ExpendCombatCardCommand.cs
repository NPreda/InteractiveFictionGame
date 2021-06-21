using UnityEngine;
using System.Collections;

public class ExpendCombatCardCommand : Command
{

    public  ExpendCombatCardCommand()
    {
    }

    public override void StartCommandExecution()
    {
        SceneControl.Instance.combatSystem.ExpendSelectedCard();
        CommandExecutionComplete();
    }

}
