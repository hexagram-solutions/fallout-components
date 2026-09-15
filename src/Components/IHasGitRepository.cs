using Fallout.Common;
using Fallout.Common.Git;

namespace Hexagrams.Fallout.Components;

/// <summary>
/// Provides properties for accessing information about the current Git repository.
/// </summary>
public interface IHasGitRepository : IFalloutBuild
{
    /// <summary>
    /// Gets information about the current Git repository.
    /// </summary>
    [Required]
    [GitRepository]
    GitRepository GitRepository => TryGetValue(() => GitRepository)!;
}
