using System;


namespace R5T.S0043
{
    public static class Instances
    {
        public static Z0000.ICharacters Characters => Z0000.Characters.Instance;
        public static Z0036.ICommitMessages CommitMessages => Z0036.CommitMessages.Instance;
        public static F0053.ICodeFileGenerator CodeFileGenerator => F0053.CodeFileGenerator.Instance;
        public static F0057.IRepositoryDirectoryNameOperator RepositoryDirectoryNameOperator => F0057.RepositoryDirectoryNameOperator.Instance;
        public static IDirectoryNames DirectoryNames => S0043.DirectoryNames.Instance;
        public static F0057.IRepositoryDirectoryPathOperator RepositoryDirectoryPathOperator => F0057.RepositoryDirectoryPathOperator.Instance;
        public static F0042.IDirectoryPaths DirectoryPaths => F0042.DirectoryPaths.Instance;
        public static F0002.IEnumerationHelper EnumerationHelper => F0002.EnumerationHelper.Instance;
        public static F0002.IExecutablePathOperator ExecutablePathOperator => F0002.ExecutablePathOperator.Instance;
        public static F0000.IFileExtensionOperator FileExtensionOperator => F0000.FileExtensionOperator.Instance;
        public static IFileExtensions FileExtensions => S0043.FileExtensions.Instance;
        public static IFileNames FileNames => S0043.FileNames.Instance;
        public static IFileNameOperator FileNameOperator => S0043.FileNameOperator.Instance;
        public static F0000.IFileNameOperator FileNameOperator_Base => F0000.FileNameOperator.Instance;
        public static F0002.IFilePathOperator FilePathOperator => F0002.FilePathOperator.Instance;
        public static IFilePaths FilePaths => S0043.FilePaths.Instance;
        public static IFileSystemOperator FileSystemOperator => S0043.FileSystemOperator.Instance;
        public static IGitHubLicenseIdentifiers GitHubLicenseIdentifiers => S0043.GitHubLicenseIdentifiers.Instance;
        public static IGitHubOperator GitHubOperator => S0043.GitHubOperator.Instance;
        public static F0041.IGitHubOperator GitHubOperator_Base => F0041.GitHubOperator.Instance;
        public static F0047.IGitHubOwners GitHubOwners => F0047.GitHubOwners.Instance;
        public static IGitOperator GitOperator => S0043.GitOperator.Instance;
        public static F0032.IJsonOperator JsonOperator => F0032.JsonOperator.Instance;
        public static F0043.ILibraryDescriptionOperator LibraryDescriptionOperator => F0043.LibraryDescriptionOperator.Instance;
        public static F0043.ILibraryNameOperator LibraryNameOperator => F0043.LibraryNameOperator.Instance;
        public static ILicenseIdentifiers LicenseIdentifiers => S0043.LicenseIdentifiers.Instance;
        public static F0060.ILocalRepositoryOperator LocalRepositoryOperator => F0060.LocalRepositoryOperator.Instance;
        public static F0044.INameOperator NameOperator => F0044.NameOperator.Instance;
        public static F0061.IOperations Operations => F0061.Operations.Instance;
        public static Z0013.IOrganizationNamespaceTokens OrganizationNamespaceTokens => Z0013.OrganizationNamespaceTokens.Instance;
        public static F0002.IPathOperator PathOperator => F0002.PathOperator.Instance;
        public static F0052.IProjectDirectoryNameOperator ProjectDirectoryNameOperator => F0052.ProjectDirectoryNameOperator.Instance;
        public static IProjectFileGenerator ProjectFileGenerator => S0043.ProjectFileGenerator.Instance;
        public static IProjectFileOperator ProjectFileOperator => S0043.ProjectFileOperator.Instance;
        public static IProjectNameOperator ProjectNameOperator => S0043.ProjectNameOperator.Instance;
        public static F0040.IProjectNamespacesOperator ProjectNamespacesOperator => F0040.ProjectNamespacesOperator.Instance;
        public static IProjectOperations ProjectOperations => S0043.ProjectOperations.Instance;
        public static F0051.IProjectOperator ProjectOperator => F0051.ProjectOperator.Instance;
        public static IProjectPathsOperator ProjectPathsOperator => S0043.ProjectPathsOperator.Instance;
        public static IProjectFileTypeStrings ProjectFileTypeStrings => S0043.ProjectFileTypeStrings.Instance;
        public static T0146.IReasonOperator ReasonOperator => T0146.ReasonOperator.Instance;
        public static IRemoteRepositoryOperator RemoteRepositoryOperator => S0043.RemoteRepositoryOperator.Instance;
        public static F0046.IRepositoryDescriptionOperator RepositoryDescriptionOperator => F0046.RepositoryDescriptionOperator.Instance;
        public static Z0014.IRepositoryDescriptions RepositoryDescriptions => Z0014.RepositoryDescriptions.Instance;
        public static F0046.IRepositoryNameOperator RepositoryNameOperator => F0046.RepositoryNameOperator.Instance;
        public static Z0014.IRepositoryNames RepositoryNames => Z0014.RepositoryNames.Instance;
        public static IRepositoryOperations RepositoryOperations => S0043.RepositoryGeneration.Instance;
        public static F0060.IRepositoryOperator RepositoryOperator => F0060.RepositoryOperator.Instance;
        public static F0042.IRepositoryPathsOperator RepositoryPathsOperator => F0042.RepositoryPathsOperator.Instance;
        public static IRepositoryTestOperations RepositoryTestOperations => S0043.RepositoryTestOperations.Instance;
        public static T0146.IResultOperator ResultOperator => T0146.ResultOperator.Instance;
        public static IServicesOperator ServicesOperator => S0043.ServicesOperator.Instance;
        public static F0024.ISolutionFileGenerator SolutionFileGenerator => F0024.SolutionFileGenerator.Instance;
        public static ISolutionFileOperator SolutionFileOperator => S0043.SolutionFileOperator.Instance;
        public static ISolutionFolderName SolutionFolderName => S0043.SolutionFolderName.Instance;
        public static F0048.ISolutionNameOperator SolutionNameOperator => F0048.SolutionNameOperator.Instance;
        public static ISolutionOperations SolutionOperations => S0043.SolutionOperations.Instance;
        public static ISolutionOperator SolutionOperator => S0043.SolutionOperator.Instance;
        public static ISolutionPathsOperator SolutionPathsOperator => S0043.SolutionPathsOperator.Instance;
        public static ISolutionTestOperations SolutionTestOperations => S0043.SolutionTestOperations.Instance;
        public static F0000.IStreamReaderOperator StreamReaderOperator => F0000.StreamReaderOperator.Instance;
        public static IStrings Strings => S0043.Strings.Instance;
        public static ITextFileGenerator TextFileGenerator => S0043.TextFileGenerator.Instance;
        public static F0042.ITemplateFilePaths TemplateFilePaths => F0042.TemplateFilePaths.Instance;
    }
}
