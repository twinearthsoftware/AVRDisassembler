# AVRDisassembler
A .NET Core (cross platform) ATMEL AVR / Arduino (Intel HEX) Disassembler. 

![AVRDisassembler](https://github.com/christophediericx/AVRDisassembler/blob/master/Images/AVRDisassembler.png)

## Latest binaries ##

| Operating System | Release |
| ---------------- | ------- |
| **Windows x64 (7-8-8.1-10)**      | [AVRDisassembler-0.2.1-Windows-x64.zip](https://github.com/christophediericx/AVRDisassembler/releases/download/v0.2.1/AVRDisassembler-0.2.1-Windows-x64.zip) |
| **Linux**        | [AVRDisassembler-0.2.1-Ubuntu.21.04-x64.zip](https://github.com/christophediericx/AVRDisassembler/releases/download/v0.2.1/AVRDisassembler-0.2.1-Ubuntu.21.04-x64.zip) |
|                  | [AVRDisassembler-0.2.1-Ubuntu.21.04-x64.zip](https://github.com/christophediericx/AVRDisassembler/releases/download/v0.2.1/AVRDisassembler-0.2.1-Ubuntu.21.04-x64.zip) |
|                  | [AVRDisassembler-0.2.1-Centos.7-x64.zip](https://github.com/christophediericx/AVRDisassembler/releases/download/v0.2.1/AVRDisassembler-0.2.1-Centos.7-x64.zip) |
|                  | [AVRDisassembler-0.2.1-Debian.8-x64.zip](https://github.com/christophediericx/AVRDisassembler/releases/download/v0.2.1/AVRDisassembler-0.2.1-Debian.8-x64.zip) |
| **OS X**         | [AVRDisassembler-0.2.1-OSX-Sierra-x64.zip](https://github.com/christophediericx/AVRDisassembler/releases/download/v0.2.1/AVRDisassembler-0.2.1-OSX-Sierra-x64.zip) |
|                  | [AVRDisassembler-0.2.1-OSX-ElCapitan-x64](https://github.com/christophediericx/AVRDisassembler/releases/download/v0.2.1/AVRDisassembler-0.2.1-OSX-ElCapitan-x64.zip) |
|                  | [AVRDisassembler-0.2.1-OSX-Yosemite-x64.zip](https://github.com/christophediericx/AVRDisassembler/releases/download/v0.2.1/AVRDisassembler-0.2.1-OSX-Yosemite-x64.zip) |


## How to use from the command line ##

Run the application without arguments to print its usage:

Windows:
```
AVRDisassembler.exe
```

Linux or OSX:
```
./AVRDisassembler
```

```
AVRDisassembler 0.2.1
Christophe Diericx

ERROR(S):
  Required option 'i, input' is missing.

  -i, --input    Required. Input file (HEX) to be disassembled.

  --json         (Default: false) Format output as JSON.

  --help         Display this help screen.

  --version      Display version information.
```

Only a single parameter (an input file) is required. At the moment only **HEX** files are supported (this also means the disassembler currently can't interleave source or debugging information as found in an ELF file).

## How to use as a library ##

**Note:** *This package has not been marked as stable. The reason for that is mostly that one of its dependencies (CommandLineParser) is currently still marked as prerelease*

[![NuGet version](https://badge.fury.io/nu/AVRDisassembler.svg)](https://badge.fury.io/nu/AVRDisassembler)

Link the following nuget package in your project in order to use the AVRDisassembler: https://www.nuget.org/packages/AVRDisassembler/

Alternatively, install the package using the nuget package manager console:

```
Install-Package AVRDisassembler -Pre
```

## How to build##

Preconditions: ```7z``` and ```dotnet``` [https://dotnet.microsoft.com/download/dotnet/3.1](3.1)



```bash

cd AVRDisassembler/Source/AVRDissasembler/AVRDissasembler

./build-release.sh # on linux
./build-release.bat # on Windows

```

the binaries will be located in ```AVRDisassembler/Source/AVRDisassembler/AVRDisassembler/bin/dist```

## Sample output ##
By default AVRDissassembler will write to stdout, making it easy to pipe output to a file.
```
...
0FE4:   50-93-2B-02     sts 0x022b, r21       ; Store Direct to Data Space
0FE8:   40-93-2A-02     sts 0x022a, r20       ; Store Direct to Data Space
0FEC:   F9-01           movw r30, r18         ; Copy Register Word
0FEE:   E6-56           subi r30, 0x66        ; Subtract Immediate
0FF0:   FF-4F           sbci r31, 0xff        ; Subtract Immediate with Carry SBI
0FF2:   24-91           lpm r18, Z            ; Load Program Memory
0FF4:   20-93-29-02     sts 0x0229, r18       ; Store Direct to Data Space
0FF8:   31-2C           mov r3, r1            ; Copy Register
0FFA:   53-C0           rjmp .+166            ; Relative Jump
0FFC:   10-92-80-00     sts 0x0080, r1        ; Store Direct to Data Space
1000:   10-92-81-00     sts 0x0081, r1        ; Store Direct to Data Space
1004:   90-91-81-00     lds r25, 0x0081       ; Load Direct from Data Space (32-bit)
1008:   98-60           ori r25, 0x08         ; Logical OR with Immediate
100A:   90-93-81-00     sts 0x0081, r25       ; Store Direct to Data Space
100E:   90-91-81-00     lds r25, 0x0081       ; Load Direct from Data Space (32-bit)
1012:   91-60           ori r25, 0x01         ; Logical OR with Immediate
1014:   90-93-81-00     sts 0x0081, r25       ; Store Direct to Data Space
1018:   28-2F           mov r18, r24          ; Copy Register
101A:   30-E0           ldi r19, 0x00         ; Load Immediate
101C:   F9-01           movw r30, r18         ; Copy Register Word
101E:   E2-55           subi r30, 0x52        ; Subtract Immediate
1020:   FF-4F           sbci r31, 0xff        ; Subtract Immediate with Carry SBI
1022:   E4-91           lpm r30, Z            ; Load Program Memory
1024:   F0-E0           ldi r31, 0x00         ; Load Immediate
1026:   EE-0F           add r30, r30          ; Add without Carry
1028:   FF-1F           adc r31, r31          ; Add with Carry
102A:   E4-53           subi r30, 0x34        ; Subtract Immediate
102C:   FF-4F           sbci r31, 0xff        ; Subtract Immediate with Carry SBI
102E:   45-91           lpm r20, Z+           ; Load Program Memory
1030:   54-91           lpm r21, Z            ; Load Program Memory
1032:   50-93-24-02     sts 0x0224, r21       ; Store Direct to Data Space
1036:   40-93-23-02     sts 0x0223, r20       ; Store Direct to Data Space
103A:   F9-01           movw r30, r18         ; Copy Register Word
103C:   E6-56           subi r30, 0x66        ; Subtract Immediate
103E:   FF-4F           sbci r31, 0xff        ; Subtract Immediate with Carry SBI
1040:   24-91           lpm r18, Z            ; Load Program Memory
1042:   20-93-22-02     sts 0x0222, r18       ; Store Direct to Data Space
...
```
Optionally, one can also specify the **--json** parameter in order to emit an object tree to stdout instead. Although this option is made available (and beats having to reparse text as generated by the disassembler), people using .NET would be far better off directly consuming the library (with this option allowing for easier interopability with other languages).

```
... 
  },
  {
    "OriginalBytes": [
      141,
      127
    ],
    "OpCode": {
      "Comment": "Logical AND with Immediate",
      "Size": "_16",
      "Name": "ANDI"
    },
    "Operands": [
      {
        "Type": "DestinationRegister",
        "RepresentationMode": "Register",
        "Bytes": null,
        "Value": 24,
        "Increment": false,
        "Decrement": false,
        "Displacement": false
      },
      {
        "Type": "ConstantData",
        "RepresentationMode": "Hexadecimal",
        "Bytes": null,
        "Value": 253,
        "Increment": false,
        "Decrement": false,
        "Displacement": false
      }
    ],
    "Offset": 5012
  },
  ...
  ```


## Changelog ##
```
0.2.1   Switch to netcoreapp3.1
0.2.0   Release binaries, build script.
0.1.1   Fix bugs in operand parsing for CBR, CLR. Add operand extraction tests.
0.1.0   Initial release.
```
