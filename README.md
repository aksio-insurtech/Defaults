# Defaults

[![Build](https://github.com/aksio-system/Defaults/actions/workflows/build.yml/badge.svg)](https://github.com/aksio-system/Defaults/actions/workflows/build.yml)
[![Nuget](https://img.shields.io/nuget/v/aksio.defaults)](http://nuget.org/packages/aksio.defaults)
[![Nuget](https://img.shields.io/nuget/v/aksio.defaults.specs)](http://nuget.org/packages/aksio.defaults.specs)

This repository contains the default setup for projects with properties for how they should build
and also static code analysis for projects.
It contains custom rules and the default rule-sets with the tuned rules we care about.

Read more about the custom analyzers [here](./Documentation/CodeAnalysis/Analyzers/overview.md).

## Getting Started

In your project all you need is to add a **PackageReference** to the [package](https://www.nuget.org/packages/Aksio.Defaults/).
The `dotnet` tool-chain will during build include any `.props` or `.targets` files found in the package by convention.
From the `.props` file you'll get a lot of default configuration set up, it will put in package information saying it is an Aksio package
and all the defaults of Aksio. This can be overridden if you're only interested in parts of the configuration.

If you're using an IDE such as Visual Studio, add a reference to the **Aksio.Defaults** package from the UI.

If you're using the **dotnet** tool you add the reference by doing the following from your terminal:

```shell
$ dotnet add package Aksio.Defaults
```

Or manually add the following to your `.csproj` - obviously for good measure,
you should just add the `<PackageReference>` inside an existing `<ItemGroup>`
with package references.

```xml
<ItemGroup>
    <PackageReference Include="Aksio.Defaults" Version="1.*" PrivateAssets="All"/>
</ItemGroup>
```

> Note: The `PrivateAssets="All"` is important to not let the rules and setup affect any consumer of your package.

By using a wildcard for minor in the version of the packages, you're guaranteed to have the latest of the package.

For your spec projects there is a second package with specific rules for that context as we tend to write the
specs differently; `Aksio.Defaults.Specs`.

### MSBuild

This project relies heavily on MSBuild and its capabilities. It leverages both [reserved well known properties](https://docs.microsoft.com/en-us/visualstudio/msbuild/msbuild-reserved-and-well-known-properties?view=vs-2019)
and [common project properties](https://docs.microsoft.com/en-us/visualstudio/msbuild/common-msbuild-project-properties?view=vs-2019).
It takes advantage of a feature in MSBuild that by convention will include props from a file named the same as its package name in any
consumers. In our case this is the [Aksio.Defaults.props](./Source/Defaults.Aksio.Defaults.props) and [Aksio.Defaults.Specs.props](./Source/Defaults.Aksio.Defaults.Specs.props).

### Static Code Analysis

The props files configures a default behavior for builds with a [common set of static code analysis rules](./Source/Defaults/code_analysis.ruleset) and
[stylecop rules](./Source/Defaults/stylecop.json). In addition to this it provides a set of default NuGet metadata properties to ease
the creation of projects that are to be published as NuGet packages.

Read more about the [ruleset format](https://github.com/dotnet/roslyn/blob/master/docs/compilers/Rule%20Set%20Format.md) and the default [rulset](https://docs.microsoft.com/en-us/visualstudio/code-quality/rule-set-reference?view=vs-2019).
In addition, we leverage a 3rd party ruleset - read more about the different rules [here](https://github.com/meziantou/Meziantou.Analyzer/tree/main/docs).

This repository also adds custom Aksio rules. Read the [documentation](./Documentation/CodeAnalysis/Analyzers/overview.md) on the different rules.

With the introduction of [Global AnalyserConfig](https://docs.microsoft.com/en-us/dotnet/fundamentals/code-analysis/configuration-files#global-analyzerconfig) one can
take typical things one would hav ein `.editorconfig` files and package for reuse. This project does so as well by adding a global editorconfig.
For examples on how these can be set up look [here](https://github.com/dotnet/roslyn/blob/main/.editorconfig) or [here](https://gist.github.com/bryanknox/e07027d4d32e0288e488b918545786c8).

### Packages

NuGet packages that are published on the public NuGet feed should adhere to the defined [best practices](https://docs.microsoft.com/en-us/nuget/create-packages/package-authoring-best-practices).
The default **props** file puts in most of the metadata, but some of it is specific to each project and should be
included specifically in the **.csproj** or [Directory.Build.props](https://docs.microsoft.com/en-us/visualstudio/msbuild/build-process-overview?view=vs-2019#user-configurable-imports).

Add the following properties and configure them according to your project:

```xml
<PropertyGroup>
    <IsPackable>true</IsPackable>
    <RepositoryUrl>https://github.com/aksio-system/{repository}</RepositoryUrl>
    <PackageProjectUrl>https://github.com/aksio-system/{repository}</PackageProjectUrl>
</PropertyGroup>
```

In addition you might want to include the **README** file of your project. Add the following
with the correct relative filepath to the **README** file:

```xml
<PropertyGroup>
    <PackageReadmeFile>README.md</PackageReadmeFile>
</PropertyGroup>

<ItemGroup>
    <Content Include="../README.md" PackagePath="/" />
</ItemGroup>
```
