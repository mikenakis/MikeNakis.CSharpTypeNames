# MikeNakis.CSharpTypeNames<br/><sub><sup>A tiny library that generates the human-readable name of any System.Type in C# notation.</sup></sub>

<p align="center">
  <img title="MikeNakis.CSharpTypeNames Logo" src="MikeNakis.CSharpTypeNames-Logo.svg" width="256" />
</p>

[![Build](https://github.com/mikenakis/MikeNakis.CSharpTypeNames/actions/workflows/continuous-integration.yml/badge.svg)](https://github.com/mikenakis/MikeNakis.CSharpTypeNames/actions/workflows/continuous-integration.yml)

## What is MikeNakis.CSharpTypeNames?

MikeNakis.CSharpTypeNames is a tiny library that generates human-readable dotnet type names in C# notation.

To understand what problem this library solves, see the following blog post:

 - [michael.gr - Human-readable names of DotNet types in C# notation](https://blog.michael.gr/2025/05/human-readable-names-of-dotnet-types-in.html)

MikeNakis.CSharpTypeNames exposes a single extension method on `System.Type` which can be used as a universal replacement for the `Name` and `FullName` properties of `System.Type`.

The method accepts an optional parameter which can be used to customize the generated name. For example, you can choose:

- Whether you want `System.Nullable<T>` to be replaced with `T?`
- Whether you want `System.ValueTuple<T1,T2>` to be replaced with `(T1,T2)`
- etc.

## How well does it work?
 
It works pretty well. I do not know of any cases that it does not handle correctly.

- The test project exercises this library against a wide array of test cases, many of which are quite creative, and they are all handled flawlessly.

- The drill project exercises this library against 13K types (and their nested types) in the DotNet SDK, and it handles them all flawlessly too.

This gives sufficient reason to believe that `MikeNakis.CSharpTypeNames` works correctly.

## How fast is it?

MikeNakis.CSharpTypeNames is quite efficient:

- It uses a `StringBuilder` instead of string concatenation.
- It refrains from any unnecessary memory allocations.
- It avoids string search in all but one case where it is unavoidable due to bad design of DotNet type names.
- It completely avoids sub-string replacement.

The MikeNakis.CSharpTypeNames.Drill project takes less than 30 milliseconds to generate the human-readable names of 13K types found in the DotNet SDK.

## How small is it?

The dll is less than 10 kilobytes.

## What does it depend on?

MikeNakis.CSharpTypeNames does not have any dependencies.

It targets netstandard2.0, so you can use it not only in projects targeting netcore, but also in projects targeting netframework. 

(The test and drill projects target net8.0 and do have a few dependencies each, but that is irrelevant.)

## How to use?

    using MikeNakis.CSharpTypeNames.Extensions;

	    string typeName = typeof( int ).GetCSharpName();

## License

This project is still in an early state of development, and I want to retain the ability to refactor it as I see fit, so, for now:

**No License**. (All rights reserved.)

For more information, see [LICENSE.md](LICENSE.md)

## Coding style

This project is written using _**my very own™**_ coding style.

More information: [michael.gr - On Coding Style](https://blog.michael.gr/2018/04/on-coding-style.html)

----------------------
Cover image: The MikeNakis.CSharpTypeNames logo, by Mike Nakis.
