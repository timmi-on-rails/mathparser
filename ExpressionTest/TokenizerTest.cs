using NUnit.Framework;
using System;
using System.Linq;
using MathParser;
using System.Collections.Generic;

namespace ExpressionTest
{
	[TestFixture]
	public class TokenizerTest
	{
		private void AssertTokensMatch(string expression, params Token[] expectedTokens)
		{
			IEnumerable<Token> actualTokens = Tokenizer.GetTokens(expression);

			Assert.IsTrue(actualTokens.SequenceEqual(expectedTokens));
		}

		private Token Token(TokenType tokenType, string value)
		{
			return new Token(tokenType, value, 0);
		}

		[Test]
		public void TestQuestionMark()
		{
			Assert.AreEqual(TokenType.QuestionMark, Tokenizer.GetTokens("?").First().TokenType);
		}

		[Test]
		public void TestMultiChar()
		{
			Assert.AreEqual(TokenType.LessOrEqual, Tokenizer.GetTokens("<=").First().TokenType);
		}

		//[Test]
		public void TestCase()
		{
			AssertTokensMatch("x+y",
				Token(TokenType.Identifier, "x"),
				Token(TokenType.Plus, "+"),
				Token(TokenType.Identifier, "y")
			);
		}
	}
}

