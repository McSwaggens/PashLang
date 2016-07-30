namespace PASM
{
    /// <summary>
    /// An instance of a function and context
    /// </summary>
    public class FunctionInstance
    {
		
        public bool doesReturnValue = true;

		/// <summary>
		/// The line to return to when re is hit.
		/// </summary>
        public int returnLine;

		/// <summary>
		/// The register to return the data to upon completion.
		/// </summary>
		public int returnVariablePos;

		/// <summary>
		/// Restricted for returns
		/// Flagged if the returning variable is in the method context.
		/// </summary>
		public bool methodVariable = false;

		/// <summary>
		/// The register context for the method.
		/// </summary>
		public Raster register;

        public FunctionInstance(int size)
        {
			register = new Raster(size);
        }
    }
}