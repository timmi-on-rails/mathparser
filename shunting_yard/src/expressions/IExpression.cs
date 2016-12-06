namespace shunting_yard
{
	interface IExpression
	{
		void Accept(IExpressionVisitor visitor);
	}
}
