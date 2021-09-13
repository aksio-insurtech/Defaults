# Getting Started

In your project all you need is to add a **PackageReference** to the [package](https://www.nuget.org/packages/Aksio.Defaults/).
The `dotnet` tool-chain will during build include any `.props` or `.targets` files found in the package by convention.
From the `.props` file you'll get a lot of default configuration set up, it will put in package information saying it is an Aksio package
and all the defaults of Aksio. This can be overridden if you're only interested in parts of the configuration.

You add the reference by doing the following from your terminal:

```shell
$ dotnet add package Aksio.Defaults
```

Or manually add the following to your `.csproj` - obviously for good measure,
you should just add the `<PackageReference>` inside an existing `<ItemGroup>`
with package references.

```xml
<ItemGroup>
    <PackageReference Include="Aksio.Defaults" Version="1.*"/>
</ItemGroup>
```

By using a wildcard for minor in the version of the packages, you're guaranteed to have the latest of the package.
