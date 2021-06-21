using Patterns.StateMachine;


//Base UI Card State.
public abstract class UiBaseUnitState : IState
{


    //--------------------------------------------------------------------------------------------------------------

    #region Constructor

    protected UiBaseUnitState(IUiUnit handler)
    {
        Handler = handler;
        isInitialized = true;
    }

    #endregion

    protected IUiUnit Handler { get; }
    protected BaseStateMachine Fsm { get; }
    public bool isInitialized { get; }

    //--------------------------------------------------------------------------------------------------------------

    #region Operations

    /// <summary>
    ///     Enables the unit entirely. Collision, Rigidybody and adds Alpha.
    /// </summary>
    protected void Enable()
    {

    }

    /// <summary>
    ///     Disables the unit entirely. Collision, Rigidybody and adds Alpha.
    /// </summary>
    protected virtual void Disable()
    {

    }


    #endregion

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
