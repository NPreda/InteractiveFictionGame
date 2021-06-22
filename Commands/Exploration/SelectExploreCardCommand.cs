using UnityEngine;
using System.Collections;

public class SelectExploreCardCommand : Command
{
    private UiExploreCard card;

    public SelectExploreCardCommand(UiExploreCard card)
    {
        this.card = card;
    }

    public override void StartCommandExecution()
    {
        StoryletCache.StoreStorylet(card.card.storylet);

        new OpenTraveletDisplayCommand().AddToQueue();
        SceneControl.Instance.exploreDisplay.ExpendCard(card);
        CommandExecutionComplete();
    }

}
