using Patterns.StateMachine;


public class LossState : BaseCombatSystemState
{


    //--------------------------------------------------------------------------------------------------------------

    #region Constructor

    public LossState(CombatSystem handler)  : base(handler)
    {
    }

    #endregion

    //--------------------------------------------------------------------------------------------------------------

    #region FSM

    public override void OnEnterState()
    {
        Handler.CancelInvoke();

        if(Handler.defaultLoss == null)
        {
            UiUnit strongestEnemy = (UiUnit)CombatBlackboard.Enemies[0];
            Storylet enemyResult = strongestEnemy.enemyWin;
            new PlayStoryTriggerCommand(enemyResult).AddToQueue();
        }else{
            new PlayStoryTriggerCommand(Handler.defaultLoss).AddToQueue();
        }
    }

    public override void OnExitState()
    {

    }



    #endregion

    //--------------------------------------------------------------------------------------------------------------
}
