using System;

namespace MathParser
{
	interface IExpressionVisitor
	{
		void Visit(BinaryExpression binaryExpression);

		void Visit(PrefixExpression prefixExpression);

		void Visit(PostfixExpression postfixExpression);

		void Visit(GroupExpression groupExpression);

		void Visit(ValueExpression valueExpression);

		void Visit(CallExpression functionExpression);

		void Visit(VariableExpression variableExpression);

		void Visit(VariableAssignmentExpression variableAssignmentExpression);

		void Visit(TernaryExpression ternaryExpression);

		void Visit(ComparisonExpression comparisonExpression);

		void Visit(FunctionAssignmentExpression functionAssignmentExpression);
	}

	static class ExpressionVisitorExtensions
	{
		public static void Traverse(this IExpressionVisitor visitor, Action visitSelf, params IExpression[] children)
		{
			foreach (IExpression child in children)
			{
				child.Accept(visitor);
			}
			visitSelf();
		}
	}
}
