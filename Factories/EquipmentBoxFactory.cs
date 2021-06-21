using UnityEngine;

public class EquipmentBoxFactory : Factory
{

    public UiEquipmentBox GetNewInstance(GameObject parent, EquipmentType equipmentType, string path)
    {
        this.prefab = Resources.Load(path) as GameObject;
        GameObject box = GetNewInstance(parent);
        var boxScript = box.GetComponent<UiEquipmentBox>();
        boxScript.Setup(equipmentType);

        return boxScript;
    }

    public UiEquipmentBox GetNewInstance(GameObject parent, EquipmentType equipmentType, GameObject prefab)
    {
        this.prefab = prefab;
        GameObject box = GetNewInstance(parent);
        var boxScript = box.GetComponent<UiEquipmentBox>();
        boxScript.Setup(equipmentType);

        return boxScript;
    }

}