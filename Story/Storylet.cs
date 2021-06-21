using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

[CreateAssetMenu(fileName = "storylet", menuName= "StoryElements/Storylet")]
public class Storylet : StoryTrigger
{
    [HideInInspector]public string id{get => name;}
    [SerializeField] public string title;
    [TextArea(25,8)] public string body;
    [SerializeField] public List<Choice> choices = new List<Choice>();    

    private void OnValidate(){
        foreach (var c in choices){       //if something changes in inspector go through all it's elements and clean up
            if (!c.conditional){
                c.conditions = null;      //deletes conditions if the choice is not conditional
            }

            if (!c.contested){
                c.contest = null;        //deletes the contest if the choice is not contested
            }else{
                c.defaultResult = null;   //deletes the default result if there is no success/fail test
            }   
        }
    }
}
