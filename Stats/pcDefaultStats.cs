using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class  that holds the method for creating the game default stats
public class pcDefaultStats : RPGStatCollection
{
    protected override void ConfigureStats(){
        pcAttribute might = CreateOrGetStat<pcAttribute>(RPGStatType.Might);
        might.StatName = "Might";
        might.StatBaseValue = 20;

        pcAttribute finesse = CreateOrGetStat<pcAttribute>(RPGStatType.Finesse);
        finesse.StatName = "Finesse";
        finesse.StatBaseValue = 20;

        pcAttribute presence = CreateOrGetStat<pcAttribute>(RPGStatType.Presence);
        presence.StatName = "Presence";
        presence.StatBaseValue = 20;

       pcDerivedStat health = CreateOrGetStat<pcDerivedStat>(RPGStatType.Health);
        health.StatName = "Health";
        health.ConnectedType = RPGStatType.Might;
        health.StatBaseMax = 20;
        health.statFill();

        pcDerivedStat speed= CreateOrGetStat<pcDerivedStat>(RPGStatType.Speed);
        speed.StatName = "Speed";
        speed.ConnectedType = RPGStatType.Finesse;
        speed.StatBaseMax = 20;
        speed.statFill();

        pcDerivedStat resolve = CreateOrGetStat<pcDerivedStat>(RPGStatType.Resolve);
        resolve.StatName = "Resolve";
        resolve.ConnectedType = RPGStatType.Presence;
        resolve.StatBaseMax = 20;
        resolve.statFill();
    
        pcFloatingStat energy = CreateOrGetStat<pcFloatingStat>(RPGStatType.Energy);
        energy.StatName = "Energy";
        energy.LimitMin = 0;
        energy.StatBaseMax = 3;
        energy.isHidden = true;
    }
}
