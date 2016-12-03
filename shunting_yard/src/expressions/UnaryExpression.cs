using System;

namespace shunting_yard
{
	abstract class UnaryExpression : IExpression
	{
		public IExpression Operand { get; }

		public UnaryExpression(IExpression operand)
		{			
			Operand = operand;
		}

		public virtual bool CanEvaluate()
		{
			return Operand.CanEvaluate();
		}

		public abstract double Evaluate();
	}
}
