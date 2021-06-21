using UnityEngine;
using System.Collections;

public class SelectCombatCardCommand : Command
{
    private IUiCard card;

    public  SelectCombatCardCommand(IUiCard card)
    {
        this.card = card;
    }

    public override void StartCommandExecution()
    {
        SceneControl.Instance.combatSystem.playerHand.SelectCard(card);
        CommandExecutionComplete();
    }

}
