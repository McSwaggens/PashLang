using System;

namespace SnapScript
{
	public class String : Token
	{
		public string raw;
		public String (string raw)
		{
			this.raw = raw;
		}
	}
}

