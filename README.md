# fallout-components | [![continuous](https://github.com/hexagram-solutions/fallout-components/actions/workflows/continuous.yml/badge.svg)](https://github.com/hexagram-solutions/fallout-components/actions/workflows/continuous.yml) ![release](https://github.com/hexagram-solutions/fallout-components/actions/workflows/release.yml/badge.svg)

Shared components for the [Fallout build system](https://fallout.build).

For more information about these components, see the [docs](https://hexagram-solutions.github.io/fallout-components/api/Hexagrams.Fallout.Components.html).

To read more about shared components in general, see the [official Fallout docs](https://docs.fallout.build/docs/sharing/build-components).

This library is published to [NuGet](https://www.nuget.org/packages/Hexagrams.Fallout.Components).

## Contributing

This repository uses [Husky.Net](https://alirezanet.github.io/Husky.Net/) for
pre-push hooks. After cloning the repo, set up husky by running the following
commands:

```pwsh
dotnet tool restore
dotnet husky install
```

## Usage

To use the shared components in your build, install the NuGet package:

```powershell
dotnet add package .\build\MyFalloutBuild.csproj Hexagrams.Fallout.Components
```

> ℹ In your build project, you'll want to keep or add an explicit
> package reference
> to `Fallout.Common`. This will ensure you keep the project organization provided
> by the Fallout MSBuild targets:
>
> ```powershell
> dotnet add package .\build\MyFalloutBuild.csproj Fallout.Common
> ```

See the [samples](./samples/) for examples of how to use these components in
your build projects.

## Build

This project uses the Fallout build tool (naturally). Fallout builds can be
invoked in the following ways:

### Fallout global tool

The preferred way to invoke Fallout builds is with the [global tool](https://docs.fallout.build/docs/getting-started/installation).
To install it, run the following command:

```powershell
dotnet tool install Fallout.GlobalTool --global
```

The tool installs as the command `fallout`. Verify your installation by listing
the available targets:

```powershell
fallout --help
```

Build targets can now be run like so:

```powershell
fallout compile
fallout test
fallout verify-format
# etc.
```

> ℹ On Linux, you may need to add `$HOME/.dotnet/tools` to your `PATH` before
> the `fallout` command resolves.

A global install is optional. This repo also pins the tool in
`.config/dotnet-tools.json`, so the local copy works just as well:

```powershell
dotnet tool restore
dotnet fallout test
```

> ℹ For added flavour, enable tab-completion for the global tool in your shell.
> See the official docs for instructions [here](https://docs.fallout.build/docs/global-tool/shell-completion/).

### Scripts

Fallout generates PowerShell, cmd, and bash scripts that invoke builds and build
targets. To select a build target, specify it either as an argument or with the
`--target` switch. For example:

```powershell
./build.ps1 # Run the default build
./build.ps1 test # Run the 'test' target
./build.ps1 --target test # Run the 'test' target
```

### Console app

Fallout builds are pure C# console apps. So, to run a build you can run the
`_build` project from your IDE, just as you would any other executable.
