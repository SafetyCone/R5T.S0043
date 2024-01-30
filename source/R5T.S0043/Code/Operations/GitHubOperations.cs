using System;
using System.IO;
using System.Threading.Tasks;

using R5T.F0041;


namespace R5T.S0043
{
    public static class GitHubOperations
    {
        public static async Task SubMain()
        {
            await GitHubOperations.DeleteLocalAndRemoteRepository();
            //await GitHubOperations.CreateAndCloneRepository();
            //await GitHubOperations.DeleteRepository();
            //await GitHubOperations.CreateRepository();
            //await GitHubOperations.IsRepositoryPrivate();
            //await GitHubOperations.DoesRepositoryExist();
        }

#pragma warning disable IDE0051 // Remove unused private members

        private static async Task DeleteLocalAndRemoteRepository()
        {
            var repositoryName = "Test123";

            var repositoryDirectoryPath = Instances.PathOperator.Get_DirectoryPath(
                Instances.DirectoryPaths.SafetyConeRepositoriesDirectory,
                repositoryName);
            
            // Delete local.
            Instances.FileSystemOperator.Delete_Directory_OkIfNotExists(repositoryDirectoryPath);

            // Delete remote.
            await Instances.GitHubOperator.DeleteRepository(
                Instances.GitHubOwners.SafetyCone,
                repositoryName);
        }

        private static async Task CreateAndCloneRepository()
        {
            var repositoryName = "Test123";
            var description = "A test repository";

            var repositorySpecification = Instances.RepositoryOperator.Get_RepositorySpecification(
                Instances.GitHubOwners.SafetyCone,
                repositoryName,
                description,
                true);

            await Instances.GitHubOperator.CreateRepository_NonIdempotent(repositorySpecification);

            await Instances.GitOperator.Clone_NonIdempotent(repositoryName);
        }

        private static async Task DeleteRepository()
        {
            var repositoryName = "Test123";

            await Instances.GitHubOperator.DeleteRepository_NonIdempotent(
                Instances.GitHubOwners.SafetyCone,
                repositoryName);
        }

        private static async Task CreateRepository()
        {
            var repositoryName = "Test123";
            var description = "A test repository";

            var repositorySpecification = new GitHubRepositorySpecification
            {
                Organization = Instances.GitHubOwners.SafetyCone,
                Name = repositoryName,
                Description = description,
                Visibility = GitHubRepositoryVisibility.Private,
                InitializeWithReadMe = true,
                License = GitHubRepositoryLicense.MIT,
            };

            await Instances.GitHubOperator.CreateRepository_NonIdempotent(repositorySpecification);
        }

        private static async Task IsRepositoryPrivate()
        {
            var repositoryName = "R5T.S0043";

            var repositoryIsPrivate = await Instances.GitHubOperator.IsPrivate_SafetyCone(repositoryName);

            Console.WriteLine($"Repository '{repositoryName}' is private: {repositoryIsPrivate}");
        }

        private static async Task DoesRepositoryExist()
        {
            var repositoryName = "R5T.S0043";

            var repositoryExists = await Instances.GitHubOperator.RepositoryExists_SafetyCone(repositoryName);

            Console.WriteLine($"Repository '{repositoryName}' exists: {repositoryExists}");
        }
    }
}
