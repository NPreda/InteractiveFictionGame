using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text;
using System.ComponentModel;


public enum ContestType{
    Broad,
    Narrow
}


[System.Serializable]
public class Contest
{
    public RPGStatType statType;
    public ContestType contestType;
    public int diff;                                     //Difficulty of the challenge, usually from 1-100
    public Result success;
    public Result failure;
    [HideInInspector]private float broadConstant = 60;   //Difficulty scalar broad challenges, default as of alpha is 60. 
    [HideInInspector]private float narrowConstant = 10;  //Difficulty scalar of narrow challenges, default as of alpha is 10. 
    

    private int BroadChance(){
        //To be used with very large value ranges (1-100 or above)
        //the narrowConstant determine the chance of success if the stat value equals the difficulty
        float statValue = (float) Character.Instance.statManager.GetStatValue(statType);
        int chance = Mathf.FloorToInt(statValue/(float)diff * broadConstant);
        //Debug.Log(string.Format("Following chance {0}", chance));
        return chance;
    }

    private int NarrowChance(){
        //To be used with very small value ranges (1-10 or below)
        float statValue = (float) Character.Instance.statManager.GetStatValue(statType);
        int chance = Mathf.FloorToInt(50 + (statValue - (float)diff) * narrowConstant);
        //Debug.Log(string.Format("Following chance {0}", chance));
        return chance;
    }

    public string QualityName(){
            return Character.Instance.statManager.stats.GetStat(statType).StatName;                //return a string version of the stat name to be displayed
    }

    public int ContestChance(){
        int chance = 0;
        switch(contestType)
        {
            case ContestType.Broad:
                chance = BroadChance();
                break;
            case ContestType.Narrow:
                chance = NarrowChance();
                break;
        }
        return chance;
    }

    public bool RollContest(){
        int diceResult = Random.Range(0, 100);
        if (Random.Range(0,100) <= ContestChance())
        {
            return true;
        }else{
            return false;
        }
    }
}