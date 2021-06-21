using UnityEngine;
using TMPro;


public class SpawnFloatingNumberCommand: Command
{
    private ITarget target;
    private int value;
    private Color color;

    public SpawnFloatingNumberCommand(DamageInfo damageInfo)
    {        
        this.target = damageInfo.target;
        this.value = damageInfo.value;
        this.color = Color.red;
    }

    public SpawnFloatingNumberCommand(HealInfo healInfo)
    {        
        this.target = healInfo.target;
        this.value = healInfo.value;
        this.color = Color.green;
    }

    public override void StartCommandExecution()
    {
        var prefab = Resources.Load("Prefabs/BattleUI/CountFloat");
        var floater = GameObject.Instantiate(prefab,target.gameObject.transform) as GameObject;
        floater.transform.localPosition = new Vector3(0,0,0);
        var textComponent = floater.GetComponent<TMP_Text>();
        textComponent.color = this.color;
        textComponent.text = this.value.ToString();
        CommandExecutionComplete();
    }
}
