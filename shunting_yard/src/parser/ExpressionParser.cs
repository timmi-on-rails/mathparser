using System;
using System.Collections.Generic;

namespace MathParser
{
	class ExpressionParser
	{
		static readonly Dictionary<TokenType, IPrefixParselet> _prefixParselets = new Dictionary<TokenType, IPrefixParselet>();
		static readonly Dictionary<TokenType, IInfixParselet> _infixParselets = new Dictionary<TokenType, IInfixParselet>();

		static ExpressionParser()
		{
			registerPrefixParselet(TokenType.Identifier, new VariableParselet());
			registerPrefixParselet(TokenType.Numeric, new NumberParselet());
			registerPrefixParselet(TokenType.False, new FixValueParselet(Value.Boolean(false)));
			registerPrefixParselet(TokenType.True, new FixValueParselet(Value.Boolean(true)));
			registerPrefixParselet(TokenType.LeftParenthesis, new GroupParselet());
			registerPrefixParselet(TokenType.Minus, new PrefixOperatorParselet(PrefixExpressionType.Negation, Precedences.PREFIX));
			registerPrefixParselet(TokenType.Plus, new PrefixOperatorParselet(PrefixExpressionType.Positive, Precedences.PREFIX));

			registerInfixParselet(TokenType.Assignment, new AssignParselet());
			registerInfixParselet(TokenType.Less, new ComparisonParselet(ComparisonExpressionType.Less, Precedences.COMPARISON, Associativity.Left));
			registerInfixParselet(TokenType.Greater, new ComparisonParselet(ComparisonExpressionType.Bigger, Precedences.COMPARISON, Associativity.Left));
			registerInfixParselet(TokenType.QuestionMark, new TernaryParselet());
			registerInfixParselet(TokenType.LeftParenthesis, new CallParselet());
			registerInfixParselet(TokenType.Plus, new BinaryOperatorParselet(BinaryExpressionType.Addition, Precedences.SUM, Associativity.Left));
			registerInfixParselet(TokenType.Minus, new BinaryOperatorParselet(BinaryExpressionType.Substraction, Precedences.SUM, Associativity.Left));
			registerInfixParselet(TokenType.Star, new BinaryOperatorParselet(BinaryExpressionType.Multiplication, Precedences.PRODUCT, Associativity.Left));
			registerInfixParselet(TokenType.Identifier, new BinaryOperatorParselet(BinaryExpressionType.Multiplication, Precedences.PRODUCT, Associativity.Left));
			registerInfixParselet(TokenType.Slash, new BinaryOperatorParselet(BinaryExpressionType.Division, Precedences.PRODUCT, Associativity.Left));
			registerInfixParselet(TokenType.Pow, new BinaryOperatorParselet(BinaryExpressionType.Power, Precedences.EXPONENT, Associativity.Right));
			registerInfixParselet(TokenType.Percent, new BinaryOperatorParselet(BinaryExpressionType.Modulo, Precedences.PRODUCT, Associativity.Left));
		}

		static void registerPrefixParselet(TokenType tokenType, IPrefixParselet prefixParselet)
		{
			_prefixParselets.Add(tokenType, prefixParselet);
		}

		static void registerInfixParselet(TokenType tokenType, IInfixParselet infixParselet)
		{
			_infixParselets.Add(tokenType, infixParselet);
		}

		public IExpression Parse(TokenStream tokenStream)
		{
			return ParseExpression(tokenStream, Precedences.EXPRESSION);
		}

		IExpression ParseExpression(TokenStream tokenStream, int precedence)
		{
			Token token = tokenStream.Consume();

			IPrefixParselet prefixParselet;
			if (!_prefixParselets.TryGetValue(token.TokenType, out prefixParselet))
			{
				// TODO better exception
				throw new ArgumentException(
					"Could not parse \"" + token.Content + "\".");
			}
			IExpression leftExpression = prefixParselet.Parse(ParseExpression, tokenStream, token);

			IInfixParselet infixParselet;
			while (_infixParselets.TryGetValue(tokenStream.Peek().TokenType, out infixParselet)
				   	&& (precedence < infixParselet.Precedence))
			{
				tokenStream.Consume();
				leftExpression = infixParselet.Parse(ParseExpression, tokenStream, leftExpression);
			}

			return leftExpression;
		}
	}
}
