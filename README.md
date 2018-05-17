# Cake.Figlet

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

