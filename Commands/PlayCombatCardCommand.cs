using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayCombatCardCommand : Command
{
    private ITarget attacker = CombatBlackboard.Player;     
    private List<ITarget> targets;
    private IUiCard uiCard;

    public PlayCombatCardCommand(ITarget target, IUiCard uiCard)
    {   
        this.targets =  new List<ITarget>(1) {target} ;
        this.uiCard = uiCard;
    }

    public PlayCombatCardCommand(List<ITarget> targets, IUiCard uiCard)
    {   
        this.targets =  targets;
        this.uiCard = uiCard;
    }

    public PlayCombatCardCommand(TargetType targetType, IUiCard uiCard)
    {
        if(targetType == TargetType.Foe)
        {
            targets = CombatBlackboard.Enemies;
        }else if(targetType == TargetType.Friend)
        {
            targets = CombatBlackboard.Enemies;
        }else if(targetType == TargetType.Player)
        {
            targets = new List<ITarget>(1) {CombatBlackboard.Player};
        }   
        this.uiCard = uiCard;
    }

    public override void StartCommandExecution()
    {
        CombatCard card = (CombatCard) uiCard.returnCard();
        if(CheckEnergy(card.energyCost))  //if there is sufficient energy in the bank, to the following
        {
            new SpendEnergyCommand(card.energyCost).AddToQueue();

            new ExpendCombatCardCommand().AddToQueue();
            new RunCombatEffectsCommand(this.attacker, this.targets, card.cardEffect).AddToQueue();
        

        }else{
            new MakeCombatCardIdleCommand(this.uiCard);
        }
        CommandExecutionComplete();
    }

    //What happens when you play a card
    private bool CheckEnergy(int cardCost)
    {
        int currentEnergy = Character.Instance.ReturnStatPacket("Energy").value;
        if (cardCost > currentEnergy || cardCost < 0){
            return false;
        } else {
            return true;
        }
    }

}
