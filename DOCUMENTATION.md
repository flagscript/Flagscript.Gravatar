# Flagscript.Gravatar Documentation (>= v1.0.0)

Thanks for using Flagscript.Gravatar! If you have any quesitons, feel free to post a question on the [issues board](../../issues). 

## Contents

1. Profile Library
2. Tag Helper

## Profile Library

Flagscript.Gravatar contains a library to obtain Gravatar profile information given a Gravatar email. 

### Gravatar Hash

Although used internally, a method is available to obtain a [Gravatar hash](https://en.gravatar.com/site/implement/hash/) for a Gravatar email address. 

```csharp
using Flagscript.Gravatar;

var testEmail = "fakeemail@nodomain.tech";

var library = = new GravatarLibrary();
var emailHash = library.GenerateEmailHash(testEmail);
```

### Gravatar Profile

Specific properties of the [Gravatar Profile](https://en.gravatar.com/site/implement/profiles/) can be obtained using the library. 

An optional in-memory cache for profiles can be used by passing a **GravatarProfileMemoryCache** into the library.

```csharp
using Flagscript.Gravatar;

var testEmail = "fakeemail@nodomain.tech";

var memCache = new GravatarProfileMemoryCache();
var library = new GravatarLibrary(memCache);
var profile = await library.GetGravatarProfile(testEmail);
```

## Tag Helper

An aspnet core tag helper is available to display gravatar images. This tag helper can take a DI library to use caching. 

DI Setup:

```csharp
// Startup.cs
using Flagscript.Gravatar;
using Flagscript.Gravatar.TagHelpers;

public void ConfigureServices(IServiceCollection services)
{

	// Flagscript Injects
	services.AddSingleton<GravatarProfileMemoryCache>();
	services.AddSingleton<GravatarLibrary>();
	services.AddSingleton<GravatarTagHelperConfiguration>();

}
```

View Imports:

```csharp
@addTagHelper *, Flagscript.Gravatar
```

Razor:

```csharp
<gravatar size="250">someemail@somedomain.com</gravatar>
```
