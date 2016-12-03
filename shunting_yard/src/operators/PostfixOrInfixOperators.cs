using System;
using System.Collections.Generic;

namespace shunting_yard
{
	static class PostfixOrInfixOperators
	{
		public static IReadOnlyDictionary<TokenType, Operator> Get
		{
			get { return postfixOrInfixOperators; }
		}

		private static Dictionary<TokenType, Operator> postfixOrInfixOperators = new Dictionary<TokenType, Operator>
		{
			{ TokenType.Minus, new Operator(TokenType.Plus, OperatorType.Infix, Associativity.Left, 1,
					(mathParser, output) =>
					{
						IExpression exp2 = output.Pop();
						IExpression exp1 = output.Pop();
						output.Push(new SubExpression(exp1, exp2));
					})
			},
			{ TokenType.Plus, new Operator(TokenType.Plus, OperatorType.Infix, Associativity.Left, 1,
					(mathParser, output) =>
					{
						IExpression exp2 = output.Pop();
						IExpression exp1 = output.Pop();
						output.Push(new AddExpression(exp1, exp2));
					})
			},
			{ TokenType.Star, new Operator(TokenType.Star, OperatorType.Infix, Associativity.Left, 2,
					(mathParser, output) =>
					{
						IExpression exp2 = output.Pop();
						IExpression exp1 = output.Pop();
						output.Push(new MulExpression(exp1, exp2));
					})
			},
			{ TokenType.Slash, new Operator(TokenType.Slash, OperatorType.Infix, Associativity.Left, 2,
					(mathParser, output) =>
					{
						IExpression exp2 = output.Pop();
						IExpression exp1 = output.Pop();
						output.Push(new DivExpression(exp1, exp2));
					})
			},
			{ TokenType.Pow, new Operator(TokenType.Pow, OperatorType.Infix, Associativity.Right, 3,
					(mathParser, output) =>
					{
						IExpression exp2 = output.Pop();
						IExpression exp1 = output.Pop();
						output.Push(new PowerExpression(exp1, exp2));
					})
			},
			{ TokenType.Equal, new Operator(TokenType.Equal, OperatorType.Infix, Associativity.Right, 0,
					(mathParser, output) =>
					{
						IExpression exp2 = output.Pop();
						IExpression exp1 = output.Pop();
						if (exp1 is VariableExpression)
						{
							IExpression variableAssignment = new VariableAssignmentExpression(((VariableExpression)exp1).VariableName, exp2, mathParser.VariablesManager);
							output.Push(variableAssignment);
						}
						else
						{
							throw new FormatException("cannot assign to " + exp1.ToString());
						}
					})
			}
		};
	}
}

