using System;


namespace R5T.S0043
{
    [Obsolete("See R5T.Z0036.ICommitMessages.")]
    public class CommitMessages : ICommitMessages
	{
		#region Infrastructure

	    public static CommitMessages Instance { get; } = new();

	    private CommitMessages()
	    {
        }

	    #endregion
	}
}