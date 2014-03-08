﻿using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Weakly;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Demo.Win8
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private async void OnWeakActionAndWeakFunc(object sender, RoutedEventArgs e)
        {
            var instance = new TestMethods();

            object resultObject = null;
            try
            {
                var method = GetTestMethod("VoidNoParams");
                var action = new WeakAction(instance, method);
                action.Invoke();
            }
            catch (Exception ex)
            {
                resultObject = ex.Message;
            }

            await ShowMessageBox(resultObject, "VoidNoParams");

            try
            {
                var method = GetTestMethod("IntOneParam");
                var function = new WeakFunc<int, int>(instance, method);
                resultObject = function.Invoke(10);
            }
            catch (Exception ex)
            {
                resultObject = ex.Message;
            }

            await ShowMessageBox(resultObject, "VoidNoParams");
        }

        private async void OnDynamicDelegate(object sender, RoutedEventArgs e)
        {
            await InvokeDynamicDelegate(null, "StaticVoidNoParams");
            await InvokeDynamicDelegate(null, "StaticIntNoParams");
            await InvokeDynamicDelegate(null, "StaticVoidOneParam", 1);
            await InvokeDynamicDelegate(null, "StaticIntOneParam", 2);

            var instance = new TestMethods();
            await InvokeDynamicDelegate(instance, "VoidNoParams");
            await InvokeDynamicDelegate(instance, "IntNoParams");
            await InvokeDynamicDelegate(instance, "VoidOneParam", 1);
            await InvokeDynamicDelegate(instance, "IntOneParam", 2);
        }

        private static async Task InvokeDynamicDelegate(object instance, string methodName, params object[] parameters)
        {
            object resultObject;

            try
            {
                var method = GetTestMethod(methodName);
                var function = DynamicDelegate.From(method);
                resultObject = function(instance, parameters);
            }
            catch (Exception ex)
            {
                resultObject = ex.Message;
            }

            await ShowMessageBox(resultObject, methodName);
        }

        private static MethodInfo GetTestMethod(string methodName)
        {
            return typeof (TestMethods).GetRuntimeMethods().FirstOrDefault(m => m.Name == methodName);
        }

        private static async Task ShowMessageBox(object result, string caption)
        {
            var message = (result != null) ? result.ToString() : "null";

            var messageDialog = new MessageDialog(message, caption);
            messageDialog.Commands.Add(new UICommand("OK"));
            messageDialog.DefaultCommandIndex = 0;
            await messageDialog.ShowAsync();
        }
    }
}