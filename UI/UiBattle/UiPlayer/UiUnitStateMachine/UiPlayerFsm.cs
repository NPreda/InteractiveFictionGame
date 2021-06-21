using Patterns.StateMachine;
using UnityEngine;


public class UiPlayerFsm : BaseStateMachine
{
    //--------------------------------------------------------------------------------------------------------------

    #region Constructor

    public UiPlayerFsm(IUiPlayer handler = null) : base(handler)
    {

        IdleState = new UiPlayerIdle(handler);
        TargetState = new UiPlayerTargeted(handler);
        StartTurnState = new UiPlayerStartTurn(handler);
        EndTurnState = new UiPlayerEndTurn(handler);


        RegisterState(IdleState);
        RegisterState(TargetState);
        RegisterState(StartTurnState);
        RegisterState(EndTurnState);



        Initialize();
    }

    #endregion

    //--------------------------------------------------------------------------------------------------------------

    #region Properties

    UiPlayerIdle IdleState { get; }
    UiPlayerTargeted TargetState { get; }
    UiPlayerStartTurn StartTurnState { get; }
    UiPlayerEndTurn EndTurnState { get; }

    
    
    #endregion
    //--------------------------------------------------------------------------------------------------------------
    
    #region Operations

    public void Idle() => PushState<UiPlayerIdle>();
    public void Targeted()  =>  PushState<UiPlayerTargeted>();
    public void StartTurn()  =>  PushState<UiPlayerStartTurn>();
    public void EndTurn()  =>  PushState<UiPlayerEndTurn>();

    #endregion

    //--------------------------------------------------------------------------------------------------------------
}