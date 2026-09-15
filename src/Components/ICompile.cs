using Fallout.Common;
using Fallout.Common.Tooling;
using Fallout.Common.Tools.DotNet;
using Fallout.Common.Utilities;
using Fallout.Common.Utilities.Collections;
using Fallout.Solutions;
using static Fallout.Common.Tools.DotNet.DotNetTasks;

namespace Hexagrams.Fallout.Components;

/// <summary>
/// Targets and settings for compiling the solution.
/// </summary>
public interface ICompile : IRestore, IClean, IHasConfiguration
{
    /// <summary>
    /// Compile the solution with <c>dotnet build</c>.
    /// </summary>
    Target Compile => t => t
        .Description("Compile the solution")
        .DependsOn(Clean)
        .DependsOn(Restore)
        .WhenSkipped(DependencyBehavior.Skip)
        .Executes(() =>
        {
            ReportSummary(d => d
                .WhenNotNull(this as IHasVersioning, (x, o) => x
                    .AddPair("Version", o!.Versioning.Version)));

            DotNetBuild(s => s
                .Apply(CompileSettingsBase)
                .Apply(CompileSettings));

            DotNetPublish(s => s
                    .Apply(PublishSettingsBase)
                    .Apply(PublishSettings)
                    .CombineWith(PublishConfigurations, (x, v) => x.SetProject((string) v.Project)
                        .SetFramework(v.Framework)),
                PublishDegreeOfParallelism);
        });

    /// <summary>
    /// Settings for controlling compilation behavior.
    /// </summary>
    sealed Configure<DotNetBuildSettings> CompileSettingsBase => t => t
        .SetProjectFile(Solution)
        .SetConfiguration(Configuration)
        .When(_ => IsServerBuild, s => s
            .EnableContinuousIntegrationBuild())
        .SetNoRestore(SucceededTargets.Contains(Restore))
        .WhenNotNull(this as IHasGitRepository, (s, o) => s
            .SetRepositoryUrl(o!.GitRepository.HttpsUrl))
        .WhenNotNull(this as IHasVersioning, (s, o) => s
            .SetAssemblyVersion(o!.Versioning.AssemblyVersion)
            .SetFileVersion(o.Versioning.FileVersion)
            .SetInformationalVersion(o.Versioning.Version));

    /// <summary>
    /// Settings for controlling publish behavior.
    /// </summary>
    sealed Configure<DotNetPublishSettings> PublishSettingsBase => t => t
        .SetConfiguration(Configuration)
        .EnableNoBuild()
        .EnableNoLogo()
        .When(_ => IsServerBuild, s => s
            .EnableContinuousIntegrationBuild())
        .WhenNotNull(this as IHasGitRepository, (s, o) => s
            .SetRepositoryUrl(o!.GitRepository.HttpsUrl))
        .WhenNotNull(this as IHasVersioning, (s, o) => s
            .SetAssemblyVersion(o!.Versioning.AssemblyVersion)
            .SetFileVersion(o.Versioning.FileVersion)
            .SetInformationalVersion(o.Versioning.Version));

    /// <summary>
    /// Additional settings for controlling the <c>dotnet build</c> command.
    /// </summary>
    Configure<DotNetBuildSettings> CompileSettings => t => t;

    /// <summary>
    /// Additional settings for controlling the <c>dotnet publish</c> command.
    /// </summary>
    Configure<DotNetPublishSettings> PublishSettings => t => t;

    /// <summary>
    /// The publish configurations to build with.
    /// </summary>
    IEnumerable<(Project Project, string Framework)> PublishConfigurations => [];

    /// <summary>
    /// The number of projects to publish in parallel when running the <c>dotnet publish</c> command. Defaults to
    /// <c>10</c>.
    /// </summary>
    int PublishDegreeOfParallelism => 10;
}
