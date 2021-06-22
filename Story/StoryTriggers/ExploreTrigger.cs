
using UnityEngine;

[CreateAssetMenu(fileName = "ExploreTrigger", menuName= "StoryElements/Triggers/Explore Trigger")]
public class ExploreTrigger : StoryTrigger
{
    public ExploreLocation exploreLocation;  //the area being explored
    public int startingPathProgress = 0;     //the initial progress bonus if leaving from a town or a city
}
