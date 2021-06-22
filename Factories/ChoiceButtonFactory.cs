using UnityEngine;

public class ChoiceButtonFactory : Factory
{

    public UIChoiceButton GetNewInstance(GameObject parent, Choice choice , string path)
    {
        this.prefab = Resources.Load(path) as GameObject;

        GameObject choiceButton = GetNewInstance(parent);
        UIChoiceButton  choiceScript = choiceButton.GetComponent<UIChoiceButton>();              //gets the prefab script that handles its variables
        choiceScript.Populate(choice);                                           //load the choice settings into the prefab script

        return choiceScript;
    }

    public UIChoiceButton GetNewInstance(GameObject parent, Choice choice, GameObject prefab)
    {
        this.prefab = prefab;

        GameObject choiceButton = GetNewInstance(parent);
        UIChoiceButton  choiceScript = choiceButton.GetComponent<UIChoiceButton>();              //gets the prefab script that handles its variables
        choiceScript.Populate(choice);                                           //load the choice settings into the prefab script

        return choiceScript;
    }

}