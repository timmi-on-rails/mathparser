using System;
using System.Linq;
using System.Collections.Generic;

namespace shunting_yard
{
	partial class MathParser
	{
		private readonly Dictionary<TokenType, IPrefixParselet> mPrefixParselets =
			new Dictionary<TokenType, IPrefixParselet>();

		private readonly Dictionary<TokenType, IInfixParselet> mInfixParselets =
			new Dictionary<TokenType, IInfixParselet>();


		private void register(TokenType token, IPrefixParselet parselet)
		{
			mPrefixParselets.Add(token, parselet);
		}

		private void register(TokenType token, IInfixParselet parselet)
		{
			mInfixParselets.Add(token, parselet);
		}


		internal IExpression parseExpression(int precedence)
		{
			Token token = consume();
			IPrefixParselet prefix = mPrefixParselets[token.TokenType];

			if (prefix == null)
				throw new ArgumentException(
					"Could not parse \"" + token.Value + "\".");

			IExpression left = prefix.Parse(this, token);

			while (precedence < getPrecedence())
			{
				token = consume();

				IInfixParselet infix = mInfixParselets[token.TokenType];
				left = infix.parse(this, left, token);
			}

			return left;
		}

		int getPrecedence()
		{
			IInfixParselet parselet;
			if (mInfixParselets.TryGetValue(lookAhead().TokenType, out parselet))
			{
				return parselet.Precedence;
			}

			return 0;
		}

		internal Token consume(TokenType expected)
		{
			Token token = lookAhead();
			if (token.TokenType != expected)
			{
				throw new ArgumentException("Expected token " + expected +
											" and found " + token.TokenType);
			}

			return consume();
		}

		Queue<Token> _tokens = new Queue<Token>();

		private Token consume()
		{
			return _tokens.Dequeue();
		}

		internal IExpression parseExpression()
		{
			return parseExpression(0);
		}

		private Token lookAhead()
		{
			return _tokens.Peek();
		}

		internal bool match(TokenType expected)
		{
			Token token = lookAhead();
			if (token.TokenType != expected)
			{
				return false;
			}

			consume();
			return true;
		}

		public ExpressionTree Parse(string expression)
		{
			mPrefixParselets.Clear();
			mInfixParselets.Clear();

			FunctionsManager.Define("Max", (double[] arg) => arg.Max());

			register(TokenType.Ident, new IdentParselet());
			register(TokenType.Lpar, new GroupParselet());
			register(TokenType.Lpar, new CallParselet());
			register(TokenType.Minus, new PrefixOperatorParselet(PrefixExpressionType.Negation, Precedence.PREFIX));
			register(TokenType.Plus, new BinaryOperatorParselet(BinaryExpressionType.Add, Precedence.SUM, LeftOrRight.Left));
			register(TokenType.Minus, new BinaryOperatorParselet(BinaryExpressionType.Sub, Precedence.SUM, LeftOrRight.Left));
			register(TokenType.Star, new BinaryOperatorParselet(BinaryExpressionType.Mul, Precedence.PRODUCT, LeftOrRight.Left));
			register(TokenType.Slash, new BinaryOperatorParselet(BinaryExpressionType.Div, Precedence.PRODUCT, LeftOrRight.Left));
			register(TokenType.Pow, new BinaryOperatorParselet(BinaryExpressionType.Pow, Precedence.EXPONENT, LeftOrRight.Right));

			_tokens.Clear();
			foreach (Token tk in Tokenizer.GetTokens(expression))
			{
				_tokens.Enqueue(tk);
			}

			IExpression res = parseExpression(0);


			return new ExpressionTree(this, res);
		}

	}
}

