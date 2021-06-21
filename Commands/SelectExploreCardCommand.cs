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
        new OpenExploreStoryletCommand(card.card.storylet).AddToQueue();
        SceneControl.Instance.exploreDisplay.ExpendCard(card);
        CommandExecutionComplete();
    }

}
