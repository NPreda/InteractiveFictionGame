using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UiInventoryPanel : MonoBehaviour
{
    private string tabPrefabPath = "Prefabs/ItemSlot";
    private List<ItemSlot> slots = new List<ItemSlot>();
    private QualityType qualityType;

    //functional variablws
    protected bool _isDirty = false;

    protected void Dirty() => _isDirty = true;

    public void Update()
    {
        if(_isDirty) RefreshUI();
    }

    public void ShowItems(QualityType qualityType)
    {
        this.qualityType = qualityType;
        Dirty();
    }

    protected virtual List<Quality> LoadItems()
    {
        List<Quality> tempList  = Inventory.Instance.ReturnGenericItems().Where(x => x.qualityType == qualityType).Cast<Quality>().ToList();
        return tempList;
    }

    //refresh function for the UI if the inventory changes
    private void RefreshUI()
    {
        ClearSlots();
        var loadedItems = LoadItems();
        SlotFactory slotFactory = new SlotFactory();
        foreach(Quality item in loadedItems)
        {
            slots.Add(slotFactory.GetNewInstance(this.gameObject, item, tabPrefabPath));
        }

        _isDirty = false;
    }

    private void ClearSlots()
    {
        for(int i=slots.Count -1; i >= 0; i--)
        {
            var slot = slots[i];
            slots.Remove(slot);
            slot.Destroy();
        }
    }

}