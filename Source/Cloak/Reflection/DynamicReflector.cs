﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Cloak.Reflection
{
	// Adapted from https://github.com/gregoryyoung/m-r/blob/master/SimpleCQRS/InfrastructureCrap.DontBotherReadingItsNotImportant.cs

	public static class DynamicReflector
	{
		public static dynamic For(object target)
		{
			Contract.Requires(target != null);

			return target == null || target.GetType().IsPrimitive || target is string
				? target
				: new PrivateReflectionDynamicObject(target);
		}

		private sealed class PrivateReflectionDynamicObject : DynamicObject
		{
			private const BindingFlags _bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

			private static IDictionary<Type, IDictionary<string, ReflectedValue>> _valuesOnType = new ConcurrentDictionary<Type, IDictionary<string, ReflectedValue>>();

			private readonly object _target;
			private readonly Type _targetType;

			public PrivateReflectionDynamicObject(object target)
			{
				_target = target;
				_targetType = target.GetType();
			}

			public override string ToString()
			{
				return _target.ToString();
			}

			public override bool TryGetMember(GetMemberBinder binder, out object result)
			{
				var value = ReflectValue(binder.Name);

				result = DynamicReflector.For(value.Get(_target, index: null));

				return true;
			}

			public override bool TrySetMember(SetMemberBinder binder, object value)
			{
				ReflectValue(binder.Name).Set(_target, value, index: null);

				return true;
			}

			public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
			{
				result = ReflectIndexValue().Get(_target, indexes);

				return true;
			}

			public override bool TrySetIndex(SetIndexBinder binder, object[] indexes, object value)
			{
				ReflectIndexValue().Set(_target, value, indexes);

				return true;
			}

			public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
			{
				result = DynamicReflector.For(InvokeTargetMember(binder.Name, args));

				return true;
			}

			public override bool TryConvert(ConvertBinder binder, out object result)
			{
				result = Convert.ChangeType(_target, binder.Type);

				return true;
			}

			private ReflectedValue ReflectValue(string name)
			{
				var targetTypeValues = ReflectTargetTypeValues(_targetType);

				ReflectedValue value;

				if(!targetTypeValues.TryGetValue(name, out value))
				{
					// Get a list of supported properties and fields, skipping the auto property backing fields which start with '<'

					var valueNames = targetTypeValues.Keys.Where(name2 => name2[0] != '<').OrderBy(name2 => name2);

					throw new ArgumentException(
						String.Format(
							"The name {0} does not exist on target type {1}. Supported properties are: {2}",
							name,
							_targetType,
							String.Join(", ", valueNames)),
						"name");
				}

				return value;
			}

			private static IDictionary<string, ReflectedValue> ReflectTargetTypeValues(Type targetType)
			{
				IDictionary<string, ReflectedValue> typeValues;

				if(!_valuesOnType.TryGetValue(targetType, out typeValues))
				{
					typeValues = new ConcurrentDictionary<string, ReflectedValue>();

					foreach(var property in targetType.GetProperties(_bindingFlags).Where(property => property.DeclaringType == targetType))
					{
						typeValues[property.Name] = new ReflectedValue(property);
					}

					foreach(var field in targetType.GetFields(_bindingFlags).Where(field => field.DeclaringType == targetType))
					{
						typeValues[field.Name] = new ReflectedValue(field);
					}

					if(targetType.BaseType != null)
					{
						foreach(var value in ReflectTargetTypeValues(targetType.BaseType).Values)
						{
							typeValues[value.Name] = value;
						}
					}

					_valuesOnType[targetType] = typeValues;
				}

				return typeValues;
			}

			private ReflectedValue ReflectIndexValue()
			{
				// The index property is always named "Item" in C#

				return ReflectValue("Item");
			}

			private object InvokeTargetMember(string name, object[] args)
			{
				try
				{
					return _targetType.InvokeMember(name, _bindingFlags | BindingFlags.InvokeMethod, null, _target, args);
				}
				catch(MissingMethodException)
				{
					return _targetType.BaseType == null ? null : InvokeTargetMember(name, args);
				}
			}
		}

		private sealed class ReflectedValue
		{
			private readonly PropertyInfo _property;
			private readonly FieldInfo _field;

			internal ReflectedValue(PropertyInfo property)
			{
				_property = property;
			}

			internal ReflectedValue(FieldInfo field)
			{
				_field = field;
			}

			internal string Name
			{
				get { return _property != null ? _property.Name : _field.Name; }
			}

			internal object Get(object obj, object[] index)
			{
				return _property != null ? _property.GetValue(obj, index) : _field.GetValue(obj);
			}

			internal void Set(object obj, object value, object[] index)
			{
				if(_property != null)
				{
					_property.SetValue(obj, value, index);
				}
				else
				{
					_field.SetValue(obj, value);
				}
			}
		}
	}
}