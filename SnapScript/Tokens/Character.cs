using System;

namespace SnapScript
{
	public class Character : Token
	{
		public Character (string c)
		{
			raw = c [0];
		}
		
		public override string ToString ()
		{
			return "" + raw;
		}
	}
}

