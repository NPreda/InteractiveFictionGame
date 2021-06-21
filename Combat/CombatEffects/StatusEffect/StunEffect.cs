using UnityEngine;

public class StunEffect : StatusEffect
{

    public StunEffect(ITarget handler) : base(handler, StatusType.Stun, DisplayType.Priority)
    {
        this.statusName = "Stun";
        this.statusDescription = "A moment of confusion and doubt";
        this.icon = Resources.Load<Sprite>("Sprites/UI/Battle/StatusEffects/Stun");
        this.visibleValue = false;
    }


    public override void OnTurnStart()
    {
        this.stack = 0;
        this.handler.FinishTurn();
    }


}