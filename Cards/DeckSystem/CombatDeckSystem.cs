using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class CombatDeckSystem : CardDeckSystem<CombatCard>
{

    public CombatDeckSystem()
    {
        PopulateDeck();
    }

    public override void PopulateDeck()
    {
        base.ClearAll();

        //first we have to get the initial card templates
        List<CombatCard> combatList = CombatCardDB.Instance.returnCards();
        List<Card> returnedTemplates = combatList.Cast<Card>().ToList();

        //then we have to multiply them into the deck basedon their suggested "quantity" property
        foreach(var template in returnedTemplates){
            for (int i = 0; i < template.quantity; i++)
            {
                cardDeck.Add(template);
            }            
        }
        Debug.Log("Cards Loaded with count:" + cardDeck.Count());
    }

    public override List<CombatCard> ReturnHandCards()
    {
        return handCards.Cast<CombatCard>().ToList();
    }

    public override CombatCard DrawAndReturnCard()
    {
        return (CombatCard)DrawCard();
    }
}