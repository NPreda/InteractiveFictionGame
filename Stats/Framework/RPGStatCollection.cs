using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGStatCollection
{
    [SerializeField]
    private static Dictionary<RPGStatType, RPGStat> _statDict;

    public RPGStatCollection(){
        _statDict = new Dictionary<RPGStatType, RPGStat>();
        ConfigureStats();
    }

    public static Dictionary<RPGStatType, RPGStat> StatDict {
        get {
            if (_statDict == null) {
                _statDict = new Dictionary<RPGStatType, RPGStat>();
            }
            return _statDict;
        }
    }

    protected virtual void ConfigureStats(){

    }

    public bool Contains(RPGStatType statType){
        return _statDict.ContainsKey(statType);
    }

    public RPGStat GetStat(RPGStatType statType) {
        if (Contains(statType)) {
            return StatDict[statType];
        }
        Debug.Log(String.Format("RPGStatCollections could not find stat type {0}", statType));
        return null;
}

    public T GetStat<T>(RPGStatType statType) where T: RPGStat{
        return GetStat(statType) as T;
    }

    protected T CreateStat<T>(RPGStatType statType) where T: RPGStat{
        T stat = System.Activator.CreateInstance<T>();
        StatDict.Add(statType, stat);
        return stat;
    }

    protected T CreateOrGetStat<T>(RPGStatType statType) where T: RPGStat{
        T stat = GetStat<T>(statType);
        if (stat == null){
            stat = CreateStat<T>(statType);
        }

        return stat;
    }
}
