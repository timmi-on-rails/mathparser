using System;
using System.Linq;
using System.Collections.Generic;

namespace MathParser
{
	class EvaluationVisitor : IExpressionVisitor
	{
		readonly ISymbolManager _symbolProvider;
		protected readonly Stack<Value> _evaluationStack;

		public Traversal Traversal { get { return Traversal.None; } }

		public EvaluationVisitor(ISymbolManager symbolProvider)
		{
			_symbolProvider = symbolProvider;
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
			bool isNumberLeft = leftOperand.IsFloatingPointNumber || leftOperand.IsInteger;
			bool isNumberRight = rightOperand.IsFloatingPointNumber || rightOperand.IsInteger;

			if (isNumberLeft && isNumberRight)
			{
				double leftValue = leftOperand.ToDouble();
				double rightValue = rightOperand.ToDouble();
				double result;

				switch (binaryExpression.BinaryExpressionType)
				{
					case BinaryExpressionType.Add:
						result = leftValue + rightValue;
						break;
					case BinaryExpressionType.Sub:
						result = leftValue - rightValue;
						break;
					case BinaryExpressionType.Mul:
						result = leftValue * rightValue;
						break;
					case BinaryExpressionType.Div:
						result = leftValue / rightValue;
						break;
					case BinaryExpressionType.Pow:
						result = Math.Pow(leftValue, rightValue);
						break;
					default:
						string message = String.Format("Unhandled binary operation {0}.", binaryExpression.BinaryExpressionType);
						throw new EvaluationException(message);
				}

				// TODO integer operations
				_evaluationStack.Push(Value.FloatingPointNumber(result));
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

			if (operand.IsInteger || operand.IsFloatingPointNumber)
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

				_evaluationStack.Push(Value.FloatingPointNumber(result));
			}
			else
			{
				string message = String.Format("Unable to execute prefix operation {0} for operand {1}.",
											   prefixExpression.PrefixExpressionType, operand);
				throw new EvaluationException(message);
			}
		}

		public void Visit(ValueExpression numberExpression)
		{
			_evaluationStack.Push(numberExpression.Value);
		}

		public void Visit(CallExpression functionExpression)
		{
			foreach (IExpression argument in functionExpression.Arguments)
			{
				argument.Accept(this);
			}

			int numArguments = functionExpression.Arguments.Count();

			if (_symbolProvider.IsSet(functionExpression.FunctionName))
			{
				List<Value> arguments = new List<Value>();

				for (int i = 0; i < numArguments; i++)
				{
					arguments.Add(_evaluationStack.Pop());
				}
				arguments.Reverse();

				Value result = _symbolProvider.Get(functionExpression.FunctionName).ToFunction()(arguments.ToArray());
				_evaluationStack.Push(result);
			}
			else
			{
				throw new EvaluationException("Undefined function " + functionExpression.FunctionName +
											  " with " + numArguments + " arguments.");
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

		public void Visit(ComparisonExpression comparisonExpression)
		{
			comparisonExpression.LeftOperand.Accept(this);
			comparisonExpression.RightOperand.Accept(this);

			Value rightOperand = _evaluationStack.Pop();
			Value leftOperand = _evaluationStack.Pop();
			bool isNumberLeft = leftOperand.IsFloatingPointNumber || leftOperand.IsInteger;
			bool isNumberRight = rightOperand.IsFloatingPointNumber || rightOperand.IsInteger;

			if (isNumberLeft && isNumberRight)
			{
				double leftValue = leftOperand.ToDouble();
				double rightValue = rightOperand.ToDouble();
				bool result;

				switch (comparisonExpression.ComparisonExpressionType)
				{
					case ComparisonExpressionType.Less:
						result = leftValue < rightValue;
						break;
					default:
						string message = String.Format("Unhandled comparison operation {0}.", comparisonExpression.ComparisonExpressionType);
						throw new EvaluationException(message);
				}

				_evaluationStack.Push(Value.Boolean(result));
			}
			else
			{
				string message = String.Format("Incompatible types of operands {0} and {1} for comparsion operation {2}.",
							   leftOperand, rightOperand, comparisonExpression.ComparisonExpressionType);
				throw new EvaluationException(message);
			}
		}

		public virtual void Visit(VariableExpression variableExpression)
		{
			if (_symbolProvider.IsSet(variableExpression.VariableName))
			{
				_evaluationStack.Push(_symbolProvider.Get(variableExpression.VariableName));
			}
			else
			{
				throw new EvaluationException("Unknown variable " + variableExpression.VariableName + ".");
			}
		}

		public void Visit(PostfixExpression postfixExpression)
		{
			throw new NotImplementedException();
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
			throw new NotImplementedException();
		}
	}
}
