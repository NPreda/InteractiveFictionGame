public class RefillEnergyCommand: Command
{

    public RefillEnergyCommand()
    {        
    }

    public override void StartCommandExecution()
    {
        Character.Instance.refillStat("Energy");
        CommandExecutionComplete();
    }
}
