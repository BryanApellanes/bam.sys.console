#!/bin/bash

pushd ..
~/.dotnet/dotnet restore
~/.dotnet/dotnet build -o ./dist
popd