using System;

namespace shunting_yard
{
	class VariableExpression : IExpression
	{
		private readonly VariablesManager _variablesManager;

		public string VariableName { get; }

		public VariableExpression(string variableName, VariablesManager variableManager)
		{
			VariableName = variableName;
			_variablesManager = variableManager;
		}

		public bool CanEvaluate()
		{
			return _variablesManager.IsSet(VariableName);
		}

		public double Evaluate()
		{
			return _variablesManager.Get(VariableName);
		}

		public override string ToString()
		{
			return VariableName;
		}
	}
}
