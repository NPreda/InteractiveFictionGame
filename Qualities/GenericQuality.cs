using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "New Generic", menuName = "Quality/Generic" )]
public class GenericQuality : Quality
{
    [BoxGroup("QualityTraits")]
    public QualityType qualityType;    

    [BoxGroup("QualityTraits")]
    public int val;      

    [BoxGroup("QualityTraits")]  
    public int max;

    [BoxGroup("QualityTraits")]  
    public QualityDescriptionType descriptionType;

    [BoxGroup("QualityTraits")]
    public List<DescriptionStruct> descriptions = new List<DescriptionStruct>();

    public void OnEnable()
    {
        this.val = 0;
    }

    public override string ReturnDescription()
    {
        QualityDescriptionParser descParser = new QualityDescriptionParser();
        return descParser.ReturnDescription(this);
    }

}