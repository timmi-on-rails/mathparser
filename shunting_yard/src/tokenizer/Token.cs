using System;

namespace shunting_yard
{
	// TODO needs some context for showing error messages
	class Token
	{
		public TokenType TokenType { get; }

		public string Value { get; }

		public Token(TokenType tokenType, string value)
		{
			TokenType = tokenType;
			Value = value;
		}
	}
}
