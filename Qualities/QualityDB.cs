using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class QualityDB : MonoBehaviour
{
    [SerializeField]
    public List<Quality>  qualities;


    //Instance Control
    //variables to help global instance selection
    private static QualityDB  m_Instance;
    public static QualityDB  Instance { get { return m_Instance; } }

    
    void Awake()
    { 
        m_Instance = this;          //Get this instance
    }
    
    public void Setup()
    {
        UnityEngine.Object[] qualityObjects = Resources.LoadAll("Qualities");
        Quality[] temp = new Quality[qualityObjects.Length];
        qualityObjects.CopyTo(temp, 0);
        qualities = temp.ToList<Quality>();
    }

    public bool inDatabase(String id)
    {
        bool result = false;
        foreach(var q in qualities){
            if(q.id == id){
                result = true;
            }
        }
        return result;
    }

    public Quality returnQuality(String id)
    {
        foreach(var q in qualities){
            if(q.id == id){
               return q;
            }
        }
        return null;
    }

    public int GetValue(String id)              //get how much of a quality there is. 
    {
        Quality quality = returnQuality(id);
        if(quality == null)
        {
            throw new Exception("Quality '"+ id +"' not found inside the DB");
        }
        else if(quality is GenericQuality)
        {
            var generic = (GenericQuality) quality;
            return generic.val;
        }
        else
        {
            return 1;
        }
    }
}
