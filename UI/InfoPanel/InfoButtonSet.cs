using Tools.UI;
using UnityEngine;
using System;

public class InfoButtonSet : TweeningMover
{
    private UIButtonGroup buttonGroup = new UIButtonGroup();
    [SerializeField] UIButton[] buttons;

    public event Action<UIButton> OnButtonPressed;        //event sent when an item is left-clicked


    public override void Awake()
    {
        base.Awake();
        buttonGroup.OnButtonPressed += OnButtonSelected;
        foreach(var button in buttons)
        {
            buttonGroup.Add(button);
        }
    }

    public void DeselectAll() => buttonGroup.DeselectAll();

    //highlight selection
    protected void OnButtonSelected(UIButton selectedButton)
    {
        OnButtonPressed(selectedButton);
    }
}