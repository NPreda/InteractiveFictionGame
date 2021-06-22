using UnityEngine;
using System.Collections;

public class DrawExploreHandCommand : Command
{

    public DrawExploreHandCommand()
    {        

    }

    public override void StartCommandExecution()
    {
        SceneControl.Instance.exploreDisplay.DrawHand();
        CommandExecutionComplete();
    }
}
