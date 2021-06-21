using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager
{
    //delegate()
    public delegate void statChange (RPGStat stat);
    //event  
    public static event statChange OnStatChanged;

    //Dictionary holding stat linkages
    private static Dictionary<RPGStatType, RPGStatType> _linkDict = new Dictionary<RPGStatType, RPGStatType>();  //with this we reference attributes which effect stats

    //The container class for all the stats
    public RPGStatCollection stats;

    public void Setup() {
        stats = new pcDefaultStats();  //Get the stats for this game 
         
        var statTypes = Enum.GetValues(typeof(RPGStatType));               

        foreach(var stat in RPGStatCollection.StatDict){                  //for all instantiated possible stat types in the dictionary...
            stat.Value.OnStatUpdated += updateStats;                       //...sign up to its delegate/event so that you know when it changes

            if( stat.Value is pcDerivedStat){                                           //it it is a derived stat...
                _linkDict.Add(((pcDerivedStat)stat.Value).ConnectedType,  stat.Key);    //...link enum type of the connection together and its enum type
           }
        }
    }

    //the change to the individual stat object is linked to the rest of the game through this
    public void updateStats(string statName){
        RPGStatType statType = returnStatType(statName);                           //get the enum type of the stat that has changed
        if(_linkDict.TryGetValue(statType, out RPGStatType value)){                //if the link dictionary has the stat as a key for a stat to be modified
            stats.GetStat<pcDerivedStat>(value).StatBaseMax = stats.GetStat(statType).StatValue;
            Debug.Log(string.Format("Stat by the name {0} changed to {1}",stats.GetStat<pcDerivedStat>(value).StatName, stats.GetStat<pcDerivedStat>(value).StatMax));
        }
        
        RPGStat stat = stats.GetStat(statType);
        if(OnStatChanged != null)  OnStatChanged(stat);                      //an "echo event" to signal stat changes to its controller
    }

    ///
    /// UTILITY METHODS
    ///

    public RPGStatType returnStatType(string statName) => (RPGStatType)Enum.Parse(typeof( RPGStatType ), statName);             //turns a string with the stat name into an Enum type

    public string returnStatName(RPGStatType statType) => stats.GetStat(statType).StatName;                //return a string version of the stat name to be displayed

    ///
    /// STAT MODIFIER METHODS
    ///
    public void AddModifier(RPGStatType statType, int value, object source){            //adds a stat modifier to a specific stat referencing the original object
        RPGStat stat = stats.GetStat(statType);
        stat.AddModifier(new statModifier(value, source));
    }

    public void RemoveAllModifiersFromSource(object source){                    //remove all modifiers tied to a specific source item/object
            foreach(var stat in RPGStatCollection.StatDict.Values){
                if(stat is pcAttribute  || stat is pcDerivedStat){
                    stat.RemoveAllModifiersFromSource(source);
                }
            }
    }


    //Change the stat value to something new
    public void statSet(RPGStatType statType, int value){                       //set the stat to a specific value
        RPGStat stat = stats.GetStat(statType);
        stat.StatBaseValue = value;
    }


    //Change the stat value to something new
    public void statSet(string statName, int value){                       //set the stat to a specific value
        RPGStatType statType = returnStatType(statName);
        RPGStat stat = stats.GetStat(statType);
        stat.StatBaseValue = value;
    }

    //Gets stat value
    public int GetStatValue(RPGStatType type)
    {
        RPGStat stat = stats.GetStat(type);
        int statValue = stat.StatValue;   //get the total modified value of the stat mentioned
        return statValue;
    }  

    //Gets stat value using string
    public int GetStatValue(string statName)
    {
        RPGStatType type = (RPGStatType)Enum.Parse(typeof(RPGStatType), statName);  //Turns a string into an ENUM value
        RPGStat stat = stats.GetStat(type);
        int statValue = stat.StatValue;   //get the total modified value of the stat mentioned
        return statValue;
    }  

    //Gets stat base value
    public int GetStatBaseValue(RPGStatType type)
    {
        RPGStat stat = stats.GetStat(type);
        int statValue = stat.StatBaseValue;   //get the base value of the stat mentioned
        return statValue;
    }  

    //Gets stat base value
    public int GetStatBaseValue(string statName)
    {
        RPGStatType type = (RPGStatType)Enum.Parse(typeof(RPGStatType), statName);  //Turns a string into an ENUM value
        RPGStat stat = stats.GetStat(type);
        int statValue = stat.StatBaseValue;   //get the base value of the stat mentioned
        return statValue;
    }  

    //gets the actual stat object based on the name string
    public RPGStat getStatByName(string statName){
        RPGStatType statType = returnStatType(statName);
        RPGStat stat = stats.GetStat(statType);
        return stat;
    }

    //gets the actual stat object based on the enum t<pw
    public RPGStat getStatByName(RPGStatType statType){
        RPGStat stat = stats.GetStat(statType);
        return stat;
    }
}
