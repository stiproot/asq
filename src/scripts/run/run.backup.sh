#!/bin/bash

echo "Attempting to run project"

cd ../../src/api/asqapi/
dotnet run
cd ../../scripts/

cd ../../src/ui/asq-ui/
ng serve --open
cd ../../scripts/

cd ../server/
node index.js
cd ../scripts/

