using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCrewUtils
{
    public class Utils : Singleton<Utils>
    {
        public float GetAnglesFromVector(Vector3 vectorToCheck)
        {
            float angleInRadians = Mathf.Atan2(vectorToCheck.y, vectorToCheck.x);
            float angleInDegrees = angleInRadians * Mathf.Rad2Deg;
            return angleInDegrees;
        }

        [Tooltip("X for fliping through x axis, Y is similar")]
        public void FlipObject(Transform objectFilp, Transform targetObject, string axis)
        {
            if (axis.Equals("X"))
            {
                if (objectFilp.position.x < targetObject.position.x)
                    objectFilp.localScale = new Vector3(-1, 1, 1);
                else objectFilp.localScale = new Vector3(1, 1, 1);
            }
            else if (axis.Equals("Y"))
            {
                if (objectFilp.position.y < targetObject.position.y)
                    objectFilp.localScale = new Vector3(1, -1, 1);
                else objectFilp.localScale = new Vector3(1, 1, 1);
            }
        }
    }
}