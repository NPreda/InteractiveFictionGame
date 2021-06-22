using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShieldScript : MonoBehaviour
{
    public int armorValue;

    [SerializeField] private float duration = 0.5f;

    [SerializeField] private Sprite shield;
    [SerializeField] private Sprite brokenShield;
    [SerializeField] private Image displayShield;
    [SerializeField] private TMP_Text displayValue;

    private int _armor = 0;
    private Image imageComponent;


    public void Start()
    {
        displayShield.gameObject.SetActive(false);
        displayValue.gameObject.SetActive(false);        
    }

    void Update()
    {
        if(_armor != armorValue)
            StartCoroutine ("Count");
        else   
            StopCoroutine ("Count");
    }

    IEnumerator Count()
    {
        int start = _armor;
        for (float timer = 0; timer < duration; timer += Time.deltaTime) {
            float progress = timer / duration;
            _armor = (int)Mathf.Lerp (_armor, armorValue, progress);
            RefreshUI();
            yield return null;
        }
        _armor = armorValue;
        RefreshUI();
    }


    void RefreshUI()
    {
        if(_armor == 0)
        {
            //displayShield.sprite = brokenShield;
            displayShield.gameObject.SetActive(false);
            displayValue.gameObject.SetActive(false);
        }else{
            displayShield.sprite = shield;
            displayShield.gameObject.SetActive(true);
            displayValue.gameObject.SetActive(true);            
        }
        displayValue.text = _armor.ToString();
    }

}
