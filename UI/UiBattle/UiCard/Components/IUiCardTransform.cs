using UnityEngine;
using System;

//     Interface for simple Transform operations.
public interface IUiCardTransform
{
    //Move to position relative to parent
    void MoveTo(Vector3 position, float speed, Action method = null)  ;

    //Move along the y axis
    void MoveY(float distance, float speed, Action method= null)  ;

    //Rotate in the 3d space.
    void RotateTo(Vector3 rotation, float speed, Action method= null)  ;

    //Scale in the 3d space.
    void ScaleTo(Vector3 scale, float speed, Action method= null)  ;

    //fades between 0-1
    void Fade(float alpha, float speed, Action method= null)  ;

    //cancels existing animnations:
    void CancelAnimation();

    //checks if the animation is still running
    bool IsAnimating();
}
