using UnityEngine;


namespace Assets.Scripts.PropertyAttributes 
{
    public class FieldRangeAttribute : PropertyAttribute 
    {
        public string MinPropertyNeme;
        public string MaxPropertyNeme;


        public FieldRangeAttribute(string minPropertyName, string maxPropertyName) {
            MinPropertyNeme = minPropertyName;
            MaxPropertyNeme = maxPropertyName;
        }

    }
}

