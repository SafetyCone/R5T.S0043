using System;
using System.Threading.Tasks;


namespace R5T.S0043
{
    class Program
    {
        static async Task Main()
        {
            //await Program.CreateInstancesFile();
            //Program.CreateProgramFile();

            //await Instances.RepositoryOperations.CreateNew_EmptyRepository_NonIdempotent();
            //await Instances.RepositoryOperations.CreateNew_MinimalRepository_NonIdempotent();
            //await Instances.RepositoryOperations.Delete_Idempotent();
            await Instances.RepositoryOperations.CreateNew_LibraryRepository();
            //await Instances.RepositoryOperations.CreateNew_LibraryOnlyRepository();
            //await Instances.RepositoryOperations.CreateNew_ConsoleRepository();

            //Instances.SolutionOperations.AddNew_LibraryProjectToSolution();
            //Instances.SolutionOperations.AddNew_ConsoleProjectToSolution();
            //Instances.SolutionOperations.Add_DependencyProjectReferenceToSolution();
            //Instances.SolutionOperations.CreateNewSolutionFile();
            //Instances.SolutionOperations.UpgradeSolutionFiles();

            //Instances.ProjectOperations.AddPackageProperties();
            //Instances.ProjectOperations.CreateProjectFileOnly();
            //Instances.ProjectOperations.ChangeToNetStandard2_0();
            //Instances.ProjectOperations.ChangeToNetStandard2_1();
            //Instances.ProjectOperations.ChangeToNet6_0();

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
            var projectFilePath = @"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.F0024\source\R5T.F0024.T001\R5T.F0024.T001.csproj";

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
