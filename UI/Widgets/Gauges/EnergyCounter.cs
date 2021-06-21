using UnityEngine;
using TMPro;


public class EnergyCounter : MonoBehaviour
{
    [SerializeField]private TMP_Text energyCounter;    


    void OnEnable()
    {
        Character.OnCharChanged += RefreshUI;
    }


    void OnDisable()
    {
        Character.OnCharChanged -= RefreshUI;
    }


    private void RefreshUI()
    {
        Character character = Character.Instance;
        StatPacket statPacket = character.ReturnStatPacket("Energy");
        energyCounter.text = string.Format("{0}/{1}",statPacket.value,statPacket.maxValue);   //applies value into text
    }
}
