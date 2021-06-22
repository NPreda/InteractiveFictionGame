using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CloseTraveletDisplayCommand : Command
{
    public CloseTraveletDisplayCommand()
    {        
    }

    public override void StartCommandExecution()
    {
        SceneControl.Instance.exploreDisplay.storyDisplay.Disable();
        CommandExecutionComplete();
    }

}