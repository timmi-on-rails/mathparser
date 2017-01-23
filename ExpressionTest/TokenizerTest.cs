using System;
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
			AssertTokensMatch("=", Token(TokenType.Assignment, "=", 0), Token(TokenType.EndOfFile, "", 1));
			AssertTokensMatch("==", Token(TokenType.Equal, "==", 0), Token(TokenType.EndOfFile, "", 2));
			AssertTokensMatch("!=", Token(TokenType.NotEqual, "!=", 0), Token(TokenType.EndOfFile, "", 2));
			AssertTokensMatch("(", Token(TokenType.LeftParenthesis, "(", 0), Token(TokenType.EndOfFile, "", 1));
			AssertTokensMatch(")", Token(TokenType.RightParenthesis, ")", 0), Token(TokenType.EndOfFile, "", 1));
			AssertTokensMatch("+", Token(TokenType.Plus, "+", 0), Token(TokenType.EndOfFile, "", 1));
			AssertTokensMatch("-", Token(TokenType.Minus, "-", 0), Token(TokenType.EndOfFile, "", 1));
			AssertTokensMatch("*", Token(TokenType.Star, "*", 0), Token(TokenType.EndOfFile, "", 1));
			AssertTokensMatch("/", Token(TokenType.Slash, "/", 0), Token(TokenType.EndOfFile, "", 1));
			AssertTokensMatch("^", Token(TokenType.Pow, "^", 0), Token(TokenType.EndOfFile, "", 1));
			AssertTokensMatch("?", Token(TokenType.QuestionMark, "?", 0), Token(TokenType.EndOfFile, "", 1));
			AssertTokensMatch(":", Token(TokenType.Colon, ":", 0), Token(TokenType.EndOfFile, "", 1));
			AssertTokensMatch("", Token(TokenType.EndOfFile, "", 0));
			AssertTokensMatch("<", Token(TokenType.Less, "<", 0), Token(TokenType.EndOfFile, "", 1));
			AssertTokensMatch(">", Token(TokenType.Greater, ">", 0), Token(TokenType.EndOfFile, "", 1));
			AssertTokensMatch(",", Token(TokenType.Comma, ",", 0), Token(TokenType.EndOfFile, "", 1));
			AssertTokensMatch("<=", Token(TokenType.LessOrEqual, "<=", 0), Token(TokenType.EndOfFile, "", 2));
			AssertTokensMatch(">=", Token(TokenType.GreaterOrEqual, ">=", 0), Token(TokenType.EndOfFile, "", 2));
			AssertTokensMatch("true", Token(TokenType.True, "true", 0), Token(TokenType.EndOfFile, "", 4));
			AssertTokensMatch("false", Token(TokenType.False, "false", 0), Token(TokenType.EndOfFile, "", 5));
			AssertTokensMatch("365", Token(TokenType.Integer, "365", 0), Token(TokenType.EndOfFile, "", 3));
			AssertTokensMatch("x", Token(TokenType.Identifier, "x", 0), Token(TokenType.EndOfFile, "", 1));
			AssertTokensMatch("!", Token(TokenType.Exclamation, "!", 0), Token(TokenType.EndOfFile, "", 1));
			AssertTokensMatch("  ", Token(TokenType.WhiteSpace, "  ", 0), Token(TokenType.EndOfFile, "", 2));
			AssertTokensMatch("\t ", Token(TokenType.WhiteSpace, "\t ", 0), Token(TokenType.EndOfFile, "", 2));
		}

		[Test]
		public void TestNumberToken()
		{
			AssertTokensMatch("23123", Token(TokenType.Integer, "23123", 0), Token(TokenType.EndOfFile, "", 5));
			AssertTokensMatch(".23", Token(TokenType.FloatingPointNumber, ".23", 0), Token(TokenType.EndOfFile, "", 3));
			AssertTokensMatch("13.3", Token(TokenType.FloatingPointNumber, "13.3", 0), Token(TokenType.EndOfFile, "", 4));
			AssertTokensMatch("27.", Token(TokenType.Unknown, "27.", 0), Token(TokenType.EndOfFile, "", 3));
			AssertTokensMatch("1e10", Token(TokenType.FloatingPointNumber, "1e10", 0), Token(TokenType.EndOfFile, "", 4));
			AssertTokensMatch("1.e10", Token(TokenType.Unknown, "1.e10", 0), Token(TokenType.EndOfFile, "", 5));
			AssertTokensMatch("1.7e3", Token(TokenType.FloatingPointNumber, "1.7e3", 0), Token(TokenType.EndOfFile, "", 5));
			AssertTokensMatch("1.22e+4", Token(TokenType.FloatingPointNumber, "1.22e+4", 0), Token(TokenType.EndOfFile, "", 7));
			AssertTokensMatch("1.22e-34", Token(TokenType.FloatingPointNumber, "1.22e-34", 0), Token(TokenType.EndOfFile, "", 8));
			AssertTokensMatch("-3.4",
				Token(TokenType.Minus, "-", 0),
				Token(TokenType.FloatingPointNumber, "3.4", 1),
				Token(TokenType.EndOfFile, "", 4));
		}

		[Test]
		public void TestIdentifier()
		{
			AssertTokensMatch("43xyz",
				Token(TokenType.Integer, "43", 0),
				Token(TokenType.Identifier, "xyz", 2),
				Token(TokenType.EndOfFile, "", 5));
		}

		[Test]
		public void TestSomeOperators()
		{
			AssertTokensMatch("1+  y",
				Token(TokenType.Integer, "1", 0),
				Token(TokenType.Plus, "+", 1),
				Token(TokenType.WhiteSpace, "  ", 2),
				Token(TokenType.Identifier, "y", 4),
				Token(TokenType.EndOfFile, "", 5));

			AssertTokensMatch("1<=2",
				Token(TokenType.Integer, "1", 0),
				Token(TokenType.LessOrEqual, "<=", 1),
				Token(TokenType.Integer, "2", 3),
				Token(TokenType.EndOfFile, "", 4));
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
