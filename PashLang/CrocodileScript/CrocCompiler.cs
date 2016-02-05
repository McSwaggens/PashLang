using System;
using System.Collections.Generic;

namespace CrocodileScript
{
	public class CrocCompiler
	{

		public CrocResult CResult = new CrocResult();
		public string StartFunction = null;

		public List<string> CompiledCode = new List<string>();
		public List<string> Imports = new List<string> ();

        //FunctionSpace
        public List<Function> Functions = new List<Function>();
        public List<Variable> Variables = new List<Variable>();

		/*
		 *  CROCODILE COMPILER
		 */

		/*
		 * 
		 * The python lives in an aquatic habitat,
		 * and when swimming it is vulnerable to crocodiles.
		 * While younger specimens can be captured and eaten by a crocodile of any size,
		 * the older a python gets, the larger it becomes.
		 * A large python can be captured and eaten only by a large crocodile.
		 * 
		 */

		public string Code;

		public CrocCompiler (string[] Code)
		{
			this.Code = BatterCode (Code);
		}

		public CrocResult Compile()
		{
			CResult.BatteredCode = Code;
			List<Line> InitialLines = BuildStructure (Code);
			isInitial = false;
			InitialLines.ForEach(ln => ln.PreCompile());
			CResult.PASM = CompiledCode.ToArray ();
			return CResult;
		}

		public string BatterCode(string[] code) {
			string build = "";
			foreach (string pt in code) {
				build += pt;
			}
			return build;
		}

		bool isInitial = true;

		public List<Line> BuildStructure(string code) {
			char[] CArr = code.ToCharArray ();
			bool inQ = false;
			string Current = "";
			QuoteType quoteType = QuoteType.Double;

			List<Line> Lines = new List<Line> ();

			for (int i = 0; i < CArr.Length; i++) {
				char c = CArr [i];

				if (c == '"') {
					if (inQ) {
						if (quoteType == QuoteType.Double)
							inQ = false;
					} else {
						inQ = true;
						quoteType = QuoteType.Double;
					}
				} else if (c == '\'') {
					if (inQ) {
						if (quoteType == QuoteType.Single)
							inQ = false;
					} else {
						inQ = true;
						quoteType = QuoteType.Single;
					}
				}

				if (!inQ) {
					if (c == '{') {
						bool _inQ = false;
						Block block = new Block ();

						string blockBuild = "";
						int BracketCount = 1;
						while (true) {
							i++;
							if (i > CArr.Length)
								throw new Exception ("Expected end of block, got end of script..."); //TODO: Add custom error reporting and throwing...
							c = CArr [i];

							if (c == '"') {
								_inQ = !_inQ;
							}

							if (!_inQ) {
								if (c == '{')
									BracketCount++;
								else if (c == '}') {
									BracketCount--;
									if (BracketCount == 0) {
										//Reached the end of the block...
										break;
									}
								}
								/*
								 * Don't want to add a ; check, just incase the block is something like an array.. [ ]
								 */
							}
							blockBuild += c;
						}
						block.Lines = BuildStructure (blockBuild);
						Lines.Add(new Line (Current, block, this));
					}
					if (c == ';') {
						Lines.Add (new Line (Current, this));
						Current = "";
					} else {
						Current += c;
					}
				} else {
					Current += c;
				}
			}
			return Lines;
		}


        ///------------------------------------------------------------------------
        private enum BODMAS
        {
            BRACKETS, OTHER, DEVIDE, MULTIPLY, ADDITION, SUBTRACTION
        }

        private enum PEMDAS
        {
            PARENTHESIS, EXPONENTS, MULTIPLICATION_DIVISION, ADDITION_SUBTRACTION
        }

        private char[] GetOperatorsFromBODMAS(BODMAS t)
        {
            switch (t)
            {
                case BODMAS.OTHER: return new char[] { '^' };
                case BODMAS.DEVIDE: return new char[] { '/', '%' };
                case BODMAS.MULTIPLY: return new char[] { '*' };
                case BODMAS.ADDITION: return new char[] { '+' };
                case BODMAS.SUBTRACTION: return new char[] { '-' };
            }
            return null;
        }

        private char[] GetOperatorsFromPEMDAS(PEMDAS t)
        {
            switch (t)
            {
                case PEMDAS.EXPONENTS: return new char[] { '^' };
                case PEMDAS.MULTIPLICATION_DIVISION: return new char[] { '/', '%', '*' };
                case PEMDAS.ADDITION_SUBTRACTION: return new char[] { '+', '-' };
            }
            return null;
        }

        public void Calculate(string calculation)
        {

            Section sect = SeperateCalculation(calculation);
            Variable v = GenerateExpressionFromParts_ALGEBRA(sect);
        }
        ////Algebra BODMAS passovers
        ///Brackets Orders Division Multiplication Addition Subtraction

