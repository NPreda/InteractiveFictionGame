using Tools.UI;
using UnityEngine;
using System;

public class CharMenu : MonoBehaviour
{
    [SerializeField]private InfoPanel infoPanel;
    [SerializeField]private InfoButtonSet infoButtonSet;

    public void Activate()
    {
        infoButtonSet.Enable();
    }

    public void Deactivate()
    {
        infoPanel.Disable();
        infoButtonSet.Disable();
    }
}