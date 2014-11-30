using UnityEngine;


namespace Assets.Scripts.Extensions
{
    public static class QuaternionExtensions
    {
        public static Vector4 ToVector4(this Quaternion q)
        {
            return new Vector4(q.x, q.y, q.z, q.w);
        }
    }
}
