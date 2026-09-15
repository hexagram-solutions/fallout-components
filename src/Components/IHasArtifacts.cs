using Fallout.Common;
using Fallout.Common.IO;

namespace Hexagrams.Fallout.Components;

/// <summary>
/// Provides properties for controlling build artifact output.
/// </summary>
public interface IHasArtifacts : IFalloutBuild
{
    /// <summary>
    /// The output directory for build artifacts.
    /// </summary>
    AbsolutePath ArtifactsDirectory => RootDirectory / "artifacts";
}
