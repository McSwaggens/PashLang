﻿using System;

namespace SnapScript
{
	public class String : Token
	{
		public String (string raw)
		{
			this.raw = raw;
		}
		
		public override string ToString ()
		{
			return "" + raw;
		}
	}
}

