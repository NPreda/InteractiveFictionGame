using UnityEngine;

public class PlayerTurnState : BaseCombatSystemState
{


    //--------------------------------------------------------------------------------------------------------------

    #region Constructor

    public PlayerTurnState(CombatSystem handler)  : base(handler)
    {
    }

    #endregion

    //--------------------------------------------------------------------------------------------------------------

    #region FSM

    public override void OnEnterState()
    {
        new RefillEnergyCommand().AddToQueue();
        CombatBlackboard.Player.PlayTurn();
        Handler.DrawHand();
    }

    public override void OnExitState()
    {
        new DiscardHandCommand().AddToQueue();
        CombatBlackboard.Player.FinishTurn();
    }

    #endregion

    //--------------------------------------------------------------------------------------------------------------
    void GoToCompanionTurn() => Handler.CompanionState();
    void TryToEscape() => Handler.EscapeState();
    void LoseBattle() => Handler.LoseState();
    void WinBattle() => Handler.WinState();
    //--------------------------------------------------------------------------------------------------------------

}
