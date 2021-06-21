using UnityEngine;
using System;

namespace Tools.UI{
    public class FloatingEffect : MonoBehaviour
    {
        [SerializeField] float floatRadius;
        [SerializeField] float speed;

        private Vector3 parentPosition;
        private bool isActive = false;

        void Start()
        {
            parentPosition = this.transform.parent.transform.position;
            isActive = true;
            StartMoving();
        }

        public void MoveTo(Vector3 position, float speed, Action method = null)   =>  LeanTween.moveLocal(gameObject, position, speed).setOnComplete(method) ;

        private void StartMoving()
        {
            if(isActive)
            {
                Vector3 newPos = GetNewPosition();
                MoveTo(newPos, speed, StartMoving);
            }
        }


        private Vector3 GetNewPosition()
        {
            int degree = UnityEngine.Random.Range(0,360);
            float radian = degree*Mathf.Deg2Rad;
            float x = Mathf.Cos(radian);
            float y = Mathf.Sin(radian);
            Vector3 direction = new Vector3 (x,y,0)*floatRadius;
            Vector3 newPosition = parentPosition + direction;
            return newPosition;
        }
    }
}