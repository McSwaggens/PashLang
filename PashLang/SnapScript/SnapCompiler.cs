using System;
using System.Collections.Generic;
using System.Linq;
using static SnapScript.Logger;

namespace SnapScript
{
    public class SnapCompiler
    {
		public SnapCompiler()
		{
		}

		public string[] Compile(string code)
		{
			//Print out our input code
			foreach (char c in code) {
				Console.Write(c);
			}

			//Generate tokens

			Token[] tokens = Lexer.GenerateTokens (code);

			Console.WriteLine ($"Token generation returned {tokens.Length} tokens.");

			foreach (Token token in tokens) {
				Console.WriteLine (token.GetDefinitionString());
			}

			//Execute parser with the generated tokens

			///Maybe return a SnapResult object with the results of the compilation, including; PASM Code, PASM .h header file, Errors, Warnings, etc...

			return new string[]{};

		}
    }
}