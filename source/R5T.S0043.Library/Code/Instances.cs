using System;

using R5T.Z0000;

namespace R5T.S0043.Library
{
    public static class Instances
    {
        public static ICharacters Characters { get; } = Z0000.Characters.Instance;
        public static IDirectoryNameOperator DirectoryNameOperator { get; } = Library.DirectoryNameOperator.Instance;
        public static IDirectoryPaths DirectoryPaths { get; } = Library.DirectoryPaths.Instance;
        public static F0002.IExecutablePathOperator ExecutablePathOperator { get; } = F0002.ExecutablePathOperator.Instance;
        public static IGitHubOwnerNames GitHubOwnerNames { get; } = Library.GitHubOwnerNames.Instance;
        public static IGitHubOwnerOperator GitHubOwnerOperator { get; } = Library.GitHubOwnerOperator.Instance;
        public static IGitHubOwners GitHubOwners { get; } = Library.GitHubOwners.Instance;
        public static IOrganizationNamespaceTokens OrganizationNamespaceTokens { get; } = Library.OrganizationNamespaceTokens.Instance;
        public static F0002.IPathOperator PathOperator { get; } = F0002.PathOperator.Instance;
    }
}