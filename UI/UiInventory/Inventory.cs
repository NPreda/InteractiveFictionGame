using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class Inventory : MonoBehaviour
{
    public List<Quality> inventoryItems = new List<Quality>();

    //delegate()
    public delegate void InvChange ();
    //event  
    public static event InvChange OnInvChanged;


    //variables to help global instance selection
    private static Inventory m_Instance;
    public static  Inventory Instance { get { return m_Instance; } }
     
    void Awake() 
    {
        m_Instance = this;          //Get this instance
    }


    //an "echo event" to signal inventory changes to everyone
    private void SignalChange()
    {
        if(OnInvChanged != null) OnInvChanged();                     
    }

    //---------------------------------------------------------------------------------------------------------------------
    #region ItemAccess

    //return true/false if object is in inventory based on id
    public bool inInventory(string id) => inventoryItems.Any(cus => cus.id == id);

    //Return quality object based on ID
    public Quality ReturnItem(string id) => inventoryItems.SingleOrDefault(x => x.id == id); 
    

    //Return the icon associated with the item
    public Sprite ReturnItemIcon(string id) =>  ReturnItem(id)?.icon ?? null; //assign if not null

    //return the name associated with the item
    public string ReturnItemName(String id) => ReturnItem(id)?.qualityName ?? string.Empty; //assign if not null

    //Return the value/quantity of the Quality
    public int ReturnItemValue(string id)
    {
        if(ReturnItem(id) != null)  return QualityDB.Instance.GetValue(id);
        else return 0;
    }

    //gets all the equippable items in the inventory
    public List<EquippableQuality> ReturnEquippableItems() => inventoryItems.OfType<EquippableQuality>().ToList();

    //gets all the  Generic "state-machine" items in the inventory
    public List<GenericQuality> ReturnGenericItems() => inventoryItems.OfType<GenericQuality>().ToList();

    #endregion
    //---------------------------------------------------------------------------------------------------------------------
    #region InventoryManipulation

    public void AddItem(string itemID, int newValue)
    {
        Quality item = QualityDB.Instance.returnQuality(itemID);
        if(item != null) AddItem(item, newValue);
        else throw new System.Exception("ERROR: Attempted to give invalid quality to inventory with id: " + itemID);
    }

    public void AddItem(Quality item, int newValue)
    {     
        //first lets handle equippables
        if(item is EquippableQuality)   AddEquipmentToInventory((EquippableQuality)item, newValue);
        else if(item is GenericQuality) AddGenericToInventory((GenericQuality)item, newValue);
        SignalChange();
    }

    public void RemoveItem(GenericQuality item)
    {  
        if (inventoryItems.Remove(item)){
            item.val = 0;
            SignalChange();
        }
    }

    public void RemoveItem(Quality item)
    {  
        if (inventoryItems.Remove(item)){
            SignalChange();
        }
    }


    private void AddEquipmentToInventory(EquippableQuality item, int newValue)
    {
        if(inInventory(item.id) && newValue <= 0)  //check if being removed
        {
            inventoryItems.Remove(item);
        }
        else if(!inInventory(item.id)) //check if being added
        {
            inventoryItems.Add(item);
        }
    }

    private void AddGenericToInventory(GenericQuality item, int newValue)
    {
        if(inInventory(item.id) && newValue <= 0)       //first check that we're not outright removing it
        {         
            RemoveItem(item);           
        }
        else if(inInventory(item.id))
        {
            item.val = newValue;
        }
        else if(!inInventory(item.id)) 
        {
            inventoryItems.Add(item);
            item.val = newValue;

        }
    }

    #endregion
    //---------------------------------------------------------------------------------------------------------------------
    #region EquipMethods

    public void EquipItem(EquippableQuality item)
    {
        item.isEquipped = true;
        SignalChange();
    }

    public void UnequipItem(EquippableQuality item)
    {
        if (!item.isBonded)   item.isEquipped = false;
        SignalChange();
    }

    public void ForceUnequipItem(EquippableQuality item)
    {
        item.isEquipped = false;
        inventoryItems.Remove(item);
        SignalChange();
    }


    //returns an item if something is equiped, null if not, and throws an exception if multiple items of the same type are equipped
    public EquippableQuality ReturnEquippedType(EquipmentType equipmentType)
    {
        List<EquippableQuality> itemsOfType = ReturnEquippableItems().Where(x => x.equipmentType == equipmentType).ToList();    //return all possible equippables of type.
        var item = itemsOfType.SingleOrDefault(x => x.isEquipped == true);  //returns object if true, null if false, and exception if multiples

        return item;
    }

    #endregion
    //---------------------------------------------------------------------------------------------------------------------

}

    
