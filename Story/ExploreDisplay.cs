using Tools.UI;
using UnityEngine;

public class ExploreDisplay : TweeningMover
{
    public UIButton deckButton;
    public GameObject cardPrefab;
    public StoryDisplay storyDisplay;

    //-----------------------------------------------------------------------------------------------------------------
    #region CardSystemProperties

    public int deckCost = 5;
    public int handSize = 3;
    private ExploreDeckSystem exploreDeck;
    [SerializeField] private UiPlayerHand playerHand;

    #endregion
    //-----------------------------------------------------------------------------------------------------------------

    
    //Object to process information of screen data
    ExpressionParser exParser = new ExpressionParser();

    //-----------------------------------------------------------------------------------------------------------------
    #region ActivationControls

    public void Setup(){
        exploreDeck = new ExploreDeckSystem();
    }

    public override void Enable()
    {
        base.Enable();
        exploreDeck.StartDeck(ExploreLocation.Test);
        RefreshHandUI();    //refresh to show "permanent" cards that stick after closing
        new DrawExploreHandCommand().AddToQueue();
    }

    public override void Disable()
    {
        base.Disable();
        playerHand.DiscardAllCards();        //get rid of the UI cards
        storyDisplay.Disable();
    }

    #endregion
    //-----------------------------------------------------------------------------------------------------------------
    #region HandControl

    //update the UI cards to match the system cards
    public void RefreshHandUI()
    {
        var handCards = exploreDeck.ReturnHandCards();
        foreach(var card in handCards)
        {
            playerHand.DrawCard(card);
        }
    }

    public void DrawHand()        //draws cards for this turn
    {
        exploreDeck.RefreshDeck();
        playerHand.DiscardCards();

        int currentCards = exploreDeck.ReturnHandCount();
        int currentUICards = playerHand.ReturnHandCount();
        if(currentCards != currentUICards)
        {
            throw new System.Exception("There is a missmatch between the deck system and the UI. Deck contains " + currentCards + " vs. the UI's " + currentUICards);
        }

        for (int i = currentCards; i < handSize; i++)
        {
            new DrawExploreCardCommand().AddToQueue();
        }

    }

    #endregion
    //-----------------------------------------------------------------------------------------------------------------
    #region CardControl

    public void ExpendCard(UiCard uiCard)    //properly remove the selected card after play
    {
        ExploreCard exploreCard = (ExploreCard)uiCard.returnCard(); //we get the actual logic of the card
        exploreDeck.ExpendCard(exploreCard);
        playerHand.ExpendCard(uiCard);
    }

    public void DrawCardToHand()    //draws a card from the deck and adds it to the hand
    {
        ExploreCard newCard = exploreDeck.DrawAndReturnCard();
        playerHand.DrawCard(newCard);
    }

    public void AddCardToHand(ExploreCard newCard)   //spawns a non-deck card directly to the hand
    {
        exploreDeck.AddToHand(newCard);
        playerHand.DrawCard(newCard);
    }

    public void DiscardCard(IUiCard card)       //Discards a specific card into your discards pile
    {
        exploreDeck.DiscardCard(card.returnCard());
        playerHand.DiscardCard(card);
    }

    #endregion
    //-----------------------------------------------------------------------------------------------------------------

}