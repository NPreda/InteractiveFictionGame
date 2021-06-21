using System;



public class StatusEffectFactory
{
    public StatusEffect ReturnStatusEffect(ITarget handler, StatusType status)
    {
        //StatusType statusType  = (StatusType)Enum.Parse(typeof(StatusType), statusString);
        if(status == StatusType.Stun)
        {
            return new StunEffect(handler);
        }else if (status == StatusType.Mark){
            return new MarkEffect(handler);
        }else if (status == StatusType.Bleed){
            return new BleedEffect(handler);
        }else if (status == StatusType.Weak){
            return new WeakEffect(handler);
        }else if (status == StatusType.Vulnerable){
            return new VulnerableEffect(handler);
        }else if (status == StatusType.Haste){
            return new HasteEffect(handler);
        }else if (status == StatusType.Slow){
            return new SlowEffect(handler);
        }else if (status == StatusType.Riposte){
            return new RiposteEffect(handler);
        }else{
            throw new Exception("Unknown Type was trying to be creating");
        }
    }
}