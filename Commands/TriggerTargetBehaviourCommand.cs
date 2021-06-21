using UnityEngine;
using System;

public class TriggerTargetBehaviourCommand : Command
{
    private ITarget target;

    public TriggerTargetBehaviourCommand(ITarget target)
    {        
        this.target = target;
    }

    public override void StartCommandExecution()
    {
        UiCombatCard selectedCard = SceneControl.Instance.combatSystem.GetSelectedCard();
        if (selectedCard!= null)
        {
            CombatCard card = (CombatCard) selectedCard.card;
            if (selectedCard.IsArrowing && card.targetType == TargetType.Friend)
            {
                if(target.allegiance == TargetType.Friend || target.allegiance == TargetType.Player)    
                    target.Targeted();
            }else if(selectedCard.IsArrowing && card.targetType == target.allegiance)  
            {
                target.Targeted();        
            }
        }
        CommandExecutionComplete();
    }

}
