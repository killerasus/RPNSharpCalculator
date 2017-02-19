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

