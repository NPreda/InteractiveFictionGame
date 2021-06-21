using System;
using UnityEngine;
using EasyButtons;
using TMPro;


//The Player Hand.
public class UiPlayerHand : UiCardPile, IUiPlayerHand
{
    //------------------------------------------------------------------------------------------------------------------
    #region Presets
    public GameObject cardPrefab;   //Prefab needed to generate the card gameObjects
    public GameObject cardPanel;    //location of the cards
    public GameObject SpawnPoint;    //location of the cards
    public GameObject DespawnPoint;    //location of the cards

    //where we show the counts
    [SerializeField]private UiCardCounter deckCounter;
    [SerializeField]private UiCardCounter graveCounter;

    #endregion
    //------------------------------------------------------------------------------------------------------------------
    #region Properties

    // Card currently selected by the player.
    public IUiCard selectedCard { get; private set; }

    event Action<IUiCard> OnCardSelected = card => { };

    event Action<IUiCard> OnCardPlayed = card => { };

    // Event raised when a card is played.
    Action<IUiCard> IUiPlayerHand.OnCardPlayed
    {
        get => OnCardPlayed;
        set => OnCardPlayed = value;
    }

    //Event raised when a card is selected.
    Action<IUiCard> IUiPlayerHand.OnCardSelected
    {
        get => OnCardSelected;
        set => OnCardSelected = value;
    }

    #endregion
    //------------------------------------------------------------------------------------------------------------------
    #region StartMethods

    public override void Awake(){
        base.Awake();
    }

    public GameObject GetPrefab() => cardPrefab;

    #endregion
    //------------------------------------------------------------------------------------------------------------------
    #region Operations

    public void DrawCard(Card card)
    {
        var cardGo = Instantiate(cardPrefab, cardPanel.transform);
        cardGo.transform.position = SpawnPoint.transform.position;

        var cardScript = cardGo.GetComponent<IUiCard>();
        cardScript.Setup(card);
        AddCard(cardScript);
    }

    //Select the card in the parameter.
    public void SelectCard(IUiCard card)
    {
        selectedCard = card ?? throw new ArgumentNullException("Null is not a valid argument.");

        //disable all cards
        DisableCards();
    }

    //Unselect the card in the parameter.
    public void UnselectCard()
    {
        selectedCard = null;
        EnableCards();
    }

    //Unselect the card which is currently selected. Nothing happens if current is null.
    public void Unselect() => UnselectCard();

    //Disables input for all cards.
    public void DisableCards()
    {
        foreach (var otherCard in Cards)
        {
            if (otherCard != selectedCard)
            {
                otherCard.Disable();
            }
        }
    }

    //Enables input for all cards.
    public void EnableCards()
    {
        foreach (var otherCard in Cards)
        {
                otherCard.Idle();
        }
    }

    //this checks if cards have an isDiscardable set to true to discard
    public void DiscardCards()  
    {
        for (int i = Cards.Count - 1; i >= 0; i--)
        {
            var card = Cards[i];
            if(card.isDiscardable)
                DiscardCard(card);
        }
    }

    //Sends all the cards into the graveyard, without checking anything
    public void DiscardAllCards()
    {
        for (int i = Cards.Count - 1; i >= 0; i--)
        {
            var card = Cards[i];
            DiscardCard(card);
        }
    }

    public void DiscardCard(IUiCard card)
    {
            RemoveCard(card);
            card.Discard();
            card.MoveTo(DespawnPoint.GetComponent<RectTransform>().localPosition , 0.4f);        
    }

    public void ExpendCard(IUiCard card)
    {
        if(card.isSingleUse)
        {
            RemoveCard(card);
            card.ExpendCard();
        }else{
            DiscardCard(card);
        }
    }

    public void ExpendSelectedCard()
    {
        ExpendCard(selectedCard);
        EnableCards();    
    } 

    public int ReturnHandCount()
    {
        return Cards.Count;
    }

    public void UpdateCounters(int deckCount, int graveCount)
    {
        if(deckCounter != null) deckCounter.SetCount(deckCount);
        if(graveCounter != null) graveCounter.SetCount(graveCount);
    }

    [Button]
    void NotifyCardSelected() => OnCardSelected?.Invoke(selectedCard);

    #endregion
    //------------------------------------------------------------------------------------------------------------------
}

