![Logo](./build/weakly_icon.png?raw=true)
# Weakly

Weakly is a collection of some useful weak-reference types available as portable class library for **net45+win8+wp8+wpa81** and available through nuget.


## Install
Weakly is available through NuGet:

**Install-Package** [Weakly](https://www.nuget.org/packages/Weakly/)


## Content

### Builders
Create compiled Expressions for:
* `Builder.DynamicDelegate` creates compiled version of MethodInfo.Invoke
* `Builder.OpenAction` creates open delegates
* `Builder.OpenFunc` creates open delegates
* `Builder.PropertyAccessor` creates compiled version of PropertyInfo.SetValue and GetValue

### Collections
* `WeakCollection<T>`
* `WeakValueDictionary<TKey, TValue>`
* some Helpers

### Delegates
* `WeakAction`
* `WeakFunc<TResult>`
* `DisposableAction` executes an action when disposed

### Events
* `WeakEventHandler`
* `WeakEventSource<TEventArgs>`

### IO
* `MemoryTributary` is a MemoryStream replacement using multiple memory segments

### Reflection
* `ReflectionPath` to reflect a path of properties
* some Helpers

### Tasks
* Common Tasks
* Exception handling
* APM pattern helper
