# Flagscript.Gravatar Documentation (>= v0.9.1-beta)

Thanks for using Flagscript! If you have any quesitons, feel free to post a question on the [issues board](../../issues). 

## Contents

1. Exceptions
   - Contains information on exceptions in the Flagscript libraries.

## Exceptions

The following exceptions can be thrown by Flagscript libraries. Handling these exceptions allows you process exceptions specific to the Flagscript framework.

### Base Exception

The exception class **_FlagscriptException_** is at the root of hierarchy for all exceptions thrown in the Flagscript framework. You may catch this exception to distinguish Flagscript exceptions from dotnet or other library exceptions.

A short example of distinguishing all Flagscript Exceptions:

```csharp
using Flagscript;

try
{
  // Code using Flagscript libraries.
}
catch (FlagscriptException fe)
{
}
```

### Configuration Exception

The exception class **_FlagscriptConfigurationException_** is thrown when there is an error with a Flagscript framework configuration. You may catch this exception to distinguish Flagscript configuraiton exceptions from other Flagscript exceptions.

A short example of handling Flagscript configuration exceptions:

```csharp
using Flagscript;

try
{
  // Code using Flagscript libraries.
}
catch (FlagscriptConfigurationException fce)
{
}
```
