using System;


namespace R5T.S0043
{
    class Program
    {
        static void Main()
        {
            Program.CreateInstancesFile();
            //Program.CreateProgramFile();
        }

        static void CreateInstancesFile()
        {
            /// Inputs.
            var projectFilePath = @"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.F0024\source\R5T.F0024.Construction\R5T.F0024.Construction.csproj";

            /// Run.
            var instancesFilePath = Instances.ProjectPathsOperator.GetInstancesFilePath(projectFilePath);
            var namespaceName = Instances.ProjectNamespacesOperator.GetDefaultNamespaceName(projectFilePath);

            Instances.CodeFileGenerator.CreateInstancesFile(
                instancesFilePath,
                namespaceName);
        }

        static void CreateProgramFile()
        {
            /// Inputs.
            var projectFilePath = @"";

            /// Run.
            var programFilePath = Instances.ProjectPathsOperator.GetProgramFilePath(projectFilePath);
            var namespaceName = Instances.ProjectNamespacesOperator.GetDefaultNamespaceName(projectFilePath);

            Instances.CodeFileGenerator.CreateProgramFile(
                programFilePath,
                namespaceName);
        }
    }
}
