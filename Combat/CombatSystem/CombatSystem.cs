
using System.Collections.Generic;
using UnityEngine;
using Tools.UI;
using Patterns.StateMachine;
using System.Linq;

public class CombatSystem : TweeningMover, IStateMachineHandler
{
    public string Name => gameObject.name;

    //internal elements
    public List<Unit> units;

    public List<GameObject> unitObjects;         //reference to the scripts of all the unit objects

    //Preset positions to be populated by the Battle Setup
    public UnitFactory enemyFactory;

    //Various buttons
    public UIPressButton turnButton;
    public UIPressButton fleeButton;

    //the hand system
    public int handSize;
    [SerializeField] public UiPlayerHand playerHand;
    public CombatDeckSystem  combatDeck;

    //combat logging and description systems
    [SerializeField] private TextLogControl logControl;
    [SerializeField] private InfoPanelController unitInfoPanel;

    //defautl storylets for the win and loss states
    public Storylet defaultWin;
    public Storylet defaultLoss;
    public Storylet fleeStorylet;

    //FSM and Blackboard
    CombatSystemFsm Fsm;

    //functional variablws
    private bool _isDirty = false;

    //------------------------------------------------------------------------------------------------------------------------------------------
    #region ActivationControls

    public void Update()
    {
        if(_isDirty) RefreshUI();
        Fsm?.Update();
    }

    public void Setup()
    {
        Fsm = new CombatSystemFsm(this);
        combatDeck = new CombatDeckSystem();
        IdleState();
    }

    public void Enable(CombatTrigger ct){
        base.Enable();

        RegisterButtons();
        defaultWin = ct.winResult;
        defaultLoss = ct.loseResult;
        fleeStorylet = ct.fleeResult;

        units = ct.units;

        StartState();
    }

    public override void Disable()
    {
        base.Disable();
        CombatBlackboard.ClearEnemies();
        DiscardHand();
        IdleState();
        UnregisterButtons();
    }

    private void Dirty() => _isDirty = true;
    #endregion
    //------------------------------------------------------------------------------------------------------------------------------------------
    #region HandControl

    public void DrawHand()        //draws cards for this turn
    {
        for (int i = 0; i < handSize; i++)
        {
            new DrawCombatCardCommand().AddToQueue();
        }
        Dirty();
    }

    public void DiscardHand()   //discards all remaining cards into the pile
    {
        combatDeck.DiscardHand();
        playerHand.DiscardAllCards();
        Dirty();
    }

    #endregion
    //------------------------------------------------------------------------------------------------------------------------------------------
    #region CardControl

    public void DrawCardToHand()    //draws a card from the deck and adds it to the hand
    {
        CombatCard newCard = combatDeck.DrawAndReturnCard();
        playerHand.DrawCard(newCard);
        Dirty();
    }

    public void AddCardToHand(CombatCard newCard)   //spawns a non-deck card directly to the hand
    {
        combatDeck.AddToHand(newCard);
        playerHand.DrawCard(newCard);
        Dirty();
    }

    public void DiscardCard(IUiCard card)       //Discards a specific card into your discards pile
    {
        combatDeck.DiscardCard((CombatCard)card.returnCard());
        playerHand.DiscardCard(card);
        Dirty();
    }
    #endregion
    //------------------------------------------------------------------------------------------------------------------------------------------
    #region SelectedCard
    public void SelectCard(IUiCard card)    //selects a card, play the animation for it
    {
        playerHand.SelectCard(card);
        Dirty();
    }

    public void UnselectCard()          //deselect a card, unplays the animation
    {
        playerHand.UnselectCard();
        Dirty();
    }

    public UiCombatCard GetSelectedCard(){      //get the card currently selected by the player
        return (UiCombatCard)playerHand.selectedCard;
    }

    public void ExpendSelectedCard()    //properly remove the selected card after play
    {
        IUiCard uiCard = GetSelectedCard();    //we get the UI part of the card currently selected
        CombatCard combatCard = (CombatCard)uiCard.returnCard(); //we get the actual logic of the card
        combatDeck.ExpendCard(combatCard);
        playerHand.ExpendSelectedCard();
        Dirty();
    }

    #endregion
    //------------------------------------------------------------------------------------------------------------------------------------------

    public void RegisterButtons(){
        turnButton.OnClickEvent += FinishPlayerTurn;
        fleeButton.OnClickEvent += SpawnText;
    }

    public void UnregisterButtons(){
        turnButton.OnClickEvent -= FinishPlayerTurn;
        fleeButton.OnClickEvent -= SpawnText;

    }

    public void RefreshUI()
    {
        playerHand.UpdateCounters(combatDeck.ReturnDeckCount(), combatDeck.ReturnDiscardCount());
    }

    public void FinishPlayerTurn(){
        CompanionState();
    }

    public void FinishPlayerTurn(UIButton button){
        CompanionState();
    }

    public void RollBark()
    {
        int randomNumber = UnityEngine.Random.Range(0, 100);
        if (randomNumber < 5)
        {
            List<ITarget> targets = CombatBlackboard.AllTargets();
            int index = UnityEngine.Random.Range(0, targets.Count);
            targets[index].Bark();
        }
    }

    public void SortEnemies()
    {
        List<IUiUnit> unorderedList = CombatBlackboard.Enemies.Cast<IUiUnit>().ToList();
        List<IUiUnit> countOrdered = unorderedList.OrderBy(go => go.strength).ToList();

        for (int i = 0; i < countOrdered.Count; i++)
        {
            countOrdered[i].gameObject.transform.SetSiblingIndex(i);
        }
    }

    //--------------------------------------------------------------------------------------------------------------------------
    #region  LogDescControl

    public void AppendToLog(string entry)
    {
        logControl.LogText(entry);
    }

    public void ShowDescription(IUiUnit unit)
    {
        UiCombatCard selectedCard = SceneControl.Instance.combatSystem.GetSelectedCard();
        if (selectedCard == null)
        {
            logControl.gameObject.SetActive(false);
            unitInfoPanel.Load(unit);
            unitInfoPanel.ShowPanel();
        }
    }

    public void HideDescription()
    {
        unitInfoPanel.HidePanel();
        logControl.gameObject.SetActive(true);
    }

    public void SpawnText(UIButton button)
    {
        EscapeState();
    }

    #endregion
    //------------------------------------------------------------------------------------------------------------
    #region FSMStates

    public bool IsStarting => Fsm.IsCurrent<CombatStartState>();
    public bool IsPlayerState => Fsm.IsCurrent<PlayerTurnState>();
    public bool isEnemyState => Fsm.IsCurrent<EnemyTurnState>();
    public bool isCompanionState => Fsm.IsCurrent<CompanionTurnState>();
    public bool isEscapeState => Fsm.IsCurrent<AttemptEscapeState>();
    public bool isWinState => Fsm.IsCurrent<VictoryState>();
    public bool isLoseState => Fsm.IsCurrent<LossState>();
    public bool isIdleState => Fsm.IsCurrent<IdleState>();

    #endregion
    //------------------------------------------------------------------------------------------------------------

    #region FSMOperations

        public void StartState() => Fsm.StartState();
        public void PlayerState() => Fsm.PlayerState();
        public void EnemyState() => Fsm.EnemyState();
        public void CompanionState() => Fsm.CompanionState();
        public void EscapeState() => Fsm.EscapeState();
        public void WinState() => Fsm.WinState();
        public void LoseState() => Fsm.LossState();
        public void IdleState() => Fsm.GoToIdleState();

    #endregion
    //--------------------------------------------------------------------------------------------------------------    
}
