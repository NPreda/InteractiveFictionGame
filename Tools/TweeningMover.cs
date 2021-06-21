using UnityEngine;
using System;

//Granuler motion of attached transform
namespace Tools.UI
{
    public class TweeningMover : MonoBehaviour
    {
        public CanvasGroup canvasGroup;
        public bool isActive = false;

        public virtual void Awake()
        {
            canvasGroup = gameObject.GetComponent<CanvasGroup>();
        }

        public virtual void Enable()
        {
            isActive = true;
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
        }

        public virtual void Disable()
        {
            isActive = false;
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
        }

        public bool IsAnimating() =>  LeanTween.isTweening(gameObject);

        public void ScaleTo(Vector3 scale, float speed, Action method = null)  => LeanTween.scale(gameObject, scale, speed).setOnComplete(method) ;

        public void MoveToGlobal(Vector3 position, float speed, Action method = null)   =>  LeanTween.move(gameObject, position, speed).setOnComplete(method) ;

        public void MoveTo(Vector3 position, float speed, Action method = null)   =>  LeanTween.moveLocal(gameObject, position, speed).setOnComplete(method) ;
    
        public void MoveY(float distance, float speed, Action method = null)   => LeanTween.moveY(gameObject, gameObject.transform.position.y + distance, speed).setOnComplete(method) ;

        public void RotateTo(Vector3 rotation, float speed, Action method = null)   => LeanTween.rotate(gameObject, rotation, speed).setOnComplete(method) ;

        public void Fade(float alpha, float speed, Action method = null)   =>  LeanTween.alphaCanvas(GetComponent<CanvasGroup>(), alpha, speed).setOnComplete(method) ;

        public void CancelAnimation() => LeanTween.cancel(gameObject, false);

    }
}