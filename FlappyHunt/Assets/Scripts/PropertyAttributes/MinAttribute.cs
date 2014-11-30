using UnityEngine;


namespace Assets.Scripts.PropertyAttributes 
{
    public class MinAttribute : PropertyAttribute 
    {
        public float Min;

        public MinAttribute(float min) 
        {
            Min = min;
        }
    }
}

