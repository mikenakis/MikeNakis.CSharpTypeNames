# MikeNakis.CSharpTypeNames<br/><sub><sup>A tiny library that computes the name of any System.Type in human-readable C# notation.</sup></sub>

<p align="center">
  <img title="MikeNakis.CSharpTypeNames Logo" src="MikeNakis.CSharpTypeNames-Logo.svg" width="256" />
</p>

[![Build](https://github.com/mikenakis/MikeNakis.CSharpTypeNames/actions/workflows/github-workflow.yml/badge.svg)](https://github.com/mikenakis/MikeNakis.CSharpTypeNames/actions/workflows/github-workflow.yml)

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
