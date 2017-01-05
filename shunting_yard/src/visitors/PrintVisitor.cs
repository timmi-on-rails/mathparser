using System;
using System.Collections.Generic;
using System.Linq;

namespace MathParser
{
	class PrintVisitor : AbstractExpressionVisitor
	{
		readonly Stack<string> _returnStack = new Stack<string>();

		public override void Visit(BinaryExpression binaryExpression)
		{
			string infix;
			switch (binaryExpression.BinaryExpressionType)
			{
				case BinaryExpressionType.Add:
					infix = "+";
					break;
				case BinaryExpressionType.Div:
					infix = "/";
					break;
				case BinaryExpressionType.Mul:
					infix = "*";
					break;
				case BinaryExpressionType.Pow:
					infix = "^";
					break;
				case BinaryExpressionType.Sub:
					infix = "-";
					break;
				default:
					throw new ArgumentException("unhandled infix expression type");
			}

			string right = _returnStack.Pop();
			string left = _returnStack.Pop();

			string output = String.Format("({0}{1}{2})", left, infix, right);
			_returnStack.Push(output);
		}

		public override void Visit(ValueExpression valueExpression)
		{
			_returnStack.Push(valueExpression.Value.ToString());
		}

		public override void Visit(ComparisonExpression comparisonExpression)
		{
			string comparison;
			switch (comparisonExpression.ComparisonExpressionType)
			{
				case ComparisonExpressionType.Less:
					comparison = "<";
					break;
				case ComparisonExpressionType.Bigger:
					comparison = ">";
					break;
				default:
					throw new ArgumentException("unhandled comparison expression type");
			}

			string right = _returnStack.Pop();
			string left = _returnStack.Pop();

			string output = String.Format("({0}{1}{2})", left, comparison, right);
			_returnStack.Push(output);
		}

		public override void Visit(TernaryExpression ternaryExpression)
		{
			string falseCase = _returnStack.Pop();
			string trueCase = _returnStack.Pop();
			string condition = _returnStack.Pop();

			_returnStack.Push("{" + condition + " ? " + trueCase + " : " + falseCase + "}");
		}

		public override void Visit(VariableExpression variableExpression)
		{
			_returnStack.Push(variableExpression.VariableName);
		}

		public override void Visit(PrefixExpression prefixExpression)
		{
			string prefix;
			switch (prefixExpression.PrefixExpressionType)
			{
				case PrefixExpressionType.Negation:
					prefix = "-";
					break;
				case PrefixExpressionType.Positive:
					prefix = "+";
					break;
				default:
					throw new ArgumentException("unhandled prefix expression type");
			}

			string right = _returnStack.Pop();
			string output = String.Format("({0}{1})", prefix, right);
			_returnStack.Push(output);
		}

		public override void Visit(PostfixExpression postfixExpression)
		{
			string postfix;
			switch (postfixExpression.PostfixExpressionType)
			{
				default:
					throw new ArgumentException("unhandled postfix expression type");
			}

			string left = _returnStack.Pop();
			string output = String.Format("({0}{1})", left, postfix);
			_returnStack.Push(output);
		}

		public override void Visit(CallExpression functionExpression)
		{
			string args = String.Join(",", functionExpression.Arguments.Select(arg => _returnStack.Pop()).Reverse());
			_returnStack.Push(functionExpression.FunctionName + "(" + args + ")");
		}

		public string GetResult()
		{
			return _returnStack.Pop();
		}
	}
}
