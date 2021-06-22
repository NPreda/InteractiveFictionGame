using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class StoryletCache 
{

    public static List<Storylet> storyletBacklog = new List<Storylet>();

    public static StoryletType storyletType;

    public static Sprite image;
    public static string title;
    public static string body;

    public static List<Choice> choices = new List<Choice>();   

    public static void StoreStorylet(Storylet storylet)
    {
        //let's break it down
        storyletType = storylet.storyletType;
        image = storylet.sideImage;
        title = storylet.title;
        body = storylet.body;
        choices.Clear();
        choices = storylet.choices.ToList();

        storyletBacklog.Add(storylet);  //store the thing to make a dump late

        if (storyletBacklog.Count == 10) //let's not story too much
        {
            storyletBacklog.RemoveAt(0);
        }

    }
}