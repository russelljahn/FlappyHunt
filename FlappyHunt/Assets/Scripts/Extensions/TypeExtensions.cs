using System;


namespace Assets.Scripts.Extensions 
{
    public static class TypeExtensions 
    {
        public static object GetDefaultValue(this Type type) 
        {
            if (!type.IsValueType || Nullable.GetUnderlyingType(type) != null)
            {
                return null;
            }
            return Activator.CreateInstance(type);
        }
    }
}
