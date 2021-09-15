# Overview

Below are the Aksio specific rules that we are enforcing.
Rules can be disabled either through custom [rulesets](https://docs.microsoft.com/en-us/visualstudio/code-quality/how-to-create-a-custom-rule-set)
or using the [-nowarn](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-options/nowarn-compiler-option) compiler option or using the
[NoWarn property](https://docs.microsoft.com/en-us/visualstudio/msbuild/common-msbuild-project-properties?view=vs-2019) in your `.csproj` file.

Example of NoWarn property:

```xml
<NoWarn>$(NoWarn),AK0001,AK0004</NoWarn>
```

## Rules

| Id | Title |
| --- | ----- |
| [AS0001](./AS0001.md) | SerializableNotAllowed |
| [AS0002](./AS0002.md) | PrivateNotAllowed |
| [AS0003](./AS0003.md) | SealedNotAllowed |
| [AS0004](./AS0004.md) | ExceptionShouldNotBeSuffixed |
| [AS0005](./AS0005.md) | ExceptionShouldOnlyHaveOneConstructor |
| [AS0006](./AS0006.md) | ExceptionConstructorParametersShouldNotContainMessage |
| [AS0007](./AS0007.md) | ExceptionDescriptionShouldFollowStandard |
| [AS0008](./AS0008.md) | ExceptionShouldBeSpecific |

Due to some rules in the standard analyzers from Microsoft being marked as
*hidden* and as a consequence is not possible to enable during build, we've
created wrappers for the following.

| Id | Title |
| --- | ----- |
| CS8019 | RemoveUnnecessaryImports |
