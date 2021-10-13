# [v1.1.14] - 2021-10-13 [PR: #26](https://github.com/aksio-system/Defaults/pull/26)

### Fixed

- Disabling static code analysis rule CA1014 - requirement of `[CLSCompliant]` attribute on assemblies. This showed up as an error after upgrading to .NET Core 6 RC2. Might be something we can revert later.


# [v1.1.13] - 2021-10-6 [PR: #25](https://github.com/aksio-system/Defaults/pull/25)

### Fixed

- Disabling rule SA1513: Closing brace should be followed by blank line.


# [v1.1.12] - 2021-10-1 [PR: #24](https://github.com/aksio-system/Defaults/pull/24)

### Fixed

- Disabling SA1516 rule : Elements should be separated by blank line 


# [v1.1.11] - 2021-9-22 [PR: #23](https://github.com/aksio-system/Defaults/pull/23)

### Fixed

- Adding escalation of warnings to errors with new `MSBuildTreatWarningsAsErrors` to enable this for all code analysis rules. Read more [here](https://github.com/MicrosoftDocs/visualstudio-docs/issues/4660), discovered in [this workaround](https://github.com/dotnet/roslyn/issues/43051#issuecomment-631943470).
- Disabling RCS1079 for enabling the use of `NotImplementedException`.


# [v1.1.10] - 2021-9-17 [PR: #22](https://github.com/aksio-system/Defaults/pull/22)

### Fixed

- Official stable version `StyleCop.Analyzers` is 1.1.118 and it predates C#9 and records - the latest beta version 1.2.0-beta.354 supports this. Read more [here](https://github.com/DotNetAnalyzers/StyleCopAnalyzers/issues/3213).



# [v1.1.9] - 2021-9-15 [PR: #21](https://github.com/aksio-system/Defaults/pull/21)

### Fixed

- Moving common rules found in **.editorconfig** files to rulesets to maintain it in one place instead.



# [v1.1.8] - 2021-9-15 [PR: #20](https://github.com/aksio-system/Defaults/pull/20)

### Fixed

- Adding `$(NoWarn)` in front of `<NoWarn/>` settings in props files - this will allow combining the common rules with specifics in either a **.csproj** file or a **Directory.Build.props** file. 


# [v1.1.7] - 2021-9-15 [PR: #19](https://github.com/aksio-system/Defaults/pull/19)

### Fixed

- [AS0008](./Documentation/CodeAnalysis/Analyzers/AS0008.md) rule was too strict; allowing for [NotImplementedException](https://docs.microsoft.com/en-us/dotnet/api/system.notimplementedexception).


# [v1.1.6] - 2021-9-15 [PR: #18](https://github.com/aksio-system/Defaults/pull/18)

### Fixed

- Configuring missing rules to allow for both .NET 6 and Specs to be written with a natural language.



# [v1.1.5] - 2021-9-15 [PR: #17](https://github.com/aksio-system/Defaults/pull/17)

### Fixed

- Moving rules from <NoWarn/> for Specs projects into the `code_analysis.ruleset`file.



# [v1.1.4] - 2021-9-14 [PR: #16](https://github.com/aksio-system/Defaults/pull/16)

### Fixed

- Disabling [SA1000](https://documentation.help/StyleCop/SA1000.html) to allow for formatting that says `new()`.



# [v1.1.3] - 2021-9-14 [PR: #15](https://github.com/aksio-system/Defaults/pull/15)

### Fixed

- Disabling [SA1009](https://documentation.help/StyleCop/SA1009.html) StyleCop rule.



# [v1.1.2] - 2021-9-14 [PR: #14](https://github.com/aksio-system/Defaults/pull/14)

### Fixed

- Disabling the StyleCop rule [SA1633](https://documentation.help/StyleCop/SA1633.html) as we are not requiring a file header.


# [v1.1.1] - 2021-9-14 [PR: #13](https://github.com/aksio-system/Defaults/pull/13)

### Fixed

- Generate Xml documentation by default to default location (bin/<config>/<assemblyname>.xml).
- Fix an error where the Aksio logo asset was included twice.



# [v1.1.0] - 2021-9-14 [PR: #12](https://github.com/aksio-system/Defaults/pull/12)

### Added

- Adding sane default NoWarn for specifications (Aksio.Defaults.Specs).


# [v1.0.2] - 2021-9-14 [PR: #11](https://github.com/aksio-system/Defaults/pull/11)

### Fixed

- Wrong target framework had been set. Supporting netstandard2.0.
- Fixing comparison of message for AS0006 to ignore case.


# [v1.0.1] - 2021-9-14 [PR: #9](https://github.com/aksio-system/Defaults/pull/9)

### Fixed

- NuGet package build fixed to not force the inclusion of symbols when some packages don't have them.



# [v1.0.0] - 2021-9-14 [PR: #8](https://github.com/aksio-system/Defaults/pull/8)

Initial release

