using System;
using System.Collections.Generic;
namespace SnapScript
{
	public enum OperatorType
	{
		Arrow, Plus, Dash, Star, ForwardSlash, BackSlash, Comment, Equals, Comparison, ReverseComparison, SelfAddition, SelfNegate,

		Comparison_BiggerThan, Comparison_BiggerOrEqualTo,
		Comparison_SmallerThan, Comparison_SmallerOrEqualTo,

		OpeningBracket, ClosingBracket, OpeningBlockBracket, ClosingBlockBracket, OpeningSquareBracket, ClosingSquareBracket, OpeningVBracket, ClosingVBracket,

		Dot,
		PreCompileNotation, SemiColon, Colon
	}

	public class OperatorGenerator
	{
		public static Dictionary<string, OperatorType> DeclaredOperators = new Dictionary<string, OperatorType> () {
			{ "->", OperatorType.Arrow },
			{ "+",  OperatorType.Plus },
			{ "-",  OperatorType.Dash },
			{ "*",  OperatorType.Star },
			{ "/",  OperatorType.ForwardSlash },
			{ "\\", OperatorType.BackSlash },
			{ "//", OperatorType.Comment },
			{ "=",  OperatorType.Equals },
			{ "==", OperatorType.Comparison },
			{ "!=", OperatorType.ReverseComparison },
			{ "+=", OperatorType.SelfAddition },
			{ "-=", OperatorType.SelfNegate },
			{ ">",  OperatorType.Comparison_BiggerThan },
			{ ">=", OperatorType.Comparison_BiggerOrEqualTo },
			{ "<",  OperatorType.Comparison_SmallerThan },
			{ "<=", OperatorType.Comparison_SmallerOrEqualTo },
			{ "(", OperatorType.OpeningBracket },
			{ ")", OperatorType.ClosingBracket },
			{ "{", OperatorType.OpeningBlockBracket },
			{ "}", OperatorType.ClosingBlockBracket },
			{ "[", OperatorType.OpeningSquareBracket },
			{ "]", OperatorType.ClosingSquareBracket },
			{ ".", OperatorType.Dot },
			{ "#", OperatorType.PreCompileNotation },
			{ ";", OperatorType.SemiColon },
			{ ":", OperatorType.Colon },
		};

		public static OperatorType[] GetOperators(string text) {
			List<OperatorType> operators = new List<OperatorType> ();
			while (text != "") {
				for (int i = 0; i < text.Length; i++) {
					string cut = Sub (text.ToCharArray(), i, text.Length);
					OperatorType type;
					if (GetDeclaredType(cut, out type)) {
						text = Sub (text.ToCharArray (), 0, text.Length-cut.Length);
						operators.Add(type);
					}
				}
			}
			return operators.ToArray ();
		}

		private static bool GetDeclaredType(string gen, out OperatorType type) {
			type = OperatorType.Arrow;
			foreach (KeyValuePair<string, OperatorType> pair in DeclaredOperators) {
				if (pair.Key == gen) {
					type = pair.Value;
					return true;
				}
			}
			return false;
		}

		private static string Sub(char[] chars, int From, int To) {
			string current = "";
			for (int i = From; i < To; i++) {
				current += chars [i];
			}
			return current;
		}
	}
}

