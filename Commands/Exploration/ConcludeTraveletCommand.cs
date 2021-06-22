using UnityEngine;
using System.Collections;
using System;

public class ConcludeTraveletCommand : Command
{

    public ConcludeTraveletCommand()
    {        

    }

    public override void StartCommandExecution()
    {
        if(SceneControl.Instance.sceneState == SceneState.Explore) 
        {
            new CloseTraveletDisplayCommand().AddToQueue();
            new DrawExploreHandCommand().AddToQueue();
        }
        else
        {
            throw new Exception("ERROR: A travelet attempted to close while outside the Exploration Scene");
        } 
        CommandExecutionComplete();
    }

}
