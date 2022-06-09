using System;


namespace R5T.S0043
{
    public static class Instances
    {
        public static ICodeFileGenerator CodeFileGenerator { get; } = S0043.CodeFileGenerator.Instance;
        public static IProjectNamespacesOperator ProjectNamespacesOperator { get; } = S0043.ProjectNamespacesOperator.Instance;
        public static IProjectPathsOperator ProjectPathsOperator { get; } = S0043.ProjectPathsOperator.Instance;
    }
}
