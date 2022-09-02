#!/bin/bash

cd /home/simon/Code/projects/asq/src/workers/EventEngine
dotnet clean
dotnet publish --configuration Release
cd bin/Release
scp -r netcoreapp3.1/ root@197.242.147.62:/home/asq/workers/mail
