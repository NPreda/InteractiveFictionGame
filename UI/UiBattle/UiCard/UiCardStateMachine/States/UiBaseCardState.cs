using Patterns.StateMachine;


//Base UI Card State.
public abstract class UiBaseCardState : IState
{


    //--------------------------------------------------------------------------------------------------------------

    #region Constructor

    protected UiBaseCardState(IUiCard handler, UiCardParameters parameters)
    {
        Handler = handler;
        Parameters = parameters;
        isInitialized = true;
    }

    #endregion

    protected IUiCard Handler { get; }
    protected UiCardParameters Parameters { get; }
    protected BaseStateMachine Fsm { get; }
    public bool isInitialized { get; }

    //--------------------------------------------------------------------------------------------------------------

    #region Operations

    /// <summary>
    ///     Enables the card entirely. Collision, Rigidybody and adds Alpha.
    /// </summary>
    protected void Enable()
    {
        Handler.Fade(1f,0.5f);

    }

    /// <summary>
    ///     Disables the card entirely. Collision, Rigidybody and adds Alpha.
    /// </summary>
    protected virtual void Disable()
    {
        Handler.Fade(0.5f,0.5f);
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
