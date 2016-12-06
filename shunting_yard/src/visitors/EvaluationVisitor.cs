using System;
using System.Collections.Generic;

namespace shunting_yard
{
	// TODO abstract base visitor class
	// so that not every concrete visitor needs to visit all node types
	// maybe some indirection in base class to have a visitor called on every IExpression visit
	class EvaluationVisitor : IExpressionVisitor
	{
		private readonly Stack<double> _stack = new Stack<double>();

		FunctionsManager _functionManager;

		public EvaluationVisitor(FunctionsManager fM)
		{
			_functionManager = fM;
		}

		public double GetResult()
		{
			if (_stack.Count == 1)
			{
				return _stack.Pop();
			}
			else
			{
				throw new ArgumentException("wahhhh zu viele: " + _stack.Count);
			}
		}

		public void Visit(BinaryExpression binaryExpression)
		{
			double right = _stack.Pop();
			double left = _stack.Pop();
			double result;

			switch (binaryExpression.BinaryExpressionType)
			{
				case BinaryExpressionType.Add:
					result = left + right;
					break;
				case BinaryExpressionType.Sub:
					result = left - right;
					break;
				case BinaryExpressionType.Mul:
					result = left * right;
					break;
				case BinaryExpressionType.Div:
					result = left / right;
					break;
				case BinaryExpressionType.Pow:
					result = Math.Pow(left, right);
					break;
				default:
					throw new ArgumentException("unknown binary expr " + binaryExpression.BinaryExpressionType);
			}

			_stack.Push(result);
		}

		public void Visit(PostfixExpression postfixExpression)
		{
		}

		public void Visit(PrefixExpression unaryExpression)
		{
			switch (unaryExpression.PrefixExpressionType)
			{
				case PrefixExpressionType.Negation:
					_stack.Push(-_stack.Pop());
					break;
				default:
					throw new ArgumentException("unknown unary expr " + unaryExpression.PrefixExpressionType);
			}
		}

		public void Visit(ValueExpression valueExpression)
		{
			_stack.Push(valueExpression.Value);
		}

		public void Visit(FunctionExpression functionExpression)
		{
			List<double> args = new List<double>();

			for (int i = 0; i < functionExpression.Arguments.Length; i++)
			{
				args.Add(_stack.Pop());
			}
			args.Reverse();

			_stack.Push(_functionManager.Call(functionExpression.FunctionName, args.ToArray()));
		}

		public void Visit(VariableExpression variableExpression)
		{
		}

		public void Visit(VariableAssignmentExpression variableAssignmentExpression)
		{
		}
	}
}
