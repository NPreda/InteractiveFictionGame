using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Custom UI/Choice Skin Data")]
public class ChoiceSkinData :  ButtonSkinData
{
    public Color  inactiveColor = new Color(255,255,255,255);
    public Sprite inactiveBackground;
}