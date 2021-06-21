using System.Collections.Generic;

public class UiEquipmentPanel : UiInventoryPanel
{
    private EquipmentType equipmentType;


    public virtual void ShowItems(EquipmentType equipmentType)
    {
        this.equipmentType = equipmentType;
        Inventory.OnInvChanged += Dirty;
        Dirty();
    }

    protected override List<Quality> LoadItems()
    {
        List<Quality> tempList  = new List<Quality>();
        foreach(var item in Inventory.Instance.ReturnEquippableItems()){
            if(item.equipmentType == this.equipmentType && !item.isEquipped)
            {
                tempList.Add(item);
            }
        }

        return tempList;
    }    
}