namespace MathParser
{
	public partial class Value
	{
		public static Value Add(Value valueA, Value valueB)
		{
			if (valueA.IsInteger && valueB.IsInteger)
			{
				long a = valueA.ToInt64();
				long b = valueB.ToInt64();
				return Integer(a + b);
			}

			bool isNumberA = valueA.IsDecimal || valueA.IsInteger;
			bool isNumberB = valueB.IsDecimal || valueB.IsInteger;

			if (isNumberA && isNumberB)
			{
				double a = valueA.ToDouble();
				double b = valueB.ToDouble();
				return Decimal(a + b);
			}

			if (valueA.IsExpression || valueB.IsExpression)
			{
				IExpression a = new ValueExpression(valueA);
				IExpression b = new ValueExpression(valueB);
				IExpression result = new BinaryExpression(BinaryExpressionType.Addition, a, b);
				return Expression(new Expression(result));
			}

			string message = string.Format("Incompatible types of operands {0} and {1} for binary operation {2}.",
										   valueA.ValueType, valueB.ValueType, BinaryExpressionType.Addition);
			throw new EvaluationException(message);
		}
	}
}
