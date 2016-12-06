using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;

namespace shunting_yard
{
	public partial class MathParser
	{
		internal VariablesManager VariablesManager { get; }

		internal FunctionsManager FunctionsManager { get; }

		public MathParser()
		{
			VariablesManager = new VariablesManager();
			FunctionsManager = new FunctionsManager();
		}

		/*public bool TryParse(string expression, out IExpression result)
		{			
			try
			{
				result = Parse(expression);
				return true;
			}
			catch (MathParserException)
			{
				result = new ValueExpression(0);
				return false;
			}
		}*/

		public ExpressionTree ParseShuntingYard(string expression)
		{
			Stack<IExpression> output = new Stack<IExpression>();
			Stack<Operator> operators = new Stack<Operator>();

			ParserState state = ParserState.WantOperand;

			foreach (Token nextToken in Tokenizer.GetTokens(expression))
			{
				if (state == ParserState.WantOperand)
				{
					if (nextToken.TokenType == TokenType.Ident)
					{
						double result;
						if (Double.TryParse(nextToken.Value, NumberStyles.Any, CultureInfo.InvariantCulture, out result))
						{
							output.Push(new ValueExpression(result));
						}
						else
						{
							output.Push(new VariableExpression(nextToken.Value, VariablesManager));
						}

						state = ParserState.HaveOperand;
						continue;
					}

					if (nextToken.TokenType == TokenType.Lpar)
					{
						operators.Push(new Operator(TokenType.Lpar, OperatorType.Prefix, Associativity.None, -1, null));
						continue;
					}

					if (nextToken.TokenType == TokenType.Rpar)
					{
						if (operators.Count == 0)
						{
							throw new FormatException("missing (");
						}

						if (operators.Peek().TokenType == TokenType.Lpar && operators.Peek().OperatorType == OperatorType.Infix)
						{
							if (operators.Peek().ArgumentCount == 0)
							{
								VariableExpression funcName = output.Pop() as VariableExpression;
								if (funcName != null)
								{
									output.Push(new FunctionExpression(funcName.VariableName, new IExpression[] { }));
									operators.Pop();
									state = ParserState.HaveOperand;
									continue;
								}
							}
						}
						// maybe throw error
					}

					if (PrefixOperators.Get.ContainsKey(nextToken.TokenType))
					{
						operators.Push(PrefixOperators.Get[nextToken.TokenType]);
						continue;
					}

					throw new FormatException("expecting operand");
				}

				if (state == ParserState.HaveOperand)
				{
					if (nextToken.TokenType == TokenType.Eof)
					{
						while (operators.Count > 0)
						{
							Operator op = operators.Pop();
							if (op.TokenType != TokenType.Lpar)
							{
								op.Apply(this, output);
							}
							else
							{
								throw new FormatException("missing )");
							}
						}
						break;
					}

					if (PostfixOrInfixOperators.Get.ContainsKey(nextToken.TokenType)
						&& PostfixOrInfixOperators.Get[nextToken.TokenType].OperatorType == OperatorType.Postfix)
					{
						operators.Push(PostfixOrInfixOperators.Get[nextToken.TokenType]);
						continue;
					}

					if (nextToken.TokenType == TokenType.Lpar)
					{
						operators.Push(new Operator(TokenType.Lpar, OperatorType.Infix, Associativity.None, -1, null));
						state = ParserState.WantOperand;
						continue;
					}

					if (nextToken.TokenType == TokenType.Rpar)
					{
						while (true)
						{
							if (operators.Count == 0)
							{
								throw new FormatException("missing (");
							}

							Operator op = operators.Pop();
							if (op.TokenType != TokenType.Lpar)
							{
								op.Apply(this, output);
							}
							else
							{
								if (op.OperatorType == OperatorType.Infix)
								{
									op.ArgumentCount++;

									List<IExpression> arguments = new List<IExpression>();

									for (int i = 0; i < op.ArgumentCount; i++)
									{
										arguments.Add(output.Pop());
									}

									VariableExpression funcName = output.Pop() as VariableExpression;
									if (funcName != null)
									{
										arguments.Reverse();
										output.Push(new FunctionExpression(funcName.VariableName, arguments.ToArray()));
									}
								}
								break;
							}
						}
						continue;
					}

					if (nextToken.TokenType == TokenType.Comma)
					{
						while (true)
						{
							if (operators.Count == 0)
							{
								throw new FormatException("missing function call left (");
							}

							if (operators.Peek().TokenType != TokenType.Lpar)
							{
								operators.Pop().Apply(this, output);
							}
							else
							{
								Operator op = operators.Pop();
								op.ArgumentCount++;
								operators.Push(op);
								break;
							}
						}

						state = ParserState.WantOperand;
						continue;
					}

					if (PostfixOrInfixOperators.Get.ContainsKey(nextToken.TokenType)
						&& PostfixOrInfixOperators.Get[nextToken.TokenType].OperatorType == OperatorType.Infix)
					{
						Operator op = PostfixOrInfixOperators.Get[nextToken.TokenType];

						if (operators.Count != 0
							&& !(operators.Peek().Priority < op.Priority && op.Associativity == Associativity.Left)
							&& !(operators.Peek().Priority <= op.Priority && op.Associativity == Associativity.Right))
						{
							operators.Pop().Apply(this, output);
						}

						operators.Push(op);
						state = ParserState.WantOperand;
						continue;
					}

					throw new FormatException("expecting operator");
				}
			}


			if (output.Count != 1)
			{
				throw new Exception("too many");
			}

			return new ExpressionTree(this, output.Pop());
		}
	}
}
