using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace MathParser
{
	static class Tokenizer
	{
		static readonly IReadOnlyDictionary<string, TokenType> terminals = new Dictionary<string, TokenType>
		{
			{ "+", TokenType.Plus },
			{ "-", TokenType.Minus },
			{ "*", TokenType.Star },
			{ "/", TokenType.Slash },
			{ "(", TokenType.Lpar },
			{ ")", TokenType.Rpar },
			{ "^", TokenType.Pow },
			{ "=", TokenType.Equal },
			{ ",", TokenType.Comma },
			{ "?", TokenType.QuestionMark },
			{ ":", TokenType.Colon },
			{ "<", TokenType.Smaller },
			{ ">", TokenType.Bigger },
			{ "<=", TokenType.LessOrEqual }
		};

		static readonly int maxCharCountTerminals;

		static Tokenizer()
		{
			maxCharCountTerminals = terminals.Keys.Max(terminal => terminal.Length);
		}

		public static IEnumerable<Token> GetTokens(string expression)
		{
			Regex RE = new Regex(@"([\s\" + String.Join(@"\", terminals.Keys) + "])");
			string[] tokensWithWhiteSpace = RE.Split(expression).ToArray();

			int i = 0;

			foreach (string token in tokensWithWhiteSpace)
			{
				if (!string.IsNullOrWhiteSpace(token))
				{
					if (terminals.ContainsKey(token))
					{
						yield return new Token(terminals[token], token, i);
					}
					else
					{
						string trimmedToken = token.Trim();
						int index = i + token.IndexOf(trimmedToken, StringComparison.Ordinal);
						double tmp;
						if (Double.TryParse(trimmedToken, NumberStyles.Any, CultureInfo.InvariantCulture, out tmp))
						{
							yield return new Token(TokenType.Numeric, trimmedToken, index);
						}
						else if (Char.IsLetter(trimmedToken[0]) && trimmedToken.All(c => Char.IsLetterOrDigit(c) || c == '_'))
						{
							yield return new Token(TokenType.Identifier, trimmedToken, index);
						}
						else
						{
							yield return new Token(TokenType.Unknown, trimmedToken, index);
						}
					}
				}

				i += token.Length;
			}

			yield return new Token(TokenType.Eof, "", i);
		}

		/*
		public static IEnumerable<Token> GetTokens(string expression)
		{
			Regex RE = new Regex(@"([\s\" + String.Join(@"\", terminals.Keys) + "])");
			string[] tokensWithWhiteSpace = RE.Split(expression).ToArray();

			int i = 0;

			foreach (string token in tokensWithWhiteSpace)
			{
				if (!string.IsNullOrWhiteSpace(token))
				{
					if (terminals.ContainsKey(token))
					{
						yield return new Token(terminals[token], token, i);
					}
					else
					{
						string trimmedToken = token.Trim();
						int index = i + token.IndexOf(trimmedToken, StringComparison.Ordinal);
						double tmp;
						if (Double.TryParse(trimmedToken, NumberStyles.Any, CultureInfo.InvariantCulture, out tmp))
						{
							yield return new Token(TokenType.Numeric, trimmedToken, index);
						}
						else if (Char.IsLetter(trimmedToken[0]) && trimmedToken.All(c => Char.IsLetterOrDigit(c) || c == '_'))
						{
							yield return new Token(TokenType.Identifier, trimmedToken, index);
						}
						else
						{
							yield return new Token(TokenType.Unknown, trimmedToken, index);
						}
					}
				}

				i += token.Length;
			}

			yield return new Token(TokenType.Eof, "", i);
		}
		*/
	}
}
