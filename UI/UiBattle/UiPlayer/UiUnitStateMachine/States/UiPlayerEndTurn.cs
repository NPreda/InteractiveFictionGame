using Patterns.StateMachine;
using UnityEngine;
using UnityEngine.EventSystems;


public class UiPlayerEndTurn : UiBasePlayerState
{
    //--------------------------------------------------------------------------------------------------------------

    public UiPlayerEndTurn(IUiPlayer handler) : base(handler)
    {
    }
    //--------------------------------------------------------------------------------------------------------------

    public override void OnEnterState()
    {
        foreach(var statusEffect in Handler.statusEffects)
        {
            if (statusEffect.stack != 0) 
                statusEffect.OnTurnEnd();
        }

        Handler.finishedTurn = true;
        Handler.DirtUI();
        Handler.Idle();
    }

    public override void OnExitState()
    {

    }

}
