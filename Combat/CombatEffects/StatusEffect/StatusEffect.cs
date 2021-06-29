using UnityEngine;


public class StatusEffect
{
    public ITarget handler;
    public int stack = 0;
    public bool visibleValue = true;

    public string statusName;
    public string statusDescription;

    public DisplayType displayType;
    public StatusType statusType;
    public Sprite icon;

    public StatusEffect(ITarget handler, StatusType statusType, DisplayType displayType)
    {
        this.handler = handler;
        this.statusType = statusType;
        this.displayType = displayType;
        this.icon = Resources.Load<Sprite>("Sprites/UI/Battle/StatusEffects/Default");
    }

    public void AddValue(int value)
    {
        this.stack = this.stack + value;
    }

    public void TickDown()
    {
        stack--;
    } 

    public void Remove()
    {
        stack = 0;
    }

    public void Remove(int amount)
    {
        stack = (int) Mathf.Clamp(stack-amount, 0, Mathf.Infinity);
    }

    public virtual void OnTurnStart()
    {

    }
    
    public virtual void OnApply()
    {

    }

    public virtual DamageInfo OnAttack(DamageInfo damageInfo)
    {
        return damageInfo;
    }

    public virtual HealInfo OnHeal(HealInfo healInfo)
    {
        return healInfo;
    }

    public virtual ArmorInfo OnArmor(ArmorInfo armorInfo)
    {
        return armorInfo;
    }

    public virtual void OnTurnEnd()
    {

    }
}