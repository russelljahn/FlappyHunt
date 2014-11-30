using UnityEngine;


namespace Assets.Scripts.Extensions 
{
    public static class Vector4Extensions 
    {
		public static Quaternion ToQuaternion(this Vector4 v) 
        {
			return new Quaternion(v.x, v.y, v.z, v.w);
		}
    }
}
