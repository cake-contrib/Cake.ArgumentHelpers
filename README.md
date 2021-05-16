# Cake.ArgumentHelpers

[Cake](http://cakebuild.net/) aliases (methods) to help with consuming arguments and environment variables.

## Give a Star! :star:

If you like or are using this project please give it a star. Thanks!

## Getting Started

Just reference the Cake.ArgumentHelpers NuGet package directly in your build script via a Cake addin directive.

> `#addin nuget:?package=Cake.ArgumentHelpers`

## Available Aliases

Yep, just two so far...

### ArgumentOrEnvironmentVariable (for boolean values)

`bool ArgumentOrEnvironmentVariable(..., string name, string environmentNamePrefix, bool defaultValue)`

Since `EnvironmentVariable` always returns a string, it is more complex to involve it in an `Argument` call with a fallback. This alias makes it a single call, hiding away the string comparison done to massage the result to a `bool` value.

It works by getting a boolean value with multiple fallbacks:

1. Try to get it from `Argument` (e.g., command line: `-SomeSetting="true"`)
2. Try to get it from `EnvironmentVariable` (e.g., `$env:SomeProject_SomeSetting = $True;`)
3. Use a `defaultValue` if we don't find it elsewhere

#### Example

Given a potential command line argument of `SomeSetting` that could also be set via an environment variable prefixed with a project name, get the boolean value or `false` if it isn't found:

```csharp
var isSomethingTrue = ArgumentOrEnvironmentVariable("SomeSetting", "SomeProject_", false);
```

### ArgumentOrEnvironmentVariable (for string values)

`string ArgumentOrEnvironmentVariable(..., string name, string environmentNamePrefix[, string defaultValue])`

This is a helper method that simply wraps around nested calls to Argument and EnvironmentVariable (and offering a fallback default).

It works by getting a string value with multiple fallbacks:

1. Try to get it from `Argument` (e.g., command line: `-SomeSetting="SomeValue"`)
2. Try to get it from `EnvironmentVariable` (e.g., `$env:SomeProject_SomeSetting = "SomeOtherValue";`)
3. Use a `defaultValue` if we don't find it elsewhere

#### Example

Given a potential command line argument of `SomeSetting` that could also be set via an environment variable (optionally prefixed with a project name), get the value from the command line first, falling back to the environment variable next before using the default fallback value if none of those are found:

```csharp
var someVariableValue = ArgumentOrEnvironmentVariable("SomeSetting", "SomeProject_", "SomeFallbackValue");
```

## Discussion

For questions and to discuss ideas & feature requests, use the [GitHub discussions on the Cake GitHub repository](https://github.com/cake-build/cake/discussions), under the [Extension Q&A](https://github.com/cake-build/cake/discussions/categories/extension-q-a) category.

[![Join in the discussion on the Cake repository](https://img.shields.io/badge/GitHub-Discussions-green?logo=github)](https://github.com/cake-build/cake/discussions)

## Release History

Click on the [Releases](https://github.com/cake-contrib/Cake.ArgumentHelpers/releases) tab on GitHub.

---

_Copyright &copy; 2017-2021 Cake Contributors - Provided under the [MIT License](LICENSE)._
