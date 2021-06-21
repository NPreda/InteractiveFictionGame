using Tools.UI;
using UnityEngine;

public class ExploreDisplay : TweeningMover
{
    public UIChoiceButton backButton;
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
        deckButton.OnClickEvent += OnDeckClick;
        backButton.OnClickEvent += OnBackClick;
    }

    public override void Enable()
    {
        base.Enable();
        exploreDeck.StartDeck(ExploreLocation.Test);
        RefreshHandUI();
    }

    public override void Disable()
    {
        base.Disable();
        playerHand.DiscardAllCards();        //get rid of the UI cards
        storyDisplay.Disable();
    }

    #endregion
    //-----------------------------------------------------------------------------------------------------------------
    #region Supply Check Logic

    private bool SupplyCheck(int supplyCost){        //check if you have enough supplies to do anything
        string targetQuality = "supply";
        int targetValue = supplyCost;
        int currentValue = exParser.ReturnValue(targetQuality);
        bool result = false;
        if(targetValue <= currentValue){
            result = true;
        }else{
            result = false;
        }
        return result;
    }

    private void SupplyCharge(int supplyCost){
        string targetQuality = "supply";
        int currentValue = exParser.ReturnValue(targetQuality);
        int targetValue = currentValue - supplyCost;
        exParser.GiveValue(targetQuality, targetValue);
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

    public bool DrawHand()        //draws cards for this turn
    {
        bool drawFlag = false;

        int currentCards = exploreDeck.ReturnHandCount();
        int currentUICards = playerHand.ReturnHandCount();
        if(currentCards != currentUICards)
        {
            throw new System.Exception("There is a missmatch between the deck system and the UI. Deck contains " + currentCards + " vs. the UI's " + currentUICards);
        }

        for (int i = currentCards; i < handSize; i++)
        {
            new DrawExploreCardCommand().AddToQueue();
            drawFlag = true;
        }

        return drawFlag;
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

    #region UiInteractions

    public void OnBackClick(UIChoiceButton button)
    {
        if(storyDisplay.isActive is false){
            //We assume this will always return to a proper storylet screen. Otherwise we are doomed.
            Storylet st = (Storylet) button.choice.defaultResult.goTo;
            new SwitchToStoryModeCommand(st).AddToQueue();
        }
    }

    public void OnDeckClick(UIButton button)
    {
        if((storyDisplay.isActive is false) && SupplyCheck(deckCost)){
            exploreDeck.RefreshDeck();
            playerHand.DiscardCards();
            if(DrawHand())
                SupplyCharge(deckCost);
        }
    }
    #endregion
    //-----------------------------------------------------------------------------------------------------------------

}
