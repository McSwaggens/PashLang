using System;
using System.Collections.Generic;

namespace CrocodileScript
{
	public class CrocResult
	{
		public string[] PASM;
		public string[] HeaderFile;
		public string BatteredCode;

		public List<Warning> Warnings = new List<Warning>();
		public List<Error> Errors = new List<Error>();

		public bool WasSuccessfull {
			get { return Errors.Count == 0; }
		}

		public bool HasWarnings {
			get { return Warnings.Count > 0; }
		}
	}

	public class Warning {
		public string Message;
		public string LineOfCode;
		public string Cause;
	}

	public class Error {
		public string Message;
		public string LineOfCode;
		public string Cause;
	}
}

