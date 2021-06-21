using Patterns.StateMachine;


//Base UI Card State.
public abstract class UiBasePlayerState : IState
{


    //--------------------------------------------------------------------------------------------------------------

    #region Constructor

    protected UiBasePlayerState(IUiPlayer handler)
    {
        Handler = handler;
        isInitialized = true;
    }

    #endregion

    protected IUiPlayer Handler { get; }
    protected BaseStateMachine Fsm { get; }
    public bool isInitialized { get; }

    //--------------------------------------------------------------------------------------------------------------

    #region Operations

    /// <summary>
    ///     Enables the cartouche entirely. Collision, Rigidybody and adds Alpha.
    /// </summary>
    protected void Enable()
    {

    }

    /// <summary>
    ///     Disables the cartouche entirely. Collision, Rigidybody and adds Alpha.
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
