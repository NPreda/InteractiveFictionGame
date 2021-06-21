using UnityEngine;
using Tools.UI;
using UnityEngine.EventSystems;



[RequireComponent(typeof(FancyTooltipTrigger))]
[RequireComponent(typeof(IMouseInput))]
public class EquipmentSlot : ItemSlot
{
    public EquipmentType equipmentType;
    
    public void Setup(EquipmentType equipmentType)
    {
        this.equipmentType = equipmentType;
        Inventory.OnInvChanged += Dirty;
    }

    private void SetBackground()
    {
        switch(equipmentType)
        {
            case EquipmentType.Weapon:
                icon.sprite = Resources.Load<Sprite>("Sprites/UI/Icons/Slots/weapon_background");
                break;
            case EquipmentType.Armor:
                icon.sprite = Resources.Load<Sprite>("Sprites/UI/Icons/Slots/armor_background");
                break;
            default:
                break;
        }
    }

    protected override void Subscribe()
    {
        mInput.OnPointerDown += Unequip;
    }


    //equip the item if it is an equippable
    private void Unequip(PointerEventData eventData){       
        if (item != null && item is EquippableQuality){
            new UnequipItemCommand((EquippableQuality)item).AddToQueue();
        }
    }

    protected override void RefreshUI()
    {
        this.item = Inventory.Instance.ReturnEquippedType(equipmentType);

        if (item == null) {            //disable image component if there is no item
            SetBackground();
            number.text = string.Empty;    
            tooltipTrigger.SetContent(string.Empty, string.Empty);
            tooltipTrigger.enabled = false;
        }else{
            icon.sprite = item.icon;  //load the icon of the object into the slot
            number.text = string.Empty;
            icon.enabled = true;     
            tooltipTrigger.SetContent(item.qualityName, item.ReturnDescription());
            tooltipTrigger.enabled = true;
        }
    }

}
