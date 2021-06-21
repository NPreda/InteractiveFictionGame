using UnityEngine;
using System.Collections;

public class UnselectCardCommand : Command
{

    public  UnselectCardCommand()
    {
    }

    public override void StartCommandExecution()
    {
        SceneControl.Instance.combatSystem.UnselectCard();
        CommandExecutionComplete();
    }

}
