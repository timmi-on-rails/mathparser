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

		public void Accept(IExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}
	}
}
