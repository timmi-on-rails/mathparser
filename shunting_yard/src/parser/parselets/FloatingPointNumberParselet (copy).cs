﻿using System;
using System.Globalization;
using Tokenizer;

namespace MathParser
{
	class FloatingPointNumberParselet : IPrefixParselet
	{
		public IExpression Parse(ParseExpressionDelegate parseExpression, TokenStream tokenStream, Token token)
		{
			double result = Double.Parse(token.Content, NumberStyles.Any, CultureInfo.InvariantCulture);
			return new ValueExpression(Value.Decimal(result));
		}
	}
}
