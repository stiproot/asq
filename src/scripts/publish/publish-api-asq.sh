#!/bin/bash

cd /home/simon/Code/projects/asq/src/api/asqapi
dotnet clean
dotnet publish --configuration Release
cd bin/Release
rm netcoreapp3.1/appsettings.json
rm netcoreapp3.1/appsettings.Development.json
scp -r netcoreapp3.1/ root@197.242.147.62:/home/asq/api
