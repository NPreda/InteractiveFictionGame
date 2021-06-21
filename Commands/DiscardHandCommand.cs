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

    public IEnumerator DelayAction(float delayTime)
    {
    //Wait for the specified delay time before continuing.
    yield return new WaitForSeconds(delayTime);
    
    //Do the action after the delay time has finished.
    }
}
