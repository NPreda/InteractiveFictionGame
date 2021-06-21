using Tools.UI;
using UnityEngine;

[RequireComponent(typeof(IMouseInput))]
public class UISimplePressButton: UIPressButton
{
    public override void Awake()
    {
        base.Awake();
    }

    protected override void OnSkinUI ()
    {  
    }

}