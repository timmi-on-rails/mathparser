using System;
using System.Globalization;

namespace shunting_yard
{
	class IdentParselet : IPrefixParselet
	{
		public IExpression Parse(MathParser parser, Token token)
		{
			double result;
			if (Double.TryParse(token.Value, NumberStyles.Any, CultureInfo.InvariantCulture, out result))
			{
				return new ValueExpression(result);
			}
			else
			{
				return new VariableExpression(token.Value, parser.VariablesManager);
			}

			return new VariableExpression(token.Value, parser.VariablesManager);
		}
	}
}

