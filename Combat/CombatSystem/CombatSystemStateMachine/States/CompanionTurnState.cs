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
