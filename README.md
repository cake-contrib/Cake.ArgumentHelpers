# Cake.ArgumentHelpers

[Cake](http://cakebuild.net/) aliases (methods) to help with consuming arguments and environment variables.

## Getting Started

Just reference the Cake.ArgumentHelpers NuGet package directly in your build script via a Cake addin directive.

> `#addin nuget:?package=Cake.ArgumentHelpers`

## Available Aliases

Yep, just one so far...

### `bool ArgumentOrEnvironmentVariable(..., string name, string environmentNamePrefix, bool defaultValue)`

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

## 