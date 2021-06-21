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

    public IEnumerator DelayAction(float delayTime)
    {
    //Wait for the specified delay time before continuing.
    yield return new WaitForSeconds(delayTime);
    
    //Do the action after the delay time has finished.
    }
}
