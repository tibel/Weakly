
namespace Weakly.Builders
{
    /// <summary>
    /// Helper to access the dedicated builders.
    /// </summary>
    public static class Builder
    {
        /// <summary>
        /// The dynamic delegate builder.
        /// </summary>
        public static IDynamicDelegateBuilder DynamicDelegate = new CachingDynamicDelegateBuilderDecorator(new ExpressionDynamicDelegateBuilder());

        /// <summary>
        /// The open action builder.
        /// </summary>
        public static IOpenActionBuilder OpenAction = new CachingOpenActionBuilderDecorator(new ExpressionOpenActionBuilder());

        /// <summary>
        /// The open function builder.
        /// </summary>
        public static IOpenFuncBuilder OpenFunc = new CachingOpenFuncBuilderDecorator(new ExpressionOpenFuncBuilder());

        /// <summary>
        /// The property accessor builder.
        /// </summary>
        public static IPropertyAccessorBuilder PropertyAccessor = new CachingPropertyAccessorBuilderDecorator(new ExpressionPropertyAccessorBuilder());
    }
}
