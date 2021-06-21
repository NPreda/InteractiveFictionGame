using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Tools.UI{

    public enum OrbitType
    {
        Sequencial, 
        Centered
    }

    [ExecuteInEditMode]
    public class OrbitSorter : MonoBehaviour
    {
        [SerializeField] public Transform pivot;
        [SerializeField] private OrbitType orbitType;
        [SerializeField] private float radius = 1f;
        [SerializeField] private float startingAngle = 180f;
        [SerializeField] private float angle = 1f;


        private List<Transform> childObjects = new List<Transform>();

        private List<Transform> _lastChildObjects= new List<Transform>();
        private float _startingAngle;
        private bool _isDirty = false;

        void Update()
        {
            if(!pivot) return;  //pivot is a hard requirement

            childObjects.Clear();
            childObjects = GetChildren();

            if((_lastChildObjects.Count != childObjects.Count) || (_isDirty))
            {
                    //copy latest state
                    _lastChildObjects = childObjects.ToList();

                    //calculate start position depending on sort type
                    if(orbitType == OrbitType.Centered)
                    {
                       _startingAngle = startingAngle - ((_lastChildObjects.Count-1) * angle)/2; 
                    }else {
                       _startingAngle = startingAngle;
                    }

                    //reposition
                    for (int i = 0; i < _lastChildObjects.Count; i++)
                    {   
                        Vector3 newPos = CalculatePosition(i);
                        PositionElement(_lastChildObjects[i], newPos);
                    }
                    _isDirty = false;
            }
        }


        public virtual List<Transform> GetChildren()
        {
            List<Transform> transforms = new List<Transform>();
            foreach (Transform child in this.gameObject.transform)
            {
                if(child != pivot)
                    transforms.Add(child);
            }
            return transforms;
        }
        private Vector3 CalculatePosition(int elementCount)
        {
            float radPosition = (_startingAngle + (elementCount * angle)) * Mathf.Deg2Rad;
            float xPosition = pivot.position.x + Mathf.Cos( radPosition )*radius;
            float yPosition = pivot.position.y + Mathf.Sin( radPosition )*radius;
            float zPosition = pivot.position.z;
            Vector3 newPos = new Vector3(xPosition,yPosition,zPosition);        
            return newPos;    
        }

        public virtual void PositionElement(Transform element, Vector3 newPos)
        {
            element.position = newPos;
        }

        private void Refresh()
        {
            _isDirty = true;
            _lastChildObjects.Clear();
        }

        private void OnValidate ()
        {
            Refresh();
        }

    }
}