using System;
using System.Collections.Generic;
using System.Linq;

namespace MathParser
{
	class PrintVisitor : BottomUpExpressionVisitor
	{
		readonly Stack<string> _stack = new Stack<string>();

		public string GetResult()
		{
			if (_stack.Count == 1)
			{
				return _stack.Pop();
			}
			else
			{
				string message = String.Format("Stack contains {0} values. It should contain exactly one value.", _stack.Count);
				throw new EvaluationException(message);
			}
		}

		public override void Visit(BinaryExpression binaryExpression)
		{
			string infix;
			switch (binaryExpression.BinaryExpressionType)
			{
				case BinaryExpressionType.Addition:
					infix = "+";
					break;
				case BinaryExpressionType.Division:
					infix = "/";
					break;
				case BinaryExpressionType.Multiplication:
					infix = "*";
					break;
				case BinaryExpressionType.Power:
					infix = "^";
					break;
				case BinaryExpressionType.Substraction:
					infix = "-";
					break;
				case BinaryExpressionType.Modulo:
					infix = "%";
					break;
				case BinaryExpressionType.Equal:
					infix = "==";
					break;
				case BinaryExpressionType.NotEqual:
					infix = "!=";
					break;
				case BinaryExpressionType.Less:
					infix = "<";
					break;
				case BinaryExpressionType.LessOrEqual:
					infix = "<=";
					break;
				case BinaryExpressionType.Greater:
					infix = ">";
					break;
				case BinaryExpressionType.GreaterOrEqual:
					infix = ">=";
					break;
				default:
					throw new ArgumentException("unhandled binary expression type");
			}

			string right = _stack.Pop();
			string left = _stack.Pop();

			string output = String.Format("({0}{1}{2})", left, infix, right);
			_stack.Push(output);
		}

		public override void Visit(ValueExpression valueExpression)
		{
			_stack.Push(valueExpression.Value.ToString());
		}

		public override void Visit(TernaryExpression ternaryExpression)
		{
			string falseCase = _stack.Pop();
			string trueCase = _stack.Pop();
			string condition = _stack.Pop();

			_stack.Push("{" + condition + " ? " + trueCase + " : " + falseCase + "}");
		}

		public override void Visit(VariableExpression variableExpression)
		{
			_stack.Push(variableExpression.Identifier);
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

			string right = _stack.Pop();
			string output = String.Format("({0}{1})", prefix, right);
			_stack.Push(output);
		}

		public override void Visit(PostfixExpression postfixExpression)
		{
			string postfix;
			switch (postfixExpression.PostfixExpressionType)
			{
				case PostfixExpressionType.Factorial:
					postfix = "!";
					break;
				default:
					throw new ArgumentException("unhandled postfix expression type");
			}

			string left = _stack.Pop();
			string output = String.Format("({0}{1})", left, postfix);
			_stack.Push(output);
		}

		public override void Visit(CallExpression functionExpression)
		{
			string args = String.Join(",", functionExpression.Arguments.Select(arg => _stack.Pop()).Reverse());
			string functionName = _stack.Pop();
			_stack.Push(functionName + "(" + args + ")");
		}

		public override void Visit(VariableAssignmentExpression variableAssignmentExpression)
		{
			_stack.Push("(" + variableAssignmentExpression.Identifier + " = " + _stack.Pop() + ")");
		}

		public override void Visit(FunctionAssignmentExpression functionAssignmentExpression)
		{
			_stack.Push("(" + functionAssignmentExpression.FunctionIdentifier +
						"(" + String.Join(", ", functionAssignmentExpression.ArgumentNames) + ")" + " = " + _stack.Pop() + ")");
		}
	}
}
