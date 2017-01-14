using System;
using System.Collections.Generic;

namespace MathParser
{
	class FunctionsManager : IFunctionProvider
	{
		readonly Dictionary<FunctionKey, Func<object[], object>> _functions = new Dictionary<FunctionKey, Func<object[], object>>();

		public bool IsDefined(string functionName, int argumentCount)
		{
			FunctionKey fKey = new FunctionKey(functionName, argumentCount);
			return _functions.ContainsKey(fKey);
		}

		public void Define(string functionName, Func<object[], object> function, int? argumentCount = null)
		{
			FunctionKey fKey = new FunctionKey(functionName, argumentCount);
			_functions[fKey] = function;
		}

		public object Call(string functionName, object[] arguments)
		{
			FunctionKey fKey = new FunctionKey(functionName, arguments.Length);
			return _functions[fKey](arguments);
		}

		private sealed class FunctionKey
		{
			public string Name { get; }

			public int? ArgumentCount { get; }

			public FunctionKey(string name, int? argumentCount)
			{
				Name = name;
				ArgumentCount = argumentCount;
			}

			public override bool Equals(object obj)
			{
				if (obj is FunctionKey)
				{
					FunctionKey other = (FunctionKey)obj;
					return (other.Name == Name)
					&& ((other.ArgumentCount == ArgumentCount) || !ArgumentCount.HasValue || !other.ArgumentCount.HasValue);
				}
				else
				{
					return false;
				}
			}

			public override int GetHashCode()
			{
				return Name.GetHashCode();
			}
		}
	}
}
