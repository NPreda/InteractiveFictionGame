using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools.UI;
using TMPro;
using UnityEngine.UI;


//    Main components of an UI card.
public interface IUiCardComponents 
{
    IMouseInput Input { get; }
    GameObject gameObject { get; }
    Transform transform { get; }
    LineRenderer LineRenderer { get; }
}
