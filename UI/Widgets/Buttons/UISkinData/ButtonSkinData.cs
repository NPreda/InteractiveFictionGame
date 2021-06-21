using UnityEngine;
using TMPro;

[CreateAssetMenu(menuName = "Custom UI/Button Skin Data")]
public class ButtonSkinData :  ScriptableObject
{
    [Header("Button Skin")]
    public Sprite unselectBackground;
    public Sprite unselectIcon;
    public TMP_FontAsset unselectFont;
    public Color unselectColor= new Color(255,255,255,255);

    public Sprite selectBackground;
    public Sprite selectIcon;
    public TMP_FontAsset selectFont;
    public Color selectColor = new Color(255,255,255,255);
}
