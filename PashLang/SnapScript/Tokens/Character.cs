using System;

namespace SnapScript
{
	public class Character : Token
	{
		public char raw;
		public Character (string c)
		{
			raw = c [0];
		}
	}
}

