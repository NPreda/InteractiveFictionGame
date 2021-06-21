public class SpendEnergyCommand: Command
{
    private int energyCost;

    public SpendEnergyCommand(int energyCost)
    {        
        this.energyCost = energyCost;
    }

    public override void StartCommandExecution()
    {
        Character.Instance.burnEnergy(this.energyCost);
        CommandExecutionComplete();
    }
}
