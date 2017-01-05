using System;
using System.Linq;
using System.Collections.Generic;

namespace MathParser
{
	class EvaluationVisitor : AbstractExpressionVisitor
	{
		protected readonly Stack<double> _stack = new Stack<double>();
		private readonly Stack<bool> _stackBools = new Stack<bool>();

		FunctionsManager _functionManager;
		VariablesManager _variablesManager;

		public EvaluationVisitor(FunctionsManager fM, VariablesManager vm)
		{
			_functionManager = fM;
			_variablesManager = vm;
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

		public override void Visit(BinaryExpression binaryExpression)
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

		public override void Visit(PrefixExpression prefixExpression)
		{
			switch (prefixExpression.PrefixExpressionType)
			{
				case PrefixExpressionType.Negation:
					_stack.Push(-_stack.Pop());
					break;
				default:
					throw new ArgumentException("unknown unary expr " + prefixExpression.PrefixExpressionType);
			}
		}

		public override void Visit(ValueExpression valueExpression)
		{
			_stack.Push(valueExpression.Value);
		}

		public override void Visit(CallExpression functionExpression)
		{
			List<double> args = new List<double>();

			for (int i = 0; i < functionExpression.Arguments.Count(); i++)
			{
				args.Add(_stack.Pop());
			}
			args.Reverse();

			_stack.Push(_functionManager.Call(functionExpression.FunctionName, args.ToArray()));
		}

		public override void Visit(TernaryExpression ternaryExpression)
		{
			double falseVale = _stack.Pop();
			double trueVal = _stack.Pop();
			bool compRes = _stackBools.Pop();
			_stack.Push(compRes ? trueVal : falseVale);
		}

		public override void Visit(ComparisonExpression comparisonExpression)
		{
			double right = _stack.Pop();
			double left = _stack.Pop();
			bool result;

			switch (comparisonExpression.ComparisonExpressionType)
			{
				case ComparisonExpressionType.Less:
					result = left < right;
					break;
				default:
					throw new ArgumentException("unknown comparison expr " + comparisonExpression.ComparisonExpressionType);
			}

			_stackBools.Push(result);
		}

		public override void Visit(VariableExpression variableExpression)
		{
			if (!_variablesManager.IsSet(variableExpression.VariableName))
			{
				throw new UnknownVariableException();
			}

			_stack.Push(_variablesManager.Get(variableExpression.VariableName));
		}
	}
}
