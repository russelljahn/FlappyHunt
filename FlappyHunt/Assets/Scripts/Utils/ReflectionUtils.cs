using System;
using System.Reflection;
using Assets.Scripts.Extensions;


namespace Assets.Scripts.Utils {
    public static class ReflectionUtils {

        public static TMethodSignatureDelegate GetMethod<TMethodSignatureDelegate>(object state, string methodName) where TMethodSignatureDelegate : class {
            const BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public |
                                              BindingFlags.NonPublic | BindingFlags.InvokeMethod;
            // Check method by name exists
            var methodInfo = state.GetType().GetMethod(methodName, bindingFlags);
            if (methodInfo.IsNull()) {
                return null;
            }

            // Validate signature. Returns null upon incompatible signature
            var methodDelegate = Delegate.CreateDelegate(typeof(TMethodSignatureDelegate), state, methodInfo, false);
            return methodDelegate as TMethodSignatureDelegate;
        }

    }
}
