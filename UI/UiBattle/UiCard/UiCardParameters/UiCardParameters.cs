﻿using UnityEngine;
using EasyButtons;

[CreateAssetMenu(menuName = "Card Config Parameters")]
public class UiCardParameters : ScriptableObject
{
    #region Disable

    [Header("Disable")]
    [Tooltip("How a card fades when disabled.")]
    [SerializeField]
    [Range(0.1f, 1)]
    float disabledAlpha;

    #endregion

    //--------------------------------------------------------------------------------------------------------------
    public float DisabledAlpha
    {
        get => disabledAlpha;
        set => disabledAlpha = value;
    }

    //--------------------------------------------------------------------------------------------------------------

    [Button]
    public void SetDefaults()
    {
        disabledAlpha = 0.5f;

        dissolveSpeed = 2f;

        hoverHeight = 1;
        hoverScale = 1.3f;
        hoverSpeed = 1.5f;

        height = 0.12f;
        spacing = -2;
        bentAngle = 20;

        rotationSpeed = 20;
        movementSpeed = 4;
        scaleSpeed = 7;

        startSizeWhenDraw = 0.05f;
        discardedSize = 0.5f;
    }    
    //--------------------------------------------------------------------------------------------------------------

    #region Effects

    public float DissolveSpeed
    {
        get => dissolveSpeed;
        set => dissolveSpeed = value;
    }

    [Header("Effects")]
    [SerializeField]
    [Tooltip("Speed of dissolve effect")]
    [Range(0, 10f)]
    float dissolveSpeed;


    #endregion
    //--------------------------------------------------------------------------------------------------------------

    #region Hover

    public float HoverHeight
    {
        get => hoverHeight;
        set => hoverHeight = value;
    }

    public float HoverScale
    {
        get => hoverScale;
        set => hoverScale = value;
    }

    [Header("Hover")]
    [SerializeField]
    [Tooltip("How much the card will go upwards when hovered.")]
    [Range(0, 100)]
    float hoverHeight;

    [SerializeField]
    [Tooltip("How much a hovered card scales.")]
    [Range(0.9f, 2f)]
    float hoverScale;

    [SerializeField]
    [Range(0f, 5f)]
    [Tooltip("Speed of a card while it is hovering")]
    float hoverSpeed;

    #endregion

    //--------------------------------------------------------------------------------------------------------------

    #region Bend

    public float Height
    {
        get => height;
        set => height = value;
    }

    public float BentAngle
    {
        get => bentAngle;
        set => bentAngle = value;
    }

    [Header("Bend")]
    [SerializeField]
    [Tooltip("Height factor between two cards.")]
    [Range(0f, 1f)]
    float height;

    [SerializeField]
    [Tooltip("Amount of space between the cards on the X axis")]
    [Range(0f, -5f)]
    float spacing;

    public float Spacing
    {
        get => spacing;
        set => spacing = -value;
    }

    [SerializeField]
    [Tooltip("Total angle in degrees the cards will bend.")]
    [Range(0, 60)]
    float bentAngle;

    #endregion

    //--------------------------------------------------------------------------------------------------------------

    #region Movement

    [Header("Rotation")]
    [SerializeField]
    [Range(0f, 5f)]
    [Tooltip("Speed of a card while it is rotating")]
    float rotationSpeed;

    [Header("Movement")]
    [SerializeField]
    [Range(0f, 5f)]
    [Tooltip("Speed of a card while it is moving")]
    float movementSpeed;

    [Header("Scale")]
    [SerializeField]
    [Range(0f, 5f)]
    [Tooltip("Speed of a card while it is scaling")]
    float scaleSpeed;

    public float HoverSpeed
    {
        get => hoverSpeed;
        set => hoverSpeed = value;
    }

    public float MovementSpeed
    {
        get => movementSpeed;
        set => movementSpeed = value;
    }

    public float RotationSpeed
    {
        get => rotationSpeed;
        set => rotationSpeed = value;
    }

    public float ScaleSpeed
    {
        get => scaleSpeed;
        set => scaleSpeed = value;
    }

    #endregion

    //--------------------------------------------------------------------------------------------------------------

    #region Draw Discard

    [Header("Draw")]
    [SerializeField]
    [Range(0, 1)]
    [Tooltip("Scale when draw the card")]
    float startSizeWhenDraw;

    public float StartSizeWhenDraw
    {
        get => startSizeWhenDraw;
        set => startSizeWhenDraw = value;
    }

    //--------------------------------------------------------------------------------------------------------------

    [Header("Discard")]
    [SerializeField]
    [Range(0, 1)]
    [Tooltip("Scale when discard the card")]
    float discardedSize;

    public float DiscardedSize => discardedSize;

    #endregion
}
