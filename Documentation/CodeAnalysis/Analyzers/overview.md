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
| [AK0001](./AK0001.md) | SerializableNotAllowed |
| [AK0002](./AK0002.md) | PrivateNotAllowed |
| [AK0003](./AK0003.md) | SealedNotAllowed |
| [AK0004](./AK0004.md) | ExceptionShouldNotBeSuffixed |
| [AK0005](./AK0005.md) | ExceptionShouldOnlyHaveOneConstructor |
| [AK0006](./AK0006.md) | ExceptionConstructorParametersShouldNotContainMessage |
| [AK0007](./AK0007.md) | ExceptionDescriptionShouldFollowStandard |
| [AK0008](./DL0008.md) | ExceptionShouldBeSpecific |

Due to some rules in the standard analyzers from Microsoft being marked as
*hidden* and as a consequence is not possible to enable during build, we've
created wrappers for the following.

| Id | Title |
| --- | ----- |
| CS8019 | RemoveUnnecessaryImports |
