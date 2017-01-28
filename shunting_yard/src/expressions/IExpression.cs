namespace MathParser
{
	// TODO we need some source context in expression tree aswell

	interface IExpression
	{
		void Accept(IExpressionVisitor visitor);
	}
}
