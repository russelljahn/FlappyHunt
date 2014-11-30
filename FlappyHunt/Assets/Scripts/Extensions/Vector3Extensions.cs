using UnityEngine;


namespace Assets.Scripts.Extensions 
{
    public static class Vector3Extensions 
    {
		public static Vector3 ToVector3(this float[] components) 
        {
		    if (components.Length == 3) {
		        return new Vector3(components[0], components[1], components[2]);
		    }
		    Debug.LogError("Must pass in an array of size 3!");
		    return Vector3.zero;
		}


        public static float[] ToArray(this Vector3 v) {
            return new [] {v.x, v.y, v.z};
        }


        public static Quaternion ToQuaternion(this Vector3 eulerAngle)
        {
            return Quaternion.Euler(eulerAngle);
        }
    }
}
