public enum QualityDescriptionType{
    Default,        //just gives the default
    Strict,         //gives the description at the exact value, gives error if value has no description
    Tiered,         //gives the description of the lowest tier value
    Forgiving       //same as Strict, but instead of an error, it just returns the default value
}