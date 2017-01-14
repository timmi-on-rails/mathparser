using System;

namespace MathParser
{
	interface IExpressionVisitor
	{
		Traversal Traversal { get; }

		void Visit(BinaryExpression binaryExpression);

		void Visit(PrefixExpression prefixExpression);

		void Visit(PostfixExpression postfixExpression);

		void Visit(GroupExpression groupExpression);

		void Visit(NumberExpression numberExpression);

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
			Traversal traversal = visitor.Traversal;

			if (traversal == Traversal.BottomUp)
			{
				foreach (IExpression child in children)
				{
					child.Accept(visitor);
				}
			}
			else if (traversal == Traversal.None)
			{
			}
			else
			{
				throw new ArgumentException("unknown traversal type");
			}

			visitSelf();
		}
	}
}
