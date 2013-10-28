using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Weakly;

namespace Demo.Silverlight
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnWeakActionAndWeakFunc(object sender, RoutedEventArgs e)
        {
            var instance = new TestMethods();

            try
            {
                var method = typeof(TestMethods).GetMethod("VoidNoParams", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                var action = new WeakAction(instance, method);
                action.Invoke();

                MessageBox.Show("null", "VoidNoParams", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "VoidNoParams", MessageBoxButton.OK);
            }

            try
            {
                var method = typeof(TestMethods).GetMethod("IntOneParam", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                var function = new WeakFunc<int, int>(instance, method);
                var result = function.Invoke(10);

                MessageBox.Show(result.ToString(), "IntOneParam", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "VoidNoParams", MessageBoxButton.OK);
            }
        }

        private void OnDynamicDelegate(object sender, RoutedEventArgs e)
        {
            InvokeDynamicDelegate(null, "StaticVoidNoParams");
            InvokeDynamicDelegate(null, "StaticIntNoParams");
            InvokeDynamicDelegate(null, "StaticVoidOneParam", 1);
            InvokeDynamicDelegate(null, "StaticIntOneParam", 2);

            var instance = new TestMethods();
            InvokeDynamicDelegate(instance, "VoidNoParams");
            InvokeDynamicDelegate(instance, "IntNoParams");
            InvokeDynamicDelegate(instance, "VoidOneParam", 1);
            InvokeDynamicDelegate(instance, "IntOneParam", 2);
        }

        private static void InvokeDynamicDelegate(object instance, string methodName, params object[] parameters)
        {
            try
            {
                var method = typeof(TestMethods).GetMethod(methodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
                var function = DynamicDelegate.From(method);
                var result = function(instance, parameters);

                var resultText = (result != null) ? result.ToString() : "null";
                MessageBox.Show(resultText, methodName, MessageBoxButton.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, methodName, MessageBoxButton.OK);
            }
        }

        private bool _weakEventRegistered;

        private void OnWeakEventHandler(object sender, RoutedEventArgs e)
        {
            if (!_weakEventRegistered)
            {
                _weakEventRegistered = true;
                var eventInfo = sender.GetType().GetEvent("Click");
                DynamicWeakEventHandler.Register<RoutedEventArgs>(sender, eventInfo, OnWeak);

                MessageBox.Show("Weak handler registered. Click again to test it.", "Weak Handler", MessageBoxButton.OK);
            }
        }

        private void OnWeak(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Hello from weak handler", "Weak Handler", MessageBoxButton.OK);
        }
    }
}
