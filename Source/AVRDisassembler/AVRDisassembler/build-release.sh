#!/usr/bin/env bash

# @echo off

appname=AVRDisassembler
version=0.2.1

echo "**** Building ${appname} release ${version} ****"

echo -- Cleaning bin/dist ...
rm -rf ./bin/dist/*

echo -- Building...
dotnet clean
dotnet restore
dotnet build -c release

echo -- Publishing ...

dotnet publish -c release -r win7-x64 -o bin/dist/${appname}-${version}-Windows-x64
dotnet publish -c release -r ubuntu.21.04-x64 -o bin/dist/${appname}-${version}-Ubuntu.21.04-x64
dotnet publish -c release -r ubuntu.21.04-x64 -o bin/dist/${appname}-${version}-Ubuntu.21.04-x64
dotnet publish -c release -r centos.7-x64 -o bin/dist/${appname}-${version}-Centos.7-x64
dotnet publish -c release -r debian.8-x64 -o bin/dist/${appname}-${version}-Debian.8-x64
dotnet publish -c release -r osx.10.10-x64 -o bin/dist/${appname}-${version}-OSX-Yosemite-x64
dotnet publish -c release -r osx.10.11-x64 -o bin/dist/${appname}-${version}-OSX-ElCapitan-x64
dotnet publish -c release -r osx.10.12-x64 -o bin/dist/${appname}-${version}-OSX-Sierra-x64

cd bin
cd dist

echo -- Zipping ...
7z a ${appname}-${version}-Windows-x64.zip ${appname}-${version}-Windows-x64
7z a ${appname}-${version}-Ubuntu.21.04-x64.zip ${appname}-${version}-Ubuntu.21.04x64
7z a ${appname}-${version}-Ubuntu.21.04-x64.zip ${appname}-${version}-Ubuntu.21.04-x64
7z a ${appname}-${version}-Centos.7-x64.zip ${appname}-${version}-Centos.7-x64
7z a ${appname}-${version}-Debian.8-x64.zip ${appname}-${version}-Debian.8-x64
7z a ${appname}-${version}-OSX-Yosemite-x64.zip ${appname}-${version}-OSX-Yosemite-x64
7z a ${appname}-${version}-OSX-ElCapitan-x64.zip ${appname}-${version}-OSX-ElCapitan-x64
7z a ${appname}-${version}-OSX-Sierra-x64.zip ${appname}-${version}-OSX-Sierra-x64

cd ..
cd ..

echo All done!