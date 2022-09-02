#!/bin/bash

cd /home/simon/Code/projects/asq/src/ui/asq-ui
ng build --prod
cd dist
#scp -r asq-ui/ root@197.242.147.62:/home/asq/ui
scp -r asq-ui/ root@197.242.147.62:/var/www
