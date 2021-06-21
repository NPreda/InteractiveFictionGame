using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SwitchToStoryModeCommand : Command
{
    private Storylet storylet;

    public  SwitchToStoryModeCommand(Storylet storylet)
    {        
        this.storylet = storylet;
    }

    public override void StartCommandExecution()
    {
        SceneControl.Instance.DisplayStory(storylet);
        CommandExecutionComplete();
    }

}