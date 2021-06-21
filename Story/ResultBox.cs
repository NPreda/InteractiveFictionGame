using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ResultBox : MonoBehaviour
{
    public GameObject bodyPanel;
    public TMP_Text body;
    public TMP_Text rewards;
    public UIChoiceButton button;

    public void SetActive(){
        gameObject.SetActive(true);
    }

    public void SetInactive(){
        gameObject.SetActive(false);
    }
}
