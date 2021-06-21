using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;


//HumanDescription type : strict, loose, forgiving
public class Quality : ScriptableObject
{
    [HideInInspector]public string id{get => name;}

    [HorizontalGroup("Basic Info", 80)]
    [PreviewField(80)]
    [HideLabel]
    public Sprite icon;

    [VerticalGroup("Basic Info/Strings")]
    [LabelWidth(100)]
    public string qualityName;

    [VerticalGroup("Basic Info/Strings")]
    [LabelWidth(100)]
    [TextArea(3,3)]
    [SerializeField]
    public string defaultDesc;    //this will be shown if the description dictionary has nothing else.

    public virtual string ReturnDescription()
    {
        return defaultDesc;
    }

}
