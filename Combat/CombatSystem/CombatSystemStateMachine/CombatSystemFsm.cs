using Patterns.StateMachine;
using UnityEngine;


///     State Machine that holds all states of a UI Card.
public class CombatSystemFsm : BaseStateMachine
{
    //--------------------------------------------------------------------------------------------------------------

    #region Constructor

    public CombatSystemFsm(CombatSystem handler) :
    base(handler)
    {
        CombatStartState = new CombatStartState(handler);
        PlayerTurnState = new PlayerTurnState(handler);
        EnemyTurnState  = new EnemyTurnState(handler);
        CompanionTurnState  = new CompanionTurnState(handler);
        AttemptEscapeState = new AttemptEscapeState(handler);
        VictoryState = new VictoryState(handler);
        LoseState = new LossState(handler);
        IdleState = new IdleState(handler);

        RegisterState(CombatStartState);
        RegisterState(PlayerTurnState);
        RegisterState(EnemyTurnState);
        RegisterState(CompanionTurnState);
        RegisterState(AttemptEscapeState);
        RegisterState(VictoryState);
        RegisterState(LoseState);
        RegisterState(IdleState);

        Initialize();
    }

    #endregion

    //--------------------------------------------------------------------------------------------------------------

    #region Properties

    CombatStartState CombatStartState { get; }
    PlayerTurnState PlayerTurnState { get; }
    EnemyTurnState EnemyTurnState {get;}
    CompanionTurnState CompanionTurnState {get;}
    AttemptEscapeState AttemptEscapeState { get; }
    VictoryState VictoryState {get;}
    LossState LoseState { get; }
    IdleState IdleState{ get; }


    #endregion

    //--------------------------------------------------------------------------------------------------------------

    #region Operations

    public void StartState() => PushState<CombatStartState>();
    public void PlayerState() => PushState<PlayerTurnState>();
    public void EnemyState() => PushState<EnemyTurnState>();
    public void CompanionState() => PushState<CompanionTurnState>();
    public void EscapeState() => PushState<AttemptEscapeState>();
    public void WinState() => PushState<VictoryState>();
    public void LossState() => PushState<LossState>();
    public void GoToIdleState() => PushState<IdleState>();

    #endregion

    //--------------------------------------------------------------------------------------------------------------
}