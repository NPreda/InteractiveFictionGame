using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text;
using System.ComponentModel;


public enum Sign 
{
    //Possible operations to conditionals to be displayed in inspector
    [InspectorName("=")] equal = 0,
    [InspectorName(">")] greater_than = 1,
    [InspectorName(">=")] greater_inclusive = 2,
    [InspectorName("<")] less_than = 3,
    [InspectorName("<=")] less_inclusive = 4,
    [InspectorName("!=")] not_equal = 5    
}



[System.Serializable]
public class StatCondition
{
    //Conditions related to stats.
    public bool hideOnFalse;
    public string quality;
    public Sign operation;
    public int value;    
}


[System.Serializable]
public class Conditions{
    public StatCondition[] statConditions;    
}

