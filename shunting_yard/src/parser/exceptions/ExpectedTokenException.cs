using System;
using Tokenizer;

namespace MathParser
{
	class ExpectedTokenException : ParserException
	{
		public TokenType TokenType { get; }

		public int Position { get; }

		internal ExpectedTokenException() { }

		internal ExpectedTokenException(string message) : base(message) { }

		internal ExpectedTokenException(string message, Exception innerException) : base(message, innerException) { }

		internal ExpectedTokenException(TokenType tokenType, int position) : base("Expected token " + tokenType + " at position " + position)
		{
			TokenType = tokenType;
			Position = position;
		}
	}
}
