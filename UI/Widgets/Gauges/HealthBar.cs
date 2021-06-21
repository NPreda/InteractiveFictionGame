using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Transform bar;

    void Start()
    {
        bar.localScale = new Vector3(1f, 1f, 1f);

    }

    public void SetSize(int current, int max){
        float sizeNormalized = (float)current/(float)max;
        bar.localScale = new Vector3(sizeNormalized, 1f, 1f);
    }

    public void SetSize(float sizeNormalized){
        bar.localScale = new Vector3(sizeNormalized, 1f, 1f);
    }

}
