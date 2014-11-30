using UnityEngine;


namespace Assets.Scripts.PropertyAttributes
{
    public class MaxAttribute : PropertyAttribute
    {
        public float Max;

        public MaxAttribute(float max)
        {
            Max = max;
        }
    }
}
