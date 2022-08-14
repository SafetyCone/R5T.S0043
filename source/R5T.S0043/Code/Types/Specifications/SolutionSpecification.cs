using System;
using System.Collections.Generic;


namespace R5T.S0043.T001
{
    public class SolutionSpecification
    {
        public Guid Identity { get; }
        public string Name { get; set; }
        public List<Guid> ProjectIdentities { get; } = new();
        public List<Guid> DependencyProjectIdentities { get; } = new();
    }
}