        Variable GenerateExpressionFromParts_ALGEBRA (Section sect)
        {
            Variable returner = new Variable(null);
            Variable nextVar = new Variable(null);
            //Do the calculation is BODMAS order

            foreach (PEMDAS t in Enum.GetValues(typeof(PEMDAS)))
            {
                char[] arr = GetOperatorsFromPEMDAS(t);
                List<char> operators = null;
                if (arr != null)
                    operators = new List<char>(arr);
                sect.Parts.RemoveAll(p => p == null);
                for (int i = 0; i < sect.Parts.Count; i++)
                {
                    sect.Parts.RemoveAll(p => p == null);
                    Part current = sect.Parts[i];
                    if (current == null) continue;
                    if (t == PEMDAS.PARENTHESIS)
                    {
                        if (current is Section)
                        {
                            Variable vr = GenerateExpressionFromParts_ALGEBRA((Section)current);
                            PointValue pv = new PointValue(vr.ID, current.Operator);
                            sect.Parts[i] = pv;
                            i--;
                        }
                        continue;
                    }
                    if (!operators.Contains(current.Operator)) continue;

                    Part next = null;
                    
                    i++;
                    next = sect.Parts[i];
                    bool nextPt = false;
                    if (next is Value)
                    {
                        CompiledCode.Add("st " + nextVar.ID + " " + GetTypeASM(GetVariableTypeFromRaw(((Value)next).Rep)) + " " + ((Value)next).Rep);
                    }
                    else if (next is PointValue) nextPt = true;

                    Variable currentVar = null;
                    currentVar = new Variable(null);
                    bool currentPt = false;
                    if (current is Value)
                    {
                        currentVar = new Variable(null);
                        CompiledCode.Add("st " + currentVar.ID + " " + GetTypeASM(GetVariableTypeFromRaw(((Value)current).Rep)) + " " + ((Value)current).Rep);
                    }
                    else if (current is PointValue) currentPt = true;

                    CompiledCode.Add("st " + currentVar.ID + " QMATH " + (currentPt ? ((PointValue)current).pt : currentVar.ID) + current.Operator + (nextPt ? ((PointValue)next).pt : nextVar.ID));
                    sect.Parts[i] = null;
                    sect.Parts[i - 1] = new PointValue(currentVar.ID, next.Operator);
                    i -= 2;
                    returner = currentVar;
                }
            }

            return returner;
        }

        #region NON ALGEBRA
        Variable GenerateExpressionFromParts (Section sect)
        {
            char workingOperator = '\0';
            Variable workingVariable = null;//Total
            Variable setVariable = new Variable(null);

            foreach (Part pt in sect.Parts)
            {
                if (pt is Section)
                {
                    Variable vr = GenerateExpressionFromParts_ALGEBRA(((Section)pt));

                    if (workingVariable == null)
                        workingVariable = vr;
                    else
                    {
                        if (isTypeInteger(workingVariable.type) && isTypeInteger(vr.type))
                        {
                            CompiledCode.Add("st " + workingVariable.Tag + " QMATH " + workingVariable.Tag + workingOperator + vr.Tag);
                        }
                    }
                    workingOperator = pt.Operator;
                }
                else
                if (pt is Value)
                {
                    Value val = (Value)pt;
                    VariableType type = GetVariableTypeFromRaw(val.Rep);
                    setVariable.type = type;
                    if (type == VariableType.INT32)
                    {
                        if (workingVariable != null)
                        {
                            CompiledCode.Add("st " + setVariable.Tag + " INT " + Excerpt(val.Rep, type));
                            CompiledCode.Add("st " + workingVariable.Tag + " QMATH " + workingVariable.Tag + workingOperator + setVariable.Tag);
                        }
                        else
                        {
                            workingVariable = new Variable(null);
                            CompiledCode.Add("st " + workingVariable.Tag + " INT " + val.Rep);
                        }
                        workingVariable.type = type;
                    }
                    workingOperator = val.Operator;
                }

            }
            return workingVariable;
        }

        #endregion

        object Excerpt(string rep, VariableType t)
        {
            switch (t)
            {
                case VariableType.BYTE: return byte.Parse(rep);
                case VariableType.INT16: return short.Parse(rep);
                case VariableType.INT32: return int.Parse(rep);
                case VariableType.INT64: return long.Parse(rep);
            }
            throw new Exception("Unable to convert " + rep);
        }


        //We will make a new version of this when we can do char[] arrays.
        //object Excerpt(string rep, VariableType t)
        //{
        //    switch (t)
        //    {
        //        case VariableType.INT: return int.Parse(rep);
        //        case VariableType.STRING: return rep.TrimStart('\"').TrimEnd('\"');
        //    }
        //    throw new Exception("Unable to convert " + rep);
        //}

        private Variable GetVariableByID(int id)
        {
            foreach (Variable var in Variables) if (var.ID == id) return var;
            return null;
        }

