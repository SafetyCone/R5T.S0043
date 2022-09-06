using System;
using System.Linq;
using System.Threading.Tasks;

using Octokit;

using R5T.Aalborg;

using R5T.D0082.T000;
using R5T.T0132;


namespace R5T.S0043
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// Prior work: R5T.D0082.I001.GitHubOperator
    /// </remarks>
	[DraftFunctionalityMarker]
	public partial interface IGitHubOperator : IDraftFunctionalityMarker
	{
        public async Task<long> CreateRepository_NonIdempotent(GitHubRepositorySpecification repositorySpecification)
        {
            var autoInit = repositorySpecification.InitializeWithReadMe;

            var licenseTemplate = repositorySpecification.License.GetLicenseIdentifier();

            var @private = repositorySpecification.Visibility.IsPrivate();

            var newRepository = new NewRepository(repositorySpecification.Name)
            {
                AutoInit = autoInit,
                Description = repositorySpecification.Description,
                LicenseTemplate = licenseTemplate,
                Private = @private, // Default is public (private = false).
            };

            var gitHubClient = await this.GetGitHubClient();

            var currentUser = await gitHubClient.User.Current();

            var createdRepository = currentUser.Login == repositorySpecification.Organization
                ? await gitHubClient.Repository.Create(newRepository)
                : await gitHubClient.Repository.Create(
                    repositorySpecification.Organization,
                    newRepository)
                ;

            // Wait a few seconds for the repository to be fully created on GitHub's side. This allows any following operations that request the repository to succeed.
            await Task.Delay(3000);

            return createdRepository.Id;
        }

        public async Task<long> CreateRepository(GitHubRepositorySpecification repositorySpecification)
        {
            var repositoryExists = await this.RepositoryExists(
                repositorySpecification.Organization,
                repositorySpecification.Name);

            var repositoryID = repositoryExists
                ? await this.GetRepositoryID(
                    repositorySpecification.Organization,
                    repositorySpecification.Name)
                : await this.CreateRepository_NonIdempotent(repositorySpecification);
                ;

            return repositoryID;
        }

        public async Task DeleteRepository_NonIdempotent(string owner, string name)
        {
            var gitHubClient = await this.GetGitHubClient();

            await gitHubClient.Repository.Delete(owner, name);
        }

        public async Task DeleteRepository(string owner, string name)
        {
            var repositoryExists = await this.RepositoryExists(owner, name);
            if(repositoryExists)
            {
                await this.DeleteRepository_NonIdempotent(owner, name);
            }
        }

        public async Task<T> GetFromRepository<T>(
            string owner,
            string name,
            Func<Repository, Task<T>> repositoryFunction)
        {
            var gitHubClient = await this.GetGitHubClient();

            var repository = await this.GetRepository(gitHubClient,
                owner, name);

            var output = await repositoryFunction(repository);
            return output;
        }

        public Task<Authentication> GetGitHubAuthentication()
        {
            var gettingGitHubAuthentication = Instances.JsonOperator.LoadFromFile<Authentication>(
                 Instances.FilePaths.GitHubAuthenticationFile_Json,
                 "GitHubAuthentication");

            return gettingGitHubAuthentication;
        }

        public Authentication GetGitHubAuthentication_Synchronous()
        {
            var gitHubAuthentication = Instances.JsonOperator.LoadFromFile_Synchronous<Authentication>(
                 Instances.FilePaths.GitHubAuthenticationFile_Json,
                 "GitHubAuthentication");

            return gitHubAuthentication;
        }

        public async Task<GitHubClient> GetGitHubClient()
        {
            var gitHubAuthentication = await this.GetGitHubAuthentication();

            var gitHubClient = this.GetGitHubClient(gitHubAuthentication);
            return gitHubClient;
        }

        public GitHubClient GetGitHubClient_Synchronous()
        {
            var gitHubAuthentication = this.GetGitHubAuthentication_Synchronous();

            var gitHubClient = this.GetGitHubClient(gitHubAuthentication);
            return gitHubClient;
        }

        public GitHubClient GetGitHubClient(Authentication gitHubAuthentication)
        {
            // Get a GitHub client.
            var productHeaderValueValue = "Rivet";
            var productHeaderValue = ProductHeaderValue.Parse(productHeaderValueValue);

            var basicAuthentication = new Credentials(
                gitHubAuthentication.Username,
                gitHubAuthentication.Password);

            var gitHubClient = new GitHubClient(productHeaderValue)
            {
                Credentials = basicAuthentication,
            };

            return gitHubClient;
        }

        public Task<Repository> GetRepository(GitHubClient gitHubClient,
            string owner,
            string name)
        {
            return gitHubClient.Repository.Get(owner, name);
        }

        public async Task<long> GetRepositoryID(string owner, string name)
        {
            var output = await this.GetFromRepository(
                owner,
                name,
                repository => Task.FromResult(repository.Id));

            return output;
        }

        public async Task<string> GetCloneUrl(string owner, string name)
        {
            var output = await this.GetFromRepository(owner, name,
                repository => Task.FromResult(repository.CloneUrl));

            return output;
        }

        public async Task<T> InClientContext<T>(Func<GitHubClient, Task<T>> gitHubClientAction)
        {
            var gitHubClient = this.GetGitHubClient_Synchronous();

            var output = await gitHubClientAction(gitHubClient);
            return output;
        }

        public async Task<bool> IsPrivate(string owner, string name)
        {
            var isPrivate = await this.GetFromRepository(owner, name,
                repository =>
                {
                    var output = repository.Private;

                    return Task.FromResult(output);
                });

            return isPrivate;
        }

        public Task<bool> IsPrivate_SafetyCone(string name)
        {
            return this.IsPrivate(
                Instances.GitHubOwners.SafetyCone,
                name);
        }

        public async Task<bool> RepositoryExists(string owner, string name)
        {
            var output = await this.InClientContext(async gitHubClient =>
            {
                var searchRequest = new SearchRepositoriesRequest(name)
                {
                    User = owner,
                };

                var searchResult = await gitHubClient.Search.SearchRepo(searchRequest);

                var exists = searchResult.Items
                    .Where(x => x.Name == name)
                    .Any();

                return exists;
            });

            return output;
        }

        public Task<bool> RepositoryExists_SafetyCone(string name)
        {
            return this.RepositoryExists(
                Instances.GitHubOwners.SafetyCone,
                name);
        }

        public async Task VerifyRepositoryDoesNotExist(string owner, string name)
        {
            var repositoryExists = await this.RepositoryExists(owner, name);
            if(repositoryExists)
            {
                var ownedRepositoryName = Instances.RepositoryNameOperator.GetOwnedRepositoryName(owner, name);

                throw new Exception($"{ownedRepositoryName}: Repository exists.");
            }
        }
    }
}