using UnityEngine;

public class UnitFactory: MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject unitPrefab;

    public ITarget LoadPlayer()
    {

        GameObject unitObject = Instantiate(playerPrefab, new Vector3(0, 0, 0), Quaternion.identity, gameObject.transform) as GameObject;                             //gets the enemy prefab
        IUiPlayer pScript = unitObject.GetComponent<IUiPlayer>();              //gets the prefab script that handles its variables
        CombatBlackboard.Player = pScript;
        pScript.Initialize("Test Player", TargetType.Player);                                           //load the choice settings into the prefab script
        pScript.Idle();
        return pScript;        
    }

    public ITarget Load(Unit inputUnit, TargetType allegiance)
    {
        //Creates units in their respective position with the unit + position information
        GameObject unitObject = Instantiate(unitPrefab, new Vector3(0, 0, 0), Quaternion.identity, gameObject.transform) as GameObject;                             //gets the enemy prefab
        IUiUnit uScript = unitObject.GetComponent<IUiUnit>();              //gets the prefab script that handles its variables
        uScript.Initialize(inputUnit.unitName, allegiance, inputUnit);                                           //load the choice settings into the prefab script

        if(allegiance == TargetType.Friend) CombatBlackboard.AddCompanion(uScript);
        else CombatBlackboard.AddEnemy(uScript);

        uScript.Idle();
        return uScript;
    }
}