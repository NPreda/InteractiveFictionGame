using Patterns.StateMachine;


public abstract class BaseCombatSystemState : IState
{


    //--------------------------------------------------------------------------------------------------------------

    #region Constructor

    protected BaseCombatSystemState(CombatSystem handler)
    {
        Handler = handler;
        isInitialized = true;
    }

    #endregion

    protected CombatSystem Handler { get; }

    protected BaseStateMachine Fsm { get; }
    public bool isInitialized { get; }

    //--------------------------------------------------------------------------------------------------------------



    #region FSM

    void IState.OnInitialize()
    {
    }

    public virtual void OnEnterState()
    {
    }

    public virtual void OnExitState()
    {
    }

    public virtual void OnUpdate()
    {
    }

    public virtual void OnNextState(IState next)
    {
    }

    public virtual void OnClear()
    {
    }

    #endregion

    //--------------------------------------------------------------------------------------------------------------
}
