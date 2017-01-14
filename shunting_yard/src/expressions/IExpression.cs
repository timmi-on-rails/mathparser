namespace MathParser
{
	interface IExpression
	{
		void Accept(IExpressionVisitor visitor);
	}
}
