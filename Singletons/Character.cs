using System;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    //delegate()
    public delegate void charChange ();
    //event  
    public static event charChange OnCharChanged;


    //the stat manager object with all the stats
    public StatManager statManager = new StatManager();
    
    //variables to help global instance selection
    private static Character  m_Instance;
    public static  Character  Instance { get { return m_Instance; } }

    void Awake(){
        m_Instance = this;          //Get this instance
    }
     
    public void Setup() {         
        statManager.Setup();
        StatManager.OnStatChanged += refreshScreen;  
    }

    void OnDestroy()
    {
        m_Instance = null;
    }

    public void refreshScreen(RPGStat stat){
        if(OnCharChanged != null) OnCharChanged();                      //an "echo event" to signal character changes to the UI and Game manager
    }

    //------------------------------------------------------------------------------------------------------------------------------------------------------
    #region StatMethods

    public string[] getStatList()  =>   System.Enum.GetNames( typeof(RPGStatType) ); //returns a string list of all stats for the main player character.

    public List<StatPacket> getStatPackets()
    {
        List<StatPacket> packets = new List<StatPacket>();
        foreach (string statName in getStatList())
        {
            StatPacket packet = ReturnStatPacket(statName);
            packets.Add(packet);
        }
        return packets;
    }

    public StatPacket ReturnStatPacket(string statName) => new StatPacket(statManager.getStatByName(statName));    //turns string into a statpacket containing all stat information

    public RPGStatType returnStatType(string statName) => statManager.returnStatType(statName);  //turns a string with the stat name into an Enum type

    public string returnStatName(RPGStatType statType) => statManager.returnStatName(statType);  //return a string version of the stat name to be displayed

    public bool isAttribute(string statName) => statManager.getStatByName(statName) is pcAttribute ;      //Asks if a stat name is a main attribute

    public bool isDerived(string statName) => statManager.getStatByName(statName) is pcDerivedStat;        //Asks if a stat name is derived from a main attribute, having a max limit and variable value

    public bool isFloating(string statName) => statManager.getStatByName(statName) is pcFloatingStat;       //Asks if a stat name is a hidden value, not to be displayed

    public void refillStat(string statName){ 
        try{
            pcFloatingStat stat = statManager.stats.GetStat<pcFloatingStat>(returnStatType(statName));
            stat.statFill();
        }catch{
            throw new Exception("Trying to refill a stat that cannot be refilled, if it even exists");
        }
    }

    #endregion
    //------------------------------------------------------------------------------------------------------------------------------------------------------

    #region  EnergyMethods

    public void burnEnergy(int cardCost) =>  statManager.statSet("Energy", statManager.GetStatBaseValue("Energy") - cardCost);

    #endregion
    //------------------------------------------------------------------------------------------------------------------------------------------------------
}
