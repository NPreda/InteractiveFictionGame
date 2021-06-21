//packets which holds a flattened image of a stat. 
public class StatPacket
{
    public string name;
    public string type;
    public int value = -1;
    public int baseValue = -1;
    public int maxValue = -1;
    public int maxBaseValue = -1;
    public bool isAttribute = false;
    public bool isDerived = false;
    public bool isFloating = false;

    public StatPacket(RPGStat stat)
    {
        name = stat.StatName;

        if(stat is pcAttribute){
            isAttribute = true;
            value = stat.StatValue;
            baseValue = stat.StatBaseValue;
        }else if(stat is pcDerivedStat){
            pcDerivedStat castStat = (pcDerivedStat) stat;
            isDerived = true;
            value = castStat.StatValue;
            baseValue = castStat.StatBaseValue;
            maxValue = castStat.StatMax;
            maxBaseValue = castStat.StatBaseMax;
        }else if(stat is pcFloatingStat){
            pcFloatingStat castStat = (pcFloatingStat)stat;
            isFloating = true;
            value = castStat.StatValue;
            baseValue = castStat.StatBaseValue;
            maxValue = castStat.StatMax;
            maxBaseValue = castStat.StatBaseMax;
        }
    }
}
