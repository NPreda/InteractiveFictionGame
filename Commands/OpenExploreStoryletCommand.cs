using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OpenExploreStoryletCommand : Command
{
    private Storylet storylet;

    public OpenExploreStoryletCommand(Storylet storylet)
    {        
        this.storylet = storylet;
    }

    public override void StartCommandExecution()
    {
        //We assume this will always return to a proper storylet screen. Otherwise we are doomed.
        SceneControl.Instance.exploreDisplay.storyDisplay.Enable(storylet);
        CommandExecutionComplete();
    }

}