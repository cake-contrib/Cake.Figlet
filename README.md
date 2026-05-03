# Cake.Figlet

> [!IMPORTANT]
> **This addin is no longer needed and will be archived.**
>
> Since [Cake 1.0.0](https://github.com/cake-build/cake/releases/tag/v1.0.0)
> (released 2021-02-07), Cake ships
> [Spectre.Console](https://spectreconsole.net/) as a direct dependency
> of `Cake.Cli`, which provides a `FigletText` widget out of the box.
> Any `.cake` script running on Cake &ge; 1.0.0 can produce FIGlet
> output without referencing this addin:
>
> ```csharp
> using Spectre.Console;
>
> AnsiConsole.Write(new FigletText("Cake").LeftJustified().Color(Color.Red));
> ```
>
> Existing consumers can continue to use Cake.Figlet on the published
> NuGet package, but no further releases are planned and the
> repository will be archived once this notice is merged. New build
> scripts should use `Spectre.Console.FigletText` directly.

Cake.Figlet adds FIGlet font support to Cake allowing superfulous Ascii art to be added
to your build scripts easily.

![screenshot](docs/cake.figlet.PNG)

[![Build status](https://ci.appveyor.com/api/projects/status/3l0xm56cpakmiu2c/branch/master?svg=true)](https://ci.appveyor.com/project/enkafan/cake-figlet/branch/master) [![NuGet version](https://badge.fury.io/nu/cake.figlet.svg)](https://www.nuget.org/packages/cake.figlet) [![CodeFactor](https://www.codefactor.io/repository/github/cake-contrib/cake.figlet/badge)](https://www.codefactor.io/repository/github/cake-contrib/cake.figlet)

## Referencing

Reference the library directly in your build script via a cake addin directive:

```
#addin "Cake.Figlet"
```

## Usage

```
Setup(ctx => {
    Information("");
    Information(Figlet("Cake.Figlet"));
});
```

This will output
```
----------------------------------------
Setup
----------------------------------------
Executing custom setup action...

  ____         _              _____  _         _        _
 / ___|  __ _ | | __  ___    |  ___|(_)  __ _ | |  ___ | |_
| |     / _` || |/ / / _ \   | |_   | | / _` || | / _ \| __|
| |___ | (_| ||   < |  __/ _ |  _|  | || (_| || ||  __/| |_
 \____| \__,_||_|\_\ \___|(_)|_|    |_| \__, ||_| \___| \__|
                                        |___/

```

## Thanks

Figlet code is based heavily from [Philippe Auriou's FIGlet library](https://github.com/auriou/FIGlet)

