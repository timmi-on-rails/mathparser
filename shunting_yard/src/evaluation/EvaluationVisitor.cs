using System;
using System.Linq;
using System.Collections.Generic;

namespace MathParser
{
	class EvaluationVisitor : IExpressionVisitor
	{
		readonly ISymbolManager _symbolManager;
		protected readonly Stack<Value> _evaluationStack;

		public Traversal Traversal { get { return Traversal.None; } }

		public EvaluationVisitor(ISymbolManager symbolManager)
		{
			_symbolManager = symbolManager;
			_evaluationStack = new Stack<Value>();
		}

		public Value GetResult()
		{
			if (_evaluationStack.Count == 1)
			{
				return _evaluationStack.Pop();
			}
			else
			{
				throw new EvaluationException("Evaluation stack still contains " + _evaluationStack.Count +
											  " values. It should contain exactly one value.");
			}
		}

		public void Visit(BinaryExpression binaryExpression)
		{
			binaryExpression.LeftOperand.Accept(this);
			binaryExpression.RightOperand.Accept(this);

			Value rightOperand = _evaluationStack.Pop();
			Value leftOperand = _evaluationStack.Pop();
			bool isNumberLeft = leftOperand.IsDecimal || leftOperand.IsInteger;
			bool isNumberRight = rightOperand.IsDecimal || rightOperand.IsInteger;

			switch (binaryExpression.BinaryExpressionType)
			{
				case BinaryExpressionType.Addition:
					Value result = Value.Add(leftOperand, rightOperand);
					_evaluationStack.Push(result);
					return;
			}

			if (isNumberLeft && isNumberRight)
			{
				double leftValue = leftOperand.ToDouble();
				double rightValue = rightOperand.ToDouble();
				Value result;

				switch (binaryExpression.BinaryExpressionType)
				{
					case BinaryExpressionType.Substraction:
						result = Value.Decimal(leftValue - rightValue);
						break;
					case BinaryExpressionType.Multiplication:
						result = Value.Decimal(leftValue * rightValue);
						break;
					case BinaryExpressionType.Division:
						result = Value.Decimal(leftValue / rightValue);
						break;
					case BinaryExpressionType.Power:
						result = Value.Decimal(Math.Pow(leftValue, rightValue));
						break;
					case BinaryExpressionType.Modulo:
						result = Value.Decimal(leftValue % rightValue);
						break;
					case BinaryExpressionType.Equal:
						result = Value.Boolean(leftValue == rightValue);
						break;
					case BinaryExpressionType.NotEqual:
						result = Value.Boolean(leftValue != rightValue);
						break;
					case BinaryExpressionType.Less:
						result = Value.Boolean(leftValue < rightValue);
						break;
					case BinaryExpressionType.LessOrEqual:
						result = Value.Boolean(leftValue <= rightValue);
						break;
					case BinaryExpressionType.Greater:
						result = Value.Boolean(leftValue > rightValue);
						break;
					case BinaryExpressionType.GreaterOrEqual:
						result = Value.Boolean(leftValue >= rightValue);
						break;

					default:
						string message = String.Format("Unhandled binary operation {0}.", binaryExpression.BinaryExpressionType);
						throw new EvaluationException(message);
				}

				// TODO integer operations
				_evaluationStack.Push(result);
			}
			else
			{
				string message = String.Format("Incompatible types of operands {0} and {1} for binary operation {2}.",
							   leftOperand, rightOperand, binaryExpression.BinaryExpressionType);
				throw new EvaluationException(message);
			}
		}

		public void Visit(PrefixExpression prefixExpression)
		{
			prefixExpression.RightOperand.Accept(this);

			Value operand = _evaluationStack.Pop();

			if (operand.IsInteger || operand.IsDecimal)
			{
				double value = operand.ToDouble();
				double result;

				switch (prefixExpression.PrefixExpressionType)
				{
					case PrefixExpressionType.Negation:
						result = -value;
						break;
					default:
						string message = String.Format("Unhandled prefix operation {0}.", prefixExpression.PrefixExpressionType);
						throw new EvaluationException(message);
				}

				_evaluationStack.Push(Value.Decimal(result));
			}
			else
			{
				string message = String.Format("Unable to execute prefix operation {0} for operand {1}.",
											   prefixExpression.PrefixExpressionType, operand);
				throw new EvaluationException(message);
			}
		}

