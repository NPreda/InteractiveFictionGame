using UnityEngine;

public class MarkEffect : StatusEffect
{
    //This focuses all your allies attack on this target with a bonus. 
    //To be implemented when we implement companions

    public MarkEffect(ITarget handler) : base(handler, StatusType.Mark, DisplayType.Priority)
    {
        this.statusName = "Marked";
        this.statusDescription = "Attention has been drawn, and with it, danger.";
        this.icon = Resources.Load<Sprite>("Sprites/UI/Battle/StatusEffects/Marked");

    }

}