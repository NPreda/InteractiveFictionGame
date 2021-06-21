public class RemoveStatusEffectCommand : Command
{
    private ITarget target;
    private StatusType statusType;

    public RemoveStatusEffectCommand(ITarget target,StatusType statusType)
    {        
        this.target = target;
        this.statusType = statusType;
    }

    public override void StartCommandExecution()
    {
        target.RemoveStatus(statusType);
        CommandExecutionComplete();
    }
}
