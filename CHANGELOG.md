# [v1.5.12] - 2022-4-17 [PR: #69](https://github.com/aksio-insurtech/Defaults/pull/69)

### Fixed

- Disabling CA1310 for not requiring to have to use `StringComparison` for string operations. Our things are primarily backend dockerized solutions, we have control over culture settings.


# [v1.5.11] - 2022-4-16 [PR: #68](https://github.com/aksio-insurtech/Defaults/pull/68)

### Fixed

- Disabling **MA0047** that required one to pass precise `StringComparison` arguments for string comparison methods.


# [v1.5.10] - 2022-3-25 [PR: #67](https://github.com/aksio-insurtech/Defaults/pull/67)

### Fixed

- Disabling CA1305 related to globalization. We have control over the runtime environment.


# [v1.5.9] - 2022-3-25 [PR: #66](https://github.com/aksio-insurtech/Defaults/pull/66)

### Fixed

- Styled the code according to our style rules.
- Disable requirement for globalization awareness in code, we run our things in controlled environments (#64).
- You have to await calls. Calls that are not awaited (CS4014) will cause a compiler error (#65).
- You have to await within an async method (CS1998) (#65).


# [v1.5.8] - 2022-1-15 [PR: #63](https://github.com/aksio-insurtech/Defaults/pull/63)

### Fixed

- Disabling CA1019


# [v1.5.7] - 2022-1-15 [PR: #62](https://github.com/aksio-insurtech/Defaults/pull/62)

### Fixed

- Fixing visibility filter for members to be consistent. Adding a new setup for configuring expected visibility on different member types.


# [v1.5.6] - 2022-1-15 [PR: #61](https://github.com/aksio-insurtech/Defaults/pull/61)

### Fixed

- Property ordering ignores anything but public properties now. Reason for this is that it ends up conflicting with other rules wanting publics before privates and causing private properties to sit inbetween public properties and public constructors and not in their more natural location.


# [v1.5.5] - 2022-1-15 [PR: #60](https://github.com/aksio-insurtech/Defaults/pull/60)

### Fixed

- Disable MA0076


# [v1.5.4] - 2022-1-14 [PR: #59](https://github.com/aksio-insurtech/Defaults/pull/59)

### Fixed

- Improve error messages for ordering rules to include the word **come** in the context of *before* and *after*.


# [v1.5.3] - 2022-1-14 [PR: #58](https://github.com/aksio-insurtech/Defaults/pull/58)

### Fixed

- Disable MA0031


# [v1.5.2] - 2022-1-14 [PR: #57](https://github.com/aksio-insurtech/Defaults/pull/57)

### Fixed

- Disable MA0046
- Disable CA1003


# [v1.5.1] - 2022-1-14 [PR: #56](https://github.com/aksio-insurtech/Defaults/pull/56)

### Fixed

- Disable MA0015, MA0018, MA0049


# [v1.5.0] - 2022-1-14 [PR: #55](https://github.com/aksio-insurtech/Defaults/pull/55)

### Changed

- Allowing attributes to be sealed - as they should actually be sealed, which another rule will catch.


# [v1.4.12] - 2021-12-16 [PR: #54](https://github.com/aksio-system/Defaults/pull/54)

### Fixed

- Disabling MA0051

# [v1.4.11] - 2021-12-16 [PR: #53](https://github.com/aksio-system/Defaults/pull/53)

### Fixed

- Disabling MA0011 and MA0053


# [v1.4.10] - 2021-12-16 [PR: #52](https://github.com/aksio-system/Defaults/pull/52)

### Fixed

- Disabling MA0038.


# [v1.4.9] - 2021-12-16 [PR: #51](https://github.com/aksio-system/Defaults/pull/51)

### Fixed

- Disable rules IDE0160, CA2000, MA0026, MA0054, MA0058, MA0075.


# [v1.4.8] - 2021-12-15 [PR: #50](https://github.com/aksio-system/Defaults/pull/50)

### Fixed

- Fixing how the `.globalconfig` is included according to documentation found [here](https://docs.microsoft.com/en-us/dotnet/fundamentals/code-analysis/configuration-files#naming).


# [v1.4.7] - 2021-12-15 [PR: #49](https://github.com/aksio-system/Defaults/pull/49)

### Fixed

- Disable naming rules IDE1006 for specs.

# [v1.4.5] - 2021-12-15 [PR: #47](https://github.com/aksio-system/Defaults/pull/47)

### Fixed

- Disabling MA0004 that suggests adding .ConfigureAwait() for contexts that can be left.


# [v1.4.4] - 2021-12-15 [PR: #46](https://github.com/aksio-system/Defaults/pull/46)

### Fixed

- Disabling MA0002 rule.


# [v1.4.3] - 2021-12-15 [PR: #45](https://github.com/aksio-system/Defaults/pull/45)

## Summary

Disabling some of the rules from the [Meziantou Analyzers](https://github.com/meziantou/Meziantou.Analyzer/tree/main/docs).

### Fixed

- Disable MA0003, MA0006, MA0007, MA00025, MA0040.


# [v1.4.2] - 2021-12-15 [PR: #44](https://github.com/aksio-system/Defaults/pull/44)

### Fixed

- Disabling [IDE1006](https://docs.microsoft.com/en-us/dotnet/fundamentals/code-analysis/style-rules/naming-rules#rule-id-ide1006-naming-rule-violation), which effectively nulled out anything trying to use the `.editorconfig` for diagnostics & style rules.


# [v1.4.1] - 2021-12-15 [PR: #43](https://github.com/aksio-system/Defaults/pull/43)

### Fixed

- Upgrading all 3rd parties.


# [v1.4.0] - 2021-12-15 [PR: #42](https://github.com/aksio-system/Defaults/pull/42)

## Summary

Trying to 

### Added

- Added `<EnforceCodeStyleInBuild/>` for props files.
- Added explicit package reference to `Microsoft.CodeAnalysis.CSharp.CodeStyle`.

### Fixed

- Setting severity to error for C# naming rules.


# [v1.3.1] - 2021-12-15 [PR: #41](https://github.com/aksio-system/Defaults/pull/41)

### Fixed

- Making sure we get to publish with `dot` files (e.g. .editorconfig)


# [v1.3.0] - 2021-12-15 [PR: #40](https://github.com/aksio-system/Defaults/pull/40)

### Added

- Adding a global `.editorconfig` file to get all of our naming styles standardized for projects. Leveraging the [GlobalAnalyzerConfig](https://docs.microsoft.com/en-us/dotnet/fundamentals/code-analysis/configuration-files#global-analyzerconfig) option

# [v1.2.6] - 2021-12-15 [PR: #39](https://github.com/aksio-system/Defaults/pull/39)

### Fixed

- Disabling CA5394 - Random is random enough for our purposes and for cryptography we would use a cryptography API.


# [v1.2.5] - 2021-12-15 [PR: #38](https://github.com/aksio-system/Defaults/pull/38)

### Fixed

- Disabling CA2000 for specs. Not requiring using statements in specs.

# [v1.2.4] - 2021-12-14 [PR: #36](https://github.com/aksio-system/Defaults/pull/36)

### Fixed

- Disable CA2234 - disallowing specific API call. Weird rule - why not mark this as obsolete if its not a desirable API call.


# [v1.2.3] - 2021-12-14 [PR: #35](https://github.com/aksio-system/Defaults/pull/35)

### Fixed

- Disabling SA1115 - allowing empty lines between arguments. This to allow for comments related to arguments being passed in and not having to condensed of a view on it.
- Disabling warning related to having to add `ConfigureAwait()` to async calls (CS0012, RCS1090, CA2007). If running in an app model context, which eseentially we will do for all our stuff - this is just unwanted and not needed noise. Read more here: https://devblogs.microsoft.com/dotnet/configureawait-faq/#when-should-i-use-configureawaitfalse.



# [v1.2.2] - 2021-12-14 [PR: #34](https://github.com/aksio-system/Defaults/pull/34)

### Fixed

- Disabling SA1201 since we now have our own rule for ordering.



# [v1.2.1] - 2021-12-6 [PR: #33](https://github.com/aksio-system/Defaults/pull/33)

### Fixed

It tried to instansiate the baseclassf or the new tests, which failed.
Moved the DiagnosticAnalyzerAttribute to implementing classes to fix this.

# [v1.2.0] - 2021-12-6 [PR: #32](https://github.com/aksio-system/Defaults/pull/32)

## Summary

Aksios version of Stylecop rule SA1201: ElementsMustAppearInTheCorrectOrder, as we want a slightly different ordering of the elements.

This does not consider interfaces, enums, structs or classes within a class. That needs to be discussed.

# [v1.1.19] - 2021-11-23 [PR: #31](https://github.com/aksio-system/Defaults/pull/31)

### Fixed

- Disabling [SA1128](https://github.com/DotNetAnalyzers/StyleCopAnalyzers/blob/master/documentation/SA1128.md) - Constructor initailizes must be on own line
- Disabling [SA1623](https://documentation.help/StyleCop/SA1623.html) - Property summary documentation must match accessor.
- Disabling [SA1642](https://documentation.help/StyleCop/SA1642.html) - Constructor summary documentation must begin with standard text.


# [v1.1.18] - 2021-10-28 [PR: #30](https://github.com/aksio-system/Defaults/pull/30)

### Fixed

- Disabling RCS1163 to allow for unused parameters on methods.

# [v1.1.17] - 2021-10-27 [PR: #29](https://github.com/aksio-system/Defaults/pull/29)

### Fixed

- Correction from v1.1.16; Accidently turned off wrong rule; SA1308 instead of CA1308.


# [v1.1.16] - 2021-10-26 [PR: #28](https://github.com/aksio-system/Defaults/pull/28)

### Fixed

- Allowing string interpolation over concatenation (RCS1217)
- Allowing `ToLowerInvariant()` on strings (SA1308)


# [v1.1.15] - 2021-10-13 [PR: #27](https://github.com/aksio-system/Defaults/pull/27)

### Fixed

- Disable static code analysis rule CA1812 - it prohibited the use of top level programs. This started occurring after upgrading to .NET Core 6 RC2 - we might be able to revert this change later.


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

