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

using System.Collections;

namespace RPNCalculator
{
    /// <summary>
    /// Calculator operations.
    /// </summary>
    public enum Operations
    {
        SUM,
        SUB,
        DIV,
        MUL,
        UNARY_MINUS
    };

    /// <summary>
    /// RPN Stack processing.
    /// </summary>
    public class RPNStack
    {
        public RPNStack ()
        {
            _stack = new Stack();
            _errorMessage = "";
        }

        /// <summary>
        /// Push the specified value into the stack
        /// </summary>
        /// <param name="val">Value.</param>
        public void PushValue(float val)
        {
            _stack.Push(val);
        }

        /// <summary>
        /// Execute operation on stack. Sets ErrorMessage if operation is unsuccessful.
        /// </summary>
        /// <param name="operation">An operation</param>
        /// <returns>True if operation successful, False otherwise</returns>
        public bool PushOperation(Operations operation)
        {
            ErrorMessage = "";

            switch(operation)
            {
                case Operations.SUM:
                    if(_stack.Count < 2)
                    {
                        ErrorMessage = "Stack has less than 2 elements.";
                        return false;
                    }
                    else
                    {
                        float val1 = (float)_stack.Pop();
                        float val2 = (float)_stack.Pop();
                        _stack.Push(val2 + val1);
                    }
                    break;
                case Operations.SUB:
                    if(_stack.Count < 2)
                    {
                        ErrorMessage = "Stack has less than 2 elements.";
                        return false;
                    }
                    else
                    {
                        float val1 = (float)_stack.Pop();
                        float val2 = (float)_stack.Pop();
                        _stack.Push(val2 - val1);
                    }
                    break;
                case Operations.DIV:
                    if (_stack.Count < 2)
                    {
                        ErrorMessage = "Stack has less than 2 elements.";
                        return false;
                    }
                    else
                    {
                        float val1 = (float)_stack.Pop();
                        float val2 = (float)_stack.Pop();

                        if(val2.CompareTo(0.0f) == 0)
                        {
                            ErrorMessage = "Division by 0.";
                            _stack.Push(float.NaN);
                            return false;
                        }
                        _stack.Push(val2 / val1);
                    }
                    break;
                case Operations.MUL:
                    if (_stack.Count < 2)
                    {
                        ErrorMessage = "Stack has less than 2 elements.";
                        return false;
                    }
                    else
                    {
                        float val1 = (float)_stack.Pop();
                        float val2 = (float)_stack.Pop();
                        _stack.Push(val2 * val1);
                    }
                    break;
                case Operations.UNARY_MINUS:
                    {
                        if (_stack.Count < 1)
                        {
                            ErrorMessage = "Stack has less than 1 elements.";
                            return false;
                        }
                        else
                        {
                            float val = (float) _stack.Pop();
                            _stack.Push(-val);
                        }
                    }
                    break;
                default:
                    ErrorMessage = "Unknown operation.";
                    return false;

            }
            return true;
        }

        /// <summary>
        /// Pops the top of the stack
        /// </summary>
        /// <returns>Value on top of the stack, NaN if no value is pushed</returns>
        public float Pop( )
        {
            if(_stack.Count > 0)
            {
                return (float) _stack.Pop();
            }
            
            return float.NaN;
        }

        /// <summary>
        /// Gets the error message, if any.
        /// </summary>
        /// <value>The error message.</value>
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            private set
            {
                _errorMessage = value;
            }
        }

        /// <summary>
        /// Stack collection used to store values
        /// </summary>
        private Stack _stack;

        /// <summary>
        /// Holds the error message for an unsuccessful operation
        /// </summary>
        private string _errorMessage;
    }
}

