﻿using System;
using System.Collections.Generic;
using MathParser;
using NUnit.Framework;

namespace ExpressionTest
{
	[TestFixture]
	public class TokenizerTest
	{
		[Test]
		public void TestEveryTokenType()
		{
			AssertTokensMatch("=", Token(TokenType.Assignment, "=", 0), Token(TokenType.Eof, "", 1));
			AssertTokensMatch("==", Token(TokenType.Equal, "==", 0), Token(TokenType.Eof, "", 2));
			AssertTokensMatch("!=", Token(TokenType.NotEqual, "!=", 0), Token(TokenType.Eof, "", 2));
			AssertTokensMatch("(", Token(TokenType.LeftParenthesis, "(", 0), Token(TokenType.Eof, "", 1));
			AssertTokensMatch(")", Token(TokenType.Rpar, ")", 0), Token(TokenType.Eof, "", 1));
			AssertTokensMatch("+", Token(TokenType.Plus, "+", 0), Token(TokenType.Eof, "", 1));
			AssertTokensMatch("-", Token(TokenType.Minus, "-", 0), Token(TokenType.Eof, "", 1));
			AssertTokensMatch("*", Token(TokenType.Star, "*", 0), Token(TokenType.Eof, "", 1));
			AssertTokensMatch("/", Token(TokenType.Slash, "/", 0), Token(TokenType.Eof, "", 1));
			AssertTokensMatch("^", Token(TokenType.Pow, "^", 0), Token(TokenType.Eof, "", 1));
			AssertTokensMatch("?", Token(TokenType.QuestionMark, "?", 0), Token(TokenType.Eof, "", 1));
			AssertTokensMatch(":", Token(TokenType.Colon, ":", 0), Token(TokenType.Eof, "", 1));
			AssertTokensMatch("", Token(TokenType.Eof, "", 0));
			AssertTokensMatch("<", Token(TokenType.Smaller, "<", 0), Token(TokenType.Eof, "", 1));
			AssertTokensMatch(">", Token(TokenType.Bigger, ">", 0), Token(TokenType.Eof, "", 1));
			AssertTokensMatch(",", Token(TokenType.Comma, ",", 0), Token(TokenType.Eof, "", 1));
			AssertTokensMatch("<=", Token(TokenType.LessOrEqual, "<=", 0), Token(TokenType.Eof, "", 2));
			AssertTokensMatch(">=", Token(TokenType.BiggerOrEqual, ">=", 0), Token(TokenType.Eof, "", 2));
			AssertTokensMatch("true", Token(TokenType.True, "true", 0), Token(TokenType.Eof, "", 4));
			AssertTokensMatch("false", Token(TokenType.False, "false", 0), Token(TokenType.Eof, "", 5));
			AssertTokensMatch("ans", Token(TokenType.Ans, "ans", 0), Token(TokenType.Eof, "", 3));
			AssertTokensMatch("365", Token(TokenType.Numeric, "365", 0), Token(TokenType.Eof, "", 3));
			AssertTokensMatch("x", Token(TokenType.Identifier, "x", 0), Token(TokenType.Eof, "", 1));
			AssertTokensMatch("!", Token(TokenType.Unknown, "!", 0), Token(TokenType.Eof, "", 1));
			AssertTokensMatch("  ", Token(TokenType.WhiteSpace, "  ", 0), Token(TokenType.Eof, "", 2));
		}

		[Test]
		public void TestNumberToken()
		{
			AssertTokensMatch("23123", Token(TokenType.Numeric, "23123", 0), Token(TokenType.Eof, "", 5));
			AssertTokensMatch(".23", Token(TokenType.Numeric, ".23", 0), Token(TokenType.Eof, "", 3));
			AssertTokensMatch("13.3", Token(TokenType.Numeric, "13.3", 0), Token(TokenType.Eof, "", 4));
			AssertTokensMatch("27.", Token(TokenType.Unknown, "27.", 0), Token(TokenType.Eof, "", 3));
			AssertTokensMatch("1e10", Token(TokenType.Numeric, "1e10", 0), Token(TokenType.Eof, "", 4));
			AssertTokensMatch("1.e10", Token(TokenType.Unknown, "1.e10", 0), Token(TokenType.Eof, "", 5));
			AssertTokensMatch("1.7e3", Token(TokenType.Numeric, "1.7e3", 0), Token(TokenType.Eof, "", 5));
			AssertTokensMatch("1.22e+4", Token(TokenType.Numeric, "1.22e+4", 0), Token(TokenType.Eof, "", 7));
			AssertTokensMatch("1.22e-34", Token(TokenType.Numeric, "1.22e-34", 0), Token(TokenType.Eof, "", 8));
		}

		[Test]
		public void TestCase()
		{
			AssertTokensMatch("x+y",
				Token(TokenType.Identifier, "x", 0),
				Token(TokenType.Plus, "+", 1),
				Token(TokenType.Identifier, "y", 2),
				Token(TokenType.Eof, "", 3)
			);
		}

		Token Token(TokenType tokenType, string value, int position)
		{
			return new Token(tokenType, value, position);
		}

		void AssertTokensMatch(string expression, params Token[] expectedTokens)
		{
			IEnumerable<Token> actualTokens = Tokenizer.Tokenize(expression);

			IEnumerator<Token> actualEnumerator = actualTokens.GetEnumerator();
			IEnumerator<Token> expectedEnumerator = ((IEnumerable<Token>)expectedTokens).GetEnumerator();

			bool actualHasNext, expectedHasNext;

			while ((actualHasNext = actualEnumerator.MoveNext()) & (expectedHasNext = expectedEnumerator.MoveNext()))
			{
				Token actualToken = actualEnumerator.Current;
				Token expectedToken = expectedEnumerator.Current;

				bool tokensMatch = actualToken.TokenType == expectedToken.TokenType
								   && String.Equals(actualToken.Content, expectedToken.Content, StringComparison.Ordinal)
								   && actualToken.Position == expectedToken.Position;

				if (!tokensMatch)
				{
					string message = "expected token: {0},{1},{2} - got token: {3},{4},{5} in \"{6}\"";
					Assert.Fail(String.Format(message, expectedToken.TokenType, expectedToken.Content, expectedToken.Position,
											  actualToken.TokenType, actualToken.Content, actualToken.Position, expression));
				}
			}

			if (actualHasNext)
			{
				Assert.Fail("Did not expect token: " + actualEnumerator.Current.TokenType);
			}

			if (expectedHasNext)
			{
				Assert.Fail("Missing expected token: " + expectedEnumerator.Current.TokenType);
			}
		}
	}
}

