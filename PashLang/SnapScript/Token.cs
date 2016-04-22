using System;

namespace SnapScript
{
	public class Token
	{
		//Base token to be extended into other classes, this acts as a base

		public object raw;
		
		public override string ToString ()
		{
			string ret = $"[{this.GetType().ToString().Substring(11)}]  \t{ToString()}";
			return ret;
		}
	}
}

