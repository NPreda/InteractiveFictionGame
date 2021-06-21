using System;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class statModifier
{
    private  int _modValue;
    private  object _modSource;


    public int ModValue{
        get { return _modValue; }
        set { _modValue = value; }  
    }

    public object ModSource{
        get{return _modSource;}
        set{_modSource = value;}
    }

    ///
    /// CONSTRUCTORS
    ///
    public statModifier(){
        this.ModValue = 0;
        this.ModSource = null;
    }

    public statModifier(int value, object source){
        this.ModValue = value;
        this.ModSource = source;
    }

}
