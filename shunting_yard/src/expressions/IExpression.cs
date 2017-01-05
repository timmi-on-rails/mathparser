namespace MathParser
{
	interface IExpression : ISyntaxNode
	{
	}

	interface ISyntaxNode
	{
		void Accept(IExpressionVisitor visitor);
	}
}
