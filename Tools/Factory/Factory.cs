using UnityEngine;

/// <summary>
/// Factory design pattern.
/// </summary>
public class Factory
{
    // Reference to prefab.
    public GameObject prefab;

    
    /// <summary>
    /// Creating new instance of prefab.
    /// </summary>
    /// <returns>New instance of prefab.</returns>
    public GameObject GetNewInstance(GameObject parent)
    {
        return GameObject.Instantiate(prefab, parent.transform);
    }
}