using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolveBar : MonoBehaviour
{
    [SerializeField] private GameObject ResolvePip;
    private List<ResolvePip> resolvePips = new List<ResolvePip>();

    private const int ResolvePipCount = 10;

    void Start()
    {
        for(int i=0; i<ResolvePipCount; i++)
        {
            GameObject resolvePip = Instantiate(ResolvePip,  gameObject.transform) as GameObject;
            resolvePips.Add(resolvePip.GetComponent<ResolvePip>());
        }
    }

    public void UpdateResolve(int current, int max)
    {
        float resolvePercentage = current / max;
        UpdateResolve(resolvePercentage);
    }

    public void UpdateResolve(float resolvePercentage)
    {
        int fullPips = Mathf.RoundToInt(resolvePercentage * ResolvePipCount);
        for (int i = 0; i < fullPips; i++)
        {
            resolvePips[i].ResetPip();
        };

        for (int i = fullPips; i < ResolvePipCount; i++)
            resolvePips[i].EmptyPip();
    }
}
