# Cake.Figlet

Cake.Figlet adds FIGlet font support to Cake allowing superfulous Ascii art to be added
to your build scripts easily.

[![screenshot](docs/cake.figlet.png)]

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

Figlet code is based heavilty from [Philippe Auriou's FIGlet library](https://github.com/auriou/FIGlet)

