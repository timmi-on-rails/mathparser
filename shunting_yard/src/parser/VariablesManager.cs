using System;
using System.Collections.Generic;

namespace MathParser
{
	public class VariablesManager : IVariableProvider
	{
		readonly Dictionary<string, object> _variables = new Dictionary<string, object>();

		public bool IsSet(string variableName)
		{
			return _variables.ContainsKey(variableName);
		}

		public object Get(string variableName)
		{
			return _variables[variableName];
		}

		public void Set(string variableName, object value)
		{
			_variables[variableName] = value;
		}
	}
}

