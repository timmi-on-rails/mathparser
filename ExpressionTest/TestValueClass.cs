﻿using MathParser;
using NUnit.Framework;

namespace ExpressionTest
{
	[TestFixture]
	public class TestValueClass
	{
		[Test]
		public void TestInteger()
		{
			Value integer = Value.Integer(16);
			Assert.IsTrue(integer.IsInteger);
			Assert.IsFalse(integer.IsFloatingPointNumber);
			Assert.IsFalse(integer.IsBoolean);
			Assert.IsFalse(integer.IsFunction);
		}

		[Test]
		public void TestFloatingPointNumber()
		{
			Value floatingPointNumber = Value.FloatingPointNumber(-2.3);
			Assert.IsFalse(floatingPointNumber.IsInteger);
			Assert.IsTrue(floatingPointNumber.IsFloatingPointNumber);
			Assert.IsFalse(floatingPointNumber.IsBoolean);
			Assert.IsFalse(floatingPointNumber.IsFunction);
		}

		[Test]
		public void TestBoolean()
		{
			Value boolean = Value.Boolean(true);
			Assert.IsFalse(boolean.IsInteger);
			Assert.IsFalse(boolean.IsFloatingPointNumber);
			Assert.IsTrue(boolean.IsBoolean);
			Assert.IsFalse(boolean.IsFunction);
		}

		[Test]
		public void TestFunction()
		{
			Value function = Value.Function(someCalculationFunction);
			Assert.IsFalse(function.IsInteger);
			Assert.IsFalse(function.IsFloatingPointNumber);
			Assert.IsFalse(function.IsBoolean);
			Assert.IsTrue(function.IsFunction);
		}

		Value someCalculationFunction(params Value[] arguments)
		{
			return null;
		}

		// TODO test exception throwing on conversions

	}
}
