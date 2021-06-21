using Custom.UI;
using Tools.UI;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;

[RequireComponent(typeof(IMouseInput))]
[RequireComponent(typeof(Image))]
public class UIRoundPressButton: UIPressButton
{
    [SerializeField] private Image icon;
    private Sprite _icon;

    [SerializeField] private Sprite activeIcon;
    [SerializeField] private Sprite inactiveIcon;

    public override void Awake()
    {
        //icon.alphaHitTestMinimumThreshold = 0.9f;   //prevent clicks outside the circle from registering
        base.Awake();
    }

    public void Populate(string objectName, Sprite activeIcon, Sprite inactiveIcon)
    {
        base.Populate(objectName);
        this.activeIcon = activeIcon;
        this.inactiveIcon = inactiveIcon;
    }

    protected override void OnSkinUI ()
    {
        //base.OnSkinUI();

        if(_isSelected)
            _icon = activeIcon;
        else
            _icon =  inactiveIcon;

        if(icon != null) icon.sprite = _icon;        
    }

}