        private VariableType GetVariableTypeFromRaw(string rep)
        {
            char[] carr = rep.ToCharArray();
            if (isNumber(carr[0]) && isNumber(carr[0])) return VariableType.INT32;
            if (carr[0] == '"' && carr[carr.Length - 1] == '"') return VariableType.ARRAY;
            if (rep == "true" || rep == "false") return VariableType.BOOL;
            //Check if the value is an already defined variable or method...
            foreach (Variable vr in Variables)
            {
                if (vr.Name == rep) return vr.type;
            }
            throw new SyntaxException("Unknown Variable or Variable Type: " + rep, SyntaxError.SyntaxError);
        }

        private string GetTypeASM(VariableType type)
        {
            switch (type)
            {
                case VariableType.BYTE: return "BYTE";
                case VariableType.INT16: return "INT16";
                case VariableType.INT32: return "INT32";
                case VariableType.INT64: return "INT64";
                case VariableType.BOOL: return "BOOL";
                default: throw new Exception("Cannot convert type " + type.ToString());
            }
        }

	    bool isTypeInteger(VariableType type) => (type == VariableType.BYTE || type == VariableType.INT16 || type == VariableType.INT32 || type == VariableType.INT64);

        bool isNumber(char c) => char.IsNumber(c);

        private Section SeperateCalculation(string calc)
        {
            char[] arr = calc.ToCharArray();
            char lastChar = '\0';
            bool inQ = false;
            string S_Current = "";
            Section Current = new Section();
            Current.Parent = Current;
            for (int i = 0; i < arr.Length; i++)
            {
                bool wasLastOperator = isOperator(lastChar);
                char c = arr[i];
                if (c == '\"') inQ = !inQ;
                else if (!inQ)
                {
                    if (c == '(')
                    {
                        if (wasLastOperator)
                        {
                            Section sect = new Section();
                            sect.Parent = Current;
                            Current.Parts.Add(sect);
                            Current = sect;
                            S_Current = "";
                        }
                    }
                    else if (c == ')')
                    {
                        if (S_Current != "")
                        {
                            Value val = new Value(S_Current);
                            Current.Parts.Add(val);
                        }
                        Current = Current.Parent;
                        S_Current = "";
                    }
                    else if (isOperator(c))
                    {
                        if (S_Current == "" && Current.Parts[Current.Parts.Count-1] is Section)
                        {
                            Section sect = ((Section)Current.Parts[Current.Parts.Count-1]);
                            sect.Operator = c;
                            wasLastOperator = true;
                        }
                        else
                        {
                            Value val = new Value(S_Current, c);
                            Current.Parts.Add(val);
                            S_Current = "";
                            wasLastOperator = true;
                        }
                    }
                    else
                    {
                        wasLastOperator = false;
                        if (c != ' ') S_Current += c;
                    }
                    
                    //TODO: Math operators
                }
                if (c != ' ')
                    lastChar = c;
            }
            if (S_Current != "")
            {
                Value value = new Value(S_Current);
                Current.Parts.Add(value);
            }
            return Current;
        }

        private class Part
        {
            public char Operator = '\0';
        }
        private class Section : Part
        {
            public Section Parent;
            public List<Part> Parts = new List<Part>();
            public override string ToString() => "Section";
        }
        private class Value : Part
        {
            public string Rep;
            public Value(string g)
            {
                Rep = g;
            }
            public Value(string g, char c)
            {
                Rep = g;
                Operator = c;
            }

            public override string ToString()
            {
                return Rep + Operator;
            }
        }

        private class PointValue : Part
        {
            public int pt;
            public PointValue(int point, char c)
            {
                pt = point;
                Operator = c;
            }
        }
        

        ///-------------------------------------------------------------------------

        public Function ResolveFunctionFromName(string Name)
        {
            foreach (Function f in Functions) if (f.Name + "" == Name) return f; return null;
        }

        public Function ResolveFunctionFromID(string ID)
        {
            foreach (Function f in Functions) if (f.ID + "" == ID) return f; return null;
        }

        public static VariableType ResolveVariableType(string t)
        {
            switch (t)
            {
                case "byte": return VariableType.BYTE;
                case "int16": return VariableType.INT16;
                case "int32": return VariableType.INT32;
                case "int64": return VariableType.INT64;
                case "bool": return VariableType.BOOL;
                case "void": return VariableType.VOID;
            }
            throw new SyntaxException("Unknown Type " + t, SyntaxError.SyntaxError);
        }

        private static List<char> MathOperators = new List<char>()
        {
            '+', '-', '/', '*', '%'
        };

        private static bool isOperator(char c)
        {
            foreach (char g in MathOperators) if (c == g) return true;
            return false;
        }

        public enum QuoteType {
			Single, Double
		}
        
	}
}

