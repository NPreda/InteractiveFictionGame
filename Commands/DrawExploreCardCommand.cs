using UnityEngine;
using System.Collections;

public class DrawExploreCardCommand : Command
{

    public DrawExploreCardCommand()
    {        

    }

    public override void StartCommandExecution()
    {
        SceneControl.Instance.exploreDisplay.DrawCardToHand();
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
