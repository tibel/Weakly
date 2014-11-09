using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Library
{
    public static class TestRunner
    {
        public static Type TestMethodsType
        {
            get { return typeof (TestMethods); }
        }

        public static MethodInfo GetTestMethod(string methodName)
        {
            return TestMethodsType.GetRuntimeMethods().FirstOrDefault(m => m.Name == methodName);
        }
    }
}
