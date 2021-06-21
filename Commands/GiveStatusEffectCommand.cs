public class GiveStatusEffectCommand : Command
{
    private ITarget target;
    private StatusType statusType;
    private int value;

    public GiveStatusEffectCommand(ITarget target,StatusType statusType, int value)
    {        
        this.target = target;
        this.statusType = statusType;
        this.value = value;
    }

    public override void StartCommandExecution()
    {
        target.GiveStatus(statusType, value);
        CommandExecutionComplete();
    }
}
