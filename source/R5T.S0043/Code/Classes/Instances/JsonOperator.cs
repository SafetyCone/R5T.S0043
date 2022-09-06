using System;


namespace R5T.S0043
{
	public class JsonOperator : IJsonOperator
	{
		#region Infrastructure

	    public static JsonOperator Instance { get; } = new();

	    private JsonOperator()
	    {
        }

	    #endregion
	}


	namespace Internal
    {
		public class JsonOperator : IJsonOperator
		{
			#region Infrastructure

			public static JsonOperator Instance { get; } = new();

			private JsonOperator()
			{
			}

			#endregion
		}
	}
}