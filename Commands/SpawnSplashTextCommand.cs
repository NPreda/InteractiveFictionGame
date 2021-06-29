using UnityEngine;
using System;
using TMPro;

public class SpawnSplashTextCommand: Command
{
    private string displayText;
    private Color displayColor;

    public SpawnSplashTextCommand(string displayText, Color displayColor)
    {        
        this.displayText = displayText;
        this.displayColor = displayColor;
    }

    public override void StartCommandExecution()
    {
        GameObject textPrefab = Resources.Load("Prefabs/BattleUI/unitPopText") as GameObject;
        GameObject resSplash = GameObject.Instantiate(textPrefab,  SceneControl.Instance.mainCanvas.transform) as GameObject;
        var textComponent = resSplash.GetComponent<TMP_Text>();
        textComponent.color = displayColor;;
        textComponent.text = displayText;       
        resSplash.GetComponent<FadeTween>().StartTextFade();   

        DelayAction(1f);    //let anymation play

        CommandExecutionComplete();
    }
}
