namespace shunting_yard
{
	interface IExpressionVisitor
	{
		void Visit(BinaryExpression binaryExpression);

		void Visit(PrefixExpression prefixExpression);

		void Visit(PostfixExpression postfixExpression);

		void Visit(ValueExpression valueExpression);

		void Visit(FunctionExpression functionExpression);

		void Visit(VariableExpression variableExpression);

		void Visit(VariableAssignmentExpression variableAssignmentExpression);
	}
}
