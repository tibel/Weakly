using System;
using System.Linq;
using System.Reflection;

namespace Weakly
{
    /// <summary>
    /// Provides functionality to reflect a path of properties.
    /// </summary>
    public sealed class ReflectionPath
    {
        private readonly string[] _items;
        private readonly Type[] _reflectedTypes;
        private readonly Func<object, object>[] _getters;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReflectionPath"/> class.
        /// </summary>
        /// <param name="path">The reflection path.</param>
        public ReflectionPath(string path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));

            _items = path.Split('.');
            _reflectedTypes = new Type[_items.Length];
            _getters = new Func<object, object>[_items.Length];
        }

        /// <summary>
        /// Gets the value for the specified <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The source instance.</param>
        /// <returns>The value.</returns>
        public object GetValue(object source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var current = source;
            for (var i = 0; i < _items.Length; i++)
            {
                if (current == null)
                    throw new NullReferenceException(
                        string.Format(
                            "Could not get the value of the property path '{0}' because '{1}' is null on object '{2}'.",
                            string.Join(".", _items),
                            string.Join(".", _items.Take(i)),
                            source
                            )
                        );

                var getValue = _getters[i];
                var currentType = current.GetType();
                if (getValue == null || _reflectedTypes[i] != currentType)
                {
                    var pi = currentType.GetRuntimeProperty(_items[i]);
                    if (pi == null)
                        throw new InvalidOperationException(
                            string.Format(
                                "Could not find property '{0}' on type '{1}'.",
                                _items[i],
                                currentType
                                )
                            );

                    _reflectedTypes[i] = currentType;
                    getValue = _getters[i] = pi.GetValue;
                }

                current = getValue(current);
            }

            return current;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return string.Join(".", _items);
        }
    }
}
