using System.Collections.Generic;
using System.Linq;

public class QualityDescriptionParser
{
    public string ReturnDescription(GenericQuality quality)
    {
        var descType = quality.descriptionType;
        DescriptionStruct descStruct = null;

        switch(descType)
        {
            case QualityDescriptionType.Strict:
                descStruct = quality.descriptions.FirstOrDefault(x => x.key == quality.val);
                if(descStruct != null)  return descStruct.desc;
                else return "<color=red> The description for the given value could not be found </color>";
            case QualityDescriptionType.Forgiving:
                descStruct = quality.descriptions.FirstOrDefault(x => x.key == quality.val);
                if(descStruct != null)  return descStruct.desc;
                else return quality.defaultDesc;           
            case QualityDescriptionType.Tiered:
                return TieredSearch(quality.descriptions, quality.val, quality.defaultDesc);
            default:
                return quality.defaultDesc;
        }
    }

    // Sort the list 
    public string TieredSearch(List<DescriptionStruct> descriptions, int value, string defaultDesc)
    {
        //Sort the list
        List<DescriptionStruct> sortedList= descriptions.OrderBy(x => x.key).ToList();

        // ... And search for the closest desc
        DescriptionStruct descStruct = sortedList.Where(x => x.key < value).Max();
        string desc = descStruct != null ? descStruct.desc :  defaultDesc ;

        return desc;
    }
}