using System;

namespace shunting_yard
{
	class VariableAssignmentExpression : IExpression
	{
		private readonly VariablesManager _variablesManager;

		public IExpression Expression { get; }

		public string VariableName { get; }

		public VariableAssignmentExpression(string variableName, IExpression expression, VariablesManager variablesManager)
		{
			VariableName = variableName;
			Expression = expression;
			_variablesManager = variablesManager;
		}

		public bool CanEvaluate()
		{
			return Expression.CanEvaluate();
		}

		public double Evaluate()
		{
			double value = Expression.Evaluate();
			_variablesManager.Set(VariableName, value);
			return value;
		}

		public override string ToString()
		{
			return String.Format("({0} = {1})", VariableName, Expression.ToString());
		}
	}
}
