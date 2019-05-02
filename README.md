# Flagscript.Gravatar

Dotnet Standard integration with [https://gravatar.com](Gravatar) APIs.

| Version | Status |
| --- | --- |
| Latest | [![last commit](https://img.shields.io/github/last-commit/flagscript/Flagscript.Gravatar.svg?logo=github)](https://github.com/flagscript/Flagscript.Gravatar) [![build status](https://img.shields.io/appveyor/ci/Flagscript/flagscript-gravatar.svg?logo=appveyor)](https://ci.appveyor.com/project/Flagscript/flagscript-gravatar) [![unit test](https://img.shields.io/appveyor/tests/Flagscript/flagscript-gravatar.svg?label=unit%20tests&logo=appveyor)](https://ci.appveyor.com/project/Flagscript/flagscript-gravatar) |
| Master | [![last commit](https://img.shields.io/github/last-commit/flagscript/Flagscript.Gravatar/master.svg?logo=github)](https://github.com/flagscript/Flagscript.Gravatar) [![build status](https://img.shields.io/appveyor/ci/Flagscript/flagscript-gravatar/master.svg?logo=appveyor)](https://ci.appveyor.com/project/Flagscript/flagscript-gravatar) [![unit and integration  test](https://img.shields.io/appveyor/tests/Flagscript/flagscript-gravatar/master.svg?label=unit/integration%20tests&logo=appveyor)](https://ci.appveyor.com/project/Flagscript/flagscript-gravatar) [![Codacy](https://img.shields.io/codacy/grade/096a3c8d327e4e168bea4e3ebf06d402fake.svg?logo=codacy)](https://app.codacy.com/project/flagscript/Flagscript.Gravatar/dashboard) [![LGTM Total Alerts](https://img.shields.io/lgtm/alerts/g/flagscript/Flagscript.Gravatar.svg?logo=lgtm&logoWidth=18)](https://lgtm.com/projects/g/flagscript/Flagscript.Gravatar/alerts/) |
| Pre-Release | [![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/Flagscript.Gravatar.svg?logo=nuget)](https://www.nuget.org/packages/Flagscript.Gravatar) |
| Release | [![Nuget](https://img.shields.io/nuget/v/Flagscript.Gravatar.svg?logo=nuget)](https://www.nuget.org/packages/Flagscript.Gravatar) |

## Simple Usage

The Flagscript.Gravatar package has two primary functionalities:

- A library to wrap the Gravatar APIs.

```csharp
using Flagscript.Gravatar;

var email = "someemail@somedomain.com";
var library = new GravatarLibrary();
var gravatarEmailHash = library.GenerateEmailHash(email);
var gravatarProfile = library.GetGravatarProfile(email);
```

- An asp.net core tag helper to insert gravatar images

```csharp

@addTagHelper *, Flagscript.Gravatar

<gravatar size="250">someemail@somedomain.com</gravatar>

```

## Documentation

[Documentation](./DOCUMENTATION.md) on how to use the Flagscript.Gravatar library is available within this repository. 

## Download

Flagscript.Gravatar is available as a NuGet package:

### .NET CLI

```bash
> dotnet add package Flagscript.Gravatar --version 0.9.0-beta
```

### .csproj

```xml
<PackageReference Include="Flagscript.Gravatar" Version="0.9.0-beta" />
```

## Contributing

Although contributions for this project are not yet open, please read 
[CONTRIBUTING](https://github.com/flagscript/Flagscript.Gravatar/blob/master/CONTRIBUTING.md) 
for details on our code of conduct.

## Versioning

We use [SemVer](http://semver.org/) for versioning. For the versions available, see 
the [tags on this repository](https://github.com/flagscript/Flagscript.Gravatar/releases). 

## Authors

* **Greg Kaestle** - [Flagscript](https://flagscript.technology)

## License

This project is licensed under the MIT License - see the [LICENSE](https://github.com/flagscript/Flagscript.Gravatar/blob/master/LICENSE.md) file for details.
