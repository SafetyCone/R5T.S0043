using System;
using System.Threading.Tasks;


namespace R5T.S0043
{
    class Program
    {
        static async Task Main()
        {
            await Program.CreateInstancesFile();
            //Program.CreateProgramFile();

            //await Instances.RepositoryOperations.CreateNew_EmptyRepository_NonIdempotent();
            //await Instances.RepositoryOperations.Delete_Idempotent();
            //await Instances.RepositoryOperations.CreateNew_LibraryOnlyRepository();
            //await Instances.RepositoryOperations.CreateNew_ConsoleRepository();

            //Instances.SolutionOperations.AddNew_LibraryProjectToSolution();
            //Instances.SolutionOperations.Add_DependencyProjectReferenceToSolution();

            //await GitHubOperations.SubMain();

            /// Tests.
            //await Instances.RepositoryTestOperations.TryCreateScriptRepository();
            //await Instances.RepositoryTestOperations.TryCreate_NewLibraryOnlyRepository();

            //Instances.SolutionTestOperations.TryAddProject();
        }

#pragma warning disable IDE0051 // Remove unused private members

        static Task CreateInstancesFile()
        {
            /// Inputs.
            var projectFilePath = @"C:\Code\DEV\Git\GitHub\davidcoats\D8S.W0002.Private\source\D8S.W0002\D8S.W0002.csproj";

            /// Run.
            var instancesFilePath = Instances.ProjectPathsOperator.GetInstancesFilePath(projectFilePath);
            var namespaceName = Instances.ProjectNamespacesOperator.GetDefaultNamespaceName(projectFilePath);

            Instances.CodeFileGenerator.CreateInstancesFile(
                instancesFilePath,
                namespaceName);

            return Task.CompletedTask;
        }

        static Task CreateProgramFile()
        {
            /// Inputs.
            var projectFilePath = @"";

            /// Run.
            var programFilePath = Instances.ProjectPathsOperator.GetProgramFilePath(projectFilePath);
            var namespaceName = Instances.ProjectNamespacesOperator.GetDefaultNamespaceName(projectFilePath);

            Instances.CodeFileGenerator.CreateProgramFile(
                programFilePath,
                namespaceName);

            return Task.CompletedTask;
        }
    }
}
