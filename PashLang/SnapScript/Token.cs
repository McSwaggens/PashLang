using System;

namespace SnapScript
{
	public class Token
	{
		//Base token to be extended into other classes, this acts as a base


		public override string ToString ()
		{
			string ret = $"[{this.GetType()}] ";
			if (this is Operator)
				ret += ((Operator)this).type;
			else if (this is Word)
				ret += ((Word)this).raw;
			else if (this is PreCompleNotation)
				ret += ((PreCompleNotation)this).Notation;
			else if (this is String)
				ret += ((String)this).raw;
			else if (this is Character)
				ret += ((Character)this).raw;
			return ret;
		}
	}
}

