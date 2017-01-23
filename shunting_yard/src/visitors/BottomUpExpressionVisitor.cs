namespace MathParser
{
	abstract class BottomUpExpressionVisitor : IExpressionVisitor
	{
		public Traversal Traversal { get { return Traversal.BottomUp; } }

		public virtual void Visit(PostfixExpression postfixExpression)
		{
		}

		public virtual void Visit(CallExpression functionExpression)
		{
		}

		public virtual void Visit(VariableAssignmentExpression variableAssignmentExpression)
		{
		}

		public virtual void Visit(GroupExpression groupExpression)
		{
		}

		public virtual void Visit(FunctionAssignmentExpression functionAssignmentExpression)
		{
		}

		public virtual void Visit(TernaryExpression ternaryExpression)
		{
		}

		public virtual void Visit(VariableExpression variableExpression)
		{
		}

		public virtual void Visit(ValueExpression valueExpression)
		{
		}

		public virtual void Visit(PrefixExpression prefixExpression)
		{
		}

		public virtual void Visit(BinaryExpression binaryExpression)
		{
		}
	}
}
