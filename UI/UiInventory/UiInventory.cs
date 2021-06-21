using UnityEngine;
using System;
using Tools.UI;

public class UiInventory : TweeningMover
{
    public GameObject tabPanel;
    public UiInventoryPanel inventoryPanel;

    public QualityType qualityType;

    //button management
    private UIButtonGroup tabGroup = new UIButtonGroup();

    //prefabs. TODO: to be replaced with Resource reference possible
    public GameObject tabPrefab;

    //functional variablws
    private bool _isDirty = false;

    //------------------------------------------------------------------------------------------------------------------------------------
    #region InitializationMethods

    private void Dirty() => _isDirty = true;

    public override void Awake()
    {
        base.Awake();
        GameManager.BackendInitialized +=  Setup;
        tabGroup.OnButtonPressed += SwitchTabs;
        Inventory.OnInvChanged += Dirty;
    }

    public void Update()
    {
        if(_isDirty) RefreshUI();
    }


    public void Setup()
    {
        SpawnTabs();
        
        tabGroup.SelectButton(tabGroup.buttons[0].id);  //hackish way to start at a specific tab

        Dirty();        //refreshes the UI to match inventory setup
    }


    //dynamically spawn the tabs based on the hardcoded quality type.
    private void SpawnTabs()
    {
        var buttonFactory = new ButtonFactory();
        foreach(QualityType qualityType in Enum.GetValues(typeof(QualityType)))             
        {
            if (qualityType != QualityType.Invisible)
            {
                UIButton button = buttonFactory.GetNewInstance(tabPanel, qualityType.ToString(), tabPrefab);
                tabGroup.Add(button);
            }
        }
    }

    #endregion
    //------------------------------------------------------------------------------------------------------------------------------------


    private void SwitchTabs(UIButton button)
    {
        qualityType = (QualityType)Enum.Parse(typeof(QualityType), button.id);  //get the quality type based on the button name
        Dirty();
    }

    //refresh function for the UI if the inventory changes
    private void RefreshUI()
    {
        inventoryPanel.ShowItems(qualityType);

        _isDirty = false;
    }

}
