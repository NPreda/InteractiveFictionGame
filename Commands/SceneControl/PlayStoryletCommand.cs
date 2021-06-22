using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayStoryletCommand : Command
{
    Storylet storylet;
    
    public PlayStoryletCommand(Storylet storylet)
    {        
        this.storylet = storylet;
    }

    public override void StartCommandExecution()
    {
        StoryletCache.StoreStorylet(storylet);   //let's put it in the cache

        new UpturnFateCommand().AddToQueue();   //let's also spin the dice of fate to give new results

        if(storylet.storyletType == StoryletType.Storylet)
            new SwitchToStoryModeCommand().AddToQueue();
        else if(storylet.storyletType == StoryletType.Travelet)
        {
            new SwitchToExploreModeCommand().AddToQueue();
            new OpenTraveletDisplayCommand().AddToQueue();
        }

        CommandExecutionComplete();
    }

}