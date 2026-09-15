using Fallout.Common;
using Fallout.Solutions;

namespace Hexagrams.Fallout.Components;

/// <summary>
/// Provides properties for accessing information about the solution being built.
/// </summary>
public interface IHasSolution : IFalloutBuild
{
    /// <summary>
    /// Gets a representation of the solution being built.
    /// </summary>
    [Solution]
    [Required]
    Solution Solution => TryGetValue(() => Solution)!;
}
