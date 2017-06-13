using System;
using System.Globalization;
using Tokenizer;

namespace MathParser
{
	class IntegerParselet : IPrefixParselet
	{
		public IExpression Parse(ParseExpressionDelegate parseExpression, TokenStream tokenStream, Token token)
		{
			long result = Int64.Parse(token.Content, NumberStyles.Any, CultureInfo.InvariantCulture);
			return new ValueExpression(Value.Integer(result));
		}
	}
}
