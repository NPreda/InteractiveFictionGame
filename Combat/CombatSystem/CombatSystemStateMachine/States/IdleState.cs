using Patterns.StateMachine;


public class IdleState : BaseCombatSystemState
{


    //--------------------------------------------------------------------------------------------------------------

    #region Constructor

    public IdleState(CombatSystem handler)  : base(handler)
    {
    }

    #endregion

    //--------------------------------------------------------------------------------------------------------------

    #region FSM

    public override void OnEnterState()
    {
    }

    public override void OnExitState()
    {

    }



    #endregion

    //--------------------------------------------------------------------------------------------------------------
}
