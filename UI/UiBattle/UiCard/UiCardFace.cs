using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UiCardFace : MonoBehaviour
{    
    //--------------------------------------------------------------------------------------------------------------

    #region PrivateReferences
    [SerializeField]  private Image background;
    [SerializeField]  private Image effectImage;

    [SerializeField]  private Image frameGlow;
    [SerializeField]  private Image frame;

    [SerializeField]  private Image typeFrame;
    [SerializeField]  private Image typeIcon;
    [SerializeField]  private Image costFrame;

    [SerializeField]  private TMP_Text faceTitle;
    [SerializeField]  private TMP_Text faceDesc;
    [SerializeField]  private TMP_Text faceCost;


    #endregion

    //--------------------------------------------------------------------------------------------------------------

    public void LoadImage(Image image, Sprite skinSprite)
    {
        if(skinSprite != null)
        {
            image.gameObject.SetActive(true);
            image.sprite = skinSprite;
        }else{
            image.gameObject.SetActive(false);
        }
    }

    public void LoadFace(UiCardSkin cardData, Sprite cardImage, string title, string desc, string cost)
    {
        LoadImage(background, cardData.background);
        LoadImage(effectImage, cardImage);
        LoadImage(frameGlow, cardData.frameGlow);
        LoadImage(frame, cardData.frame);
        LoadImage(typeFrame, cardData.typeFrame);
        LoadImage(typeIcon, cardData.typeIcon);
        LoadImage(costFrame, cardData.costFrame);

        faceTitle.text = title;
        faceDesc.text = desc;
        faceCost.text = cost;
    }

    public void LoadFace(UiCardSkin cardData, Sprite cardImage, string title, string desc)
    {
        LoadImage(background, cardData.background);
        LoadImage(effectImage, cardImage);
        LoadImage(frameGlow, cardData.frameGlow);
        LoadImage(frame, cardData.frame);

        faceTitle.text = title;
        faceDesc.text = desc;

        costFrame.gameObject.SetActive(false);
        faceCost.gameObject.SetActive(false);
        typeFrame.gameObject.SetActive(false);
        typeIcon.gameObject.SetActive(false);
    }


    public void LoadFace(Sprite cardImage, string title, string desc, string cost)
    {
        LoadImage(effectImage, cardImage);
        faceTitle.text = title;
        faceDesc.text = desc;
        faceCost.text = cost;
    }

    public void LoadFace(Sprite cardImage, string title, string desc)
    {
        LoadImage(effectImage, cardImage);
        faceTitle.text = title;
        faceDesc.text = desc;
        costFrame.gameObject.SetActive(false);
        faceCost.gameObject.SetActive(false);
        typeFrame.gameObject.SetActive(false);
        typeIcon.gameObject.SetActive(false);
    }

}
