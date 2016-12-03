using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace shunting_yard
{
	static class Tokenizer
	{
		private static readonly IReadOnlyDictionary<string, TokenType> terminals = new Dictionary<string, TokenType>
		{
			{ "+", TokenType.Plus },
			{ "-", TokenType.Minus },
			{ "*", TokenType.Star },
			{ "/", TokenType.Slash },
			{ "(", TokenType.Lpar },
			{ ")", TokenType.Rpar },
			{ "^", TokenType.Pow },
			{ "=", TokenType.Equal },
			{ ",", TokenType.Comma }
		};

		public static IEnumerable<Token> GetTokens(string expression)
		{
			Regex RE = new Regex(@"([\" + String.Join(@"\", terminals.Keys) + "])");
			string[] tokens = RE.Split(expression).Where(x => !string.IsNullOrEmpty(x)).ToArray();

			foreach (string token in tokens)
			{
				if (terminals.ContainsKey(token))
				{
					yield return new Token(terminals[token], token);
				}
				else
				{
					yield return new Token(TokenType.Ident, token.Trim());
				}
			}

			yield return new Token(TokenType.Eof, "");
		}
	}
}
