using System;

namespace shunting_yard
{
	public class ValueExpression : IExpression
	{
		public double Value { get; }

		public ValueExpression(double value)
		{
			Value = value;
		}

		public bool CanEvaluate()
		{
			return true;
		}

		public double Evaluate()
		{
			return Value;
		}

		public override string ToString()
		{
			return Value.ToString();
		}
	}
}
