using System;

namespace MathParser
{
	public delegate Value Function(params Value[] arguments);

	public partial class Value
	{
		internal enum Type
		{
			Empty,
			Integer,
			Decimal,
			Boolean,
			Function,
			Expression
		}

		readonly object _value;

		internal Type ValueType { get; }

		public bool IsEmpty
		{
			get { return ValueType == Type.Empty; }
		}

		public bool IsExpression
		{
			get { return ValueType == Type.Expression; }
		}

		public bool IsInteger
		{
			get { return ValueType == Type.Integer; }
		}

		public bool IsDecimal
		{
			get { return ValueType == Type.Decimal; }
		}

		public bool IsBoolean
		{
			get { return ValueType == Type.Boolean; }
		}

		public bool IsFunction
		{
			get { return ValueType == Type.Function; }
		}

		Value(Type valueType, object value)
		{
			ValueType = valueType;
			_value = value;
		}

		public static Value Integer(long l)
		{
			return new Value(Type.Integer, l);
		}

		public static Value Decimal(double d)
		{
			return new Value(Type.Decimal, d);
		}

		public static Value Boolean(bool b)
		{
			return new Value(Type.Boolean, b);
		}
		// TODO function with identifier and argument names
		public static Value Function(Function function)
		{
			return new Value(Type.Function, function);
		}

		public static Value Empty()
		{
			return new Value(Type.Empty, null);
		}

		public static Value Expression(Expression expression)
		{
			return new Value(Type.Expression, expression);
		}

		public long ToInt64()
		{
			return Convert.ToInt64(_value);
		}

		public double ToDouble()
		{
			return Convert.ToDouble(_value);
		}

		public bool ToBoolean()
		{
			return Convert.ToBoolean(_value);
		}

		public Function ToFunction()
		{
			return (Function)_value;
		}

		public Expression ToExpression()
		{
			return (Expression)_value;
		}

		public override string ToString()
		{
			return _value.ToString();
		}
	}
}
