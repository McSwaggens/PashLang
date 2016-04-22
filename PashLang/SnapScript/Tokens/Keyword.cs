using System;

namespace SnapScript
{
	public class Keyword : Token
	{
		public EnumKeyword keyword = EnumKeyword.NO_KEYWORD;

		public Keyword (EnumKeyword keyword)
		{
			this.keyword = keyword;
		}
	}
}

