using System;

using R5T.D0082.T000;

using R5T.T0142;

using Instances = R5T.S0043.Instances;


namespace System
{
    [DraftTypeMarker]
    public static class GitHubRepositoryLicenseExtensions
    {
        public static string GetLicenseIdentifier(this GitHubRepositoryLicense gitHubRepositoryLicense)
        {
            return gitHubRepositoryLicense switch
            {
                GitHubRepositoryLicense.MIT => Instances.GitHubLicenseIdentifiers.MIT,
                _ => throw Instances.EnumerationHelper.GetSwitchDefaultCaseException(gitHubRepositoryLicense),
            };
        }
    }
}
