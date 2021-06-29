public class RemoveStatusEffectCommand : Command
{
    private ITarget target;
    private StatusType statusType;
    private int amount = 100;

    public RemoveStatusEffectCommand(ITarget target,StatusType statusType)
    {        
        this.target = target;
        this.statusType = statusType;
    }

    public RemoveStatusEffectCommand(ITarget target,StatusType statusType, int amount)
    {        
        this.target = target;
        this.statusType = statusType;
        this.amount = amount;
    }

    public override void StartCommandExecution()
    {
        if(amount == 100)
            target.RemoveStatus(statusType);
        else
            target.RemoveStatus(statusType,amount);
        CommandExecutionComplete();
    }
}
