using UnityEngine;

public class EffectComponent 
{
    public ITarget source;
    public ITarget target;
    public int value;

    public EffectComponent(){}

    public EffectComponent(ITarget source, ITarget target, int value)
    {
        this.source = source;
        this.target = target;
        this.value = value;
    }

}
