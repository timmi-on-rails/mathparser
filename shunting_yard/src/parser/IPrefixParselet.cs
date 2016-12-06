using System;

namespace shunting_yard
{
	interface IPrefixParselet
	{
		IExpression Parse(MathParser parser, Token token);
	}
}

