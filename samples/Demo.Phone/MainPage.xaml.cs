using Demo.Library;
using Microsoft.Phone.Controls;
using System;
using System.Windows;
using Weakly;

namespace Demo.Phone
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}

        private void OnWeakActionAndWeakFunc(object sender, RoutedEventArgs e)
        {
            var instance = Activator.CreateInstance(TestRunner.TestMethodsType);

            try
            {
                var method = TestRunner.GetTestMethod("VoidNoParams");
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
                var method = TestRunner.GetTestMethod("IntOneParam");
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

            var instance = Activator.CreateInstance(TestRunner.TestMethodsType);
            InvokeDynamicDelegate(instance, "VoidNoParams");
            InvokeDynamicDelegate(instance, "IntNoParams");
            InvokeDynamicDelegate(instance, "VoidOneParam", 1);
            InvokeDynamicDelegate(instance, "IntOneParam", 2);
        }

        private static void InvokeDynamicDelegate(object instance, string methodName, params object[] parameters)
        {
            try
            {
                var method = TestRunner.GetTestMethod(methodName);
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
                WeakEventHandler.Register<RoutedEventArgs>(sender, "Click", OnWeak);

                MessageBox.Show("Weak handler registered. Click again to test it.", "Weak Handler", MessageBoxButton.OK);
            }
        }

        private void OnWeak(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Hello from weak handler", "Weak Handler", MessageBoxButton.OK);
        }
    }
}