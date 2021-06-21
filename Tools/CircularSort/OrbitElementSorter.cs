using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools.UI;

namespace Tools.UI{
    public class OrbitElementSorter : OrbitSorter
    {
        private const float moveTime = 1f;

        public override List<Transform> GetChildren()
        {
            List<Transform> transforms = new List<Transform>();
            foreach (Transform child in this.gameObject.transform)
            {
                if(child != pivot && child.gameObject.GetComponent<OrbitElement>() != null)
                {
                    transforms.Add(child);
                }
            }
            return transforms;
        }

        public override void PositionElement(Transform element, Vector3 newPos)
        {
            if (Application.isPlaying) {
                element.gameObject.GetComponent<OrbitElement>().MoveTo(newPos, moveTime, null);
            } else {
                element.position = newPos;
            }             
        }
    }
}

