using TMPro;
using UnityEngine;

public class UiEquipmentBox : MonoBehaviour
{
    [SerializeField]private TMP_Text title;
    [SerializeField]private EquipmentSlot equipmentSlot;
    [SerializeField]private UiEquipmentPanel equipmentPanel;

    public EquipmentType equipmentType;

    public void Setup(EquipmentType equipmentType)
    {
        title.text = equipmentType.ToString();
        equipmentSlot.Setup(equipmentType);
        equipmentPanel.ShowItems(equipmentType);
    }

    public void Destroy() => Destroy(this.gameObject);

}