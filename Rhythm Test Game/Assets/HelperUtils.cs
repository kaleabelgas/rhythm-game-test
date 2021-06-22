using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Utils
{
    public static class HelperUtils
    {
        public static float Remap(this float from, float fromMin, float fromMax, float toMin, float toMax)
        {
            var fromAbs = from - fromMin;
            var fromMaxAbs = fromMax - fromMin;

            var normal = fromAbs / fromMaxAbs;

            var toMaxAbs = toMax - toMin;
            var toAbs = toMaxAbs * normal;

            var to = toAbs + toMin;

            return to;
        }

        public static float SnapToNumber(float toSnap, float factor)
        {
            return Mathf.Round(toSnap / factor) * factor; 
        }


        public static Vector2 Rotate(this Vector2 v, float degrees)
        {
            float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
            float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

            float tx = v.x;
            float ty = v.y;
            v.x = (cos * tx) - (sin * ty);
            v.y = (sin * tx) + (cos * ty);
            return v;
        }


        public static float TriangleWave(float x)
        {
            return Mathf.Abs((x % 4) - 2) - 1;
        }

        public static Vector3 ProjectLocal(this Transform thisTransform, Vector3 vector, Vector3 onNormal)
        {
            var notePosRelativeToHitbar = thisTransform.InverseTransformPoint(vector);

            var pointPerpendicularToNoteLocal = Vector3.Project(notePosRelativeToHitbar, onNormal);

            var pointPerpendicularToNoteGlobal = thisTransform.TransformPoint(pointPerpendicularToNoteLocal);

            return pointPerpendicularToNoteGlobal;
        }

        public static float GetClosest(this List<float> thisList, float toCompare)
        {
            var closestItem = thisList[thisList.Count - 1];

            var smallestDifference = thisList[thisList.Count - 1];

            foreach (var item in thisList)
            {
                var difference = Mathf.Abs(item - toCompare);

                if(difference < smallestDifference)
                {
                    smallestDifference = difference;
                    closestItem = item;
                }
            }
            return closestItem;
        }
    }
}
