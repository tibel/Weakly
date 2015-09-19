using System;
using System.Globalization;

namespace Weakly
{
    /// <summary>
    /// An instance of this class can be used wherever you might otherwise use
    /// "new Object()".  The name will show up in the debugger, instead of
    /// merely "{object}"
    /// </summary>
    public sealed class NamedObject
    {
        private string _name;

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedObject"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public NamedObject(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            _name = name;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            if (_name[0] != '{')
            {
                // lazily add {} around the name, to avoid allocating a string until it's actually needed
                _name = String.Format(CultureInfo.InvariantCulture, "{{{0}}}", _name);
            }

            return _name;
        }
    }
}
