using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


    public enum EquipmentType{
        Weapon,
        Armor
    }

[CreateAssetMenu(fileName = "New Equippable", menuName = "Quality/Equippable" )]
public class EquippableQuality : Quality
{
    [HideInInspector]public bool isEquipped = false;

    [BoxGroup("EquipmentTraits")]
    [Tooltip("Bonded equipment cannot be directly removed")]
    public bool isBonded;

    [BoxGroup("EquipmentTraits")]
    public EquipmentType equipmentType;

    [BoxGroup("EquipmentTraits")]
    public SerializableDictionary<RPGStatType, int> modifiers;
    
     private void OnEnable()
     {
        isEquipped = false;
     }

    public override string ReturnDescription()
    {
        return defaultDesc;
    }

}