public class SpawnEnemyCommand: Command
{
    private Unit unit;
    private UnitFactory enemyFactory;

    public SpawnEnemyCommand(Unit u, UnitFactory enemyFactory)
    {        
        this.unit = u;
        this.enemyFactory = enemyFactory;
    }

    public override void StartCommandExecution()
    {
        enemyFactory.Load(unit, TargetType.Foe);
        CommandExecutionComplete();
    }
}
