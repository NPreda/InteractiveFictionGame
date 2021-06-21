using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

public class pcFloatingStat : RPGStat
{
    ///stat variables specific to this sort of "container stats"
    private int _statBaseMax;                       //the base unmodified Maximum value of the stat
    private int _limitMin = 10;                           //the lower limit of the StatBaseMax variable (ex: your minimum health bar)
    private bool _isHidden = false;

    //Set and get the lower limit of the StatBaseMax variable
    public virtual int LimitMin{
        get { return _limitMin; }
        set {
            if (value >= 0){
                _limitMin = value;
            }else{
                _limitMin = 0;
            }            
        }
    }

    //Set and get the isHidden variable that determines if the stat is shown in the main screen immediately
    public virtual bool isHidden{
        get { return _isHidden; }
        set {
            _isHidden = value;
        }
    }

    //The Base Stat in this case acts like the variable quantity being damaged/healed
    public override int StatBaseValue{                       //get-set sets the current value the attribute has
        get{
            if (base.StatBaseValue > StatMax)
            {
                base.StatBaseValue = StatMax;
            }
            else if (base.StatBaseValue < 0)
            {
                base.StatBaseValue = 0;
            }
            return base.StatBaseValue;
        }
        set{
            if (base.StatBaseValue != value)
            {
                base.StatBaseValue = value;
            }
        }
    }

    ///the Maximum value returned to the player/game, with the addition of the modifiers
    public virtual int StatMax {             //this method gets
        get {

            if(this.StatBaseMax + this.TotalMod < this.LimitMin){
                return this.LimitMin;
            }else{
            return this.StatBaseMax + this.TotalMod; 
            }
        }
    }


    /// The Base Value of the Maximum limit, effected only by its linked attribute
    public virtual int StatBaseMax {
        get { return _statBaseMax; }
        set {
            //soft lower limits method where if under the lower limit, it just gets ignored
            if(value <= this.LimitMin){             //check if lower limit was reached (as of writing Default: 10)
                _statBaseMax = this.LimitMin;
            }else{
                _statBaseMax = value;
            } 

            //hard lower limit method, where the lower limit gets added on            
            //_statBaseMax = limitMin + value;     
            
            sendUpdate();         //send an update to subscribers that stat has been updated
        }
    }    

    /// Refill the value to the maximum 
    public virtual void statFill(){
        this.StatBaseValue = this.StatMax;
    }



    ///
    /// CONSTRUCTORS
    ///
    public pcFloatingStat() : base(){                          //basic constructor
        this.StatName = string.Empty;
        this.StatBaseMax = this.LimitMin; 
        this.StatBaseValue = 0;    
    }
}
