using UnityEngine;

public class ButtonFactory : Factory
{

    public UIButton GetNewInstance(GameObject parent, string name, string path)
    {
        this.prefab = Resources.Load(path) as GameObject;
        GameObject categoryButton = GetNewInstance(parent);
        var buttonScript = categoryButton.GetComponent<UIButton>();
        buttonScript.Populate(name);

        return buttonScript;
    }

    public UIButton GetNewInstance(GameObject parent, string name, GameObject prefab)
    {
        this.prefab = prefab;
        GameObject categoryButton = GetNewInstance(parent);
        var buttonScript = categoryButton.GetComponent<UIButton>();
        buttonScript.Populate(name);

        return buttonScript;
    }

}