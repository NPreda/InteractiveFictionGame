using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OpenTraveletDisplayCommand : Command
{

    public OpenTraveletDisplayCommand()
    {        
    }

    public override void StartCommandExecution()
    {
        //We assume this will always return to a proper storylet screen. Otherwise we are doomed.
        SceneControl.Instance.exploreDisplay.storyDisplay.Enable();
        CommandExecutionComplete();
    }

}