using System.Collections.Generic;
using Fallout.Common;
using Fallout.Solutions;
using Hexagrams.Fallout.Components;

// ReSharper disable RedundantExtendsListEntry

class Build : FalloutBuild, ICompile, ITest, IReportCoverage
{
    public static int Main() => Execute<Build>(x => ((ICompile) x).Compile);

    // The path to the solution file must be explicitly specified because this project is nested below the main build
    // project for the repository. Normally, you don't have to specify a relative path to the solution file.
    [Solution("./Hexagrams.Fallout.Samples.Test.slnx")]
    readonly Solution Solution;
    Solution IHasSolution.Solution => Solution;

    public IEnumerable<Project> TestProjects => Solution.GetAllProjects("*.Tests");

    public bool CreateCoverageHtmlReport => true;
}
