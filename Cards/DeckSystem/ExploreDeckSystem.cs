using UnityEngine;
using System.Collections.Generic;
using System.Linq;


public class ExploreDeckSystem: CardDeckSystem<ExploreCard>
{
    private ExploreLocation locationType;

    //Refresh Deck at start of exploration
    public void StartDeck(ExploreLocation locationType)
    {
        //update to new location
        this.locationType = locationType;

        //clear the deck and discards
        cardDeck.Clear();
        discards.Clear();

        //clear the hand card with the exception of danger cards
        for(int i = handCards.Count - 1; i >= 0; i--)
        {
            var card = (ExploreCard)handCards[i];
            if(card.type != ExploreType.Danger)
            {
                DiscardCard(card);
            }
        }

        PopulateDeck();
    }

    //Refresh Deck at start of exploration
    public void RefreshDeck()
    {
        //clear the deck and discards
        cardDeck.Clear();
        discards.Clear();

        //clear the hand card with the exception of danger cards
        for(int i = handCards.Count - 1; i >= 0; i--)
        {
            var card = (ExploreCard)handCards[i];
            if(card.type != ExploreType.Danger && card.type != ExploreType.Barrier)
            {
                DiscardCard(card);
            }
        }

        PopulateDeck();
    }

    //Refresh Deck in the middle of exploration

    public override void PopulateDeck()
    {
        //get and cast all the cards
        List<ExploreCard> exploreList = ExploreCardDB.Instance.returnCards(locationType);
        List<Card> returnedTemplates = exploreList.Cast<Card>().ToList();


        //then we have to multiply them into the deck basedon their suggested "quantity" property
        foreach(var template in returnedTemplates){
            for (int i = 0; i < template.quantity; i++)
            {
                cardDeck.Add(template);
            }            
        }

        Debug.Log("Cards Loaded with count:" + cardDeck.Count());
    }

    public override ExploreCard DrawAndReturnCard()
    {
        return (ExploreCard)DrawCard();
    }

    public override List<ExploreCard> ReturnHandCards()
    {
        return handCards.Cast<ExploreCard>().ToList();
    }
}