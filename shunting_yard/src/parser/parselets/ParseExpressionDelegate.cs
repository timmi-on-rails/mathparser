namespace MathParser
{
	delegate IExpression ParseExpressionDelegate(TokenStream tokenStream, int precedence = Precedences.EXPRESSION);
}
