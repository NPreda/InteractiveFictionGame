using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public abstract class CardDeckSystem<T> 
{

    //------------------------------------------------------------------------------------------------------------------
    #region Properties


    protected List<Card> cardDeck = new List<Card>();
    protected List<Card> handCards = new List<Card>();
    protected List<Card> discards = new List<Card>();

    #endregion
    //------------------------------------------------------------------------------------------------------------------

    public CardDeckSystem()
    {
        PopulateDeck();
    }

    public abstract void PopulateDeck();

    public abstract List<T> ReturnHandCards();
    
    public abstract T DrawAndReturnCard();

    public int ReturnHandCount() => handCards.Count();

    public int ReturnDeckCount() => cardDeck.Count();

    public int ReturnDiscardCount() => discards.Count();

    public void ClearAll(){
        cardDeck.Clear();
        handCards.Clear();
        discards.Clear();
    }


    protected Card DrawCard()
    {
        if(cardDeck.Count() == 0){  //refreh deck if empty
            ReturnDiscards();
        }
        
        int index = UnityEngine.Random.Range(0, cardDeck.Count()-1);
        Card temp = cardDeck[index];
        cardDeck.Remove(temp);
        handCards.Add(temp); 

        return temp; //return latest card added to hand
    }

    public void RemoveCard(Card card)
    {
        handCards.Remove(card);
    }

    public void DiscardCard(Card card)
    {
        discards.Add(card);
        handCards.Remove(card);
    }

    public void ExpendCard(Card card)
    {
        if (card.isSingleUse)   RemoveCard(card);
        else    DiscardCard(card);
    }

    public void DiscardHand()
    {
        discards = discards.Concat(handCards).ToList();
        handCards.Clear();
    }

    public void ReturnDiscards(){
        cardDeck = discards.ToList();
        discards.Clear();
    }

    public void AddToHand(Card card) => handCards.Add(card);

    public void AddToDiscards(Card card) => discards.Add(card);

}
