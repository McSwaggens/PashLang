using System;

namespace SnapScript
{
	public class NumberToken : Token
	{
		public NumberToken (string raw)
		{
			this.raw = long.Parse (raw);
		}

		public override string ToString ()
		{
			return "" + raw;
		}
	}
}

