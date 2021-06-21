using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Unit", menuName= "Combat/Unit")]
public class Unit : ScriptableObject
{
    [HideInInspector]public string id{get => name;}
    public string unitName;
    [TextArea(25,7)] public string unitDescription;
    public Sprite icon;
    [SerializeReference] public UnitAI unitBehaviour;

    [SerializeField] public int maxHP;      
    [SerializeField] public int strength;
    [SerializeField] public int speed;

    [SerializeField] public Storylet enemyWin; 
    [SerializeField] public Storylet enemyLoss;


    [SerializeField] public List<string> barkStrings;


}
