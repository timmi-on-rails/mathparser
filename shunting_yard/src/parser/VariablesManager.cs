using System;
using System.Collections.Generic;

namespace MathParser
{
	class VariablesManager
	{
		private readonly Dictionary<string, double> _variables = new Dictionary<string, double>();

		public bool IsSet(string variableName)
		{
			return _variables.ContainsKey(variableName);
		}

		public double Get(string variableName)
		{
			return _variables[variableName];
		}

		public void Set(string variableName, double value)
		{
			_variables[variableName] = value;
		}
	}
}

