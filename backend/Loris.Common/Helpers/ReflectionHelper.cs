using System;
using System.Reflection;

namespace Loris.Common.Helpers
{
    public static class ReflectionHelper
    {
        public static object ExecuteMethod(object classInstance, string methodName, params object[] paramsMethod)
        {
            var methodInfo = classInstance.GetType().GetMethod(methodName);
            if (methodInfo == null)
            {
                throw new Exception(string.Format("Método {0} inexistente na classe {1}", methodName, classInstance));
            }
            var result = methodInfo.Invoke(classInstance, (paramsMethod.Length == 0) ? null : paramsMethod);

            return result;
        }

        public static object GetInstance(string assemblyName, string className, params object[] paramsClass)
        {
            var assembly = Assembly.Load(assemblyName);
            var adoDacType = assembly.GetType(className, true, true);
            object classInstance = Activator.CreateInstance(adoDacType, args: (paramsClass.Length == 0) ? null : paramsClass);

            return classInstance;
        }

        public static object GetInstance(string strFullyQualifiedName)
        {
            Type type = Type.GetType(strFullyQualifiedName);
            if (type != null)
            {
                return Activator.CreateInstance(type);
            }

            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                type = asm.GetType(strFullyQualifiedName);
                if (type != null)
                {
                    return Activator.CreateInstance(type);
                }
            }
            return null;
        }
    }
}
