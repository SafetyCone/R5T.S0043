using System;

using R5T.T0132;


namespace R5T.S0043
{
	[DraftFunctionalityMarker]
	public partial interface IRepositoryNameOperator : IFunctionalityMarker
	{
		public string GetOwnedRepositoryName(string ownerName, string repositoryName)
        {
			var output = $"{ownerName}{Instances.Characters.Slash}{repositoryName}";
			return output;
        }

		public string GetRepositoryName_FromLibraryName(string libraryName)
        {
			// Repository name is just the library name.
			var output = libraryName;
			return output;
        }
	}
}