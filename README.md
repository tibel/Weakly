# Weakly

Weakly is a collection of some useful weak-reference types available as portable class library for **net45+win8+wp8+wpa81** and available through nuget.

Normally it is not possible to call private methods using reflection in Windows Phone and Windows Store Apps. You will get a MethodAccessException.
Weakly solves this problem by using compiled Expressions. So it is not only possible to call private methods but also faster as dynamic invocation.


## Install
Weakly is available through NuGet:

**Install-Package** [Weakly](https://www.nuget.org/packages/Weakly/)

## Content

### Collections
* WeakCollection&lt;T&gt;
* WeakValueDictionary&lt;TKey, TValue&gt;

### Events
* WeakEventHandler&lt;TEventArgs&gt;
* WeakEventSource&lt;TEventArgs&gt;

### Delegates
* WeakAction to WeakAction&lt;T1, T2, T3, T4, T5&gt;
* WeakFunc&lt;TResult&gt; to WeakFunc&lt;T1, T2, T3, T4, T5, TResult&gt;
* DynamicDelegate (compiled version of MethodInfo.Invoke)
