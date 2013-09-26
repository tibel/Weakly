using System.Diagnostics;

namespace Demo.Silverlight
{
    internal class TestMethods
    {
        #region static

        private static void StaticVoidNoParams()
        {
            Debug.WriteLine("StaticVoidNoParams");
        }

        private static int StaticIntNoParams()
        {
            return 11;
        }

        private static void StaticVoidOneParam(int echo)
        {
            Debug.WriteLine("StaticVoidOneParam {0}", echo);
        }

        private static int StaticIntOneParam(int add)
        {
            return 11 + add;
        }

        #endregion

        #region instance

        private void VoidNoParams()
        {
            Debug.WriteLine("VoidNoParams");
        }

        private int IntNoParams()
        {
            return 17;
        }

        private void VoidOneParam(int echo)
        {
            Debug.WriteLine("VoidOneParam {0}", echo);
        }

        private int IntOneParam(int add)
        {
            return 3 + add;
        }

        #endregion
    }
}
