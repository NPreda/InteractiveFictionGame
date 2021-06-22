using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayStoryTriggerCommand : Command
{
    StoryTrigger storyTrigger;
    
    public  PlayStoryTriggerCommand(StoryTrigger storyTrigger)
    {        
        this.storyTrigger = storyTrigger;
    }

    public override void StartCommandExecution()
    {
        if(storyTrigger is ExploreTrigger)
        {
            new SwitchToExploreModeCommand().AddToQueue();
        }
        else if (storyTrigger is CombatTrigger)
        {
            new SwitchToCombatModeCommand((CombatTrigger) storyTrigger).AddToQueue();
        }
        else if (storyTrigger is ConcludeTrigger)
        {
            new ConcludeTraveletCommand().AddToQueue();
        }
        else if (storyTrigger is Storylet)
        {
            new PlayStoryletCommand((Storylet)storyTrigger).AddToQueue();
        }
        else
        {
            throw new System.Exception("Error: StoryTrigger being loaded is not of a parssable type: " + storyTrigger?.GetType());            
        }

        CommandExecutionComplete();
    }

}