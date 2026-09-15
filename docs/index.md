# Hexagrams.Fallout.Components

Shared components for the [Fallout build system](https://fallout.build).

This library provides reusable build targets -- `Compile`, `Test`, `Pack`,
`Push`, `ReportCoverage` and friends -- as C# interfaces that your own Fallout
build project implements. It follows Fallout's
[shared build components](https://docs.fallout.build/docs/sharing/build-components)
pattern, so a build class picks up a target and its settings simply by
implementing the matching interface.

## Getting started

Install the package into your build project:

```powershell
dotnet package add Hexagrams.Fallout.Components --project .\build\MyFalloutBuild.csproj
```

Then implement the components you want:

```csharp
class Build : FalloutBuild, ICompile, ITest
{
    public static int Main() => Execute<Build>(x => ((ITest) x).Test);
}
```

See the <xref:Hexagrams.Fallout.Components> API reference for the full list
of components, and the
[samples](https://github.com/hexagram-solutions/fallout-components/tree/main/samples)
for runnable examples of each one in isolation.
