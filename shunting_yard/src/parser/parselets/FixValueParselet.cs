using Tokenizer;

namespace MathParser
{
	class FixValueParselet : IPrefixParselet
	{
		readonly Value _value;

		public FixValueParselet(Value value)
		{
			_value = value;
		}

		public IExpression Parse(ParseExpressionDelegate parseExpression, TokenStream tokenStream, Token token)
		{
			return new ValueExpression(_value);
		}
	}
}
