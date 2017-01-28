namespace MathParser
{
	public class Token
	{
		public TokenType TokenType { get; }

		public string Content { get; }

		public int Position { get; }

		public Token(TokenType tokenType, string content, int position)
		{
			TokenType = tokenType;
			Content = content;
			Position = position;
		}
	}
}
