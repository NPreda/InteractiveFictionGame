using System.Collections.Generic;

public class CombatStartState : BaseCombatSystemState
{


    //--------------------------------------------------------------------------------------------------------------

    #region Constructor

    public CombatStartState(CombatSystem handler) : base(handler)
    {
    }

    #endregion

    //--------------------------------------------------------------------------------------------------------------

    #region FSM

    public override void OnEnterState()
    {
        new RefillEnergyCommand().AddToQueue();
        SetupBattle(Handler.units);

        Handler.combatDeck = new CombatDeckSystem(); 

        Handler.InvokeRepeating("RollBark", 2f, 5f);

        EnemyTurn();
    }

    public override void OnExitState()
    {

    }

    public void SetupBattle(List<Unit> units)
    {
        //Start populating the enemy panel
        for (int index = 0; index < units.Count; index++){
            new SpawnEnemyCommand(units[index], Handler.enemyFactory).AddToQueue();
        }
    }


    #endregion

    //--------------------------------------------------------------------------------------------------------------
    void EnemyTurn() => Handler.EnemyState();
    //--------------------------------------------------------------------------------------------------------------
}
