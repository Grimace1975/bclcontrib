using System.Globalization;
namespace System.Collections.Object
{
	/// <summary>
	/// NamedObject
	/// </summary>
	public class NamedObject
	{
		private string _name;

		/// <summary>
		/// Initializes a new instance of the <see cref="NamedObject"/> class.
		/// </summary>
		/// <param name="name">The name.</param>
		public NamedObject(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException(name);
			}
			_name = name;
		}

		/// <summary>
		/// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
		/// </returns>
		public override string ToString()
		{
			if (_name[0] != '{')
			{
				_name = string.Format(CultureInfo.InvariantCulture, "{{{0}}}", new object[] { _name });
			}
			return _name;
		}
	}
}