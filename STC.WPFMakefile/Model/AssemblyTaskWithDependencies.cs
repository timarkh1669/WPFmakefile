using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STC.WPFMakefile.TopologicalSort;

namespace STC.WPFMakefile.Model
{
    public class AssemblyTaskWithDependencies : IGraphVertex<AssemblyTaskWithDependencies>
    {
        public string Name { get; }
        public List<string> DependenciesNames { get; }
        public List<string> Actions { get; }

        public List<AssemblyTaskWithDependencies> OutEdges { get; private set; }

        public AssemblyTaskWithDependencies(string name,
            List<string> dependencies,
            List<string> actions)
        {
            Name = name;
            DependenciesNames = dependencies ?? new List<string>();
            Actions = actions ?? new List<string>();
            OutEdges = new List<AssemblyTaskWithDependencies>();
        }

        public void AddDependency(AssemblyTaskWithDependencies task)
        {
            OutEdges.Add(task);
        }

        public void AddAction(string task)
        {
            Actions.Add(task);
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
