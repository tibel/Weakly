using System;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Demo.Library;
using Weakly;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Weakly.Builders;

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
            var instance = Activator.CreateInstance(TestRunner.TestMethodsType);

            object resultObject = null;
            try
            {
                var method = TestRunner.GetTestMethod("VoidNoParams");
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
                var method = TestRunner.GetTestMethod("IntOneParam");
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

            var instance = Activator.CreateInstance(TestRunner.TestMethodsType);
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
                var method = TestRunner.GetTestMethod(methodName);
                var function = Builder.DynamicDelegate.BuildDynamic(method);
                resultObject = function(instance, parameters);
            }
            catch (Exception ex)
            {
                resultObject = ex.Message;
            }

            await ShowMessageBox(resultObject, methodName);
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
