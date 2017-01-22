﻿using System;
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

			string right = _stack.Pop();
			string left = _stack.Pop();

			string output = String.Format("({0}{1}{2})", left, infix, right);
			_stack.Push(output);
		}

		public override void Visit(ValueExpression valueExpression)
		{
			_stack.Push(valueExpression.Value.ToString());
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

			string right = _stack.Pop();
			string left = _stack.Pop();

			string output = String.Format("({0}{1}{2})", left, comparison, right);
			_stack.Push(output);
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
			_stack.Push(functionExpression.FunctionName + "(" + args + ")");
		}

		public override void Visit(VariableAssignmentExpression variableAssignmentExpression)
		{
			_stack.Push("(" + variableAssignmentExpression.Identifier + " = " + _stack.Pop() + ")");
		}
	}
}
