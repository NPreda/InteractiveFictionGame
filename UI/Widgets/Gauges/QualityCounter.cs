using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QualityCounter : MonoBehaviour
{
    [SerializeField]private string qualityID;
    [SerializeField]private TMP_Text qualityCounter;
    [SerializeField]private Image qualityIcon;
    private int value;   
    private bool _isDirty; 

    public void Awake()
    {
        GameManager.BackendInitialized += Load;
    }

    void Load()
    {
        //check if the quality is actually something that exists
        if(QualityDB.Instance.inDatabase(qualityID) == false)
            throw new System.Exception("Quality with id '"+ qualityID +"' could not be found in DB");
            
        Inventory.OnInvChanged += RefreshUI;
        Dirty();
    }

    private void Dirty() => _isDirty = true;

    private void RefreshUI()
    {
        Inventory inventory = Inventory.Instance;

        //get value
        value = QualityDB.Instance.GetValue(qualityID);
        qualityCounter.text = value.ToString();

        //get icon
        if(qualityIcon) qualityIcon.sprite = QualityDB.Instance.returnQuality(qualityID).icon;

        _isDirty = false;
    }

    public void Update()
    {
        if(_isDirty) RefreshUI();
    }
}
