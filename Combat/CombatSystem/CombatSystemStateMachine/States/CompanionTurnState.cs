using Patterns.StateMachine;


public class CompanionTurnState : BaseCombatSystemState
{


    //--------------------------------------------------------------------------------------------------------------

    #region Constructor

    public CompanionTurnState(CombatSystem handler)  : base(handler)
    {
    }

    #endregion

    //--------------------------------------------------------------------------------------------------------------

    #region FSM

    public override void OnEnterState()
    {
        CombatBlackboard.Player.FinishTurn();   //hackish placement due to preventing accidental death while switching to the victory/flee state due to status effects
        EnemyTurn();
    }

    public override void OnExitState()
    {

    }



    #endregion

    //--------------------------------------------------------------------------------------------------------------
    void EnemyTurn() => Handler.EnemyState();
    //--------------------------------------------------------------------------------------------------------------
}
