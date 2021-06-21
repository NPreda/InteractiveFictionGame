using UnityEngine;

public class SlotFactory : Factory
{
    public ItemSlot GetNewInstance(GameObject parent, Quality item, string path)
    {
        this.prefab = Resources.Load(path) as GameObject;
        GameObject slot = GetNewInstance(parent);
        var slotScript = slot.GetComponent<ItemSlot>();
        slotScript.Setup(item);

        return slotScript;
    }

    public ItemSlot GetNewInstance(GameObject parent, Quality item, GameObject prefab)
    {
        this.prefab = prefab;
        GameObject slot = GetNewInstance(parent);
        var slotScript = slot.GetComponent<ItemSlot>();
        slotScript.Setup(item);

        return slotScript;
    }
}