// MIT License
//
// Copyright (c) 2017 Bruno Baère Pederassi Lomba de Araujo
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
//
using NUnit.Framework;
using System;
using RPNCalculator;

namespace RPNCalculatorUnitTests
{
	[TestFixture()]
	public class Test
	{
		private RPNStack _stack;

		[SetUp()]
		public void Setup()
		{
			_stack = new RPNStack();
		}

		[Test()]
		public void IsEmptyOnCreation()
		{
			float result = _stack.Pop();
			Assert.IsNaN(result);
		}

		[Test()]
		public void TwoElementsOnStack()
		{
			_stack.PushValue (2);
			_stack.PushValue (3);
			Assert.AreEqual (3, _stack.Pop ());
			Assert.AreEqual (2, _stack.Pop ());
		}

		[Test()]
		public void SumOneElement()
		{
			_stack.PushValue (2);
			Assert.False(_stack.PushOperation (Operations.SUM));
			Assert.AreEqual ( "Stack has less than 2 elements.", _stack.ErrorMessage);
		}

		[Test()]
		public void SumTwoElements()
		{
			_stack.PushValue (2);
			_stack.PushValue (3);
			Assert.True( _stack.PushOperation (Operations.SUM) );
			Assert.AreEqual (5, _stack.Pop ());
		}

		[Test()]
		public void Negative()
		{
			_stack.PushValue (2);
			Assert.True( _stack.PushOperation (Operations.UNARY_MINUS) );
			Assert.AreEqual (-2, _stack.Pop ());
			_stack.PushValue (-100);
			Assert.True( _stack.PushOperation (Operations.UNARY_MINUS) );
			Assert.AreEqual (100, _stack.Pop ());
		}

		[Test()]
		public void NegativeEmptyStack()
		{
			Assert.False( _stack.PushOperation (Operations.UNARY_MINUS) );
			Assert.AreEqual ("Stack has less than 1 elements.", _stack.ErrorMessage);
		}

		[Test()]
		public void SumTwoElementsNegative()
		{
			_stack.PushValue (2);
			_stack.PushValue (3);
			Assert.True( _stack.PushOperation (Operations.UNARY_MINUS) );
			Assert.True( _stack.PushOperation (Operations.SUM) );
			Assert.AreEqual (-1, _stack.Pop ());
		}

		[Test()]
		public void DivisionByZero()
		{
			_stack.PushValue (0);
			_stack.PushValue (20);
			Assert.False (_stack.PushOperation (Operations.DIV));
			Assert.AreEqual ("Division by 0.", _stack.ErrorMessage);
			Assert.IsNaN (_stack.Pop ());
		}

		[Test()]
		public void SquareRootNoElement()
		{
			Assert.False( _stack.PushOperation (Operations.SQUARE_ROOT) );
			Assert.AreEqual ("Stack has less than 1 elements.", _stack.ErrorMessage);
		}

		[Test()]
		public void SquareRootNegativeElement()
		{
			_stack.PushValue (-4);
			Assert.True (_stack.PushOperation (Operations.SQUARE_ROOT));
			Assert.AreEqual (sqrt (-4), _stack.Pop());
		}

		[Test()]
		public void SuqareRootOf4()
		{
			_stack.PushValue (4);
			Assert.True (_stack.PushOperation (Operations.SQUARE_ROOT));
			Assert.AreEqual (sqrt (4), _stack.Pop ());
		}
	}

}