		public void Visit(ValueExpression valueExpression)
		{
			if (valueExpression.Value.IsExpression)
			{
				valueExpression.Value.ToExpression().Expr.Accept(this);
			}
			else
			{
				_evaluationStack.Push(valueExpression.Value);
			}
		}

		public void Visit(CallExpression functionExpression)
		{
			functionExpression.FunctionExpression.Accept(this);

			foreach (IExpression argument in functionExpression.Arguments)
			{
				argument.Accept(this);
			}

			int numArguments = functionExpression.Arguments.Count();

			List<Value> arguments = new List<Value>();

			for (int i = 0; i < numArguments; i++)
			{
				arguments.Add(_evaluationStack.Pop());
			}
			arguments.Reverse();

			Value function = _evaluationStack.Pop();

			if (function.IsFunction)
			{

				Value result = function.ToFunction()(arguments.ToArray());
				_evaluationStack.Push(result);
			}
			else
			{
				throw new EvaluationException("Invalid function call with " + numArguments + " arguments.");
			}
		}

		public void Visit(TernaryExpression ternaryExpression)
		{
			ternaryExpression.Condition.Accept(this);
			Value conditionOperand = _evaluationStack.Pop();

			if (conditionOperand.IsBoolean)
			{
				bool condition = conditionOperand.ToBoolean();

				if (condition)
				{
					ternaryExpression.TrueCase.Accept(this);
				}
				else
				{
					ternaryExpression.FalseCase.Accept(this);
				}
			}
			else
			{
				string message = String.Format("Condition in ternary operation must be boolean, got " + conditionOperand + " instead.");
				throw new EvaluationException(message);
			}
		}

		public virtual void Visit(VariableExpression variableExpression)
		{
			if (_symbolManager != null && _symbolManager.IsSet(variableExpression.Identifier))
			{
				// TODO decide whether this is good or not , do we want to evaluate like that?!
				Value value = _symbolManager.Get(variableExpression.Identifier);

				if (value.IsExpression)
				{
					value.ToExpression().Expr.Accept(this);
				}
				else
				{
					_evaluationStack.Push(value);
				}
			}
			else
			{
				_evaluationStack.Push(Value.Expression(new Expression(variableExpression)));
			}
		}

		public void Visit(PostfixExpression postfixExpression)
		{
			postfixExpression.LeftOperand.Accept(this);

			Value operand = _evaluationStack.Pop();

			if (operand.IsInteger)
			{
				Value value = operand;
				Value result;

				switch (postfixExpression.PostfixExpressionType)
				{
					case PostfixExpressionType.Factorial:
						long res = 1;
						long val = value.ToInt64();

						while (val != 1)
						{
							res = res * val;
							val = val - 1;
						}

						result = Value.Integer(res);
						break;
					default:
						string message = String.Format("Unhandled postfix operation {0}.", postfixExpression.PostfixExpressionType);
						throw new EvaluationException(message);
				}

				_evaluationStack.Push(result);
			}
			else
			{
				string message = String.Format("Unable to execute postfix operation {0} for operand {1}.",
											   postfixExpression.PostfixExpressionType, operand);
				throw new EvaluationException(message);
			}
		}

		public void Visit(GroupExpression groupExpression)
		{
			groupExpression.Operand.Accept(this);
		}

		public void Visit(VariableAssignmentExpression variableAssignmentExpression)
		{
			variableAssignmentExpression.Expression.Accept(this);
		}

		public void Visit(FunctionAssignmentExpression functionAssignmentExpression)
		{
			Value value = AssignVisitor.GetFunction(functionAssignmentExpression, _symbolManager);
			_evaluationStack.Push(value);
		}
	}
}
