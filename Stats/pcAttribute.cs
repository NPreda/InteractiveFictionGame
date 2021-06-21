using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

public class pcAttribute : RPGStat
{
    ///variables specific to this larger Attribute type stat
    private int _attrTick;                                          //these are "experience points" per attribute
    private int limitMax = 100;                                     //the maximum limit it can reach(hardcoded)
    private int limitMin = 0;                                       //the minimum limit it can reach(hardcoded)

    ///Method that handles the XP values of the particular attribute
    public int AttrTick{                                              
        get { return _attrTick; }
        set
        {
            if (value >= this.StatBaseValue)            //if the Ticks have reached the same level as the base value...
            {  
                while (value >= this.StatBaseValue)     //...and while "value" remains above the base value
                {
                    value = value - this.StatBaseValue; //..substract/"spend" the base value..
                    this.StatBaseValue += 1;            //...and  increment the base value (level up by use system)
                }
                _attrTick = value;                      // what remains will be saved in the variable
            }else{
                _attrTick = value;
            }
        }
    }

    ///Method that return the total stat value with the addition of modifiers
    public override int StatValue {             
        get {
            if (base.StatValue + this.TotalMod > limitMax){                  //if the total is above limitMax, cut it off at limitMax
                return limitMax;
            }else if(base.StatValue + this.TotalMod < limitMin){               //if the total is bellow limitMin, cut it off at limitMin
                return limitMin;
            }else{
            return base.StatValue + this.TotalMod;                      //else, just give it out as is
            }
        }
    }

    ///Method that handles getting and setting the attribute base/unmodified value
    public override int StatBaseValue {       
        get { return base.StatBaseValue; }
        set {
            if(value <= limitMin){                     //this base value should never be set to anything below limitMin or above limitMax
                base.StatBaseValue = limitMin;
            }else if(value >= limitMax){
                base.StatBaseValue = limitMax;
            }else{
                base.StatBaseValue = value;
            } 
        }
    }


    ///
    /// CONSTRUCTORS
    ///
    public pcAttribute()  : base() {                          
        this.StatName = string.Empty;
        this.StatBaseValue = limitMin; 
        this.AttrTick = 0;    
    }

    public pcAttribute(string name, int value)  : base()  { 
        this.StatName = name;
        this.StatBaseValue = value;
        this.AttrTick = 0;
    }
}
