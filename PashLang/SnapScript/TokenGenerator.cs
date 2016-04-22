﻿using System;
using System.Collections.Generic;

namespace SnapScript
{
	public class TokenGenerator
	{
		private static List<char> SpecialCharacters = new List<char>("+-!@#$%^&*():;\"',.?/~`\\|=<>{}[]".ToCharArray ());

		public static Token[] GenerateTokens(string code)
		{
			//Generate a list of tokens from the given code

			List<Token> tokens = new List<Token> ();

			char[] characters = code.ToCharArray ();
			string current = "";
			for (int i = 0; i < characters.Length; i++) {
				char c = characters [i];

				//Check for PreCompileNotation
				if (c == '#') {
					i++;
					for (; i < characters.Length; i++) {
						c = characters [i];
						if (!char.IsLetter (c)) {
							PreCompleNotation precomp = new PreCompleNotation (current);
							tokens.Add (precomp);
							current = "";
							i--;
							break;
						} else
							current += c;
					}
				} else if (c == '"') {
					bool ignore = false;
					i++;
					for (; i < characters.Length; i++) {
						c = characters [i];
						if (c == '\\')
							ignore = true;
						else if (c == '"') {
							if (ignore)
								ignore = false;
							else {
								SnapScript.String str = new String (current);
								tokens.Add (str);
								current = "";
								break;
							}
						} else
							current += c;
					}
				}
				else if (c == '\'') {
					bool ignore = false;
					i++;
					for (; i < characters.Length; i++) {
						c = characters [i];
						if (c == '\\')
							ignore = true;
						else if (c == '\'') {
							if (ignore)
								ignore = false;
							else {
								SnapScript.Character str = new Character (current);
								tokens.Add (str);
								current = "";
								break;
							}
						} else
							current += c;
					}
				}
				//Check for operators
				else
				if (SpecialCharacters.Contains (c)) {
					for (; i < characters.Length; i++) {
						c = characters [i];
						if (!SpecialCharacters.Contains (c)) {
							//current += c;
							OperatorType[] types = OperatorGenerator.GetOperators (current);
							Array.Reverse (types);
							foreach (OperatorType type in types)
								tokens.Add (new Operator (type));
							current = "";
							i--;
							break;
						} else
							current += c;
					}
				}
				//Word token generation
				else if (char.IsLetter (c) || c == '_') {
					for (; i < characters.Length; i++) {
						c = characters [i];
						if (!char.IsLetter (c) && c != '_') {
							Word word = new Word (current);
							tokens.Add (word);
							current = "";
							i--;
							break;
						} else
							current += c;
					}
				}
			}

			return tokens.ToArray();
		}
	}
}

