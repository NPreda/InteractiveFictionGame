using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AttemptEscapeState : BaseCombatSystemState
{


    //--------------------------------------------------------------------------------------------------------------

    #region Constructor

    public AttemptEscapeState(CombatSystem handler)  : base(handler)
    {
    }

    #endregion

    //--------------------------------------------------------------------------------------------------------------

    #region FSM

    public override void OnEnterState()
    {
        //get the average of all the enemy speeds
        int averageSpeed =  (int)CombatBlackboard.Enemies.Average(x => x.ReturnSpeed());

        //terrible hack to get a speed contest going
        Contest escapeContest = new Contest();
        escapeContest.contestType = ContestType.Broad;
        escapeContest.diff = averageSpeed;
        escapeContest.statType = RPGStatType.Speed;
        bool result = escapeContest.RollContest();

        if(result)
        {
            new SpawnSplashTextCommand("Escaped" , Color.green).AddToQueue();
            new PlayStoryTriggerCommand(Handler.fleeStorylet).AddToQueue();
        }
        else
        {
            new SpawnSplashTextCommand("Escape Failed", Color.red).AddToQueue();
            Handler.CompanionState();   
        }

    }

    public override void OnExitState()
    {

    }


    #endregion

    //--------------------------------------------------------------------------------------------------------------
}
