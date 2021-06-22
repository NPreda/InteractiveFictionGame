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

}
