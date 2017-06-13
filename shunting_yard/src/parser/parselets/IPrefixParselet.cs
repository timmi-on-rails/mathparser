using Tokenizer;

namespace MathParser
{
	interface IPrefixParselet
	{
		IExpression Parse(ParseExpressionDelegate parseExpression, TokenStream tokenStream, Token token);
	}
}
