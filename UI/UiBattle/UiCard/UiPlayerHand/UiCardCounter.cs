using System;
using UnityEngine;
using EasyButtons;
using TMPro;

public class UiCardCounter : MonoBehaviour
{
    [SerializeField]public TMP_Text count;

    public void SetCount(int count)
    {
        this.count.text = count.ToString();
    }
}