public class RemoveAllStatusEffectsCommand : Command
{
    private ITarget target;
    private StatusType statusType;

    public RemoveAllStatusEffectsCommand(ITarget target)
    {        
        this.target = target;
    }

    public override void StartCommandExecution()
    {
        target.RemoveAllStatuses();
        CommandExecutionComplete();
    }
}
