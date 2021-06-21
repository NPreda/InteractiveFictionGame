public class DealDamageCommand : Command
{
    private ITarget target;
    private DamageInfo damageInfo;

    public DealDamageCommand(DamageInfo damageInfo)
    {        
        this.target = damageInfo.target;
        this.damageInfo = damageInfo;
    }

    public override void StartCommandExecution()
    {
        target.OnAttack(damageInfo);
        CommandExecutionComplete();
    }

}
