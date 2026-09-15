using Fallout.Common;
using Fallout.Solutions;
using Hexagrams.Fallout.Components;

class Build : FalloutBuild, ICompile
{
    public static int Main() => Execute<Build>(x => ((ICompile) x).Compile);

    // The path to the solution file must be explicitly specified because this project is nested below the main build
    // project for the repository. Normally, you don't have to specify a relative path to the solution file.
    [Solution("./Hexagrams.Fallout.Samples.Basic.slnx")]
    readonly Solution Solution;
    Solution IHasSolution.Solution => Solution;

    Target ICompile.Compile => t => t
        .Inherit<ICompile>();
}
