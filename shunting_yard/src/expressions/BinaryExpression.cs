using System;
using System.Collections.Generic;

namespace shunting_yard
{
	abstract class BinaryExpression : IExpression
	{
		public IExpression Operand1 { get; }

		public IExpression Operand2 { get; }

		public IEnumerable<IExpression> Children
		{
			get
			{
				yield return Operand1;
				yield return Operand2;
			}
		}

		public BinaryExpression(IExpression operand1, IExpression operand2)
		{			
			this.Operand1 = operand1;
			this.Operand2 = operand2;
		}

		public virtual bool CanEvaluate()
		{
			return Operand1.CanEvaluate() && Operand2.CanEvaluate();
		}

		public abstract double Evaluate();
	}
}
