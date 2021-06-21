using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Tools.UI;


public class UiEquipment : TweeningMover
{
    private List<UiEquipmentBox> boxList = new List<UiEquipmentBox>();

    //prefabs. TODO: to be replaced with Resource reference possible
    public GameObject boxPrefab;

    //functional variablws
    private bool _isDirty = false;

    private void Dirty() => _isDirty = true;

    public override void Awake()
    {
        base.Awake();
        GameManager.BackendInitialized +=  Setup;
        Inventory.OnInvChanged += Dirty;
    }

    public void Update()
    {
        if(_isDirty) RefreshUI();
    }

    public void Setup()
    {
        Dirty();        //refreshes the UI to match inventory setup
    }

    private void ClearBoxes()
    {
        for(int i=boxList.Count -1; i >= 0; i--)
        {
            var box = boxList[i];
            box.Destroy();
            boxList.Remove(box);
        }
    }

    //refresh function for the UI if the inventory changes
    private void RefreshUI()
    {
        ClearBoxes();
        var boxFactory = new EquipmentBoxFactory();
        var presentTypes = Inventory.Instance.ReturnEquippableItems().Select(o=>o.equipmentType).Distinct();
        foreach(EquipmentType equipmentType in presentTypes)
        {
            var newBox = boxFactory.GetNewInstance(this.gameObject, equipmentType, boxPrefab);
            boxList.Add(newBox);
        }

        _isDirty = false;
    }

}