using Patterns.StateMachine;
using UnityEngine;
using UnityEngine.EventSystems;


public class UiPlayerStartTurn : UiBasePlayerState
{
    //--------------------------------------------------------------------------------------------------------------

    public UiPlayerStartTurn(IUiPlayer handler) : base(handler)
    {
    }
    //--------------------------------------------------------------------------------------------------------------

    public override void OnEnterState()
    {
        //reset modifiers
        Handler.speedModifier = 0;
        Handler.damageModifier = 0;

        //apply effects
        Debug.Log("Here: " + Handler.statusEffects);
        foreach(var statusEffect in Handler.statusEffects)
        {
            if (statusEffect.stack != 0) 
                statusEffect.OnTurnStart();
        }

        Handler.DirtUI();
        Handler.Idle();
    }

    public override void OnExitState()
    {

    }

}
