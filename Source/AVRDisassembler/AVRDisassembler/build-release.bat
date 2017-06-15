@echo off

set appname=AVRDisassembler
set version=0.2

echo **** Building %appname% release %version% ****

echo -- Cleaning bin/dist ...
del /S /F /Q .\bin\dist\*

echo -- Building...
dotnet clean
dotnet restore
dotnet build -c release

echo -- Publishing ...

dotnet publish -c release -r win7-x64 -o bin/dist/%appname%-%version%-win-x64
dotnet publish -c release -r ubuntu.16.04-x64 -o bin/dist/%appname%-%version%-ubuntu.16.04-x64
dotnet publish -c release -r ubuntu.16.10-x64 -o bin/dist/%appname%-%version%-ubuntu.16.10-x64
dotnet publish -c release -r centos.7-x64 -o bin/dist/%appname%-%version%-centos.7-x64
dotnet publish -c release -r debian.8-x64 -o bin/dist/%appname%-%version%-debian.8-x64
dotnet publish -c release -r osx.10.10-x64 -o bin/dist/%appname%-%version%-OSX-Yosemite-x64
dotnet publish -c release -r osx.10.11-x64 -o bin/dist/%appname%-%version%-OSX-ElCapitan-x64
dotnet publish -c release -r osx.10.12-x64 -o bin/dist/%appname%-%version%-OSX-Sierra-x64

cd bin
cd dist

echo -- Zipping ...
7z a %appname%-%version%-win-x64.zip %appname%-%version%-win-x64
7z a %appname%-%version%-ubuntu.16.04-x64.zip %appname%-%version%-ubuntu.16.04-x64
7z a %appname%-%version%-ubuntu.16.10-x64.zip %appname%-%version%-ubuntu.16.10-x64
7z a %appname%-%version%-centos.7-x64.zip %appname%-%version%-centos.7-x64
7z a %appname%-%version%-debian.8-x64.zip %appname%-%version%-debian.8-x64
7z a %appname%-%version%-OSX-Yosemite-x64.zip %appname%-%version%-OSX-Yosemite-x64
7z a %appname%-%version%-OSX-ElCapitan-x64.zip %appname%-%version%-OSX-ElCapitan-x64
7z a %appname%-%version%-OSX-Sierra-x64.zip %appname%-%version%-OSX-Sierra-x64

cd ..
cd ..

echo All done!