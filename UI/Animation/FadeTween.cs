using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FadeTween : MonoBehaviour
{

    public void StartTextFade(){
        TMP_Text text = gameObject.GetComponent<TMP_Text>();
        FadeAlphaText(text,0, 1).setOnComplete(OnDestroy);
    }


    void OnDestroy(){
        Destroy(gameObject);
    }

    public LTDescr FadeAlphaText(TMP_Text textMesh, float to, float time)
    {
        var _color = textMesh.color;
        var _tween = LeanTween
            .value(textMesh.gameObject, _color.a, to, time)
            .setOnUpdate((float _value) =>
            {
                _color.a = _value;
                textMesh.color = _color;
            });
        return _tween;
    }
}
