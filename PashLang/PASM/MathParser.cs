using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace PASM
{
    /// <summary>
    ///     This is a mathematical expression parser that allows you to parser a string value,
    ///     perform the required calculations, and return a value in form of a int.
    /// </summary>

    public class MathParser
    {
        #region Constructors and Destructors

        /// <summary>
        ///     This constructor will add some basic operators, functions, and variables
        ///     to the parser. Please note that you are able to change that using
        ///     boolean flags
        /// </summary>
        /// <param name="loadPreDefinedFunctions">
        ///     This will load "abs", "cos", "cosh", "arccos", "sin", "sinh", "arcsin", "tan",
        ///     "tanh", "arctan", "sqrt", "rem", "round"
        /// </param>
        /// <param name="loadPreDefinedOperators">This will load "%", "*", ":", "/", "+", "-", ">", "&lt;", "="</param>
        /// <param name="loadPreDefinedVariables">This will load "pi"</param>
        public MathParser(
            bool loadPreDefinedFunctions = true,
            bool loadPreDefinedOperators = true,
            bool loadPreDefinedVariables = true)
        {
            this.CULTURE_INFO = CultureInfo.InvariantCulture;
            this.LocalVariables = new Dictionary<string, decimal>();
            this.LocalFunctions = new Dictionary<string, Func<decimal[], decimal>>();
            this.OperatorAction = new Dictionary<string, Func<decimal, decimal, decimal>>();
            this.OperatorList = new List<string>();
            if (loadPreDefinedOperators)
            {
                // by default, we will load basic arithmetic operators.
                // please note, its possible to do it either inside the constructor,
                // or outside the class. the lowest value will be executed first!
                this.OperatorList.Add("%"); // modulo
                this.OperatorList.Add("^"); // to the power of
                this.OperatorList.Add(":"); // division 1
                this.OperatorList.Add("/"); // division 2
                this.OperatorList.Add("*"); // multiplication
                this.OperatorList.Add("-"); // subtraction
                this.OperatorList.Add("+"); // addition

                this.OperatorList.Add(">"); // greater than
                this.OperatorList.Add("<"); // less than
                this.OperatorList.Add("="); // are equal

                // when an operator is executed, the parser needs to know how.
                // this is how you can add your own operators. note, the order
                // in this list does not matter.
                this.OperatorAction.Add("%", (numberA, numberB) => numberA % numberB);
                this.OperatorAction.Add("^", (numberA, numberB) => (decimal)Math.Pow((double)numberA, (double)numberB));
                this.OperatorAction.Add(":", (numberA, numberB) => numberA / numberB);
                this.OperatorAction.Add("/", (numberA, numberB) => numberA / numberB);
                this.OperatorAction.Add("*", (numberA, numberB) => numberA * numberB);
                this.OperatorAction.Add("+", (numberA, numberB) => numberA + numberB);
                this.OperatorAction.Add("-", (numberA, numberB) => numberA - numberB);

                this.OperatorAction.Add(">", (numberA, numberB) => numberA > numberB ? 1 : 0);
                this.OperatorAction.Add("<", (numberA, numberB) => numberA < numberB ? 1 : 0);
                this.OperatorAction.Add("=", (numberA, numberB) => numberA == numberB ? 1 : 0);
            }

            if (loadPreDefinedFunctions)
            {
                // these are the basic functions you might be able to use.
                // as with operators, localFunctions might be adjusted, i.e.
                // you can add or remove a function.
                // please open the "MathosTest" project, and find MathParser.cs
                // in "CustomFunction" you will see three ways of adding 
                // a new function to this variable!
                // EACH FUNCTION MAY ONLY TAKE ONE PARAMETER, AND RETURN ONE
                // VALUE. THESE VALUES SHOULD BE IN "DECIMAL FORMAT"!
                this.LocalFunctions.Add("abs", x => (decimal)Math.Abs((double)x[0]));

                this.LocalFunctions.Add("cos", x => (decimal)Math.Cos((double)x[0]));
                this.LocalFunctions.Add("cosh", x => (decimal)Math.Cosh((double)x[0]));
                this.LocalFunctions.Add("arccos", x => (decimal)Math.Acos((double)x[0]));

                this.LocalFunctions.Add("sin", x => (decimal)Math.Sin((double)x[0]));
                this.LocalFunctions.Add("sinh", x => (decimal)Math.Sinh((double)x[0]));
                this.LocalFunctions.Add("arcsin", x => (decimal)Math.Asin((double)x[0]));

                this.LocalFunctions.Add("tan", x => (decimal)Math.Tan((double)x[0]));
                this.LocalFunctions.Add("tanh", x => (decimal)Math.Tanh((double)x[0]));
                this.LocalFunctions.Add("arctan", x => (decimal)Math.Atan((double)x[0]));

                this.LocalFunctions.Add("sqrt", x => (decimal)Math.Sqrt((double)x[0]));
                this.LocalFunctions.Add("rem", x => (decimal)Math.IEEERemainder((double)x[0], (double)x[1]));
                this.LocalFunctions.Add("round", x => (decimal)Math.Round((double)x[0]));
                this.LocalFunctions.Add("pow", x => (decimal)Math.Pow((double)x[0], (double)x[1]));

                this.LocalFunctions.Add("rndm", x => (decimal)new Random().Next((int)x[0], (int)x[1]));
                this.LocalFunctions.Add("factorial", x => (decimal)this.Factorial((int)x[0]));
            }

            if (loadPreDefinedVariables)
            {
                // local variables such as pi can also be added into the parser.
                this.LocalVariables.Add("pi", (decimal)Math.PI);
                this.LocalVariables.Add("e", (decimal)Math.E);
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     When converting the result from the Parse method or ProgrammaticallyParse method ToString(),
        ///     please use this cultur info.
        /// </summary>
        public CultureInfo CULTURE_INFO { get; private set; }

        /* THE LOCAL VARIABLES */

        /// <summary>
        ///     All functions that you want to define should be inside this property.
        /// </summary>
        public Dictionary<string, Func<decimal[], decimal>> LocalFunctions { get; set; }

        /// <summary>
        ///     All variables that you want to define should be inside this property.
        /// </summary>
        public Dictionary<string, decimal> LocalVariables { get; set; }

        /// <summary>
        ///     When adding a variable in the OperatorList property, you need to assign how that operator should work.
        /// </summary>
        public Dictionary<string, Func<decimal, decimal, decimal>> OperatorAction { get; set; }

        /// <summary>
        ///     All operators should be inside this property.
        ///     The first operator is executed first, et cetera.
        ///     An operator may only be ONE character.
        /// </summary>
        public List<string> OperatorList { get; set; }

        #endregion

        /* PARSER FUNCTIONS, PUBLIC */

        #region Public Methods and Operators

        /// <summary>
        ///     Enter the math expression in form of a string.
        /// </summary>
        /// <param name="mathExpression"></param>
        /// <returns></returns>
        public decimal Parse(string mathExpression)
        {
            return this.Scanner(mathExpression);
        }

        /// <summary>
        ///     Enter the math expression in form of a string. You might also add/edit variables using "let" keyword.
        ///     For example, "let sampleVariable = 2+2".
        ///     Another way of adding/editing a variable is to type "varName := 20"
        ///     Last way of adding/editing a variable is to type "let varName be 20"
        /// </summary>
        /// <param name="mathExpression"></param>
        /// <returns></returns>
        public decimal ProgrammaticallyParse(string mathExpression)
        {
            if (mathExpression.Contains("="))
            {
                //mathExpression = mathExpression.Replace(" ", ""); // remove white space
                string varName = mathExpression.Substring(0, mathExpression.IndexOf("="));
                mathExpression = mathExpression.Replace(varName + "=", "");

                decimal varValue = this.Parse(mathExpression);
                varName = varName.Replace(" ", "");
                if (this.LocalVariables.ContainsKey(varName))
                {
                    this.LocalVariables[varName] = varValue;
                }
                else
                {
                    this.LocalVariables.Add(varName, varValue);
                }

                return varValue;
            }
            return this.Parse(mathExpression);
        }

        #endregion

        /* UNDER THE HOOD - THE CORE OF THE PARSER */

        #region Methods

        private decimal BasicArithmeticalExpression(List<string> _tokens)
        {
            // PERFORMING A BASIC ARITHMETICAL EXPRESSION CALCULATION
            // THIS METHOD CAN ONLY OPERATE WITH NUMBERS AND OPERATORS
            // AND WILL NOT UNDERSTAND ANYTHING BEYOND THAT.

            if (_tokens.Count == 1)
            {
                return decimal.Parse(_tokens[0], this.CULTURE_INFO);
            }
            if (_tokens.Count == 2)
            {
                string op = _tokens[0];
                if (op == "-" || op == "+")
                {
                    return decimal.Parse((op == "+" ? "" : "-") + _tokens[1], this.CULTURE_INFO);
                }
                return this.OperatorAction[op](0, decimal.Parse(_tokens[1], this.CULTURE_INFO));
            }
            if (_tokens.Count == 0)
            {
                return 0;
            }
            foreach (string op in this.OperatorList)
            {
                while (_tokens.IndexOf(op) != -1)
                {
                    int opPlace = _tokens.IndexOf(op);

                    decimal numberA = Convert.ToDecimal(_tokens[opPlace - 1], this.CULTURE_INFO);
                    decimal numberB = Convert.ToDecimal(_tokens[opPlace + 1], this.CULTURE_INFO);

                    decimal result = this.OperatorAction[op](numberA, numberB);

                    _tokens[opPlace - 1] = result.ToString(this.CULTURE_INFO);
                    _tokens.RemoveRange(opPlace, 2);
                }
            }
            return Convert.ToDecimal(_tokens[0], this.CULTURE_INFO);
        }

        private int Factorial(int i)
        {
            if (i <= 1)
            {
                return 1;
            }
            return i * this.Factorial(i - 1);
        }

        private decimal MathParserLogic(List<string> _tokens)
        {
            // CALCULATING THE EXPRESSIONS INSIDE THE BRACKETS
            // IF NEEDED, EXECUTE A FUNCTION

            while (_tokens.IndexOf("(") != -1)
            {
                // getting data between "(", ")"
                int open = _tokens.LastIndexOf("(");
                int close = _tokens.IndexOf(")", open); // in case open is -1, i.e. no "(" // , open == 0 ? 0 : open - 1
                if (open >= close)
                {
                    // if there is no closing bracket, throw a new exception
                    throw new ArithmeticException("No closing bracket/parenthesis! tkn: " + open);
                }
                var roughExpr = new List<string>();
                for (int i = open + 1; i < close; i++)
                {
                    roughExpr.Add(_tokens[i]);
                }

                decimal result; // the temporary result is stored here

                string functioName = _tokens[open == 0 ? 0 : open - 1];
                var _args = new decimal[0];
                if (this.LocalFunctions.ContainsKey(functioName))
                {
                    if (roughExpr.Contains(","))
                    {
                        // converting all arguments into a decimal array
                        for (int i = 0; i < roughExpr.Count; i++)
                        {
                            int firstCommaOrEndOfExpression = roughExpr.IndexOf(",", i) != -1
                                ? roughExpr.IndexOf(",", i)
                                : roughExpr.Count;

                            var defaultExpr = new List<string>();
                            while (i < firstCommaOrEndOfExpression)
                            {
                                defaultExpr.Add(roughExpr[i]);
                                i++;
                            }

                            // changing the size of the array of arguments
                            Array.Resize(ref _args, _args.Length + 1);
                            if (defaultExpr.Count == 0)
                            {
                                _args[_args.Length - 1] = 0;
                            }
                            else
                            {
                                _args[_args.Length - 1] = this.BasicArithmeticalExpression(defaultExpr);
                            }
                        }

                        // finnaly, passing the arguments to the given function
                        result = decimal.Parse(
                            this.LocalFunctions[functioName](_args).ToString(this.CULTURE_INFO),
                            this.CULTURE_INFO);
                    }

                    else
                    {
                        // but if we only have one argument, then we pass it directly to the function
                        result =
                            decimal.Parse(
                                this.LocalFunctions[functioName](new[] { this.BasicArithmeticalExpression(roughExpr) })
                                    .ToString(this.CULTURE_INFO),
                                this.CULTURE_INFO);
                    }
                }
                else
                {
                    // if no function is need to execute following expression, pass it
                    // to the "BasicArithmeticalExpression" method.
                    result = this.BasicArithmeticalExpression(roughExpr);
                }

                // when all the calculations have been done
                // we replace the "opening bracket with the result"
                // and removing the rest.
                _tokens[open] = result.ToString(this.CULTURE_INFO);
                _tokens.RemoveRange(open + 1, close - open);
                if (this.LocalFunctions.ContainsKey(functioName))
                {
                    // if we also executed a function, removing
                    // the function name as well.
                    _tokens.RemoveAt(open - 1);
                }
            }

            // at this point, we should have replaced all brackets
            // with the appropriate values, so we can simply
            // calculate the expression. it's not so complex
            // any more!
            return this.BasicArithmeticalExpression(_tokens);
        }

        private decimal Scanner(string _expr)
        {
            // SCANNING THE INPUT STRING AND CONVERT IT INTO TOKENS

            var _tokens = new List<string>();
            string _vector = "";

            //_expr = _expr.Replace(" ", ""); // remove white space
            _expr = _expr.Replace("+-", "-"); // some basic arithmetical rules
            _expr = _expr.Replace("-+", "-");
            _expr = _expr.Replace("--", "+");

            _expr = this.LocalVariables.Aggregate(
                _expr,
                (current, item) => current.Replace(item.Key, "(" + item.Value.ToString(this.CULTURE_INFO) + ")"));

            for (int i = 0; i < _expr.Length; i++)
            {
                char ch = _expr[i];

                if (char.IsWhiteSpace(ch))
                {
                } // could also be used to remove white spaces.
                else if (Char.IsLetter(ch))
                {
                    if (i != 0 && (Char.IsDigit(_expr[i - 1]) || Char.IsDigit(_expr[i - 1]) || _expr[i - 1] == ')'))
                    {
                        _tokens.Add("*");
                    }

                    _vector = _vector + ch;
                    while ((i + 1) < _expr.Length && Char.IsLetter(_expr[i + 1]))
                    {
                        i++;
                        _vector = _vector + _expr[i];
                    }
                    if (_vector != null)
                    {
                        _tokens.Add(_vector);
                        _vector = "";
                    }
                }
                else if (Char.IsDigit(ch))
                {
                    _vector = _vector + ch;
                    while ((i + 1) < _expr.Length && (Char.IsDigit(_expr[i + 1]) || _expr[i + 1] == '.')) // removed || _expr[i + 1] == ','
                    {
                        i++;
                        _vector = _vector + _expr[i];
                    }
                    if (_vector != null)
                    {
                        _tokens.Add(_vector);
                        _vector = "";
                    }
                }
                else if ((i + 1) < _expr.Length && (ch == '-' || ch == '+') && Char.IsDigit(_expr[i + 1])
                         && (i == 0 || this.OperatorList.IndexOf(_expr[i - 1].ToString()) != -1
                             || ((i - 1) > 0 && _expr[i - 1] == '(')))
                {
                    // if the above is true, then, the token for that negative number will be "-1", not "-","1".
                    // to sum up, the above will be true if the minus sign is in front of the number, but
                    // at the beginning, for example, -1+2, or, when it is inside the brakets (-1).
                    // NOTE: this works for + sign as well!
                    _vector = _vector + ch;
                    while ((i + 1) < _expr.Length && (Char.IsDigit(_expr[i + 1]) || _expr[i + 1] == '.')) // removed || _expr[i + 1] == ','
                    {
                        i++;
                        _vector = _vector + _expr[i];
                    }
                    if (_vector != null)
                    {
                        _tokens.Add(_vector);
                        _vector = "";
                    }
                }
                else if (ch == '(')
                {
                    if (i != 0 && (Char.IsDigit(_expr[i - 1]) || Char.IsDigit(_expr[i - 1]) || _expr[i - 1] == ')'))
                    {
                        _tokens.Add("*");
                        _tokens.Add("(");
                    }
                    else
                    {
                        _tokens.Add("(");
                    }
                }
                else
                {
                    _tokens.Add(ch.ToString());
                }
            }

            return this.MathParserLogic(_tokens);
        }

        #endregion
    }
}
