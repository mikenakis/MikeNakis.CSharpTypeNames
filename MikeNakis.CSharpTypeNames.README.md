# MikeNakis.CSharpTypeNames<br/><sub><sup>A tiny library that generates the human-readable name of any System.Type in C# notation.</sup></sub>

<p align="center">
  <img title="MikeNakis.CSharpTypeNames Logo" src="MikeNakis.CSharpTypeNames-Logo.svg" width="256" />
</p>

[![Build](https://github.com/mikenakis/MikeNakis.CSharpTypeNames/actions/workflows/CSharpTypeNames-github-workflow.yml/badge.svg)](https://github.com/mikenakis/MikeNakis.CSharpTypeNames/actions/workflows/CSharpTypeNames-github-workflow.yml)

For an in-depth explanation of the problem that this library solves, see the following blog post:

 - [michael.gr - Human-readable names of DotNet types in C# notation](https://blog.michael.gr/2025/05/human-readable-names-of-dotnet-types-in.html)

## How to use

    using MikeNakis.CSharpTypeNames.Extensions;

	    string typeName = typeof( int ).GetCSharpName();

If you would like to have fine control over exactly how the name is generated, see class 
`MikeNakis.CSharpTypeNames.Generator`, method `GetCSharpTypeName()` which accepts customization options.

## License

**All rights reserved.**

For more information, see [LICENSE.md](LICENSE.md)

## Coding style

This project is written using _**my very own™**_ coding style.

More information: [michael.gr - On Coding Style](https://blog.michael.gr/2018/04/on-coding-style.html)

----------------------
Cover image: The MikeNakis.CSharpTypeNames logo, by Mike Nakis.
