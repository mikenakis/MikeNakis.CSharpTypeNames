# MikeNakis.CSharpTypeNames<br/><sub><sup>A tiny library that generates the human-readable name of any System.Type in C# notation.</sup></sub>

<p align="center">
  <img title="MikeNakis.CSharpTypeNames Logo" src="MikeNakis.CSharpTypeNames-Logo.svg" width="256" />
</p>

[![Build](https://github.com/mikenakis/MikeNakis.CSharpTypeNames/actions/workflows/github-workflow.yml/badge.svg)](https://github.com/mikenakis/MikeNakis.CSharpTypeNames/actions/workflows/github-workflow.yml)

For an in-depth explanation of the problem that this library solves, see the following blog post:

 - [michael.gr - Human-readable names of DotNet types in C# notation](https://blog.michael.gr/2025/05/human-readable-names-of-dotnet-types-in.html)

## How to use

    using MikeNakis.CSharpTypeNames.Extensions;

	    string typeName = typeof( int ).GetCSharpName();

The method accepts an optional parameter with customization options, allowing you to specify how you would like it to
generate the type name.

## License

**All rights reserved.**

For more information, see [LICENSE.md](LICENSE.md)

## Coding style

This project is written using _**my very own™**_ coding style.

More information: [michael.gr - On Coding Style](https://blog.michael.gr/2018/04/on-coding-style.html)

TODO:

- Add handling for tuples
- Add the ability to specify whether to emit generic parameter names. (Default is to leave blank.)
- Add the ability to specify whether `Sys.Nullable<T>` should be replaced with `T?` (separately from `useAliases`)
- See if the code can be further simplified. (Might be able to merge `recurse2()` into `recurse()`)

----------------------
Cover image: The MikeNakis.CSharpTypeNames logo, by Mike Nakis.
