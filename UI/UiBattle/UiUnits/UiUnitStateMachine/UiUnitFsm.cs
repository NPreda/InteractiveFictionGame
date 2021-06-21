using Patterns.StateMachine;
using UnityEngine;


///     State Machine that holds all states of a UI Card.
public class UiUnitFsm : BaseStateMachine
{
    //--------------------------------------------------------------------------------------------------------------

    #region Constructor

    public UiUnitFsm(IUiUnit handler = null) : base(handler)
    {

        IdleState = new UiUnitIdle(handler);
        TargetState = new UiUnitTargeted(handler);
        PlayState = new UiUnitPlaying(handler);
        DeadState = new UiUnitDead(handler);

        RegisterState(IdleState);
        RegisterState(TargetState);
        RegisterState(PlayState);
        RegisterState(DeadState);

        Initialize();
    }

    #endregion

    //--------------------------------------------------------------------------------------------------------------

    #region Properties

    UiUnitIdle IdleState { get; }
    UiUnitTargeted TargetState { get; }
    UiUnitPlaying PlayState {get;}
    UiUnitDead DeadState{get;}
    
    
    #endregion
    //--------------------------------------------------------------------------------------------------------------
    
    #region Operations

    public void Idle() => PushState<UiUnitIdle>();
    public void Targeted()  =>  PushState<UiUnitTargeted>();
    public void PlayTurn() =>  PushState<UiUnitPlaying>();
    public void Die() => PushState<UiUnitDead>();

    #endregion

    //--------------------------------------------------------------------------------------------------------------
}