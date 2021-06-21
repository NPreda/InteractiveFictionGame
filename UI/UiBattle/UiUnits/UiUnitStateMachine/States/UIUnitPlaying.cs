using Patterns.StateMachine;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Linq;

public class UiUnitPlaying : UiBaseUnitState
{
    //--------------------------------------------------------------------------------------------------------------

    public UiUnitPlaying(IUiUnit handler) : base(handler)
    {
    }
    //--------------------------------------------------------------------------------------------------------------

    public override void OnEnterState()
    {
        //reset modifiers
        Handler.speedModifier = 0;
        Handler.damageModifier = 0;

        //apply effects
        foreach(var statusEffect in Handler.statusEffects)
        {
            if (statusEffect.stack != 0) 
                statusEffect.OnTurnStart();
        }

        Handler.DirtUI();

        if(!Handler.IsIdle)
        {
            if(Handler.combatActions.Count != 0)
            {
                processActions();
            }else{
                createAction();
            }

        }
        GoToIdle();
    }

    public override void OnExitState()
    {       
        foreach(var statusEffect in Handler.statusEffects)
        {
            if (statusEffect.stack != 0) 
                statusEffect.OnTurnEnd();
        }

        Handler.finishedTurn = true;
        Handler.DirtUI();

    }

    //--------------------------------------------------------------------------------------------------------------

    public void createAction()
    {
        List<ITarget> allTargets = CombatBlackboard.AllTargets();
        UnitEffect unitEffect = Handler.unitAI.Run(Handler, allTargets);
        new SpawnCombatActionCommand(Handler, unitEffect).AddToQueue();
    }

    public void processActions()
    {
        foreach(var action in Handler.combatActions.ToList())
        {
            action.UpdateValues();
            if(action.GetTick() <= 0)
            {
                Handler.combatActions.Remove(action);
                action.Play();
            }
        }
    }


    void GoToIdle() => Handler.Idle();
    //--------------------------------------------------------------------------------------------------------------
}
