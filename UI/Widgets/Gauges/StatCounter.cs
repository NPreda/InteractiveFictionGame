using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatCounter : MonoBehaviour
{
    [SerializeField]private RPGStatType statType;
    [SerializeField]private TMP_Text statName;
    [SerializeField]private TMP_Text statCounter;
    [SerializeField]private TMP_Text statMax;
    private int value;   
    private bool _isDirty; 

    public void Awake()
    {
        GameManager.BackendInitialized += Load;
    }

    void Load()
    {
        //check if the quality is actually something that exists
        Character.OnCharChanged += Dirty;
        Dirty();
    }

    private void Dirty() => _isDirty = true;

    private void RefreshUI()
    {
        //get value
        value = Character.Instance.ReturnStatPacket(statType.ToString()).value;

        if(statName != null)statName.text = statType.ToString() + ":";
        statCounter.text = value.ToString();

        if(statMax != null)statMax.text = Character.Instance.ReturnStatPacket(statType.ToString()).maxValue.ToString();


        _isDirty = false;
    }

    public void Update()
    {
        if(_isDirty) RefreshUI();
    }
}
