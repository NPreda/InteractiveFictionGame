using System.Collections.Generic;
using System.Linq;


public class EnemyTurnState : BaseCombatSystemState
{


    //--------------------------------------------------------------------------------------------------------------

    #region Constructor

    public EnemyTurnState(CombatSystem handler)  : base(handler)
    {
    }

    #endregion

    //--------------------------------------------------------------------------------------------------------------

    #region FSM

    public override void OnEnterState()
    {
        //the unit mix might change so we need to re-load the list a lot
        bool _turnFinish = false;
        while (_turnFinish == false){
            _turnFinish = true;
            foreach(var unit in CombatBlackboard.Enemies.ToList())
                if(unit.finishedTurn == false)
                {
                    unit.PlayTurn();
                    _turnFinish = false;
                    break;
                }
        }


        GoToPlayerTurn();
    }

    public override void OnExitState()
    {
        for( int i = 0; i < CombatBlackboard.Enemies.Count; i++ )
        {
            CombatBlackboard.Enemies[i].finishedTurn = false;
        }
    }



    #endregion

    //--------------------------------------------------------------------------------------------------------------

    void GoToPlayerTurn() => Handler.PlayerState();

    //--------------------------------------------------------------------------------------------------------------
}
