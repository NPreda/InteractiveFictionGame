using UnityEngine;

[System.Serializable]
public class SimpleAttackEffect: CardEffect
{
    [SerializeField]private DamageType damageType;
    private DamageInfo damageInfo;

    public override void PlayEffect(ITarget target)
    {
        this.damageInfo = new DamageInfo();
        this.damageInfo.value = ReturnTotalEffectValue();
        this.damageInfo.damageType = damageType;
        this.damageInfo.source = source;
        this.damageInfo.target = target;
        
        new DealDamageCommand(damageInfo).AddToQueue();
    }

    protected override int ReturnEffectValueBonus()
    {
        Debug.Log(source);
        return source.damageModifier + ReturnScaleValue(strengthType);
    }

}