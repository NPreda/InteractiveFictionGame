using UnityEngine;
using System;
using System.Collections.Generic;


public class VictoryState : BaseCombatSystemState
{


    //--------------------------------------------------------------------------------------------------------------

    #region Constructor

    public VictoryState(CombatSystem handler)  : base(handler)
    {
    }

    #endregion

    //--------------------------------------------------------------------------------------------------------------

    #region FSM

    public override void OnEnterState()
    {
        Handler.CancelInvoke();
        if(Handler.defaultWin == null)
        {
            UiUnit remainingEnemy = (UiUnit)CombatBlackboard.Enemies[0];
            Storylet enemyResult = remainingEnemy.enemyLoss;
            new PlayStoryTriggerCommand(enemyResult).AddToQueue();
        }else{
            new PlayStoryTriggerCommand(Handler.defaultWin).AddToQueue();
        }
    }  

    public override void OnExitState()
    {

    }



    #endregion

    //--------------------------------------------------------------------------------------------------------------
}
