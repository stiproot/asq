#!/bin/bash

echo "Attempting database creation..."

sudo mysql -e "DROP DATABASE if exists ASQ_TEST;"

sudo mysql -e "DROP DATABASE if exists ASQ;"
sudo mysql -e "create database ASQ;"

sudo mysql ASQ < ./tables/user-creation.mysql.sql
sudo mysql ASQ < ./tables/meeting-creation.mysql.sql
sudo mysql ASQ < ./tables/blog-post-creation.mysql.sql
sudo mysql ASQ < ./tables/notification-creation.mysql.sql
sudo mysql ASQ < ./tables/video-creation.mysql.sql

sudo mysql ASQ < ./lookups/insert-account-type.mysql.sql
sudo mysql ASQ < ./lookups/insert-card-type.mysql.sql
sudo mysql ASQ < ./lookups/insert-focus.mysql.sql
sudo mysql ASQ < ./lookups/insert-meeting-status.mysql.sql
sudo mysql ASQ < ./lookups/insert-notification-type.mysql.sql
sudo mysql ASQ < ./lookups/insert-timezones.mysql.sql

sudo mysql ASQ < ./procs/pr-get-meeting-by-ext-meeting-id.mysql.sql

#sudo mysql ASQ < ../db/mysql/test/test-data-notification.mysql.sql






