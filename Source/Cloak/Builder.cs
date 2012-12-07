using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloak
{
	/// <summary>
	/// Base implementation of a class which builds instances of another
	/// </summary>
	/// <typeparam name="T">The type of built class</typeparam>
	public abstract class Builder<T>
	{
		public static implicit operator T(Builder<T> builder)
		{
			return builder.Instance;
		}

		private T _instance;

		/// <summary>
		/// Gets whether this builder has cached an instance since the last modification
		/// </summary>
		public bool HasInstance { get; private set; }

		/// <summary>
		/// Gets the instance described by this builder
		/// </summary>
		public T Instance
		{
			get
			{
				if(!HasInstance)
				{
					_instance = CreateInstance();

					HasInstance = true;
				}

				return _instance;
			}
		}

		/// <summary>
		/// When implemented by a derived class, creates a fresh instance of type <typeparamref name="T"/>
		/// </summary>
		/// <returns>A fresh instance of type <typeparamref name="T"/></returns>
		protected abstract T CreateInstance();

		/// <summary>
		/// Resets the built instance to a fresh instance of <typeparamref name="T"/>
		/// </summary>
		public void ClearInstance()
		{
			_instance = default(T);

			HasInstance = false;
		}

		/// <summary>
		/// Sets the specified field to the specified value only if it is different
		/// </summary>
		/// <typeparam name="TField">The field's type</typeparam>
		/// <param name="field">The field to set</param>
		/// <param name="newValue">The new value of the field</param>
		/// <param name="equalityComparer">Compares the current and new values for equality</param>
		/// <returns>Whether the field was set to the new value</returns>
		protected bool SetField<TField>(ref TField field, TField newValue, IEqualityComparer<TField> equalityComparer = null)
		{
			equalityComparer = equalityComparer ?? EqualityComparer<TField>.Default;

			var different = !equalityComparer.Equals(field, newValue);

			if(different)
			{
				field = newValue;

				ClearInstance();
			}

			return different;
		}
	}
}