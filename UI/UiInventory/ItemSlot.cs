using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Tools.UI;
using System.Text;
using System.Collections.Generic;


[RequireComponent(typeof(FancyTooltipTrigger))]
[RequireComponent(typeof(IMouseInput))]
public class ItemSlot : MonoBehaviour
{

    public Image icon;
    public TMP_Text number;

    protected IMouseInput mInput;
    protected FancyTooltipTrigger tooltipTrigger;

    protected bool _isDirty = false;

    private Quality _item;
    public Quality item {
        get { return _item; }
        set {
            _item = value;
            Dirty();
            }
    }

    protected void Dirty() => _isDirty = true;

    public void Start()
    {
        mInput = this.gameObject.GetComponent<UiMouseInputProvider>();
        tooltipTrigger = this.gameObject.GetComponent<FancyTooltipTrigger>();

        Subscribe();

        Dirty();
    }

    protected virtual void Subscribe()
    {
        mInput.OnPointerDown += Equip;
    }

    public void Update()
    {
        if(_isDirty) RefreshUI();
    }

    public void Setup(Quality item) => this.item = item;

    //equip the item if it is an equippable
    private void Equip(PointerEventData eventData){       
        if (item != null && item is EquippableQuality){
            new TryEquipItemCommand((EquippableQuality)item).AddToQueue();
            Destroy();
        }
    }

    protected virtual void RefreshUI()
    {
        if (item == null) //disable image component if there is no item
        {            
            icon.enabled = false;
            number.text = string.Empty;
            tooltipTrigger.SetContent(string.Empty, string.Empty);
            tooltipTrigger.enabled = false;
        }
        else if(item is GenericQuality)
        {
            GenericQuality _itemTemp = (GenericQuality)item;
            icon.sprite = item.icon;  //load the icon of the object into the slot
            number.text = _itemTemp.val.ToString();      
            icon.enabled = true;         
            tooltipTrigger.SetContent(item.qualityName, item.ReturnDescription());
            tooltipTrigger.enabled = true;
        }
        else if(item is EquippableQuality)
        {
            icon.sprite = item.icon;  //load the icon of the object into the slot
            number.text = string.Empty;
            icon.enabled = true; 
            string tooltipBody = BuildEquipmentTooltip((EquippableQuality)item);
            tooltipTrigger.SetContent(item.qualityName, tooltipBody);
            tooltipTrigger.enabled = true;   
        }
        else
        {
            icon.sprite = item.icon;  //load the icon of the object into the slot
            number.text = string.Empty;
            icon.enabled = true; 
            tooltipTrigger.SetContent(item.qualityName, item.ReturnDescription());
            tooltipTrigger.enabled = true;
        }
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }



    public string BuildEquipmentTooltip(EquippableQuality item){
        string tooltipBody = item.ReturnDescription();

        StringBuilder sb = new StringBuilder();

        sb.Length = 0;
        
        foreach(KeyValuePair<RPGStatType, int> modifier in item.modifiers)      //go through the modifiers dictionary and add them to the stats
        {
            string statName = modifier.Key.ToString();
            int value = modifier.Value;
            if(value != 0){
                if(sb.Length > 0)
                    sb.AppendLine();

                if (value > 0)
                    sb.Append("+");

                sb.Append(value);
                sb.Append(" ");
                sb.Append(statName);
            }
        }
        tooltipBody = tooltipBody + "\n\n" + sb.ToString();
        return tooltipBody;
    }

}
