using System;
using System.Linq;
using System.Collections.Generic;

namespace MathParser
{
	class EvaluationVisitor : IExpressionVisitor
	{
		readonly IFunctionProvider _functionProvider;
		readonly IVariableProvider _variableProvider;
		protected readonly Stack<object> _evaluationStack;

		public Traversal Traversal { get { return Traversal.None; } }

		public EvaluationVisitor(IFunctionProvider functionProvider, IVariableProvider variableProvider)
		{
			_functionProvider = functionProvider;
			_variableProvider = variableProvider;
			_evaluationStack = new Stack<object>();
		}

		public object GetResult()
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

			object rightOperand = _evaluationStack.Pop();
			object leftOperand = _evaluationStack.Pop();

			if ((leftOperand is double) && (rightOperand is double))
			{
				double leftValue = (double)leftOperand;
				double rightValue = (double)rightOperand;
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

			object operand = _evaluationStack.Pop();

			if (operand is double)
			{
				double value = (double)operand;
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

				_evaluationStack.Push(result);
			}
			else
			{
				string message = String.Format("Unable to execute prefix operation {0} for operand {1}.",
											   prefixExpression.PrefixExpressionType, operand);
				throw new EvaluationException(message);
			}
		}

		public void Visit(NumberExpression numberExpression)
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

			if (_functionProvider.IsDefined(functionExpression.FunctionName, numArguments))
			{
				List<object> arguments = new List<object>();

				for (int i = 0; i < numArguments; i++)
				{
					arguments.Add(_evaluationStack.Pop());
				}
				arguments.Reverse();

				object result = _functionProvider.Call(functionExpression.FunctionName, arguments.ToArray());
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
			object conditionOperand = _evaluationStack.Pop();

			if (conditionOperand is bool)
			{
				bool condition = (bool)conditionOperand;

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

			object rightOperand = _evaluationStack.Pop();
			object leftOperand = _evaluationStack.Pop();

			if ((leftOperand is double) && (rightOperand is double))
			{
				double leftValue = (double)leftOperand;
				double rightValue = (double)rightOperand;
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

				_evaluationStack.Push(result);
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
			if (_variableProvider.IsSet(variableExpression.VariableName))
			{
				_evaluationStack.Push(_variableProvider.Get(variableExpression.VariableName));
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
