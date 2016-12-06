namespace shunting_yard
{
	interface IInfixParselet
	{
		IExpression parse(MathParser parser, IExpression left, Token token);
		int Precedence { get; }
	}
}
