using System;

using R5T.F0000;
using R5T.F0002;
using R5T.F0020;
using R5T.F0024;
using R5T.Z0000;


namespace R5T.S0043
{
    public static class Instances
    {
        public static ICharacters Characters { get; } = Z0000.Characters.Instance;
        public static ICodeFileGenerator CodeFileGenerator { get; } = S0043.CodeFileGenerator.Instance;
        public static IDirectoryNameOperator DirectoryNameOperator { get; } = S0043.DirectoryNameOperator.Instance;
        public static IDirectoryNames DirectoryNames { get; } = S0043.DirectoryNames.Instance;
        public static IDirectoryPathOperator DirectoryPathOperator { get; } = S0043.DirectoryPathOperator.Instance;
        public static IDirectoryPaths DirectoryPaths { get; } = S0043.DirectoryPaths.Instance;
        public static IEnumerationHelper EnumerationHelper { get; } = F0002.EnumerationHelper.Instance;
        public static F0002.IExecutablePathOperator ExecutablePathOperator { get; } = F0002.ExecutablePathOperator.Instance;
        public static IFileExtensionOperator FileExtensionOperator { get; } = F0000.FileExtensionOperator.Instance;
        public static IFileExtensions FileExtensions { get; } = S0043.FileExtensions.Instance;
        public static IFileNames FileNames { get; } = S0043.FileNames.Instance;
        public static IFileNameOperator FileNameOperator { get; } = S0043.FileNameOperator.Instance;
        public static F0000.IFileNameOperator FileNameOperator_Base { get; } = F0000.FileNameOperator.Instance;
        public static IFilePathOperator FilePathOperator { get; } = S0043.FilePathOperator.Instance;
        public static IFilePaths FilePaths { get; } = S0043.FilePaths.Instance;
        public static IFileSystemOperator FileSystemOperator { get; } = F0000.FileSystemOperator.Instance;
        public static IGitHubLicenseIdentifiers GitHubLicenseIdentifiers { get; } = S0043.GitHubLicenseIdentifiers.Instance;
        public static IGitHubOperator GitHubOperator { get; } = S0043.GitHubOperator.Instance;
        public static IGitHubOwners GitHubOwners { get; } = S0043.GitHubOwners.Instance;
        public static IGitOperator GitOperator { get; } = S0043.GitOperator.Instance;
        public static IJsonOperator JsonOperator { get; } = S0043.JsonOperator.Instance;
        public static ILibraryOperator LibraryOperator { get; } = S0043.LibraryOperator.Instance;
        public static ILocalRepositoryOperator LocalRepositoryOperator { get; } = S0043.LocalRepositoryOperator.Instance;
        public static INameOperator NameOperator { get; } = S0043.NameOperator.Instance;
        public static IPathOperator PathOperator { get; } = F0002.PathOperator.Instance;
        public static IProjectFileGenerator ProjectFileGenerator { get; } = F0020.ProjectFileGenerator.Instance;
        public static IProjectFileOperator ProjectFileOperator { get; } = F0020.ProjectFileOperator.Instance;
        public static IProjectNameOperator ProjectNameOperator { get; } = S0043.ProjectNameOperator.Instance;
        public static IProjectNamespacesOperator ProjectNamespacesOperator { get; } = S0043.ProjectNamespacesOperator.Instance;
        public static IProjectOperator ProjectOperator { get; } = S0043.ProjectOperator.Instance;
        public static IProjectPathsOperator ProjectPathsOperator { get; } = S0043.ProjectPathsOperator.Instance;
        public static IRemoteRepositoryOperator RemoteRepositoryOperator { get; } = S0043.RemoteRepositoryOperator.Instance;
        public static IRepositoryDescriptions RepositoryDescriptions { get; } = S0043.RepositoryDescriptions.Instance;
        public static IRepositoryNameOperator RepositoryNameOperator { get; } = S0043.RepositoryNameOperator.Instance;
        public static IRepositoryNames RepositoryNames { get; } = S0043.RepositoryNames.Instance;
        public static IRepositoryOperations RepositoryOperations { get; } = S0043.RepositoryGeneration.Instance;
        public static IRepositoryOperator RepositoryOperator { get; } = S0043.RepositoryOperations.Instance;
        public static IRepositoryPathsOperator RepositoryPathsOperator { get; } = S0043.RepositoryPathsOperator.Instance;
        public static IRepositoryTestOperations RepositoryTestOperations { get; } = S0043.RepositoryTestOperations.Instance;
        public static IServicesOperator ServicesOperator { get; } = S0043.ServicesOperator.Instance;
        public static ISolutionFileGenerator SolutionFileGenerator { get; } = F0024.SolutionFileGenerator.Instance;
        public static ISolutionFileOperator SolutionFileOperator { get; } = S0043.SolutionFileOperator.Instance;
        public static ISolutionFolderName SolutionFolderName { get; } = S0043.SolutionFolderName.Instance;
        public static ISolutionNameOperator SolutionNameOperator { get; } = S0043.SolutionNameOperator.Instance;
        public static ISolutionOperations SolutionOperations { get; } = S0043.SolutionOperations.Instance;
        public static ISolutionOperator SolutionOperator { get; } = S0043.SolutionOperator.Instance;
        public static ISolutionPathsOperator SolutionPathsOperator { get; } = S0043.SolutionPathsOperator.Instance;
        public static ISolutionTestOperations SolutionTestOperations { get; } = S0043.SolutionTestOperations.Instance;
        public static IStreamReaderOperator StreamReaderOperator { get; } = F0002.StreamReaderOperator.Instance;
        public static ITemplateFilePaths TemplateFilePaths { get; } = S0043.TemplateFilePaths.Instance;
        public static ITextFileGenerator TextFileGenerator { get; } = S0043.TextFileGenerator.Instance;
    }
}
