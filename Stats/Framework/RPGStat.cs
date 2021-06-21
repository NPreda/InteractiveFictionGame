using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;


/// The base class for all other Stats and Attributes
public class RPGStat {
    //delegate()
    public delegate void StatUpdate (string Name);
    //event  
    public event StatUpdate OnStatUpdated;

    /// Used by the StatName Property
    private string _statName;

    /// Used by the StatBase Value Property
    private int _statBaseValue;

    ///modifier variables
    private readonly List<statModifier> statModifiers;              //Used to list all modifiers applied to the stat
    public readonly ReadOnlyCollection<statModifier> StatModifiers; //used by scripts outside the object to see the modifiers
    private int _totalMod;                                          //the total value of applied modifiers 
    private bool isDirty = true;                                    //if the total value has been changed and needs to be recalculated

    /// The Name of the Stat
    public string StatName {
        get { return _statName; }
        set { _statName = value; }  
    }

    /// The Total Value of the stat
    public virtual int StatValue {
        get { return StatBaseValue; }
    }

    /// The Base Value of the stat
    public virtual int StatBaseValue {
        get { return _statBaseValue; }
        set {
            if(value <= 0){       //check if lower limit was reached
                _statBaseValue = 0;
            }else{
                _statBaseValue = value;
            }
            
            sendUpdate();         //send an update to subscribers that stat has been updated
        }
    }

    // The total value of all modifiers
    public virtual int TotalMod{
        get{
            if(isDirty) {                           //if value has been changed
                _totalMod = UpdateTotalMod();       //update the total modifier value
                isDirty = false;
            }
            return _totalMod;
        }
    }

    // Updates the total value of all modifiers together
    public virtual int UpdateTotalMod() {
        int updatedTotalMod = 0;
        
        //total up all applied modifiers
        for (int i = 0; i < statModifiers.Count; i++){          //count and add all modifiers in the local modifier list
            updatedTotalMod += statModifiers[i].ModValue;       //add them together
        }

        return updatedTotalMod;                                 //return to update the modifier total
    }

    ///Method that adds a modifier to the mod list
    public virtual void AddModifier(statModifier mod){
        isDirty = true;                 //set the flag that the modifiers have changed
        statModifiers.Add(mod);
        sendUpdate();
    }

    ///Method that removes a modifier from the mod list
    public virtual bool RemoveModifier(statModifier mod){
        isDirty = true;                 //set the flag that the modifiers have changed
        return statModifiers.Remove(mod);
    }

    ///Method that removes all modifiers from a specific source object from the mod list
    public virtual bool RemoveAllModifiersFromSource(object source){
        bool didRemove = false;
        isDirty = true;         //set the flag that the modifiers have changed

        for (int i = statModifiers.Count - 1; i >= 0; i--){
            if (statModifiers[i].ModSource == source){
                didRemove = true;
                statModifiers.RemoveAt(i);
            }
        }

        sendUpdate();
        return didRemove;
    }

    ///Method handling delegate/event transmission
    public virtual void sendUpdate(){
            if(OnStatUpdated != null){OnStatUpdated(_statName);}      //Raise OnStatUpdated event if there are subscribers  
    }

    /// Basic Constructor
    public RPGStat(){
        this.StatName = string.Empty;
        this.StatBaseValue = 0;
        statModifiers =new List<statModifier>();
        StatModifiers = statModifiers.AsReadOnly();  
    }

    /// Full Constructor 
    public RPGStat(string name, int value) {
        this.StatName = name;
        this.StatBaseValue = value;
    }
}