using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ExploreType{
    Normal = 0,
    Barrier = 1,
    Danger = 2
}

public enum ExploreLocation{
    Test
}

[CreateAssetMenu(fileName = "New Card", menuName= "Cards/Explore")]
public class ExploreCard : Card
{
    public Storylet storylet;
    public ExploreLocation location;
    public ExploreType type;
}
