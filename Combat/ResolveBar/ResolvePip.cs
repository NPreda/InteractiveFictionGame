using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Image))]
public class ResolvePip : MonoBehaviour
{
    public Image imageComponent;
    public Sprite fullState;
    public Sprite emptyState;

    public void Start()
    {}
    
    public void ResetPip() => imageComponent.sprite = fullState;

    public void EmptyPip() => imageComponent.sprite = emptyState;
}
