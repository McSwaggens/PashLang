using System;

namespace SnapScript
{
	public class Word : Token
	{
		public string raw;

		public Word (string raw)
		{
			this.raw = raw;
		}

		public Token Clasify()
		{
			//Generate new token
			return null;
		}
	}
}

