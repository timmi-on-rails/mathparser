using System;
using System.Globalization;

namespace MathParser
{
	class NumberParselet : IPrefixParselet
	{
		public IExpression Parse(ParseExpressionDelegate parseExpression, TokenStream tokenStream, Token token)
		{
			double result = Double.Parse(token.Content, NumberStyles.Any, CultureInfo.InvariantCulture);
			return new NumberExpression(result);
		}
	}
}
