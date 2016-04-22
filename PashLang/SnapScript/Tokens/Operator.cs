using System;

namespace SnapScript
{
	public class Operator : Token
	{
		public OperatorType type;
		public Operator (OperatorType type)
		{
			this.type = type;
		}
	}
}

