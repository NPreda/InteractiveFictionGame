using UnityEngine;
using System.Collections;

public class FinishPlayerTurnCommand : Command
{

    public FinishPlayerTurnCommand()
    {        

    }

    public override void StartCommandExecution()
    {
        SceneControl.Instance.combatSystem.FinishPlayerTurn();
    }

}
