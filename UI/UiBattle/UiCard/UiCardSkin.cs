using UnityEngine;

[CreateAssetMenu(menuName = "UI Card Skin")]
public class UiCardSkin :  ScriptableObject
{
    [Header("UI Card Skin Data")]
	public Sprite background;
    public Sprite imageReplacer;
    public Sprite frameGlow;
    public Sprite frame;
    public Sprite typeFrame;
    public Sprite costFrame;
    public Sprite typeIcon;
}
